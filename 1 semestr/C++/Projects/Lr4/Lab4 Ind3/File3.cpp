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
   const int N=3;
	int **mas = new int *[N];
	 int s,i1,i2,array[N][N],bo=1,i,j;

for (int i = 0; i < N; i++)
{
  mas[i] = new int [N];
}

   array[0][0]=1;
   array[0][1]=0;
   array[0][2]=0;
   array[1][0]=0;
   array[1][1]=1;
   array[1][2]=0;
   array[2][0]=0;
   array[2][1]=0;
   array[2][2]=1;
	   cout<<"Исходная матрица";
	   cout<<"\n";
		for(i=0;i<N;i++)
		 {
			 for(j=0;j<N;j++)
			  {
				  cout<<array[i][j]<<" ";
			  }
				  cout<<"\n";
		 }
	  for(i1=0;i1<N;i1++)
	{
		 for(i2=0;i2<N;i2++)
		 {
				s=0;
				int i;
				for(i=0;i<N;i++)
						s+=array[i1][i]*array[i2][i];
				if( i1!=i2 && s!=0 || i1==i2 && s!=1.0)
				{

						bo=0;
						break;
				}
		 }
	}
	if(bo)
	   cout<<"\nМатрица ортонормированная";
	else
	   cout<<"\nМатрица не ортонормированная";







for (int i = 0; i < N; i++) {
  delete []mas[i];
}
delete []mas;
	getch();
	return 0;
}
//---------------------------------------------------------------------------
