
#include <stdio.h>
#include <conio.h>
#include <stdlib.h>
#include <ctype.h>
#include <math.h>




int main()
{
	system("chcp 1251"); // переходим в консоли на русский язык

	float a1 = 0, b1 = 0, c1 = 0;

	printf("\nВведите сторону треугольника a= ");
	scanf_s("%f", &a1);

	if (a1 == 0)//если введена не цифра,то scanf_s присваивает переменной ноль
	  {
		printf("\nВведено неверное значение");
	  }
	else
	  {
		printf("\nВведите сторону треугольника b= ");
		scanf_s("%f", &b1);

		  if (b1 == 0)
		    {
			  printf("\nВведено неверное значение");
		    }  
		  else
		    {
			  printf("\nВведите сторону треугольника c= ");
			  scanf_s("%f", &c1);

			  if (c1 == 0)
			    {
				  printf("\nВведено неверное значение");
			    }
			else 
			  {
				  if (a1 < b1 + c1 || b1 < a1 + c1 || c1 < a1 + b1)
				    {
					  printf("\nТакой треугольник существует");
						  if(a1 == b1 ==c1)
						    {
							  printf("\nИ этот треугольник является равноcторонний");
						    }
						  else if (a1 == b1 || a1 == c1 || b1 == c1)
						    {
							  printf("\nИ этот треугольник является равнобедренным");
						    }
						  else if (powf(a1, 2) == powf(b1, 2) + powf(c1, 2) || powf(b1, 2) == powf(a1, 2) + powf(c1, 2) || powf(c1, 2) == powf(a1, 2) + powf(b1, 2))
						    {
							  printf("\nИ этот треугольник является прямоугольным");
						    }
						  else
						    {
							  printf("\nИ этот треугольник является треугольником общего вида");
						    }
				    }
			  }

		    }
	  }


	return 0;
}
