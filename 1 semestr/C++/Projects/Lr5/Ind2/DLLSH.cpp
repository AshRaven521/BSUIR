//---------------------------------------------------------------------------

#include <vcl.h>
#include <windows.h>
#include <iostream.h>
#include <math.h>
#pragma hdrstop
//---------------------------------------------------------------------------
//---------------------------------------------------------------------------

#pragma argsused
int WINAPI DllEntryPoint(HINSTANCE hinst, unsigned long reason, void* lpReserved)
{
	return 1;
}

bool hH=true;
int i=1;
  extern "C"  bool _declspec(dllexport) _stdcall calculate( int f,int p,int *X)
{
	double result;
			   if (f == p-1 )
			 return false;
	   cout << "Step\t" << i << endl;
	   i++;
	   result = cos((float)X[f]);
		  if(result<=0)
			hH=false;
	   cout << "Result= " << result << endl;
	   calculate(f-1,p,X);

	   return hH;
}

//---------------------------------------------------------------------------
