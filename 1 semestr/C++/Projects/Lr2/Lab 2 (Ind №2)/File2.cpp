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
SetConsoleCP(1251);
SetConsoleOutputCP(1251);
float x,y,s,z;
cout<<"x=";
cin>>x;
cout<<"y=";
cin>>y;
z=x/y;
   if(z<0) {
   s=powf((powf(x,2) + y),3);
   cout<<"Выбрано значение x/y<0";
   }
   else  if(z>0){
	s=log(fabs(z) + z);
  cout<<"Выбрано значение x/y>0";
   }
   else {
   s=powf(fabs(sin(y)),1/3);
   cout<<"Выбрано значение x/y<0";
   }
   cout<<"\ny="<<y;
   getch();
	return 0;
}
//---------------------------------------------------------------------------
