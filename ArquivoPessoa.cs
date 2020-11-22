using System.IO;
using System.Collections.Generic;

namespace Projeto2
{
    class ArquivoPessoa : Arquivo
    {
        public override void EscreverNoArquivo(string texto){
            StreamWriter sw = new StreamWriter("./_arquivos/pessoas.txt", true);

            sw.WriteLine(texto);
            
            sw.Close();
        }

        public override List<string> LerArquivo() {
            string[] linhas = File.ReadAllLines("./_arquivos/pessoas.txt");

            List<string> pessoas = new List<string>();

            foreach (string linha in linhas) {
                pessoas.Add(linha.Split('\n')[0]);
            }

            return pessoas;
        }

        public List<string> getCpfCnpjDasPessoas() {
            string[] linhas = File.ReadAllLines("./_arquivos/pessoas.txt");

            List<string> cpfCnpj = new List<string>();

            foreach (string linha in linhas) {
                cpfCnpj.Add(linha.Split(';')[3]);
            }

            return cpfCnpj;
        }

    }
}