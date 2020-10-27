//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include <tchar.h>
#include <math.h>;
#include <iostream.h>;
#include <conio.h>;
#include <windows.h>
//---------------------------------------------------------------------------

#pragma argsused
int _tmain(int argc, _TCHAR* argv[])
{
   int x,y;
   cout<<"x=";
   cin>>x;
   cout<<"y=";
   cin>>y;
   if (x==y) x=y=0;
   else {
	if(x<y) x=0;
	else y=0;
   }


cout<<"\nx="<<x;
cout<<"\ny="<<y;
getch();

	return 0;
}
//---------------------------------------------------------------------------
