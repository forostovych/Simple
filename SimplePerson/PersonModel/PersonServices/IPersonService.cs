using Simple.PersonModel.PersonModels;

namespace Simple.PersonModel.PersonServices
{
    public interface IPersonService
    {

        IPerson CreatePerson(string name, PersonRole role);

        string GetPersonReport(IPerson person);

    }
}
