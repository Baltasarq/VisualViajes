namespace Viajes.Core {
    using System.IO;
    using System.Text;
    using System.Xml;
    using System.Collections;
    using System.Xml.Linq;
	using System.Collections.Generic;
    
	public class RegistroViajes: ICollection<Viaje> {
		public const string ArchivoXml = "viajes.xml";
		public const string EtqViajes = "viajes";
		public const string EtqViaje = "viaje";
		public const string EtqInicio = "inicio";
		public const string EtqDestino = "destino";
		public const string EtqKms = "kms";

        /// <summary>
        /// Crea un <see cref="T:Viajes.Core.RegistroViajes"/> sin viajes.
        /// </summary>
		public RegistroViajes()
		{
			this.viajes = new List<Viaje>();
		}
        
        /// <summary>
        /// Crea un <see cref="T:Viajes.Core.RegistroViajes"/>
        /// con los viajes dados.
        /// </summary>
        /// <param name="viajes">Varios <see cref="Viajes"/> de partida.</param>
        public RegistroViajes(IEnumerable<Viaje> viajes)
            :this()
        {
            this.viajes.AddRange( viajes );
        }

        /// <summary>
        /// Inserta un viaje dado al final de la lista.
        /// </summary>
        /// <param name="v">Un objeto <see cref="Viaje"/>.</param>
		public void Add(Viaje v)
		{
			this.viajes.Add( v );
		}

        /// <summary>
        /// Elimina un viaje dado.
        /// </summary>
        /// <returns>True si se ha eliminado, False en otro caso.</returns>
        /// <param name="v">El <see cref="Viaje"/> a eliminar.</param>
		public bool Remove(Viaje v)
		{
			return this.viajes.Remove( v );
		}

        /// <summary>
        /// Elimina un viaje en la pos. i.
        /// </summary>
        /// <param name="i">La pos. a eliminar</param>
		public void RemoveAt(int i)
		{
			this.viajes.RemoveAt( i );
		}

        /// <summary>
        /// Inserta al final varios viajes dados.
        /// </summary>
        /// <param name="vs">Los <see cref="Viaje"/>s a insertar.</param>
		public void AddRange(IEnumerable<Viaje> vs)
		{
			this.viajes.AddRange( vs );
		}

        /// <summary>
        /// Devuelve el total de viajes guardados en este registro.
        /// </summary>
        /// <value>El total de viajes, como entero.</value>
		public int Count {
			get { return this.viajes.Count; }
		}

        /// <summary>
        /// Retorna False, pues este registro no es de solo lectura.
        /// </summary>
        /// <value><c>false</c>.</value>
		public bool IsReadOnly {
			get { return false; }
		}

        /// <summary>
        /// Elimina todos los viajes almacenados.
        /// </summary>
		public void Clear()
		{
			this.viajes.Clear();
		}

        /// <summary>
        /// Comprueba si el viaje dado se encuentra guardado.
        /// </summary>
        /// <returns>True si se encuentra, False en otro caso.</returns>
        /// <param name="v">El <see cref="Viaje"/> a comprobar.</param>
		public bool Contains(Viaje v)
		{
			return this.viajes.Contains( v );
		}

        /// <summary>
        /// Copia los viajes a partir de la pos. dada a un vector.
        /// </summary>
        /// <param name="v">El vector al que copiar los viajes.</param>
        /// <param name="i">La pos. desde la que copiar.</param>
		public void CopyTo(Viaje[] v, int i)
		{
			this.viajes.CopyTo( v, i );
		}

		// Enumerador aplicado a Viaje.
		IEnumerator<Viaje> IEnumerable<Viaje>.GetEnumerator()
	 	{
	 		foreach(var v in this.viajes) {
	 			yield return v;
	 		}
	 	}
	 			
	 	// Enumerador sin tipo
	 	IEnumerator IEnumerable.GetEnumerator()
	 	{
	 			foreach(var v in this.viajes) {
	 				yield return v;
	 			}
	 	}

		// Indizador
		public Viaje this[int i] {
			get { return this.viajes[ i ]; }
			set { this.viajes[ i ] = value; }
		}

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:Viajes.Core.RegistroViajes"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:Viajes.Core.RegistroViajes"/>.</returns>
		public override string ToString()
		{
			var toret = new StringBuilder();

			foreach(Viaje v in this.viajes) {
				toret.AppendLine( v.ToString() );
			}

			return toret.ToString();
		}
        
        /// <summary>
        /// Guarda la lista de viajes como xml.
        /// </summary>
        public void GuardaXml()
        {
            this.GuardaXml( ArchivoXml );
        }
        
        /// <summary>
        /// Guarda la lista de viajes como XML.
        /// <param name="nf">El nombre del archivo.</param>
        /// </summary>
        public void GuardaXml(string nf)
        {
            var doc = new XDocument();
            var root = new XElement( EtqViajes );
            
            foreach(Viaje viaje in this.viajes) {
                root.Add(
                    new XElement( EtqViaje,
                            new XAttribute( EtqInicio, viaje.Inicio ),
                            new XAttribute( EtqDestino, viaje.Destino ),
                            new XElement( EtqKms, viaje.Kms.ToString() ) ) );
            }
            
            doc.Save( nf );
        }

		public static RegistroViajes RecuperaXml(string f)
		{
			var toret = new RegistroViajes();
            
            try {
                var doc = XDocument.Load( f );
                
                if ( doc.Root != null
                  && doc.Root.Name == EtqViajes )
                {
                    var viajes = doc.Root.Elements( EtqViaje );
                    
                    foreach(XElement viajeXml in viajes) {
                        toret.Add( new Viaje(
                            (string) viajeXml.Attribute( EtqInicio ),
                            (string) viajeXml.Attribute( EtqDestino ),
                            (int) viajeXml.Element( EtqKms ) ) );
                    }
                }
            }
            catch(XmlException)
            {
                toret.Clear();
            }
            catch(IOException)
            {
                toret.Clear();
            }
                
            return toret;
        }

        /// <summary>
        /// Crea un registro de viajes con la lista de viajes recuperada
        /// del archivo por defecto.
        /// </summary>
        /// <returns>Un <see cref="RegistroViajes"/>.</returns>
		public static RegistroViajes RecuperaXml()
		{
			return RecuperaXml( ArchivoXml );
		}

		private List<Viaje> viajes;
	}
}
