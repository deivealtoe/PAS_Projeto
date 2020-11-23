using System;

namespace Projeto2
{
    class Program
    {
        static void Main(string[] args)
        {
            //Menu
            string menu;
            bool repetir = true;

            while(repetir == true){
  
            Console.WriteLine(" ");

            Console.WriteLine("╔═════════════MENU DE OPÇÕES═════════════╗    ");

            Console.WriteLine("║ 1 - CADASTRAR PRODUTO                  ║    ");

            Console.WriteLine("║                                        ║    ");

            Console.WriteLine("║ 2 - CADASTRAR PESSOA                   ║    ");

            Console.WriteLine("║                                        ║    ");

            Console.WriteLine("║ 3 - REALIZAR PEDIDO DE COMPRA          ║    ");
            
            Console.WriteLine("║                                        ║    ");

            Console.WriteLine("║ 4 - REALIZAR PEDIDO DE VENDA           ║    ");

            Console.WriteLine("╚════════════════════════════════════════╝    ");

            Console.WriteLine(" ");

            Console.Write("Escolha uma opção: ");

            menu = Console.ReadLine();
            Console.Clear();
            Console.WriteLine(" ");

            //Opções do menu
            switch(menu){

            //Cadastra o produto
            case "1":

            int codigoProduto;
            string descricao;
            double peso;
            double valorUnitario;

            Console.WriteLine("CADASTRE O PRODUTO DESEJADO\n");

            codigoProduto = Produto.NumeroDoProduto();

            Console.WriteLine("Digite a descrição do produto: ");
            descricao = Console.ReadLine();
            
            Console.WriteLine("Digite o peso do produto(kg): ");
            peso = Double.Parse(Console.ReadLine());

            Console.WriteLine("Digite o valor da unidade do produto: ");
            valorUnitario = Double.Parse(Console.ReadLine());

            Produto produto = new Produto(codigoProduto,descricao,peso,valorUnitario);
            
            Console.WriteLine("\nProduto cadastrado com sucesso");
            break;

            case "2":

            int codigoPessoa; 
            string nome; 
            string sobrenome; 
            string cpfCnpj;
            string stringTipo = ""; 
            Pessoa.Tipo tipo = Pessoa.Tipo.Colaborador;

            Console.WriteLine("CADASTRE A PESSOA DESEJADA\n");

            codigoPessoa = Pessoa.NumeroDaPessoa();

            Console.WriteLine("Digite o nome: ");
            nome = Console.ReadLine();

            Console.WriteLine("Digite o sobrenome: ");
            sobrenome = Console.ReadLine();

            Console.WriteLine("Digite o cpf/cnpj: ");
            cpfCnpj = Console.ReadLine();

            while(stringTipo == "CLIENTE" || stringTipo == "FORNECEDOR" || stringTipo == "COLABORADOR"){

                Console.WriteLine("Digite qual o tipo(Cliente | Fornecedor | Colaborador): ");
                stringTipo = Console.ReadLine().ToUpper();

                switch(stringTipo.Trim()){
                    case "CLIENTE": 
                    tipo = Pessoa.Tipo.Cliente;
                    break;
                    case "FORNECEDOR": 
                    tipo = Pessoa.Tipo.Fornecedor;
                    break;
                    case "COLABORADOR": 
                    tipo = Pessoa.Tipo.Colaborador;
                    break;
                    default:
                    Console.WriteLine("Tipo inválido\n");
                    break;
                }
            }

            string campoEndereco; 
            string bairro; 
            string estado;
            string cidade; 
            string pais;

            Console.WriteLine("Digite o endereço(Avenida/Setor/etc): ");
            campoEndereco = Console.ReadLine();

            Console.WriteLine("Digite o bairro: ");
            bairro = Console.ReadLine();

            Console.WriteLine("Digite o estado: ");
            estado = Console.ReadLine();

            Console.WriteLine("Digite a cidade: ");
            cidade = Console.ReadLine();

            Console.WriteLine("Digite o pais: ");
            pais = Console.ReadLine();

            Endereco endereco = new Endereco(campoEndereco,bairro,estado,cidade,pais);

            Pessoa pessoa = new Pessoa(codigoPessoa,nome,sobrenome,cpfCnpj,tipo,endereco);

            if(pessoa.ArmazenarCadastroDaPessoa()){
                Console.WriteLine("\nPessoa cadastrada com sucesso");
            }
            else{
                Console.WriteLine("\nJá existe uma pessoa cadastrada com esse cpf/cnpj");
            }
            break;

            case "3":

            
            break;

            //Caso não seja nenhuma das opções acima
            default:
            Console.WriteLine("OPÇÃO INVALIDA...");
            break;
            }
        }  
            

        }
    }
}