using System.Net;

namespace GastosResidenciais.Exception.ExceptionsBase
{
    public class NotFoundException : GastosResidenciaisException
    {
        public NotFoundException(string message) : base(message) { }

        public override int StatusCode => (int)HttpStatusCode.NotFound;

        public override List<string> GetErrors()
        {
            return [Message];
        }
    }
}
