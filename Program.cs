﻿using System;

namespace Projeto2
{
    class Program
    {
        static void Main(string[] args)
        {

            //Instância de classes
            Produto produto = new Produto();
            Endereco endereco = new Endereco();
            Pessoa pessoa = new Pessoa();
            ItemDeCompra item = new ItemDeCompra();
            CarrinhoDeCompra carrinhoDeCompra = new CarrinhoDeCompra();
            Pedido pedidoDeCompra = new PedidoDeCompra();
            Pedido pedidoDeVenda = new PedidoDeVenda();
            Estoque estoque = new Estoque();

            //variáveis
            int codigoProduto, codigoPessoa, qtd, codigoPedido, codigoFornecedor, codigoCliente;
            string descricao, nome, sobrenome, cpfCnpj, cep, bairro, estado,
            cidade, pais, stringTipo = "", escolha = "sim"; 
            double peso, valorUnitario;
            Pessoa.Tipo tipo = Pessoa.Tipo.Colaborador;
            bool confirmado, continuar = true;

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

            Console.WriteLine("║ 3 - INSERIR PRODUTO NO ESTOQUE         ║    ");

            Console.WriteLine("║                                        ║    ");

            Console.WriteLine("║ 4 - REALIZAR PEDIDO DE COMPRA          ║    ");
            
            Console.WriteLine("║                                        ║    ");

            Console.WriteLine("║ 5 - REALIZAR PEDIDO DE VENDA           ║    ");

            Console.WriteLine("║                                        ║    ");

            Console.WriteLine("║ 6 - CONFIRMAR PEDIDO DE COMPRA         ║    ");

            Console.WriteLine("║                                        ║    ");

            Console.WriteLine("║ 7 - CONFIRMAR PEDIDO DE VENDA          ║    ");

            Console.WriteLine("║                                        ║    ");

            Console.WriteLine("║ 8 - SAIR                               ║    ");

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
                    Console.WriteLine("CADASTRE O PRODUTO DESEJADO\n");

                    codigoProduto = Produto.NumeroDoProduto();

                    Console.WriteLine("Digite a descrição do produto: ");
                    descricao = Console.ReadLine();
                    
                    Console.WriteLine("Digite o peso do produto(kg): ");
                    peso = Double.Parse(Console.ReadLine());

                    Console.WriteLine("Digite o valor da unidade do produto: ");
                    valorUnitario = Double.Parse(Console.ReadLine());

                    produto = new Produto(codigoProduto,descricao,peso,valorUnitario);

                    produto.ArmazenarCadastroDoProduto();
                    
                    Console.WriteLine("\nProduto cadastrado com sucesso");
                    break;

                    //Cadastra a pessoa
                    case "2":
                    Console.WriteLine("CADASTRE A PESSOA DESEJADA\n");

                    codigoPessoa = Pessoa.NumeroDaPessoa();

                    Console.WriteLine("Digite o nome: ");
                    nome = Console.ReadLine();

                    Console.WriteLine("Digite o sobrenome: ");
                    sobrenome = Console.ReadLine();

                    Console.WriteLine("Digite o cpf/cnpj: ");
                    cpfCnpj = Console.ReadLine();

                    while(continuar == true){

                        Console.WriteLine("Digite qual o tipo(Cliente | Fornecedor | Colaborador): ");
                        stringTipo = Console.ReadLine().ToUpper();

                        switch(stringTipo.Trim()){
                            case "CLIENTE": 
                            tipo = Pessoa.Tipo.Cliente;
                            continuar = false;
                            break;
                            case "FORNECEDOR": 
                            tipo = Pessoa.Tipo.Fornecedor;
                            continuar = false;
                            break;
                            case "COLABORADOR": 
                            tipo = Pessoa.Tipo.Colaborador;
                            continuar = false;
                            break;
                            default:
                            Console.WriteLine("\nTipo inválido\n");
                            break;
                        }
                    }

                    Console.WriteLine("Digite o cep: ");
                    cep = Console.ReadLine();

                    Console.WriteLine("Digite o bairro: ");
                    bairro = Console.ReadLine();

                    Console.WriteLine("Digite a cidade: ");
                    cidade = Console.ReadLine();

                    Console.WriteLine("Digite o estado: ");
                    estado = Console.ReadLine();

                    Console.WriteLine("Digite o pais: ");
                    pais = Console.ReadLine();

                    endereco = new Endereco(cep,bairro,cidade,estado,pais);

                    pessoa = new Pessoa(codigoPessoa,nome,sobrenome,cpfCnpj,tipo,endereco);

                    if(pessoa.ArmazenarCadastroDaPessoa()){
                        Console.WriteLine("\nPessoa cadastrada com sucesso");
                    }
                    else{
                        Console.WriteLine("\nJá existe uma pessoa cadastrada com esse cpf/cnpj");
                    }
                    break;

                    //Insere um produto no estoque caso já exista uma quantidade desse produto
                    case "3":
                    Console.WriteLine("INSIRA O PRODUTO NO ESTOQUE");

                    Console.WriteLine("Digite o código do produto cadastrado: ");
                    codigoProduto = Int32.Parse(Console.ReadLine());

                    Console.WriteLine("Digite a quantidade existente: ");
                    qtd = Int32.Parse(Console.ReadLine());

                    if(estoque.InserirProdutoEstoque(codigoProduto,qtd)){
                        Console.WriteLine("\nProduto inserido com sucesso");
                    }else{
                        Console.WriteLine("\nNão foi possível inserir o produto no estoque");
                    }
                    break;

                    //Realiza um pedido de compra
                    case "4":
                    Console.WriteLine("REALIZE O PEDIDO DE COMPRA\n");

                    codigoPedido = Pedido.NumeroDoPedido();
                    
                    confirmado = false;

                    Console.WriteLine(pessoa.MostrarPessoasCadastradas());

                    Console.WriteLine("Digite o código do fornecedor: ");
                    codigoFornecedor = Int32.Parse(Console.ReadLine());

                    Pessoa fornecedor = Pessoa.PegarDadosDaPessoa(codigoFornecedor);

                    if(fornecedor.getCodigo() != 0 && pedidoDeCompra.ValidarPessoa(fornecedor)){

                        Console.WriteLine("\nESCOLHA OS PRODUTOS DESEJADOS\n");

                        Console.WriteLine(produto.MostrarProdutosCadastrados());

                        while(escolha == "sim"){

                            Console.WriteLine("Digite o código do produto: ");
                            codigoProduto = Int32.Parse(Console.ReadLine());

                            produto = Produto.PegarDadosDoProduto(codigoProduto);

                            if(produto.getCodigo() != 0){
                                continuar = true;
                                while(continuar == true){

                                    Console.WriteLine("Digite a quantidade desejada: ");
                                    qtd = Int32.Parse(Console.ReadLine());

                                    if(qtd > 0){
                                        continuar = false;
                                        item = new ItemDeCompra(produto, qtd);

                                        carrinhoDeCompra.AdicionarItem(item);

                                        Console.WriteLine(carrinhoDeCompra.getResumoCarrinho());
                                    }else{
                                        Console.WriteLine("\nA quantidade desejada precisa ser maior que 0\n");
                                    }
                                }
                                Console.WriteLine("Deseja escolher mais algum produto?(Sim|Não) ");
                                escolha = Console.ReadLine().ToLower(); 
                                Console.WriteLine("");
                            }else{
                                Console.WriteLine("\nCódigo do produto inválido\n");
                            }                         
                        }
                        pedidoDeCompra = new PedidoDeCompra(codigoPedido,confirmado,carrinhoDeCompra,fornecedor);

                        pedidoDeCompra.ArmazenarPedido();
                        Console.WriteLine("\nPedido realizado com sucesso");
                        
                    }else{
                        Console.WriteLine("\nCódigo do fornecedor inválido");
                    }                  
                    break;

                    //Realiza um pedido de venda
                    case "5":
                    Console.WriteLine("REALIZE O PEDIDO DE VENDA\n");

                    codigoPedido = Pedido.NumeroDoPedido();
                    
                    confirmado = false;

                    Console.WriteLine(pessoa.MostrarPessoasCadastradas());

                    Console.WriteLine("Digite o código do cliente: ");
                    codigoCliente = Int32.Parse(Console.ReadLine());

                    Pessoa cliente = Pessoa.PegarDadosDaPessoa(codigoCliente);

                    if(cliente.getCodigo() != 0 && pedidoDeVenda.ValidarPessoa(cliente)){

                        Console.WriteLine("\nESCOLHA OS PRODUTOS DESEJADOS\n");

                        Console.WriteLine(produto.MostrarProdutosCadastrados());

                        escolha = "sim";

                        while(escolha == "sim"){

                            Console.WriteLine("Digite o código do produto: ");
                            codigoProduto = Int32.Parse(Console.ReadLine());

                            produto = Produto.PegarDadosDoProduto(codigoProduto);

                            if(produto.getCodigo() != 0){
                                continuar = true;
                                while(continuar == true){

                                    Console.WriteLine("Digite a quantidade desejada: ");
                                    qtd = Int32.Parse(Console.ReadLine());

                                    if(qtd > 0){
                                        continuar = false;
                                        item = new ItemDeCompra(produto, qtd);

                                        carrinhoDeCompra.AdicionarItem(item);

                                        Console.WriteLine(carrinhoDeCompra.getResumoCarrinho());
                                    }else{
                                        Console.WriteLine("\nA quantidade desejada precisa ser maior que 0\n");
                                    }
                                }
                                Console.WriteLine("Deseja escolher mais algum produto?(Sim|Não) ");
                                escolha = Console.ReadLine().ToLower(); 
                                Console.WriteLine("");
                            }else{
                                Console.WriteLine("\nCódigo do produto inválido\n");
                            }    
                        }
                        pedidoDeVenda = new PedidoDeVenda(codigoPedido,confirmado,carrinhoDeCompra,cliente);

                        pedidoDeVenda.ArmazenarPedido();
                        Console.WriteLine("\nPedido realizado com sucesso");
                        
                    }else{
                        Console.WriteLine("\nCódigo do cliente inválido");
                    }
                    break;

                    //Confirma um pedido de compra
                    case "6":
                    if(pedidoDeCompra.MostrarPedidosCadastrados().Length > 0){

                        Console.WriteLine("CONFIRME O PEDIDO DE COMPRA DESEJADO\n");

                        Console.WriteLine(pedidoDeCompra.MostrarPedidosCadastrados());

                        Console.WriteLine("Digite o código do pedido: ");
                        codigoPedido = Int32.Parse(Console.ReadLine());

                        pedidoDeCompra = pedidoDeCompra.PegarDadosDoPedido(codigoPedido);

                        if(pedidoDeCompra.getConfirmado().Equals(false) && pedidoDeCompra.GetCarrinhoDeCompra() != null){
                            pedidoDeCompra.ConfirmarPedido();

                            estoque.AtualizarEstoqueCompra(pedidoDeCompra);

                            Console.WriteLine("\nPedido confirmado com sucesso");
                        }else{
                            Console.WriteLine("\nNão foi possível confirmar o pedido"); 
                        } 
                    }else{
                        Console.WriteLine("Não há nenhum pedido de compra para confirmar");
                    }
                    break;

                    //Confirma um pedido de venda
                    case "7":
                    if(pedidoDeVenda.MostrarPedidosCadastrados().Length > 0){
                        Console.WriteLine("CONFIRME O PEDIDO DE VENDA DESEJADO\n");

                        Console.WriteLine(pedidoDeVenda.MostrarPedidosCadastrados());

                        Console.WriteLine("Digite o código do pedido: ");
                        codigoPedido = Int32.Parse(Console.ReadLine());

                        pedidoDeVenda = pedidoDeVenda.PegarDadosDoPedido(codigoPedido);

                        if(pedidoDeVenda.getConfirmado().Equals(false) && pedidoDeVenda.GetCarrinhoDeCompra() != null){
                            if(estoque.ValidarPedidoDeVenda(pedidoDeVenda)){
                                pedidoDeVenda.ConfirmarPedido();
                                estoque.AtualizarEstoqueVenda(pedidoDeVenda);
                                Console.WriteLine("\nPedido confirmado com sucesso");
                            }
                        }else{
                            Console.WriteLine("\nNão foi possível confirmar o pedido"); 
                        }
                    }else{
                        Console.WriteLine("Não há nenhum pedido de venda para confirmar");
                    }
                    break;

                    //Encerra a aplicação
                    case "8":
                    Console.Clear();
                    Console.WriteLine("PROGRAMA FINALIZADO COM SUCESSO");
                    repetir = false;
                    break;

                    //Caso não seja nenhuma das opções acima
                    default:
                    Console.WriteLine("OPÇÃO INVALIDA");
                    break;
                }
            }   
        }
    }
}

