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

        public override void ConfirmarPedido(){

            this.setConfirmado(true);

            string linhaCompleta = "";

            linhaCompleta += this.getCodigo() + ";";
            linhaCompleta += this.getConfirmado() + ";";
            linhaCompleta += this.getCliente().getCodigo() + ";";
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
            string codigocliente = "";
            string valor = "";
            

            foreach(string pd in listaPedidos){

                codigo = pd.Split(';')[0];
                confirmado = pd.Split(';')[1];
                codigocliente = pd.Split(';')[2];

                Pessoa cliente = Pessoa.PegarDadosDaPessoa(Int32.Parse(codigocliente));

                if(cliente.tipo == Pessoa.Tipo.Cliente && confirmado.Equals("False")){

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
                     " - Cliente: " + cliente.getNome() + " " + carrinho + " - Valor: R$" + valor + " |";
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

                int codigoCliente = Int32.Parse(linha.Split(';')[2]);

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

                Pessoa cliente = Pessoa.PegarDadosDaPessoa(codigoCliente);

                PedidoDeVenda pedido = new PedidoDeVenda(codigo,confirmado,carrinhoDeCompra,cliente);

                return pedido;
            }

            return new PedidoDeVenda();
        }

    }
}