using Compilador.Dominio;
using FluentAssertions;
using Xunit;

namespace Compilador.Tests
{
    public class UnitTest1
    {

        [Fact]
        public void Test1()
        {
            int expectedValue = 3;

            Class1 a = new Class1();

            int b = a.somar(1,2);

            b.Should().Be(expectedValue);


        }
    }
}