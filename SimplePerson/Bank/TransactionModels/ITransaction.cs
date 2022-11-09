namespace Simple.Bank.TransactionModels
{
    public interface ITransaction
    {
        DateTime Data { get; set; }
        Guid FromID { get; set; }
        Guid ToID { get; set; }
        decimal TransactionAmonut { get; set; }
        string Description { get; set; }
    }
}
