using System;
using System.Collections.Generic;
using System.Text;

namespace System.Windows
{
    public enum ShutdownMode
    {
        OnLastWindowClose = 0,
        OnMainWindowClose = 1,
        OnExplicitShutdown = 2
    }
}
