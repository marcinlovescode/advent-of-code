using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Intcode;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Tests
{
    public class IntcodeTests
    {
        [TestCaseSource(typeof(IntcodeTests), "TestCases")]
        public long[] Given_TestCase_When_Compute_Returns_Output(long[] input)
        {
            var instruction = 0;
            var intCode = new Intcode();
            intCode.LoadMemory(input);
            intCode.Run(new long[]{1});
            return input;
        }

        [TestCaseSource(typeof(IntcodeTests), "TestCases2")]
        public string Given_TestCase_When_Compute_Returns_CorrectlyComputedOutput(long[] input, long[] inputParametrer)
        {
            long optupt = -1;
            var intCode = new Intcode();
            intCode.LoadMemory(input);
            intCode.Run(inputParametrer, i => optupt = i);
            return optupt.ToString();
        }

        [TestCase(1, 1)]
        [TestCase(01, 1)]
        [TestCase(101, 1)]
        [TestCase(1111101, 1)]
        public void Given_OptCode_When_GetInstructionOptCode_Then_InstructionCodeIsReturned(int code,
            int expexctedResult)
        {
            //Arrange
            //Act
            var result = Intcode.GetInstructionOptCode(code);
            //Assert
            Assert.That(result, Is.EqualTo(expexctedResult));
        }

        [TestCase(1, 1, 0)]
        [TestCase(01, 1, 0)]
        [TestCase(101, 1, 1)]
        [TestCase(1111101, 1, 1)]
        [TestCase(1110101, 2, 0)]
        public void Given_OptCode_When_GetParameterMode_Then_InstructionCodeIsReturned(int code, int parameterNumber,
            int expexctedResult)
        {
            //Arrange
            //Act
            var result = Intcode.GetParameterMode(code, parameterNumber);
            //Assert
            Assert.That(result, Is.EqualTo(expexctedResult));
        }

        [Test]
        public void Day9_FirstTestCase()
        {
            var input = new long[] {109,1,204,-1,1001,100,1,100,1008,100,16,101,1006,101,0,99};
            var instruction = 0;
            var intCode = new Intcode();
            intCode.LoadMemory(input);
            var producedValue = true;
            var result = new List<long>();
            while (producedValue)
            {
                long? output = null;
                producedValue = false;
                intCode.Run(new long[]{}, l => output = l);
                if (output != null)
                {
                    result.Add( output.Value);
                    producedValue = true;
                }
            }
            Assert.That(result, Is.EquivalentTo(new []{109,1,204,-1,1001,100,1,100,1008,100,16,101,1006,101,0,99}));  
        }
        
        [Test]
        public void Day9_ThridTestCase()
        {
            var input = new long[] {104,1125899906842624,99};
            var instruction = 0;
            var intCode = new Intcode();
            intCode.LoadMemory(input);
            var producedValue = true;
            var result = new List<long>();
            while (producedValue)
            {
                long? output = null;
                producedValue = false;
                intCode.Run(new long[]{}, l => output = l);
                if (output != null)
                {
                    result.Add( output.Value);
                    producedValue = true;
                }
            }
          Assert.That(result, Is.EquivalentTo(new []{1125899906842624}));  
        }
        
        [Test]
        public void Day9_SecondTestCase()
        {
            var input = new long[] {1102,34915192,34915192,7,4,7,99,0};
            var instruction = 0;
            var intCode = new Intcode();
            intCode.LoadMemory(input);
            var producedValue = true;
            var result = new List<long>();
            while (producedValue)
            {
                long? output = null;
                producedValue = false;
                intCode.Run(new long[]{}, l => output = l);
                if (output != null)
                {
                    result.Add( output.Value);
                    producedValue = true;
                }
            }
            Assert.That(result.Single().ToString().Length, Is.EqualTo(16));  
        }
        
        public static IEnumerable TestCases
        {
            get
            {
                yield return new TestCaseData(new long[] {1, 0, 0, 0, 99}).Returns(new long[] {2, 0, 0, 0, 99});
                yield return new TestCaseData(new long[] {2, 3, 0, 3, 99}).Returns(new long[] {2, 3, 0, 6, 99});
                yield return new TestCaseData(new long[] {2, 4, 4, 5, 99, 0}).Returns(new long[] {2, 4, 4, 5, 99, 9801});
                yield return new TestCaseData(new long[] {1, 1, 1, 4, 99, 5, 6, 0, 99}).Returns(new long[]
                    {30, 1, 1, 4, 2, 5, 6, 0, 99});
                yield return new TestCaseData(new long[] {1, 1, 1, 4, 99, 5, 6, 0, 99}).Returns(new long[]
                    {30, 1, 1, 4, 2, 5, 6, 0, 99});
                yield return new TestCaseData(new long[] {1002, 4, 3, 4, 33}).Returns(new long[] {1002, 4, 3, 4, 99});
                yield return new TestCaseData(new long[] {1002, 4, 3, 4, 33}).Returns(new long[] {1002, 4, 3, 4, 99});
            }
        }

        public static IEnumerable TestCases2
        {
            get
            {
                yield return new TestCaseData(new long[] {3,9,8,9,10,9,4,9,99,-1,8}, new long[]{8,9})
                    .Returns("1");
                yield return new TestCaseData(new long[] {3,9,8,9,10,9,4,9,99,-1,8}, new long[]{9})
                    .Returns("0");
                yield return new TestCaseData(new long[] {3,9,7,9,10,9,4,9,99,-1,8}, new long[]{7})
                    .Returns("1");
                yield return new TestCaseData(new long[] {3,9,7,9,10,9,4,9,99,-1,8}, new long[]{8})
                    .Returns("0");
                yield return new TestCaseData(new long[] {3, 12, 6, 12, 15, 1, 13, 14, 13, 4, 13, 99, -1, 0, 1, 9}, new long[]{0})
                    .Returns("0");
                yield return new TestCaseData(new long[] {3, 12, 6, 12, 15, 1, 13, 14, 13, 4, 13, 99, -1, 0, 1, 9}, new long[]{5})
                    .Returns("1");
                yield return new TestCaseData(new long[]
                {
                    3, 21, 1008, 21, 8, 20, 1005, 20, 22, 107, 8, 21, 20, 1006, 20, 31,
                    1106, 0, 36, 98, 0, 0, 1002, 21, 125, 20, 4, 20, 1105, 1, 46, 104,
                    999, 1105, 1, 46, 1101, 1000, 1, 20, 4, 20, 1105, 1, 46, 98, 99
                }, new long[]{5}).Returns("999");
                yield return new TestCaseData(new long[]
                {
                    3, 21, 1008, 21, 8, 20, 1005, 20, 22, 107, 8, 21, 20, 1006, 20, 31,
                    1106, 0, 36, 98, 0, 0, 1002, 21, 125, 20, 4, 20, 1105, 1, 46, 104,
                    999, 1105, 1, 46, 1101, 1000, 1, 20, 4, 20, 1105, 1, 46, 98, 99
                }, new long[]{8}).Returns("1000");
                yield return new TestCaseData(new long[]
                {
                    3, 21, 1008, 21, 8, 20, 1005, 20, 22, 107, 8, 21, 20, 1006, 20, 31,
                    1106, 0, 36, 98, 0, 0, 1002, 21, 125, 20, 4, 20, 1105, 1, 46, 104,
                    999, 1105, 1, 46, 1101, 1000, 1, 20, 4, 20, 1105, 1, 46, 98, 99
                }, new long[]{9}).Returns("1001");
                
            }
        }
    }
}