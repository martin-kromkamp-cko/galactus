using System.ComponentModel;
using System.Globalization;

namespace Processing.Configuration.Identifiers;

internal class IdConverter : TypeConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
    {
        return sourceType == typeof(string);
    }

    public override object? ConvertFrom(ITypeDescriptorContext? context,
        CultureInfo? culture, object value)
    {
        return Id.TryParse(value as string, out Id? id) 
            ? id 
            : null;
    }

    public override bool CanConvertTo(ITypeDescriptorContext? context,
        Type? destinationType)
    {
        return destinationType == typeof(string);
    }

    public override object? ConvertTo(ITypeDescriptorContext? context,
        CultureInfo? culture, object value, Type destinationType)
    {
        return destinationType == typeof(string) 
            ? ((Id)value).Value 
            : null;
    }
}