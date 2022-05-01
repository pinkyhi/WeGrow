using Newtonsoft.Json.Serialization;

namespace WeGrow.LiqPay.Resolvers
{
    public class LowercaseContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            return propertyName.ToLower();
        }
    }
}
