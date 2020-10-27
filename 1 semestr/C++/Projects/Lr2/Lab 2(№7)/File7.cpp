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
  float x,p,q,y,x1,x2,D;
   cout<<"x=";
   cin>>x;
   cout<<"p=";
   cin>>p;
   cout<<"q=";
   cin>>q;
   y=powf(x,3) + p*x + q;
   if(p==q==0) y=powf(x,3);
   // x к 0 не приравниваем,т.к нет коэффициента а при нем//
   if(x!=0)  y=powf(x,2) + q/x + p;
	   D=powf(q,2) - 4*p;
	   x1=(-q +sqrt(powf(q,2)-4*p))/2;
	   x2=(-q -sqrt(powf(q,2)-4*p))/2;
		cout<<"x1="<<x1;
		cout<<"x2="<<x2;
		getch();


	return 0;
}
//---------------------------------------------------------------------------
