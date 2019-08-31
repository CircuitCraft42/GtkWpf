namespace System.Windows
{
    public class ExitEventArgs : EventArgs
    {
        internal ExitEventArgs() { }
        public int ApplicationExitCode { get; set; }
    }
}