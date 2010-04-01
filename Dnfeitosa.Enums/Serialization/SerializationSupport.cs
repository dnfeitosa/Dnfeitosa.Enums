namespace Dnfeitosa.Enums.Serialization
{
    internal class SerializationSupport
    {
        public void CopyProperties(IEnum from, IEnum to)
        {
            foreach (var property in from.GetType().GetProperties())
            {
                var value = property.GetValue(from, null);
                property.SetValue(to, value, null);
            }
        }
    }
}