//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include <tchar.h>
#include <iostream.h>
#include <conio.h>
#include <math.h>
#include <windows.h>
#include <string>
using namespace std;
//---------------------------------------------------------------------------

#pragma argsused

int _tmain(int argc, _TCHAR* argv[])
{
	int oldin = GetConsoleCP();
	int oldout = GetConsoleOutputCP();

   int d,a,b;
   char zn,w;
   cout<<"a=";
   cin>>a;
   cout<<"b=";
   cin>>b;
   d=a+b;
   cout<<"zn=";
   cin>>zn;
  if (zn!='+' && zn!='-' && zn!='/' && zn!='*') {


	SetConsoleCP(1251);
	SetConsoleOutputCP(1251);
	cout<<"Неправильный ввод";
	SetConsoleCP(oldin);
	SetConsoleOutputCP(oldout);
	w=getch();
	if (w=='y') {
		cout<<"\nzn=";
		cin>>zn;
	}
	else {
		SetConsoleCP(1251);
	SetConsoleOutputCP(1251);
		cout<<"\nЗнак операции по умолчанию +";
	}
  }

   if(zn=='+') d=a+b;
	if(zn=='-') d=a-b;
	if(zn=='/') d=a/b;
	if(zn=='*') d=a*b;
	cout<<"\nd="<<d;
  getch();


	return 0;
}
//---------------------------------------------------------------------------
