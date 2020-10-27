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
	Products *next;//��������� �� ��������� ����

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
	Products *head;//��������� �� �������� ����
	int size,current;

	//�����������:
	List()
	{
		head=new Products;
		head->next=NULL;
		size=0;
		current=-1;
	}

	//����������:
	~List()
	{
		while (head!=NULL)          //���� �� ������ �� �����
	 {
		Products *temp = head->next;   //��������� ���������� ��� �������� ������ ���������� ��������
		delete head;                //����������� ����� ������������ ������
		head = temp;                  //������ ����� �� ���������
	 }
	}

	void AddList(std::string str,int a,int b);//�������� ��������� ��� ��������� �����

	void DeleteUzel(int index);
	void Show();
	Products* Pop(int N);
	int Count() {return size;};
};


//---------------------------------------------------------------------------
#endif
