namespace QuantumNetLib
{
    public class Exception : System.Exception
    {
        public Exception(string message, int errorCode) : base(message)
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
