using AutoMapper;

namespace Converters;

public class StringToDateTimeConverter : ITypeConverter<string, DateTime>
{
    public DateTime Convert(string source, DateTime destination, ResolutionContext context)
    {
        object objDateTime = source;

        if (DateTime.TryParse(objDateTime.ToString(), out DateTime dateTime))
        {
            return dateTime;
        }

        return default;
    }
}