namespace Viajes.View {
    using System.Windows.Forms;
    using System.Drawing;
    
    public partial class MainWindow {
        private void BuildIcons()
        {
            var assembly = System.Reflection.Assembly.GetEntryAssembly();
            var resourceAppIcon = assembly.
                GetManifestResourceStream( "Viajes.Res.bus.png" );
                
            if ( resourceAppIcon != null ) {
                this.Icon = Icon.FromHandle(
                    new Bitmap( resourceAppIcon ).GetHicon() );
            }
            
            return;
        }

        private void BuildMenu()
        {
            this.mPpal = new MainMenu();

            this.mArchivo = new MenuItem( "&Archivo" );
            this.mEditar = new MenuItem( "&Editar" );

            this.opSalir = new MenuItem("&Salir") { Shortcut = Shortcut.CtrlQ };
            this.opSalir.Click += (sender, e) => this.Salir();

            this.opInsertar = new MenuItem( "&Insertar" ) {
                Shortcut = Shortcut.CtrlIns
            };
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
            this.grdLista = new DataGridView()
            {
                Dock = DockStyle.Fill,
                AllowUserToResizeRows = false,
                RowHeadersVisible = false,
                AutoGenerateColumns = false,
                MultiSelect = false,
                AllowUserToAddRows = false,
                EnableHeadersVisualStyles = false
            };
            
            this.grdLista.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            this.grdLista.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;
            
            var textCellTemplate0 = new DataGridViewTextBoxCell();
            var textCellTemplate1 = new DataGridViewTextBoxCell();
            var textCellTemplate2 = new DataGridViewTextBoxCell();
            var textCellTemplate3 = new DataGridViewTextBoxCell();
            var textCellTemplate4 = new DataGridViewTextBoxCell();
            textCellTemplate0.Style.BackColor = Color.LightGray;
            textCellTemplate0.Style.ForeColor = Color.Black;
            textCellTemplate1.Style.BackColor = Color.Wheat;
            textCellTemplate1.Style.ForeColor = Color.Black;
            textCellTemplate1.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            textCellTemplate2.Style.BackColor = Color.Wheat;
            textCellTemplate2.Style.ForeColor = Color.Black;
            textCellTemplate3.Style.BackColor = Color.Wheat;
            textCellTemplate3.Style.ForeColor = Color.Black;
            textCellTemplate4.Style.BackColor = Color.Wheat;
            textCellTemplate4.Style.ForeColor = Color.Black;
            
            var column0 = new DataGridViewTextBoxColumn {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate0,
                HeaderText = "#",
                Width = 50,
                ReadOnly = true
            };
            
            var column1 = new DataGridViewTextBoxColumn {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate1,
                HeaderText = "kms",
                Width = 50,
                ReadOnly = true
            };
            
            var column2 = new DataGridViewTextBoxColumn {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate2,
                HeaderText = "Origen",
                Width = 50,
                ReadOnly = true
            };
            
            var column3 = new DataGridViewTextBoxColumn {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate3,
                HeaderText = "Destino",
                Width = 50,
                ReadOnly = true
            };
            
            var column4 = new DataGridViewTextBoxColumn {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate4,
                HeaderText = "Bus",
                Width = 50,
                ReadOnly = true
            };
            
            this.grdLista.Columns.AddRange( new DataGridViewColumn[] {
                column0, column1, column2, column3, column4
            } );


            this.pnlLista.Controls.Add( this.grdLista );
            this.pnlLista.ResumeLayout( false );
        }
        
        private void BuildStatus()
        {
            this.sbStatus = new StatusBar { Dock = DockStyle.Bottom };
            this.Controls.Add( this.sbStatus );
        }

        private void Build()
        {
            this.SuspendLayout();

            this.pnlPpal = new Panel();
            this.pnlPpal.SuspendLayout();
            this.pnlPpal.Dock = DockStyle.Fill;
            this.Controls.Add( this.pnlPpal );

            this.BuildIcons();
            this.BuildStatus();
            this.BuildMenu();
            this.BuildPanelLista();

            this.MinimumSize = new Size( 600, 400 );
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
                                (int) System.Math.Floor( width *.05 ); // Num
            this.grdLista.Columns[ ColKms ].Width =
                                (int) System.Math.Floor( width *.10 ); // Kms
            this.grdLista.Columns[ ColOrg ].Width =
                                (int) System.Math.Floor( width *.30 ); // Org
            this.grdLista.Columns[ ColDest ].Width =
                                (int) System.Math.Floor( width *.30 ); // Dest
            this.grdLista.Columns[ ColBus ].Width =
                                (int) System.Math.Floor( width *.25 ); // Dest                                
        }
        
        private MainMenu mPpal;
        private MenuItem mArchivo;
        private MenuItem mEditar;
        private MenuItem opSalir;
        private MenuItem opInsertar;
        
        private StatusBar sbStatus;
        private Panel pnlLista;
        private Panel pnlPpal;
        private DataGridView grdLista;
    }
}
