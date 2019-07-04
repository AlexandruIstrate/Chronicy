using System.Threading.Tasks;
using Chronicy.Data;
using Chronicy.Data.Storage;
using Chronicy.Sql;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chronicy.Website.Pages.Notebooks
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private IDataSource<Notebook> dataSource;

        public Notebook EditedNotebook { get; set; }

        public DetailsModel()
        {
            // TODO: SqlConnection
            dataSource = new SqlDataSource(null);
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            EditedNotebook = await dataSource.GetAsync(id);

            if (id == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}