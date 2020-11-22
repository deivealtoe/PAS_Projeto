using System.Collections.Generic;

namespace Projeto2
{
    class Produto
    {
        private int codigo;
        private string descricao;
        private double peso;
        private double valorUnitario;

        static ArquivoProduto aPR = new ArquivoProduto();

        public Produto (int codigo, string descricao, double peso, double valorUnitario) {
            this.codigo = codigo;
            this.descricao = descricao;
            this.peso = peso;
            this.valorUnitario = valorUnitario;
        }

        public int getCodigo() {
            return this.codigo;
        }
        public string getDescricao() {
            return this.descricao;
        }
        public double getPeso() {
            return this.peso;
        }
        public double getValorUnitario() {
            return this.valorUnitario;
        }

        public void setCodigo(int codigo) {
            this.codigo = codigo;
        }
        public void setDescricao(string descricao) {
            this.descricao = descricao;
        }
        public void setPeso(double peso) {
            this.peso = peso;
        }
        public void setValorUnitario(double valorUnitario) {
            this.valorUnitario = valorUnitario;
        }


        public static bool verificaSeCodigoProcuradoExiste(int codigoProcurado) {
            List<int> listaDeCodigos = aPR.getCodigosDosProdutos();

            return listaDeCodigos.Exists(codigo => codigo == codigoProcurado);
        }

        public bool armazenaCadastroDoProduto() {

            string linhaCompleta = "";

            linhaCompleta += this.getCodigo() + ";";
            linhaCompleta += this.getDescricao() + ";";
            linhaCompleta += this.getPeso() + ";";
            linhaCompleta += this.getValorUnitario();

            if (Produto.verificaSeCodigoProcuradoExiste(this.getCodigo())) {
                return false;
            }

            aPR.EscreverNoArquivo(linhaCompleta);

            return true;
        }

        public string mostrarProdutosCadastrados(){

            List<string> listaProdutos = aPR.LerArquivo();
            string produto = "";
            string codigo = "";
            string descricao = "";
            string peso = "";
            string valorUnitario = "";

            foreach(string pd in listaProdutos){
                codigo = pd.Split(';')[0];
                descricao = pd.Split(';')[1];
                peso = pd.Split(';')[2];
                valorUnitario = pd.Split(';')[3];

                produto += "\n| Codigo: " + codigo + " - Descrição: " + descricao + " - Peso: " + peso + " - Valor Unitário: " + valorUnitario + " |";
            }

            return produto+"\n";
        }

    }
}