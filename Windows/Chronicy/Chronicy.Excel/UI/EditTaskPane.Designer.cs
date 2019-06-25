namespace Chronicy.Excel.UI
{
    partial class EditTaskPane
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.editorGrid = new System.Windows.Forms.TableLayoutPanel();
            this.SuspendLayout();
            // 
            // editorGrid
            // 
            this.editorGrid.ColumnCount = 2;
            this.editorGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.editorGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.editorGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editorGrid.Location = new System.Drawing.Point(0, 0);
            this.editorGrid.Name = "editorGrid";
            this.editorGrid.Padding = new System.Windows.Forms.Padding(15);
            this.editorGrid.RowCount = 2;
            this.editorGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.editorGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.editorGrid.Size = new System.Drawing.Size(300, 600);
            this.editorGrid.TabIndex = 0;
            // 
            // EditTaskPane
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.editorGrid);
            this.Name = "EditTaskPane";
            this.Size = new System.Drawing.Size(300, 600);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel editorGrid;
    }
}
