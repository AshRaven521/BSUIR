//---------------------------------------------------------------------------

#pragma hdrstop

#include <tchar.h>
#include <iostream.h>
#include <conio.h>
#include <math.h>
#include <windows.h>
//---------------------------------------------------------------------------

#pragma argsused
char* perevodvobr(char *);
char *slozhenie(char *,char *);
int _tmain(int argc, _TCHAR* argv[])
{
  char a[9];
  char b[9];
  char *vyzov1;
  char *vyzov2;
  char *vyzov3;
  char sd[9];
  char sf[9];
SetConsoleCP(1251);
SetConsoleOutputCP(1251);
  cout<<"Введите первое двоичное число,заданное в естественной форме : ";
  cin>>a;
  cout<<"Введите второе двоичное число,заданное в естественной форме : ";
  cin>>b;
	vyzov1=perevodvobr(a);
	for(int i=0;i<9;i++)
		sd[i]=vyzov1[i];
	cout<<"\nПервое число в обратном коде :"<<sd<<endl;
	vyzov2=perevodvobr(b);
    for(int i=0;i<9;i++)
		sf[i]=vyzov2[i];
	cout<<"\nВторое число в обратном коде :"<<sf<<endl;
	vyzov3=slozhenie(sd,sf);
	for(int i=0;i<9;i++)
		sd[i]=vyzov3[i];
	cout<<"\nРезультат сложения в обратном коде :"<<sd<<endl;
	if(sd[0]=='1')
	{
		vyzov3=slozhenie(sd,"00000001\0");
		for(int i=0;i<9;i++)
			sd[i]=vyzov3[i];
	}
	cout<<"\nРезультат сложения в дополнительном коде :"<<sd<<endl;
	getch();
	return 0;
}
  char* perevodvobr(char *aA)
	{
		char c[9]="00000000\0";
		int i=0;
		int j=1;
		int k=0;
		char df[9]="00000000\0";
		while(aA[i]!='\0' && i<8)
		{
			if(aA[i]!='-' && aA[i]!='+')
			{
			   k++;
			}
		  i++;
		}
		if(aA[0]=='-')
			df[0]='-';
		else
		{
			df[0]='+';
		}
		for(j=7;j>=0;j--)
		{
			if(k>0)
			{
				df[j]=aA[k];
				k--;
			}
		}
		i=0;
		j=1;
		  while(df[i]!='\0')
			{
			   if(df[0]=='-')
				 {
					c[0]='1';
					if(i>0)
					{
						if(df[i]=='0')
							c[j]='1';
						else
							c[j]='0';
						j++;
					}
				 }
			   else
				 {
					if(i>0)
					{
						c[j]=df[i];
						j++;
					}
				 }
				i++;
			}
		return c;
	}

	char *slozhenie(char *x,char *y)
	  {
		char result[9]="00000000\0";
		bool bSign=false;
		char *rRR;
		  for(int i=7;i>=0;i--)
			{
				if((x[i]=='1' && y[i]=='0') || (x[i]=='0' && y[i]=='1'))
				  {
					if(result[i]=='1')
					  {
						  result[i]='0';
						  if(i>0)
							result[i-1]='1';
						  else
							bSign=true;
					  }
					  else
					  {
						result[i]='1';
					  }
				  }


				if(x[i]=='0' && y[i]=='0')
				  {
					if(result[i]!='1')
					  result[i]='0';
				  }
				if(x[i]=='1' && y[i]=='1')
				  {
					if(result[i]!='1')
					  {
					  result[i]='0';
					  if(i>0)
						result[i-1]='1';
					  else
						bSign=true;
					  }
					else
					  {
						result[i]='1';
						if(i>0)
							result[i-1]='1';
						else
							bSign=true;
					  }
				  }
			}
		  if(bSign){
			char sS[8]="00000001";
			rRR=slozhenie(result,(char*)sS);
			for(int i=0;i<9;i++)
				result[i]=rRR[i];
		  }

		  return result;
	  }
//---------------------------------------------------------------------------
