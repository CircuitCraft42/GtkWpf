using System.Collections;
using System.Collections.Generic;
using System.Security;

namespace System.Windows
{
    public class Application
    {
        private static Application _Current;
        public static Application Current { get => _Current; }

        [SecurityCritical]
        public Application()
        {

            if(_Current != null)
            {
                throw new InvalidOperationException();
            }

            _Current = this;

            Gtk.Application.Init();

            OnStartup(StartupEventArgs.Inst);

        }

        internal int ExitCode = 0;

        protected virtual void OnStartup(StartupEventArgs e) => Startup?.Invoke(this, e);

        public Window MainWindow
        {
            get => Windows.MainWindow;
            set => Windows.MainWindow = value;
        }
        public IDictionary Properties { get; } = new Dictionary<string, object>();
        public WindowCollection Windows = new WindowCollection();

        public event ExitEventHandler Exit;

        protected virtual void OnExit(ExitEventArgs e)
        {
            Exit?.Invoke(this, e);
            ExitCode = e.ApplicationExitCode;
        }

        public int Run()
        {
            Gtk.Application.Run();
            return ExitCode;
        }

        public event StartupEventHandler Startup;

        public void Shutdown()
        {
            Shutdown(0);
        }

        [SecurityCritical]
        public void Shutdown(int exitCode)
        {
            OnExit(new ExitEventArgs()
            {
                ApplicationExitCode = exitCode
            });
        }

        public ShutdownMode ShutdownMode { get; set; } = ShutdownMode.OnLastWindowClose;
    }
}
