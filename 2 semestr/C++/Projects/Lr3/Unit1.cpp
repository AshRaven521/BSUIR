//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "Unit1.h"
#include "Unit2.h"
//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"
TForm1 *Form1;
List lst;

//---------------------------------------------------------------------------
__fastcall TForm1::TForm1(TComponent* Owner)
	: TForm(Owner)
{
}
//---------------------------------------------------------------------------

//---------------------------------------------------------------------------



//---------------------------------------------------------------------------
void TForm1::MemoToForm(int iPal)
{
	Memo1->Lines->Clear();

	for(int i=0;i<lst.size;i++)
	{
		Products* element=lst.Pop(i);
		if( iPal==-1) //-1 ������������ ��� ������ ���� ���������� �� �����(�.�. �� ������ ������� ������� ��� ������)
		{
			std::string str=element->name;
			AnsiString strStr=str.c_str();
			AnsiString strMemo="������������: "+strStr+"; ����������: ";
			strMemo=strMemo+IntToStr(element->quantity)+"; ����� ����: ";
			strMemo=strMemo+IntToStr(element->WorkNumber)+";";

			Memo1->Lines->Add(strMemo);
		}
	}
}
//---------------------------------------------------------------------------

void __fastcall TForm1::Button9Click(TObject *Sender)
{
	MemoToForm(-1);
}
//---------------------------------------------------------------------------


void __fastcall TForm1::Button8Click(TObject *Sender)
{
	if(lst.current<lst.size-1)
	{
		lst.current++;
		ObjToForm();
		Label7->Caption="������� ����: "+IntToStr(lst.current+1);
	}
}
//---------------------------------------------------------------------------

void __fastcall TForm1::Button7Click(TObject *Sender)
{
	if(lst.current>0)
	{
		lst.current--;
		ObjToForm();
		Label7->Caption="������� ����: "+IntToStr(lst.current+1);
	}
}
//---------------------------------------------------------------------------

void __fastcall TForm1::Button6Click(TObject *Sender)
{
	Products* element=lst.Pop(lst.current);
	AnsiString str=Edit1->Text;
	element->name=str.c_str();
	str=Edit2->Text;
	element->quantity=StrToInt(str);
	element->WorkNumber=ComboBox3->ItemIndex;
}
//---------------------------------------------------------------------------


void __fastcall TForm1::FormCreate(TObject *Sender)
{

	ComboBox3->Items->Add("�� ������");
	ComboBox3->Items->Add("1");
	ComboBox3->Items->Add("2");
	ComboBox3->Items->Add("3");
	ComboBox3->Items->Add("4");
	ComboBox3->Items->Add("5");
	ComboBox3->ItemIndex=0;

	ComboBox2->Items->Add("���������");
	ComboBox2->Items->Add("����� ����");
	ComboBox2->Items->Add("����������");
	ComboBox2->ItemIndex=0;

	Label6->Caption="���������� �����: 0";
	Label7->Caption="������� ������� ����: 0";
}
//---------------------------------------------------------------------------

void TForm1::ObjToForm()
{
	Products* element=lst.Pop(lst.current);

	std::string str=element->name;
	Edit1->Text=str.c_str();
	Edit2->Text=IntToStr(element->quantity);
	ComboBox3->ItemIndex=element->WorkNumber;
}

void __fastcall TForm1::Button1Click(TObject *Sender)
{
	lst.AddList("",0,0);

	ObjToForm();

	Label6->Caption="���������� �����: "+IntToStr(lst.size);;
	Label7->Caption="������� ����: "+IntToStr(lst.current+1);
}
//---------------------------------------------------------------------------

void __fastcall TForm1::Button10Click(TObject *Sender)
{
	lst.DeleteUzel(lst.current);

	ObjToForm();

	Label6->Caption="���������� �����: "+IntToStr(lst.size);;
	Label7->Caption="������� ����: "+IntToStr(lst.current+1);
}
//---------------------------------------------------------------------------

void __fastcall TForm1::Button5Click(TObject *Sender)
{
	Memo1->Lines->Clear();

	AnsiString strF=Edit4->Text;
	std::string search=strF.c_str();
	std::string source="";
	int iPos=-1;
	for(int i=0;i<lst.size;i++)//�� ���������� �����
	{
		Products* element=lst.Pop(i);//������� pop ���������� ������� ����
		if(ComboBox2->ItemIndex==0)
		{
			source=element->name;
			iPos=source.find(search);//find-���������� ����� �������,�.� iPos!=-1
		}

		if(ComboBox2->ItemIndex==1)
		{
			AnsiString str="";
			str=IntToStr(element->WorkNumber);
			source=str.c_str();
			iPos=source.find(search);
		}

		if(ComboBox2->ItemIndex==2)
		{
			iPos=-1;
			if(StrToInt(strF) <= element->quantity)//�������� �� ������� ������ ���������,�� ������ ���������
			{
				iPos=1;
			}
		}

		if(iPos!=-1)//�����
		{
			std::string str=element->name;
			AnsiString strStr=str.c_str();
			AnsiString strMemo="������������: "+strStr+"; ����������: ";
			strMemo=strMemo+IntToStr(element->quantity)+"; ����� ����: ";
			strMemo=strMemo+IntToStr(element->WorkNumber)+";";
			Memo1->Lines->Add(strMemo);
		}
	}
}
//---------------------------------------------------------------------------

