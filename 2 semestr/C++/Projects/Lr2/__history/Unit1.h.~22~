//---------------------------------------------------------------------------

#ifndef Unit1H
#define Unit1H
//---------------------------------------------------------------------------
#include <Classes.hpp>
#include <Controls.hpp>
#include <StdCtrls.hpp>
#include <Forms.hpp>
#include <Dialogs.hpp>

#include "Unit2.h"
//---------------------------------------------------------------------------
class TForm1 : public TForm
{
__published:	// IDE-managed Components
	TButton *Button1;
	TButton *Button2;
	TButton *Button3;
	TButton *Button4;
	TButton *Button5;
	TEdit *Edit1;
	TLabel *Label1;
	TLabel *Label2;
	TComboBox *ComboBox1;
	TLabel *Label3;
	TEdit *Edit2;
	TLabel *Label4;
	TLabel *Label5;
	TComboBox *ComboBox3;
	TEdit *Edit3;
	TOpenDialog *OpenDialog1;
	TSaveDialog *SaveDialog1;
	TButton *Button6;
	TEdit *Edit4;
	TLabel *Label6;
	TLabel *Label7;
	TButton *Button7;
	TButton *Button8;
	TMemo *Memo1;
	TLabel *Label8;
	TButton *Button9;
	TComboBox *ComboBox2;
	TButton *Button10;
	void __fastcall Button2Click(TObject *Sender);
	void __fastcall Button4Click(TObject *Sender);
	void __fastcall FormCreate(TObject *Sender);
	void __fastcall Button1Click(TObject *Sender);
	void __fastcall Button6Click(TObject *Sender);
	void __fastcall Button5Click(TObject *Sender);
	void __fastcall Button7Click(TObject *Sender);
	void __fastcall Button8Click(TObject *Sender);
	void __fastcall Button9Click(TObject *Sender);
	void __fastcall Button3Click(TObject *Sender);
	void __fastcall Button10Click(TObject *Sender);
private:	// User declarations
	int count;
	int current;
	int iPlaces;
public:		// User declarations
	Hospital *AddObjHosp(Hospital *Obj,int amount);
	void ObjToForm();
	void MemoToForm(int iPal);
	Hospital *DeleteObjHosp(Hospital *Obj);
	__fastcall TForm1(TComponent* Owner);
};
//---------------------------------------------------------------------------
extern PACKAGE TForm1 *Form1;
//---------------------------------------------------------------------------
#endif
