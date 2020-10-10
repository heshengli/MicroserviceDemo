using JetBrains.Annotations;
using Volo.Abp.Domain.Entities;

namespace Volo.Abp.IdentityServer
{
    public abstract class Property : Entity
    {
        public virtual string Key { get; set; }
        public virtual string Value { get; set; }

        protected Property()
        {

        }

        protected Property([NotNull]string key, [NotNull]string value)
        {
            Check.NotNull(key, nameof(key));
            Check.NotNull(value, nameof(value));

            Key = key;
            Value = value;
        }
    }
}
