using System;
using System.Collections.Generic;

namespace Projeto2
{
    class PedidoDeVenda : Pedido
    {
        private Pessoa cliente;


        static ArquivoPedido aPD = new ArquivoPedido();

        public PedidoDeVenda(){

        }

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

        public bool VerificarSeCodigoProcuradoExiste(int codigoProcurado) {
            List<int> listaDeCodigos = aPD.getCodigosDasPessoas();

            return listaDeCodigos.Exists(codigo => codigo == codigoProcurado);
        }

        public override string CarrinhoDeCompra(){

            string itens = "";

            foreach (ItemDeCompra item in this.GetCarrinhoDeCompra().getItensDoCarrinho()) {

                itens += item.getProduto().getCodigo() + ";" +
                item.getQtdCompra() + ";";
            }

            itens += this.GetCarrinhoDeCompra().getValorTotalDoCarrinho();

            return itens;
        }

        public override bool ArmazenarPedido() {

            string itens = "";

            foreach (ItemDeCompra item in this.GetCarrinhoDeCompra().getItensDoCarrinho()) {

                itens += item.getProduto().getCodigo() + ";" +
                item.getQtdCompra() + ";";
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

        public override bool ConfirmarPedido(Pedido pedido){

            string linhaCompleta = "";

            linhaCompleta += pedido.getCodigo() + ";";
            linhaCompleta += pedido.getConfirmado() + ";";
            linhaCompleta += this.getCliente().getCodigo() + ";";
            linhaCompleta += CarrinhoDeCompra();
            
            aPD.EscreverNaLinhaEspecifica(linhaCompleta, pedido.getCodigo());

            return true;
        }

            public string MostrarPedidosCadastrados(){

            List<string> listaPedidos = aPD.LerArquivo();

            string pedidos = "";
            string codigo = "";
            string confirmado = "";
            string cliente = "";
            string valor = "";
            

            foreach(string pd in listaPedidos){

                string carrinho = "";

                List<string> tamanho = new List<string>();

                string[] partes = pd.Split(';');

                foreach (string parte in partes) {
                    tamanho.Add(parte);
                }

                codigo = pd.Split(';')[0];
                confirmado = pd.Split(';')[1];
                cliente = pd.Split(';')[2];

                for (int i = 3; i < tamanho.Count - 1; i++){ 
                    if(i % 2 != 0){
                        carrinho += "- Código do produto: " + pd.Split(';')[i] + " ";
                    }else{
                        carrinho += "- Quantidade: " + pd.Split(';')[i] + " ";
                    }
                }

                valor = pd.Split(';')[tamanho.Count - 1];
                
                pedidos += "\n| Código do pedido: " + codigo + " - Confirmado: " + confirmado + " - Código do fornecedor: " + cliente + " " + carrinho + " - Valor: R$" + valor + " |";
            }

            return pedidos+"\n";
        }

        public Pedido PegarDadosDoPedido(int codigoProcurado){

            if(VerificarSeCodigoProcuradoExiste(codigoProcurado)){

                string linha = aPD.LerALinhaEspecifica(codigoProcurado);

                List<string> tamanho = new List<string>();

                string[] partes = linha.Split(';');

                int codigo = Int32.Parse(linha.Split(';')[0]);

                string stringConfirmado = linha.Split(';')[1];

                bool confirmado = false;

                switch(stringConfirmado){
                    case "true": confirmado = true;
                    break;
                    case "false": confirmado = false;
                    break;
                }

                int codigoCliente = Int32.Parse(linha.Split(';')[2]);

                CarrinhoDeCompra carrinhoDeCompra = new CarrinhoDeCompra();

                Produto produto = new Produto();

                ItemDeCompra itemDeCompra = new ItemDeCompra();

                for (int i = 3; i < tamanho.Count - 1; i++){ 
                    if(i % 2 != 0){
                        produto = Produto.PegarDadosDoProduto(Int32.Parse(linha.Split(';')[i]));
                        itemDeCompra.setProduto(produto);
                    }else{
                        int qtd =  Int32.Parse(linha.Split(';')[i]);
                        itemDeCompra.setQtdCompra(qtd);
                        carrinhoDeCompra.AdicionarItem(itemDeCompra);
                    }
                }

                Pessoa fornecedor = Pessoa.PegarDadosDaPessoa(codigoCliente);

                PedidoDeCompra pedido = new PedidoDeCompra(codigo,confirmado,carrinhoDeCompra,fornecedor);

                return pedido;
            }

            return new PedidoDeCompra();
        }



    }
}