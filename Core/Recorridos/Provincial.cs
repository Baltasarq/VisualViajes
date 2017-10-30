namespace Viajes.Core {
	public class Provincial: Recorrido {
        /// <summary>Tiempo transporte va a máxima velocidad.</summary>
        public const double PorcentajeMaxVelocidad = 0.8;
        /// <summary>El nombre del recorrido.</summary>
		public const string Id = "Provincial";

		public Provincial(double kms)
				: base( kms )
		{
		}

		public override string ToString ()
		{
			return Id + "\n" + base.ToString();
		}
	}
}

