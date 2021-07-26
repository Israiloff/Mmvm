namespace Israiloff.Mmvm.Net.Core.Services.Serializer
{
    public interface ISerializer
    {
        string Serialize(object target);

        TResult Deserialize<TResult>(string data);
    }
}