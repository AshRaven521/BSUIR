
#include <stdio.h>
#include <math.h>
#include <conio.h>
#include <stdlib.h>


float leftsight(float);
float fact(int);
float rightsight(float,int);

int main()
{
	system("chcp 1251"); // переходим в консоли на русский язык
	float x = 0;
	int n = 0;
	float e = 0;
	float a = 0;
	float b = 0;

	printf("Введите x:");
	scanf_s("%f", &x);

	printf("Введите n:");
	scanf_s("%d", &n);

	printf("Введите погрешность e:");
	scanf_s("%f", &e);

	a = leftsight(x);
	b = rightsight(x,n);

	printf("Левая часть выражения равна:%f\n", a);
	printf("Правая часть выражения равна:%f\n", b);

	if (fabsf(a - b) <= e)
	  {
		printf("Исследуемое выражение сходится при n=%d\n", n);
	  }
	else
	  {
		printf("Исследумое выражение не сходится\n");
	  }
}

float leftsight(float x)
  {
	float a = sin(x);
	return a;
  }

float fact(int n)
  {
	if (n < 0)// если пользователь ввел отрицательное число
	  {
		return 0; // возвращаем ноль
	  }
		
	if (n == 0)// если пользователь ввел ноль, 
	  {
		return 1; // возвращаем факториал от нуля -  1 )
	  } 
		
	else// Во всех остальных случаях 
	{
		return n * fact(n - 1); // делаем рекурсию.
	} 
		
  }

float rightsight(float x, int n)
  {
	float a = 0;
	for (int i = 1; i <= n;i++)
	  {
		a = a + (powf(-1, n - 1) * (powf(x, 2 * n - 1) / fact(2 * n - 1)));
		
	  }
	return a;
  }