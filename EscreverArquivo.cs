using System.IO;
using System.Text;

namespace Projeto2
{
    class EscreverArquivo
    {
        public void Escrever(string str){

            StreamWriter sw = new StreamWriter("arquivo.txt", true);

            sw.WriteLine(str);
            
            sw.Close();
        }

    }
}