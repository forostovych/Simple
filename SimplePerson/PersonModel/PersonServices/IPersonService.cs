using Simple.PersonModel.PersonModels;

namespace Simple.PersonModel.PersonServices
{
    public interface IPersonService
    {

        Person CreatePerson(string name, PersonRole role);

        string GetPersonReport(Person person);

    }
}
