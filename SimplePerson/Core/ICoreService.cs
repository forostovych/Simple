using Simple.Bank;
using Simple.CardTable.CardDeckModel;
using Simple.CardTable.CardTableModel;
using Simple.CardTable;
using Simple.PersonModel.PersonModels;
using System;
using System.Data;

namespace Simple.Core
{
    public interface ICoreService
    {

        public void AddNewPlayer(string name, decimal startMoney);
        public void StartGame(int countCardDeks);

    }
}