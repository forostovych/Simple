namespace Simple.PersonModel.PersonModels
{
    public class Person : IPerson
    {

        public Guid ID { get; set; }
        public Guid AccountID { get; set; }
        public string Name { get; set; }
        public int WinGamesCount { get; set; }
        public int GamesCount { get; set; }
        public PersonRole Role { get; set; }

    }
}
