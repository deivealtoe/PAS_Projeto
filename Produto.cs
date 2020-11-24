using System;
using System.Collections.Generic;

namespace Projeto2
{
    class Produto
    {
        private int codigo;
        private string descricao;
        private double peso;
        private double valorUnitario;

        static ArquivoProduto aPR = new ArquivoProduto();

        public Produto(){

        }

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

        public static bool VerificarSeCodigoProcuradoExiste(int codigoProcurado) {
            List<int> listaDeCodigos = aPR.getCodigosDosProdutos();

            return listaDeCodigos.Exists(codigo => codigo == codigoProcurado);
        }

        public static int NumeroDoProduto(){

            List<string> lista = aPR.LerArquivo();

            int numeroDoPedido = lista.Count + 1;

            return numeroDoPedido;
        }

        public void ArmazenarCadastroDoProduto() {

            string linhaCompleta = "";

            linhaCompleta += this.getCodigo() + ";";
            linhaCompleta += this.getDescricao() + ";";
            linhaCompleta += this.getPeso() + ";";
            linhaCompleta += this.getValorUnitario();

            aPR.EscreverNoArquivo(linhaCompleta);
        }

        public string MostrarProdutosCadastrados(){

            List<string> listaProdutos = aPR.LerArquivo();
            string produtos = "";
            string codigo = "";
            string descricao = "";
            string peso = "";
            string valorUnitario = "";

            foreach(string pd in listaProdutos){
                codigo = pd.Split(';')[0];
                descricao = pd.Split(';')[1];
                peso = pd.Split(';')[2];
                valorUnitario = pd.Split(';')[3];

                produtos += "\n| Código: " + codigo + " - Descrição: " + descricao + " - Peso: " + peso + " - Valor Unitário: " + valorUnitario + " |";
            }

            return produtos+"\n";
        }

        public static Produto PegarDadosDoProduto(int codigoProcurado){

            if(VerificarSeCodigoProcuradoExiste(codigoProcurado)){
                string linha = aPR.LerALinhaEspecifica(codigoProcurado);

                int codigo = Int32.Parse(linha.Split(';')[0]);
                string descricao = linha.Split(';')[1];
                double peso = Double.Parse(linha.Split(';')[2]);
                double valorUnitario = Double.Parse(linha.Split(';')[3]);

                Produto produto = new Produto(codigo,descricao,peso,valorUnitario);

                return produto;
            }
            
            return new Produto();
        }

    }
}