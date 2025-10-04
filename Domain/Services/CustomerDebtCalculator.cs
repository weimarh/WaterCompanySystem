namespace Domain.Services
{
    public static class CustomerDebtCalculator
    {
        public static string CalculateDebt(string previousReading, string latestReading, string pricePerLiter)
        {
            int readingDifference = 0;
            decimal decimalPricePerLiter = 0;
            
            try
            {
                readingDifference = int.Parse(latestReading) - int.Parse(previousReading);
            }
            catch (FormatException ex)
            {
                throw new FormatException(ex.ToString());
            }

            try
            {
                decimalPricePerLiter = decimal.Parse(pricePerLiter);
            }
            catch (FormatException ex)
            {
                throw new FormatException(ex.ToString());
            }

            return (readingDifference * decimalPricePerLiter).ToString();
        }
    }
}
