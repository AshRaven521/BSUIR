#include <stdio.h>
#include <conio.h>
#include <stdlib.h>
#include <ctype.h>
#include <string.h>
#include <math.h>

struct data
{
	/*mvs-минуты в сети
	mvds-минуты в другие сети
	gorod-минуты на городскую линию
	 sms-кол-во смс */
	int mvs;
	int mvds;
	int gorod;
	int sms;
};

struct tarifs
{
	char name[16];
	float mvsprice;
	float mvdsprice;
	float gorodprice;
	float smsprice;
	float abonentprce;
};

void vvod(data&);
void raschet(tarifs*, data, float*,int);
void vyvod(tarifs*,  int );
void contacts(void);

// Вывести меню пользователю и вернуть номер варианта
int prompt_menu_item()
{
	// Выбранный вариант меню
	int variant;
	printf("Выберите вариант\n");
	printf("1. Ввод входных параметров\n"
		"2. Расчет оптимального тарифа\n"
		"3. Информационные услуги(перечень существующих тарифов с описанием)\n"
		"4. Обратная связь(контактная информация компании МТС)\n"
		"5. Выход из программы\n");
	printf(">>> ");
	scanf_s("%d", &variant);

	return variant;
}
int main()
{
	system("chcp 1251"); // переходим в консоли на русский язык
	data obj;
	obj.mvds = 1;
	obj.gorod = 1;
	obj.mvs = 1;
	obj.sms = 1;

	tarifs* proj;
	proj = new tarifs[3];

	strcpy_s(proj[0].name, "Безлимитище 2.0");
	proj[0].mvsprice = 0.045f;
	proj[0].mvdsprice = 0.095f;
	proj[0].abonentprce = 22.5f;
	proj[0].gorodprice = 0.095f;
	proj[0].smsprice = 0.075f;

	strcpy_s(proj[1].name, "Супер max+");
	proj[1].mvsprice = 0;
	proj[1].mvdsprice = 0;
	proj[1].abonentprce = 25.0;
	proj[1].gorodprice = 0;
	proj[1].smsprice = 0.0595f;

	strcpy_s(proj[2].name, "Супер");
	proj[2].mvsprice = 0.0469f;
	proj[2].mvdsprice = 0.1147f;
	proj[2].abonentprce = 10.4f;
	proj[2].gorodprice = 0.1147f;
	proj[2].smsprice = 0.0595f;

	int variant;
	int amount = 3;
	float result[3];


	do
	{
		system("cls");
		variant = prompt_menu_item();

		switch (variant)
		  {
		    case 1:
			  vvod(obj);
			  break;
		    case 2:
			  raschet(proj, obj, result,amount);
			  break;
			case 3:
				vyvod(proj, amount);
			case 4:
				contacts();
		  }

	} while (variant != 5);

}

void vvod(data& obj)
{
	printf("Введите количество минут в сети:");
	scanf_s("%d", &obj.mvs);

	printf("Введите количество минут в другие сети:");
	scanf_s("%d", &obj.mvds);

	printf("Введите количество минут для городских линий :");
	scanf_s("%d", &obj.gorod);

	printf("Введите количество sms :");
	scanf_s("%d", &obj.sms);
}

void raschet(tarifs* proj, data obj, float* result,int amount)
{
	float a = 10000.2f;
	int index = 0;

	for (int i = 0; i <amount ; i++)
	  {
		result[i] = (obj.mvs * proj[i].mvsprice) + (obj.mvds * proj[i].mvdsprice) + (obj.gorod * proj[i].gorodprice) + (obj.sms * proj[i].smsprice) + proj->abonentprce;
		printf("Ваш тарифный план: %s\n",proj[i].name);
		printf("Оплата составит: %.2f\n\n", result[i]);
		
		if (result[i] < a)
		  {
			a = result[i];
			index = i;
		  }
		
	  }
	printf("Оптимальный для вас план: %s", proj[index].name);
	int r=_getch();
}

void vyvod(tarifs* proj,  int amount)
  {
	for (int i = 0; i < amount; i++)
	  {
		printf("Название тарифа:%s\n", proj[i].name);
		printf("Стоимость минуты внутри сети: %.2f\n", proj[i].mvsprice);
		printf("Стоимость минуты во все сети: %.2f\n", proj[i].mvdsprice);
		printf("Стоимость минуты на городской линии: %.2f\n", proj[i].gorodprice);
		printf("Стоимость sms:%.2f\n", proj[i].smsprice);
		printf("Стоимость абонентской платы:%.2f\n\n", proj[i].abonentprce);
	  }
	int r=_getch();
  }

void contacts(void)
  {
	printf("Сайт: mts.by\n");
	printf("Онлайн-поддержка: help.mts.by/hc/ru\n");
	int r=_getch();
  }