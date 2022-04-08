using Compilador.Dominio;

AnaliseLexica analisadorLexico = new AnaliseLexica();
GeradorItemsLexicos item = new();
List<GeradorItemsLexicos> listAnalisador = analisadorLexico.Analisador();


for (int i = 0; i < listAnalisador.Count; i++)
{
    Console.WriteLine(listAnalisador[i]);
}


