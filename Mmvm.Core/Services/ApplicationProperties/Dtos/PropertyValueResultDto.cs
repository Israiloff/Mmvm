using Israiloff.Mmvm.Net.Core.Services.ApplicationProperties.Enum;

namespace Israiloff.Mmvm.Net.Core.Services.ApplicationProperties.Dtos
{
    public class PropertyValueResultDto<TValue>
    {
        public TValue Value { get; set; }

        public PropertyValueResolveResult Result { get; set; }
    }
}