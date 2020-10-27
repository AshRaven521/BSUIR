int cMin(int *S,int m)
{
	int min=1000000;
	for(int j=0;j<m;j++)
	{
		if(S[j]<min)
		{
			min=S[j];
		}
	}

	return min;
}
