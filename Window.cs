using System.ComponentModel;
using System.Security;
using Gdk;

namespace System.Windows
{
    public class Window : UIElement
    {
        internal override Gtk.Widget Impl => Self;
        private readonly Gtk.Window Self = new Gtk.Window(Gtk.WindowType.Toplevel);

        [SecurityCritical]
        public Window()
        {
            Self.DeleteEvent += delegate(object sender, Gtk.DeleteEventArgs args)
            {
                CancelEventArgs cancel = new CancelEventArgs();
                OnClosing(cancel);
                args.RetVal = (bool)args.RetVal | cancel.Cancel;
            };

            Self.DestroyEvent += delegate(object sender, Gtk.DestroyEventArgs args)
            {
                OnClosed(new EventArgs());
            };


            Application.Current.Windows.AddWindow(this);
            OnInitialized(new EventArgs());
        }

        public bool AllowsTransparency => false;

        public Pixbuf GdkIcon
        {
            get => Self.Icon;
            set => Self.Icon = value;
        }

        public event EventHandler Initialized;
        protected virtual void OnInitialized(EventArgs e) => Initialized?.Invoke(this, e);

        public event CancelEventHandler Closing;
        protected virtual void OnClosing(CancelEventArgs e) => Closing?.Invoke(this, e);

        public event EventHandler Closed;
        protected virtual void OnClosed(EventArgs e) => Closed?.Invoke(this, e);

        public void Show() => Self.ShowAll();

        private object content;
        public object Content
        {
            get => content;
            set
            {
                content = value;
                UpdateContent(value);
            }
        }

        private void UpdateContent(object value)
        {
            switch (value)
            {
                case UIElement element:
                    Self.Child = element.Impl;
                    break;
                case string str:
                    Self.Child = new Gtk.Label(str);
                    break;

            }
        }

        [SecurityCritical]
        public void Close()
        {
            CancelEventArgs cancel = new CancelEventArgs();
            OnClosing(cancel);
            if(cancel.Cancel) return;
            Self.Destroy();
        }
    }
}
