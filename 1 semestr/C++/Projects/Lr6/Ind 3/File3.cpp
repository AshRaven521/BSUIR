//---------------------------------------------------------------------------

#pragma hdrstop

#include <tchar.h>
#include <iostream.h>
#include <conio.h>
#include <math.h>
#include <windows.h>
#include <stdio.h>
#include <stdlib.h>
//---------------------------------------------------------------------------

#pragma argsused

char * IntToChar(int);
int CharToInt(char *);
float CharToFloat(char *);
char * FloatToChar(float);
int _tmain(int argc, _TCHAR* argv[])
{
	SetConsoleCP(1251);
	SetConsoleOutputCP(1251);

	int N;
	cout<<"Введите целое число: ";
	cin >> N;
	cout<<"Исходное число : "<<N;
	char *result=IntToChar(N);
	cout<<"\nПолученная строка: "<<result<<endl;

	char str1[10];
	cout<<"\nВведите строку: ";
	cin>>str1;
	cout<<"Исходная строка: "<<str1;
	int result2=CharToInt(str1);
	cout<<"\nПолученное число :"<<result2<<endl;

	char str10[10];
	cout<<"\nВведите вещественную строку : ";
	cin>>str10;
	cout<<"Исходная вещественная строка : "<<str10;
	float result3=CharToFloat(str10);
	cout<<"\nПолученное вещественное число : "<<result3<<endl;

	float M;
	cout<<"\nВведите вещественное число : ";
	cin>>M;
	cout<<"Исходное вещественное число : "<<M;
	char *result4=FloatToChar(M);
	cout<<"\nСтрока с вещественным числом : "<<result4<<endl;
	getch();
	return 0;
}

  char * IntToChar(int N)
{
	int count = 1;
	int m=N;
	while (m=m/10)
		count++;
	int *p = new int[count];
	int i = count-1;
	while (N){
		p[i] = N % 10;
		N /= 10;
		i--;
	}
	//for (int i = 0; i < count; i++)
	//	cout <<endl<< p[i] << ' ';

	char *b;
	b=new char[count];
	for(i=0;i<count;i++)
	{
		b[i]=p[i]+'0';
	}

	return b;
}

  int  CharToInt(char *str1)
{

	int i=0;
	int p[10];
	while(str1[i]!='\0')
	{
		p[i]=str1[i]-'0';
		i++;
	}
	int count=i;
	int f=0;
	for ( i = 0; i<count; i++)
	{
		f=f+p[i]*powf(10,count-1-i);
	}

	return f;
}

float CharToFloat(char *str10)
{
	   int i=0;
	   int g[10];
	   int dr[10];
	   bool a,bfl=true;
	   int count=0;
	   int zcount=0;
		 while(str10[i]!='\0')
		   {
				if(str10[i]=='.')
				 {
					 bfl=false;
					 i=i+1;
				 }
			   if(bfl)
				{
					g[i]=str10[i]-'0';
					count++;
				}
				else
				{
					dr[zcount]=str10[i]-'0';
					zcount++;
				}
				i++;
		   }
	   ;
	   float e=0;
	   float ez=0;
		 for ( i = 0; i<count; i++)
		   {
			 e=e+g[i]*powf(10,count-1-i);
		   }
		 for ( i = 0; i<zcount; i++)
		   {
			 ez=ez+dr[i]/powf(10,i+1);
		   }
		   e=e+ez;
	   return e;
	}

	  char * FloatToChar(float M)
		{
		   int t,count=1;
		   float p;
		   double x,y;
		   t=(int)M;
		   p=M-t;
		   char *b=IntToChar(t);

			char *s=new char[10];
			sprintf(s,"%f",M);

		   return s;
		}
//---------------------------------------------------------------------------
