namespace Viajes.View {
    using System.Drawing;
    using System.Windows.Forms;
    
    /// <summary>
    /// Dlg para insertar los datos de un viaje.
    /// </summary>
    public class DlgInserta: Form {
        /// <summary>
        /// Inicializa una nueva instancia de este dlg.
        /// </summary>
        public DlgInserta()
        {
            this.Build();
        }
        
        private Panel BuildPnlKms()
        {
            var toret = new Panel();
            this.edKms = new NumericUpDown {
                Value = 0,
                TextAlign = HorizontalAlignment.Right,
                Dock = DockStyle.Fill
            };

            var lbKms = new Label {
                Text = "Kms:",
                Dock = DockStyle.Left
            };
            
            toret.Controls.Add( this.edKms );
            toret.Controls.Add( lbKms );
            toret.Dock = DockStyle.Top;
            toret.MaximumSize = new Size( int.MaxValue, edKms.Height * 2 );
            
            return toret;
        }
        
        private Panel BuildPnlCiudadOrigen()
        {
            var toret = new Panel { Dock = DockStyle.Top };
            this.edCiudadOrigen = new TextBox { Dock = DockStyle.Fill };
            var lbCiudadOrigen = new Label {
                Text = "Ciudad origen:",
                Dock = DockStyle.Left
            };
            
            toret.Controls.Add( this.edCiudadOrigen );
            toret.Controls.Add( lbCiudadOrigen );
            toret.MaximumSize = new Size( int.MaxValue, edCiudadOrigen.Height * 2 );
            
            this.edCiudadOrigen.Validating += (sender, cancelArgs) => {
                var btAccept = (Button) this.AcceptButton;
                bool invalid = string.IsNullOrWhiteSpace( this.CiudadOrigen );
                
                invalid = invalid || !char.IsLetter( this.CiudadOrigen[ 0 ] );
                
                if ( invalid ) {
                    this.edCiudadOrigen.Text = "¿Ciudad de origen?";
                }
                
                btAccept.Enabled = !invalid;
                cancelArgs.Cancel = invalid;
            };
            
            return toret;
        }
        
        private Panel BuildPnlCiudadDestino()
        {
            var toret = new Panel();
            this.edCiudadDestino = new TextBox { Dock = DockStyle.Fill };
            var lbCiudadDestino = new Label()
            {
                Text = "Ciudad destino:",
                Dock = DockStyle.Left
            };
            
            toret.Controls.Add( this.edCiudadDestino );
            toret.Controls.Add( lbCiudadDestino );
            toret.Dock = DockStyle.Top;
            toret.MaximumSize = new Size( int.MaxValue, edCiudadDestino.Height * 2 );        
            
            this.edCiudadDestino.Validating += (sender, cancelArgs) => {
                var btAccept = (Button) this.AcceptButton;
                bool invalid = string.IsNullOrWhiteSpace( this.CiudadDestino );
                
                invalid = invalid || !char.IsLetter( this.CiudadDestino[ 0 ] );

                if ( invalid ) {
                    this.edCiudadDestino.Text = "¿Ciudad de destino?";
                }
                
                btAccept.Enabled = !invalid;
                cancelArgs.Cancel = invalid;
            };
            
            return toret;
        }
        
        private Panel BuildPnlBotones()
        {
            var toret = new TableLayoutPanel()
            {
                ColumnCount = 2,
                RowCount = 1
            };

            var btCierra = new Button()
            {
                DialogResult = DialogResult.Cancel,
                Text = "&Cancelar"
            };

            var btGuarda = new Button()
            {
                DialogResult = DialogResult.OK,
                Text = "&Guardar"
            };
            
            this.AcceptButton = btGuarda;
            this.CancelButton = btCierra;

            toret.Controls.Add( btGuarda );
            toret.Controls.Add( btCierra );
            toret.Dock = DockStyle.Top;
            
            return toret;
        }

        private void Build()
        {
            this.SuspendLayout();

            var pnlInserta = new TableLayoutPanel { Dock = DockStyle.Fill };
            pnlInserta.SuspendLayout();
            this.Controls.Add( pnlInserta );

            var pnlCiudadOrigen = this.BuildPnlCiudadOrigen();
            pnlInserta.Controls.Add( pnlCiudadOrigen );

            var pnlCiudadDestino = this.BuildPnlCiudadDestino();
            pnlInserta.Controls.Add(pnlCiudadDestino);

            var pnlKms = this.BuildPnlKms();
            pnlInserta.Controls.Add( pnlKms );

            
            var pnlBotones = this.BuildPnlBotones();
            pnlInserta.Controls.Add( pnlBotones );

            pnlInserta.ResumeLayout( true );
            
            this.Text = "Nuevo viaje";
            this.Size = new Size( 400, 
                            pnlCiudadOrigen.Height + pnlCiudadDestino.Height
                            + pnlKms.Height + pnlBotones.Height );
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.ResumeLayout( false );
        }
        
        /// <summary>
        /// Valida los datos en el dlg.
        /// </summary>
        /// <param name="e">Los params. del evento.</param>
        private void OnValidate(System.ComponentModel.CancelEventArgs e)
        {
            bool toret = string.IsNullOrWhiteSpace( this.CiudadOrigen );
            
            toret = toret || string.IsNullOrWhiteSpace( this.CiudadDestino );
            toret = toret || double.TryParse( this.edKms.Text, out double res );
            
            e.Cancel = toret;
        }

        /// <summary>
        /// Obtiene la ciudad origen introducida por el usuario.
        /// </summary>
        /// <value>Una cadena de caracteres con la ciudad de origen.</value>
        public string CiudadOrigen => this.edCiudadOrigen.Text;

        /// <summary>
        /// Obtiene la ciudad destino introducida por el usuario.
        /// </summary>
        /// <value>Una cadena de caracteres con la ciudad de destino.</value>
        public string CiudadDestino => this.edCiudadDestino.Text;

        /// <summary>
        /// Obtiene los kms. introducidos por el usuario.
        /// </summary>
        /// <value>Los kms., como entero.</value>
        public double Kms => System.Convert.ToDouble( this.edKms.Value );

        private TextBox edCiudadOrigen;
        private TextBox edCiudadDestino;
        private NumericUpDown edKms;
    }    
}
