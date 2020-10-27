//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include <tchar.h>
#include <iostream.h>
#include <conio.h>
#include <math.h>
//---------------------------------------------------------------------------

#pragma argsused
int _tmain(int argc, _TCHAR* argv[])
{ float a,b;
bool y=true;
cout<<"a=";
cin>>b;
cout<<"b=";
cin>>b;
 if(a>b)  y=true;
	else  y=false;
float c=y?a:b;
cout<<"c="<<c;
getch();


	return 0;
}
//---------------------------------------------------------------------------
