using System;
using System.Collections.Generic;

namespace Projeto2
{
    class Pessoa
    {
        private int codigo;
        private string nome;
        private string sobrenome;
        private string cpfCnpj;
        public enum Tipo{Cliente, Fornecedor, Colaborador};
        public Tipo tipo;
        private Endereco endereco;

        static ArquivoPessoa aPE = new ArquivoPessoa();

        public Pessoa(){

        }

        public Pessoa(int codigo, string nome, string sobrenome, string cpfCnpj, Tipo tipo, Endereco endereco) {
            this.codigo = codigo;
            this.nome = nome;
            this.sobrenome = sobrenome;
            this.cpfCnpj = cpfCnpj;
            this.tipo = tipo;
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


        public string getCpfCnpj() {
            return this.cpfCnpj;
        }


        public Endereco getEndereco() {
            return this.endereco;
        }


        public void setCodigo(int codigo){
            this.codigo = codigo;
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


        public void setEndereco(Endereco endereco) {
            this.endereco = endereco;
        }

        public static bool VerificarSeCodigoProcuradoExiste(int codigoProcurado) {
            List<int> listaDeCodigos = aPE.getCodigosDasPessoas();

            return listaDeCodigos.Exists(codigo => codigo == codigoProcurado);
        }

        public bool VerificarSeCpfCnpjProcuradoExiste(string cpfCnpjProcurado) {
            List<string> listaDeCpfCnpj = aPE.getCpfCnpjDasPessoas();

            return listaDeCpfCnpj.Exists(cpfCnpj => cpfCnpj == cpfCnpjProcurado);
        }

        public static int NumeroDaPessoa(){

            List<string> lista = aPE.LerArquivo();

            int numeroDoPedido = lista.Count + 1;

            return numeroDoPedido;
        }

        public bool ArmazenarCadastroDaPessoa() {

            string linhaCompleta = "";

            linhaCompleta += this.getCodigo() + ";";
            linhaCompleta += this.getNome() + ";";
            linhaCompleta += this.getSobrenome() + ";";
            linhaCompleta += this.getCpfCnpj() + ";";
            linhaCompleta += this.tipo + ";";
            linhaCompleta += this.getEndereco().getEndereco() + ";";
            linhaCompleta += this.getEndereco().getBairro() + ";";
            linhaCompleta += this.getEndereco().getCidade() + ";";
            linhaCompleta += this.getEndereco().getEstado() + ";";
            linhaCompleta += this.getEndereco().getPais();

            if (VerificarSeCpfCnpjProcuradoExiste(this.getCpfCnpj())) {
                return false;
            }

            aPE.EscreverNoArquivo(linhaCompleta);

            return true;
        }

        public string MostrarPessoasCadastradas(){

            List<string> listaPessoas = aPE.LerArquivo();
            
            string pessoas = "";

            string codigo = "";
            string nome = "";
            string sobrenome = "";
            string cpfCnpj = "";
            string tipo = "";
            string endereco  = "";
            string bairro = "";
            string estado = "";
            string cidade = "";
            string pais = "";

            foreach(string pe in listaPessoas){
                codigo = pe.Split(';')[0];
                nome = pe.Split(';')[1];
                sobrenome = pe.Split(';')[2];
                cpfCnpj = pe.Split(';')[3];
                tipo = pe.Split(';')[4];
                endereco = pe.Split(';')[5];
                bairro = pe.Split(';')[6];
                estado = pe.Split(';')[7];
                cidade = pe.Split(';')[8];
                pais = pe.Split(';')[8];

                pessoas += "\n| Código: " + codigo + " - Nome: " + nome + " - Sobrenome: " + sobrenome + 
                " - Cpf/Cnpj: " + cpfCnpj + " - Tipo: " + tipo + " - Endereco: " + endereco + 
                " - Bairro " + bairro + " - Cidade: " + cidade + " - Estado: " + estado + " - Pais: " + pais + " |";
            }

            return pessoas+"\n";
        }

        public string ProcurarPessoa(int codigoProcurado){

            if(VerificarSeCodigoProcuradoExiste(codigoProcurado)){
                string linha = aPE.LerALinhaEspecifica(codigoProcurado);

                string pessoa = "";

                string codigo = linha.Split(';')[0];
                string nome = linha.Split(';')[1];
                string sobrenome = linha.Split(';')[2];
                string cpfCnpj = linha.Split(';')[3];
                string tipo = linha.Split(';')[4];
                string endereco = linha.Split(';')[5];
                string bairro = linha.Split(';')[6];
                string estado = linha.Split(';')[7];
                string cidade = linha.Split(';')[8];
                string pais = linha.Split(';')[9];

                pessoa += "\n| Código: " + codigo + " - Nome: " + nome + " - Sobrenome: " + sobrenome + 
                " - Cpf/Cnpj: " + cpfCnpj + " - Tipo: " + tipo + " - Endereco: " + endereco + 
                " - Bairro " + bairro + " - Cidade: " + cidade + " - Estado: " + estado + " - Pais: " + pais + " |";

                return pessoa+"\n";
            }

            return "Não existe uma pessoa com esse código";
        }

        public static Pessoa PegarDadosDaPessoa(int codigoProcurado){

            if(VerificarSeCodigoProcuradoExiste(codigoProcurado)){
                string linha = aPE.LerALinhaEspecifica(codigoProcurado);

                int codigo = Int32.Parse(linha.Split(';')[0]);
                string nome = linha.Split(';')[1];
                string sobrenome = linha.Split(';')[2];
                string cpfCnpj = linha.Split(';')[3];
                string tipoString = linha.Split(';')[4];

                Tipo tipo = Tipo.Colaborador;

                switch(tipoString){
                    case "Cliente": tipo = Tipo.Cliente;
                    break;
                    case "Fornecedor": tipo = Tipo.Fornecedor;
                    break;
                    case "Colaborador": tipo = Tipo.Colaborador;
                    break;
                }

                string endereco = linha.Split(';')[5];
                string bairro = linha.Split(';')[6];
                string cidade = linha.Split(';')[7];
                string estado = linha.Split(';')[8];
                string pais = linha.Split(';')[9];

                Endereco en = new Endereco(endereco,bairro,cidade,estado,pais);

                Pessoa pessoa = new Pessoa(codigo, nome, sobrenome, cpfCnpj, tipo, en);

                return pessoa;
            }

            return new Pessoa();
        }

    }
}