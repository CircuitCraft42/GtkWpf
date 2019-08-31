namespace System.Windows
{
    public class RoutedEventArgs : EventArgs
    {
        public RoutedEvent RoutedEvent { get; set; }
        public bool Handled { get; set; }
        public object Source { get; set; }
        public object OriginalSource { get; }

        public RoutedEventArgs()
        {
        }

        public RoutedEventArgs(RoutedEvent routedEvent)
        {
            RoutedEvent = routedEvent;
        }

        public RoutedEventArgs(RoutedEvent routedEvent, object source) : this(routedEvent)
        {
            Source = source;
        }
    }
}