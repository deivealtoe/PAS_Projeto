using System;
using System.IO;
using System.Collections.Generic;

namespace Projeto2
{
    class ArquivoPedido : Arquivo
    {
        public override void EscreverNoArquivo(string texto){
            StreamWriter sw = new StreamWriter("./_arquivos/pedidos.txt", true);

            sw.WriteLine(texto);
            
            sw.Close();
        }

        public override List<string> LerArquivo() {
            string[] linhas = File.ReadAllLines("./_arquivos/pedidos.txt");

            List<string> pedidos = new List<string>();

            foreach (string linha in linhas) {
                pedidos.Add(linha.Split('\n')[0]);
            }

            return pedidos;
        }

        public string LerALinhaEspecifica(int codigo){

            string[] linhas = File.ReadAllLines("./_arquivos/pedidos.txt");

            return linhas[codigo -1];
        }

        public void EscreverNaLinhaEspecifica(string texto, int codigo){

            StreamWriter sw = new StreamWriter("./_arquivos/pedidos.txt");

            sw.WriteLine(texto, codigo);
            
            sw.Close();   
        }

        public List<int> getCodigosDasPessoas() {
            string[] linhas = File.ReadAllLines("./_arquivos/pessoas.txt");

            List<int> codigos = new List<int>();

            foreach (string linha in linhas) {
                codigos.Add(Int32.Parse(linha.Split(';')[0]));
            }

            return codigos;
        }


    }
}