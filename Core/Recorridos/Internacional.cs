namespace Viajes.Core.Recorridos {
	public class Internacional: Recorrido {
        /// <summary>Tiempo transporte va a máxima velocidad.</summary>
        public const double PorcentajeMaxVelocidad = 0.9;
        // <summary>El nombre del recorrido.</summary>
		public const string Id = "Internacional";

		public Internacional(double kms)
				: base( kms )
		{
		}

		public override string ToString ()
		{
			return Id + "\n" + base.ToString();
		}
	}
}
