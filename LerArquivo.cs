using System.IO;
using System.Text;

namespace Projeto2
{
    class LerArquivo
    {
        public string Ler(){

            FileStream meuArq = new FileStream("arquivo.txt", FileMode.Open, FileAccess.Read);

            StreamReader sr = new StreamReader(meuArq, Encoding.UTF8);

            string str = sr.ReadToEnd();
            
            sr.Close();
            meuArq.Close();

            return str;
        }

    }
}