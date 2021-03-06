﻿namespace Chronicy.Excel.UI
{
    partial class NotebooksTaskPane
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
            this.label1 = new System.Windows.Forms.Label();
            this.notebookComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.stackComboBox = new System.Windows.Forms.ComboBox();
            this.fieldsGridView = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.fieldsGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Notebook";
            // 
            // notebookComboBox
            // 
            this.notebookComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.notebookComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.notebookComboBox.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.notebookComboBox.FormattingEnabled = true;
            this.notebookComboBox.Location = new System.Drawing.Point(63, 27);
            this.notebookComboBox.Name = "notebookComboBox";
            this.notebookComboBox.Size = new System.Drawing.Size(234, 21);
            this.notebookComboBox.TabIndex = 2;
            this.notebookComboBox.SelectedIndexChanged += new System.EventHandler(this.OnNotebookChanged);
            this.notebookComboBox.SelectionChangeCommitted += new System.EventHandler(this.OnNotebookChangeCommited);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Stack";
            // 
            // stackComboBox
            // 
            this.stackComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.stackComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.stackComboBox.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.stackComboBox.FormattingEnabled = true;
            this.stackComboBox.Location = new System.Drawing.Point(63, 55);
            this.stackComboBox.Name = "stackComboBox";
            this.stackComboBox.Size = new System.Drawing.Size(234, 21);
            this.stackComboBox.TabIndex = 5;
            this.stackComboBox.SelectedIndexChanged += new System.EventHandler(this.OnStackChanged);
            // 
            // fieldsGridView
            // 
            this.fieldsGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fieldsGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.fieldsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.fieldsGridView.Location = new System.Drawing.Point(6, 98);
            this.fieldsGridView.Name = "fieldsGridView";
            this.fieldsGridView.Size = new System.Drawing.Size(291, 435);
            this.fieldsGridView.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Fields";
            // 
            // NotebooksTaskPane
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.fieldsGridView);
            this.Controls.Add(this.stackComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.notebookComboBox);
            this.Name = "NotebooksTaskPane";
            this.Load += new System.EventHandler(this.OnLoad);
            this.Controls.SetChildIndex(this.notebookComboBox, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.stackComboBox, 0);
            this.Controls.SetChildIndex(this.fieldsGridView, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            ((System.ComponentModel.ISupportInitialize)(this.fieldsGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox notebookComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox stackComboBox;
        private System.Windows.Forms.DataGridView fieldsGridView;
        private System.Windows.Forms.Label label3;
    }
}
