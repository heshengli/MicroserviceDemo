using System;

namespace Volo.Abp.IdentityServer.ApiResources
{
    public class ApiResourceProperty : Property
    {
        public Guid ApiResourceId { get; set; }
        public ApiResource ApiResource { get; set; }

        public override object[] GetKeys()
        {
            return new object[] { ApiResourceId, ApiResource };
        }
    }
}
