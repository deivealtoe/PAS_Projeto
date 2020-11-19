namespace Projeto2
{
    class Endereco
    {

        private string endereco;
        private string bairro;
        private string cidade;
        private string estado;
        private string pais;

        public Endereco (string endereco, string bairro, string cidade, string pais) {

            this.endereco = endereco;
            this.bairro = bairro;
            this.cidade = cidade;
            this.pais = pais;
        }

        public string getEndereco(){
            return this.endereco;
        }

        public string getBairro(){
            return this.bairro;
        }

        public string getCidade(){
            return this.cidade;
        }

        public string getPais(){
            return this.pais;
        }

        public void setEndereco(string endereco){
            this.endereco = endereco;
        }

        public void setBairro(string bairro){
            this.bairro = bairro;
        }

        public void setCidade(string cidade){
            this.cidade = cidade;
        }

        public void setPais(string pais){
            this.pais = pais;
        }

    }
}