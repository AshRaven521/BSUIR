//---------------------------------------------------------------------------
#include <string>
#ifndef Unit2H
#define Unit2H

class Products
{
  public:
	std::string name;
	int quantity;
	int WorkNumber;
	Products *next;//указатель на следующий узел

	Products()
	{
		name="";
		quantity=0;
		WorkNumber=0;
		next=NULL;
	}
};

class List
{
   public:
	Products *head;//Указатель на корневой узел
	int size,current;

	//Конструктор:
	List()
	{
		head=new Products;
		head->next=NULL;
		size=0;
		current=-1;
	}

	//Деструктор:
	~List()
	{
		while (head!=NULL)          //Пока по адресу не пусто
	 {
		Products *temp = head->next;   //Временная переменная для хранения адреса следующего элемента
		delete head;                //Освобождаем адрес обозначающий начало
		head = temp;                  //Меняем адрес на следующий
	 }
	}

	void AddList(std::string str,int a,int b);//Передаем аргументы для изменения полей

	void DeleteUzel(int index);
	void Show();
	Products* Pop(int N);
	int Count() {return size;};
};


//---------------------------------------------------------------------------
#endif
