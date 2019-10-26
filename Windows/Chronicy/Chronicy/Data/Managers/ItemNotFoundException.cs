using System;

namespace Chronicy.Data.Managers
{
    /// <summary>
    /// Represents that a <see cref="Chronicy.Data.Managers.NotebookManager"/> could not find an item.
    /// </summary>
    public class ItemNotFoundException : Exception
    {
        public ItemNotFoundException(string name) : base($"The item named { name } could not be found")
        {
        }
    }
}
