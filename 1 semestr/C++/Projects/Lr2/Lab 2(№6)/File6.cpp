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
int N,c;
float a,b,Y;
cout<<"a=";
cin>>a;
cout<<"b=";
cin>>b;
cout<<"c=";
cin>>c;
cout<<"N=";
cin>>N;
	switch(N)
	{
		case 2: Y=(b*c) - powf(a,2);break;
		case 56: Y=b*c;break;
		case 7: Y=powf(a,2) + c;break;
		case 3: Y=a-(b*c);break;
		default: Y=powf((a+b),3);break;
	 }
	 cout<<"Y="<<Y;
  getch();
	return 0;
}
//---------------------------------------------------------------------------
