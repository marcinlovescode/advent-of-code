using System;
using NUnit.Framework;

namespace AdventOfCode.Amplifiers.Tests
{
    public class AmplifierTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Given_Instructions_When_LoadIntcodeInstructions_Then_InstructionIsLoaded()
        {
            //Arrange
            var instructions = new long[] {99};
            var amplifier = new Amplifier(0);
            //Act
            amplifier.LoadIntcodeInstructions(instructions);
            //Assert
            Assert.DoesNotThrow(() => amplifier.Amplify(0));
        }
        
        [Test]
        public void Given_AmplifierWithHaltProgram_When_LoadIntcodeInstructions_Then_InstructionIsLoaded()
        {
            //Arrange
            var instructions = new long[] {99};
            var amplifier = new Amplifier(0);
            amplifier.LoadIntcodeInstructions(instructions);
            //Act
            var result = amplifier.Amplify(0);
            //Assert
            Assert.That(result, Is.Null);
        }
        
        [Test]
        public void Given_NoInstructions_When_LoadIntcodeInstructions_Then_Throws()
        {
            //Arrange
            var amplifier = new Amplifier(0);
            //Act
            //Assert
            Assert.Throws<Exception>(() => amplifier.Amplify(0));
        }
    }
}