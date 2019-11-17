using System.Collections.Generic;
using System.Threading.Tasks;
using Chronicy.Data;
using Chronicy.Sql;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chronicy.Website.Pages.Notebooks
{
    [Authorize]
    public class IndexModel : PageModel
    {
        //private readonly IDataSource<Notebook> dataSource;

        public List<Notebook> Items { get; set; }

        public IndexModel(ISqlDatabase database)
        {
            //dataSource = new SqlDataSource(database);
        }

        public Task OnGetAsync()
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

            return Task.CompletedTask;
        }
    }
}