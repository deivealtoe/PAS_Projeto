namespace Projeto2
{
    class ItemDeCompra
    {  
        private Produto produto;
        private int qtdCompra;

        public ItemDeCompra(){
            
        }

        public ItemDeCompra(Produto produto, int qtdCompra) {
            this.produto = produto;
            this.qtdCompra = qtdCompra;
        }
        
        
        public Produto getProduto() {
            return this.produto;
        }
        public int getQtdCompra() {
            return this.qtdCompra;
        }
        public double getValorTotal() {
            return this.produto.getValorUnitario() * this.qtdCompra;
        }
        
        
        public void setProduto(Produto produto) {
            this.produto = produto;
        }
        public void setQtdCompra(int qtdCompra) {
            this.qtdCompra = qtdCompra;
        }

    }
}