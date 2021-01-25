using System;
using NotesApp.Tools;
using Xunit;

namespace NotesApp.Tests.Tools
{
    public class ShortGuidTests
    {
        [Fact]
        public void ToShortId_FromShortId_Should_Converts_Correctly()
        {
            var firstGuid = Guid.NewGuid();
            var secondGuid = ShortGuid.FromShortId(ShortGuid.ToShortId(firstGuid));
            Assert.Equal(firstGuid, secondGuid);
        }

        [Fact]
        public void FromShortId_Should_Converts_Correctly_With_Double_Equal_Characters_In_The_End()
        {
            ShortGuid.FromShortId(ShortGuid.ToShortId(Guid.NewGuid())+"==");
        }

        [Fact]
        public void FromShortId_Should_Converts_Correctly_String_Guid_To_Guid()
        {
            var firstGuid = Guid.NewGuid();
            var secondGuid = ShortGuid.FromShortId(firstGuid.ToString());
            Assert.Equal(firstGuid, secondGuid);
        }

        [Fact]
        public void FromShortId_Should_Throw_FormatException_If_Invalid_Argument()
        {
            Assert.Throws<FormatException>(() => ShortGuid.FromShortId("hello world"));
        }

        [Fact]
        public void FromShortId_Should_Return_Null_If_Argument_Null()
        {
            Assert.Null(ShortGuid.FromShortId(null));
        }
    }
}
