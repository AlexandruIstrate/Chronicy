using System.Threading.Tasks;
using Chronicy.Data;
using Chronicy.Data.Storage;
using Chronicy.Sql;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chronicy.Website.Pages.Notebooks
{
    public class DeleteModel : PageModel
    {
        private IDataSource<Notebook> dataSource;

        [BindProperty]
        public Notebook EditedNotebook { get; set; }

        public DeleteModel()
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

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            EditedNotebook = await dataSource.GetAsync(id);

            if (EditedNotebook == null)
            {
                return NotFound();
            }

            await dataSource.DeleteAsync(id);

            return RedirectToPage("./Index");
        }
    }
}