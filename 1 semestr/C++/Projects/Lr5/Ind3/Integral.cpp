//---------------------------------------------------------------------------

#pragma hdrstop

#include <tchar.h>
#include <math.h>
#include <iostream.h>
#include <conio.h>
#include <windows.h>
//---------------------------------------------------------------------------

#pragma argsused

	 double getf(double,int);
	 double LeftRectangle(double ,double ,int,int);
	 double RightRectangle(double,double,int,int);
	 double CentralRectangle(double,double,int,int);
	 double Trapeze(double,double,int,int);
int _tmain(int argc, _TCHAR* argv[])
{
 SetConsoleCP(1251);
 SetConsoleOutputCP(1251);
	double b;
	double a;
	double I,h,Xi,f;
	int i,n,k;
cout<<"Введите n:";
cin>>n;
		a=0.8;
		b=1.6;
		k=1;
		cout<<"Integral1"<<endl;
		I=LeftRectangle(a,b,k,n);
		cout<<"I1="<<I<<endl<<endl;
		I=RightRectangle(a,b,k,n);
		cout<<"I2="<<I<<endl<<endl;
		I=CentralRectangle(a,b,k,n);
		cout<<"I3="<<I<<endl<<endl;
		I=Trapeze(a,b,k,n);
		cout<<"I4="<<I<<endl<<endl;

		a=0.5;
		b=1.8;
		k=2;
		cout<<"Integral2"<<endl;
		I=LeftRectangle(a,b,k,n);
		cout<<"I1="<<I<<endl<<endl;
		I=RightRectangle(a,b,k,n);
		cout<<"I2="<<I<<endl<<endl;
		I=CentralRectangle(a,b,k,n);
		cout<<"I3="<<I<<endl<<endl;
		I=Trapeze(a,b,k,n);
		cout<<"I4="<<I<<endl<<endl;

		a=1.2;
		b=2.4;
		k=3;
		cout<<"Integral3"<<endl;
		I=LeftRectangle(a,b,k,n);
		cout<<"I1="<<I<<endl<<endl;
		I=RightRectangle(a,b,k,n);
		cout<<"I2="<<I<<endl<<endl;
		I=CentralRectangle(a,b,k,n);
		cout<<"I3="<<I<<endl<<endl;
		I=Trapeze(a,b,k,n);
		cout<<"I4="<<I<<endl<<endl;

		a=0.4;
		b=1.2;
		k=4;
		cout<<"Integral4"<<endl;
		I=LeftRectangle(a,b,k,n);
		cout<<"I1="<<I<<endl<<endl;
		I=RightRectangle(a,b,k,n);
		cout<<"I2="<<I<<endl<<endl;
		I=CentralRectangle(a,b,k,n);
		cout<<"I3="<<I<<endl<<endl;
		I=Trapeze(a,b,k,n);
		cout<<"I4="<<I<<endl<<endl;


	getch();
	return 0;
}

   double getf(double Xi,int k)
	{
	   double S=0;
	   switch(k)
	   {
		   case 1:S=sqrt(0.3*powf(Xi,2)+2.3)/(1.8+(sqrt(2*Xi+1.6)));break;
		   case 2:S=(cos(powf(Xi,2))+0.6)/(1.2+sin(0.7*Xi+0.2));break;
		   case 3:S=1/(sqrt(0.5+powf(Xi,2)));break;
		   case 4:S=cos(Xi)/(Xi+2);break;
	   }

	   return S;
	}

   double LeftRectangle(double a,double b,int k,int n)
	 {
	double I,h,Xi,f;
	int i;

		  //Метод левых прямоугольников
		h=(b-a)/n;
		 I=0;
		  for(i=0;i<=n-1;i++)
			{
				Xi=a+i*h;
				 cout<<"Xi="<<Xi<<endl;
				f=getf(Xi,k);
				 cout<<"f(Xi)="<<f<<endl;
				I+=f;
				 cout<<"Ii="<<I<<endl;
			}
				 I=h*I;
	   return I;
	 }

	double RightRectangle(double a,double b,int k,int n)
	  {

	double I=0,h,Xi,f;
	int i;
		  //Метод правых прямоугоьников
		  h=(b-a)/n;
		   for(i=1;i<=n;i++)
			 {
			   Xi=a+i*h;
				 cout<<"Xi="<<Xi<<endl;
				f=getf(Xi,k);
				 cout<<"f(Xi)="<<f<<endl;
				I+=f;
				 cout<<"Ii="<<I<<endl;
			 }
			  I=h*I;
		return I;
	  }

	double CentralRectangle(double a,double b,int k,int n)
	  {

	double I=0,h,Xi,f;
	int i;
		  //Метод центральных прямоугольников
		  h=(b-a)/n;
			for(i=0;i<=n-1;i++)
			 {
			   Xi=a+i*h+h/2;
				 cout<<"Xi="<<Xi<<endl;
				f=getf(Xi,k);
				 cout<<"f(Xi)="<<f<<endl;
				I+=f;
				 cout<<"Ii="<<I<<endl;
			 }
			  I=h*I;
		return I;
	  }

	 double Trapeze(double a,double b,int k,int n)
	   {

	double I=0,h,Xi,f;
	int i;
		   //Метод трапеций
		   h=(b-a)/n;
		double *ps=new double [n];
		  for(i=0;i<=n;i++)
			 {
			   Xi=a+i*h;
				 cout<<"Xi="<<Xi<<endl;
				ps[i]=getf(Xi,k);
				 cout<<"f(Xi)="<<ps[i]<<endl;
					   if(i>0 && i<n)
						 I+=ps[i];

				 cout<<"Ii="<<I<<endl;
			 }
		 I=I+(ps[0]+ps[n])/2;
		 I=h*I;

		   delete [] ps;
		 return I;
	   }
//---------------------------------------------------------------------------
