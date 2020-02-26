//---------------------------------------------------------------------------


#pragma hdrstop

#include "Unit2.h"

//---------------------------------------------------------------------------

void RectangleMy::SetCoordinates(int x1,int y1,int x2,int y2)
{
	xRect1=x1;
	yRect1=y1;
	xRect2=x2;
	yRect2=y2;
}


int RectangleMy::GetX1()
{
	return xRect1;
}

int RectangleMy::GetY1()
{
	return yRect1;
}

int RectangleMy::GetX2()
{
	return xRect2;
}

int RectangleMy::GetY2()
{
	return yRect2;
}

#pragma package(smart_init)
