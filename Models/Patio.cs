using System.Collections.Generic;

namespace MottuApi.Models
{
    /// <summary>
    /// Representa um p�tio de estacionamento de motos.
    /// </summary>
    public class Patio
    {
        /// <summary>
        /// Identificador �nico do p�tio.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nome do p�tio.
        /// </summary>
        public string Nome { get; set; } = string.Empty;

        /// <summary>
        /// Localiza��o f�sica do p�tio.
        /// </summary>
        public string Localizacao { get; set; } = string.Empty;

        /// <summary>
        /// Movimenta��es relacionadas a este p�tio.
        /// </summary>
        public ICollection<Movimentacao>? Movimentacoes { get; set; }
    }
}