using System.Collections;

namespace System.Windows
{
    public class WindowCollection : ICollection
    {
        private readonly IList Windows = new ArrayList();

        public int    Count          => Windows.Count;
        public bool   IsSynchronized => false;
        public Window this[int item] => (Window)Windows[item];
        public object SyncRoot       => Windows.SyncRoot;

        public void CopyTo(Array array,    int index) => Windows.CopyTo(array, index);
        public void CopyTo(Window[] array, int index) => CopyTo(array, index);

        public IEnumerator GetEnumerator() => Windows.GetEnumerator();

        private int MainWindowIndex = 0;

        internal void AddWindow(Window window) {
            Windows.Add(window);
            window.Closed += Window_Closed;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Windows.Remove(sender);
            Application current = Application.Current;
            ShutdownMode mode = current.ShutdownMode;
            if (mode == ShutdownMode.OnExplicitShutdown) return;
            if (mode == ShutdownMode.OnLastWindowClose)
            {
                if (Count == 0) current.Shutdown();
                return;
            }
            if (mode == ShutdownMode.OnMainWindowClose
                && MainWindow == sender)
            {
                current.Shutdown();
            }
        }

        internal Window MainWindow
        {
            get => (Window)Windows[MainWindowIndex];
            set => MainWindowIndex = Windows.IndexOf(value);
        }
    }
}
