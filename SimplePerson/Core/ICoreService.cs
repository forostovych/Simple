namespace Simple.Core
{
    public interface ICoreService
    {

        public void AddNewPlayer(string name, decimal startMoney);
        public void StartGame(int countCardDeks);

    }
}