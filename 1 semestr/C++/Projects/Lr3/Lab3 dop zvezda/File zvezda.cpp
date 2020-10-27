//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop
#include <iostream.h>
#include <math.h>
#include <conio.h>
#include <tchar.h>
//---------------------------------------------------------------------------

#pragma argsused
int _tmain(int argc, _TCHAR* argv[])
{
  int n1=1, n2=1, n;
  int k;
  cout << "Enter k: ";
  cin >> k;

  if(k == 1 || k == 2)
  {
	cout << "Element " << k << " = 1";
	getch();
	return 0;
  }
  //k >= 3
  int s = 2, digit_n, ans;
  while(s < k)//???true
  {
	n = n1 + n2;
	n1 = n2;
	n2 = n;

	digit_n = 0;
	for(int i = n; i != 0; i /= 10)
	  digit_n++;

	s += digit_n;
	if(s == k)
	  ans = n % 10;
	else if(s > k)
	{
	  int diff = s - k;
	  n /= int(pow(10, double(diff)));
	  ans = n % 10;
	}
  }

  cout << "Element " << k << " of sequence is " << ans;


  getch();
  return 0;
}
//---------------------------------------------------------------------------
