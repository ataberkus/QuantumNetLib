namespace QuantumNetLib
{
    [System.Serializable]
    public class QException : System.Exception
    {
        public QException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }

        public QException(string message) : base(message)
        {
            ErrorCode = 0;
        }

        public QException(string message, int errorCode, System.Exception innerException)
            : base(message, innerException)
        {
            ErrorCode = errorCode;
        }

        protected QException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
            ErrorCode = info != null ? info.GetInt32(nameof(ErrorCode)) : 0;
        }

        public int ErrorCode { get; }

        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            base.GetObjectData(info, context);
            if (info != null) info.AddValue(nameof(ErrorCode), ErrorCode);
        }

        public override string ToString()
        {
            return $"Error code: {ErrorCode}\n{base.ToString()}";
        }
    }
}
