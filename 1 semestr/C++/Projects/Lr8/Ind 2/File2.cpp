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
#include <fstream>
//---------------------------------------------------------------------------

#pragma argsused
  struct product
	{
		char nazvanie[32];
		int kolichestvo;
		int WorkshopNumber;
	};
product *CreateStruct(product *);
product *AddStruct(product *,const int );
void setData(product *,const int);
void showData(const product *,const int );
void DeleteAndChange(product * &, int &,int);
void Sortirovka(product *,const int);
void ShellSort(product *,const int);
void Save(product *, const int);
void Load(product* &, int &);
// ������� ���� ������������ � ������� ����� ��������
int prompt_menu_item()
{
	// ��������� ������� ����
	int variant;
	cout << "�������� �������\n" << endl;
	cout << "1. ����������� ����������\n"
		 << "2. �������� ���������\n"
		 << "3. �������� ���������\n"
		 << "4. ������� ���������\n"
		 << "5. ����������� ���������\n"
		 << "6. �����\n" << endl;
	cout << ">>> ";
	cin >> variant;
	return variant;
}
int _tmain(int argc, _TCHAR* argv[])
{
SetConsoleCP(1251);
SetConsoleOutputCP(1251);
//�������� ��������� �� �����(�� ������� �� ���)
product *Obj=0;
  int variant;
  int productAmount=0;

	Load(Obj,productAmount);


	do
	 {
  system("cls");
   variant = prompt_menu_item();

	switch (variant)
	{
		case 1:
			showData(Obj,productAmount);
			break;
		case 2:
		{
			Obj=AddStruct(Obj,productAmount);
			setData(Obj,productAmount);
			productAmount++;
			Save(Obj,productAmount);
			break;
		}
		case 3:
			DeleteAndChange(Obj,productAmount,1);
			Save(Obj,productAmount);
			break;
		case 4:
			DeleteAndChange(Obj,productAmount,0);
			Save(Obj,productAmount);
			break;
		case 5:
			Sortirovka(Obj,productAmount);
			break;
		case 6:
			Save(Obj,productAmount);
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
	 }while(variant!=6);

	  delete[] Obj;

	return 0;
}
  product *CreateStruct(product *Obj)
	{
		   Obj=new product[1];//��������� ������ ��� ������ ���������

		return Obj;
	}

  product *AddStruct(product *Obj,const int amount)
	{
		   product *tempObj=new product[amount+1];
			 for(int i=0;i<amount;i++)
			   {
				  tempObj[i]=Obj[i];//�������� �� ��������� �����
			   }
			 delete []Obj;
			 Obj=tempObj;

		return Obj;
	}

  void setData(product *Obj,const int amount)
	{
		cin.get();
		cout<<"������������ ������ :";
		cin.getline(Obj[amount].nazvanie,32);

		cout<<"���������� ������ :";
		cin>>Obj[amount].kolichestvo;
		cin.get();

		cout<<"����� ���� :";
		cin>>Obj[amount].WorkshopNumber;
		cin.get();
	}

  void showData(const product *Obj,const int amount)
	{
		system("cls");
		printf("�         �������� ������    ���-��    ����� ����");
		cout<<"\n======================================================";
		  for(int i=0;i<amount;i++)
			{
				printf("\n %d %20s %10d %10d",i+1,Obj[i].nazvanie,Obj[i].kolichestvo,Obj[i].WorkshopNumber);
			}
	}

  void DeleteAndChange(product* &Obj,int &amount,int vybor)
	{
		int rr,i=0;
		cout<<"������� ����� ���� :";
		cin>>rr;
		for(i=0;i<amount;i++)
		 {
		  if(Obj[i].WorkshopNumber==rr)
			{
				if(vybor==0)
				  {

					 product* tmp = new product[amount - 1];
					   for (int j = 0; j <i ; j++)
						 {
						   tmp[j] = Obj[j];
						 }
					   for (int j = i; j < amount - 1; j++)
						 {
						   tmp[j] = Obj[j + 1];
						 }
					 delete[]Obj;
					 Obj = tmp;
					 amount--;

				  }
				if(vybor==1)
				  {
					 cin.get();
					 cout<<"�������� ������ ("<<Obj[i].nazvanie<<"):";
					 cin.getline(Obj[i].nazvanie,32);

					 cout<<"���������� ������ ("<<Obj[i].kolichestvo<<"):";
					 cin>>Obj[i].kolichestvo;
					 cin.get();

					 cout<<"����� ���� ("<<Obj[i].WorkshopNumber<<"):";
					 cin>>Obj[i].WorkshopNumber;
					 cin.get();
				  }
			}

		 }
	}

  void Sortirovka(product *Obj,const int amount)
	{
	//�������� ��� ���� ������ ��������,����� ��� ���������� ��������� ������ ���� �������� ��� ������ � ����� ������,� ����� ��� �� ��������
	  int i=0,ff,productAmount2=0;
	  cout<<"������� ����� ���� :";
	  cin>>ff;
	  product *Obj2;
	   Obj2=CreateStruct(Obj2);
		for(i=0;i<amount;i++)
		  {
			 if(Obj[i].WorkshopNumber==ff)
			   {
				   Obj2=AddStruct(Obj2,productAmount2);
				   strcpy(Obj2[productAmount2].nazvanie,Obj[i].nazvanie);
				   Obj2[productAmount2].kolichestvo=Obj[i].kolichestvo;
				   Obj2[productAmount2].WorkshopNumber=Obj[i].WorkshopNumber;
				   productAmount2++;
			   }

		  }

	  ShellSort(Obj2,productAmount2);
	}

  void ShellSort(product *Obj2, const int productAmount2)
	{
	  int i, j, step,n=productAmount2;
	  int tmp,tmpWorkshop;
	  char tmpNazvanie[32];
	  for (step = n / 2; step > 0; step /= 2)
		{
		  for (i = step; i < n; i++)
			{
			  tmp = Obj2[i].kolichestvo;
			  tmpWorkshop=Obj2[i].WorkshopNumber;
			  strcpy(tmpNazvanie,Obj2[i].nazvanie);
				for (j = i; j >= step; j -= step)
				  {
					if (tmp > Obj2[j - step].kolichestvo)//��� ����� ����� ��������� ���������� �� ����������� �� ��������
					  {
						strcpy(Obj2[j].nazvanie,Obj2[j-step].nazvanie);
						Obj2[j].kolichestvo = Obj2[j - step].kolichestvo;
						Obj2[j].WorkshopNumber = Obj2[j - step].WorkshopNumber;
					  }
					else
					  break;
				  }
			  Obj2[j].kolichestvo = tmp;
			  Obj2[j].WorkshopNumber=tmpWorkshop;
			  strcpy(Obj2[j].nazvanie,tmpNazvanie);
			}
		}
	 printf("�         �������� ������       ���-��    ����� ����");
	 cout<<endl;
	  for(i=0;i<productAmount2;i++)
		{

		   printf("\n %d %20s %10d %10d",i+1,Obj2[i].nazvanie,Obj2[i].kolichestvo,Obj2[i].WorkshopNumber);

		}
	}

  void Save(product *Obj, const int amount)
	{
	  ofstream fout;
	  fout.open("EndlessData.txt", ios_base::trunc);//ios_base::trunc-����,������� ������� ���������� �����,���� �� ����������
		if(!fout.is_open())
		  {
			cout<<"������ ��� ����������  �����";
		  }
		else
		  {
			  cout<<"���� ������� ��������";

			for (int i = 1; i < amount; i++)
			  {
				fout<<i<<"\t"<<Obj[i].nazvanie<<"\t"<<Obj[i].kolichestvo<<"\t"<<Obj[i].WorkshopNumber<<endl;
				//fout.write((char*)&Obj[i], sizeof(product));
			  }
		  }
	  fout.close();
	}

  void Load(product* &Obj, int &amount)//���� �� ���������� & ,�� ��� �������� obj � main ��� ����� ������
	{

	   ifstream fin;
	   fin.open("BeginingData.txt");

		 if(!fin.is_open())
		   {
			 cout<<"������ �������� �����";
		   }
		 else
		   {
			 cout<<"���� ������� ������";
			 char ch[80];
			 char *fm;
			 int i=0;
			   while(fin.getline(ch,80))
				 {
				   Obj=AddStruct(Obj,amount);
					 fm = strtok(ch,"\t");
					 i=0;
					   while (fm != NULL)                         // ���� ���� �������
						 {
						   fm = strtok (NULL, "\t");
						   if(i==0)
						   strcpy(Obj[amount].nazvanie,fm);
						   if(i==1)
							Obj[amount].kolichestvo=atoi(fm);
						   if(i==2)
							Obj[amount].WorkshopNumber=atoi(fm);
						   i++;//������� ��� �������� ����� ��� �� ��������� � �����
						 }
					amount++;//���������� ������� ��������
				 }

		   }
	   fin.close();
	}
//---------------------------------------------------------------------------

//---------------------------------------------------------------------------
