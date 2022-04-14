﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Compilador.Dominio
{
    public class AnaliseLexica
    {
        
        public static Regex regexInt = new Regex("^[0-9]+$");
        public static Regex regexIds = new Regex("^[a-zA-Z_]([a-zA-Z_]|[0-9])*$");
        public static Regex regexDouble = new Regex(@"^[0-9]+\.[0-9]+$");

        public static char operadorAtribuicao = '=';

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

        public static List<string> operadoresAritmeticos = new List<string>() { "+", "-", "*", "/", "%"};

        public static List<string> operadoresLogicos = new List<string>() {"&&", "||", "!"};


        public List<GeradorItemsLexicos> Analisador()
        {
            string codigo = "a = a;";

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
                
                else if (Char.IsDigit(codigo[i]))
                {
                    lexema+=codigo[i];
                }
                else if (codigo[i] == '.')
                {
                    lexema += codigo[i];
                }
                else if (VerificarExistenciaOperadorLogico(codigo[i].ToString()))
                {
                    lexema += codigo[i];

                    //adiciona && etc...
                    string operadorLogico = VerificarOperadorLogico(lexema);
                    if (lexema.Equals(operadorLogico))
                    {
                        GeradorItemsLexicos novoItem = new GeradorItemsLexicos(lexema, lexema, "Operador Lógico");
                        lexemaTokenSimbolo.Add(novoItem);
                        lexema = "";

                    }
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
                        if (regexIds.IsMatch(lexema))
                        {
                            AdicionaNaTabelaDeSimbolos(lexema, ref contadorTabelaSimbolos, lexemaTokenSimbolo);
                        }
                    }


                    //adiciona numero double 
                    if (regexDouble.IsMatch(lexema))
                    {
                        GeradorItemsLexicos novoItem = new GeradorItemsLexicos(lexema, "NUM_DEC, " + lexema, "Numero decimal");
                        lexemaTokenSimbolo.Add(novoItem);
                    }

                    //adiciona numero inteiro 
                    if (regexInt.IsMatch(lexema))
                    {
                        GeradorItemsLexicos novoItem = new GeradorItemsLexicos(lexema, "NUM_INT, " + lexema, "Numero inteiro");
                        lexemaTokenSimbolo.Add(novoItem);
                    }
                        
                    

                    //adicionar no simbolo especial
                    if (simbolo != null)
                    {
                        GeradorItemsLexicos novoItem = new GeradorItemsLexicos(simbolo, simbolo, "símbolo especial");
                        lexemaTokenSimbolo.Add(novoItem);
                    }


                    

                    //adiciona =
                    if (codigo[i] == operadorAtribuicao)
                    {
                        GeradorItemsLexicos novoItem = new GeradorItemsLexicos(operadorAtribuicao.ToString(), operadorAtribuicao.ToString(), "Operador de Atribuição");
                        lexemaTokenSimbolo.Add(novoItem);
                    }

                    string operadorAritimetico = VerificarOperadorAritimetico(codigo[i].ToString());

                    //adiciona + - etc...
                    if (codigo[i].ToString() == operadorAritimetico)
                    {
                        GeradorItemsLexicos novoItem = new GeradorItemsLexicos(codigo[i].ToString(), codigo[i].ToString(), "Operador Aritmético");
                        lexemaTokenSimbolo.Add(novoItem);
                    }

                    lexema = "";
                }
            }

            return lexemaTokenSimbolo;
        }

        private bool VerificarExistenciaOperadorLogico(string operador)
        {
            foreach(var op in operadoresLogicos)
            {
                if (op.Contains(operador))
                {
                    return true;
                }

            }

            return false;
        }

        private string VerificarOperadorLogico(string operador)
        {
            //&&x

            foreach (var op in operadoresLogicos)
            {
                if (op.Equals(operador))
                {
                    return operador;
                }
            }

            return null;
        }

        private string VerificarOperadorAritimetico(string opereador)
        {
            foreach (var op in operadoresAritmeticos)
            {
                if (op.Equals(opereador))
                {
                    return opereador;
                }
            }

            return null;
        }

        private string AdicionaEspacoNoFinalCasoNecessario(string codigo)
        {
            if(codigo[codigo.Length-1] != ' ')
            {
                codigo += " ";
            }

            return codigo;
        }

        private void AdicionaNaTabelaDeSimbolos(string lexema, ref int contadorTabelaSimbolos, List<GeradorItemsLexicos> lexemaTokenSimbolo)
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
