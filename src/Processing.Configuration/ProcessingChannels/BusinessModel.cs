using System.Runtime.Serialization;

namespace Processing.Configuration.ProcessingChannels;

public enum BusinessModel
{
    /// <summary>
    /// marketplace
    /// </summary>
    [EnumMember(Value = "marketplace")]
    Marketplace,

    /// <summary>
    /// payfac
    /// </summary>
    [EnumMember(Value = "payfac")]
    Payfac,

    /// <summary>
    /// mor
    /// </summary>
    [EnumMember(Value = "mor")]
    Mor,
}