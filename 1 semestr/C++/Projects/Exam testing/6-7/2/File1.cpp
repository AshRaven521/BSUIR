//---------------------------------------------------------------------------

#pragma hdrstop

#include <tchar.h>
#include <iostream.h>
#include <fstream>
#include <conio.h>
#include <windows.h>
//---------------------------------------------------------------------------

#pragma argsused
   struct student
	 {
		 char surname[32];
		 int groupnumber;
		 int mark;
	 };
void groups(student *);
int _tmain(int argc, _TCHAR* argv[])
{

   SetConsoleCP(1251);
   SetConsoleOutputCP(1251);
   int i=0;
	student array[5];
	  for(int i=0;i<5;i++)
		{
			cout<<"\n������� ������� �������� :";
			cin>>array[i].surname;
			cout<<"\n������� ����� ������ :";
			cin>>array[i].groupnumber;
			cout<<"\n������� ������ �� ������� :";
			cin>>array[i].mark;
		}
	groups(array);
	 getch();
	return 0;
}

  void groups(student *array)
	{

		int a=0;
		cout<<"\n������� ����� ������ ������� ��������� :";
		cin>>a;
		  ofstream fout;
		  fout.open("students.txt",ios_base::trunc);
			if(!fout.is_open())
			  {
				  cout<<"\n������ ��� �������� �����";
			  }
			else
			  {
				  cout<<"\n���� ������ �������";

					for(int i=0;i<5;i++)
					  {
						if(array[i].groupnumber==a)
						  {
							cout<<"\n\n������� �������� ��������� ������ :"<<array[i].surname;
							fout<<"\n\n������� �������� ��������� ������ :"<<array[i].surname;
							cout<<"\n\n������  :"<<array[i].groupnumber;
							fout<<"\n\n������  :"<<array[i].groupnumber;
							cout<<"\n\n������ �� ������� :"<<array[i].mark;
							fout<<"\n\n������ �� ������� :"<<array[i].mark;
						  }
					  }
			  }
		  fout.close();
	}
//---------------------------------------------------------------------------
