using System.Text.RegularExpressions;

namespace Domain.ValueObjects
{
    public partial record Money
    {
        private const string Pattern = @"^\d{1,6}(?:\.\d{1,2})?$";

        public string Value { get; init; }
        private Money(string value) => Value = value;

        public static Money? Create(string value)
        {
            ArgumentException.ThrowIfNullOrEmpty(value, nameof(value));

            if (!MoneyRegex().IsMatch(value)) return null;

            return new Money(value);
        }

        [GeneratedRegex(Pattern)]
        private static partial Regex MoneyRegex();
    }
}
