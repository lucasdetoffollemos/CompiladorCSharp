using Compilador.Dominio;



string fileName = @"C:\arquivosParaLer\file4.txt";

IEnumerable<string> lines = File.ReadLines(fileName);

AnaliseLexica analisadorLexico = new AnaliseLexica();
List<GeradorItemsLexicos> listAnalisador = new();

foreach (var line in lines)
{
    if (string.IsNullOrEmpty(line))
    {
        continue;
    }

    List<char> linha = line.ToList();

    listAnalisador.AddRange(analisadorLexico.Analisador(linha));

}




for (int i = 0; i < listAnalisador.Count; i++)
{
    Console.WriteLine(listAnalisador[i]);
}


