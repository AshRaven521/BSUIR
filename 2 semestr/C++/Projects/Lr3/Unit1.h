//---------------------------------------------------------------------------

#ifndef Unit1H
#define Unit1H
//---------------------------------------------------------------------------
#include <Classes.hpp>
#include <Controls.hpp>
#include <StdCtrls.hpp>
#include <Forms.hpp>
#include <Dialogs.hpp>
//---------------------------------------------------------------------------
class TForm1 : public TForm
{
__published:	// IDE-managed Components
	TLabel *Label1;
	TLabel *Label3;
	TLabel *Label5;
	TLabel *Label6;
	TLabel *Label7;
	TLabel *Label8;
	TButton *Button1;
	TButton *Button5;
	TEdit *Edit1;
	TEdit *Edit2;
	TComboBox *ComboBox3;
	TButton *Button6;
	TEdit *Edit4;
	TButton *Button7;
	TButton *Button8;
	TMemo *Memo1;
	TButton *Button9;
	TComboBox *ComboBox2;
	TButton *Button10;
	void __fastcall Button9Click(TObject *Sender);
	void __fastcall Button8Click(TObject *Sender);
	void __fastcall Button7Click(TObject *Sender);
	void __fastcall Button6Click(TObject *Sender);
	void __fastcall FormCreate(TObject *Sender);
	void __fastcall Button1Click(TObject *Sender);
	void __fastcall Button10Click(TObject *Sender);
	void __fastcall Button5Click(TObject *Sender);
private:	// User declarations
	void MemoToForm(int iPal);
public:		// User declarations
	void ObjToForm();
	__fastcall TForm1(TComponent* Owner);
};
//---------------------------------------------------------------------------
extern PACKAGE TForm1 *Form1;
//---------------------------------------------------------------------------
#endif
