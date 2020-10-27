//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include <tchar.h>
#include <iostream.h>
#include <conio.h>
#include <math.h>
#include <windows.h>
//---------------------------------------------------------------------------

#pragma argsused
int _tmain()
{
   SetConsoleCP(1251);
   SetConsoleOutputCP(1251);
	 int size=0;
	 cout<<"Enter size square: ";
	 cin>>size;

	 if (!(size & 1))
	 {
		 cout<<"size should be even";
		 getch();
		 exit(1);
	 }

	 int max_size=size+(size-1);

	 int**pMagic_square = new  int*[max_size];
	 for (int i=0; i<max_size; i++)
		pMagic_square[i]=new int[max_size];

	 int i,j;
	 for (i=0; i<max_size; i++)
	 for (j=0; j<max_size; j++)
	 pMagic_square[i][j]=0;
	  int k=1;
	 i=(max_size>>1);
	 j=0;
	 while (i<max_size)
	 {
		 int i1=i;
		 int j1=j;
		 int gr=size>>1; //gr-расстояние от границы увеличенного массива
		 while (j1<((j+(max_size>>1))+1))
		 {
			 pMagic_square[i1][j1]=k;
			 if ((i1>(gr)) && (j1<(gr))) //left
				pMagic_square[i1][j1+size]=k;
			 if ((i1>(size+gr)-1) && (j1>(gr)))
				pMagic_square[i1-size][j1]=k; //down
			 if ((i1<(gr)) && (j1>(gr)))
				pMagic_square[i1+size][j1]=k; //up
			 if ((i1>(gr)) && (j1>(size+gr)-1))
				pMagic_square[i1][j1-size]=k; //right
			 --i1;
			 ++j1;
			 ++k;
		 }
		 ++i;
		 ++j;
	 }
	 cout<<endl<<"Magic square: "<<endl<<endl;
	 for (i=(size-1)>>1; i<=max_size-(size>>1)-1; i++ ){
		 for (j=(size-1)>>1; j<=max_size-(size>>1)-1; j++ )
			 cout << pMagic_square[i][j] << " ";
		 cout << std::endl;
	 }

	 for (int i = 0; i < max_size; ++i)
		delete[] pMagic_square[i];
		delete[] pMagic_square;


	 getch();;
	 return 0;
}
//---------------------------------------------------------------------------
