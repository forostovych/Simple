using Simple.PersonModel.PersonModels;
using System;
using System.Data;

namespace Simple.Core
{
    public interface ICoreService
    {
        Person AddPlayer(string name, decimal amount, PersonRole role = PersonRole.Player);
        string GetAccountReportByPerson(Person person);

    }
}