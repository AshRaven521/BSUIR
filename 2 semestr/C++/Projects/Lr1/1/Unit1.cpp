//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "Unit2.h"
#include "Unit1.h"


//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"
TForm1 *Form1;
//RectangleMy rect;
flagman rect;
//---------------------------------------------------------------------------
__fastcall TForm1::TForm1(TComponent* Owner)
	: TForm(Owner)
  {
	iCount=0;
  }
//---------------------------------------------------------------------------


//---------------------------------------------------------------------------
void __fastcall TForm1::Timer1Timer(TObject *Sender)
{
	rect.ShowMan(Image1 -> Canvas);

   if(iCount<=5)
	 {
	   rect.HideFlag(Image1 -> Canvas);
	   rect.move(Image1 -> Canvas);
	   rect.ShowFlag(Image1 -> Canvas);
	 }

   iCount++;

   if(iCount>5)
	 {
	   rect.HideFlag(Image1 -> Canvas);
	   rect.moveBack(Image1 -> Canvas);
	   rect.ShowFlag(Image1 -> Canvas);
	   if(iCount>10)
		 iCount=0;
	 }

	//rect.show(Image1->Canvas);

}
//---------------------------------------------------------------------------
