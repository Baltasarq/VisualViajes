namespace VisualViajes.View {
    using System;
	using System.Windows.Forms;
    
    using Core;

	public partial class MainWindow: Form {
		public const int ColNum = 0;
		public const int ColKms = 1;
		public const int ColOrg = 2;
		public const int ColDest = 3;
        public const int ColHoraLLegada = 4;

		public MainWindow()
		{
			this.Build();
			this.viajes = RegistroViajes.RecuperaXml();
		}
        
		private void Salir()
		{
			this.viajes.GuardaXml();
			this.Dispose( true );
		}

		private void Actualiza()
		{
            DateTime ahora = DateTime.Now;
            
			this.sbStatus.Text = "Viajes: " + this.viajes.Count.ToString()
                            + " | " + ahora.ToShortDateString()
                            + " | " + ahora.ToShortTimeString();

			this.ActualizaLista( 0 );
		}

		private void Inserta()
		{
            var dlgInserta = new DlgInserta();
            
			if ( dlgInserta.ShowDialog() == DialogResult.OK ) {
				this.viajes.Add( new Viaje( dlgInserta.CiudadOrigen,
			                                    dlgInserta.CiudadDestino,
			                                    dlgInserta.Kms ) );

				this.Actualiza();
			}

			return;
		}

		private void ActualizaLista(int numRow)
		{
            int numRecorridos = this.viajes.Count;
            
			// Crea y actualiza filas
			for (int i = numRow; i < numRecorridos; ++i) {
				if ( this.grdLista.Rows.Count <= i ) {
					this.grdLista.Rows.Add();
				}

				this.ActualizaFilaDeLista( i );
			}

			// Eliminar filas sobrantes
			int numExtra = this.grdLista.Rows.Count - numRecorridos;
			for(; numExtra > 0 ; --numExtra) {
			    this.grdLista.Rows.RemoveAt( numRecorridos );
			}

			return;
		}

		private void ActualizaFilaDeLista(int rowIndex)
		{
			if ( rowIndex < 0
			  || rowIndex > this.grdLista.Rows.Count )
			{
				throw new System.ArgumentOutOfRangeException(
                            "fila fuera de rango: " + nameof( rowIndex ) );
			}

			DataGridViewRow row = this.grdLista.Rows[ rowIndex ];
			Viaje viaje = this.viajes[ rowIndex ];

            // Assign data
            DateTime llegada = viaje.CalculaHoraLlegada();
			row.Cells[ ColNum ].Value = ( rowIndex + 1 ).ToString().PadLeft( 4, ' ' );
			row.Cells[ ColKms ].Value = viaje.Kms;
			row.Cells[ ColOrg ].Value = viaje.Inicio;
			row.Cells[ ColDest ].Value = viaje.Destino;
            row.Cells[ ColHoraLLegada ].Value =
				                    llegada.ToShortDateString()
				                    + " " + llegada.ToShortTimeString();
            
            // Assign tooltip text
            foreach(DataGridViewCell cell in row.Cells) {
                cell.ToolTipText = viaje.ToString();
            }

			return;
		}
        
        private void FilaSeleccionada()
        {
            int fila = System.Math.Max( 0, this.grdLista.CurrentRow.Index );
            
            if ( this.viajes.Count > fila ) {
	            this.edDetalle.Text = this.viajes[ fila ].ToString();
	            this.edDetalle.SelectionStart = this.edDetalle.Text.Length;
	            this.edDetalle.SelectionLength = 0;
            } else {
                this.edDetalle.Clear();
            }
            
            return;
        }

		private RegistroViajes viajes;
	}
}
