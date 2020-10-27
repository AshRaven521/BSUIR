//---------------------------------------------------------------------------

#pragma hdrstop

#include <tchar.h>
#include <iostream.h>
#include <conio.h>
#include <math.h>
#include <windows.h>
#include <cstring>
//---------------------------------------------------------------------------

#pragma argsused

char* substr(char *, int, int);

int _tmain(int argc, _TCHAR* argv[])
{


  int b=0;
  int d=0;
  int a=0;
  int c=0;
  int r=0;
	SetConsoleCP(1251);
	SetConsoleOutputCP(1251);
 char string[80];
 char * sf;
  cout<<"Введите элементы строки:";
  cin>>string;
	   cout<<"Элементы строки: "<<string;
  r=strlen(string);
  while (sf!=NULL)
	{
		sf = strchr(string, '.');
		  if(sf!=NULL)
			{
			  a=(sf-string)+1;
				if(a>=0)
				  {
					c=strlen(sf);
					cout<<"\nПоложение:"<<a<<endl;
					b=0;
					bool bB=false;
					  for(int i=a;i>=0;i--)
						{
						  if(isdigit(string[a]))
						  bB=true;
							if(isdigit(string[i]))
							  b++;
						}
						  if(bB)
							{
							  d=0;
							  bool bD=false;
								for(int i=a+1;i<r;i++)
								  {
									if(isdigit(string[a]))
									  bD=true;
										if(isdigit(string[i]))
										  d++;
								  }
									if(bD)
									  {
										char *t;
										t = substr(string, a-b, a+1+d);
										double nN=atof(t);
										cout<<"Число с плавающей запятой: "<<nN;
									  }
							}
				  }
				else
				  cout<<"\nВ строке нет числа"<<endl;

		  r=strlen(sf);
		  for(int k=0;k<r;k++)
			{
			  string[k]=sf[k+1];
			}
	 }
	 }

	getch();
	return 0;
}

char* substr(char *str, int start, int length)
{
  char *s;

  s = new char[length+1];
  int j = 0;
  for (int i = start; i<length; i++)
  {
	s[j] = str[i];
	j++;
  }
  s[j] = '\0';
  return(s);
}
//---------------------------------------------------------------------------
