using Simple.PersonModel.PersonModels;
using System;

namespace Simple.Core
{
    public interface ICoreService
    {
        Person AddPlayer(string name, PersonRole role, decimal amount);
        string GetAccountReportByPerson(Person person);

    }
}