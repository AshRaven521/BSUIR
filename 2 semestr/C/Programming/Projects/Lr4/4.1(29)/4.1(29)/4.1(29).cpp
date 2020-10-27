#include <malloc.h>
#include <stdio.h>
#include <stdlib.h>

void vyvod(char**, int,int);

int main()
{
	system("chcp 1251");
	system("cls");

	char** arr = NULL;
	int tmp, i, j=0, n=0,sum=0,count=0;
	const int row = 3;//row-кол-во строк
	const int symbols = 20;
	char ch;
	printf("Какое количество матриц вы хотите ввести n=");
	scanf_s("%d", &n);
	getchar();

	//выделение памяти
	arr = (char**)malloc(sizeof(char*) * symbols);
	for (i = 0; i < row; i++)
	{
		arr[i] = (char*)malloc(sizeof(char) * row);
	}
	for (int k = 0; k < n; k++)
	{
		printf("\nМатрица №%d:\n", k + 1);

		//ввод матрицы
		for (int i = 0; i < row; i++)
		{
			sum = 0;
			count = 0;//счетчик кол-ва символов
			printf("Введите слово №%d:", i + 1);
			j = 0;
			ch = ' ';
			while ((ch=getchar()) != '\n' && j<symbols-1)
			{
				if (j >= symbols - 1)
				{
					printf("Введено слишком много символов,ввод прекращен\n");
					break;
				}
				else
				{
					arr[i][j] = ch;
					j++;
					sum += (int)ch;//сумма кодов символов

					count++;
				}
			}

			arr[i][count] = '\0';

			if (sum % 2 == 0)
			{
				for (int m = 0; m < count/2;m++)
				{
					ch = arr[i][m];
					arr[i][m] = arr[i][count - 1 - m];
					arr[i][count - 1 - m] = ch;
				}
			}
		}
	    vyvod(arr,row,20);
	}
	free(arr);
	return 0;
}

void vyvod(char** arr, int x,int y)
{
	int j = 0;
	for (int i = 0; i < x; i++)
	{
		j = 0;
		while(arr[i][j]!='\0')
		{
			printf("%c ", arr[i][j]);
			j++;
		}
		printf("\n");
	}
}
