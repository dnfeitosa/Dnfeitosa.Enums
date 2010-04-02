namespace Dnfeitosa.Enums.Serialization
{
    internal class PropertiesCopier
    {
        public void Copy(IEnum from, IEnum to)
        {
            var properties = from.GetType().GetProperties();

            foreach (var property in properties)
            {
                var value = property.GetValue(from, null);
                property.SetValue(to, value, null);
            }
        }
    }
}