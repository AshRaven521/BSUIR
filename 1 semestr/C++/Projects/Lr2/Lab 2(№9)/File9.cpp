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
int G,p,f,n;
cout<<"G=";
cin>>G;
cout<<"p=";
cin>>p;
 f=p-G;
 n=f%10;
 switch(n)
 {
	 case 1: cout<<f<<" год";break;
	 case 2: cout<<f<<" года";break;
	 case 3: cout<<f<<" года";break;
	 case 4: cout<<f<<" года";break;
	 case 5: cout<<f<<" лет";break;
	 case 6: cout<<f<<" лет";break;
	 case 7: cout<<f<<" лет";break;
	 case 8: cout<<f<<" лет";break;
	 case 9: cout<<f<<" лет";break;
	 case 0: cout<<f<<" лет";break;
 }
 getch();
	return 0;
}
//---------------------------------------------------------------------------
