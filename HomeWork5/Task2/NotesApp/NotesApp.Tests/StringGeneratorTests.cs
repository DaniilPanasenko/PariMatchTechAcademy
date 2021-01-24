using System;
using Moq;
using NotesApp.Tools;
using Xunit;

namespace NotesApp.Tests
{
    public class StringGeneratorTests
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(-10)]
        public void GenerateNumbersString_Should_Throw_ArgumentException_If_Invalid_Argument(int value)
        {
            Assert.Throws<ArgumentException>(() => StringGenerator.GenerateNumbersString(value,false));
        }

        [Fact]
        public void GenerateNumbersString_Should_Return_Empty_String_If_Argument_Equal_Zero()
        {
            Assert.Equal("", StringGenerator.GenerateNumbersString(0,false));
        }

        [Fact]
        public void GenerateNumbersString_Should_Return_String_Without_Leading_Zero_With_Special_Flag()
        {
            Assert.NotEqual('0', StringGenerator.GenerateNumbersString(10, false)[0]);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(7)]
        [InlineData(1)]
        [InlineData(18)]
        public void GenerateNumbersString_Should_Return_String_With_Count_Chars_Equal_Argument(int value)
        {
            Assert.Equal(value, StringGenerator.GenerateNumbersString(value, false).Length);
        }

        [Fact]
        public void GenerateNumbersString_Should_Return_String_Which_Convert_To_Number_Types()
        {
            decimal number;
            bool tryParse = decimal.TryParse(StringGenerator.GenerateNumbersString(10, false), out number);
            Assert.True(tryParse);
        }
    }
}