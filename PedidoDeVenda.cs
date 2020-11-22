using System.Collections.Generic;

namespace Projeto2
{
    class PedidoDeVenda : Pedido
    {
        private Pessoa cliente;


        static ArquivoPedido aPD = new ArquivoPedido();


        public PedidoDeVenda(int codigo, bool confirmado, CarrinhoDeCompra carrinhoDeCompra, Pessoa cliente):base(codigo,confirmado,carrinhoDeCompra){
            this.cliente = cliente;
        }

        public Pessoa getCliente(){
            return this.cliente;
        }

        public void setCliente(Pessoa cliente){
            this.cliente = cliente;
        }


        public override bool ValidarPessoa(Pessoa pessoa){

            Pessoa.Tipo tipo = pessoa.tipo;

            if(tipo == Pessoa.Tipo.Cliente){
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
            linhaCompleta += this.getCliente().getCodigo() + ";";
            linhaCompleta += itens;

            if (ValidarPessoa(this.getCliente())) {

                aPD.EscreverNoArquivo(linhaCompleta);
                return true;
            }

            return false;
        }


    }
}