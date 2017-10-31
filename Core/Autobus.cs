namespace VisualViajes.Core {
    using Autobuses;
    
    /// <summary>
    /// Autobuses, clase base.
    /// </summary>
	public abstract class Autobus {
        public const int VelocidadMinima = 40;
    
        protected Autobus(int numAsientos = 30)
        {
            this.NumAsientos = numAsientos;
        }
        
        /// <summary>
        /// Retorna la velocidad tope del bus.
        /// </summary>
        /// <value>Un valor real entre 60 y 200.</value>
        public virtual double VelocidadMaxima => 80;
        
        /// <summary>
        /// Crea el bus adecuado para un kilometraje dado.
        /// </summary>
        /// <returns>El <see cref="Autobus"/> que mejor se adapta</returns>
        /// <param name="kms">El kilometraje, como real.</param>
		public static Autobus Crea(double kms)
		{
			Autobus toret = null;

			if ( kms < 100 ) {
				toret = new Metropolitano();
			}
			else
			if ( kms < 200 ) {
				toret = new Intercity();
			}
			else {
				toret = new LargaDistancia();
			}

			return toret;
		}
        
        /// <summary>
        /// Devuelve el número de asientos.
        /// </summary>
        /// <value>El num. de asientos, como entero.</value>
        public int NumAsientos {
            get; private set;
        }
        
        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:Viajes.Core.Autobus"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:Viajes.Core.Autobus"/>.</returns>
        public override string ToString()
        {
            return string.Format(" ({0} pax., VMax: {1}kms/h)",
                                    NumAsientos, VelocidadMaxima);
        }
	}
}
