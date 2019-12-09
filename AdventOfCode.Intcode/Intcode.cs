using System;
using System.Linq;

namespace AdventOfCode.Intcode
{
    public  class Intcode
    {
        private long InstructionPointer { get; set; }
        private long[] Memory { get; set; }
        private long[] Input { get; set; }
        
        private long RelativeBase { get; set; }


        public Intcode()
        {
            Memory = new long[]{ };
            RelativeBase = 0;
        }

        public void LoadMemory(long[] instructions)
        {
            Memory = instructions;
            InstructionPointer = 0;
        }
        
        public void Run(long[] input, Action<long> output = null)
        {
            if (Memory.Length == 0)
            {
                throw new Exception("Progam is not loaded");
            }

            Input = input;
            
            var instructionCode = GetInstructionOptCode(GetMemoryValue(InstructionPointer));
            while (true)
            {
                switch(instructionCode)
                {
                    case 1:
                        Add();
                        break;
                    case 2:
                        Multiply();
                        break;
                    case 3:
                        Save();
                        break;
                    case 4:
                        PrintOut(output);
                        return;
                    case 5:
                        JumpIfTrue();
                        break;
                    case 6:
                        JumpIfFalse();
                        break;
                    case 7:
                        LessThan();
                        break;
                    case 8:
                        Equals();
                        break;
                    case 9:
                        AdjustRelativeBase();
                        break;
                    case 99:
                        return;
                }
                instructionCode = GetInstructionOptCode(GetMemoryValue(InstructionPointer));
            }
        }
   
        public static long GetInstructionOptCode(long code)
        {
            var codeStr = code.ToString();
            if (codeStr.Length > 1)
            {
                return long.Parse(codeStr.Substring(codeStr.Length - 2));
            }
            if(codeStr.Length == 1)
            {
                return (long)code;
            }
            throw new Exception();
        }
        
        public static long GetParameterMode(long code, long parameterNumber)
        {
            var codeStr = code.ToString();
            if (codeStr.Length < 2)
            {
                return 0;
            }

            if (codeStr.Length >= parameterNumber + 2)
            {
                return long.Parse(codeStr.Substring((int)codeStr.Length - 2 - (int)parameterNumber, 1));
            }
            return 0;
        }

        private long GetParameterValue(long parameterNumber, long optCodeIndex)
        {

            var optCode = GetMemoryValue(optCodeIndex);
            var parametertMode = GetParameterMode(optCode, parameterNumber);
            switch (parametertMode)
            {
                case 0:
                    return GetValueFromIndex(GetMemoryValue(optCodeIndex + parameterNumber));
                case 1:
                    return GetMemoryValue(optCodeIndex + parameterNumber);
                case 2:
                    return GetValueFromIndex(GetMemoryValue( optCodeIndex + parameterNumber) + RelativeBase);
                default:
                    throw new InvalidOperationException();
            }
        }
        
        private long GetWriteParameterValue(long parameterNumber, long optCodeIndex)
        {

            var optCode = GetMemoryValue(optCodeIndex);
            var parametertMode = GetParameterMode(optCode, parameterNumber);
            switch (parametertMode)
            {
                case 2:
                    return GetMemoryValue( optCodeIndex + parameterNumber) + RelativeBase;
                default:
                    return GetMemoryValue(optCodeIndex + parameterNumber);
            }
        }
        
        private  long GetValueFromIndex(long index)
        {
            return GetMemoryValue(index);
        }
      
        private void Add()
        {
            var left = GetParameterValue(1, InstructionPointer);
            var right = GetParameterValue(2, InstructionPointer);
            var oputputPosition = GetWriteParameterValue(3,InstructionPointer);;
            SetMemoryValue(oputputPosition, AddInternal(left, right));
            InstructionPointer += 4;
        }
  
        private void Multiply()
        {
            var left = GetParameterValue(1, InstructionPointer);
            var right = GetParameterValue(2, InstructionPointer);
            var oputputPosition = GetWriteParameterValue(3,InstructionPointer);;
            SetMemoryValue(oputputPosition,MultiplyInternal(left, right));
            InstructionPointer += 4;
        }
       
        private void Save()
        {
            var oputputPosition = GetWriteParameterValue(1,InstructionPointer);;
            SetMemoryValue(oputputPosition, GetInputParameter());
            InstructionPointer += 2;
        }
      
        private void PrintOut(Action<long> output)
        {
            output?.Invoke(GetParameterValue(1, InstructionPointer));
            InstructionPointer += 2;
        }
      
        private  void JumpIfTrue()
        {
            var firstInputParameter = GetParameterValue(1, InstructionPointer);
            if (firstInputParameter != 0)
            {
                InstructionPointer = GetParameterValue(2, InstructionPointer);
                return;
            }
            InstructionPointer += 3;
        }
     
        private  void JumpIfFalse()
        {
            var firstInputParameter = GetParameterValue(1, InstructionPointer);
            if (firstInputParameter == 0)
            {
                InstructionPointer = GetParameterValue(2, InstructionPointer);
                return;
            }
            InstructionPointer += 3;
        }

        private  void LessThan()
        {
            var firstInputParameter = GetParameterValue(1, InstructionPointer );
            var secondInputParameter = GetParameterValue(2, InstructionPointer);
            var oputputPosition = GetWriteParameterValue(3,InstructionPointer);;
            if (firstInputParameter <  secondInputParameter)
            {
                SetMemoryValue(oputputPosition,1);
            }
            else
            {
                SetMemoryValue(oputputPosition,0);
            }
            InstructionPointer += 4;
        }
 
        private  void Equals()
        {
            var firstInputParameter = GetParameterValue(1, InstructionPointer);
            var secondInputParameter = GetParameterValue(2, InstructionPointer);
            var oputputPosition = GetWriteParameterValue(3,InstructionPointer);;
            if (firstInputParameter ==  secondInputParameter)
            {
                SetMemoryValue(oputputPosition,1);
            }
            else
            {
                SetMemoryValue(oputputPosition,0);
            }
            InstructionPointer += 4;
        }
        
        private  void AdjustRelativeBase()
        {
            var parameter = GetParameterValue(1,InstructionPointer);
            RelativeBase += parameter;
            InstructionPointer += 2;
        }
   
        private static long AddInternal(long left, long right)
        {
            return left + right;
        }
 
        private static long MultiplyInternal(long left, long right)
        {
            return left * right;
        }

        private long GetInputParameter()
        {
            if(Input.Length == 0)
                throw new ArgumentOutOfRangeException("Input is empty");
            var tempValue = Input[0];
            if (Input.Length == 1)
            {
                Input = new long[0];
                return tempValue;
            }

            Input = Input.Skip(1).ToArray();
            return tempValue;
        }

        private long GetMemoryValue(long index)
        {
            if(index > Memory.Length -1)
                ExtendAndCopyArray(index);
            return Memory[index];
        }
        
        private void SetMemoryValue(long index, long value)
        {
            if(index > Memory.Length -1)
                ExtendAndCopyArray(index);
            Memory[index] = value;
        }

        private void ExtendAndCopyArray(long index)
        {
            var currentMemoryLength = Memory.Length;
            var newArray = Enumerable.Range(0, (int)index + 1).Select(x => 0L).ToArray();
            for (long i = 0; i < currentMemoryLength; i++)
            {
                newArray[i] = Memory[i];
            }

            Memory = newArray;
        }
    }
}