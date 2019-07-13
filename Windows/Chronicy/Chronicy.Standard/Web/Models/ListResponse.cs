using System.Collections.Generic;

namespace Chronicy.Web.Models
{
    public class ListResponse<T> : ModelBase
    {
        public List<T> List { get; set; }
    }
}
