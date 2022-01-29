
#ifndef RISCV_SIM_EXECUTOR_H
#define RISCV_SIM_EXECUTOR_H

#include "Instruction.h"

class Executor
{
public:
    void Execute(InstructionPtr& instr, Word ip)
    {
        /* YOUR CODE HERE */
        
        if(instr->_type == IType::Alu || instr->_type == IType::Br)
        {
            AluComputing(instr, instr->_data);
        }

        if(instr->_type == IType::Ld || instr->_type == IType::St)
        {
            AluComputing(instr,instr->_addr);
        }

        Word nextIp;
        switch (instr->_type)
        {
            case IType::Br:
            {
                nextIp = ip + instr->_imm.value();
                break;
            }
            case IType::St:
            {
                instr->_data = instr->_src2Val;
                break;
            }

            case IType::Csrr:
            {
                instr->_data = instr->_csrVal;
                break;
            }

            case IType::Csrw:
            {
                instr->_data = instr->_src1Val;
                break;
            }

            case IType::J:
            {
                nextIp = ip + instr->_imm.value();
                instr->_data = ip + 4;
                break;
            }

            case IType::Jr:
            {
                instr->_data = ip + 4;
                nextIp = instr->_src1Val + instr->_imm.value();
                break;
            }

            case IType::Auipc:
            {
                instr->_data = ip + instr->_imm.value();
                break;
            }
        }
        if(instr->_type == IType::Br || instr->_type == IType::J || instr->_type == IType::Jr)
        {
            TransitionComputing(instr, instr->_nextIp, ip, nextIp);
        }      
        else
        {
            TransitionComputing(instr, instr->_nextIp, ip);
        }
            
    }

private:
    /* YOUR CODE HERE */

    void TransitionComputing(InstructionPtr& instr, Word& field, Word currIp, std::optional<Word>next = NULL)
    {
        Word first_operator, second_operator, nextIp;
        if(instr->_src1 && instr->_src2)
        {
            first_operator = instr->_src1Val;
            second_operator = instr->_src2Val;
        }
       
        if(next)
        {
            nextIp = next.value();
        }

        switch (instr->_brFunc)
        {
            case BrFunc::Eq:
            {
                if((first_operator == second_operator) && nextIp)
                {
                    field = nextIp;
                }
                else
                {
                    //Берем адрес следующей инструкции
                    field = currIp + 4;
                }
                break;
            }

            case BrFunc::Neq:
            {
                if((first_operator != second_operator) && nextIp)
                {
                    field = nextIp;
                }
                else
                {
                    //Берем адрес следующей инструкции
                    field = currIp + 4;
                }
                break;
            }
        
            case BrFunc::Lt:
            {    
                if(((int32_t)first_operator < (int32_t)second_operator) && nextIp)
                {
                    field = nextIp;
                }
                else
                {
                    //Берем адрес следующей инструкции
                    field = currIp + 4;
                }
                break;
            }

            case BrFunc::Ltu:
            {    
                if((first_operator < second_operator) && nextIp)
                {
                    field = nextIp;
                }
                else
                {
                    //Берем адрес следующей инструкции
                    field = currIp + 4;
                }
                break;

            }
            case BrFunc::Ge:
            {    
                if(((int32_t)first_operator >= (int32_t)second_operator) && nextIp)
                {
                    field = nextIp;
                }
                else
                {
                    //Берем адрес следующей инструкции
                    field = currIp + 4;
                }
                break;
            }

            case BrFunc::Geu:
            {    
                if((first_operator >= second_operator) && nextIp)
                {
                    field = nextIp;
                }
                else
                {
                    //Берем адрес следующей инструкции
                    field = currIp + 4;
                }
                break;
            }

            case BrFunc::AT:
            {
                field = nextIp;
                break;
            }

            case BrFunc::NT:
            {
                field = currIp + 4;
                break;
            }
        }
    }

    void AluComputing(InstructionPtr& instr, Word& field)
    {
        Word first_operator, second_operator;

        if(instr->_src1 && (instr->_imm || instr->_src2))
        {
            first_operator = instr->_src1Val;
            if(instr -> _imm)
            {
                second_operator = instr -> _imm.value();
            }
            else
            {
                second_operator = instr -> _src2Val;
            }
        }
        else
        {
            return;
        }

        switch (instr->_aluFunc)
        {
            case AluFunc::Add:
            {
                field = first_operator + second_operator;
                break;
            }

            case AluFunc::Sub:
            {
                field = first_operator - second_operator;
                break;
            }

            case AluFunc::And:
            {
                field = first_operator & second_operator;
                break;
            }

            //Or - операция побитового ИЛИ
            case AluFunc::Or:
            {
                field = first_operator | second_operator;
                break;
            }

            //XOR - операция исключающего или
            case AluFunc::Xor:
            {
                field =  first_operator ^ second_operator;  
                break;
            }

            //Операция "меньше" для типов int32_t
            case AluFunc::Slt:
            {
                field = (int32_t)first_operator < (int32_t)second_operator;
                break;
            }

            //Операция "меньше" для типов uint32_t(т.к a и b типа Word(uint32_t) здесь не требуется явное приведение типов)
            case AluFunc::Sltu:
            {
                field = first_operator < second_operator;
                break;
            }

            //Сдвиг влево
            case AluFunc::Sll:
            {
                field = first_operator << (second_operator % 32);
                break;
            }

            //Беззнаковый сдвиг  сдвиг вправо
            case AluFunc::Srl:
            {
                field = first_operator >> (second_operator % 32);
                break;
            }

            case AluFunc::Sra:
            {
                field = (int32_t)first_operator >> ((int32_t)second_operator % 32);
                break;
            }
        }
    }

};

#endif // RISCV_SIM_EXECUTOR_H
