﻿namespace VisualViajes.Core.Recorridos {
	public class Internacional: Recorrido {
        /// <summary>Tiempo transporte va a máxima velocidad.</summary>
        public const double PorcentajeMaxVelocidad = 0.9;
        // <summary>El nombre del recorrido.</summary>
		public const string Id = "Internacional";

		public Internacional(double kms)
				: base( kms )
		{
		}
        
        /// <summary>
        /// Retorna el porcentaje de tiempo que el bus puede ir a tope.
        /// </summary>
        /// <value>Un valor real entre 0 y 1.</value>
        public override double PorcentajeVMaxima => PorcentajeMaxVelocidad;

		public override string ToString ()
		{
			return Id + "\n\t" + base.ToString();
		}
	}
}
