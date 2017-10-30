namespace Viajes.Core {
    using System;

    using Recorridos;
    
	public abstract class Recorrido {    
		protected Recorrido(double kms)
		{
            if ( kms < 1 ) {
                throw new ArgumentException( "Kms no debe ser inferior a 1" );
            }

			this.Kms = kms;
		}

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:Viajes.Core.Recorrido"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:Viajes.Core.Recorrido"/>.</returns>
		public override string ToString()
        {
            return string.Format("Kms: {0:.2f}", Kms);
        }

		public static Recorrido Crea(double kms)
		{
			Recorrido toret = null;

			if ( kms < 50 ) {
				toret = new Urbano( kms );
			}
			else
			if (kms < 100 ) {
				toret = new Interurbano( kms );
			}
			else
			if (kms < 200 ) {
				toret = new Provincial( kms );
			}
			else
			if (kms < 1000 ) {
				toret = new Nacional( kms );
			}
			else {
				toret = new Internacional( kms );
			}
            
            if ( toret == null ) {
                throw new System.ArgumentException( "Kms: "
                                + kms
                                + " no se corresponden con recorrido alguno" );
            }

			return toret;
		}
        
        /// <summary>
        /// Distancia total a recorrer.
        /// </summary>
        /// <value>The kms, como num. real.</value>
        public double Kms {
            get; private set;
        }
	}
}

