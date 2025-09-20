namespace Domain.ValueObjects
{
    public partial record HouseNumber
    {
        public string Value { get; init; }

        private HouseNumber(string value) => Value = value;

        public static HouseNumber? Create(string value)
        {
            ArgumentException.ThrowIfNullOrEmpty(value, nameof(value));

            if (value.Length < 2 || value.Length > 6)
                return null;

            if (!value.All(char.IsDigit))
                return null;

            return new HouseNumber(value);
        }
    }
}
