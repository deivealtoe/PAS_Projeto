using System;

namespace Projeto2
{
    class Program
    {
        static void Main(string[] args)
        {
            Endereco e = new Endereco();



            Produto p = new Produto(50, "Descrição", 10.55, 1.99);

            p.setCodigo(1);
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

            carrinho.adicionarItem(idc);
            //carrinho.removerItem(1);

            Console.WriteLine(carrinho.getResumoCarrinho());

        }
    }
}