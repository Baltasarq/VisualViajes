using System;
using System.Drawing;
using System.Windows.Forms;

using Viajes;

namespace VisualViaje {
	public class MainWindow: Form {
		public const int ColNum = 0;
		public const int ColKms = 1;
		public const int ColOrg = 2;
		public const int ColDest = 3;

		public MainWindow()
		{
			this.BuildGui();
			this.recorridos = Recorridos.Crea();
			this.Actualiza();
		}

		private void BuildMenu()
		{
			this.mPpal = new MainMenu();

			this.mArchivo = new MenuItem( "&Archivo" );
			this.mEditar = new MenuItem( "&Editar" );

			this.opSalir = new MenuItem( "&Salir" );
			this.opSalir.Shortcut = Shortcut.CtrlQ;
			this.opSalir.Click += (sender, e) => this.Salir();

			this.opInsertar = new MenuItem( "&Insertar" );
			this.opInsertar.Shortcut = Shortcut.CtrlIns;
			this.opInsertar.Click += (sender, e) => this.Inserta();

			this.mArchivo.MenuItems.Add( this.opSalir );
			this.mEditar.MenuItems.Add( this.opInsertar );

			this.mPpal.MenuItems.Add( this.mArchivo );
			this.mPpal.MenuItems.Add( this.mEditar );
			this.Menu = mPpal;
		}

		private void BuildPanelLista()
		{
			this.pnlLista = new Panel();
			this.pnlLista.SuspendLayout();
			this.pnlLista.Dock = DockStyle.Fill;
			this.pnlPpal.Controls.Add( this.pnlLista );

			// Crear gridview
			this.grdLista = new DataGridView();
			this.grdLista.Dock = DockStyle.Fill;
			this.grdLista.AllowUserToResizeRows = false;
			this.grdLista.RowHeadersVisible = false;
			this.grdLista.AutoGenerateColumns = false;
			this.grdLista.MultiSelect = false;
			this.grdLista.AllowUserToAddRows = false;
			var textCellTemplate0 = new DataGridViewTextBoxCell();
			var textCellTemplate1 = new DataGridViewTextBoxCell();
			var textCellTemplate2 = new DataGridViewTextBoxCell();
			var textCellTemplate3 = new DataGridViewTextBoxCell();
			textCellTemplate0.Style.BackColor = this.grdLista.RowHeadersDefaultCellStyle.BackColor;
			textCellTemplate1.Style.BackColor = Color.Wheat;
			textCellTemplate1.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
			textCellTemplate2.Style.BackColor = Color.Wheat;
			textCellTemplate3.Style.BackColor = Color.Wheat;
			var column0 = new DataGridViewTextBoxColumn();
			var column1 = new DataGridViewTextBoxColumn();
			var column2 = new DataGridViewTextBoxColumn();
			var column3 = new DataGridViewTextBoxColumn();
			column0.SortMode = DataGridViewColumnSortMode.NotSortable;
			column1.SortMode = DataGridViewColumnSortMode.NotSortable;
			column2.SortMode = DataGridViewColumnSortMode.NotSortable;
			column3.SortMode = DataGridViewColumnSortMode.NotSortable;
			column0.CellTemplate = textCellTemplate0;
			column1.CellTemplate = textCellTemplate1;
			column2.CellTemplate = textCellTemplate2;
			column3.CellTemplate = textCellTemplate3;
			column0.HeaderText = "#";
			column0.Width = 50;
			column0.ReadOnly = true;
			column1.HeaderText = "Kms";
			column1.Width = 50;
			column1.ReadOnly = true;
			column2.HeaderText = "Origen";
			column2.Width = 100;
			column2.ReadOnly = true;
			column3.HeaderText = "Destino";
			column3.Width = 100;
			column3.ReadOnly = true;

			this.grdLista.Columns.AddRange( new DataGridViewColumn[] {
				column0, column1, column2, column3
			} );


			this.pnlLista.Controls.Add( this.grdLista );
			this.pnlLista.ResumeLayout( false );
		}

		private void BuildDialogInserta()
		{
			this.dlgInserta = new Form();
			this.dlgInserta.SuspendLayout();

			var pnlInserta = new TableLayoutPanel();
			pnlInserta.Dock = DockStyle.Fill;
			pnlInserta.SuspendLayout();
			this.dlgInserta.Controls.Add( pnlInserta );

			var pnlCiudadOrigen = new Panel();
			this.tbCiudadOrigen = new TextBox();
			this.tbCiudadOrigen.Dock = DockStyle.Fill;
			var lbCiudadOrigen = new Label();
			lbCiudadOrigen.Text = "Ciudad origen:";
			lbCiudadOrigen.Dock = DockStyle.Left;
			pnlCiudadOrigen.Controls.Add( this.tbCiudadOrigen );
			pnlCiudadOrigen.Controls.Add( lbCiudadOrigen );
			pnlCiudadOrigen.Dock = DockStyle.Top;
			pnlCiudadOrigen.MaximumSize = new Size( int.MaxValue, tbCiudadOrigen.Height * 2 );
			pnlInserta.Controls.Add( pnlCiudadOrigen );

			var pnlCiudadDestino = new Panel();
			this.tbCiudadDestino = new TextBox();
			this.tbCiudadDestino.Dock = DockStyle.Fill;
			var lbCiudadDestino = new Label();
			lbCiudadDestino.Text = "Ciudad destino:";
			lbCiudadDestino.Dock = DockStyle.Left;
			pnlCiudadDestino.Controls.Add( this.tbCiudadDestino );
			pnlCiudadDestino.Controls.Add( lbCiudadDestino );
			pnlCiudadDestino.Dock = DockStyle.Top;
			pnlCiudadDestino.MaximumSize = new Size( int.MaxValue, tbCiudadDestino.Height * 2 );
			pnlInserta.Controls.Add( pnlCiudadDestino );

			var pnlKms = new Panel();
			this.tbKms = new TextBox();
			this.tbKms.Text = "0";
			this.tbKms.TextAlign = HorizontalAlignment.Right;
			this.tbKms.Dock = DockStyle.Fill;
			var lbKms = new Label();
			lbKms.Text = "Kms:";
			lbKms.Dock = DockStyle.Left;
			pnlKms.Controls.Add( this.tbKms );
			pnlKms.Controls.Add( lbKms );
			pnlKms.Dock = DockStyle.Top;
			pnlKms.MaximumSize = new Size( int.MaxValue, tbKms.Height * 2 );
			pnlInserta.Controls.Add( pnlKms );

			var pnlBotones = new TableLayoutPanel();
			pnlBotones.ColumnCount = 2;
			pnlBotones.RowCount = 1;
			var btCierra = new Button();
			btCierra.DialogResult = DialogResult.Cancel;
			btCierra.Text = "&Cancelar";
			var btGuarda = new Button();
			btGuarda.DialogResult = DialogResult.OK;
			btGuarda.Text = "&Guardar";
			pnlBotones.Controls.Add( btGuarda );
			pnlBotones.Controls.Add( btCierra );
			pnlBotones.Dock = DockStyle.Top;
			pnlInserta.Controls.Add( pnlBotones );

			this.dlgInserta.AcceptButton = btGuarda;
			this.dlgInserta.CancelButton = btCierra;
			pnlInserta.ResumeLayout( true );
			this.dlgInserta.ResumeLayout( false );
			this.dlgInserta.Text = "Nuevo recorrido";
			this.dlgInserta.Size = new Size( 400, 
			                pnlCiudadOrigen.Height + pnlCiudadDestino.Height
			                + pnlKms.Height + pnlBotones.Height );
			this.dlgInserta.FormBorderStyle = FormBorderStyle.FixedDialog;
			this.dlgInserta.MinimizeBox = false;
			this.dlgInserta.MaximizeBox = false;
			this.dlgInserta.StartPosition = FormStartPosition.CenterParent;
		}

		private void BuildStatus()
		{
			this.sbStatus = new StatusBar();
			this.sbStatus.Dock = DockStyle.Bottom;
			this.Controls.Add( this.sbStatus );
		}

		private void BuildGui()
		{
			this.SuspendLayout();

			this.pnlPpal = new Panel();
			this.pnlPpal.SuspendLayout();
			this.pnlPpal.Dock = DockStyle.Fill;
			this.Controls.Add( this.pnlPpal );

			this.BuildStatus();
			this.BuildMenu();
			this.BuildPanelLista();
			this.BuildDialogInserta();

			this.MinimumSize = new Size( 320, 200 );
			this.pnlPpal.ResumeLayout( false );
			this.ResumeLayout( true );
			this.Resize += (obj, e) => this.ResizeWindow();
			this.Text = "Visual Viajes";
			this.ResizeWindow();
			this.Closed += (sender, e) => this.Salir();
		}

		private void ResizeWindow()
		{
			// Tomar las nuevas medidas
			int width = this.pnlPpal.ClientRectangle.Width;
			
			// Redimensionar la tabla
			this.grdLista.Width = width;

			this.grdLista.Columns[ ColNum ].Width =
										(int) Math.Floor( width *.10 );	// Num
			this.grdLista.Columns[ ColKms ].Width =
										(int) Math.Floor( width *.10 );	// Kms
			this.grdLista.Columns[ ColOrg ].Width =
										(int) Math.Floor( width *.40 ); // Org
			this.grdLista.Columns[ ColDest ].Width =
										(int) Math.Floor( width *.40 ); // Dest
		}

		private void Salir()
		{
			this.recorridos.GuardaXml();
			this.Dispose( true );
		}

		private void Actualiza()
		{
			this.sbStatus.Text = "Recorridos: " + this.recorridos.Count.ToString();
			this.ActualizaLista( 0 );
		}

		private void Inserta()
		{
			this.tbCiudadDestino.Text = "";
			this.tbCiudadOrigen.Text = "";
			this.tbKms.Text = "0";

			if ( this.dlgInserta.ShowDialog() == DialogResult.OK ) {
				this.recorridos.Add( Recorrido.Crea( this.tbCiudadOrigen.Text,
			                                    this.tbCiudadDestino.Text,
			                                    Convert.ToInt32( this.tbKms.Text ) ) );

				this.Actualiza();
			}

			return;
		}

		private void ActualizaLista(int numRow)
		{
			// Crea y actualiza filas
			for (int i = numRow; i < this.recorridos.Count; ++i) {
				if ( this.grdLista.Rows.Count <= i ) {
					this.grdLista.Rows.Add();
				}

				this.ActualizaFilaDeLista( i );
			}

			// Eliminar filas sobrantes
			int numExtra = this.grdLista.Rows.Count - this.recorridos.Count;
			for(; numExtra > 0 ; --numExtra) {
			    this.grdLista.Rows.RemoveAt( this.recorridos.Count );
			}

			return;
		}

		private void ActualizaFilaDeLista(int rowIndex)
		{
			if ( rowIndex < 0
			  || rowIndex > this.grdLista.Rows.Count )
			{
				throw new ArgumentOutOfRangeException( "fila fuera de rango: " + rowIndex );
			}

			DataGridViewRow row = this.grdLista.Rows[ rowIndex ];
			Recorrido recorrido = this.recorridos[ rowIndex ];

			row.Cells[ ColNum ].Value = ( rowIndex + 1 ).ToString().PadLeft( 4, ' ' );
			row.Cells[ ColKms ].Value = recorrido.Kms;
			row.Cells[ ColOrg ].Value = recorrido.Inicio;
			row.Cells[ ColDest ].Value = recorrido.Destino;

			return;
		}

		private MainMenu mPpal;
		private MenuItem mArchivo;
		private MenuItem mEditar;
		private MenuItem opSalir;
		private MenuItem opInsertar;
		private StatusBar sbStatus;
		private Panel pnlLista;
		private Panel pnlPpal;
		private TextBox tbCiudadOrigen;
		private TextBox tbCiudadDestino;
		private TextBox tbKms;
		private DataGridView grdLista;
		private Form dlgInserta;

		private Recorridos recorridos;
	}
}

