using Chronicy.Data;
using Chronicy.Data.Storage;
using Chronicy.Sql;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Chronicy.Website.Pages.Notebooks
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly IDataSource<Notebook> dataSource;

        [BindProperty]
        public Notebook EditedNotebook { get; set; }

        public DeleteModel(ISqlDatabase database)
        {
            dataSource = new SqlDataSource(database);
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            EditedNotebook = await dataSource.GetAsync(id.Value);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            EditedNotebook = await dataSource.GetAsync(id.Value);

            if (EditedNotebook == null)
            {
                return NotFound();
            }

            await dataSource.DeleteAsync(id.Value);

            return RedirectToPage("./Index");
        }
    }
}