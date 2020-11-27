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

        public List<ItemDeCompra> getItensDoCarrinho(){
            return this.itensDeCompra;
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
                    resumoCarrinho += "Código: " + item.getProduto().getCodigo() + " - Produto: " + item.getProduto().getDescricao() + " - Qtd. no carrinho: "
                    + item.getQtdCompra() + " - Vlr. Unitário: R$" + item.getProduto().getValorUnitario() + " - Vlr. Total: R$" + item.getValorTotal() + "\n";
                }
                
                resumoCarrinho += "O valor total do carrinho é de R$" + this.getValorTotalDoCarrinho();
                
                return "\n"+resumoCarrinho+"\n";
            }

            return "\nO carrinho está vazio!\n";
        }

        private bool ItemExiste(int codigoProduto) {
            foreach (ItemDeCompra item in this.itensDeCompra) {
                if (item.getProduto().getCodigo() == codigoProduto) {
                    return true;
                }
            }
            
            return false;
        }

        public void AdicionarItem(ItemDeCompra ic) {

            CarrinhoDeCompra novoCarrinho = new CarrinhoDeCompra(this.itensDeCompra);

            if (this.ItemExiste(ic.getProduto().getCodigo())) {

                Produto produto = ic.getProduto();
                int codigo = ic.getProduto().getCodigo();
                int qtd = ic.getQtdCompra();
                int calculo = 0;
                ItemDeCompra itemAModificar = new ItemDeCompra();

                foreach(ItemDeCompra item in this.itensDeCompra){
                    if(codigo == item.getProduto().getCodigo()){

                        calculo = qtd + item.getQtdCompra();

                        ic = new ItemDeCompra(produto,calculo);
                        itemAModificar = item;
                    }
                }

                this.itensDeCompra.Remove(itemAModificar);
            }

            novoCarrinho += ic; 
        }

        public bool RemoverItem(int codigoProduto) {
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


        public static CarrinhoDeCompra operator + (CarrinhoDeCompra cc1,ItemDeCompra ic){

            Produto produto = ic.getProduto();
            int qtd = ic.getQtdCompra();

            cc1.itensDeCompra.Add(new ItemDeCompra(produto,qtd));

            return cc1;
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
                    novoCarrinho.AdicionarItem(itemCarrinho2);
                }
            }
            
            return novoCarrinho;
        }

    }
}