using System.Collections.Generic;

namespace Projeto2
{
    class PedidoDeCompra : Pedido
    {
        private Pessoa fornecedor;

        static ArquivoPedido aPD = new ArquivoPedido();

        public PedidoDeCompra(int codigo, bool confirmado, CarrinhoDeCompra carrinhoDeCompra, Pessoa fornecedor):base(codigo,confirmado,carrinhoDeCompra){
            this.fornecedor = fornecedor;
        }


        public Pessoa getFornecedor(){
            return this.fornecedor;
        }

        public void setCliente(Pessoa fornecedor){
            this.fornecedor = fornecedor;
        }

        public override bool ValidarPessoa(Pessoa pessoa){

            Pessoa.Tipo tipo = pessoa.tipo;

            if(tipo == Pessoa.Tipo.Fornecedor){
                return true;
            }

            return false;
        }

        public override bool ArmazenarPedido() {

            string itens = "";

            foreach (ItemDeCompra item in this.GetCarrinhoDeCompra().getItensdoCarrinho()) {

                itens += item.getProduto().getCodigo() + ";" + 
                //item.getProduto().getDescricao() + ";" + 
                item.getQtdCompra() + ";" + 
                //item.getProduto().getValorUnitario() + ";" + 
                item.getValorTotal() + ";";
            }

            itens += this.GetCarrinhoDeCompra().getValorTotalDoCarrinho();

            string linhaCompleta = "";

            linhaCompleta += this.getCodigo() + ";";
            linhaCompleta += this.getConfirmado() + ";";
            linhaCompleta += this.getFornecedor().getCodigo() + ";";
            linhaCompleta += itens;

            if (ValidarPessoa(this.getFornecedor())) {

                aPD.EscreverNoArquivo(linhaCompleta);
                return true;
            }

            return false;
        }

        

    }
}