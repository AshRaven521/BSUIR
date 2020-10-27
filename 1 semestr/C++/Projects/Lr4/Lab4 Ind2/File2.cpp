//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include <tchar.h>
#include <iostream.h>
#include <math.h>
#include <conio.h>
#include <windows.h>
//---------------------------------------------------------------------------

#pragma argsused
int _tmain(int argc, _TCHAR* argv[])
{
  const int N=4;
  const int M=5;
   int i,j,min=1000000;
	 int array[N][M]={
		  {2,4,6,7,8},
		  {9,11,24,61,78},
		  {31,54,67,44,98},
		  {52,76,18,49,74}
	 };
	    for(i=0;i<N;i++)
			{
				for(j=0;j<M;j++)
				  {
					 cout<<array[i][j]<<" ";
				  }
				  cout<<"\n";
			}
			   cout<<"\n";
		  for(i=0;i<N;i++)
			{
				for(j=0;j<M;j++)
				  {
					  if(i<j)
					  {
						  if(array[i][j]<min)
						  {
							   min=array[i][j];

						  }
						  cout<<array[i][j]<<" ";
					  }
				  }
				  cout<<"\n";
			}
	cout<<"\nmin="<<min;
	 getch();
	return 0;
}
//---------------------------------------------------------------------------
