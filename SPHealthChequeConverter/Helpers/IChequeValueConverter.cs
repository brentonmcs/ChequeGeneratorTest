namespace SPHealthChequeConverter.Helpers
{
    public interface IChequeValueConverter
    {
        string Convert(double amount);
    }
}