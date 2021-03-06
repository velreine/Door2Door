using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace Door2Door_API.ExtensionMethods;

public static class DataReaderExtension
{
    public static T ReadFirstOrDefault<T>([NotNull] this IDataReader reader,
        [NotNull] Func<IDataReader, T> recordReader)
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