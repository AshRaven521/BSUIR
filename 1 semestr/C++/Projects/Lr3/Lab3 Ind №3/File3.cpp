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
	float x=1.8,y=0;
	do
	   {
			  x=x+0.2;
		   y= 0.5+ powf(sinh(x),2);

			cout<<"\|x="<<x<<"\t|y="<<y<<"|"<<endl;



	   }
	while(x>=2 && x<=3);
	getch();
	return 0;
}
//---------------------------------------------------------------------------
