//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include <tchar.h>
#include <math.h>
#include <iostream.h>
#include <conio.h>
#include <windows.h>
//---------------------------------------------------------------------------

#pragma argsused
int _tmain(int argc, _TCHAR* argv[])
{
	SetConsoleCP(1251);
	SetConsoleOutputCP(1251);
	const int n=3;
	int **mas = new int *[n];
	int s,imax,jmax,bo=1,i,j,max=0,o=0,h=0;

	for (int i = 0; i < n; i++)
	{
		mas[i] = new int [n];
	}

   mas[0][0]=1;
   mas[0][1]=5;
   mas[0][2]=7;
   mas[1][0]=92;
   mas[1][1]=14;
   mas[1][2]=2;
   mas[2][0]=53;
   mas[2][1]=35;
   mas[2][2]=64;
   cout<<"»сходна€ матрица";
   cout<<"\n";
	//1-ый вложенный for выводит матрицу
	for(i=0;i<n;i++)
	{
		for(j=0;j<n;j++)
		{
			cout<<mas[i][j]<<" ";
		}
		cout<<"\n";
	}

			   //2-ой вложенный for ищет максимальный элемент
	for(i=0;i<n;i++)
	{
			 for(j=0;j<n;j++)
			  {
				  if(abs(mas[i][j]) > max)
				  {
					  max=abs(mas[i][j]);
						imax=i;
						jmax=j;
				  }
			  }
				  cout<<"\n";
	}
				cout<<"max="<<max;
				 cout<<"\n";
				 cout<<"\n";
			//¬ыдел€ем динамически пам€ть дл€ новой матрицы(не забыть ее же в конце удалить)
		   int **matr = new int *[n-1];
	   for (int i = 0; i < n-1; i++)
	{
		matr[i] = new int [n-1];
	}

		 for(i=0;i<n;i++)
		 {
			if(i!=imax)
			{


			 for(j=0;j<n;j++)
			  {
				  if( j!=jmax)
					{
					h++;
						matr[o][h]=mas[i][j];
						 cout<<matr[o][h]<<" ";
					}
			  }
				 o++;
				 cout<<"\n";
			}




		 }





for (int i = 0; i < n; i++)
{
  delete []mas[i];
}
delete []mas;

  for (int i = 0; i < n-1; i++)
{
  delete []matr[i];
}
delete []matr;
	getch();
	return 0;
}
//---------------------------------------------------------------------------
