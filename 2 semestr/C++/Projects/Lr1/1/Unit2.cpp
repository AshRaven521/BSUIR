//---------------------------------------------------------------------------


#pragma hdrstop

#include "Unit2.h"

//---------------------------------------------------------------------------

RectangleMy::RectangleMy(int a,int b,int c,int d)
{
	xRect1=a;
	yRect1=b;
	xRect2=c;
	yRect2=d;
	x1=0;
	y1=0;
}

RectangleMy::RectangleMy()
{
	xRect1=100;
	yRect1=150;
	xRect2=200;
	yRect2=250;
	x1=0;
	y1=0;
}

void RectangleMy::show(TCanvas *Canvas)//����������,��� ����� show ����������� ������ RectangleMy
{
  Canvas -> Pen -> Color=clYellow;
  Canvas -> Brush ->Color=clYellow;
  Canvas -> Rectangle(xRect1,yRect1,xRect2,yRect2);
}

void RectangleMy::move(TCanvas *Canvas)
{
	xRect1+=1;
	yRect1-=5;
	xRect2+=1;
	yRect2-=5;
	x1+=1;
	y1-=1;
}

void RectangleMy::moveBack(TCanvas *Canvas)
{
	xRect1-=1;
	yRect1+=5;
	xRect2-=1;
	yRect2+=5;
	x1-=1;
	y1+=1;
}

void RectangleMy::hide(TCanvas *Canvas)
{
	Canvas -> Pen -> Color=clWhite;
	Canvas -> Brush -> Color=clWhite;
	Canvas -> Rectangle(xRect1,yRect1,xRect2,yRect2);
}

flagman::flagman(): RectangleMy(70,70,100,100)
{

}



void flagman::ShowFlag(TCanvas *Canvas)// ����� show ����������� ������ RectangleMy
{
  RectangleMy::show(Canvas);

  Canvas -> Pen -> Color=clBlue;
  Canvas -> Brush ->Color=clBlue;
  Canvas -> Rectangle(xRect1+32+x1,yRect1+50-y1,118,125);
  Canvas -> Pen -> Color=clRed;
  Canvas -> Brush ->Color=clRed;
  Canvas -> Rectangle(xRect1+31,yRect1,xRect2+3,yRect2+25);
}

void flagman::HideFlag(TCanvas *Canvas)
{
	RectangleMy::hide(Canvas);

	Canvas -> Rectangle(xRect1+32+x1,yRect1+50-y1,118,125);
	Canvas -> Rectangle(xRect1+31,yRect1,xRect2+3,yRect2+25);
}

void flagman::ShowMan(TCanvas *Canvas)
{
	Canvas -> Pen -> Color=clBlue;
	Canvas -> Brush ->Color=clBlue;
	Canvas -> Rectangle(122,100,138,118);
	Canvas -> Rectangle(120,120,140,170);
	Canvas -> Rectangle(142,120,160,125);
	Canvas -> Rectangle(120,172,128,190);
	Canvas -> Rectangle(132,172,140,190);
	Canvas -> Pen -> Color=clGreen;
	Canvas -> Brush ->Color=clGreen;
	Canvas -> Rectangle(10,191,438,200);
}

void flagman::show(TCanvas *Canvas)
{
	this->ShowMan(Canvas);
	this->ShowFlag(Canvas);
}

#pragma package(smart_init)