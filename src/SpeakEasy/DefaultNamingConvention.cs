namespace SpeakEasy
{
    public class DefaultNamingConvention : INamingConvention
    {
        public string ConvertPropertyNameToParameterName(string propertyName)
        {
            return propertyName;
        }
    }
}