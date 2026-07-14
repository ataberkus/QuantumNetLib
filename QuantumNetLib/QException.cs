namespace QuantumNetLib
{
    public class QException : System.Exception
    {
        public QException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }

        public int ErrorCode { get; }

        public override string ToString()
        {
            return $"Error code: {ErrorCode}\nMessage: {Message}";
        }
    }
}
