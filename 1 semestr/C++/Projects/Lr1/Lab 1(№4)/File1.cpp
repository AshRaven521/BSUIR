//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include <tchar.h>
#include <iostream.h>
#include <math.h>
#include <conio.h>

//---------------------------------------------------------------------------

#pragma argsused
int _tmain(int argc, _TCHAR* argv[])
{
int a,b;
cout<<"a=";
cin>>a;
cout<<"b=";
cin>>b;
a=a+b;
cout<<"a="<<a;
cout<<"b="<<b;
cout<<"\n";
b=a-b;
cout<<"a="<<a;
cout<<"b="<<b;
cout<<"\n";
a=a-b;
cout<<"a="<<a;
cout<<"b="<<b;
cout<<"\n";
getch();
	return 0;
}
//---------------------------------------------------------------------------
