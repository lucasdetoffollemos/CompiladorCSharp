using Compilador.Dominio;



string fileName = @"C:\aquivosParaLer\file.txt";

IEnumerable<string> lines = File.ReadLines(fileName);

AnaliseLexica analisadorLexico = new AnaliseLexica();
List<GeradorItemsLexicos> listAnalisador = new();

foreach (var line in lines)
{
    List<char> linha = line.ToList();

    listAnalisador.AddRange(analisadorLexico.Analisador(linha));

}

//Console.WriteLine(String.Join(Environment.NewLine, lines));


for (int i = 0; i < listAnalisador.Count; i++)
{
    Console.WriteLine(listAnalisador[i]);
}


