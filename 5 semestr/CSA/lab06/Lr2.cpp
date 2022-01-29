#include <iostream>
#include <thread>
#include <mutex>
#include <vector>
#include <list>
#include <condition_variable>
#include <atomic>
#include <queue>

using namespace std;

#pragma region Task1
int mutex_counter = 0;
atomic<int> atomic_counter(0);
mutex c_mutex;

void AddToIndex(int* iA, bool bMethod, bool sleep, int num) 
{
    int counter=0;
    for (;;) // бесконечный цикл
    {
        if (bMethod == false) //"mutex" 
        {
            c_mutex.lock();
            counter = mutex_counter++;
            c_mutex.unlock();
        }
        else 
        {
            counter = atomic_counter++;
        }
        if (counter >= num)
        {
            break;
        }
            
        iA[counter]++;
        if (sleep)
        {
            this_thread::sleep_for(chrono::nanoseconds(10));
        }
            
    }
}

void taskSet(bool bSleep, bool bMethod) 
{
    int mistakes = 0;
    int num_threads[4] = { 4, 8, 16, 32 };
    vector<thread> threads;
    const int num_tasks = 1024 * 1024;
    int* index_array = new int[num_tasks];
    
    for (int k = 0; k < 4; k++)
    {
        mutex_counter = 0;
        atomic_counter = 0;
        for (int i = 0; i < num_tasks; i++)
        {
            index_array[i] = 0;
        }
            
        threads.clear();
        
        unsigned int start = clock();
        for (int j = 0; j < num_threads[k]; j++) 
        {
            threads.emplace_back(AddToIndex, index_array, bMethod, bSleep, num_tasks); // Передаем в вектор потоков функцию с ее параметрами, тем самым запуская поток
        }
        for (int i = 0; i < num_threads[k]; i++)
        {
            threads[i].join(); // Говорим основному потоку дождаться завершения остальных потоков
        }
        unsigned int stop = clock();
        
        cout << num_threads[k] << " threads period=" << stop - start;
         
        for (int i = 0; i < num_tasks; i++)
        {
            if (index_array[i] != 1)
            {
                mistakes++;
            }
        }
        cout << "\t" << "Mistakes:" << mistakes << endl;
    }
}
#pragma endregion


#pragma region Task2
const int queue_sizes[] = { 1, 4, 16 };

class Dynamic 
{
private:
    queue<unsigned char> queue;
    mutex mutex;
public:
    void push(unsigned char item)
    {
        mutex.lock();
        queue.push(item);
        mutex.unlock();
    }

    bool pop(unsigned char& item)
    {
        mutex.lock();
        if (queue.empty()) 
        {
            mutex.unlock();
            this_thread::sleep_for(chrono::milliseconds(1));
            mutex.lock();
            if (queue.empty()) 
            {
                mutex.unlock();
                return false;
            }
            else 
            {
                item = queue.front();
                queue.pop();
                mutex.unlock();
                return true;
            }
        }
        else 
        {
            item = queue.front();
            queue.pop();
            mutex.unlock();
            return true;
        }
    }
};

class Static 
{
private:
    int size;
    vector<unsigned char> queue;
    atomic<int> queue_tail, queue_amount;
    condition_variable condition;
    mutex mutex;
public:
    Static(int length) 
    {
        this->size = length;
        for (int i = 0; i < length; i++)
        {
            queue.push_back(0);
        }
        queue_tail = 0;
        queue_amount = 0;
    }

    void push(unsigned char item)
    {
        unique_lock<std::mutex> lock(mutex);
        condition.wait(lock, [&] { return queue_amount < size; });
        queue.at(queue_tail % size) = item;
        queue_tail++;
        queue_amount++;
        condition.notify_all();
    }

    bool pop(unsigned char& item)
    {
        unique_lock<std::mutex> lock(mutex);
        if (queue_amount == 0) 
        {
            condition.wait_for(lock, chrono::milliseconds(1), [&] {return queue_amount > 0; });
            if (queue_amount == 0) 
            {
                return false;
            }
            else 
            {
                item = queue.at(0);
                for (int i = 0; i < queue_tail % size; i++)
                {
                    queue.at(i) = queue.at(i + 1);
                }
                queue_tail--;
                queue.at(queue_tail % size) = 0;
                queue_amount--;
                condition.notify_all();
                return true;
            }
        }
        else 
        {
            item = queue.at(0);
            for (int i = 0; i < queue_tail % size; i++)
            {
                queue.at(i) = queue.at(i + 1);
            }
            queue_tail--;
            queue.at(queue_tail % size) = 0;
            queue_amount--;
            condition.notify_all();
            return true;
        }
    }
};

void test_dynamic(Dynamic& queue, int producer_num, int consumer_num) 
{
    const int task_num = 4 * 1024 * 1024;
    atomic<int> sum = 0;
    thread* producer_threads = new thread[producer_num];
    thread* consumer_threads = new thread[consumer_num];

    unsigned int start = clock();

    for (int i = 0; i < producer_num; i++) 
    {
        producer_threads[i] = thread([&queue, task_num] 
            {
                for (int j = 0; j < task_num; j++)
                {
                    queue.push(1);
                }
            });
    }

    for (int i = 0; i < consumer_num; i++) 
    {
        consumer_threads[i] = thread([&queue, &sum, task_num, producer_num] 
            {
                unsigned char item = 0;
                while (sum < task_num * producer_num) 
                {
                    if (queue.pop(item))
                    {
                        sum += item;
                    }
                }
            });
    }

    for (int i = 0; i < producer_num; i++)
    {
        producer_threads[i].join();
    }
    for (int i = 0; i < consumer_num; i++)
    {
        consumer_threads[i].join();
    }

    unsigned int stop = clock();
    
    cout << "ProducerNum: " << producer_num << " ConsumerNum: " << consumer_num << endl;
    cout << "Sum= " << sum << " (ProducerNum*TaskNum)= " << task_num * producer_num << endl;
    cout << "Period= " << stop - start << endl << endl;
}

void test_static(Static& queue, int producer_num, int consumer_num)
{
    const int task_num = 4* 1024 * 1024;
    atomic<int> sum = 0;
    thread* producer_threads = new thread[producer_num];
    thread* consumer_threads = new thread[consumer_num];

    unsigned int start = clock();

    for (int i = 0; i < producer_num; i++)
    {
        producer_threads[i] = thread([&queue, task_num]
            {
                for (int j = 0; j < task_num; j++)
                {
                    queue.push(1);
                }
            });
    }

    for (int i = 0; i < consumer_num; i++)
    {
        consumer_threads[i] = thread([&queue, &sum, task_num, producer_num]
            {
                unsigned char item = 0;
                while (sum < task_num * producer_num)
                {
                    if (queue.pop(item))
                    {
                        sum += item;
                    }
                }
            });
    }

    for (int i = 0; i < producer_num; i++)
    {
        producer_threads[i].join();
    }
    for (int i = 0; i < consumer_num; i++)
    {
        consumer_threads[i].join();
    }

    unsigned int stop = clock();

    cout << "ProducerNum: " << producer_num << " ConsumerNum: " << consumer_num << endl;
    cout << "Sum: " << sum << " (ProducerNum*TaskNum)= " << task_num * producer_num << endl;
    cout << "Period= " << stop - start << endl << endl;
}

#pragma endregion


int main() 
{
    //Task 1
    cout << "Task 1" << endl;
    cout << "Mutex without sleep:" << endl;
    taskSet(false, false);
    cout << "\nMutex with sleep:" << endl;
    taskSet(true, false);
        
    cout << endl << "Atomic without sleep:" << endl;
    taskSet(false, true);
    cout << "\nAtomic with sleep:" << endl;
    taskSet(false, true);
    //Task 1 end

    //Task 2
    cout << endl << endl << "Task 2" << endl;
    cout << endl << "Dynamic queue" << endl;
    Dynamic dynamic_queue;
    for (int i = 1; i <= 4; i *= 2)
    {
        for (int j = 1; j <= 4; j *= 2)
        {
            test_dynamic(dynamic_queue, i, j);

        }

    }

    cout << endl << "Static queue" << endl;
    for (auto queueLength : queue_sizes) 
    {
        cout << "Length: " << queueLength << endl;
        Static staticQueue(queueLength);
        for (int i = 1; i <= 4; i *= 2)
        {
            for (int j = 1; j <= 4; j *= 2)
            {
                test_static(staticQueue, i, j);
            }
        }
    }
    //Task 2 end
   

    return 0;
}
