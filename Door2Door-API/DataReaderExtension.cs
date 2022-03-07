using System.Diagnostics.CodeAnalysis;
using Npgsql;

namespace Door2Door_API;

public static class DataReaderExtension
{
    public static T ReadFirstOrDefault<T>([NotNull] this NpgsqlDataReader reader,
        [NotNull] Func<NpgsqlDataReader, T> recordReader)
    {
        if (reader == null)
        {
            throw new ArgumentNullException(nameof(reader));
        }

        if (recordReader == null)
        {
            throw new ArgumentNullException(nameof(recordReader));
        }

        return (reader.Read() ? recordReader(reader) : default)!;
    }
}