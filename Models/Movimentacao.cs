using System;

namespace MottuApi.Models
{
    /// <summary>
    /// Representa o registro de entrada e sa�da de uma moto em um p�tio.
    /// </summary>
    public class Movimentacao
    {
        /// <summary>
        /// Identificador �nico da movimenta��o.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Identificador da moto relacionada.
        /// </summary>
        public int MotoId { get; set; }

        /// <summary>
        /// Identificador do p�tio relacionado.
        /// </summary>
        public int PatioId { get; set; }

        /// <summary>
        /// Data e hora de entrada da moto no p�tio.
        /// </summary>
        public DateTime DataEntrada { get; set; }

        /// <summary>
        /// Data e hora de sa�da da moto do p�tio (opcional).
        /// </summary>
        public DateTime? DataSaida { get; set; }

        /// <summary>
        /// Moto relacionada � movimenta��o.
        /// </summary>
        public Moto? Moto { get; set; }

        /// <summary>
        /// P�tio relacionado � movimenta��o.
        /// </summary>
        public Patio? Patio { get; set; }
    }
}