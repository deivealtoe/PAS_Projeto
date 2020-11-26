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

        public override bool VerificarSeCodigoProcuradoExiste(int codigoProcurado) {
            List<int> listaDeCodigos = aPD.getCodigosDosPedidos();

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

        public override void ConfirmarPedido(){

            this.setConfirmado(true);

            string linhaCompleta = "";

            linhaCompleta += this.getCodigo() + ";";
            linhaCompleta += this.getConfirmado() + ";";
            linhaCompleta += this.getFornecedor().getCodigo() + ";";
            linhaCompleta += CarrinhoDeCompra();

            List<string> linhas = aPD.LerArquivo();
            aPD.DeletarArquivo();
            aPD.CriarArquivo();
            linhas[this.getCodigo()-1] = linhaCompleta;
            foreach(string linha in linhas){
                aPD.EscreverNoArquivo(linha);
            }
        }


        public override string MostrarPedidosCadastrados(){

            List<string> listaPedidos = aPD.LerArquivo();

            string pedidos = "";
            string codigo = "";
            string confirmado = "";
            string codigoFornecedor = "";
            string valor = "";
            

            foreach(string pd in listaPedidos){

                codigo = pd.Split(';')[0];
                confirmado = pd.Split(';')[1];
                codigoFornecedor = pd.Split(';')[2];
                
                Pessoa fornecedor = Pessoa.PegarDadosDaPessoa(Int32.Parse(codigoFornecedor));

                if(fornecedor.tipo == Pessoa.Tipo.Fornecedor && confirmado.Equals("False")){

                    string carrinho = "";

                    List<string> tamanho = new List<string>();

                    string[] partes = pd.Split(';');

                    foreach (string parte in partes) {
                        tamanho.Add(parte);
                    }

                    for (int i = 3; i < tamanho.Count - 1; i++){ 
                        if(i % 2 != 0){
                            carrinho += "- Código do produto: " + pd.Split(';')[i] + " ";
                        }else{
                            carrinho += "- Quantidade: " + pd.Split(';')[i] + " ";
                        }
                    }

                    valor = pd.Split(';')[tamanho.Count - 1];
                    
                    pedidos += "\n| Código: " + codigo + " - Confirmado: " + confirmado + 
                    " - Fornecedor: " + fornecedor.getNome() + " " + carrinho + " - Valor: R$" + valor + " |";
                }
            }
            if(pedidos.Length > 0){
                return pedidos+"\n";
            }
            return pedidos;
        }

        public override Pedido PegarDadosDoPedido(int codigoProcurado){

            if(VerificarSeCodigoProcuradoExiste(codigoProcurado)){

                string linha = aPD.LerALinhaEspecifica(codigoProcurado);

                List<string> tamanho = new List<string>();

                string[] partes = linha.Split(';');

                foreach (string parte in partes) {
                    tamanho.Add(parte);
                }

                int codigo = Int32.Parse(linha.Split(';')[0]);

                string stringConfirmado = linha.Split(';')[1];

                bool confirmado = false;

                switch(stringConfirmado){
                    case "True": confirmado = true;
                    break;
                    case "False": confirmado = false;
                    break;
                }

                int codigoFornecedor = Int32.Parse(linha.Split(';')[2]);

                CarrinhoDeCompra carrinhoDeCompra = new CarrinhoDeCompra();

                Produto produto = new Produto();

                ItemDeCompra itemDeCompra = new ItemDeCompra();

                for (int i = 3; i < tamanho.Count - 1; i++){
                    switch(i % 2){
                        case 0:
                        int qtd =  Int32.Parse(linha.Split(';')[i]);
                        itemDeCompra = new ItemDeCompra(produto,qtd);
                        carrinhoDeCompra.AdicionarItem(itemDeCompra);
                        break;
                        default:
                        produto = Produto.PegarDadosDoProduto(Int32.Parse(linha.Split(';')[i]));
                        break;
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