namespace Simple.PersonModel.PersonModels
{
    public interface IPerson
    {

        Guid ID { get; set; }
        Guid AccountID { get; set; }
        string Name { get; set; }
        int WinGamesCount { get; set; }
        int GamesCount { get; set; }
        PersonRole Role { get; set; }

    }
}

