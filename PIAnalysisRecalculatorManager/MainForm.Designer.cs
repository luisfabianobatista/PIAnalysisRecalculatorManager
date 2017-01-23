namespace PIAnalysisRecalculatorManager
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.lblStartTime = new System.Windows.Forms.Label();
            this.lblEndTime = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnRunOperation = new System.Windows.Forms.Button();
            this.btnAbort = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.lblStatusMsg = new System.Windows.Forms.Label();
            this.lblRootPath = new System.Windows.Forms.Label();
            this.grpbRecalcType = new System.Windows.Forms.GroupBox();
            this.chkbNewPIDataArchive = new System.Windows.Forms.CheckBox();
            this.rbtnRecalculate = new System.Windows.Forms.RadioButton();
            this.rbtnBackfill = new System.Windows.Forms.RadioButton();
            this.afElementFindCtrl1 = new OSIsoft.AF.UI.AFElementFindCtrl();
            this.btnDatabase = new System.Windows.Forms.Button();
            this.rtxtbMsgLog = new System.Windows.Forms.RichTextBox();
            this.numUpDownMaxSearchResults = new System.Windows.Forms.NumericUpDown();
            this.lblMaxSearchResults = new System.Windows.Forms.Label();
            this.ckbIncludeChildElements = new System.Windows.Forms.CheckBox();
            this.btnPIDataArchives = new System.Windows.Forms.Button();
            this.txtAttrFilter = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnSearchAnalysis = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnUncheckSelectedRows = new System.Windows.Forms.Button();
            this.btnCheckSelectedRows = new System.Windows.Forms.Button();
            this.btnRefreshStatusAnalyses = new System.Windows.Forms.Button();
            this.btnDisableSelectedAnalyses = new System.Windows.Forms.Button();
            this.btnEnableSelectedAnalyses = new System.Windows.Forms.Button();
            this.btnUncheckAllRows = new System.Windows.Forms.Button();
            this.btnCheckAllRows = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usersGuideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.afDtPickerEndTime = new OSIsoft.AF.UI.AFDateTimePickerCtrl();
            this.afDtPickerStartTime = new OSIsoft.AF.UI.AFDateTimePickerCtrl();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider2 = new System.Windows.Forms.ErrorProvider(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.aFAnalysisObjBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.grpbRecalcType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownMaxSearchResults)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aFAnalysisObjBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // lblStartTime
            // 
            this.lblStartTime.AutoSize = true;
            this.lblStartTime.Location = new System.Drawing.Point(3, 7);
            this.lblStartTime.Name = "lblStartTime";
            this.lblStartTime.Size = new System.Drawing.Size(55, 13);
            this.lblStartTime.TabIndex = 9;
            this.lblStartTime.Text = "Start Time";
            // 
            // lblEndTime
            // 
            this.lblEndTime.AutoSize = true;
            this.lblEndTime.Location = new System.Drawing.Point(3, 35);
            this.lblEndTime.Name = "lblEndTime";
            this.lblEndTime.Size = new System.Drawing.Size(52, 13);
            this.lblEndTime.TabIndex = 10;
            this.lblEndTime.Text = "End Time";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(6, 565);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(375, 21);
            this.progressBar1.TabIndex = 15;
            this.progressBar1.Visible = false;
            // 
            // btnRunOperation
            // 
            this.btnRunOperation.Location = new System.Drawing.Point(16, 144);
            this.btnRunOperation.Name = "btnRunOperation";
            this.btnRunOperation.Size = new System.Drawing.Size(167, 30);
            this.btnRunOperation.TabIndex = 16;
            this.btnRunOperation.Text = "Run Operation";
            this.toolTip1.SetToolTip(this.btnRunOperation, "Run the selected operation type");
            this.btnRunOperation.UseVisualStyleBackColor = true;
            this.btnRunOperation.Click += new System.EventHandler(this.btnRunOperation_Click);
            // 
            // btnAbort
            // 
            this.btnAbort.Enabled = false;
            this.btnAbort.Location = new System.Drawing.Point(202, 144);
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.Size = new System.Drawing.Size(167, 30);
            this.btnAbort.TabIndex = 17;
            this.btnAbort.Text = "Abort";
            this.toolTip1.SetToolTip(this.btnAbort, "Aborts the operation");
            this.btnAbort.UseVisualStyleBackColor = true;
            this.btnAbort.Click += new System.EventHandler(this.btnAbort_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(34, 342);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 13);
            this.lblMessage.TabIndex = 19;
            // 
            // lblStatusMsg
            // 
            this.lblStatusMsg.AutoSize = true;
            this.lblStatusMsg.Location = new System.Drawing.Point(34, 369);
            this.lblStatusMsg.Name = "lblStatusMsg";
            this.lblStatusMsg.Size = new System.Drawing.Size(0, 13);
            this.lblStatusMsg.TabIndex = 23;
            // 
            // lblRootPath
            // 
            this.lblRootPath.AutoSize = true;
            this.lblRootPath.Location = new System.Drawing.Point(9, 40);
            this.lblRootPath.Name = "lblRootPath";
            this.lblRootPath.Size = new System.Drawing.Size(79, 13);
            this.lblRootPath.TabIndex = 14;
            this.lblRootPath.Text = "Target Element";
            // 
            // grpbRecalcType
            // 
            this.grpbRecalcType.Controls.Add(this.chkbNewPIDataArchive);
            this.grpbRecalcType.Controls.Add(this.rbtnRecalculate);
            this.grpbRecalcType.Controls.Add(this.rbtnBackfill);
            this.grpbRecalcType.Location = new System.Drawing.Point(41, 63);
            this.grpbRecalcType.Name = "grpbRecalcType";
            this.grpbRecalcType.Size = new System.Drawing.Size(303, 69);
            this.grpbRecalcType.TabIndex = 29;
            this.grpbRecalcType.TabStop = false;
            this.grpbRecalcType.Text = "Operation Type";
            // 
            // chkbNewPIDataArchive
            // 
            this.chkbNewPIDataArchive.AutoSize = true;
            this.chkbNewPIDataArchive.Enabled = false;
            this.chkbNewPIDataArchive.Location = new System.Drawing.Point(106, 43);
            this.chkbNewPIDataArchive.Name = "chkbNewPIDataArchive";
            this.chkbNewPIDataArchive.Size = new System.Drawing.Size(189, 17);
            this.chkbNewPIDataArchive.TabIndex = 31;
            this.chkbNewPIDataArchive.Text = "PI Data Archive 2016 R2 or newer";
            this.chkbNewPIDataArchive.UseVisualStyleBackColor = true;
            // 
            // rbtnRecalculate
            // 
            this.rbtnRecalculate.AutoSize = true;
            this.rbtnRecalculate.Location = new System.Drawing.Point(17, 43);
            this.rbtnRecalculate.Name = "rbtnRecalculate";
            this.rbtnRecalculate.Size = new System.Drawing.Size(82, 17);
            this.rbtnRecalculate.TabIndex = 30;
            this.rbtnRecalculate.Text = "Recalculate";
            this.rbtnRecalculate.UseVisualStyleBackColor = true;
            this.rbtnRecalculate.CheckedChanged += new System.EventHandler(this.rbtnRecalculate_CheckedChanged);
            // 
            // rbtnBackfill
            // 
            this.rbtnBackfill.AutoSize = true;
            this.rbtnBackfill.Checked = true;
            this.rbtnBackfill.Location = new System.Drawing.Point(17, 20);
            this.rbtnBackfill.Name = "rbtnBackfill";
            this.rbtnBackfill.Size = new System.Drawing.Size(125, 17);
            this.rbtnBackfill.TabIndex = 0;
            this.rbtnBackfill.TabStop = true;
            this.rbtnBackfill.Text = "Backfill (fill gaps only)";
            this.rbtnBackfill.UseVisualStyleBackColor = true;
            this.rbtnBackfill.CheckedChanged += new System.EventHandler(this.rbtnTagValuesOnly_CheckedChanged);
            // 
            // afElementFindCtrl1
            // 
            this.afElementFindCtrl1.AllowEmptyText = true;
            this.afElementFindCtrl1.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.afElementFindCtrl1.Location = new System.Drawing.Point(89, 35);
            this.afElementFindCtrl1.Margin = new System.Windows.Forms.Padding(4);
            this.afElementFindCtrl1.MinimumSize = new System.Drawing.Size(0, 22);
            this.afElementFindCtrl1.Name = "afElementFindCtrl1";
            this.afElementFindCtrl1.ShowFindButton = true;
            this.afElementFindCtrl1.Size = new System.Drawing.Size(405, 24);
            this.afElementFindCtrl1.TabIndex = 32;
            this.toolTip1.SetToolTip(this.afElementFindCtrl1, "Path of the root element used in the analyses search");
            this.afElementFindCtrl1.AFElementUpdated += new OSIsoft.AF.UI.AFElementFindCtrl.AFElementUpdatedEventHandler(this.afElementFindCtrl1_AFElementUpdated);
            // 
            // btnDatabase
            // 
            this.btnDatabase.Location = new System.Drawing.Point(501, 35);
            this.btnDatabase.Name = "btnDatabase";
            this.btnDatabase.Size = new System.Drawing.Size(100, 23);
            this.btnDatabase.TabIndex = 33;
            this.btnDatabase.Text = "Select Database";
            this.toolTip1.SetToolTip(this.btnDatabase, "Selects the AF database of interest");
            this.btnDatabase.UseVisualStyleBackColor = true;
            this.btnDatabase.Click += new System.EventHandler(this.btnDatabase_Click);
            // 
            // rtxtbMsgLog
            // 
            this.rtxtbMsgLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtxtbMsgLog.DetectUrls = false;
            this.rtxtbMsgLog.HideSelection = false;
            this.rtxtbMsgLog.Location = new System.Drawing.Point(6, 190);
            this.rtxtbMsgLog.Name = "rtxtbMsgLog";
            this.rtxtbMsgLog.ReadOnly = true;
            this.rtxtbMsgLog.Size = new System.Drawing.Size(370, 369);
            this.rtxtbMsgLog.TabIndex = 34;
            this.rtxtbMsgLog.Text = "";
            // 
            // numUpDownMaxSearchResults
            // 
            this.numUpDownMaxSearchResults.Location = new System.Drawing.Point(729, 63);
            this.numUpDownMaxSearchResults.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numUpDownMaxSearchResults.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numUpDownMaxSearchResults.Name = "numUpDownMaxSearchResults";
            this.numUpDownMaxSearchResults.Size = new System.Drawing.Size(100, 20);
            this.numUpDownMaxSearchResults.TabIndex = 35;
            this.toolTip1.SetToolTip(this.numUpDownMaxSearchResults, "Maximum number of elements to be returned in the search");
            this.numUpDownMaxSearchResults.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numUpDownMaxSearchResults.ValueChanged += new System.EventHandler(this.numUpDownMaxSearchResults_ValueChanged);
            // 
            // lblMaxSearchResults
            // 
            this.lblMaxSearchResults.AutoSize = true;
            this.lblMaxSearchResults.Location = new System.Drawing.Point(614, 67);
            this.lblMaxSearchResults.Name = "lblMaxSearchResults";
            this.lblMaxSearchResults.Size = new System.Drawing.Size(105, 13);
            this.lblMaxSearchResults.TabIndex = 36;
            this.lblMaxSearchResults.Text = "Max. Search Results";
            // 
            // ckbIncludeChildElements
            // 
            this.ckbIncludeChildElements.AutoSize = true;
            this.ckbIncludeChildElements.Location = new System.Drawing.Point(478, 66);
            this.ckbIncludeChildElements.Name = "ckbIncludeChildElements";
            this.ckbIncludeChildElements.Size = new System.Drawing.Size(133, 17);
            this.ckbIncludeChildElements.TabIndex = 40;
            this.ckbIncludeChildElements.Text = "Include Child Elements";
            this.toolTip1.SetToolTip(this.ckbIncludeChildElements, "Check if the analysis of the child elemens must be included");
            this.ckbIncludeChildElements.UseVisualStyleBackColor = true;
            // 
            // btnPIDataArchives
            // 
            this.btnPIDataArchives.Location = new System.Drawing.Point(614, 35);
            this.btnPIDataArchives.Name = "btnPIDataArchives";
            this.btnPIDataArchives.Size = new System.Drawing.Size(100, 23);
            this.btnPIDataArchives.TabIndex = 42;
            this.btnPIDataArchives.Text = "PI Data Archives";
            this.toolTip1.SetToolTip(this.btnPIDataArchives, "Sets the PI Data Archive connectios");
            this.btnPIDataArchives.UseVisualStyleBackColor = true;
            this.btnPIDataArchives.Click += new System.EventHandler(this.btnPIDataArchives_Click);
            // 
            // txtAttrFilter
            // 
            this.txtAttrFilter.Location = new System.Drawing.Point(89, 63);
            this.txtAttrFilter.Name = "txtAttrFilter";
            this.txtAttrFilter.Size = new System.Drawing.Size(360, 20);
            this.txtAttrFilter.TabIndex = 43;
            this.toolTip1.SetToolTip(this.txtAttrFilter, "Filter expression used in the analyses search");
            this.txtAttrFilter.UseWaitCursor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 44;
            this.label1.Text = "Path Filter";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(3, 93);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(917, 466);
            this.dataGridView1.TabIndex = 45;
            this.dataGridView1.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_ColumnHeaderMouseClick);
            this.dataGridView1.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView1_DataBindingComplete);
            // 
            // btnSearchAnalysis
            // 
            this.btnSearchAnalysis.Location = new System.Drawing.Point(729, 35);
            this.btnSearchAnalysis.Name = "btnSearchAnalysis";
            this.btnSearchAnalysis.Size = new System.Drawing.Size(100, 23);
            this.btnSearchAnalysis.TabIndex = 46;
            this.btnSearchAnalysis.Text = "Search";
            this.toolTip1.SetToolTip(this.btnSearchAnalysis, "Search the analysis according to the root element and path filter");
            this.btnSearchAnalysis.UseVisualStyleBackColor = true;
            this.btnSearchAnalysis.Click += new System.EventHandler(this.btnSearchAnalysis_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.Controls.Add(this.btnUncheckSelectedRows);
            this.splitContainer1.Panel1.Controls.Add(this.btnCheckSelectedRows);
            this.splitContainer1.Panel1.Controls.Add(this.btnRefreshStatusAnalyses);
            this.splitContainer1.Panel1.Controls.Add(this.btnDisableSelectedAnalyses);
            this.splitContainer1.Panel1.Controls.Add(this.btnEnableSelectedAnalyses);
            this.splitContainer1.Panel1.Controls.Add(this.btnUncheckAllRows);
            this.splitContainer1.Panel1.Controls.Add(this.btnCheckAllRows);
            this.splitContainer1.Panel1.Controls.Add(this.lblRootPath);
            this.splitContainer1.Panel1.Controls.Add(this.btnSearchAnalysis);
            this.splitContainer1.Panel1.Controls.Add(this.numUpDownMaxSearchResults);
            this.splitContainer1.Panel1.Controls.Add(this.afElementFindCtrl1);
            this.splitContainer1.Panel1.Controls.Add(this.lblMaxSearchResults);
            this.splitContainer1.Panel1.Controls.Add(this.dataGridView1);
            this.splitContainer1.Panel1.Controls.Add(this.ckbIncludeChildElements);
            this.splitContainer1.Panel1.Controls.Add(this.txtAttrFilter);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.btnDatabase);
            this.splitContainer1.Panel1.Controls.Add(this.btnPIDataArchives);
            this.splitContainer1.Panel1.Controls.Add(this.menuStrip1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.Controls.Add(this.afDtPickerEndTime);
            this.splitContainer1.Panel2.Controls.Add(this.afDtPickerStartTime);
            this.splitContainer1.Panel2.Controls.Add(this.lblEndTime);
            this.splitContainer1.Panel2.Controls.Add(this.rtxtbMsgLog);
            this.splitContainer1.Panel2.Controls.Add(this.lblStartTime);
            this.splitContainer1.Panel2.Controls.Add(this.progressBar1);
            this.splitContainer1.Panel2.Controls.Add(this.grpbRecalcType);
            this.splitContainer1.Panel2.Controls.Add(this.btnRunOperation);
            this.splitContainer1.Panel2.Controls.Add(this.btnAbort);
            this.splitContainer1.Size = new System.Drawing.Size(1314, 593);
            this.splitContainer1.SplitterDistance = 927;
            this.splitContainer1.TabIndex = 47;
            // 
            // btnUncheckSelectedRows
            // 
            this.btnUncheckSelectedRows.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnUncheckSelectedRows.Location = new System.Drawing.Point(329, 565);
            this.btnUncheckSelectedRows.Name = "btnUncheckSelectedRows";
            this.btnUncheckSelectedRows.Size = new System.Drawing.Size(96, 21);
            this.btnUncheckSelectedRows.TabIndex = 55;
            this.btnUncheckSelectedRows.Text = "Deselect";
            this.toolTip1.SetToolTip(this.btnUncheckSelectedRows, "Deselects the highlighted analyses only");
            this.btnUncheckSelectedRows.UseVisualStyleBackColor = true;
            this.btnUncheckSelectedRows.Click += new System.EventHandler(this.btnUncheckSelectedRows_Click);
            // 
            // btnCheckSelectedRows
            // 
            this.btnCheckSelectedRows.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCheckSelectedRows.Location = new System.Drawing.Point(227, 565);
            this.btnCheckSelectedRows.Name = "btnCheckSelectedRows";
            this.btnCheckSelectedRows.Size = new System.Drawing.Size(96, 21);
            this.btnCheckSelectedRows.TabIndex = 54;
            this.btnCheckSelectedRows.Text = "Select";
            this.toolTip1.SetToolTip(this.btnCheckSelectedRows, "Selected the highlighted analyses only");
            this.btnCheckSelectedRows.UseVisualStyleBackColor = true;
            this.btnCheckSelectedRows.Click += new System.EventHandler(this.btnCheckSelectedRows_Click);
            // 
            // btnRefreshStatusAnalyses
            // 
            this.btnRefreshStatusAnalyses.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnRefreshStatusAnalyses.Location = new System.Drawing.Point(789, 565);
            this.btnRefreshStatusAnalyses.Name = "btnRefreshStatusAnalyses";
            this.btnRefreshStatusAnalyses.Size = new System.Drawing.Size(96, 21);
            this.btnRefreshStatusAnalyses.TabIndex = 53;
            this.btnRefreshStatusAnalyses.Text = "Refresh Status";
            this.toolTip1.SetToolTip(this.btnRefreshStatusAnalyses, "Refresh the status information of all analyses");
            this.btnRefreshStatusAnalyses.UseVisualStyleBackColor = true;
            this.btnRefreshStatusAnalyses.Click += new System.EventHandler(this.btnRefreshStatusAnalyses_Click);
            // 
            // btnDisableSelectedAnalyses
            // 
            this.btnDisableSelectedAnalyses.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnDisableSelectedAnalyses.Location = new System.Drawing.Point(687, 565);
            this.btnDisableSelectedAnalyses.Name = "btnDisableSelectedAnalyses";
            this.btnDisableSelectedAnalyses.Size = new System.Drawing.Size(96, 21);
            this.btnDisableSelectedAnalyses.TabIndex = 52;
            this.btnDisableSelectedAnalyses.Text = "Disable";
            this.toolTip1.SetToolTip(this.btnDisableSelectedAnalyses, "Disables the selected analyses");
            this.btnDisableSelectedAnalyses.UseVisualStyleBackColor = true;
            this.btnDisableSelectedAnalyses.Click += new System.EventHandler(this.btnDisableSelectedAnalyses_Click);
            // 
            // btnEnableSelectedAnalyses
            // 
            this.btnEnableSelectedAnalyses.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnEnableSelectedAnalyses.Location = new System.Drawing.Point(585, 565);
            this.btnEnableSelectedAnalyses.Name = "btnEnableSelectedAnalyses";
            this.btnEnableSelectedAnalyses.Size = new System.Drawing.Size(96, 21);
            this.btnEnableSelectedAnalyses.TabIndex = 51;
            this.btnEnableSelectedAnalyses.Text = "Enable";
            this.toolTip1.SetToolTip(this.btnEnableSelectedAnalyses, "Enables the selected analyses");
            this.btnEnableSelectedAnalyses.UseVisualStyleBackColor = true;
            this.btnEnableSelectedAnalyses.Click += new System.EventHandler(this.btnEnableSelectedAnalyses_Click);
            // 
            // btnUncheckAllRows
            // 
            this.btnUncheckAllRows.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnUncheckAllRows.Location = new System.Drawing.Point(125, 565);
            this.btnUncheckAllRows.Name = "btnUncheckAllRows";
            this.btnUncheckAllRows.Size = new System.Drawing.Size(96, 21);
            this.btnUncheckAllRows.TabIndex = 50;
            this.btnUncheckAllRows.Text = "Deselect All";
            this.toolTip1.SetToolTip(this.btnUncheckAllRows, "Deselects all analyses");
            this.btnUncheckAllRows.UseVisualStyleBackColor = true;
            this.btnUncheckAllRows.Click += new System.EventHandler(this.btnUncheckAllRows_Click);
            // 
            // btnCheckAllRows
            // 
            this.btnCheckAllRows.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCheckAllRows.Location = new System.Drawing.Point(24, 565);
            this.btnCheckAllRows.Name = "btnCheckAllRows";
            this.btnCheckAllRows.Size = new System.Drawing.Size(96, 21);
            this.btnCheckAllRows.TabIndex = 49;
            this.btnCheckAllRows.Text = "Select All";
            this.toolTip1.SetToolTip(this.btnCheckAllRows, "Selects all analyses in the list");
            this.btnCheckAllRows.UseVisualStyleBackColor = true;
            this.btnCheckAllRows.Click += new System.EventHandler(this.btnCheckAllRows_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(923, 24);
            this.menuStrip1.TabIndex = 56;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.usersGuideToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // usersGuideToolStripMenuItem
            // 
            this.usersGuideToolStripMenuItem.Name = "usersGuideToolStripMenuItem";
            this.usersGuideToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.usersGuideToolStripMenuItem.Text = "User\'s Guide";
            this.usersGuideToolStripMenuItem.Click += new System.EventHandler(this.usersGuideToolStripMenuItem_Click);
            // 
            // afDtPickerEndTime
            // 
            this.afDtPickerEndTime.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.afDtPickerEndTime.ImageIndex = 1;
            this.afDtPickerEndTime.Location = new System.Drawing.Point(66, 32);
            this.afDtPickerEndTime.Margin = new System.Windows.Forms.Padding(4);
            this.afDtPickerEndTime.MinimumSize = new System.Drawing.Size(0, 22);
            this.afDtPickerEndTime.Name = "afDtPickerEndTime";
            this.afDtPickerEndTime.Size = new System.Drawing.Size(243, 24);
            this.afDtPickerEndTime.TabIndex = 36;
            this.afDtPickerEndTime.Validated += new System.EventHandler(this.afDtPickerEndTime_Validated);
            // 
            // afDtPickerStartTime
            // 
            this.afDtPickerStartTime.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.afDtPickerStartTime.ImageIndex = 1;
            this.afDtPickerStartTime.Location = new System.Drawing.Point(66, 5);
            this.afDtPickerStartTime.Margin = new System.Windows.Forms.Padding(4);
            this.afDtPickerStartTime.MinimumSize = new System.Drawing.Size(0, 22);
            this.afDtPickerStartTime.Name = "afDtPickerStartTime";
            this.afDtPickerStartTime.Size = new System.Drawing.Size(243, 24);
            this.afDtPickerStartTime.TabIndex = 35;
            this.afDtPickerStartTime.Validated += new System.EventHandler(this.afDtPickerStartTime_Validated);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // errorProvider2
            // 
            this.errorProvider2.ContainerControl = this;
            // 
            // aFAnalysisObjBindingSource
            // 
            this.aFAnalysisObjBindingSource.DataSource = typeof(PIAnalysisRecalculatorManager.AFAnalysisObj);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1314, 593);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.lblStatusMsg);
            this.Controls.Add(this.lblMessage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "PI Analysis Recalculator Manager";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.grpbRecalcType.ResumeLayout(false);
            this.grpbRecalcType.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownMaxSearchResults)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aFAnalysisObjBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblStartTime;
        private System.Windows.Forms.Label lblEndTime;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnRunOperation;
        private System.Windows.Forms.Button btnAbort;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label lblStatusMsg;
        private System.Windows.Forms.Label lblRootPath;
        private System.Windows.Forms.GroupBox grpbRecalcType;
        private System.Windows.Forms.RadioButton rbtnRecalculate;
        private System.Windows.Forms.RadioButton rbtnBackfill;
        private OSIsoft.AF.UI.AFElementFindCtrl afElementFindCtrl1;
        private System.Windows.Forms.Button btnDatabase;
        private System.Windows.Forms.RichTextBox rtxtbMsgLog;
        private System.Windows.Forms.NumericUpDown numUpDownMaxSearchResults;
        private System.Windows.Forms.Label lblMaxSearchResults;
        private System.Windows.Forms.CheckBox ckbIncludeChildElements;
        private System.Windows.Forms.Button btnPIDataArchives;
        private System.Windows.Forms.TextBox txtAttrFilter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnSearchAnalysis;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnUncheckAllRows;
        private System.Windows.Forms.Button btnCheckAllRows;
        private System.Windows.Forms.CheckBox chkbNewPIDataArchive;
        private OSIsoft.AF.UI.AFDateTimePickerCtrl afDtPickerEndTime;
        private OSIsoft.AF.UI.AFDateTimePickerCtrl afDtPickerStartTime;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ErrorProvider errorProvider2;
        private System.Windows.Forms.Button btnDisableSelectedAnalyses;
        private System.Windows.Forms.Button btnEnableSelectedAnalyses;
        private System.Windows.Forms.Button btnRefreshStatusAnalyses;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnCheckSelectedRows;
        private System.Windows.Forms.Button btnUncheckSelectedRows;
        private System.Windows.Forms.BindingSource aFAnalysisObjBindingSource;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem usersGuideToolStripMenuItem;
    }
}

