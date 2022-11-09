namespace ConsoleApp_Pesrson.UserModel
{
    public interface IPerson
    {

        public Guid PersonID { get; set; }
        Guid AccountID { get; set; }
        string Name { get; set; }
        string Login { get; }
        int WinCount { get; set; }
        int LouseCount { get; set; }

    }
}