using System.IO;
using System.Text;
using System;
using System.Collections.Generic;

namespace Projeto2
{
    class LerArquivoProduto
    {

        public static List<int> getCodigosDosProdutos() {
            string[] linhas = System.IO.File.ReadAllLines("./_arquivos/cadastro_de_produtos.txt");

            List<int> codigos = new List<int>();

            foreach (string linha in linhas) {
                codigos.Add(Int32.Parse(linha.Split(';')[0]));
            }

            return codigos;
        }

    }
}