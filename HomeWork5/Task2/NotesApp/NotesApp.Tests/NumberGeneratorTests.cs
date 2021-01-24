using System;
using Moq;
using NotesApp.Tools;
using Xunit;

namespace NotesApp.Tests
{
    public class NumberGeneratorTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(19)]
        public void GeneratePositiveLong_Should_Throw_ArgumentException_If_Invalid_Argument(int value)
        {
            Assert.Throws<ArgumentException>(() => NumberGenerator.GeneratePositiveLong(value));
        }

        [Theory]
        [InlineData(3)]
        [InlineData(7)]
        [InlineData(1)]
        [InlineData(18)]
        public void GeneratePositiveLong_Should_Return_Long_With_Count_Numbers_Equal_Argument(int value)
        {
            var result = NumberGenerator.GeneratePositiveLong(value);
            int count = 0;
            while (result != 0)
            {
                count++;
                result /= 10;
            }
            Assert.Equal(value, count);
        }
    }
}
