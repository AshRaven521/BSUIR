//---------------------------------------------------------------------------

#include <vcl.h>
#include <windows.h>
#pragma hdrstop

#include "Unit1.h"
#include "Unit2.h"
//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"
TForm1 *Form1;

Hospital *hosp;

//---------------------------------------------------------------------------
__fastcall TForm1::TForm1(TComponent* Owner)
	: TForm(Owner)
{
	count=0;//������� ��� ������ ���������� ���������
	current=0;//������� ����� ��������
	iPlaces=2;//���������� ���� � ������
}

Hospital* TForm1::AddObjHosp(Hospital *Obj,int amount)
{
	Hospital *tempObj=new Hospital[amount+1];
	for(int i=0;i<amount;i++)
	{
		tempObj[i]=Obj[i];//�������� �� ��������� �����
	}
	delete []Obj;
	Obj=tempObj;

	return Obj;
}

void TForm1::ObjToForm()
{
	std::string str=hosp[current].name;
	Edit1->Text=str.c_str();//�������������� �� AnsiString(�� ��������� ��� ���������� � ����(TEdit)) � string � ��������
	ComboBox1->ItemIndex=hosp[current].pol;
	Edit2->Text=IntToStr(hosp[current].age);
	str=hosp[current].diagnoz;
	Edit3->Text=str.c_str();
	ComboBox3->ItemIndex=hosp[current].number;
}

void TForm1::MemoToForm(int iPal)
{
	Memo1->Lines->Clear();
	for(int i=0;i<count;i++)
	{
		if(hosp[i].number==iPal || iPal==-1)//���� ����������� ������ �������,�� �� ������� ���,��� ��������� � ������ �����,���� ����������� ������,�� �� ������ ����
		{
			std::string str=hosp[i].name;
			AnsiString strStr=str.c_str();
			AnsiString strMemo="���: "+strStr+"; ���: ";
			if(hosp[i].pol==0)
			{
				strStr="�������; ";
			}
			else
			{
				strStr="�������; ";
			}
			strMemo=strMemo+strStr+"�������: "+IntToStr(hosp[i].age)+"; �������: ";
			str=hosp[i].diagnoz;
			strStr=str.c_str();
			strMemo=strMemo+strStr+"; ����� ������: ";
			if(hosp[i].number==0)
			{
				strStr="�� ������. ";
			}
			else
			{
				strStr=IntToStr(hosp[i].number)+".";
			}
			strMemo=strMemo+strStr;
			Memo1->Lines->Add(strMemo);
		}
	}
}

//---------------------------------------------------------------------------
void __fastcall TForm1::Button2Click(TObject *Sender)
{
   if(OpenDialog1->Execute())
   {
		TFileStream *file=new TFileStream(OpenDialog1->FileName.c_str(),fmOpenRead);
		count=(file->Size)/sizeof(Hospital);//������� ���-�� ��������,����������� � �����
		if(count>0)
		{
			hosp=new Hospital[count];
			file->Read((void*)hosp,sizeof(Hospital)*count);
			current=count-1;

			ObjToForm();

			Label6->Caption="���������� �������: "+IntToStr(count);;
			Label7->Caption="������� ������: "+IntToStr(current+1);
		}
		else
		{
            MessageBox(Handle,L"������ � ����� �� �������",L"���������",NULL);
		}
		delete file;
   }


}
//---------------------------------------------------------------------------
void __fastcall TForm1::Button4Click(TObject *Sender)
{
	if(SaveDialog1->Execute())
	{
		TFileStream *file = new TFileStream(SaveDialog1->FileName.c_str(),fmCreate);
		int nWrite=file->Write((void*)hosp,sizeof(Hospital)*count);
		delete file;
	}
}
//---------------------------------------------------------------------------
void __fastcall TForm1::FormCreate(TObject *Sender)
{
	ComboBox1->Items->Add("�������");
	ComboBox1->Items->Add("�������");
	ComboBox1->ItemIndex=0;

	ComboBox3->Items->Add("�� ������");
	ComboBox3->Items->Add("1");
	ComboBox3->Items->Add("2");
	ComboBox3->Items->Add("3");
	ComboBox3->Items->Add("4");
	ComboBox3->Items->Add("5");
	ComboBox3->ItemIndex=0;

	ComboBox2->Items->Add("���");
	ComboBox2->Items->Add("�������");
	ComboBox2->Items->Add("����� ������");
	ComboBox2->ItemIndex=0;

	Label6->Caption="���������� �������: 0";
	Label7->Caption="������� ������: 0";
}
//---------------------------------------------------------------------------



void __fastcall TForm1::Button1Click(TObject *Sender)
{

	hosp=AddObjHosp(hosp,count);
	count++;
	current=count-1;

	hosp[current].name=" ";
	hosp[current].pol=0;
	hosp[current].age=0;
	hosp[current].diagnoz=" ";
	hosp[current].number=0;

	ObjToForm();

	Label6->Caption="���������� �������: "+IntToStr(count);;
	Label7->Caption="������� ������: "+IntToStr(current+1);
}
//---------------------------------------------------------------------------

void __fastcall TForm1::Button6Click(TObject *Sender)
{
	AnsiString str=Edit1->Text;
	hosp[current].name=str.c_str();
	hosp[current].pol=ComboBox1->ItemIndex;
	hosp[current].age=StrToInt(Edit2->Text);
	str=Edit3->Text;
	hosp[current].diagnoz=str.c_str();
	hosp[current].number=ComboBox3->ItemIndex;
}
//---------------------------------------------------------------------------


void __fastcall TForm1::Button5Click(TObject *Sender)
{
	Memo1->Lines->Clear();

	AnsiString str=Edit4->Text;
	std::string search=str.c_str();
	std::string source="";
	int iPos=0;
	for(int i=0;i<count;i++)
	{

	  if(ComboBox2->ItemIndex==0)
	  {

		source=hosp[i].name;
		iPos=source.find(search);
	  }

	  if(ComboBox2->ItemIndex==1)
	  {
		  source=hosp[i].diagnoz;
		  iPos=source.find(search);
	  }

	  if(ComboBox2->ItemIndex==2)
	  {
		if(hosp[i].number==StrToInt(str))
		{
		  iPos=1;
		}

	  else
	  {
		  iPos=0;
	  }

	  }
		if(iPos>0)
		{

		   std::string str=hosp[i].name;
		   AnsiString strStr=str.c_str();
		   AnsiString strMemo="���: "+strStr+"; ���: ";

		   if(hosp[i].pol==0)
		   {
				strStr="�������; ";
		   }

		   else
		   {
				strStr="�������; ";
		   }

		   strMemo=strMemo+strStr+"�������: "+IntToStr(hosp[i].age)+"; �������: ";
		   str=hosp[i].diagnoz;
		   strStr=str.c_str();
		   strMemo=strMemo+strStr+"; ����� ������: ";

		   if(hosp[i].number==0)
		   {
				strStr="�� ������. ";
		   }

		   else
		   {
				strStr=IntToStr(hosp[i].number)+".";
		   }

		   strMemo=strMemo+strStr;
		   Memo1->Lines->Add(strMemo);

		}
	}
}
//---------------------------------------------------------------------------

void __fastcall TForm1::Button7Click(TObject *Sender)
{
	if(current>0)
	{
		current--;
		ObjToForm();
		Label7->Caption="������� ������: "+IntToStr(current+1);
	}
}
//---------------------------------------------------------------------------

void __fastcall TForm1::Button8Click(TObject *Sender)
{
	if(current<count-1)
	{
		current++;
		ObjToForm();
		Label7->Caption="������� ������: "+IntToStr(current+1);
	}
}
//---------------------------------------------------------------------------

void __fastcall TForm1::Button9Click(TObject *Sender)
{
	MemoToForm(-1);
}
//---------------------------------------------------------------------------

void __fastcall TForm1::Button3Click(TObject *Sender)
{
	int countPal=ComboBox3->GetCount()-1;
	int iEach=0;
	int iPolPal=-1;
	std::string iDiagPal="";
	for(int j=0;j<count;j++)
	{
		hosp[j].number=0;//�������� ������ ����� � ���� ���������
	}
	for(int i=0;i<countPal;i++)//��������� ���� �� ���� �������
	{
		iEach=0;
		iPolPal=-1;
		iDiagPal=-1;
		for(int j=0;j<count;j++)//��������� ���� �� ��������� � ��������� ������� ���� ��������� � ������ ����� i+1
		{
			if(hosp[j].number==(i+1))
			{
				iEach++;
				iPolPal=hosp[j].pol;
			}
		}
		if(iEach<iPlaces)//���� ����� � ������ ����
		{
			for(int k=0;k<(iPlaces-iEach);k++)//��������� ���� �� ���������� ��������� ���� � ������
			{
				for(int l=0;l<count;l++)//��������� ���� �� ��������� � ������� ��� ������
				{
					if(hosp[l].number==0)//���� ��� ������
					{
						if(iPolPal==-1 || (hosp[i].pol==iPolPal  && hosp[i].diagnoz==iDiagPal))//���� ��� �������� ��������� � ��� ����������� � ������
						{
							hosp[l].number=i+1;//�������� � ������, ����� ���������
							iPolPal=hosp[l].pol;

							iDiagPal=hosp[l].diagnoz;
							break;//������� �� ����� ��������� � ��������� � ���������� ���������� �����
						}
					}
				}
			}
		}
	}
	MemoToForm(0);

	MessageBox(Handle,L"��������� ���������",L"���������",NULL);
}
//---------------------------------------------------------------------------

Hospital* TForm1::DeleteObjHosp(Hospital *Obj)
{
	Hospital* tmp = new Hospital[count - 1];

	  for (int j = 0; j <current ; j++)
	  {
		 tmp[j] = hosp[j];
	  }

	  for (int j = current; j < count - 1; j++)
	  {
		 tmp[j] = hosp[j + 1];
	  }
	delete[]hosp;
	hosp = tmp;
	count--;
}


void __fastcall TForm1::Button10Click(TObject *Sender)
{
	hosp=DeleteObjHosp(hosp);
	current=0;

	ObjToForm();

	Label6->Caption="���������� �������: "+IntToStr(count);;
	Label7->Caption="������� ������: "+IntToStr(current+1);
}
//---------------------------------------------------------------------------




