namespace Simple.Bank.TransactionModels
{
    public interface ITransaction
    {
        DateTime Deta { get; set; }
        Guid FromID { get; set; }
        Guid ToID { get; set; }
        decimal TransactionAmonut { get; set; }
        string Description { get; set; }
    }
}
