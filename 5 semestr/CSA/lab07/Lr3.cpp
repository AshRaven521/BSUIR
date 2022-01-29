#include <iostream>
#include <omp.h>
#include <string>
using namespace std;

int** serialMatrixMultiple(int** a, int** b, int rows, int columns);

int** OMPMatrixMultiple(int** a, int** b, int rows, int columns);


int main() {
	
	const int rows = 1000;
	const int columns = 1000;
	int** a = new int*[rows];
	int** b = new int*[rows];
	
	srand(time(0));
	
	for (int i = 0; i < rows; i++)
	{
		a[i] = new int[columns];
		b[i] = new int[columns];
		for (int j = 0; j < columns; j++)
		{
			a[i][j] = rand() % 21 - 5;
			b[i][j] = rand() % 21 - 5;
		}
	}

	auto t1 = omp_get_wtime();
	serialMatrixMultiple(a, b, rows, columns);
	auto t2 = omp_get_wtime();

	cout << "Serial: " <<"\t" << "period= " << t2 - t1 << endl;

	t1 = omp_get_wtime();
	OMPMatrixMultiple(a, b, rows, columns);
	t2 = omp_get_wtime();

	cout << "Parallel: " <<"\t" << "period= " << t2 - t1 << endl;


	delete a, b;
	return 0;
}


int** serialMatrixMultiple(int** a, int** b, int rows, int columns) 
{
	int** resultMatrix = new int* [rows];
	for (int i = 0; i < rows; i++)
	{
		resultMatrix[i] = new int[columns];

		for (int j = 0; j < columns; j++)
		{
			resultMatrix[i][j] = 0;
			int local_sum = 0;
			for (int k = 0; k < columns; k++)
			{
				local_sum += a[i][k] * b[k][j];
			}
			resultMatrix[i][j] = local_sum;
		}
	}

	return resultMatrix;
}

int** OMPMatrixMultiple(int** a, int** b, int rows, int columns)
{
	int** resultMatrix = new int* [rows];
	
#pragma omp parallel
	{
		for (int i = 0; i < rows; i++)
		{
			resultMatrix[i] = new int[columns];

			for (int j = 0; j < columns; j++)
			{
				resultMatrix[i][j] = 0;
				int local_sum = 0;
			
				#pragma omp for
				for (int k = 0; k < columns; k++)
				{
					local_sum += a[i][k] * b[k][j];
				}
				
				// atomic - атомарная операция, при которой работает один поток. Каждый поток по очереди записывает свое значение local_sum в resultMatrix
				//Можно убрать omp for и тогда убрать +
				#pragma omp atomic
				resultMatrix[i][j] += local_sum;
			}
		}
	}

	return resultMatrix;
}
