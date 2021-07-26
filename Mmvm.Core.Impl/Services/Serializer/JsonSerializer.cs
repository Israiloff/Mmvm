using Israiloff.Mmvm.Net.Container.Attributes;
using Israiloff.Mmvm.Net.Core.Services.Serializer;
using Newtonsoft.Json;

namespace Israiloff.Mmvm.Net.Core.Impl.Services.Serializer
{
    [Service(Name = nameof(JsonSerializer))]
    public class JsonSerializer : ISerializer
    {
        #region IJsonConverter impl

        public string Serialize(object target)
        {
            return target == null ? string.Empty : JsonConvert.SerializeObject(target);
        }

        public TResult Deserialize<TResult>(string data)
        {
            return JsonConvert.DeserializeObject<TResult>(data);
        }

        #endregion
    }
}