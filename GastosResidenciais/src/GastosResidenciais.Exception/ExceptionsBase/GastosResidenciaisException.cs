namespace GastosResidenciais.Exception.ExceptionsBase
{
    public abstract class GastosResidenciaisException : SystemException
    {
        protected GastosResidenciaisException(string message) : base(message) { }

        public abstract int StatusCode { get; }
        public abstract List<string> GetErrors();
    }
}
