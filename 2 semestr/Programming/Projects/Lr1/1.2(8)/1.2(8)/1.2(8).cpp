
#include <stdio.h>
#include <conio.h>
#include <stdlib.h>
#include <ctype.h>


int main()
{
	system("chcp 1251"); // переходим в консоли на русский язык

	char ch[5], temp, temp1;
	printf("\nВведите четырехзначное число ch=");
	  for (int i = 0; i <= 3; i++)
	    {
		  scanf_s("%c", &ch[i]);
	    }
	  //сделать проверку на ввод(например,пользователь вводит трехзначное число
	  ch[4] = '\0';
	  printf("%s", ch);
	temp = ch[0];
	temp1 = ch[1];
	ch[0] = ch[2];
	ch[1] = ch[3];
	ch[2] = temp;
	ch[3] = temp1;
	if (ch[0] == '0')
	  {
		for (int i = 0; i <= 2; i++)
			ch[i] = ch[i + 1];
		ch[3] = '\0';
	  }
	printf("\n%s", ch);

	return 0;

}
