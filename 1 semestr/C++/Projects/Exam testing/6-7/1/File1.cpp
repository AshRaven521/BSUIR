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
float** Load();
int _tmain(int argc, _TCHAR* argv[])
{
  SetConsoleCP(1251);
  SetConsoleOutputCP(1251);
	 float **array;
	 array=Load();
	 getch();

	return 0;
}

float** Load()
   {

	   float **a=new float*[3];
	   int t=0,f=0;
	   float sum=0;
	   for(int i=0;i<3;i++)
		 {
			a[i]=new float [3];
		 }

	   ifstream fin;
	   fin.open("sd.txt");
		 if(!fin.is_open())
		   {
			   cout<<"������ ��� �������� �����";
		   }

		 else
		   {
			   cout<<"\n���� ������� ������";
			   //fin>>a[0]>>a[1]>>a[2]>>a[3]>>a[4]>>a[5];
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
				   sum=sum+a[f][t];
				   f++;
				}
			 t++;
		   }
	  cout<<"\n����� �������� �������� :"<<sum;
	  fin.close();
	  for(int i=0;i<3;i++)
		{
			delete [] a[i];
		}
	  return a;
   }
//---------------------------------------------------------------------------

//---------------------------------------------------------------------------