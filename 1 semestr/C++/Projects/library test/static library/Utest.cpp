//---------------------------------------------------------------------------

#pragma hdrstop

#include <tchar.h>
#include <conio.h>
#include <iostream.h>
#incude <squares.h>
//---------------------------------------------------------------------------

#pragma argsused
int _tmain(int argc, _TCHAR* argv[])
{
   int i;
	 cout<<"select figure:\n1-kvadrat\n2-pryamougolnic\n3-treugolnic\n4-krug\n5-trapecia\n0-vyhod\n";
	 cin>>i;
	 switch(i)
	 {
		 case1:cout<<kvadrat(5.5);break;
		 case2:cout<<pryamougolnic(6.3,4);break;
		 case3:cout<<treugolnic(4.3,8.1);break
		 case4:cout<<krug(5.9);break;
		 case5:cout<<trapecia(9.4,2.1,5);break;
		 case0:return0;
	 }
	   getch();
	return 0;
}
//---------------------------------------------------------------------------
