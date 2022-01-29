#include <mmintrin.h>
#include <time.h>
#include <iostream>

void outpw(char name, int16_t* pw, int count);
void outpb(char name, int8_t* pb, int count);

__m64* upack_8_16(__m64* pb);

int main()
{
    int const count = 8;
    
    int8_t A[count], B[count], C[count];
    int16_t D[count], F[count];

    srand(time(0));
    for (int i = 0; i < count; i++)
    {
        A[i] = rand() % INT8_MAX - INT8_MAX / 2;
        B[i] = rand() % INT8_MAX - INT8_MAX / 2;
        C[i] = rand() % INT8_MAX - INT8_MAX / 2;
        D[i] = rand() % INT8_MAX - INT8_MAX / 2;
    }

    for (int i = 0; i < count; i++)
    {
        F[i] = (A[i] + B[i]) * (C[i] - D[i]);
    }

    outpb('A', A, count);
    outpb('B', B, count);
    outpb('C', C, count);
    outpw('D', D, count);
    outpw('F', F, count);

    //MMX
    auto mA = reinterpret_cast<__m64*>(A);
    auto mB = reinterpret_cast<__m64*>(B);
    auto mC = reinterpret_cast<__m64*>(C);
    auto mD = reinterpret_cast<__m64*>(D);
    auto mF = new __m64[2];

    //mA = upack_8_16(mA);
    //mB = upack_8_16(mB);
    mC = upack_8_16(mC);

    __m64 as = _mm_adds_pi8(*mA, *mB);
    //int8_t* V = reinterpret_cast<int8_t*>(&as);
    //outpb('V', V, count);
    __m64* res = upack_8_16(&as);
    
    mF[0] = _mm_mullo_pi16(res[0], _mm_sub_pi16(mC[0], mD[0]));
    mF[1] = _mm_mullo_pi16(res[1], _mm_sub_pi16(mC[1], mD[1]));
    //mF[0] = _mm_mullo_pi16(_mm_adds_pi16(mA[0], mB[0]), _mm_sub_pi16(mC[0], mD[0]));
    //mF[1] = _mm_mullo_pi16(_mm_adds_pi16(mA[1], mB[1]), _mm_sub_pi16(mC[1], mD[1]));

    int16_t* M = reinterpret_cast<int16_t*>(mF);
    outpw('M', M, count);
    
    std::cout << std::endl;
    return 0;
}


void outpw(char name, int16_t* pw, int count) 
{
    std::cout << name << "\t";
    for (int i = 0; i < count; i++) 
    {
        std::cout << int(pw[i]) << "\t";
    }
    std::cout << std::endl;
}

void outpb(char name, int8_t* pb, int count)
{
    std::cout << name << "\t";
    for (int i = 0; i < count; i++)
    {
        std::cout << int(pb[i]) << "\t";
    }
    std::cout << std::endl;
}

__m64* upack_8_16(__m64* pb) 
{
    __m64* res = new __m64[2];
    __m64 cmp = _mm_cmpgt_pi8(_mm_setzero_si64(), *pb);
    res[0] = _mm_unpacklo_pi8(*pb, cmp);
    res[1] = _mm_unpackhi_pi8(*pb, cmp);
    
    return res;
}


