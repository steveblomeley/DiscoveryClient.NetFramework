using System;
using System.ServiceModel.Channels;

namespace DiscoveryClient
{
    public class MulticastCapabilitiesBindingElement : BindingElement, IBindingMulticastCapabilities
    {
        private readonly bool _isMulticast;

        public MulticastCapabilitiesBindingElement(bool isMulticast)
        {
            this._isMulticast = isMulticast;
        }

        public override T GetProperty<T>(BindingContext context)
        {
            if (typeof(T) == typeof(IBindingMulticastCapabilities))
            {
                return (T)(object)this;
            }

            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return context.GetInnerProperty<T>();
        }

        bool IBindingMulticastCapabilities.IsMulticast => _isMulticast;

        public override BindingElement Clone()
        {
            return this;
        }
    }
}