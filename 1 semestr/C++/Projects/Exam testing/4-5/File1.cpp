//---------------------------------------------------------------------------

#pragma hdrstop

#include <tchar.h>
#include <iostream.h>
#include <fstream>
#include <conio.h>
#include <math.h>
#include <windows.h>
#include <cstring>
//---------------------------------------------------------------------------

#pragma argsused
void Save(float,float,float);
float rasschet(float,float);
void Load();
int _tmain(int argc, _TCHAR* argv[])
{
  SetConsoleCP(1251);
  SetConsoleOutputCP(1251);
	  float z,x;
	  float sum;
	  cout<<"z=";
	  cin>>z;
	  cout<<"x=";
	  cin>>x;
		Save(x,z,sum);
		Load();
	  getch();
	return 0;
}

void Save(float x,float z,float f)
  {
	  ofstream fout;
	  fout.open("ss.txt",ios_base::trunc);//ios_base::trunc-����, ������� ������� ���������� �����



		if(!fout.is_open())
		  {
			  cout<<"������ ��� ���������� �����";
		  }
		else
		  {
			  cout<<"���� �������� �������";
				f=rasschet(x,z);
				fout<<"����� :"<<f;
		  }

	 fout.close();
  }

 float rasschet(float x,float z)
   {
	  float sum;
				if(z>x)
				  {
					  sum=powf(z,2)-powf(x,2);
				  }
				if(z<x)
				  {
					  sum=powf(x,2)-powf(z,2);
				  }
				if(z==x)
				  {
					  sum=0;
				  }
	  return sum;
   }

 void Load()
   {
	 float a[2][3];
	   ifstream fin;
	   fin.open("sd.txt");
		 if(!fin.is_open())
		   {
			   cout<<"������ ��� �������� �����";
		   }

		 else
		   {
			   cout<<"\n���� ������� ������";
			   //fin>>a[0]>>a[1]>>a[2]>>a[3]>>a[4]>>a[5];
				 for(int i=0;i<2;i++)
				   {
					 for(int j=0;j<3;j++)
					   {
						 fin>>a[i][j];
					   }
				   }
		   }

	  fin.close();
   }
//---------------------------------------------------------------------------
