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
    public class EditModel : PageModel
    {
        private IDataSource<Notebook> dataSource;

        [BindProperty]
        public Notebook EditedNotebook { get; set; }

        public EditModel()
        {
            // TODO: SqlConnection
            //dataSource = new SqlDataSource(null);
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Get Notebook from database
            EditedNotebook = await dataSource.GetAsync(id.Value);

            if (EditedNotebook == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                // Save Notebook to database
                await dataSource.UpdateAsync(EditedNotebook);
            }
            catch (DataSourceException)
            {
                if (!(await NotebookExistsAsync(EditedNotebook.ID)))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToPage("./Index");
        }

        private async Task<bool> NotebookExistsAsync(int id)
        {
            return (await dataSource.GetAsync(id) != null);
        }
    }
}