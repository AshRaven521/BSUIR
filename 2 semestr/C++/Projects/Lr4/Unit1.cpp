//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "Unit1.h"
#include "Unit2.h"
//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"
TForm1 *Form1;

stackdecode st;
//---------------------------------------------------------------------------
__fastcall TForm1::TForm1(TComponent* Owner)
	: TForm(Owner)
{
}
//---------------------------------------------------------------------------
void __fastcall TForm1::Button1Click(TObject *Sender)
{
	//a*(b-c)/(d+e)
	//1.6*(4.9-5.7)/(0.8+2.3)
	//a b c - * d e + /
	//1.6 4.9 5.7 - * 0.8 2.3 + /
	//-0.413

	AnsiString strStr=Edit1->Text;
	std::string str=strStr.c_str();

	AnsiString str1=Edit2->Text;
	st.a=StrToFloat(str1);

	AnsiString str2=Edit3->Text;
	st.b=StrToFloat(str2);

	AnsiString str3=Edit4->Text;
	st.c=StrToFloat(str3);

	AnsiString str4=Edit5->Text;
	st.d=StrToFloat(str4);

	AnsiString str5=Edit6->Text;
	st.e=StrToFloat(str5);


	Memo1->Clear();


	//�������������� � ����������� ����� (���)
	char note[100];
	strcpy(note,str.c_str());
	std::string pnote = st.OPZ(note);
	strStr=pnote.c_str();
	Memo1->Lines->Add("���: "+strStr);


	//���������� ���������
	double dd=st.Calc(pnote);
	Memo1->Lines->Add("���������: "+AnsiString(dd));


}
//---------------------------------------------------------------------------
void __fastcall TForm1::FormCreate(TObject *Sender)
{
	Edit1->Text="a*(b-c)/(d+e)";
	Edit2->Text="1,6";
	Edit3->Text="4,9";
	Edit4->Text="5,7";
	Edit5->Text="0,8";
	Edit6->Text="2,3";
	Memo1->Clear();
}
//---------------------------------------------------------------------------


