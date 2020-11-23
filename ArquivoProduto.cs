using System;
using System.IO;
using System.Collections.Generic;

namespace Projeto2
{
    class ArquivoProduto : Arquivo
    {
        public override void EscreverNoArquivo(string texto){
            StreamWriter sw = new StreamWriter("./_arquivos/produtos.txt", true);

            sw.WriteLine(texto);
            
            sw.Close();
        }

        public override List<string> LerArquivo() {
            string[] linhas = File.ReadAllLines("./_arquivos/produtos.txt");

            List<string> produtos = new List<string>();

            foreach (string linha in linhas) {
                produtos.Add(linha.Split('\n')[0]);
            }

            return produtos;
        }

        public string LerALinhaEspecifica(int codigo){

            string[] linhas = File.ReadAllLines("./_arquivos/produtos.txt");

            return linhas[codigo -1];
        }

        public List<int> getCodigosDosProdutos() {
            string[] linhas = File.ReadAllLines("./_arquivos/produtos.txt");

            List<int> codigos = new List<int>();

            foreach (string linha in linhas) {
                codigos.Add(Int32.Parse(linha.Split(';')[0]));
            }

            return codigos;
        }



    }
}