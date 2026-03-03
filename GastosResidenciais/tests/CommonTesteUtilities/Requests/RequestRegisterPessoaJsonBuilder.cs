using Bogus;
using GastosResidenciais.Communication.Requests;

namespace CommonTesteUtilities.Requests
{
    public class RequestRegisterPessoaJsonBuilder
    {
        public static RequestPessoaJson Build()
        {
            return new Faker<RequestPessoaJson>()
                .RuleFor(r => r.Nome, faker => faker.Person.FullName)
                .RuleFor(r => r.Idade, faker => faker.Random.Int(1, 120));
        }
    }
}
