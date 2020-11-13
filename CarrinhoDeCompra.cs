using System.Collections.Generic;

namespace Projeto2
{
    class CarrinhoDeCompra
    {

        private List<ItemDeCompra> itensDeCompra = new List<ItemDeCompra>();


        public CarrinhoDeCompra() {
    
        }
        public CarrinhoDeCompra(List<ItemDeCompra> itensDeCompra) {
            this.itensDeCompra = itensDeCompra;
        }


        public double getValorTotalDoCarrinho() {
            double valorTotalDoCarrinho = 0;
            
            foreach (ItemDeCompra item in this.itensDeCompra) {
                valorTotalDoCarrinho += item.getValorTotal();
            }
            
            return valorTotalDoCarrinho;
        }


        public string getResumoCarrinho() {
            if (this.getValorTotalDoCarrinho() != 0) {
                string resumoCarrinho = "";
                
                foreach (ItemDeCompra item in this.itensDeCompra) {
                    resumoCarrinho += "Cód. " + item.getProduto().getCodigo() + ". Produto: " + item.getProduto().getDescricao() + ". Qtd. no carrinho: " + item.getQtdCompra() + ". Vlr. Unitário: R$" + item.getProduto().getValorUnitario() + ". Total: R$" + item.getValorTotal() + ".\n";
                }
                
                resumoCarrinho += "O valor total do carrinho é de R$" + this.getValorTotalDoCarrinho();
                
                return resumoCarrinho;
            }

            return "O carrinho está vazio!";
        }


        private bool itemExiste(int codigoProduto) {
            foreach (ItemDeCompra item in this.itensDeCompra) {
                if (item.getProduto().getCodigo() == codigoProduto) {
                    return true;
                }
            }
            
            return false;
        }


        public bool adicionarItem(ItemDeCompra item) {
            if (this.itemExiste(item.getProduto().getCodigo())) {
                return false;
            }
            
            this.itensDeCompra.Add(item);
            
            return true;
        }


        public bool removerItem(int codigoProduto) {
            int contador = 0;
            
            foreach (ItemDeCompra item in this.itensDeCompra) {
                if (item.getProduto().getCodigo() == codigoProduto) {
                    this.itensDeCompra.RemoveAt(contador);
                    
                    return true;
                }
            
                contador++;
            }
            
            return false;
        }


        public static CarrinhoDeCompra operator + (CarrinhoDeCompra cc1, CarrinhoDeCompra cc2) {
            CarrinhoDeCompra novoCarrinho = new CarrinhoDeCompra(cc1.itensDeCompra);
            
            foreach (ItemDeCompra itemCarrinho2 in cc2.itensDeCompra) {
                bool naoExiste = true;
                
                foreach (ItemDeCompra itemCarrinho1 in novoCarrinho.itensDeCompra) {
                    if (itemCarrinho2.getProduto().getCodigo() == itemCarrinho1.getProduto().getCodigo()) {
                        itemCarrinho1.setQtdCompra(itemCarrinho2.getQtdCompra() + itemCarrinho1.getQtdCompra());
                        
                        naoExiste = false;
                    }
                }
            
                if (naoExiste) {
                    novoCarrinho.adicionarItem(itemCarrinho2);
                }
            }
            
            return novoCarrinho;
        }



    }
}