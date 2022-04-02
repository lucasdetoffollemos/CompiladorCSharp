using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.Dominio
{
    public class Program
    {
        //static void Main(string[] args)
        //{
        //    // função para adicionar um espaço no fim do código para analisar até o ultimo elemento
        //    string codigo = "main() x y aaa ";

        //    string lexema = "";

        //    List<Item> lexemaTokenSimbolo = new List<Item>();

        //    int contadorTabelaSimbolos = 0;

        //    for (int i = 0; i < codigo.Length; i++)
        //    {
        //        if (lexema == "void")
        //        {
        //            Item novoItem = new Item(lexema, "VOID", "palavra reservada");
        //            lexemaTokenSimbolo.Add(novoItem);
        //            lexema = "";
        //        }
        //        else if (lexema == "main")
        //        {
        //            Item novoItem = new Item(lexema, "MAIN", "palavra reservada");
        //            lexemaTokenSimbolo.Add(novoItem);
        //            lexema = "";
        //        }
        //        else if (lexema == "int")
        //        {
        //            Item novoItem = new Item(lexema, "INT", "palavra reservada");
        //            lexemaTokenSimbolo.Add(novoItem);
        //            lexema = "";
        //        }
        //        else if (lexema == "(")
        //        {
        //            Item novoItem = new Item(lexema, "(", "símbolo especial");
        //            lexemaTokenSimbolo.Add(novoItem);
        //            lexema = "";
        //        }
        //        else if (lexema == ")")
        //        {
        //            Item novoItem = new Item(lexema, ")", "símbolo especial");
        //            lexemaTokenSimbolo.Add(novoItem);
        //            lexema = "";
        //        }
        //        else if (lexema == "{")
        //        {
        //            Item novoItem = new Item(lexema, "{", "símbolo especial");
        //            lexemaTokenSimbolo.Add(novoItem);
        //            lexema = "";
        //        }
        //        else if (lexema == "}")
        //        {
        //            Item novoItem = new Item(lexema, "}", "símbolo especial");
        //            lexemaTokenSimbolo.Add(novoItem);
        //            lexema = "";
        //        }
        //        else
        //        {
        //            if (i < codigo.Length - 1)
        //            {
        //                lexema += codigo[i];
        //                if (codigo[i + 1] == ' ')
        //                {
        //                    // criar um fluxo melhor para a criação de variaveis e inserção na tabela de simbolos
        //                    if (lexema.Contains(" "))
        //                        lexema = lexema.Replace(" ", "");

        //                    Item novoItem = new Item(lexema, $"ID, {contadorTabelaSimbolos}", "identificador");
        //                    lexemaTokenSimbolo.Add(novoItem);
        //                    contadorTabelaSimbolos++;
        //                    lexema = "";
        //                }
        //                else if (codigo[i] != ' ')
        //                {
        //                    continue;
        //                }
        //                continue;
        //            }

        //        }
        //        if (codigo[i] == ' ')
        //        {
        //            continue;
        //        }
        //        else
        //            lexema += codigo[i];
        //    }

        //    for (int i = 0; i < lexemaTokenSimbolo.Count; i++)
        //    {
        //        Console.WriteLine($"Lexema: {lexemaTokenSimbolo[i].lexema}\tToken: {lexemaTokenSimbolo[i].token}\tSímbolo: {lexemaTokenSimbolo[i].simbolo}");
        //    }
        //}

        public List<Item> AnaliseLexica(string codigo)
        {
            string lexema = "";

            List<Item> lexemaTokenSimbolo = new List<Item>();

            int contadorTabelaSimbolos = 0;

            for (int i = 0; i < codigo.Length; i++)
            {
                if (lexema == "void")
                {
                    Item novoItem = new Item(lexema, "VOID", "palavra reservada");
                    lexemaTokenSimbolo.Add(novoItem);
                    lexema = "";
                }
                else if (lexema == "main")
                {
                    Item novoItem = new Item(lexema, "MAIN", "palavra reservada");
                    lexemaTokenSimbolo.Add(novoItem);
                    lexema = "";
                }
                else if (lexema == "int")
                {
                    Item novoItem = new Item(lexema, "INT", "palavra reservada");
                    lexemaTokenSimbolo.Add(novoItem);
                    lexema = "";
                }
                else if (lexema == "(")
                {
                    Item novoItem = new Item(lexema, "(", "símbolo especial");
                    lexemaTokenSimbolo.Add(novoItem);
                    lexema = "";
                }
                else if (lexema == ")")
                {
                    Item novoItem = new Item(lexema, ")", "símbolo especial");
                    lexemaTokenSimbolo.Add(novoItem);
                    lexema = "";
                }
                else if (lexema == "{")
                {
                    Item novoItem = new Item(lexema, "{", "símbolo especial");
                    lexemaTokenSimbolo.Add(novoItem);
                    lexema = "";
                }
                else if (lexema == "}")
                {
                    Item novoItem = new Item(lexema, "}", "símbolo especial");
                    lexemaTokenSimbolo.Add(novoItem);
                    lexema = "";
                }
                else
                {
                    if (i < codigo.Length - 1)
                    {
                        lexema += codigo[i];
                        if (codigo[i + 1] == ' ')
                        {
                            // criar um fluxo melhor para a criação de variaveis e inserção na tabela de simbolos
                            if (lexema.Contains(" "))
                                lexema = lexema.Replace(" ", "");

                            Item novoItem = new Item(lexema, $"ID, {contadorTabelaSimbolos}", "identificador");
                            lexemaTokenSimbolo.Add(novoItem);
                            contadorTabelaSimbolos++;
                            lexema = "";
                        }
                        else if (codigo[i] != ' ')
                        {
                            continue;
                        }
                        continue;
                    }

                }
                if (codigo[i] == ' ')
                {
                    continue;
                }
                else
                    lexema += codigo[i];
            }


            return lexemaTokenSimbolo;
        }
    }
}
