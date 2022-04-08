using Compilador.Dominio;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Compilador.Tests
{
    public class AnalisadorLexico1
    {

       // [Fact]
        //public void MAIN_ShouldBeFirst_Tokek()
        //{
        //    //arrange
        //    Program p = new Program();

        //    string codigo = "main() x y aaa ";


        //    //act
        //    var results = p.AnaliseLexica(codigo);

        //    //assert

        //    string expectedToken = "MAIN";

        //    results[0].token.Should().Be(expectedToken);

        //}

        //[Fact]
        //public void Simbolo_ShouldBe_ExpectedSimbolo()
        //{
        //    //arrange
        //    Program p = new Program();

        //    string codigo = "main() x y aaa ";


        //    //act
        //    var results = p.AnaliseLexica(codigo);

        //    //assert
        //    string expectedSimbolo = "identificador";

        //    results[3].simbolo.Should().Be(expectedSimbolo);
            

        //}

        //[Fact]
        //public void Simbolo_ShouldBe_ExpectedToken()
        //{

        //    //TESTE AINDA NÃO ESTA PASSANDO POIS O ID ESTA COMEÇAÑDO DO 0 E NÃO DO 1
        //    //VERIFICAR SE ISSO ESTÁ CORRETO NO CÓDIGO.

        //    //arrange
        //    Program p = new Program();

        //    string codigo = "main() x y aaa ";


        //    //act
        //    var results = p.AnaliseLexica(codigo);

        //    //assert
        //    string token = "ID 2";


            
        //    results[4].token.Should().Be(token);

        //}
    }
}
