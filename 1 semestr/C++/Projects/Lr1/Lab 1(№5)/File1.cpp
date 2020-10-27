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
float x,p,h,K,C,D;
cout<<"x=";
cin>>x;
cout<<"p=";
cin>>p;
cout<<"h=";
cin>>h;
cout<<"K=";
cin>>K;
cout<<"C=";
cin>>C;
cout<<"D=";
cin>>D;
 float A=x-p;
 float B=log(h);
float Y=0.78*B + (powf(A,3) / (K*C*D));
cout<<"Y="<<Y;
getch();
	return 0;
}
//---------------------------------------------------------------------------
