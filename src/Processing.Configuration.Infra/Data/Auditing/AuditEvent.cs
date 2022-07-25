namespace Processing.Configuration.Infra.Data.Auditing;

/// <summary>
/// https://github.com/thepirat000/Audit.NET/blob/master/src/Audit.NET.PostgreSql/SqlScript.sql
/// </summary>
public class AuditEvent
{
    public long Id { get; set; }

    public DateTime InsertedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public object Data { get; set; }

    public string EventType { get; set; }
}
