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


    }
}