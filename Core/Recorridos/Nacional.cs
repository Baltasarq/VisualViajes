namespace Viajes.Core  {
	public class Nacional: Recorrido {
        /// <summary>Tiempo transporte va a máxima velocidad.</summary>
        public const double PorcentajeMaxVelocidad = 0.8;
        /// <summary>El nombre del recorrido.</summary>
		public const string Id = "Nacional";

		public Nacional(double kms)
				: base( kms )
		{
		}

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:Viajes.Core.Nacional"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:Viajes.Core.Nacional"/>.</returns>
		public override string ToString ()
		{
			return Id + "\n" + base.ToString();
		}
	}
}

