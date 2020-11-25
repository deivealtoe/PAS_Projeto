namespace Projeto2
{
    class Endereco
    {
        private string cep;
        private string bairro;
        private string cidade;
        private string estado;
        private string pais;

        public Endereco(){
            
        }

        public Endereco (string cep, string bairro, string cidade, string estado, string pais) {
            this.cep = cep;
            this.bairro = bairro;
            this.cidade = cidade;
            this.estado = estado;
            this.pais = pais;
        }

        public string getCep(){
            return this.cep;
        }

        public string getBairro(){
            return this.bairro;
        }

        public string getCidade(){
            return this.cidade;
        }

        public string getEstado(){
            return this.estado;
        }

        public string getPais(){
            return this.pais;
        }

        public void setCep(string cep){
            this.cep = cep;
        }

        public void setBairro(string bairro){
            this.bairro = bairro;
        }

        public void setCidade(string cidade){
            this.cidade = cidade;
        }

        public void setEstado(string estado){
            this.estado = estado;
        }

        public void setPais(string pais){
            this.pais = pais;
        }

    }
}