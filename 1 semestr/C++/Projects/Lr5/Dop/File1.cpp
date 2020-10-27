//---------------------------------------------------------------------------

#pragma hdrstop

#include <tchar.h>
#include <iostream.h>
#include <conio.h>
#include <math.h>
#include <windows.h>

//---------------------------------------------------------------------------

#pragma argsused
  int rasschet(double **,const int,const int);
  void vyvod(double **,const int,const int);
int _tmain(int argc, _TCHAR* argv[])
{
	SetConsoleCP(1251);
	SetConsoleOutputCP(1251);
	 int k=0,i,j;
	 const int n=3;
	 const int m=4;

  double **mas = new double *[n];
			for (i=0; i < m; i++)
			{
			   mas[i]=new double [m];
			}
	 double *S = new double [m];

		for(i=0;i<n;i++)
		{
			for(j=0;j<m;j++)
			 {
			   cout<<"Введите элемент a["<<i<<"]""["<<j<<"]=";
			   cin>>mas[i][j];
			 }
		}


       //передача функции рассчета количества нулевых элементов
		 k=rasschet(mas,n,m);



		   cout<<"\nИсходный массив :"<<endl;
	   for (int i =0; i<n; i++)
	{
		for (int j =0; j<m; j++)
		{
			cout << mas[i][j] << " ";
		}
		cout << "\n";
	}

		  cout<<"\nКоличество нулевых элементов:"<<k;

		//передача функции вывода
			vyvod(mas,n,m);


   delete []mas;
   delete []S;
	getch();
	return 0;
}


   int rasschet(double **mas,const int n,const int m)
	  {
		  int i,j,k=0;

		  for(i=0;i<n;i++)
	   {
		   for(j=0;j<m;j++)
			{
				if(mas[i][j]==0)
				 {
				   k++;
				   cout<<"\nЭлемент: ["<<i<<"]"<<"["<<j<<"]";
				 }
			}
	   }
		 return k;
	  }
	void vyvod(double **mas,const int n,const int m)
	   {
		   cout<<"\nОбратный массив:"<<endl;
	   for (int i =n-1; i >= 0; i--)
	{
		for (int j =m-1; j >= 0; j--)
		{
			cout << mas[i][j] << " ";
		}
		cout << "\n";
	}
	   }
//---------------------------------------------------------------------------
