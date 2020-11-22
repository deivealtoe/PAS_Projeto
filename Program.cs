﻿using System;

namespace Projeto2
{
    class Program
    {
        static void Main(string[] args)
        {

            Produto p = new Produto(50, "Descrição", 10.55, 1.99);

            p.setCodigo(0);
            p.setDescricao("Outra Descrição");
            p.setPeso(5.55);
            p.setValorUnitario(2.50);

            Console.WriteLine(p.getCodigo());
            Console.WriteLine(p.getDescricao());
            Console.WriteLine(p.getPeso());
            Console.WriteLine(p.getValorUnitario());

            ItemDeCompra idc = new ItemDeCompra(p, 5);

            idc.setQtdCompra(30);

            Console.WriteLine(idc.getProduto().getDescricao());
            Console.WriteLine(idc.getQtdCompra());
            Console.WriteLine(idc.getValorTotal());

            CarrinhoDeCompra carrinho = new CarrinhoDeCompra();

            Estoque e = new Estoque(p, 50, 20);

            carrinho.adicionarItem(idc);
            //carrinho.removerItem(1);

            Console.WriteLine(carrinho.getResumoCarrinho());

            Console.WriteLine(p.armazenaCadastroDoProduto());

            e.calcularEstoque();

            Console.WriteLine(e.armazenaProdutoEstoque(p));

            p.setCodigo(2);
            p.setDescricao("Descrição");
            p.setPeso(2.10);
            p.setValorUnitario(6);
            
            Console.WriteLine(p.armazenaCadastroDoProduto());

            e.setProduto(p);
            e.setQtdTotal(30);
            e.setQtdReservada(29);

            e.calcularEstoque();

            Console.WriteLine(e.armazenaProdutoEstoque(p));

            p.setCodigo(4);
            p.setDescricao("Descrição");
            p.setPeso(3);
            p.setValorUnitario(9);

            Console.WriteLine(p.armazenaCadastroDoProduto());

            e.setProduto(p);
            e.setQtdTotal(100);
            e.setQtdReservada(20);

            e.calcularEstoque();

            Console.WriteLine(e.armazenaProdutoEstoque(p));

            Endereco en = new Endereco("Sei lá", "Laranjeiras", "ES", "Serra", "Brasil");

            Pessoa pe = new Pessoa(0,"Jefferson","Souza","123.164.123-23", Pessoa.Tipo.Cliente, en);
            
            Console.WriteLine(pe.armazenaCadastroDaPessoa());


            Console.WriteLine(p.mostrarProdutosCadastrados());

            PedidoDeCompra pDC = new PedidoDeCompra(1, false, carrinho, pe);

            Console.WriteLine(pDC.ArmazenarPedido());

        }
    }
}