using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Compilador.Dominio;

namespace Compilador.WebApp.Controllers
{
    public class LerArquivoController : Controller
    {
        private IWebHostEnvironment Environment;

        public LerArquivoController(IWebHostEnvironment _environment)
        {
            Environment = _environment;
        }

        public IActionResult LerCodigo()
        {
            AnaliseLexica analisadorLexico = new AnaliseLexica();

            StreamWriter sw;


            string caminhoListaTokens = Environment.WebRootPath + @"\~\Uploads\listaTokens.txt";

            string caminhoCodigo = Environment.WebRootPath + @"\~\Uploads\codigo.txt";

            if (!System.IO.File.Exists(caminhoCodigo))
            {
                ViewBag.Data = string.Empty;
                return View();
            }

            IEnumerable<string> lines = System.IO.File.ReadLines(caminhoCodigo);

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

            sw = System.IO.File.CreateText(caminhoListaTokens);

            for (int i = 0; i < listAnalisador.Count; i++)
            {
                sw.WriteLine(listAnalisador[i]);
            }

            sw.Close();

            string[] texts = System.IO.File.ReadAllLines(caminhoListaTokens);
            ViewBag.Data = texts;
            return View();

        }
    }
}
