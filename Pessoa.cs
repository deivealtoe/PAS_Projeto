using System.Collections.Generic;

namespace Projeto2
{
    class Pessoa
    {
        private int codigo;
        private string nome;
        private string sobrenome;

        //private string razaoSocial;
        private string cpfCnpj;
        public enum Tipo{Cliente, Fornecedor, Colaborador};
        public Tipo tipo;
        private Endereco endereco;

        static ArquivoPessoa aPE = new ArquivoPessoa();

        public Pessoa(int codigo, string nome, string sobrenome, string cpfCnpj, Tipo tipo, Endereco endereco) {
            this.codigo = codigo;
            this.nome = nome;
            this.sobrenome = sobrenome;
            this.cpfCnpj = cpfCnpj;
            this.tipo = tipo;
            this.endereco = endereco;
        }


        /*public Pessoa (int codigo, string razaoSocial, string cpfCnpj, Endereco endereco) {
            this.codigo = codigo;
            this.razaoSocial = razaoSocial;
            this.cpfCnpj = cpfCnpj;
            this.endereco = endereco;
        }*/


        public int getCodigo() {
            return this.codigo;
        }


        public string getNome() {
            return this.nome;
        }


        public string getSobrenome() {
            return this.sobrenome;
        }


        /*public string getRazaoSocial() {
            return this.razaoSocial;
        }*/


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


        /*public void setRazaoSocial(string razaoSocial) {
            this.razaoSocial = razaoSocial;
        }*/


        public void setEndereco(Endereco endereco) {
            this.endereco = endereco;
        }

        public static bool verificaSeCpfCnpjProcuradoExiste(string cpfCnpjProcurado) {
            List<string> listaDeCpfCnpj = ArquivoPessoa.getCpfCnpjDasPessoas();

            return listaDeCpfCnpj.Exists(cpfCnpj => cpfCnpj == cpfCnpjProcurado);
        }


        public static bool armazenaCadastroDaPessoa(Pessoa pessoa) {

            string linhaCompleta = "";

            linhaCompleta += pessoa.getCodigo() + ";";
            linhaCompleta += pessoa.getNome() + ";";
            linhaCompleta += pessoa.getSobrenome() + ";";
            //linhaCompleta += pessoa.getRazaoSocial() + ";";
            linhaCompleta += pessoa.getCpfCnpj() + ";";
            linhaCompleta += pessoa.tipo + ";";
            linhaCompleta += pessoa.getEndereco().getEndereco() + ";";
            linhaCompleta += pessoa.getEndereco().getBairro() + ";";
            linhaCompleta += pessoa.getEndereco().getCidade() + ";";
            linhaCompleta += pessoa.getEndereco().getPais();

            if (Pessoa.verificaSeCpfCnpjProcuradoExiste(pessoa.getCpfCnpj())) {
                return false;
            }

            aPE.EscreverNoArquivo(linhaCompleta);

            return true;
        }

    }
}