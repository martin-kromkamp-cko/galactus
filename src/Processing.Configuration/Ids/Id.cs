using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Processing.Configuration.Ids;

/// <summary>
///     Represents a Base-32 encoded entity identifier
/// </summary>
[TypeConverter(typeof(IdConverter))]
[Serializable]
internal sealed class Id : IEquatable<Id>
{
    private readonly Guid _decodedValue;
    private readonly string _prefix;
    private string? _formattedValue;

    public Id(string prefix, Guid value)
        : this(prefix, value, null)
    {
    }

    private Id(string prefix, Guid value, string? formattedValue)
    {
        if (string.IsNullOrWhiteSpace(prefix))
        {
            throw new ArgumentException("Prefix must be provided", nameof(prefix));
        }

        _prefix = prefix;
        _decodedValue = value;
        _formattedValue = formattedValue;
    }

    /// <summary>
    ///     Gets the formatted value of the Id
    /// </summary>
    public string Value =>
        _formattedValue ??= _prefix + "_" + Base32.Encode(_decodedValue.ToByteArray());

    public static bool operator ==(Id? id1, Id? id2)
    {
        if (id1 is not null)
        {
            return id1.Equals(id2);
        }

        // null == null
        return id2 is null;
    }

    public static bool operator !=(Id? id1, Id? id2)
    {
        return !(id1 == id2);
    }

    /// <summary>
    ///     Attempts to parse an Id from the specified formatted Id value
    /// </summary>
    /// <param name="value">The value to parse</param>
    /// <param name="id">The parsed Id if successful</param>
    /// <returns>True if the Id could be parsed, otherwise False</returns>
    public static bool TryParse(string? value, [NotNullWhen(true)] out Id? id)
    {
        id = null;

        if (string.IsNullOrEmpty(value))
        {
            return false;
        }

        string[] parts = value.Split('_');
        if (parts.Length != 2)
        {
            return false;
        }

        try
        {
            var guidId = new Guid(Base32.Decode(parts[1]));
            id = new Id(parts[0], guidId, value);

            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    ///     Creates a new Id with the specified prefix and new Guid
    /// </summary>
    /// <param name="prefix">The Id prefix</param>
    /// <returns>A new <see cref="Id" /></returns>
    public static Id NewId(string prefix)
    {
        return new Id(prefix, Guid.NewGuid());
    }

    public bool Equals(Id? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return other.GetHashCode() == GetHashCode();
    }

    public Guid ToGuid()
    {
        return _decodedValue;
    }

    public bool HasPrefix(string prefix)
    {
        return _prefix.Equals(prefix);
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as Id);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            var hash = 17;

            // Suitable nullity checks etc, of course :)
            hash = (hash * 23) + _prefix.GetHashCode();
            hash = (hash * 23) + _decodedValue.GetHashCode();
            return hash;
        }
    }

    public override string ToString()
    {
        return Value;
    }
}