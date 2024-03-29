//---------------------------------------------------------------------------


#pragma hdrstop

#include "Unit2.h"
#include <cstring>
#include <windows.h>
#include <vcl.h>
#include "ctype.h"

#pragma once


int stack::prior(char x)
{
	if ((x == '*') || (x == '/')) return 2;
	if ((x == '+') || (x == '-')) return 1;
	if ((x == '(') || (x == ')')) return 0;
}

bool stack::empty()
{
	bool b;
	if(top==0)
	{
		b=true;
	}
	else
	{
		b=false;
	}
	return b;
}

char stack::get_top_element()
{
	char x;
	x=body[top];
	return x;
}

int stack::top_prior()
{
	int a=prior(body[top]);
	return a;
}

void stack::push(char x)
{
	top++;
	body[top] = x;
}

char stack::pop()
{
	char y=body[top];
	top--;
	return y;
}




void stackdecode::push(double x)
{
	top++;
	sum.push_back(x);
}

void stackdecode::pop()
{
	top--;
	sum.erase(sum.end() - 1);/*end ���������� �������� �� ����� ����� ���������� �������� �������,������� ��������� -1
								erase ������� ����� �  �����,������� ������ get_top_element */
}

double stackdecode::get_top_element()
{
	return sum[top - 1];
}

double stackdecode::Calc(const std::string& pnote)
{
	stackdecode final;

	//����� ������ �� �������
	char temparr[20];
	strcpy(temparr,pnote.c_str());
	char *pch = strtok(temparr," ");
   /*	if(pch==","&&pch==""&&pch=="."&&pch=="!"&&pch=="/")
	{
		MessageBox(,L"������� ������ �������",L"���������",NULL);
	}*/
	while(pch!=NULL)
	{
		if(*pch=='+')
		{
			double oper1 = final.get_top_element();
			final.pop();
			double oper2 = final.get_top_element();
			final.pop();
			final.push(oper2 + oper1);
		}
		else
		{
			if(*pch=='-')
			{
				double oper1 = final.get_top_element();
				final.pop();
				double oper2 = final.get_top_element();
				final.pop();
				final.push(oper2 - oper1);
			}
			else
			{
				if(*pch=='*')
				{
					double oper1 = final.get_top_element();
					final.pop();
					double oper2 = final.get_top_element();
					final.pop();
					final.push(oper2 * oper1);
				}
				else
				{
					if(*pch=='/')
					{
						double oper1 = final.get_top_element();
						final.pop();
						double oper2 = final.get_top_element();
						final.pop();
						final.push(oper2 / oper1 * 1.0);
					}
					else
					{
						//final.push(std::atof(pch));
						double temp=0;
						switch(*pch)
						{
							case 'a': temp=a;break;
							case 'b': temp=b;break;
							case 'c': temp=c;break;
							case 'd': temp=d;break;
							case 'e': temp=e;break;
						}
						final.push(temp);
					}
				}
			}
		}

		pch=strtok(NULL," ");
	}

	double cd=final.get_top_element();
	return cd;
}

std::string stackdecode::OPZ(const char(&note)[100])
{
	int i = 0;
	stack s;
	std::string pnote;
	if (note[0] == '-')
	{
		pnote += note[0];
		i++;
	}

	for (i=0; i < strlen(note); i++)
	{
		if (note[i] == '(')
		{
			s.push(note[i]);
		}
		else if ((note[i] == '+') || (note[i] == '-') || (note[i] == '/') || (note[i] == '*'))
		{
			while ((!s.empty()) && (s.top_prior() >= s.prior(note[i])))
			{
				pnote += s.pop();
				pnote+= " ";
			}
			s.push(note[i]);
		}
		else if (note[i] == ')')
		{
			while ((!s.empty()) && (s.get_top_element() != '('))
			{
				pnote += s.pop();
				pnote += " ";
			}
			s.pop();
		}
		else
		if(isalpha(note[i]))
		{
			std::string temp = "";
			while (i < strlen(note) && note[i] != '+' && note[i] != '-' && note[i] != '/' && note[i] != '*' && note[i] != '(' && note[i] != ')')
			{
				temp += note[i];
				i++;
			}
			pnote += temp += " ";
			i--;
		}

		else if(note[i]!=' ')
		{
		 ShowMessage("Error");
		 return "None";
		}
	}
	while (!s.empty())
	{
		pnote += s.pop();
	}
	return pnote;

}


//---------------------------------------------------------------------------

#pragma package(smart_init)
