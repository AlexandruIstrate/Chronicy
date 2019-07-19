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
    public class CreateModel : PageModel
    {
        private IDataSource<Notebook> dataSource;

        [BindProperty]
        public Notebook EditedNotebook { get; set; }

        public CreateModel(ISqlDatabase database)
        {
            dataSource = new SqlDataSource(database);
        }

        public IActionResult OnGetAsync()
        {
            EditedNotebook = new Notebook("New Notebook");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await dataSource.CreateAsync(EditedNotebook);

            return RedirectToPage("./Index");
        }
    }
}