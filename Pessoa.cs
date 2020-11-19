namespace Projeto2
{
    abstract class Pessoa
    {

        private int codigo;
        private string nome;
        private string sobrenome;
        private string razaoSocial;
        private string cpfCnpj;
        private enum Tipo{Cliente, Fornecedor, Colaborador};
        private Tipo tipo;
        private Endereco endereco;


        public Pessoa(int codigo, string nome, string sobrenome, string cpfCnpj, Endereco endereco) {
            this.codigo = codigo;
            this.nome = nome;
            this.sobrenome = sobrenome;
            this.cpfCnpj = cpfCnpj;
            this.endereco = endereco;
        }


        public Pessoa (int codigo, string razaoSocial, string cpfCnpj, Endereco endereco) {
            this.codigo = codigo;
            this.razaoSocial = razaoSocial;
            this.cpfCnpj = cpfCnpj;
            this.endereco = endereco;
        }



        public int getCodigo() {
            return this.codigo;
        }


        public string getNome() {
            return this.nome;
        }


        public string getSobrenome() {
            return this.sobrenome;
        }


        public string getRazaoSocial() {
            return this.razaoSocial;
        }


        public string getCpfCnpj() {
            return this.cpfCnpj;
        }


        public Endereco getEndereco() {
            return this.endereco;
        }



        public void setNome(string nome) {
            this.nome = nome;
        }


        public void setSobrenome(string sobrenome) {
            this.sobrenome = sobrenome;
        }


        public void setCpfCnpj(string cpfCnpj) {
            this.cpfCnpj = cpfCnpj;
        }


        public void setRazaoSocial(string razaoSocial) {
            this.razaoSocial = razaoSocial;
        }


        public void setEndereco(Endereco endereco) {
            this.endereco = endereco;
        }

    }
}