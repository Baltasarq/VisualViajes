namespace Viajes.Core.Autobuses {
	public class Metropolitano: Autobus {
        /// <summary>Asientos totales</summary>
        public const int NumAsientosTotal = 45;
        /// <summary>Nombre de este transporte.</summary>
		public const string Id = "Autocar metropolitano";

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Viajes.Core.Autobuses.Metropolitano"/> class.
        /// </summary>
        public Metropolitano()
            :base( NumAsientosTotal )
        {
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:Viajes.Core.Autobuses.Metropolitano"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:Viajes.Core.Autobuses.Metropolitano"/>.</returns>
		public override string ToString()
		{
			return Id + base.ToString();
		}
	}
}

