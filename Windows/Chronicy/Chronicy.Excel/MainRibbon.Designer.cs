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
            this.dataSourceDropDown = this.Factory.CreateRibbonDropDown();
            this.notebookDropDown = this.Factory.CreateRibbonDropDown();
            this.stackDropDown = this.Factory.CreateRibbonDropDown();
            this.newNotebookButton = this.Factory.CreateRibbonButton();
            this.newStackButton = this.Factory.CreateRibbonButton();
            this.viewAllButton = this.Factory.CreateRibbonButton();
            this.trackingGroup = this.Factory.CreateRibbonGroup();
            this.workbookMenu = this.Factory.CreateRibbonMenu();
            this.workbookEnableCheckBox = this.Factory.CreateRibbonCheckBox();
            this.trackWorkbookButton = this.Factory.CreateRibbonButton();
            this.workbookCurrentLabel = this.Factory.CreateRibbonButton();
            this.sheetMenu = this.Factory.CreateRibbonMenu();
            this.sheetEnableCheckBox = this.Factory.CreateRibbonCheckBox();
            this.trackSheetButton = this.Factory.CreateRibbonButton();
            this.sheetCurrentLabel = this.Factory.CreateRibbonButton();
            this.cellsMenu = this.Factory.CreateRibbonMenu();
            this.cellsEnableCheckBox = this.Factory.CreateRibbonCheckBox();
            this.trackCellButton = this.Factory.CreateRibbonButton();
            this.cellsCurrentLabel = this.Factory.CreateRibbonButton();
            this.otherMenu = this.Factory.CreateRibbonMenu();
            this.timeWorkedCheckBox = this.Factory.CreateRibbonCheckBox();
            this.toolsGroup = this.Factory.CreateRibbonGroup();
            this.historyMenu = this.Factory.CreateRibbonMenu();
            this.syncButton = this.Factory.CreateRibbonButton();
            this.loginButton = this.Factory.CreateRibbonButton();
            this.supportGroup = this.Factory.CreateRibbonGroup();
            this.helpButton = this.Factory.CreateRibbonButton();
            this.reportBugButton = this.Factory.CreateRibbonButton();
            this.viewGitHubButton = this.Factory.CreateRibbonButton();
            this.optionsButton = this.Factory.CreateRibbonButton();
            this.tab1.SuspendLayout();
            this.tab2.SuspendLayout();
            this.extensionGroup.SuspendLayout();
            this.selectionGroup.SuspendLayout();
            this.box1.SuspendLayout();
            this.trackingGroup.SuspendLayout();
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
            this.tab2.Groups.Add(this.trackingGroup);
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
            this.connectButton.Image = global::Chronicy.Excel.Properties.Resources.IconConnect32;
            this.connectButton.Label = "Connect";
            this.connectButton.Name = "connectButton";
            this.connectButton.ShowImage = true;
            this.connectButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.OnConnectClicked);
            // 
            // enableButton
            // 
            this.enableButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.enableButton.Image = global::Chronicy.Excel.Properties.Resources.IconEnable32;
            this.enableButton.Label = "Disabled";
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
            this.box1.Items.Add(this.dataSourceDropDown);
            this.box1.Items.Add(this.notebookDropDown);
            this.box1.Items.Add(this.stackDropDown);
            this.box1.Name = "box1";
            // 
            // dataSourceDropDown
            // 
            this.dataSourceDropDown.Image = global::Chronicy.Excel.Properties.Resources.IconDataSource32;
            this.dataSourceDropDown.Label = "Data Source";
            this.dataSourceDropDown.Name = "dataSourceDropDown";
            this.dataSourceDropDown.ShowImage = true;
            this.dataSourceDropDown.SelectionChanged += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.OnDataSourceChanged);
            // 
            // notebookDropDown
            // 
            this.notebookDropDown.Image = global::Chronicy.Excel.Properties.Resources.IconNotebook32;
            this.notebookDropDown.Label = "Notebook";
            this.notebookDropDown.Name = "notebookDropDown";
            this.notebookDropDown.ShowImage = true;
            this.notebookDropDown.SelectionChanged += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.OnNotebookChanged);
            // 
            // stackDropDown
            // 
            this.stackDropDown.Image = global::Chronicy.Excel.Properties.Resources.IconStack32;
            this.stackDropDown.Label = "Stack";
            this.stackDropDown.Name = "stackDropDown";
            this.stackDropDown.ShowImage = true;
            this.stackDropDown.SelectionChanged += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.OnStackChanged);
            // 
            // newNotebookButton
            // 
            this.newNotebookButton.Image = global::Chronicy.Excel.Properties.Resources.IconNewNotebook32;
            this.newNotebookButton.Label = "New Notebook";
            this.newNotebookButton.Name = "newNotebookButton";
            this.newNotebookButton.ShowImage = true;
            this.newNotebookButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.OnNewNotebookClicked);
            // 
            // newStackButton
            // 
            this.newStackButton.Image = global::Chronicy.Excel.Properties.Resources.IconNewStack32;
            this.newStackButton.Label = "New Stack";
            this.newStackButton.Name = "newStackButton";
            this.newStackButton.ShowImage = true;
            this.newStackButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.OnNewStackClicked);
            // 
            // viewAllButton
            // 
            this.viewAllButton.Image = global::Chronicy.Excel.Properties.Resources.IconViewAll32;
            this.viewAllButton.Label = "View All";
            this.viewAllButton.Name = "viewAllButton";
            this.viewAllButton.ShowImage = true;
            this.viewAllButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.OnViewAllClicked);
            // 
            // trackingGroup
            // 
            this.trackingGroup.Items.Add(this.workbookMenu);
            this.trackingGroup.Items.Add(this.sheetMenu);
            this.trackingGroup.Items.Add(this.cellsMenu);
            this.trackingGroup.Items.Add(this.otherMenu);
            this.trackingGroup.Label = "Tracking";
            this.trackingGroup.Name = "trackingGroup";
            // 
            // workbookMenu
            // 
            this.workbookMenu.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.workbookMenu.Image = global::Chronicy.Excel.Properties.Resources.IconWorkbook32;
            this.workbookMenu.Items.Add(this.workbookEnableCheckBox);
            this.workbookMenu.Items.Add(this.trackWorkbookButton);
            this.workbookMenu.Items.Add(this.workbookCurrentLabel);
            this.workbookMenu.Label = "Workbook";
            this.workbookMenu.Name = "workbookMenu";
            this.workbookMenu.ShowImage = true;
            // 
            // workbookEnableCheckBox
            // 
            this.workbookEnableCheckBox.Label = "Enabled";
            this.workbookEnableCheckBox.Name = "workbookEnableCheckBox";
            // 
            // trackWorkbookButton
            // 
            this.trackWorkbookButton.Label = "Track Current Workbook";
            this.trackWorkbookButton.Name = "trackWorkbookButton";
            this.trackWorkbookButton.ShowImage = true;
            this.trackWorkbookButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.OnTrackWorkbook);
            // 
            // workbookCurrentLabel
            // 
            this.workbookCurrentLabel.Enabled = false;
            this.workbookCurrentLabel.Label = "Nothing Tracked";
            this.workbookCurrentLabel.Name = "workbookCurrentLabel";
            this.workbookCurrentLabel.ShowImage = true;
            // 
            // sheetMenu
            // 
            this.sheetMenu.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.sheetMenu.Image = global::Chronicy.Excel.Properties.Resources.IconSheets32;
            this.sheetMenu.Items.Add(this.sheetEnableCheckBox);
            this.sheetMenu.Items.Add(this.trackSheetButton);
            this.sheetMenu.Items.Add(this.sheetCurrentLabel);
            this.sheetMenu.Label = "Sheet";
            this.sheetMenu.Name = "sheetMenu";
            this.sheetMenu.ShowImage = true;
            // 
            // sheetEnableCheckBox
            // 
            this.sheetEnableCheckBox.Label = "Enabled";
            this.sheetEnableCheckBox.Name = "sheetEnableCheckBox";
            // 
            // trackSheetButton
            // 
            this.trackSheetButton.Label = "Change Tracked Sheet";
            this.trackSheetButton.Name = "trackSheetButton";
            this.trackSheetButton.ShowImage = true;
            this.trackSheetButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.OnTrackSheet);
            // 
            // sheetCurrentLabel
            // 
            this.sheetCurrentLabel.Enabled = false;
            this.sheetCurrentLabel.Label = "Nothing Tracked";
            this.sheetCurrentLabel.Name = "sheetCurrentLabel";
            this.sheetCurrentLabel.ShowImage = true;
            // 
            // cellsMenu
            // 
            this.cellsMenu.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.cellsMenu.Image = global::Chronicy.Excel.Properties.Resources.IconCells32;
            this.cellsMenu.Items.Add(this.cellsEnableCheckBox);
            this.cellsMenu.Items.Add(this.trackCellButton);
            this.cellsMenu.Items.Add(this.cellsCurrentLabel);
            this.cellsMenu.Label = "Cells";
            this.cellsMenu.Name = "cellsMenu";
            this.cellsMenu.ShowImage = true;
            // 
            // cellsEnableCheckBox
            // 
            this.cellsEnableCheckBox.Label = "Enabled";
            this.cellsEnableCheckBox.Name = "cellsEnableCheckBox";
            // 
            // trackCellButton
            // 
            this.trackCellButton.Label = "Change Tracked Cells";
            this.trackCellButton.Name = "trackCellButton";
            this.trackCellButton.ShowImage = true;
            this.trackCellButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.OnTrackCellRange);
            // 
            // cellsCurrentLabel
            // 
            this.cellsCurrentLabel.Enabled = false;
            this.cellsCurrentLabel.Label = "Nothing Tracked";
            this.cellsCurrentLabel.Name = "cellsCurrentLabel";
            this.cellsCurrentLabel.ShowImage = true;
            // 
            // otherMenu
            // 
            this.otherMenu.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.otherMenu.Image = global::Chronicy.Excel.Properties.Resources.IconOther32;
            this.otherMenu.Items.Add(this.timeWorkedCheckBox);
            this.otherMenu.Label = "Other";
            this.otherMenu.Name = "otherMenu";
            this.otherMenu.ShowImage = true;
            // 
            // timeWorkedCheckBox
            // 
            this.timeWorkedCheckBox.Label = "Time Worked";
            this.timeWorkedCheckBox.Name = "timeWorkedCheckBox";
            // 
            // toolsGroup
            // 
            this.toolsGroup.Items.Add(this.historyMenu);
            this.toolsGroup.Items.Add(this.syncButton);
            this.toolsGroup.Items.Add(this.loginButton);
            this.toolsGroup.Label = "Tools";
            this.toolsGroup.Name = "toolsGroup";
            // 
            // historyMenu
            // 
            this.historyMenu.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.historyMenu.Dynamic = true;
            this.historyMenu.Image = global::Chronicy.Excel.Properties.Resources.IconHistory32;
            this.historyMenu.ItemSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.historyMenu.Label = "History";
            this.historyMenu.Name = "historyMenu";
            this.historyMenu.ShowImage = true;
            this.historyMenu.ItemsLoading += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.OnHistoryMenuLoad);
            // 
            // syncButton
            // 
            this.syncButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.syncButton.Image = global::Chronicy.Excel.Properties.Resources.IconSync32;
            this.syncButton.Label = "Sync";
            this.syncButton.Name = "syncButton";
            this.syncButton.ShowImage = true;
            this.syncButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.OnSyncClicked);
            // 
            // loginButton
            // 
            this.loginButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.loginButton.Image = global::Chronicy.Excel.Properties.Resources.IconLogin32;
            this.loginButton.Label = "Login";
            this.loginButton.Name = "loginButton";
            this.loginButton.ShowImage = true;
            this.loginButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.OnLoginClicked);
            // 
            // supportGroup
            // 
            this.supportGroup.Items.Add(this.helpButton);
            this.supportGroup.Items.Add(this.reportBugButton);
            this.supportGroup.Items.Add(this.viewGitHubButton);
            this.supportGroup.Label = "Support";
            this.supportGroup.Name = "supportGroup";
            // 
            // helpButton
            // 
            this.helpButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.helpButton.Image = global::Chronicy.Excel.Properties.Resources.IconHelp32;
            this.helpButton.Label = "Help";
            this.helpButton.Name = "helpButton";
            this.helpButton.ShowImage = true;
            this.helpButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.OnHelpClicked);
            // 
            // reportBugButton
            // 
            this.reportBugButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.reportBugButton.Image = global::Chronicy.Excel.Properties.Resources.IconBug32;
            this.reportBugButton.Label = "Report a Bug";
            this.reportBugButton.Name = "reportBugButton";
            this.reportBugButton.ShowImage = true;
            this.reportBugButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.OnReportBugClicked);
            // 
            // viewGitHubButton
            // 
            this.viewGitHubButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.viewGitHubButton.Image = global::Chronicy.Excel.Properties.Resources.IconGitHub32;
            this.viewGitHubButton.Label = "View On GitHub";
            this.viewGitHubButton.Name = "viewGitHubButton";
            this.viewGitHubButton.ShowImage = true;
            this.viewGitHubButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.OnViewGitHubClicked);
            // 
            // optionsButton
            // 
            this.optionsButton.Label = "Chronicy Options";
            this.optionsButton.Name = "optionsButton";
            this.optionsButton.ShowImage = true;
            // 
            // MainRibbon
            // 
            this.Name = "MainRibbon";
            // 
            // MainRibbon.OfficeMenu
            // 
            this.OfficeMenu.Items.Add(this.optionsButton);
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
            this.trackingGroup.ResumeLayout(false);
            this.trackingGroup.PerformLayout();
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
        internal Microsoft.Office.Tools.Ribbon.RibbonButton syncButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup extensionGroup;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton connectButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonBox box1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton newNotebookButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton newStackButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton viewAllButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton trackWorkbookButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton trackSheetButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton trackCellButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup supportGroup;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton helpButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton reportBugButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton optionsButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton viewGitHubButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonMenu workbookMenu;
        internal Microsoft.Office.Tools.Ribbon.RibbonMenu sheetMenu;
        internal Microsoft.Office.Tools.Ribbon.RibbonMenu cellsMenu;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton workbookCurrentLabel;
        internal Microsoft.Office.Tools.Ribbon.RibbonCheckBox workbookEnableCheckBox;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup trackingGroup;
        internal Microsoft.Office.Tools.Ribbon.RibbonMenu otherMenu;
        internal Microsoft.Office.Tools.Ribbon.RibbonCheckBox timeWorkedCheckBox;
        internal Microsoft.Office.Tools.Ribbon.RibbonCheckBox sheetEnableCheckBox;
        internal Microsoft.Office.Tools.Ribbon.RibbonCheckBox cellsEnableCheckBox;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton sheetCurrentLabel;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton cellsCurrentLabel;
        internal Microsoft.Office.Tools.Ribbon.RibbonMenu historyMenu;
        internal Microsoft.Office.Tools.Ribbon.RibbonDropDown dataSourceDropDown;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton loginButton;
    }

    partial class ThisRibbonCollection
    {
        internal MainRibbon MainRibbon
        {
            get { return this.GetRibbon<MainRibbon>(); }
        }
    }
}
