using System;
using System.Collections.Generic;

namespace Projeto2
{
    class PedidoDeCompra : Pedido
    {
        private Pessoa fornecedor;

        static ArquivoPedido aPD = new ArquivoPedido();

        public PedidoDeCompra(){

        }

        public PedidoDeCompra(int codigo, bool confirmado, CarrinhoDeCompra carrinhoDeCompra, Pessoa fornecedor):base(codigo,confirmado,carrinhoDeCompra){
            this.fornecedor = fornecedor;
        }


        public Pessoa getFornecedor(){
            return this.fornecedor;
        }

        public void setFornecedor(Pessoa fornecedor){
            this.fornecedor = fornecedor;
        }

        public override bool ValidarPessoa(Pessoa pessoa){

            Pessoa.Tipo tipo = pessoa.tipo;

            if(tipo == Pessoa.Tipo.Fornecedor){
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

            string linhaCompleta = "";

            linhaCompleta += this.getCodigo() + ";";
            linhaCompleta += this.getConfirmado() + ";";
            linhaCompleta += this.getFornecedor().getCodigo() + ";";
            linhaCompleta += CarrinhoDeCompra();

            if (ValidarPessoa(this.getFornecedor())) {

                aPD.EscreverNoArquivo(linhaCompleta);
                return true;
            }

            return false;
        }

        public override bool ConfirmarPedido(Pedido pedido){

            pedido.setConfirmado(true);

            string linhaCompleta = "";

            linhaCompleta += pedido.getCodigo() + ";";
            linhaCompleta += pedido.getConfirmado() + ";";
            linhaCompleta += this.getFornecedor().getCodigo() + ";";
            linhaCompleta += CarrinhoDeCompra();

            aPD.EscreverNaLinhaEspecifica(linhaCompleta, pedido.getCodigo());

            return true;
        }


        public string MostrarPedidosCadastrados(){

            List<string> listaPedidos = aPD.LerArquivo();

            string pedidos = "";
            string codigo = "";
            string confirmado = "";
            string fornecedor = "";
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
                fornecedor = pd.Split(';')[2];

                for (int i = 3; i < tamanho.Count - 1; i++){ 
                    if(i % 2 != 0){
                        carrinho += "- Código do produto: " + pd.Split(';')[i] + " ";
                    }else{
                        carrinho += "- Quantidade: " + pd.Split(';')[i] + " ";
                    }
                }

                valor = pd.Split(';')[tamanho.Count - 1];
                
                pedidos += "\n| Código do pedido: " + codigo + " - Confirmado: " + confirmado + 
                " - Código do fornecedor: " + fornecedor + " " + carrinho + " - Valor: R$" + valor + " |";
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

                int codigoFornecedor = Int32.Parse(linha.Split(';')[2]);

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

                Pessoa fornecedor = Pessoa.PegarDadosDaPessoa(codigoFornecedor);

                PedidoDeCompra pedido = new PedidoDeCompra(codigo,confirmado,carrinhoDeCompra,fornecedor);

                return pedido;
            }

            return new PedidoDeCompra();
        }


    }
}