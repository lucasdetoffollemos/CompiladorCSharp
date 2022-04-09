using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.Dominio
{
    public class AnaliseLexica
    {
        public static Dictionary<string, string> palavrasReservadas = new Dictionary<string, string>()
                {
                    { "void", "VOID" },
                    { "main", "MAIN" },
                    { "int", "INT" },
                    { "float", "FLOAT" },
                    { "char", "CHAR" },
                    { "boolean", "BOOLEAN" },
                    { "if", "IF" },
                    { "else", "ELSE" },
                    { "for", "FOR" },
                    { "while", "WHILE" },
                    { "scanf", "SCANF" },
                    { "println", "PRINTLN" },
                    { "return", "RETURN" }
                };

        public static List<string> simbolosEspeciais = new List<string>() { "(", ")", "{", "}", "[", "]", ",", ";" };


        public List<GeradorItemsLexicos> Analisador()
        {
            string codigo = "({(int({[}]);;";

            string lexema = "";

            List<GeradorItemsLexicos> lexemaTokenSimbolo = new List<GeradorItemsLexicos>();

            int contadorTabelaSimbolos = 1;

            for (int i = 0; i < codigo.Length; i++)
            {
                
                if (Char.IsLetter(codigo[i]))
                {
                    lexema += codigo[i];
                }
                else
                {


                    //adicionar no token
                    string token = VerificarPalavraReservada(lexema);
                    if (token != null)
                    {
                        GeradorItemsLexicos novoItem = new GeradorItemsLexicos(lexema, token, "palavra reservada");
                        lexemaTokenSimbolo.Add(novoItem);
                        lexema = "";
                        //continue;
                    }


                    string simbolo = VerificarSimboloEspecial(codigo[i].ToString());

                    
                    //adciona identificador
                    if ((lexema != "") && (lexema != simbolo))
                    {
                        AdicionaNaTabelaDeId(lexema, contadorTabelaSimbolos, lexemaTokenSimbolo);
                        contadorTabelaSimbolos++;
                    }



                    //adicionar no simbolo especial
                    if (simbolo != null)
                    {
                        GeradorItemsLexicos novoItem = new GeradorItemsLexicos(simbolo, simbolo, "símbolo especial");
                        lexemaTokenSimbolo.Add(novoItem);

                    }


                    
                    

                    lexema = "";
                }
            }

            return lexemaTokenSimbolo;

           
        }

        private void AdicionaNaTabelaDeId(string lexema, int contadorTabelaSimbolos, List<GeradorItemsLexicos> lexemaTokenSimbolo)
        {
            GeradorItemsLexicos novoItem = new GeradorItemsLexicos(lexema, $"ID, {contadorTabelaSimbolos}", "identificador");
            lexemaTokenSimbolo.Add(novoItem);
        }

        private static string VerificarSimboloEspecial(string lexema)
        {
            foreach (var simbolo in simbolosEspeciais)
            {
                if (simbolo.Equals(lexema))
                {
                    return simbolo;
                }
            }

            return null;
        }

        private static string VerificarPalavraReservada(string lexema)
        {
            for (int i = 0; i < palavrasReservadas.Count; i++)
            {
                if (palavrasReservadas.ContainsKey(lexema))
                {
                    return palavrasReservadas[lexema];
                }
            }

            return null;

        }
    }
}
