using System.Collections.Generic;

namespace Projeto2
{
    abstract class Arquivo
    {
        public abstract void EscreverNoArquivo(string texto);

        public abstract List<string> LerArquivo();

    }
}