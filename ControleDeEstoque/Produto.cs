namespace ControleDeEstoque
{
    /// <summary>
    /// Classe que representa um produto
    /// </summary>
    public class Produto
    {
        /// <summary>
        /// Código
        /// </summary>
        public long Codigo { get; set; }

        /// <summary>
        /// Nome
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Preço
        /// </summary>
        public double Preco { get; set; }

        /// <summary>
        /// Quantidade em estoque
        /// </summary>
        public int Estoque { get; set; }
    }
}
