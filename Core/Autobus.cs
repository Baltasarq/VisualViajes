namespace Viajes.Core {
    using Autobuses;
    
    /// <summary>
    /// Autobuses, clase base.
    /// </summary>
	public abstract class Autobus {
        protected Autobus(int numAsientos = 30)
        {
            this.NumAsientos = numAsientos;
        }
        
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
            return string.Format(" ({0} pax.)", NumAsientos);
        }
	}
}
