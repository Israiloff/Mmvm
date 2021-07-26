using Israiloff.Mmvm.Net.Core.Exceptions.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Israiloff.Mmvm.Net.Core.Exceptions.Models
{
    public class CultureSpecifiedMessage
    {
        public string Title { get; set; }

        public string Body { get; set; }

        public string Comment { get; set; } = string.Empty;

        [JsonConverter(typeof(StringEnumConverter))]
        public Language Language { get; set; }
    }
}