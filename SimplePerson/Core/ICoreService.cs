using Simple.PersonModel.PersonModels;
using System;

namespace Simple.Core
{
    public interface ICoreService
    {
        IPerson AddPlayer(string name, PersonRole role, decimal amount);
        string GetAccountReportByPerson(IPerson person);

    }
}