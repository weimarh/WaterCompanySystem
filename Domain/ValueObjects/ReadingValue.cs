namespace Domain.ValueObjects
{
    public partial record ReadingValue
    {
        public string Value { get; init; }
        private ReadingValue(string value)  => Value = value;

        public static ReadingValue? Create(string value)
        {
            ArgumentException.ThrowIfNullOrEmpty(value, nameof(value));

            if (value.Length < 1 || value.Length > 6) 
                return null;

            if (!value.All(char.IsDigit))
                return null;

            return new ReadingValue(value);
        }
    }
}
