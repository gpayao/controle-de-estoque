using System;
using System.Collections.Generic;
using System.Linq;

namespace ControleDeEstoque
{
    public class Program
    {
        public static List<Produto> Produtos { get; set; }

        public static int QuantidadeProdutosVendidos { get; set; }
        public static int QuantidadeProdutosComprados { get; set; }

        public static double ValorTotalCompra { get; set; }
        public static double ValorTotalVenda { get; set; }

        public static void Main()
        {
            Console.Title = "Controle de Estoque";
            Console.WriteLine("Controle de Estoque");

            Produtos = new List<Produto>();

            int acaoAtual;
            do
            {
                Console.WriteLine("Menu Principal:\n1- Produto\n2- Compra\n3- Venda\n4- Relatório\n0- Sair");

                Console.Write("Opção: ");

                var parasable = int.TryParse(Console.ReadLine(), out acaoAtual);

                if (!parasable)
                {
                    acaoAtual = int.MaxValue;
                }

                Console.Clear();

                switch (acaoAtual)
                {
                    case 1:
                        Cadastrar();
                        break;

                    case 2:
                        Comprar();
                        break;

                    case 3:
                        Vender();
                        break;

                    case 4:
                        Relatorio();
                        break;

                    case 0:
                        break;

                    default:
                        Console.WriteLine("Não existe essa opção!");
                        break;
                }
            } while (acaoAtual != 0);
        }

        #region Helpers

        private static Produto BuscaProduto(long codigo)
        {
            Produto produto = Produtos.FirstOrDefault(x => x.Codigo == codigo);

            return produto;
        }

        #endregion

        #region Cadastro

        private static void Cadastrar()
        {
            Console.WriteLine("Menu Produto:\n1- Novo\n2- Excluir\n0- Sair");

            Console.Write("Opção: ");
            int.TryParse(Console.ReadLine(), out int acaoAtual);

            Console.Clear();

            switch (acaoAtual)
            {
                case 1:
                    NovoProduto();
                    break;

                case 2:
                    ExcluirProduto();
                    break;

                case 0:
                    break;

                default:
                    Console.WriteLine("Não existe essa opção!");
                    break;
            }
        }

        private static void NovoProduto()
        {
            Console.WriteLine("==========NOVO PRODUTO==========");
            Console.Write("Código: ");
            long.TryParse(Console.ReadLine(), out long codigo);

            if (codigo <= 0)
            {
                Console.WriteLine("Código deve ser maior que zero.");
                return;
            }

            Produto produto = BuscaProduto(codigo);

            if (produto != null)
            {
                Console.WriteLine("Já existe um produto cadastrado com este código.");
                return;
            }

            Console.Write("Nome: ");
            string nome = Console.ReadLine();

            Console.Write("Preço: ");
            double.TryParse(Console.ReadLine(), out double preco);

            Console.Write("Estoque: ");
            int.TryParse(Console.ReadLine(), out int estoque);

            Produtos.Add(new Produto { Codigo = codigo, Estoque = estoque, Nome = nome, Preco = preco });
        }

        private static void ExcluirProduto()
        {
            Console.Write("Código do produto para excluir: ");
            long.TryParse(Console.ReadLine(), out long codigo);

            Produto produto = BuscaProduto(codigo);

            if (produto == null)
            {
                Console.WriteLine("Nenhum produto encontrado.");
                return;
            }
            else
            {
                char confirma;
                do
                {
                    Console.WriteLine("Confirma a exclusão do produto? (S/N)");
                    char.TryParse(Console.ReadLine().ToUpper(), out confirma);

                    switch (confirma)
                    {
                        case 'S':
                            Produtos.Remove(produto);
                            Console.WriteLine("Produto excluído com sucesso!");
                            break;

                        case 'N':
                            Console.WriteLine("Nenhum produto foi exlcuído.");
                            break;

                        default:
                            Console.WriteLine("Não existe essa opção!");
                            confirma = default;
                            break;
                    }

                } while (confirma == default);
            }
        }

        #endregion

        #region Relatorio

        private static void Relatorio()
        {
            Console.WriteLine("Menu Relatório:\n1 - Produto\n2 - Compra\n3 - Venda\n0 - Sair");

            Console.Write("Opção: ");
            int.TryParse(Console.ReadLine(), out int acaoAtual);

            Console.Clear();

            switch (acaoAtual)
            {
                case 1:
                    RelatorioProduto();
                    break;

                case 2:
                    RelatorioCompra();
                    break;

                case 3:
                    RelatorioVenda();
                    break;

                case 0:
                    break;

                default:
                    Console.WriteLine("Não existe essa opção!");
                    break;
            }
        }

        private static void RelatorioProduto()
        {
            if (Produtos.Count > 0)
            {
                Console.WriteLine("==========RELATÓRIO DE PRODUTOS==========");
                for (int i = 0; i < Produtos.Count; i++)
                {
                    Console.WriteLine("Produto: {0}\nNome: {1}\nPreço: {2}\nQuantidade em estoque: {3}", Produtos[i].Codigo, Produtos[i].Nome, Produtos[i].Preco, Produtos[i].Estoque);

                    if (i < Produtos.Count - 1)
                    {
                        Console.WriteLine("-----------------------------------------");
                    }
                }
                Console.WriteLine("===================FIM===================");
            }
            else
            {
                Console.WriteLine("Não existe nenhum produto cadastrado.");
            }
        }

        private static void RelatorioVenda()
        {
            Console.WriteLine("==========RELATÓRIO DE VENDAS==========");
            Console.WriteLine("Produtos Vendidos: {0}\nValor Total de Venda: {1}", QuantidadeProdutosVendidos, ValorTotalVenda.ToString("N2"));
            Console.WriteLine("==================FIM==================");
        }

        private static void RelatorioCompra()
        {
            Console.WriteLine("==========RELATÓRIO DE COMPRAS==========");
            Console.WriteLine("Produtos Comprados: {0}\nValor Total de Compra: {1}", QuantidadeProdutosComprados, ValorTotalCompra.ToString("N2"));
            Console.WriteLine("==================FIM==================");
        }

        #endregion

        #region Compra & Venda

        private static void Comprar()
        {
            Console.Write("Qual o código do produto a ser comprado: ");
            long.TryParse(Console.ReadLine(), out long codigo);

            Produto produto = BuscaProduto(codigo);

            if (produto != null)
            {
                Console.WriteLine("Quantas unidades você deseja comprar?");
                int.TryParse(Console.ReadLine(), out int quantidade);

                var index = Produtos.FindIndex(p => p.Codigo == codigo);
                Produtos[index].Estoque += quantidade;

                QuantidadeProdutosComprados += quantidade;
                ValorTotalCompra += produto.Preco * quantidade;

                Console.WriteLine("Produto comprado com sucesso!\nProduto: " + produto.Codigo + "\nEstoque: " + produto.Estoque);
            }
            else
            {
                Console.WriteLine("Produto não encontrado.");
            }
        }

        private static void Vender()
        {
            Console.Write("Qual o código do produto a ser vendido: ");
            long.TryParse(Console.ReadLine(), out long codigo);

            Produto produto = BuscaProduto(codigo);

            if (produto != null)
            {
                Console.WriteLine("Quantas unidades você deseja vender?");
                int.TryParse(Console.ReadLine(), out int quantidade);
                if (produto.Estoque >= quantidade)
                {
                    var index = Produtos.FindIndex(p => p.Codigo == codigo);
                    Produtos[index].Estoque -= quantidade;

                    QuantidadeProdutosVendidos += quantidade;
                    ValorTotalVenda += produto.Preco * quantidade;

                    Console.WriteLine("Produto vendido com sucesso!\nProduto: " + produto.Codigo + "\nEstoque: " + produto.Estoque);
                }
                else
                {
                    Console.WriteLine("Não tem estoque suficiente para venda desse produto.\nEstoque Disponível: " + produto.Estoque);
                }
            }
            else
            {
                Console.WriteLine("Produto não encontrado.");
            }
        }

        #endregion
    }
}
