using System.Collections.Generic;

namespace Projeto2
{
    class Estoque
    {
        private Produto produto;
        private int qtdTotal;
        private int qtdReservada;

        static ArquivoEstoque aE = new ArquivoEstoque();

        public Estoque(Produto produto, int qtdTotal, int qtdReservada){
            this.produto = produto;
            this.qtdTotal = qtdTotal;
            this.qtdReservada = qtdReservada;
        }

        public Produto getProduto(){
            return this.produto;
        }

        public int getQtdTotal(){
            return this.qtdTotal;
        }

        public int getQtdReservada(){
            return this.qtdReservada;
        }

        public void setProduto(Produto produto){
            this.produto = produto;
        }

        public void setQtdTotal(int qtdTotal){
            this.qtdTotal = qtdTotal;
        }

        public void setQtdReservada(int qtdReservada){
            this.qtdReservada = qtdReservada;
        }

        public static bool verificaSeCodigoProcuradoExiste(int codigoProcurado) {
            List<int> listaDeCodigos = ArquivoEstoque.getCodigosDosProdutos();

            return listaDeCodigos.Exists(codigo => codigo == codigoProcurado);
        }

        public int calcularEstoque(){

           int calculo = getQtdTotal() - getQtdReservada();

           return calculo;
        }

        public bool armazenaProdutoEstoque(Produto produto){

            string linhaCompleta = "";

            linhaCompleta += produto.getCodigo() + ";";
            linhaCompleta += (calcularEstoque().ToString());

            if (Produto.verificaSeCodigoProcuradoExiste(produto.getCodigo())){

                if(Estoque.verificaSeCodigoProcuradoExiste(produto.getCodigo())){

                    aE.EscreverNaLinhaEspecifica(linhaCompleta, produto.getCodigo());

                }else{
                    aE.EscreverNoArquivo(linhaCompleta);
                }
               
                return true;
            }
            
            return false;
        }




    }

}