using System;
using System.Data;

namespace DbUp.Tests.Common.RecordingDb;

class RecordingDbDataParameter : IDbDataParameter
{
    public DbType DbType { get; set; }
    public ParameterDirection Direction { get; set; }
    public bool IsNullable { get; private set; }
#pragma warning disable CS8766 // Nullability of reference types in return type doesn't match implicitly implemented member (possibly because of nullability attributes).
    public string? ParameterName { get; set; }
    public string? SourceColumn { get; set; }
#pragma warning restore CS8766 // Nullability of reference types in return type doesn't match implicitly implemented member (possibly because of nullability attributes).
    public DataRowVersion SourceVersion { get; set; }
    public object? Value { get; set; }
    public byte Precision { get; set; }
    public byte Scale { get; set; }
    public int Size { get; set; }

    public override string ToString()
    {
        var format = "{0}={1}";
        if ((DbType == DbType.Date)
            || (DbType == DbType.DateTime)
            || (DbType == DbType.DateTime2)
            || (DbType == DbType.DateTimeOffset)
            || (DbType == DbType.AnsiString // If DbType is not explicitly set it will default to AnsiString so check our Value's type
                && (Value != null && (Value.GetType() == typeof(DateTime) || Value.GetType() == typeof(DateTimeOffset)))))
        {
            format = "{0}={1:dd\\/MM\\/yyyy hh\\:mm\\:ss}"; // Be explicit and don't rely on the system's formatting for dates so we can scrub them out later
        }

        return string.Format(format, ParameterName, Value);
    }
}
