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
	float a0,dn,e=0.001,N=0;
	int n=0;

	   do
		{
		   dn=powf(1.0/2.0,n)+powf(1.0/3.0,n);
		   N+=dn;
		   n++;
		}   while(dn>=e);
	cout<<"n="<<n;
	cout<<"\nN="<<N;
	cout<<"\ndn="<<dn;
  getch();
	return 0;
}
//---------------------------------------------------------------------------
