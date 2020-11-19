using System.IO;
using System;
using System.Collections.Generic;

namespace Projeto2
{
    class ArquivoProduto : Arquivo
    {
        public override void EscreverNoArquivo(string texto){
            StreamWriter sw = new StreamWriter("./_arquivos/cadastro_de_produtos.txt", true);

            sw.WriteLine(texto);
            
            sw.Close();
        }

        public override List<string> LerArquivo() {
            string[] linhas = File.ReadAllLines("./_arquivos/cadastro_de_produtos.txt");

            List<string> produtos = new List<string>();

            foreach (string linha in linhas) {
                produtos.Add(linha.Split('\n')[0]);
            }

            return produtos;
        }

        public static List<int> getCodigosDosProdutos() {
            string[] linhas = File.ReadAllLines("./_arquivos/cadastro_de_produtos.txt");

            List<int> codigos = new List<int>();

            foreach (string linha in linhas) {
                codigos.Add(Int32.Parse(linha.Split(';')[0]));
            }

            return codigos;
        }



    }
}