//---------------------------------------------------------------------------

#pragma hdrstop

#include <tchar.h>
#include <iostream.h>
#include <windows.h>
#include <conio.h>
#include <cstring>
//---------------------------------------------------------------------------

#pragma argsused

int _tmain(int argc, _TCHAR* argv[])
{
  SetConsoleCP(1251);
  SetConsoleOutputCP(1251);
   int a;
   int strlength;
   char text[]= "1. Ќапечатать квитанцию об оплате за телеграмму, если известна стоимость одного слова. 2. ¬ тексте слова заданной длины заменить указанной подстрокой,";
   // длина которой может не совпадать с длиной слова. 3. ¬ тексте каждую букву заменить ее номером в алфавите. 4. ¬ тексте после указанного символа вставить подстроку. 5. ѕосле каждого слова текста, оканчивающегос€ заданной подстрокой, вставить указанный символ.";
		 cout<<"«аданный текст: "<<text;
		   cout<<"\n¬ведите длину слова: ";
		   cin>>a;

			char *strText=new char[strlen(text)+1];
			strcpy(strText,text);
			char * pch = strtok (text," ,.-");
			char * strx;
			char *symbol=new char[2];
			char *sFind;
			while (pch != NULL)
			{
			  strlength=strlen(pch);
			  if(strlength==a)
			  {
				  symbol[0]=pch[0];
				  symbol[1]='\0';
				  strx=strpbrk(symbol,"бЅв¬г√дƒж∆з«к лЋмћнЌпѕр–с—т“ф‘х’ц÷ч„шЎщў");
				  if(strx!=NULL)
				  {
					  cout<<"”даленные слова: "<<pch<<endl;
					  sFind=strstr(strText,pch);
					  if(sFind!=NULL)
					  {
						int aS=sFind-strText;
						memmove(&strText[aS],&sFind[strlen(pch)+1],strlen(sFind));
					  }

				  }
			  }
			  pch = strtok (NULL, " ,.-");
			}

	cout<<"–езультат задани€: "<<strText;
	getch();
	return 0;
}


//---------------------------------------------------------------------------
