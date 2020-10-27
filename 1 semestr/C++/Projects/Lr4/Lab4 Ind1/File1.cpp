//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include <tchar.h>
#include <math.h>
#include <iostream.h>
#include <windows.h>
#include <conio.h>
//---------------------------------------------------------------------------

#pragma argsused
int _tmain(int argc, _TCHAR* argv[])
{
	SetConsoleCP(1251);
	SetConsoleOutputCP(1251);

	 const int k=5;
	  int a[k];
	  int b[k-2];
	  int min=1000000,max=0;
	for(int i=0; i<k; i++)
	   {
		   cout<<"Введите элемент a["<<i<<"]=";
		   cin>>a[i];
			 if(a[i]<min)
			  {
				 min=a[i];
			  }
			 if(a[i]>max)
			 {
				 max=a[i];
			 }
	   }
	for(int i=0; i<k; i++)
		{
			cout<<a[i]<<" ";
		}
		   cout<<"\n";
	for(int i=0; i<k; i++)
		{
		   if(a[i]!=min && a[i]!=max)
			{
				cout<<a[i]<<" ";
			}



		}

	 getch();
	return 0;
}
//---------------------------------------------------------------------------
