//---------------------------------------------------------------------------

#ifndef Unit2H
#define Unit2H

#include <iostream>
#include <string>
#include <vector>

class stack
{
	int top;
	char body[100];

	public:
		stack() { top = 0; }
		int prior(char x);
		bool empty();
		char get_top_element();
		int top_prior();
		void push(char x);
		char pop();
};


class stackdecode
{
	int top;
	std::vector<double> sum;

	public:
		float a,b,c,d,e;
		stackdecode()
		{
			top = 0;
			a=0,b=0,c=0,d=0,e=0;
		}

	void push(double x);
	void pop();
	double get_top_element();
	double Calc(const std::string& pnote);
	std::string OPZ(const char(&note)[100]);
};

//---------------------------------------------------------------------------
#endif
