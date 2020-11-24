using System;
using System.Collections.Generic;

namespace Projeto2
{
    class Estoque
    {
        private Produto produto;
        private int qtd;

        static ArquivoEstoque aE = new ArquivoEstoque();

        public Estoque(){

        }

        public Estoque(Produto produto, int qtd){
            this.produto = produto;
            this.qtd = qtd;
        }

        public Produto getProduto(){
            return this.produto;
        }

        public int getQtd(){
            return this.qtd;
        }

        public void setProduto(Produto produto){
            this.produto = produto;
        }

        public void setQtdTotal(int qtd){
            this.qtd = qtd;
        }


        public static bool VerificarSeCodigoProcuradoExiste(int codigoProcurado) {
            List<int> listaDeCodigos = aE.getCodigosDosProdutos();

            return listaDeCodigos.Exists(codigo => codigo == codigoProcurado);
        }

        public bool InserirProdutoEstoque(int codigo, int qtd){

            string linhaCompleta = "";

            linhaCompleta += codigo + ";";
            linhaCompleta += qtd;

            if (Produto.VerificarSeCodigoProcuradoExiste(codigo)){

                if(Estoque.VerificarSeCodigoProcuradoExiste(codigo)){
                    return false;
                }else{
                    aE.EscreverNoArquivo(linhaCompleta);
                    return true;
                }
            }
            
            return false;
        }

        public bool ArmazenarProdutoEstoque(int codigo, int qtd){

            string linhaCompleta = "";

            linhaCompleta += codigo + ";";
            linhaCompleta += qtd;

            if (Produto.VerificarSeCodigoProcuradoExiste(codigo)){

                if(Estoque.VerificarSeCodigoProcuradoExiste(codigo)){
                    List<string> linhas = aE.LerArquivo();
                    aE.DeletarArquivo();
                    aE.CriarArquivo();
                    linhas[codigo-1] = linhaCompleta;
                    foreach(string linha in linhas){
                        aE.EscreverNoArquivo(linha);
                    }
                }else{
                    aE.EscreverNoArquivo(linhaCompleta);
                }
               
                return true;
            }
            
            return false;
        }

        public int AtualizarEstoqueVenda(Pedido pedido){

            int codigo;
            int qtdAtual;
            int qtdProduto;
            int calculo;
            int erros = 0;

            if(pedido.GetCarrinhoDeCompra().getItensDoCarrinho().Count > 0){

                foreach (ItemDeCompra item in pedido.GetCarrinhoDeCompra().getItensDoCarrinho()) {
                    codigo = item.getProduto().getCodigo();

                    if(Estoque.VerificarSeCodigoProcuradoExiste(codigo)){
                        
                        string linha = aE.LerALinhaEspecifica(codigo);

                        qtdAtual = Int32.Parse(linha.Split(';')[1]);

                        qtdProduto = item.getQtdCompra();

                        calculo = qtdAtual - qtdProduto;

                        if(calculo >= 0){

                            ArmazenarProdutoEstoque(codigo, calculo);
                        }else{
                            Console.WriteLine("\nA quantidade no estoque é inferior a quanidade desejada");
                            erros++;
                        }
                    }
                    else{

                        qtdAtual = 0;

                        qtdProduto = item.getQtdCompra();

                        calculo = qtdAtual - qtdProduto;

                        if(calculo >= 0){

                            ArmazenarProdutoEstoque(codigo, calculo);
                        }else{
                            Console.WriteLine("\nA quantidade no estoque é inferior a quanidade desejada");
                            erros++;
                        }
                    }
                } 
            }else{
                Console.WriteLine("\nO produto não está no estoque");
                erros++;
            }
            return erros;
        }

        public void AtualizarEstoqueCompra(Pedido pedido){

            int codigo;
            int qtdAtual;
            int qtdProduto;
            int calculo;

            foreach (ItemDeCompra item in pedido.GetCarrinhoDeCompra().getItensDoCarrinho()) {
                codigo = item.getProduto().getCodigo();

                if(Estoque.VerificarSeCodigoProcuradoExiste(codigo)){

                    string linha = aE.LerALinhaEspecifica(codigo);

                    qtdAtual = Int32.Parse(linha.Split(';')[1]);

                    qtdProduto = item.getQtdCompra();

                    calculo = qtdAtual + qtdProduto;

                    ArmazenarProdutoEstoque(codigo, calculo);

                }else{

                    qtdAtual = 0;

                    qtdProduto = item.getQtdCompra();

                    calculo = qtdAtual + qtdProduto;

                    ArmazenarProdutoEstoque(codigo, calculo);
                }
            }

        }

    }

}