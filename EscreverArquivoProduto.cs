using System.IO;
using System.Text;

namespace Projeto2
{
    class EscreverArquivoProduto
    {
        public static void escreverNoArquivoDeCadastro(string texto){
            StreamWriter sw = new StreamWriter("./_arquivos/cadastro_de_produtos.txt", true);

            sw.WriteLine(texto);
            
            sw.Close();
        }

    }
}