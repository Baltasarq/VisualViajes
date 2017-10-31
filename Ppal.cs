namespace VisualViajes {
    class Ppal {
        static void Main(string[] args)
        {
            if ( args.Length > 0
              && args[ 0 ] == "--gui" )
            {
                View.WinFormsUI.MainLoop( args );
            } else {
                View.ConsoleUI.MainLoop( args );
            }

            return;
        }
    }
}
