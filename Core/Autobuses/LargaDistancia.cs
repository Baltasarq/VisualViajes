namespace Viajes.Core.Autobuses {
    /// <summary>Autocar de larga distancia: nacional, internacioal...</summary>
	public class LargaDistancia: Autobus {
        /// <summary>Asientos totales</summary>
        public const int NumAsientosTotal = 80;
        /// <summary>El nombre del transporte.</summary>
		public const string Id = "Autocar larga distancia";
        
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Viajes.Core.Autobuses.LargaDistancia"/> class.
        /// </summary>
        public LargaDistancia()
            :base( NumAsientosTotal )
        {
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:Viajes.Core.Autobuses.LargaDistancia"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:Viajes.Core.Autobuses.LargaDistancia"/>.</returns>
		public override string ToString()
		{
			return Id + base.ToString();
		}
	}
}
