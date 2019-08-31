using System;

namespace System.Windows
{
    public class StartupEventArgs : EventArgs
    {
        internal readonly static StartupEventArgs Inst = new StartupEventArgs();

        private StartupEventArgs() { }
        public string[] Args { get => Environment.GetCommandLineArgs(); }
    }
}
