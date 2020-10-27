//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include <tchar.h>
#include <iostream.h>
#include <conio.h>
#include <math.h>
#include <windows.h>
//---------------------------------------------------------------------------

#pragma argsused
int _tmain(int argc, _TCHAR* argv[])
{
  SetConsoleCP(1251);
  SetConsoleOutputCP(1251);
   int a,b;
   float x,y,h,c=0.5,d=1.5,ymin=1000000,xmin=0.5;
 //c и d - крайние значения интервала
cout<<"На сколько частей разделить область поиска корня?:";
cin>>a;
	h=(d-c)/a;
	x=0.5;
			while(x>=0.5 && x<=1.5)
		  {

			 y= sqrt(cos(x)) + log(sin(x)) - 0.5;
			   if(fabs(y)<ymin)
			   {
				  ymin=fabs(y);
				  xmin=x;
			   }
			   cout<<"x="<<x<<"\ty="<<y<<endl;
			 x+=h;


		  }
		cout<<"Минимальное значение функции по модулю:"<<ymin;
		cout<<"\nКорень уравения:"<<xmin;
	getch();
	return 0;
}
//---------------------------------------------------------------------------
