namespace Viajes.Core.Autobuses {
	public class Intercity: Autobus {
        /// <summary>Asientos totales</summary>
        public const int NumAsientosTotal = 50;
        /// <summary>Nombre de este transporte.</summary>
		public const string Id = "Autocar InterCity";
        
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Viajes.Core.Autobuses.Intercity"/> class.
        /// </summary>
        public Intercity()
            :base( NumAsientosTotal )
        {
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:Viajes.Core.Autobuses.Intercity"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:Viajes.Core.Autobuses.Intercity"/>.</returns>
		public override string ToString()
		{
			return Id + base.ToString();
		}
	}
}
