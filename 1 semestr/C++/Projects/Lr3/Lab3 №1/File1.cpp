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
  int N=0,a,b,i=1;
	 while(i>=1 && i<=30)
	   {
		 if(i%2==0)
		   {
			   a=i/2;
			   b=pow(i,3);
			   N+=pow(a-b,2);
		   }
		 else
		   {
			 a=i;
			 b=pow(i,2);
			 N+=pow(a-b,2);
		   }

	   }
		  cout<<"N="<<N;
		  getch();
	return 0;
}
//---------------------------------------------------------------------------
