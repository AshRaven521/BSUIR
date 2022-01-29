#include <iostream>
#include "lfsr.h"

void lfsr_calculate(uint16_t *reg) {

  unsigned a = get_bit(reg,0);
  unsigned b = get_bit(reg,2);
  unsigned c = get_bit(reg,3);
  unsigned d = get_bit(reg,5);

  unsigned res = ((a ^ b) ^ c) ^ d;

  *reg >>= 1;

  set_bit(reg, 15, res);
}

unsigned get_bit(uint16_t *reg ,unsigned n) {
  
  return (*reg >> n) & 1;
}
  
void set_bit(uint16_t * x,
             unsigned n,
             unsigned v) {
  *x = (*x & ~(1 << n)) | (v << n);
}

   

