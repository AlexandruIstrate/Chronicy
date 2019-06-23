using Microsoft.Office.Tools.Ribbon;
using System.Collections.Generic;

namespace Chronicy.Excel.UI
{
    public class ControlGroup
    {
        public List<RibbonControl> Controls { get; set; }

        public bool Enabled
        {
            get => Controls.Exists((control) => { return control.Enabled; });
            set => Controls.ForEach((control) => { control.Enabled = value; });
        }

        public bool Visible
        {
            get => Controls.Exists((control) => { return control.Visible; });
            set => Controls.ForEach((control) => { control.Visible = value; });
        }

        public ControlGroup(List<RibbonControl> controls = null)
        {
            Controls = controls ?? new List<RibbonControl>();
        }

        public ControlGroup(params RibbonControl[] controls)
        {
            Controls = new List<RibbonControl>(controls);
        }

        public void Add(RibbonControl control)
        {
            Controls.Add(control);
        }

        public void Remove(RibbonControl control)
        {
            Controls.Remove(control);
        }
    }
}
