//---------------------------------------------------------------------------

#ifndef Unit2H
#define Unit2H

#include <string>
#include <System.hpp>


class Hospital
{
  public:
	std::string name;
	int pol;
	int age;
	std::string diagnoz;
	int number;

	Hospital()
	{
	   name="";
	   pol=0;
	   age=0;
	   diagnoz="";
	   number=0;
	}
};
//---------------------------------------------------------------------------
#endif
