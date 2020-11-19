using System.Collections.Generic;

namespace Projeto2
{
    class Produto
    {

        private int codigo;
        private string descricao;
        private double peso;
        private double valorUnitario;

        static ArquivoProduto aP = new ArquivoProduto();

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
            List<int> listaDeCodigos = ArquivoProduto.getCodigosDosProdutos();

            return listaDeCodigos.Exists(codigo => codigo == codigoProcurado);
        }

        public static bool armazenaCadastroDoProduto(Produto produto) {

            string linhaCompleta = "";

            linhaCompleta += produto.getCodigo() + ";";
            linhaCompleta += produto.getDescricao() + ";";
            linhaCompleta += produto.getPeso() + ";";
            linhaCompleta += produto.getValorUnitario();

            if (Produto.verificaSeCodigoProcuradoExiste(produto.getCodigo())) {
                return false;
            }

            aP.EscreverNoArquivo(linhaCompleta);

            return true;
        }

        public string mostrarProdutosCadastrados(){

            List<string> listaProdutos = aP.LerArquivo();
            string produto = "";

            foreach(string pd in listaProdutos){
                produto += pd;
            }

            return produto;
        }

    }
}