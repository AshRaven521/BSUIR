//---------------------------------------------------------------------------

#pragma hdrstop

#include <tchar.h>
#include <iostream.h>
#include <fstream>
#include <conio.h>
#include <math.h>
#include <windows.h>
#include <cstring>
//---------------------------------------------------------------------------

#pragma argsused
float slozhenie(float,float);
int _tmain(int argc, _TCHAR* argv[])
{
  SetConsoleCP(1251);
  SetConsoleOutputCP(1251);
	   int t=0,f=0;
	   float sum=0;
	   float a[3][3];

	   ifstream fin;
	   fin.open("sd.txt");
		 if(!fin.is_open())
		   {
			   cout<<"������ ��� �������� �����";
		   }

		 else
		   {
			   cout<<"\n���� ������� ������";
				 for(int i=0;i<3;i++)
				   {
					 for(int j=0;j<3;j++)
					   {
						 fin>>a[i][j];
					   }
				   }
		   }

		 while(t<3 && t%2==0)//t-������ f-�������;
		   {
			  while(f<3)
				{
				   sum=slozhenie(sum,a[f][t]);
				   f++;
				}
			 t++;
		   }
	  cout<<"\n����� �������� �������� :"<<sum;
	  fin.close();

	 getch();
	return 0;
}

 float slozhenie(float a,float b)
   {
	   return a+b;
   }
//---------------------------------------------------------------------------
