namespace NexusPM.Domain.ValueObjects;
public sealed class Money : IEquatable<Money>
{

    public decimal Amount { get; }
    public string Currency { get; } = "VND";
    public bool Equals(ValueObjects.Money? other) => other is not null && this.Amount == other.Amount && this.Currency == other.Currency;
    public Money(decimal amount, string currency)
    {
        if (amount < 0) throw new ArgumentException("Amount cannot be negative.");
        if (string.IsNullOrWhiteSpace(currency)) throw new ArgumentException("Currency is required.");

        this.Amount = amount;
        this.Currency = currency.ToUpper(System.Globalization.CultureInfo.CurrentCulture);
    }

    public override string ToString() => $"{this.Amount} {this.Currency}";
}
