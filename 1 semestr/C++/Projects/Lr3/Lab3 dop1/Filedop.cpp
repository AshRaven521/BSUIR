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
SetConsoleCP(1251);
SetConsoleOutputCP(1251);
  int x,a,b,f;
	for(x=10;x<100;x++)
	  {
		   a=x/10;
		   b=x%10;
		   f=a*b*3;
		 if(f==x) cout<<"\nНайденое число:"<<x;
	  }
	  getch();
	return 0;
}
//---------------------------------------------------------------------------
