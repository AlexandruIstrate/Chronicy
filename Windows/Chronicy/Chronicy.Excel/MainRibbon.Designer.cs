namespace Chronicy.Excel
{
    partial class MainRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public MainRibbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

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
            Microsoft.Office.Tools.Ribbon.RibbonDropDownItem ribbonDropDownItemImpl1 = this.Factory.CreateRibbonDropDownItem();
            Microsoft.Office.Tools.Ribbon.RibbonDropDownItem ribbonDropDownItemImpl2 = this.Factory.CreateRibbonDropDownItem();
            this.tab1 = this.Factory.CreateRibbonTab();
            this.tab2 = this.Factory.CreateRibbonTab();
            this.group3 = this.Factory.CreateRibbonGroup();
            this.group1 = this.Factory.CreateRibbonGroup();
            this.box1 = this.Factory.CreateRibbonBox();
            this.group2 = this.Factory.CreateRibbonGroup();
            this.connectButton = this.Factory.CreateRibbonButton();
            this.enableButton = this.Factory.CreateRibbonToggleButton();
            this.notebookDropDown = this.Factory.CreateRibbonDropDown();
            this.stackDropDown = this.Factory.CreateRibbonDropDown();
            this.trackingMenu = this.Factory.CreateRibbonMenu();
            this.historyGallery = this.Factory.CreateRibbonGallery();
            this.syncButton = this.Factory.CreateRibbonButton();
            this.tab1.SuspendLayout();
            this.tab2.SuspendLayout();
            this.group3.SuspendLayout();
            this.group1.SuspendLayout();
            this.box1.SuspendLayout();
            this.group2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab1.Label = "TabAddIns";
            this.tab1.Name = "tab1";
            // 
            // tab2
            // 
            this.tab2.Groups.Add(this.group3);
            this.tab2.Groups.Add(this.group1);
            this.tab2.Groups.Add(this.group2);
            this.tab2.Label = "Chronicy";
            this.tab2.Name = "tab2";
            // 
            // group3
            // 
            this.group3.Items.Add(this.connectButton);
            this.group3.Items.Add(this.enableButton);
            this.group3.Label = "Extension";
            this.group3.Name = "group3";
            // 
            // group1
            // 
            this.group1.Items.Add(this.box1);
            this.group1.Label = "Selection";
            this.group1.Name = "group1";
            // 
            // box1
            // 
            this.box1.BoxStyle = Microsoft.Office.Tools.Ribbon.RibbonBoxStyle.Vertical;
            this.box1.Items.Add(this.notebookDropDown);
            this.box1.Items.Add(this.stackDropDown);
            this.box1.Name = "box1";
            // 
            // group2
            // 
            this.group2.Items.Add(this.trackingMenu);
            this.group2.Items.Add(this.historyGallery);
            this.group2.Items.Add(this.syncButton);
            this.group2.Label = "Tools";
            this.group2.Name = "group2";
            // 
            // connectButton
            // 
            this.connectButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.connectButton.Image = global::Chronicy.Excel.Properties.Resources.IconConnect;
            this.connectButton.Label = "Connect";
            this.connectButton.Name = "connectButton";
            this.connectButton.ShowImage = true;
            this.connectButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.OnConnectClicked);
            // 
            // enableButton
            // 
            this.enableButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.enableButton.Image = global::Chronicy.Excel.Properties.Resources.IconEnable;
            this.enableButton.Label = "Enabled";
            this.enableButton.Name = "enableButton";
            this.enableButton.ShowImage = true;
            this.enableButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.OnEnableToggled);
            // 
            // notebookDropDown
            // 
            this.notebookDropDown.Image = global::Chronicy.Excel.Properties.Resources.IconNotebook;
            ribbonDropDownItemImpl1.Label = "Home";
            this.notebookDropDown.Items.Add(ribbonDropDownItemImpl1);
            this.notebookDropDown.Label = "Notebook";
            this.notebookDropDown.Name = "notebookDropDown";
            this.notebookDropDown.ShowImage = true;
            // 
            // stackDropDown
            // 
            this.stackDropDown.Image = global::Chronicy.Excel.Properties.Resources.IconStack;
            ribbonDropDownItemImpl2.Label = "Personal Projects";
            this.stackDropDown.Items.Add(ribbonDropDownItemImpl2);
            this.stackDropDown.Label = "Stack";
            this.stackDropDown.Name = "stackDropDown";
            this.stackDropDown.ShowImage = true;
            // 
            // trackingMenu
            // 
            this.trackingMenu.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.trackingMenu.Image = global::Chronicy.Excel.Properties.Resources.IconTracking;
            this.trackingMenu.Label = "Tracking";
            this.trackingMenu.Name = "trackingMenu";
            this.trackingMenu.ShowImage = true;
            // 
            // historyGallery
            // 
            this.historyGallery.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.historyGallery.Image = global::Chronicy.Excel.Properties.Resources.IconHistory;
            this.historyGallery.Label = "History";
            this.historyGallery.Name = "historyGallery";
            this.historyGallery.ShowImage = true;
            // 
            // syncButton
            // 
            this.syncButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.syncButton.Image = global::Chronicy.Excel.Properties.Resources.IconSync;
            this.syncButton.Label = "Sync";
            this.syncButton.Name = "syncButton";
            this.syncButton.ShowImage = true;
            this.syncButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.OnSyncClicked);
            // 
            // MainRibbon
            // 
            this.Name = "MainRibbon";
            this.RibbonType = "Microsoft.Excel.Workbook";
            this.Tabs.Add(this.tab1);
            this.Tabs.Add(this.tab2);
            this.Close += new System.EventHandler(this.OnRibbonClose);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.OnRibbonLoad);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.tab2.ResumeLayout(false);
            this.tab2.PerformLayout();
            this.group3.ResumeLayout(false);
            this.group3.PerformLayout();
            this.group1.ResumeLayout(false);
            this.group1.PerformLayout();
            this.box1.ResumeLayout(false);
            this.box1.PerformLayout();
            this.group2.ResumeLayout(false);
            this.group2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab2;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
        internal Microsoft.Office.Tools.Ribbon.RibbonDropDown notebookDropDown;
        internal Microsoft.Office.Tools.Ribbon.RibbonDropDown stackDropDown;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton enableButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonBox box1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group2;
        internal Microsoft.Office.Tools.Ribbon.RibbonGallery historyGallery;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton syncButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group3;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton connectButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonMenu trackingMenu;
    }

    partial class ThisRibbonCollection
    {
        internal MainRibbon MainRibbon
        {
            get { return this.GetRibbon<MainRibbon>(); }
        }
    }
}
