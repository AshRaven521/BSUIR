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
float A=0,B=0,C=0;
cout<<"A=";
cin>>A;
cout<<"B=";
cin>>B;
cout<<"C=";
cin>>C;
if (A>B&& A>C) A=A-0.3;
else
	if(B>A && B>C) B=B-0.3;
	else
		if(C>A && C>B) C=C-0.3;
		else cout<<"The values aren't different";
cout<<"\nA="<<A;
cout<<"\nB="<<B;
cout<<"\nC="<<C;
getch();


	return 0;
}
//---------------------------------------------------------------------------
