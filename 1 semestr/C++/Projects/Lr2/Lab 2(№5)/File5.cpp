//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include <tchar.h>
 #include <math.h>
 #include <iostream.h>
 #include <conio.h>
//---------------------------------------------------------------------------

#pragma argsused
int _tmain(int argc, _TCHAR* argv[])
{
   float a,b,c,d,Z;
cout<<"a=";
cin>>a;
cout<<"b=";
cin>>b;
cout<<"c=";
cin>>c;
cout<<"d=";
cin>>d;
	if(c>=d && a<d) Z= a+(b/c);
	else if(c<d && a>=d) Z=a-(b/c);
	else Z=0;
	cout<<"Z="<<Z;
	getch();

	return 0;
}
//---------------------------------------------------------------------------
