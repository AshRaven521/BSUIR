#include "ll_cycle.h"

bool ll_has_cycle(node *head)
{
  //turtle
  node *tempOneStep = head;
  //hare
  node *tempTwoStep = head;

  while(tempOneStep && tempTwoStep && tempTwoStep->next)
  {
    tempOneStep = tempOneStep->next;
    tempTwoStep = tempTwoStep->next->next;

    if(tempOneStep == tempTwoStep)
    {
      return true;
    }
  }
  return false;
}
