namespace System.Windows.Controls
{

    public class Button : UIElement
    {

        internal override Gtk.Widget Impl => Self;
        private readonly Gtk.Button Self = new Gtk.Button();

        private object content;

        public Button()
        {
            Self.Clicked += (sender, e) => OnClick(new RoutedEventArgs(new RoutedEvent(), this));
        }

        public object Content
        {
            get => content;
            set {
                content = value;
                UpdateContent();
            }
        }

        private void UpdateContent()
        {
            switch (content)
            {
                case string _:
                    Self.Label = (string)content;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public event RoutedEventHandler Click;

        protected virtual void OnClick(RoutedEventArgs e)
        {
            Click?.Invoke(this, e);
        }
    }
}
