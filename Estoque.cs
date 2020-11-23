using System;
using System.Collections.Generic;

namespace Projeto2
{
    class Estoque
    {

        static ArquivoEstoque aE = new ArquivoEstoque();

        public Estoque(){

        }

        public static bool VerificarSeCodigoProcuradoExiste(int codigoProcurado) {
            List<int> listaDeCodigos = aE.getCodigosDosProdutos();

            return listaDeCodigos.Exists(codigo => codigo == codigoProcurado);
        }

        public bool ArmazenarProdutoEstoque(int codigo, int qtd){

            string linhaCompleta = "";

            linhaCompleta += codigo + ";";
            linhaCompleta += qtd;

            if (Produto.VerificarSeCodigoProcuradoExiste(codigo)){

                if(Estoque.VerificarSeCodigoProcuradoExiste(codigo)){

                    aE.EscreverNaLinhaEspecifica(linhaCompleta, codigo);

                }else{
                    aE.EscreverNoArquivo(linhaCompleta);
                }
               
                return true;
            }
            
            return false;
        }

        public void AtualizarEstoqueVenda(PedidoDeVenda pedido){

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

                    calculo = qtdAtual - qtdProduto;

                    string linhaCompleta = "";

                    linhaCompleta += codigo + ";";
                    linhaCompleta += calculo;

                    ArmazenarProdutoEstoque(codigo, calculo);

                }
            }

        }

        public void AtualizarEstoqueCompra(PedidoDeCompra pedido){

            int codigo;
            int qtdAtual;
            int qtdProduto;
            int calculo;

            foreach (ItemDeCompra item in pedido.GetCarrinhoDeCompra().getItensDoCarrinho()) {
                codigo = item.getProduto().getCodigo();

                string linha = aE.LerALinhaEspecifica(codigo);

                qtdAtual = Int32.Parse(linha.Split(';')[1]);

                qtdProduto = item.getQtdCompra();

                calculo = qtdAtual + qtdProduto;

                string linhaCompleta = "";

                linhaCompleta += codigo + ";";
                linhaCompleta += calculo;

                ArmazenarProdutoEstoque(codigo, calculo);
            }

        }

    }

}