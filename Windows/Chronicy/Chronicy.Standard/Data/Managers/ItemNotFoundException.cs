using System;

namespace Chronicy.Data.Managers
{
    public class ItemNotFoundException : Exception
    {
        public ItemNotFoundException(string name) : base($"The item named { name } could not be found")
        {

        }
    }
}
