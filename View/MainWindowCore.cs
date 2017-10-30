namespace Viajes.View {
	using System.Windows.Forms;
    
    using Core;

	public partial class MainWindow: Form {
		public const int ColNum = 0;
		public const int ColKms = 1;
		public const int ColOrg = 2;
		public const int ColDest = 3;
        public const int ColBus = 4;

		public MainWindow()
		{
			this.Build();
			this.recorridos = RegistroViajes.RecuperaXml();
			this.Actualiza();
		}
        
		private void Salir()
		{
			this.recorridos.GuardaXml();
			this.Dispose( true );
		}

		private void Actualiza()
		{
			this.sbStatus.Text = "Viajes: " + this.recorridos.Count.ToString();
			this.ActualizaLista( 0 );
		}

		private void Inserta()
		{
            var dlgInserta = new DlgInserta();
            
			if ( dlgInserta.ShowDialog() == DialogResult.OK ) {
				this.recorridos.Add( new Viaje( dlgInserta.CiudadOrigen,
			                                    dlgInserta.CiudadDestino,
			                                    dlgInserta.Kms ) );

				this.Actualiza();
			}

			return;
		}

		private void ActualizaLista(int numRow)
		{
            int numRecorridos = this.recorridos.Count;
            
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
			Viaje viaje = this.recorridos[ rowIndex ];

            // Assign data
			row.Cells[ ColNum ].Value = ( rowIndex + 1 ).ToString().PadLeft( 4, ' ' );
			row.Cells[ ColKms ].Value = viaje.Kms;
			row.Cells[ ColOrg ].Value = viaje.Inicio;
			row.Cells[ ColDest ].Value = viaje.Destino;
            row.Cells[ ColBus ].Value = viaje.Autobus;
            
            // Assign tooltip text
            foreach(DataGridViewCell cell in row.Cells) {
                cell.ToolTipText = viaje.ToString();
            }

			return;
		}

		private RegistroViajes recorridos;
	}
}
