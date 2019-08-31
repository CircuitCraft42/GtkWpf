
namespace System.Windows
{
    public class UIElement
    {
        internal virtual Gtk.Widget Impl => throw new NotImplementedException();
    }
}
