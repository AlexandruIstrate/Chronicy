using System;

namespace Chronicy.Service.Data
{
    public class ItemNotFoundException : Exception
    {
        public ItemNotFoundException(string name) : base($"The item named { name } could not be found")
        {

        }
    }
}
