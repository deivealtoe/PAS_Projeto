namespace Projeto2
{
    class Produto
    {

        private int codigo;
        private string descricao;
        private double peso;
        private double valorUnitario;


        public Produto (int codigo, string descricao, double peso, double valorUnitario) {
            this.codigo = codigo;
            this.descricao = descricao;
            this.peso = peso;
            this.valorUnitario = valorUnitario;
        }


        public int getCodigo() {
            return this.codigo;
        }
        public string getDescricao() {
            return this.descricao;
        }
        public double getPeso() {
            return this.peso;
        }
        public double getValorUnitario() {
            return this.valorUnitario;
        }
        

        public void setCodigo(int codigo) {
            this.codigo = codigo;
        }
        public void setDescricao(string descricao) {
            this.descricao = descricao;
        }
        public void setPeso(double peso) {
            this.peso = peso;
        }
        public void setValorUnitario(double valorUnitario) {
            this.valorUnitario = valorUnitario;
        }

    }
}