
#ifndef RISCV_SIM_CPU_H
#define RISCV_SIM_CPU_H

#include "Memory.h"
#include "Decoder.h"
#include "RegisterFile.h"
#include "CsrFile.h"
#include "Executor.h"

class Cpu
{
public:
    Cpu(Memory& mem)
        : _mem(mem)
    {

    }

    void ProcessInstruction()
    {
        /* YOUR CODE HERE */

        InstructionPtr instructionPointer = _decoder.Decode(_mem.Request(_ip));

        _rf.Read(instructionPointer);  
        _csrf.Read(instructionPointer);

        _exe.Execute(instructionPointer, _ip);
        _mem.Request(instructionPointer);
        
        _rf.Write(instructionPointer);
        _csrf.Write(instructionPointer);

        _csrf.InstructionExecuted();
        _ip = instructionPointer->_nextIp;
    }

    void Reset(Word ip)
    {
        _csrf.Reset();
        _ip = ip;
    }

    std::optional<CpuToHostData> GetMessage()
    {
        return _csrf.GetMessage();
    }

private:
    Reg32 _ip;
    Decoder _decoder;
    RegisterFile _rf;
    CsrFile _csrf;
    Executor _exe;
    Memory& _mem;
};


#endif //RISCV_SIM_CPU_H
