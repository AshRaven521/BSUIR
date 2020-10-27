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
			cout<<"\n¬ведите фамилию студента :";
			cin>>array[i].surname;
			cout<<"\n¬ведите номер группы :";
			cin>>array[i].groupnumber;
			cout<<"\n¬ведите оценку за семестр :";
			cin>>array[i].mark;
		}
	groups(array);
	 getch();
	return 0;
}

  void groups(student *array)
	{

		int a=0;
		cout<<"\n¬ведите номер группы искомых студентов :";
		cin>>a;
		  ofstream fout;
		  fout.open("students.txt",ios_base::trunc);
			if(!fout.is_open())
			  {
				  cout<<"\nќшибка при открытии файла";
			  }
			else
			  {
				  cout<<"\n‘айл открыт успешно";

					for(int i=0;i<5;i++)
					  {
						if(array[i].groupnumber==a)
						  {
							cout<<"\n\n‘амили€ студента выбранной группы :"<<array[i].surname;
							fout<<"\n\n‘амили€ студента выбранной группы :"<<array[i].surname;
							cout<<"\n\n√руппа  :"<<array[i].groupnumber;
							fout<<"\n\n√руппа  :"<<array[i].groupnumber;
							cout<<"\n\nќценка за семестр :"<<array[i].mark;
							fout<<"\n\nќценка за семестр :"<<array[i].mark;
						  }
					  }
			  }
		  fout.close();
	}
//---------------------------------------------------------------------------
