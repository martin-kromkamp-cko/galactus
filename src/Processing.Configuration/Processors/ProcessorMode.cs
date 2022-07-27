using System.Runtime.Serialization;

namespace Processing.Configuration.Processors;

public enum ProcessingMode
{
    /// <summary>
    /// gateway only
    /// </summary>
    [EnumMember(Value = "gateway_only")]
    GatewayOnly,
}