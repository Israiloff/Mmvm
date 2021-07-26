using Israiloff.Mmvm.Net.Core.Services.ApplicationProperties.Dtos;

namespace Israiloff.Mmvm.Net.Core.Services.ApplicationProperties
{
    public interface IPropertyResolver<in TPropertyName>
    {
        PropertyValueResultDto<TResultValue> GetValue<TResultValue>(TPropertyName propertyName);
    }
}