namespace Israiloff.Mmvm.Net.Mvvm.Core.Services.Command.EventArgs
{
    public class EventArgs<T> : System.EventArgs
    {
        public EventArgs(T value)
        {
            Value = value;
        }

        public T Value { get; }
    }
}