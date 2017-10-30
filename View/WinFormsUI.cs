namespace Viajes.View {
    using System.Windows.Forms;

	class WinFormsUI {
		public static void MainLoop(string[] args)
		{
			var f = new MainWindow();

			Application.Run( f );
		}
	}
}
