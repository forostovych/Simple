using Simple.Bank;

namespace Simple.PersonModel.PersonModels
{
    public class Person : BaseModel
    {
        public Guid AccountID { get; set; }
        public Guid CardDeckID { get; set; }
        public string Name { get; set; }
        public int WinGamesCount { get; set; }
        public int GamesCount { get; set; }
        public PersonRole Role { get; set; }

    }
}
