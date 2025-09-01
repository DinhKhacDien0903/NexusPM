// <copyright file="Money.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NexusPM.Domain.ValueObjects;

/// <summary>
/// Represents a monetary value with an amount and a currency.
/// </summary>
public sealed class Money : IEquatable<Money>
{
    /// <summary>
    /// Gets the monetary amount.
    /// </summary>
    public decimal Amount { get; }

    /// <summary>
    /// Gets the currency of the monetary value.
    /// </summary>
    public string Currency { get; } = "VND";

    /// <summary>
    /// Initializes a new instance of the <see cref="Money"/> class.
    /// </summary>
    /// <param name="amount">The monetary amount. Must be non-negative.</param>
    /// <param name="currency">The currency of the monetary value. Cannot be null or whitespace.</param>
    /// <exception cref="ArgumentException">Thrown when the amount is negative or the currency is invalid.</exception>
    public Money(decimal amount, string currency)
    {
        if (amount < 0)
        {
            throw new ArgumentException("Amount cannot be negative.");
        }

        if (string.IsNullOrWhiteSpace(currency))
        {
            throw new ArgumentException("Currency is required.");
        }

        this.Amount = amount;
        this.Currency = currency.ToUpper(System.Globalization.CultureInfo.CurrentCulture);
    }

    /// <summary>
    /// Determines whether the specified <see cref="Money"/> instance is equal to the current instance.
    /// </summary>
    /// <param name="other">The <see cref="Money"/> instance to compare with the current instance.</param>
    /// <returns><c>true</c> if the specified instance is equal to the current instance; otherwise, <c>false</c>.</returns>
    public bool Equals(Money? other) => other is not null && this.Amount == other.Amount && this.Currency == other.Currency;

    /// <summary>
    /// Determines whether the specified object is equal to the current instance.
    /// </summary>
    /// <param name="obj">The object to compare with the current instance.</param>
    /// <returns><c>true</c> if the specified object is equal to the current instance; otherwise, <c>false</c>.</returns>
    public override bool Equals(object? obj) => obj is Money money && this.Equals(money);

    /// <summary>
    /// Serves as the default hash function.
    /// </summary>
    /// <returns>A hash code for the current instance.</returns>
    public override int GetHashCode() => HashCode.Combine(this.Amount, this.Currency);

    /// <summary>
    /// Returns a string representation of the monetary value.
    /// </summary>
    /// <returns>A string in the format "Amount Currency".</returns>
    public override string ToString() => $"{this.Amount} {this.Currency}";
}
