using System.Collections.Generic;

namespace Projeto2
{
    abstract class Pedido
    {
        private int codigo;
        private bool confirmado;
        private CarrinhoDeCompra carrinhoDeCompra;

        static ArquivoPedido aPD = new ArquivoPedido();

        public Pedido(){
            
        }

        public Pedido(int codigo, bool confirmado, CarrinhoDeCompra carrinhoDeCompra){

            this.codigo = codigo;
            this.confirmado = confirmado;
            this.carrinhoDeCompra = carrinhoDeCompra;
        }

        public int getCodigo(){
            return this.codigo;
        }

        public bool getConfirmado(){
            return this.confirmado;
        }

        public CarrinhoDeCompra GetCarrinhoDeCompra(){
            return this.carrinhoDeCompra;
        }

        public void setCodigo(int codigo){
            this.codigo = codigo;
        }

        public void setConfirmado(bool confirmado){
            this.confirmado = confirmado;
        }

        public void setCarrinhoDeCompra(CarrinhoDeCompra carrinhoDeCompra){
            this.carrinhoDeCompra = carrinhoDeCompra;
        }

        public abstract bool ValidarPessoa(Pessoa pessoa);

        public abstract bool VerificarSeCodigoProcuradoExiste(int codigo);

        public abstract string CarrinhoDeCompra();

        public abstract bool ArmazenarPedido();

        public abstract void ConfirmarPedido();

        public abstract string MostrarPedidosCadastrados();

        public abstract Pedido PegarDadosDoPedido(int codigoProcurado);

        public static int NumeroDoPedido(){

            List<string> lista = aPD.LerArquivo();

            int numeroDoPedido = lista.Count + 1;

            return numeroDoPedido;
        }


    }
}