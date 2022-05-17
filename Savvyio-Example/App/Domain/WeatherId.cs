using System;
using System.Linq;
using Cuemon;
using Savvyio.Domain;

namespace Savvyio_Example.App.Domain
{
    public class WeatherId : SingleValueObject<Guid>
    {
        public static implicit operator WeatherId(Guid value)
        {
            return new WeatherId(value);
        }

        public WeatherId(DateTime value) : this(new Guid(Convertible.GetBytes(value.Date).Take(16).ToArray()))
        {
        }

        public WeatherId(Guid value) : base(value)
        {
        }
    }
}
