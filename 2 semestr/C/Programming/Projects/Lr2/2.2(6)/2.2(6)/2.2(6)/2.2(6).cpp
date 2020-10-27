
#include <stdio.h>
#include <math.h>
#include <conio.h>
#include <stdlib.h>

float check();
int check_int();
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
	//x = check();

	printf("Введите n:");
	scanf_s("%d", &n);
	//n = check_int();

	printf("Введите погрешность e:");
	scanf_s("%f", &e);
	//e = check();

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
	float b = 0;
	for (int i = 1; i <= n;i++)
	  {
		b = b + (powf(-1, i - 1) * (powf(x, 2 * i - 1) / fact(2 * i - 1)));
		
	  }
	return b;
  }

float check()
  {
	float a;
	rewind(stdin);
	while (!scanf_s("%f", &a) || a < 0)
	  {
		rewind(stdin);
		printf("Неправильный ввод,введите корректные данные:");
	  }
	return a;
  }

int check_int()
  {
	int c;
	rewind(stdin);
	while (!scanf_s("%d", &c) || c < 0)
	  {
		rewind(stdin);
		printf("Неправильный ввод,введите корректные данные:");
	  }
	return c;
  }