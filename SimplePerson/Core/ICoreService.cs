namespace Simple.Core
{
    public interface ICoreService
    {

        void AddNewPlayer(string name, decimal startMoney);
        Task StartGameAsync(int countCardDeks);

    }
}