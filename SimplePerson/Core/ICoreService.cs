namespace Simple.Core
{
    public interface ICoreService
    {

        void AddNewPlayer(string name, decimal startMoney);
        void StartGameAsync(int countCardDeks);

    }
}