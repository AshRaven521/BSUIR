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
{ float  k,m,e,x1,x2,L;
 cout<<"x1=";
 cin>>x1;
 cout<<"x2=";
 cin>>x2;
 cout<<"m=";
 cin>>m;
 k=pow(cos(powf(x1,2)),3) + pow(sin(powf(x2,3)),2);
 if(k<1) L=powf(k,3) + powf(m,0.2);
 else L=powf(k,2) - powf(e,m);
  cout<<"L="<<L;
 getch();

	return 0;
}
//---------------------------------------------------------------------------
