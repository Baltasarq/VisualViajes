namespace Viajes.Core {
    using System;
    
    public class Viaje {
        public Viaje(string inicio, string destino, double kms)
        {
            if ( string.IsNullOrWhiteSpace( inicio ) ) {
                throw new ArgumentException( "Inicio incorrecto, sin datos" );
            }
            
            if ( string.IsNullOrWhiteSpace( destino ) ) {
                throw new ArgumentException( "Destino incorrecto, sin datos" );
            }
            
            this.Inicio = inicio.Trim();
            this.Destino = destino.Trim();
            this.Recorrido = Recorrido.Crea( kms );
            this.Autobus = Autobus.Crea( kms );
        }
        
        public double Kms => this.Recorrido.Kms;
        
        /// <summary>
        /// El inicio del viaje.
        /// </summary>
        /// <value>El inicio, como cadena.</value>
        public string Inicio {
            get; private set;
        }
        
        /// <summary>
        /// El destino del viaje.
        /// </summary>
        /// <value>El destino, como cadena.</value>
        public string Destino {
            get; private set;
        }
        
        /// <summary>
        /// El recorrido correspondiente a los kilómetros con ese inicio y final.
        /// </summary>
        /// <value>El objeto <see cref="Recorrido"/>.</value>
        public Recorrido Recorrido {
            get; private set;
        }
        
        /// <summary>El transporte a emplear en el recorrido.</summary>
        /// <value>El objeto <see cref="Autobus"/>.</value>
        public Autobus Autobus {
            get; private set;
        }
        
        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:Viajes.Core.Viaje"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:Viajes.Core.Viaje"/>.</returns>
        public override string ToString()
        {
            return string.Format(
                "Recorrido: de {0} a {1}, {2}\n\tAutobus:{3}\n",
                this.Inicio, this.Destino,
                this.Recorrido, this.Autobus );
        }
    }
}
