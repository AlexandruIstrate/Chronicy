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
            this.tab1 = this.Factory.CreateRibbonTab();
            this.tab2 = this.Factory.CreateRibbonTab();
            this.extensionGroup = this.Factory.CreateRibbonGroup();
            this.connectButton = this.Factory.CreateRibbonButton();
            this.enableButton = this.Factory.CreateRibbonToggleButton();
            this.selectionGroup = this.Factory.CreateRibbonGroup();
            this.box1 = this.Factory.CreateRibbonBox();
            this.notebookDropDown = this.Factory.CreateRibbonDropDown();
            this.stackDropDown = this.Factory.CreateRibbonDropDown();
            this.showCompatibleCheckBox = this.Factory.CreateRibbonCheckBox();
            this.newNotebookButton = this.Factory.CreateRibbonButton();
            this.newStackButton = this.Factory.CreateRibbonButton();
            this.viewAllButton = this.Factory.CreateRibbonButton();
            this.toolsGroup = this.Factory.CreateRibbonGroup();
            this.trackingMenu = this.Factory.CreateRibbonMenu();
            this.trackWorkbookButton = this.Factory.CreateRibbonButton();
            this.trackSheetButton = this.Factory.CreateRibbonButton();
            this.trackCellButton = this.Factory.CreateRibbonButton();
            this.historyGallery = this.Factory.CreateRibbonGallery();
            this.syncButton = this.Factory.CreateRibbonButton();
            this.supportGroup = this.Factory.CreateRibbonGroup();
            this.helpButton = this.Factory.CreateRibbonButton();
            this.reportBugButton = this.Factory.CreateRibbonButton();
            this.tab1.SuspendLayout();
            this.tab2.SuspendLayout();
            this.extensionGroup.SuspendLayout();
            this.selectionGroup.SuspendLayout();
            this.box1.SuspendLayout();
            this.toolsGroup.SuspendLayout();
            this.supportGroup.SuspendLayout();
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
            this.tab2.Groups.Add(this.extensionGroup);
            this.tab2.Groups.Add(this.selectionGroup);
            this.tab2.Groups.Add(this.toolsGroup);
            this.tab2.Groups.Add(this.supportGroup);
            this.tab2.Label = "Chronicy";
            this.tab2.Name = "tab2";
            // 
            // extensionGroup
            // 
            this.extensionGroup.Items.Add(this.connectButton);
            this.extensionGroup.Items.Add(this.enableButton);
            this.extensionGroup.Label = "Extension";
            this.extensionGroup.Name = "extensionGroup";
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
            // selectionGroup
            // 
            this.selectionGroup.Items.Add(this.box1);
            this.selectionGroup.Items.Add(this.newNotebookButton);
            this.selectionGroup.Items.Add(this.newStackButton);
            this.selectionGroup.Items.Add(this.viewAllButton);
            this.selectionGroup.Label = "Selection";
            this.selectionGroup.Name = "selectionGroup";
            // 
            // box1
            // 
            this.box1.BoxStyle = Microsoft.Office.Tools.Ribbon.RibbonBoxStyle.Vertical;
            this.box1.Items.Add(this.notebookDropDown);
            this.box1.Items.Add(this.stackDropDown);
            this.box1.Items.Add(this.showCompatibleCheckBox);
            this.box1.Name = "box1";
            // 
            // notebookDropDown
            // 
            this.notebookDropDown.Image = global::Chronicy.Excel.Properties.Resources.IconNotebook;
            this.notebookDropDown.Label = "Notebook";
            this.notebookDropDown.Name = "notebookDropDown";
            this.notebookDropDown.ShowImage = true;
            // 
            // stackDropDown
            // 
            this.stackDropDown.Image = global::Chronicy.Excel.Properties.Resources.IconStack;
            this.stackDropDown.Label = "Stack";
            this.stackDropDown.Name = "stackDropDown";
            this.stackDropDown.ShowImage = true;
            // 
            // showCompatibleCheckBox
            // 
            this.showCompatibleCheckBox.Label = "Only Show Compatible";
            this.showCompatibleCheckBox.Name = "showCompatibleCheckBox";
            // 
            // newNotebookButton
            // 
            this.newNotebookButton.Label = "New Notebook";
            this.newNotebookButton.Name = "newNotebookButton";
            this.newNotebookButton.ShowImage = true;
            this.newNotebookButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.OnNewNotebookClicked);
            // 
            // newStackButton
            // 
            this.newStackButton.Label = "New Stack";
            this.newStackButton.Name = "newStackButton";
            this.newStackButton.ShowImage = true;
            this.newStackButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.OnNewStackClicked);
            // 
            // viewAllButton
            // 
            this.viewAllButton.Label = "View All";
            this.viewAllButton.Name = "viewAllButton";
            this.viewAllButton.ShowImage = true;
            this.viewAllButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.OnViewAllClicked);
            // 
            // toolsGroup
            // 
            this.toolsGroup.Items.Add(this.trackingMenu);
            this.toolsGroup.Items.Add(this.historyGallery);
            this.toolsGroup.Items.Add(this.syncButton);
            this.toolsGroup.Label = "Tools";
            this.toolsGroup.Name = "toolsGroup";
            // 
            // trackingMenu
            // 
            this.trackingMenu.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.trackingMenu.Image = global::Chronicy.Excel.Properties.Resources.IconTracking;
            this.trackingMenu.Items.Add(this.trackWorkbookButton);
            this.trackingMenu.Items.Add(this.trackSheetButton);
            this.trackingMenu.Items.Add(this.trackCellButton);
            this.trackingMenu.Label = "Tracking";
            this.trackingMenu.Name = "trackingMenu";
            this.trackingMenu.ShowImage = true;
            // 
            // trackWorkbookButton
            // 
            this.trackWorkbookButton.Label = "Workbook";
            this.trackWorkbookButton.Name = "trackWorkbookButton";
            this.trackWorkbookButton.ShowImage = true;
            this.trackWorkbookButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.OnTrackWorkbook);
            // 
            // trackSheetButton
            // 
            this.trackSheetButton.Label = "Sheet";
            this.trackSheetButton.Name = "trackSheetButton";
            this.trackSheetButton.ShowImage = true;
            this.trackSheetButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.OnTrackSheet);
            // 
            // trackCellButton
            // 
            this.trackCellButton.Label = "Cell Range";
            this.trackCellButton.Name = "trackCellButton";
            this.trackCellButton.ShowImage = true;
            this.trackCellButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.OnTrackCellRange);
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
            // supportGroup
            // 
            this.supportGroup.Items.Add(this.helpButton);
            this.supportGroup.Items.Add(this.reportBugButton);
            this.supportGroup.Label = "Support";
            this.supportGroup.Name = "supportGroup";
            // 
            // helpButton
            // 
            this.helpButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.helpButton.Image = global::Chronicy.Excel.Properties.Resources.IconHelp;
            this.helpButton.Label = "Help";
            this.helpButton.Name = "helpButton";
            this.helpButton.ShowImage = true;
            // 
            // reportBugButton
            // 
            this.reportBugButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.reportBugButton.Image = global::Chronicy.Excel.Properties.Resources.IconBug;
            this.reportBugButton.Label = "Report a Bug";
            this.reportBugButton.Name = "reportBugButton";
            this.reportBugButton.ShowImage = true;
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
            this.extensionGroup.ResumeLayout(false);
            this.extensionGroup.PerformLayout();
            this.selectionGroup.ResumeLayout(false);
            this.selectionGroup.PerformLayout();
            this.box1.ResumeLayout(false);
            this.box1.PerformLayout();
            this.toolsGroup.ResumeLayout(false);
            this.toolsGroup.PerformLayout();
            this.supportGroup.ResumeLayout(false);
            this.supportGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab2;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup selectionGroup;
        internal Microsoft.Office.Tools.Ribbon.RibbonDropDown notebookDropDown;
        internal Microsoft.Office.Tools.Ribbon.RibbonDropDown stackDropDown;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton enableButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup toolsGroup;
        internal Microsoft.Office.Tools.Ribbon.RibbonGallery historyGallery;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton syncButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup extensionGroup;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton connectButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonMenu trackingMenu;
        internal Microsoft.Office.Tools.Ribbon.RibbonBox box1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton newNotebookButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton newStackButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton viewAllButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonCheckBox showCompatibleCheckBox;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton trackWorkbookButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton trackSheetButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton trackCellButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup supportGroup;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton helpButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton reportBugButton;
    }

    partial class ThisRibbonCollection
    {
        internal MainRibbon MainRibbon
        {
            get { return this.GetRibbon<MainRibbon>(); }
        }
    }
}
