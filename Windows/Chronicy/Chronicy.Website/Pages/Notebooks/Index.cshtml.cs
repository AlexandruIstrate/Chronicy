using System.Collections.Generic;
using System.Threading.Tasks;
using Chronicy.Data;
using Chronicy.Data.Storage;
using Chronicy.Sql;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chronicy.Website.Pages.Notebooks
{
    public class IndexModel : PageModel
    {
        private IDataSource<Notebook> dataSource;

        public List<Notebook> Items { get; set; }

        public IndexModel()
        {
            // TODO: SqlConnection
            dataSource = new SqlDataSource(null);
        }

        public async Task OnGetAsync()
        {
            //Items = new List<Notebook>(await dataSource.GetAllAsync());
            Items = new List<Notebook>
            {
                new Notebook("Notebok 1"),
                new Notebook("Notebok 2"),
                new Notebook("Notebok 3"),
                new Notebook("Notebok 4"),
                new Notebook("Notebok 5")
            };
        }
    }
}