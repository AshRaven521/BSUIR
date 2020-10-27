//---------------------------------------------------------------------------

#pragma hdrstop

#include <tchar.h>
#include <iostream.h>
#include <conio.h>
#include <math.h>
#include <windows.h>
#include <string>
#include <vector>
#include <cstring>
//---------------------------------------------------------------------------

#pragma argsused

struct book
  {
	 int RegistrationNumber;
	 char author[32];
	 char nazvanie[32];
	 int year;
	 char izdatelstvo[32];
	 int pages;
  };



/* 1 ������� ���� �� �������(�.� �������� ������ ��� ������������ ���������� ���������)
   2 ������� �������� �� ������ ������ � ���������
   3 ������� ������� ������ �� �����*/
book *CreateStruct(book *);
book *AddStruct(book *,const int );
void setData(book *,const int );
void showData(const book *,const int );
void search(book *,const int,int);
void DeleteAndChange(book *,const int,int);
void ShellSort(book *,const int,int);
// ������� ���� ������������ � ������� ����� ��������
int prompt_menu_item()
{
	// ��������� ������� �����
	int variant;
	cout << "�������� �������\n" << endl;
	cout << "1. ����������� ����������\n"
		 << "2. �������� ���������\n"
		 << "3. ����� ���������\n"
		 << "4. �������� ���������\n"
		 << "5. ������� ���������\n"
		 << "6. ����������� ���������\n"
		 << "7. �����\n" << endl;
	cout << ">>> ";
	cin >> variant;
	return variant;
}
int _tmain(int argc, _TCHAR* argv[])
{
/*
  1)�������� ����-����� �� ����� �������
  2)��� ����������� ������ ������� �������� ����� �������� ���� for � ����� ������
  */

SetConsoleCP(1251);
SetConsoleOutputCP(1251);
  book *library=0;
  int bookAmount=0;
  int YesOrNot=1;//���������� ��� ���������� ����;
  int god;
  int variant;
  library=CreateStruct(library);
do
  {
  system("cls");
   variant = prompt_menu_item();

	switch (variant)
	{
		case 1:
			showData(library,bookAmount);
			break;
		case 2:
		{
			library=AddStruct(library,bookAmount);
			setData(library,bookAmount);
			bookAmount++;
			break;
		}
		case 3:
		{
			cout<<"\n\n������� ��� ��� ������ ��������� :";
			cin>>god;
			search(library,bookAmount,god);
			break;
		}
		case 4:
			DeleteAndChange(library,bookAmount,1);
			break;
		case 5:
			DeleteAndChange(library,bookAmount,0);
			break;
		case 6:
			ShellSort(library,bookAmount,god);
			break;
		case 7:
			cout << "�����..."<<endl;
			exit(EXIT_SUCCESS);
			break;
		default:
		{
			cout << "�� ������� �������� �������" << endl;
			break;
		}
	}
	getch();
  }while(variant!=7);

	  delete[] library;

	return 0;
}

  book *CreateStruct(book *Obj)
	{
		   Obj=new book[1];//��������� ������ ��� ������ ���������

		return Obj;
	}

  book *AddStruct(book *Obj,const int amount)
	{
		   book *tempObj=new book[amount+1];
			 for(int i=0;i<amount;i++)
			   {
				  tempObj[i]=Obj[i];//�������� �� ��������� �����
			   }
			 delete []Obj;
			 Obj=tempObj;

		return Obj;
	}

  void setData(book *Obj,const int amount)
	{
		cout<<"��������������� ����� :";
		cin>>Obj[amount].RegistrationNumber;
		cin.get();

		cout<<"����� :";
		cin.getline(Obj[amount].author,32);

		cout<<"�������� :";
		cin.getline(Obj[amount].nazvanie,32);

		cout<<"��� ������� :";
		cin>>Obj[amount].year;
		cin.get();

		cout<<"������������ :";
		cin.getline(Obj[amount].izdatelstvo,32);

		cout<<"���������� ������� :";
		cin>>Obj[amount].pages;
		cin.get();
	}

  void showData(const book *Obj,const int amount)
	{
		system("cls");
		printf("�         ���.�����           �����               ��������            ��� �������     ������������    ���-�� �������");
		cout<<"\n=====================================================================================================================";
		  for(int i=0;i<amount;i++)
			{
				//cout<<"\n"<<i+1<<"\t"<<Obj[i].RegistrationNumber<<"\t"<<Obj[i].author<<"\t"<<Obj[i].nazvanie<<"\t"<<Obj[i].year<<"\t"<<Obj[i].izdatelstvo<<"\t"<<Obj[i].pages<<endl;
				printf("\n %d %10d %20s %20s          %10d %20s %10d",i+1,Obj[i].RegistrationNumber,Obj[i].author,Obj[i].nazvanie,Obj[i].year,Obj[i].izdatelstvo,Obj[i].pages);
			}
	}

  void search(book *Obj,const int amount,int a)
	{
	  bool c=false;
	   printf("�         ���.�����           �����               ��������            ��� �������     ������������    ���-�� �������");
	   for(int i=0;i<amount;i++)
		 {
			 if(Obj[i].year==a)
			   {
				  cout<<endl;
				  printf("\n %d %10d %20s %20s          %10d %20s %10d",i+1,Obj[i].RegistrationNumber,Obj[i].author,Obj[i].nazvanie,Obj[i].year,Obj[i].izdatelstvo,Obj[i].pages);
				  c=true;
			   }
		 }
	   if(c==false)
		 {
			 printf("��������� �� �������");
		 }
	}

  void DeleteAndChange(book *Obj,const int amount,int vybor)
	{
		int rr,i=0;
		cout<<"������� ��� ��� ������ ��������� :";
		cin>>rr;
		  if(Obj[i].year==rr)
			{
				if(vybor==0)
				  {
					 Obj[i].RegistrationNumber=0;
					 memset(Obj[i].author,' ',32);//��������� ��������� ���� "�����" �� 32 �������(�.� ���������)
					 memset(Obj[i].nazvanie,' ',32);
					 Obj[i].year=0;
					 memset(Obj[i].izdatelstvo,' ',32);
					 Obj[i].pages=0;
				  }
				if(vybor==1)
				  {
					 cout<<"��������������� ����� ("<<Obj[i].RegistrationNumber<<"):";
					 cin>>Obj[amount].RegistrationNumber;
					 cin.get();

					 cout<<"����� ("<<Obj[i].author<<"):";
					 cin.getline(Obj[amount].author,32);

					 cout<<"�������� ("<<Obj[i].nazvanie<<"):";
					 cin.getline(Obj[amount].nazvanie,32);

					 cout<<"��� ������� ("<<Obj[i].year<<"):";
					 cin>>Obj[amount].year;
					 cin.get();

					 cout<<"������������ ("<<Obj[i].izdatelstvo<<"):";
					 cin.getline(Obj[amount].izdatelstvo,32);

					 cout<<"���������� ������� ("<<Obj[i].pages<<"):";
					 cin>>Obj[amount].pages;
					 cin.get();
				  }
			}

	}

  void ShellSort(book *Obj, const int amount,int god)
	{
	  int i, j, step,n=amount;
	  int tmp;
	  for (step = n / 2; step > 0; step /= 2)
		{
		  for (i = step; i < n; i++)
			{
			  tmp = Obj[i].year;
				for (j = i; j >= step; j -= step)
				  {
					if (tmp < Obj[j - step].year)
					  {
						Obj[j].RegistrationNumber = Obj[j - step].RegistrationNumber;
						strcpy(Obj[j].author,Obj[j-step].author);
						strcpy(Obj[j].nazvanie,Obj[j-step].nazvanie);
						Obj[j].year = Obj[j - step].year;
						strcpy(Obj[j].izdatelstvo,Obj[j-step].izdatelstvo);
						Obj[j].pages = Obj[j - step].pages;
					  }
					else
					  break;
				  }
			  Obj[j].year = tmp;
			}
		}
	cout<<"������� ��� �� �������� ����� ����������� ����� :";
	cin>>god;
	 printf("�         ���.�����           �����               ��������            ��� �������     ������������    ���-�� �������");
	 cout<<endl;
	 bool c=false;
	  for(i=0;i<amount;i++)
		{
			if(Obj[i].year>god)
			  {
				 printf("\n %d %10d %20s %20s          %10d %20s %10d",i+1,Obj[i].RegistrationNumber,Obj[i].author,Obj[i].nazvanie,Obj[i].year,Obj[i].izdatelstvo,Obj[i].pages);
				 c=true;
			  }

		}
			if(c==false)
			  {
			 printf("��������� �� �������");
			  }
	}
//---------------------------------------------------------------------------
