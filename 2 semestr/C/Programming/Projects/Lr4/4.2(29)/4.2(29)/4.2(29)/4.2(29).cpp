#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <math.h>
#define N 40

int GetDigit(char* bb);
void bubble(int* data, int size);

int main(void) 
{
	system("chcp 1251");
	system("cls");

	char buffer[N+2];
	FILE* fp;
	int sum = 0,count = 0;
	char** arr;
	
	//открываем файл
	int err = fopen_s(&fp, "bytes.txt", "r");
	if (err != 0)
	{
		exit(1);
	}

	//считаем количество строк в файле
	printf("Строки из файла\n");
	while (fgets(buffer, N+2, fp) != NULL)
	{
		printf(buffer);
		count++;
	}
	fseek(fp,0,SEEK_SET);
	
	//выделение памяти
	arr = (char**)malloc(sizeof(char) * N);
	for (int i = 0; i < count; i++)
	{
		arr[i] = (char*)malloc(sizeof(char) * N);
	}

	//заполнение массива строк из файла и массива чисел из каждой строки
	int i = 0;
	int* mass= (int*)malloc(sizeof(int) * count);
	while (fgets(buffer, N+2, fp) != NULL)
	{
		for (int j = 0; j < N; j++)
		{
			arr[i][j] = buffer[j];
		}
		arr[i][N] = '\0';
		mass[i] = GetDigit(buffer);
		i++;
	}

	fclose(fp);//Закрываем файл

	//сортировка массива чисел методом пузырька
	bubble(mass, count);

	//поиск соответствия отсортированного массива чисел и строк с такими же числами в конце
	//при нахождении строки с нужными числами записываем ее на новое место (строки меняются местами)
	for (int r = 0; r < count; r++)
	{
		for (int l = 0; l < count; l++)
		{
			int a = GetDigit(arr[l]);
			if (a == mass[r])
			{
				char* temp = arr[r];
				arr[r] = arr[l];
				arr[l] = temp;
			}
		}
	}

	//записываем отсортированный массив строк в новый файл
	err = fopen_s(&fp, "bytes2.txt", "w+");
	if (err != 0)
	{
		exit(1);
	}
	printf("\n\nОтсортированные по номеру строки\n");
	for (int l = 0; l < count; l++)
	{
		fputs(arr[l], fp);
		fputs("\n",fp);
		printf("%s\n",arr[l]);
	}
	fclose(fp);//Закрываем новый файл

	printf("\n");

	free(arr);

	return 0;
}

int GetDigit(char* bb)
{
	int sum = 0;
	for (int i = 32; i < 40; i++)
	{
		char ss = bb[i];
		int a = ss - '0';
		a = a*pow(10, (double)(39 - i));
		sum += a;
	}
	return sum;
}

void bubble(int* data, int size)
{
	int i, j;
	for (i = 0; i < size; ++i)
	{
		for (j = size - 1; j > i; --j)
		{
			if (data[j] < data[j - 1])
			{
				int t = data[j - 1];
				data[j - 1] = data[j];
				data[j] = t;
			}
		}
	}
}
