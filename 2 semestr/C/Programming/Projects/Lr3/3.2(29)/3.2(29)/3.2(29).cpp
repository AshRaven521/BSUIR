#include <stdio.h>
#include <malloc.h>
#include <stdlib.h>
#include <stdint.h>

void vyvod(int **,int );

int main()
{
	system("chcp 1251");
	system("cls");

	int** arr = NULL;
	int i, j, n, temp, sum1 = 0, sum2 = 0, sum3 = 0, sum4 = 0;

	printf("Введите количество строк в квадратной матрице: ");
	scanf_s("%d", &n);

		
	//выделение памяти
	arr = (int**)malloc(sizeof(int*)*n);
	for(i = 0; i < n; i++)
	{
		arr[i] = (int*)malloc(sizeof(int)*n);
	}

	for (i = 0; i < n; i++)  
	{
		for (j = 0; j < n; j++)  
		{
			printf("a[%d][%d] = ", i, j);
			scanf_s("%d", &(arr[i][j]));
		}
	}

	vyvod(arr, n);

	for (i = 0; i < n; i++) 
	{
		for (j = 0; j < n; j++) 
		{
			if (j > i && j < n - 1 - i)
			{
				sum1 = sum1 + arr[i][j];
				temp = arr[i][j];
				arr[i][j] = arr[n - 1- i][j];
				arr[n - 1 - i][j] = temp;
				sum2 = sum2 + arr[i][j];
			}

			if (i > j && i < n - 1 - j)
			{
				sum3 = sum3 + arr[i][j];
				temp = arr[i][j];
				arr[i][j] = arr[i][n - 1 -j];
				arr[i][n - 1 - j] = temp;
				sum4 = sum4 + arr[i][j];
			}
		}
	}

	printf("\n\n\n");

	
	vyvod(arr, n);

	printf("\nСумма первой четверти=%5d\nСумма второй четверти=%5d\nСумма третей четверти=%5d\nСумма четвертой четверти=%5d\n", sum1, sum2, sum3, sum4);

	free(arr);
	int r=getchar();//getchar записывается в переменную для устранения предупреждения   
	return 0;
}

void vyvod(int** arr, int n)
{
	for (int i = 0; i < n; i++)
	{
		for (int j = 0; j < n; j++)
		{
			printf("%5d ", arr[i][j]);
		}
		printf("\n");
	}
}