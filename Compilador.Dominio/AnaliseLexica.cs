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
            string codigo = "public static void main(string[] args){int x;}";
            codigo = AdicionaEspacoNoFinalCasoNecessario(codigo);
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

        private string AdicionaEspacoNoFinalCasoNecessario(string codigo)
        {

            if(codigo[codigo.Length-1] != ' ')
            {
                codigo += " ";
            }

            return codigo;
        }

        private void AdicionaNaTabelaDeId(string lexema, int contadorTabelaSimbolos, List<GeradorItemsLexicos> lexemaTokenSimbolo)
        {
            //Dictionary<int, string> tabelaDeSimbolos = new Dictionary<int, string>();

            //KeyValuePair<int, string> identificador = new KeyValuePair<int, string>();

            ////se o value for igual ao lexema retorna o item do dicionario e passa o value no lexema e a key no contador
            //if (tabelaDeSimbolos.ContainsValue(lexema))
            //{
                
            //}

            ////caso contrario adiciona o lexema no value e a key no contador
            //else
            //{
            //    tabelaDeSimbolos.Add(contadorTabelaSimbolos, lexema);                
            //}

            GeradorItemsLexicos novoItem = new GeradorItemsLexicos(lexema, $"ID, {contadorTabelaSimbolos}", "identificador");
            lexemaTokenSimbolo.Add(novoItem);




        }

        //private void AdicionarIdTabelaSimbolos(Dictionary<int, string> tabelaDeSimbolos)
        //{
        //    int contador
        //    string lexema

        //    for (int i = 0; i < tabelaDeSimbolos.Count; i++)
        //    {
        //        if (tabelaDeSimbolos.Values
        //    }
        //}

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
