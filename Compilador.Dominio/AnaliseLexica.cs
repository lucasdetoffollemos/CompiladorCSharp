using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.Dominio
{
    public class AnaliseLexica
    {

        public static Dictionary<int, string> tabelaDeSimbolos = new Dictionary<int, string>();

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
            string codigo = "public static void main(string[] args){int x, y, x, v,x, y}";
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
                    }

                    string simbolo = VerificarSimboloEspecial(codigo[i].ToString());

                    //adciona identificador
                    if ((lexema != "") && (lexema != simbolo))
                    {
                        AdicionaNaTabelaDeSimbolos(lexema, ref contadorTabelaSimbolos, lexemaTokenSimbolo);
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

        private void AdicionaNaTabelaDeSimbolos(string lexema, ref  int contadorTabelaSimbolos, List<GeradorItemsLexicos> lexemaTokenSimbolo)
        {
            int key = 0;
            string value = "";

            ////se o value for igual ao lexema retorna o item do dicionario e passa o value no lexema e a key no contador
            if (tabelaDeSimbolos.ContainsValue(lexema))
            {
                foreach (KeyValuePair<int, string> item in tabelaDeSimbolos)
                {
                    if (item.Value.Equals(lexema)){
                        value = item.Value;
                        key = item.Key;
                    }
                }

                GeradorItemsLexicos novoItem = new GeradorItemsLexicos(value, $"ID, {key}", "identificador");
                lexemaTokenSimbolo.Add(novoItem);
            }
            //caso contrario adiciona o lexema no value e a key no contador
            else
            {
                tabelaDeSimbolos.Add(contadorTabelaSimbolos, lexema);

                foreach (KeyValuePair<int, string> item in tabelaDeSimbolos)
                {
                    if (item.Value.Equals(lexema))
                    {
                        value = item.Value;
                        key = item.Key;
                    }
                }

                GeradorItemsLexicos novoItem = new GeradorItemsLexicos(value, $"ID, {key}", "identificador");
                lexemaTokenSimbolo.Add(novoItem);

                contadorTabelaSimbolos++;
            }
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
