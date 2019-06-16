using System.Windows.Forms;

namespace Chronicy.Excel.Forms
{
    public partial class BaseForm : Form
    {
        public IFormBehavior FormBehavior { get; set; }

        public BaseForm()
        {
            InitializeComponent();
        }
    }
}
