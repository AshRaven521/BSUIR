# Имитация системы массового обслуживания

import numpy as np
from math import log
from statistics import mean
from colorama import Fore, init

number_of_channels = 0
queue_size = 0
request_intensity = 0
service_intensity = 0
probability_to_serve_the_request = 0

service_channels = []
queue = []

done_requests = []
done_requests_ids = []
rejected_requests = []
cancelled_requests = []

current_time = 0
request_number = 0

time_interval = 0.01
time_for_new_request = 0

channels_and_queue_state = []
channels_state = []
queue_state = []

init(autoreset=True)


def start(n, m, lambda_, mu, p, run_life_time):
    """ Запускает систему массвого обслуживания(СМО) """
    global current_time, time_for_new_request

    init(n, m, lambda_, mu, p)
    create_new_request()
    time_for_new_request = request_time_generator() + current_time

    # Пока время работы СМО не закончилось
    while current_time <= run_life_time:
        current_time += time_interval
        process_queue()
        process_channels()

        get_statistic()

        if time_for_new_request <= current_time:
            create_new_request()
            time_for_new_request = request_time_generator() + current_time

    show_statistic()


def create_new_request():
    """ Создаст новую заявку, если есть места в очереди. Иначе заявка будет отклонена """
    global request_number
    request_number += 1

    request_id = request_number
    # Если количество мест очереди не превышает данное число, то
    if len(queue) < queue_size:
        # Запрос отправлен на обработку
        queue_request(request_id)
    else:
        # Запрос отклонен
        reject_request(request_id)


def queue_request(request_id):
    """ Добавляет заявку в очередь """
    queue.append((request_id, current_time))


def process_queue():
    """ Выполняет одну итерацию по очереди """
    free_channel_index = find_free_channel()

    # Если очередь пуста выходим из функции
    if len(queue) == 0:
        return

    # Если нет свободных каналов, выходим из функции
    if free_channel_index == -1:
        return

    request_id, start_time = queue.pop(0)
    time_in_queue = current_time - start_time
    serve_request(request_id, start_time, time_in_queue, free_channel_index)


def process_channels():
    """ Принимает заявку при наличии времени """
    for channel in service_channels:
        if channel is None:
            continue

        if channel[5] <= current_time:
            accept_request(channel)


def serve_request(request_id, start_time, time_in_queue, free_channel_index):
    """ Добавляет заявку на место пустого канала """
    serve_time = serve_time_generator()
    end_time = serve_time + current_time
    service_channels[free_channel_index] = \
        (request_id, start_time, time_in_queue, free_channel_index, serve_time, end_time)


def find_free_channel():
    """ Вернет индекс пустого канала. Если пустых каналов нет, вернет -1 """
    if service_channels.__contains__(None):
        return service_channels.index(None)

    return -1


def accept_request(request):
    """ С вероятностью p, заданной пользователем, принимает или отменяет заявку """
    free_channel(request[3])

    # Генерирует True или False с вероятностью p(диапазон)
    is_accept = np.random.choice([True, False],
                                 p=[probability_to_serve_the_request, 1 - probability_to_serve_the_request])

    if is_accept:
        done_request(request)
    else:
        cancel_request(request[0])
        try_to_process_request(request[0])


def done_request(request):
    """ Добавляет заявку в список выполненных """
    time_in_queuing_system = current_time - request[1]
    done_requests.append((request[0], request[2], request[4], time_in_queuing_system))


def reject_request(request_id):
    """ Добавляет заявку в список отклоненных"""
    rejected_requests.append(request_id)


def cancel_request(request_id):
    """ Добавляет заявку в список отмененных """
    cancelled_requests.append(request_id)


def free_channel(index):
    """ Освобождает канал по переданному индексу """
    service_channels[index] = None


def try_to_process_request(request_id):
    """ Пробует обработать заявку. Если очередь заполнена, то добавляет заявку в список отклоненных. Иначе добавляет
    заявку в очередь """
    if len(queue) < queue_size:
        queue_request(request_id)
    else:
        reject_request(request_id)


def init(n, m, lambda_, mu, p):
    """ Инициализирует значения """
    global number_of_channels, queue_size, request_intensity, \
        service_intensity, service_channels, probability_to_serve_the_request

    number_of_channels = n
    queue_size = m
    request_intensity = lambda_
    service_intensity = mu
    service_channels = [None for _ in range(n)]
    probability_to_serve_the_request = p


def request_time_generator():
    """ Возвращает время, затраченное на ожидание заявки в очереди """
    return exponential_value_generator(request_intensity)


def serve_time_generator():
    """ Возвращает время, затраченное на исполнение заявки """
    return exponential_value_generator(service_intensity)


def exponential_value_generator(intensity):
    return - 1 / intensity * log(1 - np.random.uniform())


def get_statistic():
    n = number_of_channels
    # Количество каналов - количество пустых каналов + длина очереди
    channels_and_queue_state.append(n - service_channels.count(None) + len(queue))
    # Количество каналов - количество пустых каналов
    channels_state.append(n - service_channels.count(None))
    # Длина очереди
    queue_state.append(len(queue))


def get_final_probabilities():
    final_probabilities = []

    n = number_of_channels
    m = queue_size

    n_ = len(channels_and_queue_state)
    for i in range(n + m + 1):
        p = channels_and_queue_state.count(i) / n_
        final_probabilities.append(p)

    return final_probabilities


def get_average_service_request_time():
    """ Возвращает среднее время затраченное на обработку заявки """
    average_service_request_time = []

    for i in range(len(done_requests)):
        average_service_request_time.append(done_requests[i][2])

    return mean(average_service_request_time)


def get_average_queue_time():
    """ Возвращает среднее время, которое заявка была в очереди """
    average_queue_time = []

    for i in range(len(done_requests)):
        average_queue_time.append(done_requests[i][1])

    return mean(average_queue_time)


def get_average_request_time_in_system():
    """ Возвращает среднее время заявки в системе """
    average_request_time_in_system = []

    for i in range(len(done_requests)):
        average_request_time_in_system.append(done_requests[i][3])

    return mean(average_request_time_in_system)


def show_statistic():
    """ Отображает полученные данные за время работы системы массового обслуживания """
    final_probabilities = get_final_probabilities()
    probability_of_idle_channels = final_probabilities[0]
    denial_of_service_probability = final_probabilities[-1]
    # Относительная пропускная способность - среднее число обслуженных заявок / среднее число поступивших заявок
    # за одно и тоже время
    relative_bandwidth = 1 - denial_of_service_probability
    # Абсолютная пропускная способность - среднее число заявок, которео может обслужить СМО за единицу времени
    absolute_bandwidth = request_intensity * relative_bandwidth
    average_channel_usage = mean(channels_state)
    average_number_of_request_in_queue = mean(queue_state)
    average_number_of_request_in_system = mean(channels_and_queue_state)
    average_service_request_time = get_average_service_request_time()
    average_queue_time = get_average_queue_time()
    average_request_time_in_system = get_average_request_time_in_system()

    print(f'{Fore.LIGHTGREEN_EX}Характеристики для очереди размером: {queue_size}')
    print(f'Финальные вероятности: {final_probabilities}')
    print(f'Вероятность простоя каналов: {probability_of_idle_channels}')
    print(f'Вероятность отказа обсуживания: {denial_of_service_probability} ')
    print(f'Относительная пропускная способность: {relative_bandwidth}')
    print(f'Абсолютная пропускная способность: {absolute_bandwidth}')
    print(f'Среднее число занятых каналов: {average_channel_usage}')
    print(f'Среднее число заявок в очереди: {average_number_of_request_in_queue}')
    print(f'Среднее число заявок в системе: {average_number_of_request_in_system}')
    print(f'Среднее время заявки под обслуживанием: {average_service_request_time}')
    print(f'Среднее время заявки в очереди: {average_queue_time}')
    print(f'Среднее время заявки в системе: {average_request_time_in_system}')
    print()
