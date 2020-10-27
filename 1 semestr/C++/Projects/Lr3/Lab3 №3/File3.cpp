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
	float x,y=0,B=M_PI/2,H,A=0;
	int M=20;
		H=(B-A)/M;
		  for(int i=0;x<=B;i++)
		   {
			   x=A+i*H;
			   y=sin(x)-cos(x);
               cout<<"sin("<<x<<") - cos("<<x<<") = "<<y<<endl;

		   }


	   getch();
	return 0;
}
//---------------------------------------------------------------------------
