//---------------------------------------------------------------------------

#pragma hdrstop

#include <tchar.h>
#include <iostream.h>
#include <conio.h>
#include <windows.h>
#include <math.h>
//---------------------------------------------------------------------------

#pragma argsused

int _tmain(int argc, _TCHAR* argv[])
{
	bool h=true;

 //Вызов DLL библиотеки
  HINSTANCE load;
  load=LoadLibrary("MyDLL.dll");
 //Получение указателя на функцию
 //pfspectr-произвольное имя
  typedef bool (_stdcall *pfspectr) (int,int,int *);
  pfspectr calculate;
  calculate=(pfspectr)GetProcAddress(load,"calculate");
	SetConsoleCP(1251);
	SetConsoleOutputCP(1251);
  const int N=6;

	   int X[N]={2,-7,12,94,-71,9};
			h=calculate(1,0,X);
			h=calculate(5,2,X);
	  if(h)
		 cout<<"Для всех элементов массива косинус больше нуля";
	  else
		 cout<<"Не для всех элементов массива косинус больше нуля";
	 //освобождение DLL
	   FreeLibrary(load);
	getch();
	return 0;
}
//---------------------------------------------------------------------------
