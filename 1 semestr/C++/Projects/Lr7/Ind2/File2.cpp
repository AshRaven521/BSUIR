//---------------------------------------------------------------------------

#pragma hdrstop

#include <tchar.h>
#include <iostream.h>
#include <conio.h>
#include <windows.h>
#include <math.h>
//---------------------------------------------------------------------------

#pragma argsused
int perevodiz2v10(char *,int);
int _tmain(int argc, _TCHAR* argv[])
{
  SetConsoleCP(1251);
  SetConsoleOutputCP(1251);
  char obr[50];
  int zv,i=0,a=0;
  cout<<"������� ����� � �������� ���� :";
  cin>>obr;
	a=obr[0]-'0';
	while(obr[i]!='\0')
	{
		if(a==1 && i>0)
		{
			if(obr[i]=='0')
				obr[i]='1';
			else
				obr[i]='0';
		}
		i++;
	}
	//int len=i-2;
	//zv=perevodiz2v10(obr,len);
	//if(a==1)
	//	zv=-zv;
	if(a==1)
	  {
         obr[0]='0';
		 cout<<"����� � ������������ �����:- "<<obr<<endl;
	  }
	else
	  {
		 cout<<"����� � ������������ �����: "<<obr<<endl;
      }

	getch();
	return 0;
}

  int perevodiz2v10(char *obry,int len)
	{
	  int i,a,sum=0,j=2;
	   for(i=len-1;i>=0;i--)
		 {
			a=obry[j]-'0';
			sum=sum+a * powf(2,i);
			j++;
		 }
		return sum;
	}

//---------------------------------------------------------------------------