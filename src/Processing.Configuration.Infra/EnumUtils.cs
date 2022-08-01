using System.Reflection;
using System.Runtime.Serialization;

namespace Processing.Configuration.Infra;

public class EnumUtils
{
    /// <summary>
    /// Tries to parse a string into an enum honoring the EnumMemberAttribute if present
    /// </summary>
    /// <param name="value">string value</param>
    /// <param name="result">out parse result</param>
    /// <typeparam name="TEnum">type</typeparam>
    /// <returns>success is parsed</returns>
    public static bool TryParseWithMemberName<TEnum>(string value, out TEnum result)
        where TEnum : struct
    {
        result = default;

        if (string.IsNullOrEmpty(value))
        {
            return false;
        }

        Type enumType = typeof(TEnum);

        foreach (string name in Enum.GetNames(typeof(TEnum)))
        {
            if (name.Equals(value, StringComparison.OrdinalIgnoreCase))
            {
                result = Enum.Parse<TEnum>(name);
                return true;
            }

            var memberAttribute = enumType.GetField(name)?.GetCustomAttribute(typeof(EnumMemberAttribute)) as EnumMemberAttribute;

            if (memberAttribute is null)
            {
                continue;
            }

            if (memberAttribute.Value.Equals(value, StringComparison.OrdinalIgnoreCase))
            {
                result = Enum.Parse<TEnum>(name);
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Gets the enum value from a string honoring the EnumMemberAttribute if present
    /// </summary>
    /// <param name="value">value</param>
    /// <typeparam name="TEnum">typeparam</typeparam>
    /// <returns>parsed value</returns>
    public static TEnum? GetEnumValueOrDefault<TEnum>(string value)
        where TEnum : struct
    {
        if (TryParseWithMemberName(value, out TEnum result))
        {
            return result;
        }

        return default;
    }
}