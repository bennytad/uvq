using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using ZedGraph;
using Microsoft.Win32;
using System.Security.Cryptography;

namespace UV_Quant
{
	/// <summary>
	/// Summary description for MainConsole.
	/// </summary>
    public class MainConsole : System.Windows.Forms.Form
    {
        //This project is based on the Oxygen processor
        //OxygenProcessor processor = new OxygenProcessor();
        PLSProcessor processor = new PLSProcessor();

        //form related variables
        private int form_width_min = 0;
        private int form_height_min = 0;
        public int current_tab_index = 0;
        public DataSummary current_ds = null;
        public bool enable_password = true;
        public bool light_on = false;
        public bool table_painted = false;

        //static variable
        public static bool calb_aborted = false;

        public Image green_circle = Bitmap.FromFile(@"green_circle.gif");
        public Image red_circle = Bitmap.FromFile(@"red_circle.gif");
        public Image yellow_circle = Bitmap.FromFile(@"yellow_circle.gif");

        private IContainer components;
        private FolderBrowserDialog data_folder_source;
        private OpenFileDialog bkgd_file_source;
        private StatusStrip statusBar;
        private ToolStripStatusLabel scan_status;
        private ToolStripStatusLabel file_count;
        private ToolStripStatusLabel current_time;
        private ToolStripStatusLabel current_status;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private Button btnNotes;
        private TabPage tabPage2;
        private GroupBox repControlBox;
        private System.Windows.Forms.Label label2;
        private Button button1;
        private TextBox bgkdFile;
        private Button button3;
        private TextBox dataFolder;
        private Button button2;
        private System.Windows.Forms.Label label3;
        private TabPage tabPage3;
        private System.Windows.Forms.Label rep_integration_period;
        private DataGridView rep_datagrid;
        private ZedGraphControl rep_graph;
        private DataGridView cm_datagrid;
        private ZedGraphControl cm_graph;
        private ZedGraphControl gas_graph;
        private GroupBox groupBox3;
        private GroupBox groupBox5;
        private GroupBox groupBox6;
        private GroupBox groupBox2;
        private GroupBox groupBox4;
        private Button btnSpect;
        private System.Windows.Forms.Label lblBWSN;
        private System.Windows.Forms.Label lblBWSpectType;
        private System.Windows.Forms.Label lblBWStatus;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private Button btnBWSpect;
        private System.Windows.Forms.Label lblOOSN;
        private System.Windows.Forms.Label lblOOSpectType;
        private System.Windows.Forms.Label lblOOStatus;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private GroupBox groupBox8;
        private TextBox txtMaxIntValue;
        private TextBox txtMinIntValue;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private GroupBox groupBox7;
        private Button btnSettings;
        private TextBox txtOperator;
        private TextBox txtPathLength;
        private TextBox txtSiteName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private GroupBox groupBox9;
        private ComboBox cmbLibrary;
        private System.Windows.Forms.Label lblLibraryName2;
        private GroupBox grpPasswordManager;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label11;
        private TextBox txtConfirmNewPass;
        private TextBox txtNewPass;
        private TextBox txtOldPass;
        private Button btnSelectGases;
        private RadioButton optUseLibraryOnFile;
        private RadioButton optSelectGases;
        private RadioButton optPreDefinedLibrary;
        private GroupBox groupBox11;
        private Button btnMonitor;
        private Button btnAlign;
        private Button btnLibraryTweaker;
        private Button btnCollectBkgd;
        private TabPage tabPage4;
        private GroupBox groupBox13;
        private GroupBox groupBox12;
        private Button button4;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label19;
        private TextBox txtSigStrTreshold;
        private TextBox txtConcTreshold;
        private ComboBox cmbGases;
        private System.Windows.Forms.Label lblDataSummary;
        private Button button5;
        private OpenFileDialog data_summary_file_dlg;
        private Button button6;
        private GroupBox groupBox14;
        private System.Windows.Forms.Label lblAvSerial;
        private System.Windows.Forms.Label lblAvSpecType;
        private System.Windows.Forms.Label lblAvSpecStatus;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label40;
        private Button button7;
        private GroupBox groupBox15;
        private Button btnOxySetting;
        private Timer timer_low_ox;
        private Timer timer_high_ox;
        private Button btnCollectLowO2;
        private Button btnCollectHighO2;
        private TextBox txtStatus;
        private System.Windows.Forms.Label lblBoxCar;
        private System.Windows.Forms.Label lblAverages;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label lblDynamicIntTime;
        private System.Windows.Forms.Label lblBackgroundUpdate;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label lblPathLength;
        private System.Windows.Forms.Label lblSiteName;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblSettings;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblSerialNumber;
        private System.Windows.Forms.Label lblSpectType;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private TableLayoutPanel statusTbl;
        private TableLayoutPanel tableLayoutPanel1;
        private TextBox textBox1;
        private System.Windows.Forms.Label label30;
        private MenuStrip mainMenu;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem registerToolStripMenuItem;
        private ToolStripMenuItem aboutUVQuantToolStripMenuItem;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem selectChemicalsToolStripMenuItem;
        private Timer command_timer;
        private ToolStripMenuItem siteInformationToolStripMenuItem;
        private GroupBox groupBox1;
        private TableLayoutPanel tableLayoutPanel2;
        private RadioButton optExtractive;
        private RadioButton optLightStick;
        private RadioButton optOpenPath;
        private RadioButton optHound;
        private System.Windows.Forms.Label lblShiftAmount;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label lblOperator;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblSpectStatus;
        private System.Windows.Forms.Label lblHardware;
        private Button btnTresholds;
        private ToolStripMenuItem uVQuantHelpToolStripMenuItem;
        private ToolStripMenuItem sampleTimeToolStripMenuItem;
        private Panel systemStatusPanel;
        private System.Windows.Forms.Label lblSystemStatus;
        private Panel sigStrePanel;
        private System.Windows.Forms.Label lblSigStre;
        private Panel lsPanel;
        private System.Windows.Forms.Label lblLsh;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label18;
        private Button btnCalibrate;
        private Button button10;
        private Button btnMet;
        private Button button12;
        private Button btnCalibGases;
        private Button bkgdSelect;
        private OpenFileDialog selectBkgdFile;
        private ToolStripMenuItem setGasLimitsToolStripMenuItem;
        private System.IO.Ports.SerialPort metSerialPort;
        private Timer autorun_timer;
        private ToolStripStatusLabel error_status;
        private CheckBox singleFile;
        private Button btnAutoCalibrate;

        int countdown = 60;

        /*
         *Class constructor
         */
        public MainConsole()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
            //this.Resize += new EventHandler(OnResize);
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainConsole));
            this.data_folder_source = new System.Windows.Forms.FolderBrowserDialog();
            this.bkgd_file_source = new System.Windows.Forms.OpenFileDialog();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.scan_status = new System.Windows.Forms.ToolStripStatusLabel();
            this.file_count = new System.Windows.Forms.ToolStripStatusLabel();
            this.current_status = new System.Windows.Forms.ToolStripStatusLabel();
            this.current_time = new System.Windows.Forms.ToolStripStatusLabel();
            this.error_status = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.systemStatusPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblSystemStatus = new System.Windows.Forms.Label();
            this.sigStrePanel = new System.Windows.Forms.Panel();
            this.label34 = new System.Windows.Forms.Label();
            this.lblSigStre = new System.Windows.Forms.Label();
            this.lsPanel = new System.Windows.Forms.Panel();
            this.label18 = new System.Windows.Forms.Label();
            this.lblLsh = new System.Windows.Forms.Label();
            this.btnCollectHighO2 = new System.Windows.Forms.Button();
            this.btnCollectLowO2 = new System.Windows.Forms.Button();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.btnCalibrate = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.btnMet = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.btnAlign = new System.Windows.Forms.Button();
            this.btnNotes = new System.Windows.Forms.Button();
            this.btnMonitor = new System.Windows.Forms.Button();
            this.btnCollectBkgd = new System.Windows.Forms.Button();
            this.cm_datagrid = new System.Windows.Forms.DataGridView();
            this.cm_graph = new ZedGraph.ZedGraphControl();
            this.statusTbl = new System.Windows.Forms.TableLayoutPanel();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.lblSettings = new System.Windows.Forms.Label();
            this.lblBackgroundUpdate = new System.Windows.Forms.Label();
            this.lblDynamicIntTime = new System.Windows.Forms.Label();
            this.lblShiftAmount = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.lblBoxCar = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.lblAverages = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.lblSiteName = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.lblPathLength = new System.Windows.Forms.Label();
            this.lblOperator = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblSpectStatus = new System.Windows.Forms.Label();
            this.lblSpectType = new System.Windows.Forms.Label();
            this.lblSerialNumber = new System.Windows.Forms.Label();
            this.lblHardware = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.singleFile = new System.Windows.Forms.CheckBox();
            this.lblLibraryName2 = new System.Windows.Forms.Label();
            this.rep_integration_period = new System.Windows.Forms.Label();
            this.rep_datagrid = new System.Windows.Forms.DataGridView();
            this.rep_graph = new ZedGraph.ZedGraphControl();
            this.repControlBox = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.bgkdFile = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.dataFolder = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnAutoCalibrate = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.optExtractive = new System.Windows.Forms.RadioButton();
            this.optLightStick = new System.Windows.Forms.RadioButton();
            this.optOpenPath = new System.Windows.Forms.RadioButton();
            this.optHound = new System.Windows.Forms.RadioButton();
            this.btnOxySetting = new System.Windows.Forms.Button();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.lblAvSerial = new System.Windows.Forms.Label();
            this.lblAvSpecType = new System.Windows.Forms.Label();
            this.lblAvSpecStatus = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.button7 = new System.Windows.Forms.Button();
            this.groupBox15 = new System.Windows.Forms.GroupBox();
            this.button6 = new System.Windows.Forms.Button();
            this.btnSelectGases = new System.Windows.Forms.Button();
            this.grpPasswordManager = new System.Windows.Forms.GroupBox();
            this.txtConfirmNewPass = new System.Windows.Forms.TextBox();
            this.txtNewPass = new System.Windows.Forms.TextBox();
            this.txtOldPass = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.bkgdSelect = new System.Windows.Forms.Button();
            this.btnCalibGases = new System.Windows.Forms.Button();
            this.btnTresholds = new System.Windows.Forms.Button();
            this.btnLibraryTweaker = new System.Windows.Forms.Button();
            this.optUseLibraryOnFile = new System.Windows.Forms.RadioButton();
            this.optSelectGases = new System.Windows.Forms.RadioButton();
            this.optPreDefinedLibrary = new System.Windows.Forms.RadioButton();
            this.cmbLibrary = new System.Windows.Forms.ComboBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.txtMaxIntValue = new System.Windows.Forms.TextBox();
            this.txtMinIntValue = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.btnSettings = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lblBWSN = new System.Windows.Forms.Label();
            this.lblBWSpectType = new System.Windows.Forms.Label();
            this.lblBWStatus = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.btnBWSpect = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblOOSN = new System.Windows.Forms.Label();
            this.lblOOSpectType = new System.Windows.Forms.Label();
            this.lblOOStatus = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.btnSpect = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtOperator = new System.Windows.Forms.TextBox();
            this.txtPathLength = new System.Windows.Forms.TextBox();
            this.txtSiteName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.lblDataSummary = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.label29 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.txtSigStrTreshold = new System.Windows.Forms.TextBox();
            this.txtConcTreshold = new System.Windows.Forms.TextBox();
            this.cmbGases = new System.Windows.Forms.ComboBox();
            this.button4 = new System.Windows.Forms.Button();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.gas_graph = new ZedGraph.ZedGraphControl();
            this.data_summary_file_dlg = new System.Windows.Forms.OpenFileDialog();
            this.timer_low_ox = new System.Windows.Forms.Timer(this.components);
            this.timer_high_ox = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectChemicalsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setGasLimitsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sampleTimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.siteInformationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uVQuantHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutUVQuantToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.command_timer = new System.Windows.Forms.Timer(this.components);
            this.selectBkgdFile = new System.Windows.Forms.OpenFileDialog();
            this.metSerialPort = new System.IO.Ports.SerialPort(this.components);
            this.autorun_timer = new System.Windows.Forms.Timer(this.components);
            this.statusBar.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.systemStatusPanel.SuspendLayout();
            this.sigStrePanel.SuspendLayout();
            this.lsPanel.SuspendLayout();
            this.groupBox11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cm_datagrid)).BeginInit();
            this.statusTbl.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rep_datagrid)).BeginInit();
            this.repControlBox.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox14.SuspendLayout();
            this.grpPasswordManager.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.groupBox13.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.mainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // bkgd_file_source
            // 
            this.bkgd_file_source.FileName = "openFileDialog1";
            // 
            // statusBar
            // 
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.scan_status,
            this.file_count,
            this.current_status,
            this.current_time,
            this.error_status});
            this.statusBar.Location = new System.Drawing.Point(0, 694);
            this.statusBar.Name = "statusBar";
            this.statusBar.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusBar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.statusBar.Size = new System.Drawing.Size(1220, 26);
            this.statusBar.TabIndex = 18;
            this.statusBar.Text = "statusStrip1";
            // 
            // scan_status
            // 
            this.scan_status.Name = "scan_status";
            this.scan_status.Size = new System.Drawing.Size(86, 21);
            this.scan_status.Text = "scan out of ";
            // 
            // file_count
            // 
            this.file_count.Name = "file_count";
            this.file_count.Size = new System.Drawing.Size(0, 21);
            // 
            // current_status
            // 
            this.current_status.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.current_status.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.current_status.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.current_status.Margin = new System.Windows.Forms.Padding(50, 3, 0, 2);
            this.current_status.Name = "current_status";
            this.current_status.Size = new System.Drawing.Size(69, 21);
            this.current_status.Text = "Status...";
            // 
            // current_time
            // 
            this.current_time.Name = "current_time";
            this.current_time.Size = new System.Drawing.Size(0, 21);
            // 
            // error_status
            // 
            this.error_status.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.error_status.ForeColor = System.Drawing.Color.Red;
            this.error_status.Margin = new System.Windows.Forms.Padding(200, 3, 0, 2);
            this.error_status.Name = "error_status";
            this.error_status.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.error_status.Size = new System.Drawing.Size(43, 21);
            this.error_status.Text = "error";
            this.error_status.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(16, 46);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1191, 644);
            this.tabControl1.TabIndex = 20;
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.TabControl1_Selected);
            // 
            // tabPage1
            // 
            this.tabPage1.AutoScroll = true;
            this.tabPage1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabPage1.Controls.Add(this.systemStatusPanel);
            this.tabPage1.Controls.Add(this.sigStrePanel);
            this.tabPage1.Controls.Add(this.lsPanel);
            this.tabPage1.Controls.Add(this.btnCollectHighO2);
            this.tabPage1.Controls.Add(this.btnCollectLowO2);
            this.tabPage1.Controls.Add(this.groupBox11);
            this.tabPage1.Controls.Add(this.cm_datagrid);
            this.tabPage1.Controls.Add(this.cm_graph);
            this.tabPage1.Controls.Add(this.statusTbl);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage1.Size = new System.Drawing.Size(1183, 615);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Real Time";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // systemStatusPanel
            // 
            this.systemStatusPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.systemStatusPanel.BackgroundImage = global::UV_Quant.Properties.Resources.green_circle;
            this.systemStatusPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.systemStatusPanel.Controls.Add(this.label1);
            this.systemStatusPanel.Controls.Add(this.lblSystemStatus);
            this.systemStatusPanel.Location = new System.Drawing.Point(1044, 171);
            this.systemStatusPanel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.systemStatusPanel.Name = "systemStatusPanel";
            this.systemStatusPanel.Size = new System.Drawing.Size(125, 117);
            this.systemStatusPanel.TabIndex = 60;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(5, 85);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "System Status";
            // 
            // lblSystemStatus
            // 
            this.lblSystemStatus.AutoSize = true;
            this.lblSystemStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSystemStatus.Location = new System.Drawing.Point(45, 49);
            this.lblSystemStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSystemStatus.Name = "lblSystemStatus";
            this.lblSystemStatus.Size = new System.Drawing.Size(30, 17);
            this.lblSystemStatus.TabIndex = 0;
            this.lblSystemStatus.Text = "OK";
            this.lblSystemStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // sigStrePanel
            // 
            this.sigStrePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sigStrePanel.BackgroundImage = global::UV_Quant.Properties.Resources.green_circle;
            this.sigStrePanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.sigStrePanel.Controls.Add(this.label34);
            this.sigStrePanel.Controls.Add(this.lblSigStre);
            this.sigStrePanel.Location = new System.Drawing.Point(1044, 452);
            this.sigStrePanel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.sigStrePanel.Name = "sigStrePanel";
            this.sigStrePanel.Size = new System.Drawing.Size(125, 117);
            this.sigStrePanel.TabIndex = 60;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.Location = new System.Drawing.Point(0, 85);
            this.label34.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(120, 17);
            this.label34.TabIndex = 2;
            this.label34.Text = "Signal Strength";
            // 
            // lblSigStre
            // 
            this.lblSigStre.AutoSize = true;
            this.lblSigStre.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSigStre.Location = new System.Drawing.Point(28, 49);
            this.lblSigStre.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSigStre.Name = "lblSigStre";
            this.lblSigStre.Size = new System.Drawing.Size(48, 17);
            this.lblSigStre.TabIndex = 0;
            this.lblSigStre.Text = "100%";
            this.lblSigStre.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lsPanel
            // 
            this.lsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lsPanel.BackgroundImage = global::UV_Quant.Properties.Resources.green_circle;
            this.lsPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.lsPanel.Controls.Add(this.label18);
            this.lsPanel.Controls.Add(this.lblLsh);
            this.lsPanel.Location = new System.Drawing.Point(1044, 309);
            this.lsPanel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lsPanel.Name = "lsPanel";
            this.lsPanel.Size = new System.Drawing.Size(125, 117);
            this.lsPanel.TabIndex = 59;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(-4, 84);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(126, 17);
            this.label18.TabIndex = 2;
            this.label18.Text = "Source Strength";
            // 
            // lblLsh
            // 
            this.lblLsh.AutoSize = true;
            this.lblLsh.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLsh.Location = new System.Drawing.Point(28, 53);
            this.lblLsh.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLsh.Name = "lblLsh";
            this.lblLsh.Size = new System.Drawing.Size(48, 17);
            this.lblLsh.TabIndex = 0;
            this.lblLsh.Text = "100%";
            this.lblLsh.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCollectHighO2
            // 
            this.btnCollectHighO2.BackgroundImage = global::UV_Quant.Properties.Resources.argos_logo_small;
            this.btnCollectHighO2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCollectHighO2.Enabled = false;
            this.btnCollectHighO2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCollectHighO2.Location = new System.Drawing.Point(392, 501);
            this.btnCollectHighO2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCollectHighO2.Name = "btnCollectHighO2";
            this.btnCollectHighO2.Size = new System.Drawing.Size(139, 98);
            this.btnCollectHighO2.TabIndex = 56;
            this.btnCollectHighO2.Text = "Collect High O2";
            this.btnCollectHighO2.UseVisualStyleBackColor = true;
            this.btnCollectHighO2.Visible = false;
            this.btnCollectHighO2.Click += new System.EventHandler(this.btnCollectHighO2_Click);
            // 
            // btnCollectLowO2
            // 
            this.btnCollectLowO2.BackgroundImage = global::UV_Quant.Properties.Resources.argos_logo_small;
            this.btnCollectLowO2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCollectLowO2.Enabled = false;
            this.btnCollectLowO2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCollectLowO2.Location = new System.Drawing.Point(392, 394);
            this.btnCollectLowO2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCollectLowO2.Name = "btnCollectLowO2";
            this.btnCollectLowO2.Size = new System.Drawing.Size(139, 98);
            this.btnCollectLowO2.TabIndex = 57;
            this.btnCollectLowO2.Text = "Collect Low O2";
            this.btnCollectLowO2.UseVisualStyleBackColor = true;
            this.btnCollectLowO2.Visible = false;
            this.btnCollectLowO2.Click += new System.EventHandler(this.btnCollectLowO2_Click);
            // 
            // groupBox11
            // 
            this.groupBox11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox11.Controls.Add(this.btnCalibrate);
            this.groupBox11.Controls.Add(this.button10);
            this.groupBox11.Controls.Add(this.btnMet);
            this.groupBox11.Controls.Add(this.button12);
            this.groupBox11.Controls.Add(this.btnAlign);
            this.groupBox11.Controls.Add(this.btnNotes);
            this.groupBox11.Controls.Add(this.btnMonitor);
            this.groupBox11.Controls.Add(this.btnCollectBkgd);
            this.groupBox11.Location = new System.Drawing.Point(13, 164);
            this.groupBox11.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox11.MinimumSize = new System.Drawing.Size(169, 444);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox11.Size = new System.Drawing.Size(328, 444);
            this.groupBox11.TabIndex = 51;
            this.groupBox11.TabStop = false;
            // 
            // btnCalibrate
            // 
            this.btnCalibrate.BackgroundImage = global::UV_Quant.Properties.Resources.argos_logo_small;
            this.btnCalibrate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCalibrate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCalibrate.Location = new System.Drawing.Point(169, 23);
            this.btnCalibrate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCalibrate.Name = "btnCalibrate";
            this.btnCalibrate.Size = new System.Drawing.Size(139, 92);
            this.btnCalibrate.TabIndex = 48;
            this.btnCalibrate.Text = "Calibrate";
            this.btnCalibrate.UseVisualStyleBackColor = true;
            this.btnCalibrate.Click += new System.EventHandler(this.button8_Click_3);
            // 
            // button10
            // 
            this.button10.BackgroundImage = global::UV_Quant.Properties.Resources.argos_logo_small;
            this.button10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button10.Enabled = false;
            this.button10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button10.Location = new System.Drawing.Point(169, 335);
            this.button10.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(139, 98);
            this.button10.TabIndex = 47;
            this.button10.Text = "External Trigger";
            this.button10.UseVisualStyleBackColor = true;
            // 
            // btnMet
            // 
            this.btnMet.BackColor = System.Drawing.Color.Green;
            this.btnMet.BackgroundImage = global::UV_Quant.Properties.Resources.argos_logo_small;
            this.btnMet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMet.Enabled = false;
            this.btnMet.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMet.Location = new System.Drawing.Point(169, 123);
            this.btnMet.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnMet.Name = "btnMet";
            this.btnMet.Size = new System.Drawing.Size(139, 98);
            this.btnMet.TabIndex = 46;
            this.btnMet.Text = "Met System";
            this.btnMet.UseVisualStyleBackColor = false;
            this.btnMet.Click += new System.EventHandler(this.btnMet_Click);
            // 
            // button12
            // 
            this.button12.BackgroundImage = global::UV_Quant.Properties.Resources.argos_logo_small;
            this.button12.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button12.Enabled = false;
            this.button12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button12.Location = new System.Drawing.Point(169, 229);
            this.button12.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(139, 98);
            this.button12.TabIndex = 49;
            this.button12.Text = "GPS";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // btnAlign
            // 
            this.btnAlign.BackgroundImage = global::UV_Quant.Properties.Resources.argos_logo_small;
            this.btnAlign.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAlign.Enabled = false;
            this.btnAlign.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAlign.Location = new System.Drawing.Point(12, 23);
            this.btnAlign.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAlign.Name = "btnAlign";
            this.btnAlign.Size = new System.Drawing.Size(139, 92);
            this.btnAlign.TabIndex = 44;
            this.btnAlign.Text = "ALIGN";
            this.btnAlign.UseVisualStyleBackColor = true;
            this.btnAlign.Click += new System.EventHandler(this.btnAlign_Click);
            // 
            // btnNotes
            // 
            this.btnNotes.BackgroundImage = global::UV_Quant.Properties.Resources.argos_logo_small;
            this.btnNotes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNotes.Location = new System.Drawing.Point(12, 335);
            this.btnNotes.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnNotes.Name = "btnNotes";
            this.btnNotes.Size = new System.Drawing.Size(139, 98);
            this.btnNotes.TabIndex = 25;
            this.btnNotes.Text = "NOTES";
            this.btnNotes.UseVisualStyleBackColor = true;
            this.btnNotes.Click += new System.EventHandler(this.btnNotes_Click);
            // 
            // btnMonitor
            // 
            this.btnMonitor.BackColor = System.Drawing.Color.Green;
            this.btnMonitor.BackgroundImage = global::UV_Quant.Properties.Resources.argos_logo_small;
            this.btnMonitor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMonitor.Enabled = false;
            this.btnMonitor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMonitor.Location = new System.Drawing.Point(12, 123);
            this.btnMonitor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnMonitor.Name = "btnMonitor";
            this.btnMonitor.Size = new System.Drawing.Size(139, 98);
            this.btnMonitor.TabIndex = 5;
            this.btnMonitor.Text = "START";
            this.btnMonitor.UseVisualStyleBackColor = false;
            this.btnMonitor.Click += new System.EventHandler(this.btnMonitor_Click);
            // 
            // btnCollectBkgd
            // 
            this.btnCollectBkgd.BackgroundImage = global::UV_Quant.Properties.Resources.argos_logo_small;
            this.btnCollectBkgd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCollectBkgd.Enabled = false;
            this.btnCollectBkgd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCollectBkgd.Location = new System.Drawing.Point(12, 229);
            this.btnCollectBkgd.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCollectBkgd.Name = "btnCollectBkgd";
            this.btnCollectBkgd.Size = new System.Drawing.Size(139, 98);
            this.btnCollectBkgd.TabIndex = 45;
            this.btnCollectBkgd.Text = "Collect Background";
            this.btnCollectBkgd.UseVisualStyleBackColor = true;
            this.btnCollectBkgd.Click += new System.EventHandler(this.btnCollectBkgd_Click);
            // 
            // cm_datagrid
            // 
            this.cm_datagrid.AllowUserToAddRows = false;
            this.cm_datagrid.AllowUserToDeleteRows = false;
            this.cm_datagrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cm_datagrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.cm_datagrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cm_datagrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.cm_datagrid.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.cm_datagrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.cm_datagrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.cm_datagrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.cm_datagrid.Cursor = System.Windows.Forms.Cursors.Default;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.cm_datagrid.DefaultCellStyle = dataGridViewCellStyle3;
            this.cm_datagrid.GridColor = System.Drawing.Color.Silver;
            this.cm_datagrid.Location = new System.Drawing.Point(349, 385);
            this.cm_datagrid.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cm_datagrid.Name = "cm_datagrid";
            this.cm_datagrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.cm_datagrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.cm_datagrid.RowTemplate.Height = 24;
            this.cm_datagrid.Size = new System.Drawing.Size(687, 214);
            this.cm_datagrid.TabIndex = 47;
            this.cm_datagrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.cm_datagrid_CellContentClick);
            // 
            // cm_graph
            // 
            this.cm_graph.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cm_graph.BackColor = System.Drawing.Color.Black;
            this.cm_graph.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cm_graph.EditButtons = System.Windows.Forms.MouseButtons.Left;
            this.cm_graph.Location = new System.Drawing.Point(349, 171);
            this.cm_graph.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.cm_graph.MinimumSize = new System.Drawing.Size(133, 123);
            this.cm_graph.Name = "cm_graph";
            this.cm_graph.PanModifierKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.None)));
            this.cm_graph.ScrollGrace = 0D;
            this.cm_graph.ScrollMaxX = 0D;
            this.cm_graph.ScrollMaxY = 0D;
            this.cm_graph.ScrollMaxY2 = 0D;
            this.cm_graph.ScrollMinX = 0D;
            this.cm_graph.ScrollMinY = 0D;
            this.cm_graph.ScrollMinY2 = 0D;
            this.cm_graph.Size = new System.Drawing.Size(687, 207);
            this.cm_graph.TabIndex = 46;
            // 
            // statusTbl
            // 
            this.statusTbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.statusTbl.BackColor = System.Drawing.Color.Transparent;
            this.statusTbl.ColumnCount = 9;
            this.statusTbl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.statusTbl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 115F));
            this.statusTbl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 144F));
            this.statusTbl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 115F));
            this.statusTbl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.statusTbl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.statusTbl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.statusTbl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.statusTbl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.statusTbl.Controls.Add(this.txtStatus, 8, 0);
            this.statusTbl.Controls.Add(this.label12, 0, 0);
            this.statusTbl.Controls.Add(this.lblSettings, 4, 0);
            this.statusTbl.Controls.Add(this.lblBackgroundUpdate, 7, 2);
            this.statusTbl.Controls.Add(this.lblDynamicIntTime, 7, 1);
            this.statusTbl.Controls.Add(this.lblShiftAmount, 5, 3);
            this.statusTbl.Controls.Add(this.label10, 2, 0);
            this.statusTbl.Controls.Add(this.label36, 6, 2);
            this.statusTbl.Controls.Add(this.lblBoxCar, 5, 2);
            this.statusTbl.Controls.Add(this.label37, 6, 1);
            this.statusTbl.Controls.Add(this.label27, 0, 1);
            this.statusTbl.Controls.Add(this.lblAverages, 5, 1);
            this.statusTbl.Controls.Add(this.label26, 0, 2);
            this.statusTbl.Controls.Add(this.label31, 4, 3);
            this.statusTbl.Controls.Add(this.label25, 0, 3);
            this.statusTbl.Controls.Add(this.label32, 4, 2);
            this.statusTbl.Controls.Add(this.lblSiteName, 1, 1);
            this.statusTbl.Controls.Add(this.label33, 4, 1);
            this.statusTbl.Controls.Add(this.lblPathLength, 1, 2);
            this.statusTbl.Controls.Add(this.lblOperator, 1, 3);
            this.statusTbl.Controls.Add(this.label7, 2, 1);
            this.statusTbl.Controls.Add(this.label8, 2, 2);
            this.statusTbl.Controls.Add(this.label9, 2, 3);
            this.statusTbl.Controls.Add(this.lblSpectStatus, 3, 3);
            this.statusTbl.Controls.Add(this.lblSpectType, 3, 2);
            this.statusTbl.Controls.Add(this.lblSerialNumber, 3, 1);
            this.statusTbl.Controls.Add(this.lblHardware, 6, 3);
            this.statusTbl.Location = new System.Drawing.Point(25, 22);
            this.statusTbl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.statusTbl.Name = "statusTbl";
            this.statusTbl.Padding = new System.Windows.Forms.Padding(0, 12, 0, 0);
            this.statusTbl.RowCount = 3;
            this.statusTbl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 56.89655F));
            this.statusTbl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 43.10345F));
            this.statusTbl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.statusTbl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            this.statusTbl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.statusTbl.Size = new System.Drawing.Size(1140, 119);
            this.statusTbl.TabIndex = 58;
            // 
            // txtStatus
            // 
            this.txtStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtStatus.Location = new System.Drawing.Point(813, 16);
            this.txtStatus.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtStatus.Multiline = true;
            this.txtStatus.Name = "txtStatus";
            this.statusTbl.SetRowSpan(this.txtStatus, 4);
            this.txtStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtStatus.Size = new System.Drawing.Size(323, 99);
            this.txtStatus.TabIndex = 42;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.statusTbl.SetColumnSpan(this.label12, 2);
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(4, 12);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(163, 25);
            this.label12.TabIndex = 24;
            this.label12.Text = "Site Information";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSettings
            // 
            this.lblSettings.AutoSize = true;
            this.lblSettings.BackColor = System.Drawing.Color.Transparent;
            this.statusTbl.SetColumnSpan(this.lblSettings, 2);
            this.lblSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSettings.Location = new System.Drawing.Point(478, 12);
            this.lblSettings.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSettings.Name = "lblSettings";
            this.lblSettings.Size = new System.Drawing.Size(91, 25);
            this.lblSettings.TabIndex = 23;
            this.lblSettings.Text = "Settings";
            this.lblSettings.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblBackgroundUpdate
            // 
            this.lblBackgroundUpdate.AutoSize = true;
            this.lblBackgroundUpdate.BackColor = System.Drawing.Color.Transparent;
            this.lblBackgroundUpdate.Location = new System.Drawing.Point(762, 75);
            this.lblBackgroundUpdate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBackgroundUpdate.Name = "lblBackgroundUpdate";
            this.lblBackgroundUpdate.Size = new System.Drawing.Size(23, 17);
            this.lblBackgroundUpdate.TabIndex = 34;
            this.lblBackgroundUpdate.Text = "---";
            this.lblBackgroundUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDynamicIntTime
            // 
            this.lblDynamicIntTime.AutoSize = true;
            this.lblDynamicIntTime.BackColor = System.Drawing.Color.Transparent;
            this.lblDynamicIntTime.Location = new System.Drawing.Point(762, 48);
            this.lblDynamicIntTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDynamicIntTime.Name = "lblDynamicIntTime";
            this.lblDynamicIntTime.Size = new System.Drawing.Size(23, 17);
            this.lblDynamicIntTime.TabIndex = 35;
            this.lblDynamicIntTime.Text = "---";
            this.lblDynamicIntTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblShiftAmount
            // 
            this.lblShiftAmount.AutoSize = true;
            this.lblShiftAmount.BackColor = System.Drawing.Color.Transparent;
            this.lblShiftAmount.Location = new System.Drawing.Point(578, 100);
            this.lblShiftAmount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblShiftAmount.Name = "lblShiftAmount";
            this.lblShiftAmount.Size = new System.Drawing.Size(18, 17);
            this.lblShiftAmount.TabIndex = 41;
            this.lblShiftAmount.Text = "--";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.statusTbl.SetColumnSpan(this.label10, 2);
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(219, 12);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(141, 25);
            this.label10.TabIndex = 22;
            this.label10.Text = "Spectrometer";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.BackColor = System.Drawing.Color.Transparent;
            this.label36.Location = new System.Drawing.Point(622, 75);
            this.label36.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(84, 25);
            this.label36.TabIndex = 33;
            this.label36.Text = "Background Update";
            this.label36.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblBoxCar
            // 
            this.lblBoxCar.AutoSize = true;
            this.lblBoxCar.BackColor = System.Drawing.Color.Transparent;
            this.lblBoxCar.Location = new System.Drawing.Point(578, 75);
            this.lblBoxCar.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBoxCar.Name = "lblBoxCar";
            this.lblBoxCar.Size = new System.Drawing.Size(18, 17);
            this.lblBoxCar.TabIndex = 40;
            this.lblBoxCar.Text = "--";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.BackColor = System.Drawing.Color.Transparent;
            this.label37.Location = new System.Drawing.Point(622, 48);
            this.label37.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(120, 17);
            this.label37.TabIndex = 32;
            this.label37.Text = "Dynamic Int. Time";
            this.label37.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.BackColor = System.Drawing.Color.Transparent;
            this.label27.Location = new System.Drawing.Point(4, 48);
            this.label27.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(73, 17);
            this.label27.TabIndex = 26;
            this.label27.Text = "Site Name";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAverages
            // 
            this.lblAverages.AutoSize = true;
            this.lblAverages.BackColor = System.Drawing.Color.Transparent;
            this.lblAverages.Location = new System.Drawing.Point(578, 48);
            this.lblAverages.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAverages.Name = "lblAverages";
            this.lblAverages.Size = new System.Drawing.Size(18, 17);
            this.lblAverages.TabIndex = 39;
            this.lblAverages.Text = "--";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.BackColor = System.Drawing.Color.Transparent;
            this.label26.Location = new System.Drawing.Point(4, 75);
            this.label26.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(85, 17);
            this.label26.TabIndex = 27;
            this.label26.Text = "Path Length";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.BackColor = System.Drawing.Color.Transparent;
            this.label31.Location = new System.Drawing.Point(478, 100);
            this.label31.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(88, 17);
            this.label31.TabIndex = 38;
            this.label31.Text = "Shift Amount";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.BackColor = System.Drawing.Color.Transparent;
            this.label25.Location = new System.Drawing.Point(4, 100);
            this.label25.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(65, 17);
            this.label25.TabIndex = 28;
            this.label25.Text = "Operator";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.BackColor = System.Drawing.Color.Transparent;
            this.label32.Location = new System.Drawing.Point(478, 75);
            this.label32.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(57, 17);
            this.label32.TabIndex = 37;
            this.label32.Text = "Box Car";
            this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSiteName
            // 
            this.lblSiteName.AutoSize = true;
            this.lblSiteName.BackColor = System.Drawing.Color.Transparent;
            this.lblSiteName.Location = new System.Drawing.Point(104, 48);
            this.lblSiteName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSiteName.Name = "lblSiteName";
            this.lblSiteName.Size = new System.Drawing.Size(73, 17);
            this.lblSiteName.TabIndex = 29;
            this.lblSiteName.Text = "Site Name";
            this.lblSiteName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.BackColor = System.Drawing.Color.Transparent;
            this.label33.Location = new System.Drawing.Point(478, 48);
            this.label33.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(68, 17);
            this.label33.TabIndex = 36;
            this.label33.Text = "Averages";
            this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPathLength
            // 
            this.lblPathLength.AutoSize = true;
            this.lblPathLength.BackColor = System.Drawing.Color.Transparent;
            this.lblPathLength.Location = new System.Drawing.Point(104, 75);
            this.lblPathLength.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPathLength.Name = "lblPathLength";
            this.lblPathLength.Size = new System.Drawing.Size(85, 17);
            this.lblPathLength.TabIndex = 30;
            this.lblPathLength.Text = "Path Length";
            this.lblPathLength.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblOperator
            // 
            this.lblOperator.AutoSize = true;
            this.lblOperator.BackColor = System.Drawing.Color.Transparent;
            this.lblOperator.Location = new System.Drawing.Point(104, 100);
            this.lblOperator.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOperator.Name = "lblOperator";
            this.lblOperator.Size = new System.Drawing.Size(65, 17);
            this.lblOperator.TabIndex = 31;
            this.lblOperator.Text = "Operator";
            this.lblOperator.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(219, 48);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(98, 17);
            this.label7.TabIndex = 3;
            this.label7.Text = "Serial Number";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Location = new System.Drawing.Point(219, 75);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(129, 17);
            this.label8.TabIndex = 14;
            this.label8.Text = "Spectrometer Type";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Location = new System.Drawing.Point(219, 100);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(48, 17);
            this.label9.TabIndex = 15;
            this.label9.Text = "Status";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSpectStatus
            // 
            this.lblSpectStatus.AutoSize = true;
            this.lblSpectStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblSpectStatus.Location = new System.Drawing.Point(363, 100);
            this.lblSpectStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSpectStatus.Name = "lblSpectStatus";
            this.lblSpectStatus.Size = new System.Drawing.Size(102, 17);
            this.lblSpectStatus.TabIndex = 16;
            this.lblSpectStatus.Text = "Not Connected";
            this.lblSpectStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSpectType
            // 
            this.lblSpectType.AutoSize = true;
            this.lblSpectType.BackColor = System.Drawing.Color.Transparent;
            this.lblSpectType.Location = new System.Drawing.Point(363, 75);
            this.lblSpectType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSpectType.Name = "lblSpectType";
            this.lblSpectType.Size = new System.Drawing.Size(23, 17);
            this.lblSpectType.TabIndex = 17;
            this.lblSpectType.Text = "---";
            this.lblSpectType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSerialNumber
            // 
            this.lblSerialNumber.AutoSize = true;
            this.lblSerialNumber.BackColor = System.Drawing.Color.Transparent;
            this.lblSerialNumber.Location = new System.Drawing.Point(363, 48);
            this.lblSerialNumber.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSerialNumber.Name = "lblSerialNumber";
            this.lblSerialNumber.Size = new System.Drawing.Size(23, 17);
            this.lblSerialNumber.TabIndex = 18;
            this.lblSerialNumber.Text = "---";
            this.lblSerialNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblHardware
            // 
            this.lblHardware.AutoSize = true;
            this.lblHardware.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHardware.Location = new System.Drawing.Point(622, 100);
            this.lblHardware.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHardware.Name = "lblHardware";
            this.lblHardware.Size = new System.Drawing.Size(23, 19);
            this.lblHardware.TabIndex = 43;
            this.lblHardware.Text = "--";
            // 
            // tabPage2
            // 
            this.tabPage2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabPage2.Controls.Add(this.singleFile);
            this.tabPage2.Controls.Add(this.lblLibraryName2);
            this.tabPage2.Controls.Add(this.rep_integration_period);
            this.tabPage2.Controls.Add(this.rep_datagrid);
            this.tabPage2.Controls.Add(this.rep_graph);
            this.tabPage2.Controls.Add(this.repControlBox);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage2.Size = new System.Drawing.Size(1183, 615);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Reprocessing";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // singleFile
            // 
            this.singleFile.AutoSize = true;
            this.singleFile.Location = new System.Drawing.Point(972, 156);
            this.singleFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.singleFile.Name = "singleFile";
            this.singleFile.Size = new System.Drawing.Size(127, 26);
            this.singleFile.TabIndex = 51;
            this.singleFile.Text = "Single File";
            this.singleFile.UseVisualStyleBackColor = true;
            // 
            // lblLibraryName2
            // 
            this.lblLibraryName2.AutoSize = true;
            this.lblLibraryName2.BackColor = System.Drawing.Color.Transparent;
            this.lblLibraryName2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLibraryName2.Location = new System.Drawing.Point(3, 172);
            this.lblLibraryName2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLibraryName2.Name = "lblLibraryName2";
            this.lblLibraryName2.Size = new System.Drawing.Size(97, 25);
            this.lblLibraryName2.TabIndex = 50;
            this.lblLibraryName2.Text = "Library =";
            // 
            // rep_integration_period
            // 
            this.rep_integration_period.AutoSize = true;
            this.rep_integration_period.BackColor = System.Drawing.Color.Transparent;
            this.rep_integration_period.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rep_integration_period.Location = new System.Drawing.Point(453, 172);
            this.rep_integration_period.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.rep_integration_period.Name = "rep_integration_period";
            this.rep_integration_period.Size = new System.Drawing.Size(201, 25);
            this.rep_integration_period.TabIndex = 45;
            this.rep_integration_period.Text = "Integration Period =";
            // 
            // rep_datagrid
            // 
            this.rep_datagrid.AllowUserToAddRows = false;
            this.rep_datagrid.AllowUserToDeleteRows = false;
            this.rep_datagrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.rep_datagrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.rep_datagrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.rep_datagrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.rep_datagrid.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.rep_datagrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.rep_datagrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.rep_datagrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.rep_datagrid.Cursor = System.Windows.Forms.Cursors.Default;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.rep_datagrid.DefaultCellStyle = dataGridViewCellStyle7;
            this.rep_datagrid.GridColor = System.Drawing.Color.Silver;
            this.rep_datagrid.Location = new System.Drawing.Point(8, 223);
            this.rep_datagrid.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rep_datagrid.Name = "rep_datagrid";
            this.rep_datagrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.rep_datagrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.rep_datagrid.RowTemplate.Height = 24;
            this.rep_datagrid.Size = new System.Drawing.Size(411, 373);
            this.rep_datagrid.TabIndex = 44;
            this.rep_datagrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.rep_datagrid_CellContentClick);
            // 
            // rep_graph
            // 
            this.rep_graph.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rep_graph.BackColor = System.Drawing.Color.Black;
            this.rep_graph.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.rep_graph.EditButtons = System.Windows.Forms.MouseButtons.Left;
            this.rep_graph.Location = new System.Drawing.Point(428, 219);
            this.rep_graph.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.rep_graph.Name = "rep_graph";
            this.rep_graph.PanModifierKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.None)));
            this.rep_graph.ScrollGrace = 0D;
            this.rep_graph.ScrollMaxX = 0D;
            this.rep_graph.ScrollMaxY = 0D;
            this.rep_graph.ScrollMaxY2 = 0D;
            this.rep_graph.ScrollMinX = 0D;
            this.rep_graph.ScrollMinY = 0D;
            this.rep_graph.ScrollMinY2 = 0D;
            this.rep_graph.Size = new System.Drawing.Size(735, 369);
            this.rep_graph.TabIndex = 43;
            // 
            // repControlBox
            // 
            this.repControlBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.repControlBox.BackColor = System.Drawing.Color.Transparent;
            this.repControlBox.Controls.Add(this.label2);
            this.repControlBox.Controls.Add(this.button1);
            this.repControlBox.Controls.Add(this.bgkdFile);
            this.repControlBox.Controls.Add(this.button3);
            this.repControlBox.Controls.Add(this.dataFolder);
            this.repControlBox.Controls.Add(this.button2);
            this.repControlBox.Controls.Add(this.label3);
            this.repControlBox.Location = new System.Drawing.Point(8, 7);
            this.repControlBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.repControlBox.Name = "repControlBox";
            this.repControlBox.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.repControlBox.Size = new System.Drawing.Size(1155, 142);
            this.repControlBox.TabIndex = 27;
            this.repControlBox.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 10);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(177, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "Background File (Optional)";
            // 
            // button1
            // 
            this.button1.BackgroundImage = global::UV_Quant.Properties.Resources.argos_logo_small;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(947, 10);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(145, 122);
            this.button1.TabIndex = 2;
            this.button1.Text = "Reprocess";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // bgkdFile
            // 
            this.bgkdFile.Location = new System.Drawing.Point(7, 32);
            this.bgkdFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.bgkdFile.Name = "bgkdFile";
            this.bgkdFile.Size = new System.Drawing.Size(804, 22);
            this.bgkdFile.TabIndex = 5;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(836, 87);
            this.button3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(85, 26);
            this.button3.TabIndex = 10;
            this.button3.Text = "...";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // dataFolder
            // 
            this.dataFolder.Location = new System.Drawing.Point(8, 89);
            this.dataFolder.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataFolder.Name = "dataFolder";
            this.dataFolder.Size = new System.Drawing.Size(803, 22);
            this.dataFolder.TabIndex = 6;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(833, 32);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(88, 26);
            this.button2.TabIndex = 9;
            this.button2.Text = "...";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 73);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(183, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "Data files Folder (Required)";
            // 
            // tabPage3
            // 
            this.tabPage3.AutoScroll = true;
            this.tabPage3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabPage3.Controls.Add(this.btnAutoCalibrate);
            this.tabPage3.Controls.Add(this.groupBox1);
            this.tabPage3.Controls.Add(this.btnOxySetting);
            this.tabPage3.Controls.Add(this.groupBox14);
            this.tabPage3.Controls.Add(this.button6);
            this.tabPage3.Controls.Add(this.btnSelectGases);
            this.tabPage3.Controls.Add(this.grpPasswordManager);
            this.tabPage3.Controls.Add(this.groupBox9);
            this.tabPage3.Controls.Add(this.groupBox8);
            this.tabPage3.Controls.Add(this.groupBox7);
            this.tabPage3.Controls.Add(this.groupBox5);
            this.tabPage3.Controls.Add(this.groupBox2);
            this.tabPage3.Controls.Add(this.groupBox3);
            this.tabPage3.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage3.Size = new System.Drawing.Size(1183, 615);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Administrator";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btnAutoCalibrate
            // 
            this.btnAutoCalibrate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAutoCalibrate.Image = global::UV_Quant.Properties.Resources.argos_logo_small;
            this.btnAutoCalibrate.Location = new System.Drawing.Point(476, 437);
            this.btnAutoCalibrate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAutoCalibrate.Name = "btnAutoCalibrate";
            this.btnAutoCalibrate.Size = new System.Drawing.Size(105, 154);
            this.btnAutoCalibrate.TabIndex = 57;
            this.btnAutoCalibrate.Text = "Calibrate";
            this.btnAutoCalibrate.UseVisualStyleBackColor = true;
            this.btnAutoCalibrate.Click += new System.EventHandler(this.btnAutoCalibrate_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel2);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(8, 14);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(567, 130);
            this.groupBox1.TabIndex = 56;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select Hardware Type";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.optExtractive, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.optLightStick, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.optOpenPath, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.optHound, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 23);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(567, 107);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // optExtractive
            // 
            this.optExtractive.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.optExtractive.AutoSize = true;
            this.optExtractive.Location = new System.Drawing.Point(287, 57);
            this.optExtractive.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.optExtractive.Name = "optExtractive";
            this.optExtractive.Size = new System.Drawing.Size(276, 46);
            this.optExtractive.TabIndex = 3;
            this.optExtractive.TabStop = true;
            this.optExtractive.Text = "Extractive";
            this.optExtractive.UseVisualStyleBackColor = true;
            this.optExtractive.CheckedChanged += new System.EventHandler(this.optExtractive_CheckedChanged);
            // 
            // optLightStick
            // 
            this.optLightStick.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.optLightStick.AutoSize = true;
            this.optLightStick.Location = new System.Drawing.Point(4, 57);
            this.optLightStick.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.optLightStick.Name = "optLightStick";
            this.optLightStick.Size = new System.Drawing.Size(275, 46);
            this.optLightStick.TabIndex = 2;
            this.optLightStick.TabStop = true;
            this.optLightStick.Text = "Light Stick";
            this.optLightStick.UseVisualStyleBackColor = true;
            this.optLightStick.CheckedChanged += new System.EventHandler(this.optLightStick_CheckedChanged);
            // 
            // optOpenPath
            // 
            this.optOpenPath.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.optOpenPath.AutoSize = true;
            this.optOpenPath.Location = new System.Drawing.Point(4, 4);
            this.optOpenPath.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.optOpenPath.Name = "optOpenPath";
            this.optOpenPath.Size = new System.Drawing.Size(275, 45);
            this.optOpenPath.TabIndex = 1;
            this.optOpenPath.TabStop = true;
            this.optOpenPath.Text = "Open Path";
            this.optOpenPath.UseVisualStyleBackColor = true;
            this.optOpenPath.CheckedChanged += new System.EventHandler(this.optOpenPath_CheckedChanged);
            // 
            // optHound
            // 
            this.optHound.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.optHound.AutoSize = true;
            this.optHound.Location = new System.Drawing.Point(287, 4);
            this.optHound.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.optHound.Name = "optHound";
            this.optHound.Size = new System.Drawing.Size(276, 45);
            this.optHound.TabIndex = 0;
            this.optHound.TabStop = true;
            this.optHound.Text = "Hound";
            this.optHound.UseVisualStyleBackColor = true;
            this.optHound.CheckedChanged += new System.EventHandler(this.optHound_CheckedChanged);
            // 
            // btnOxySetting
            // 
            this.btnOxySetting.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOxySetting.Image = global::UV_Quant.Properties.Resources.argos_logo_small;
            this.btnOxySetting.Location = new System.Drawing.Point(475, 304);
            this.btnOxySetting.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOxySetting.Name = "btnOxySetting";
            this.btnOxySetting.Size = new System.Drawing.Size(105, 118);
            this.btnOxySetting.TabIndex = 55;
            this.btnOxySetting.Text = "Oxygen Setting";
            this.btnOxySetting.UseVisualStyleBackColor = true;
            this.btnOxySetting.Click += new System.EventHandler(this.button8_Click);
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.lblAvSerial);
            this.groupBox14.Controls.Add(this.lblAvSpecType);
            this.groupBox14.Controls.Add(this.lblAvSpecStatus);
            this.groupBox14.Controls.Add(this.label38);
            this.groupBox14.Controls.Add(this.label39);
            this.groupBox14.Controls.Add(this.label40);
            this.groupBox14.Controls.Add(this.button7);
            this.groupBox14.Controls.Add(this.groupBox15);
            this.groupBox14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox14.Location = new System.Drawing.Point(8, 293);
            this.groupBox14.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox14.Size = new System.Drawing.Size(452, 130);
            this.groupBox14.TabIndex = 54;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "Avantes Spectrometer";
            // 
            // lblAvSerial
            // 
            this.lblAvSerial.AutoSize = true;
            this.lblAvSerial.BackColor = System.Drawing.Color.Transparent;
            this.lblAvSerial.Location = new System.Drawing.Point(309, 20);
            this.lblAvSerial.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAvSerial.Name = "lblAvSerial";
            this.lblAvSerial.Size = new System.Drawing.Size(26, 17);
            this.lblAvSerial.TabIndex = 53;
            this.lblAvSerial.Text = "---";
            // 
            // lblAvSpecType
            // 
            this.lblAvSpecType.AutoSize = true;
            this.lblAvSpecType.BackColor = System.Drawing.Color.Transparent;
            this.lblAvSpecType.Location = new System.Drawing.Point(309, 50);
            this.lblAvSpecType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAvSpecType.Name = "lblAvSpecType";
            this.lblAvSpecType.Size = new System.Drawing.Size(26, 17);
            this.lblAvSpecType.TabIndex = 52;
            this.lblAvSpecType.Text = "---";
            // 
            // lblAvSpecStatus
            // 
            this.lblAvSpecStatus.AutoSize = true;
            this.lblAvSpecStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblAvSpecStatus.Location = new System.Drawing.Point(309, 82);
            this.lblAvSpecStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAvSpecStatus.Name = "lblAvSpecStatus";
            this.lblAvSpecStatus.Size = new System.Drawing.Size(115, 17);
            this.lblAvSpecStatus.TabIndex = 51;
            this.lblAvSpecStatus.Text = "Not Connected";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.BackColor = System.Drawing.Color.Transparent;
            this.label38.Location = new System.Drawing.Point(156, 86);
            this.label38.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(54, 17);
            this.label38.TabIndex = 50;
            this.label38.Text = "Status";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.BackColor = System.Drawing.Color.Transparent;
            this.label39.Location = new System.Drawing.Point(156, 57);
            this.label39.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(146, 17);
            this.label39.TabIndex = 49;
            this.label39.Text = "Spectrometer Type";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.BackColor = System.Drawing.Color.Transparent;
            this.label40.Location = new System.Drawing.Point(156, 26);
            this.label40.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(111, 17);
            this.label40.TabIndex = 48;
            this.label40.Text = "Serial Number";
            // 
            // button7
            // 
            this.button7.Image = ((System.Drawing.Image)(resources.GetObject("button7.Image")));
            this.button7.Location = new System.Drawing.Point(8, 23);
            this.button7.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(140, 97);
            this.button7.TabIndex = 47;
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // groupBox15
            // 
            this.groupBox15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox15.Location = new System.Drawing.Point(584, 0);
            this.groupBox15.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox15.Size = new System.Drawing.Size(573, 130);
            this.groupBox15.TabIndex = 46;
            this.groupBox15.TabStop = false;
            this.groupBox15.Text = "Spectrometer";
            // 
            // button6
            // 
            this.button6.Enabled = false;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.ForeColor = System.Drawing.Color.Transparent;
            this.button6.Location = new System.Drawing.Point(1153, 553);
            this.button6.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(23, 17);
            this.button6.TabIndex = 51;
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Visible = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // btnSelectGases
            // 
            this.btnSelectGases.Location = new System.Drawing.Point(1012, 491);
            this.btnSelectGases.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSelectGases.Name = "btnSelectGases";
            this.btnSelectGases.Size = new System.Drawing.Size(153, 26);
            this.btnSelectGases.TabIndex = 24;
            this.btnSelectGases.Text = "Select Gases";
            this.btnSelectGases.UseVisualStyleBackColor = true;
            this.btnSelectGases.Visible = false;
            this.btnSelectGases.Click += new System.EventHandler(this.btnSelectGases_Click);
            // 
            // grpPasswordManager
            // 
            this.grpPasswordManager.Controls.Add(this.txtConfirmNewPass);
            this.grpPasswordManager.Controls.Add(this.txtNewPass);
            this.grpPasswordManager.Controls.Add(this.txtOldPass);
            this.grpPasswordManager.Controls.Add(this.label14);
            this.grpPasswordManager.Controls.Add(this.label13);
            this.grpPasswordManager.Controls.Add(this.label11);
            this.grpPasswordManager.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpPasswordManager.Location = new System.Drawing.Point(603, 293);
            this.grpPasswordManager.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpPasswordManager.Name = "grpPasswordManager";
            this.grpPasswordManager.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpPasswordManager.Size = new System.Drawing.Size(573, 130);
            this.grpPasswordManager.TabIndex = 50;
            this.grpPasswordManager.TabStop = false;
            this.grpPasswordManager.Text = "Change your password";
            // 
            // txtConfirmNewPass
            // 
            this.txtConfirmNewPass.Location = new System.Drawing.Point(201, 86);
            this.txtConfirmNewPass.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtConfirmNewPass.Name = "txtConfirmNewPass";
            this.txtConfirmNewPass.PasswordChar = '*';
            this.txtConfirmNewPass.Size = new System.Drawing.Size(261, 23);
            this.txtConfirmNewPass.TabIndex = 8;
            // 
            // txtNewPass
            // 
            this.txtNewPass.Location = new System.Drawing.Point(200, 54);
            this.txtNewPass.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtNewPass.Name = "txtNewPass";
            this.txtNewPass.PasswordChar = '*';
            this.txtNewPass.Size = new System.Drawing.Size(261, 23);
            this.txtNewPass.TabIndex = 7;
            // 
            // txtOldPass
            // 
            this.txtOldPass.Location = new System.Drawing.Point(200, 22);
            this.txtOldPass.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtOldPass.Name = "txtOldPass";
            this.txtOldPass.PasswordChar = '*';
            this.txtOldPass.Size = new System.Drawing.Size(261, 23);
            this.txtOldPass.TabIndex = 6;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(11, 91);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(171, 17);
            this.label14.TabIndex = 5;
            this.label14.Text = "Confirm New password";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(56, 59);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(111, 17);
            this.label13.TabIndex = 4;
            this.label13.Text = "New password";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(69, 27);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(106, 17);
            this.label11.TabIndex = 3;
            this.label11.Text = "Old password";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.bkgdSelect);
            this.groupBox9.Controls.Add(this.btnCalibGases);
            this.groupBox9.Controls.Add(this.btnTresholds);
            this.groupBox9.Controls.Add(this.btnLibraryTweaker);
            this.groupBox9.Controls.Add(this.optUseLibraryOnFile);
            this.groupBox9.Controls.Add(this.optSelectGases);
            this.groupBox9.Controls.Add(this.optPreDefinedLibrary);
            this.groupBox9.Controls.Add(this.cmbLibrary);
            this.groupBox9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox9.Location = new System.Drawing.Point(8, 431);
            this.groupBox9.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox9.Size = new System.Drawing.Size(452, 160);
            this.groupBox9.TabIndex = 49;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Current Library";
            // 
            // bkgdSelect
            // 
            this.bkgdSelect.Location = new System.Drawing.Point(8, 128);
            this.bkgdSelect.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.bkgdSelect.Name = "bkgdSelect";
            this.bkgdSelect.Size = new System.Drawing.Size(153, 27);
            this.bkgdSelect.TabIndex = 31;
            this.bkgdSelect.Text = "Bkgd Select";
            this.bkgdSelect.UseVisualStyleBackColor = true;
            this.bkgdSelect.Click += new System.EventHandler(this.bkgdSelect_Click);
            // 
            // btnCalibGases
            // 
            this.btnCalibGases.Location = new System.Drawing.Point(171, 128);
            this.btnCalibGases.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCalibGases.Name = "btnCalibGases";
            this.btnCalibGases.Size = new System.Drawing.Size(139, 27);
            this.btnCalibGases.TabIndex = 30;
            this.btnCalibGases.Text = "Calibration";
            this.btnCalibGases.UseVisualStyleBackColor = true;
            this.btnCalibGases.Click += new System.EventHandler(this.btnCalibGases_Click);
            // 
            // btnTresholds
            // 
            this.btnTresholds.Location = new System.Drawing.Point(171, 94);
            this.btnTresholds.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnTresholds.Name = "btnTresholds";
            this.btnTresholds.Size = new System.Drawing.Size(137, 27);
            this.btnTresholds.TabIndex = 29;
            this.btnTresholds.Text = "Set Limits";
            this.btnTresholds.UseVisualStyleBackColor = true;
            this.btnTresholds.Click += new System.EventHandler(this.btnTresholds_Click);
            // 
            // btnLibraryTweaker
            // 
            this.btnLibraryTweaker.Location = new System.Drawing.Point(8, 94);
            this.btnLibraryTweaker.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnLibraryTweaker.Name = "btnLibraryTweaker";
            this.btnLibraryTweaker.Size = new System.Drawing.Size(153, 27);
            this.btnLibraryTweaker.TabIndex = 28;
            this.btnLibraryTweaker.Text = "Tweak Library";
            this.btnLibraryTweaker.UseVisualStyleBackColor = true;
            this.btnLibraryTweaker.Click += new System.EventHandler(this.btnLibraryTweaker_Click);
            // 
            // optUseLibraryOnFile
            // 
            this.optUseLibraryOnFile.AutoSize = true;
            this.optUseLibraryOnFile.Location = new System.Drawing.Point(171, 60);
            this.optUseLibraryOnFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.optUseLibraryOnFile.Name = "optUseLibraryOnFile";
            this.optUseLibraryOnFile.Size = new System.Drawing.Size(227, 26);
            this.optUseLibraryOnFile.TabIndex = 27;
            this.optUseLibraryOnFile.TabStop = true;
            this.optUseLibraryOnFile.Text = "Use Library On File";
            this.optUseLibraryOnFile.UseVisualStyleBackColor = true;
            this.optUseLibraryOnFile.CheckedChanged += new System.EventHandler(this.optUseLibraryOnFile_CheckedChanged);
            // 
            // optSelectGases
            // 
            this.optSelectGases.AutoSize = true;
            this.optSelectGases.Location = new System.Drawing.Point(8, 60);
            this.optSelectGases.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.optSelectGases.Name = "optSelectGases";
            this.optSelectGases.Size = new System.Drawing.Size(167, 26);
            this.optSelectGases.TabIndex = 26;
            this.optSelectGases.TabStop = true;
            this.optSelectGases.Text = "Select Gases";
            this.optSelectGases.UseVisualStyleBackColor = true;
            this.optSelectGases.CheckedChanged += new System.EventHandler(this.optSelectGases_CheckedChanged);
            // 
            // optPreDefinedLibrary
            // 
            this.optPreDefinedLibrary.AutoSize = true;
            this.optPreDefinedLibrary.Location = new System.Drawing.Point(8, 22);
            this.optPreDefinedLibrary.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.optPreDefinedLibrary.Name = "optPreDefinedLibrary";
            this.optPreDefinedLibrary.Size = new System.Drawing.Size(281, 26);
            this.optPreDefinedLibrary.TabIndex = 25;
            this.optPreDefinedLibrary.TabStop = true;
            this.optPreDefinedLibrary.Text = "Use a Predefined Library";
            this.optPreDefinedLibrary.UseVisualStyleBackColor = true;
            this.optPreDefinedLibrary.CheckedChanged += new System.EventHandler(this.optPreDefinedLibrary_CheckedChanged);
            // 
            // cmbLibrary
            // 
            this.cmbLibrary.FormattingEnabled = true;
            this.cmbLibrary.Location = new System.Drawing.Point(236, 17);
            this.cmbLibrary.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbLibrary.Name = "cmbLibrary";
            this.cmbLibrary.Size = new System.Drawing.Size(152, 25);
            this.cmbLibrary.TabIndex = 22;
            this.cmbLibrary.SelectedIndexChanged += new System.EventHandler(this.cmbLibrary_SelectedIndexChanged);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.txtMaxIntValue);
            this.groupBox8.Controls.Add(this.txtMinIntValue);
            this.groupBox8.Controls.Add(this.label23);
            this.groupBox8.Controls.Add(this.label24);
            this.groupBox8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox8.Location = new System.Drawing.Point(781, 14);
            this.groupBox8.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox8.Size = new System.Drawing.Size(395, 130);
            this.groupBox8.TabIndex = 48;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Integration Period Limits";
            // 
            // txtMaxIntValue
            // 
            this.txtMaxIntValue.Location = new System.Drawing.Point(112, 70);
            this.txtMaxIntValue.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtMaxIntValue.Name = "txtMaxIntValue";
            this.txtMaxIntValue.Size = new System.Drawing.Size(100, 23);
            this.txtMaxIntValue.TabIndex = 30;
            // 
            // txtMinIntValue
            // 
            this.txtMinIntValue.Location = new System.Drawing.Point(112, 39);
            this.txtMinIntValue.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtMinIntValue.Name = "txtMinIntValue";
            this.txtMinIntValue.Size = new System.Drawing.Size(100, 23);
            this.txtMinIntValue.TabIndex = 29;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(37, 76);
            this.label23.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(73, 17);
            this.label23.TabIndex = 28;
            this.label23.Text = "Maximum";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(37, 43);
            this.label24.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(70, 17);
            this.label24.TabIndex = 27;
            this.label24.Text = "Minimum";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.btnSettings);
            this.groupBox7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox7.Location = new System.Drawing.Point(603, 14);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox7.Size = new System.Drawing.Size(159, 130);
            this.groupBox7.TabIndex = 45;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Settings";
            // 
            // btnSettings
            // 
            this.btnSettings.Image = ((System.Drawing.Image)(resources.GetObject("btnSettings.Image")));
            this.btnSettings.Location = new System.Drawing.Point(8, 23);
            this.btnSettings.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(143, 96);
            this.btnSettings.TabIndex = 22;
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click_1);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.lblBWSN);
            this.groupBox5.Controls.Add(this.lblBWSpectType);
            this.groupBox5.Controls.Add(this.lblBWStatus);
            this.groupBox5.Controls.Add(this.label20);
            this.groupBox5.Controls.Add(this.label21);
            this.groupBox5.Controls.Add(this.label22);
            this.groupBox5.Controls.Add(this.btnBWSpect);
            this.groupBox5.Controls.Add(this.groupBox6);
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(603, 151);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox5.Size = new System.Drawing.Size(573, 130);
            this.groupBox5.TabIndex = 47;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "BNW Tek Spectrometer";
            // 
            // lblBWSN
            // 
            this.lblBWSN.AutoSize = true;
            this.lblBWSN.BackColor = System.Drawing.Color.Transparent;
            this.lblBWSN.Location = new System.Drawing.Point(335, 23);
            this.lblBWSN.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBWSN.Name = "lblBWSN";
            this.lblBWSN.Size = new System.Drawing.Size(26, 17);
            this.lblBWSN.TabIndex = 59;
            this.lblBWSN.Text = "---";
            // 
            // lblBWSpectType
            // 
            this.lblBWSpectType.AutoSize = true;
            this.lblBWSpectType.BackColor = System.Drawing.Color.Transparent;
            this.lblBWSpectType.Location = new System.Drawing.Point(335, 54);
            this.lblBWSpectType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBWSpectType.Name = "lblBWSpectType";
            this.lblBWSpectType.Size = new System.Drawing.Size(26, 17);
            this.lblBWSpectType.TabIndex = 58;
            this.lblBWSpectType.Text = "---";
            // 
            // lblBWStatus
            // 
            this.lblBWStatus.AutoSize = true;
            this.lblBWStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblBWStatus.Location = new System.Drawing.Point(335, 86);
            this.lblBWStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBWStatus.Name = "lblBWStatus";
            this.lblBWStatus.Size = new System.Drawing.Size(115, 17);
            this.lblBWStatus.TabIndex = 57;
            this.lblBWStatus.Text = "Not Connected";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.label20.Location = new System.Drawing.Point(175, 84);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(54, 17);
            this.label20.TabIndex = 56;
            this.label20.Text = "Status";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.label21.Location = new System.Drawing.Point(175, 54);
            this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(146, 17);
            this.label21.TabIndex = 55;
            this.label21.Text = "Spectrometer Type";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.BackColor = System.Drawing.Color.Transparent;
            this.label22.Location = new System.Drawing.Point(175, 23);
            this.label22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(111, 17);
            this.label22.TabIndex = 54;
            this.label22.Text = "Serial Number";
            // 
            // btnBWSpect
            // 
            this.btnBWSpect.Image = ((System.Drawing.Image)(resources.GetObject("btnBWSpect.Image")));
            this.btnBWSpect.Location = new System.Drawing.Point(8, 21);
            this.btnBWSpect.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnBWSpect.Name = "btnBWSpect";
            this.btnBWSpect.Size = new System.Drawing.Size(140, 97);
            this.btnBWSpect.TabIndex = 48;
            this.btnBWSpect.UseVisualStyleBackColor = true;
            this.btnBWSpect.Click += new System.EventHandler(this.button4_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox6.Location = new System.Drawing.Point(584, 0);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox6.Size = new System.Drawing.Size(573, 130);
            this.groupBox6.TabIndex = 46;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Spectrometer";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblOOSN);
            this.groupBox2.Controls.Add(this.lblOOSpectType);
            this.groupBox2.Controls.Add(this.lblOOStatus);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.btnSpect);
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(8, 151);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(573, 130);
            this.groupBox2.TabIndex = 45;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "OceanOptics Spectrometer";
            // 
            // lblOOSN
            // 
            this.lblOOSN.AutoSize = true;
            this.lblOOSN.BackColor = System.Drawing.Color.Transparent;
            this.lblOOSN.Location = new System.Drawing.Point(309, 23);
            this.lblOOSN.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOOSN.Name = "lblOOSN";
            this.lblOOSN.Size = new System.Drawing.Size(26, 17);
            this.lblOOSN.TabIndex = 53;
            this.lblOOSN.Text = "---";
            // 
            // lblOOSpectType
            // 
            this.lblOOSpectType.AutoSize = true;
            this.lblOOSpectType.BackColor = System.Drawing.Color.Transparent;
            this.lblOOSpectType.Location = new System.Drawing.Point(309, 54);
            this.lblOOSpectType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOOSpectType.Name = "lblOOSpectType";
            this.lblOOSpectType.Size = new System.Drawing.Size(26, 17);
            this.lblOOSpectType.TabIndex = 52;
            this.lblOOSpectType.Text = "---";
            // 
            // lblOOStatus
            // 
            this.lblOOStatus.AutoSize = true;
            this.lblOOStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblOOStatus.Location = new System.Drawing.Point(309, 86);
            this.lblOOStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOOStatus.Name = "lblOOStatus";
            this.lblOOStatus.Size = new System.Drawing.Size(115, 17);
            this.lblOOStatus.TabIndex = 51;
            this.lblOOStatus.Text = "Not Connected";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Location = new System.Drawing.Point(156, 84);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(54, 17);
            this.label15.TabIndex = 50;
            this.label15.Text = "Status";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Location = new System.Drawing.Point(156, 54);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(146, 17);
            this.label16.TabIndex = 49;
            this.label16.Text = "Spectrometer Type";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Location = new System.Drawing.Point(156, 23);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(111, 17);
            this.label17.TabIndex = 48;
            this.label17.Text = "Serial Number";
            // 
            // btnSpect
            // 
            this.btnSpect.Image = ((System.Drawing.Image)(resources.GetObject("btnSpect.Image")));
            this.btnSpect.Location = new System.Drawing.Point(8, 23);
            this.btnSpect.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSpect.Name = "btnSpect";
            this.btnSpect.Size = new System.Drawing.Size(140, 97);
            this.btnSpect.TabIndex = 47;
            this.btnSpect.UseVisualStyleBackColor = true;
            this.btnSpect.Click += new System.EventHandler(this.btnSpect_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(584, 0);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox4.Size = new System.Drawing.Size(573, 130);
            this.groupBox4.TabIndex = 46;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Spectrometer";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtOperator);
            this.groupBox3.Controls.Add(this.txtPathLength);
            this.groupBox3.Controls.Add(this.txtSiteName);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Enabled = false;
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(1077, 524);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Size = new System.Drawing.Size(68, 46);
            this.groupBox3.TabIndex = 44;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Site Information";
            this.groupBox3.Visible = false;
            // 
            // txtOperator
            // 
            this.txtOperator.Location = new System.Drawing.Point(207, 90);
            this.txtOperator.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtOperator.Name = "txtOperator";
            this.txtOperator.Size = new System.Drawing.Size(132, 23);
            this.txtOperator.TabIndex = 26;
            this.txtOperator.Text = "Operator";
            // 
            // txtPathLength
            // 
            this.txtPathLength.Location = new System.Drawing.Point(207, 58);
            this.txtPathLength.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPathLength.Name = "txtPathLength";
            this.txtPathLength.Size = new System.Drawing.Size(132, 23);
            this.txtPathLength.TabIndex = 25;
            this.txtPathLength.Text = "1";
            // 
            // txtSiteName
            // 
            this.txtSiteName.Enabled = false;
            this.txtSiteName.Location = new System.Drawing.Point(207, 25);
            this.txtSiteName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSiteName.Name = "txtSiteName";
            this.txtSiteName.Size = new System.Drawing.Size(132, 23);
            this.txtSiteName.TabIndex = 24;
            this.txtSiteName.Text = "Argos";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(16, 92);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 17);
            this.label6.TabIndex = 23;
            this.label6.Text = "Operator";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(16, 63);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 17);
            this.label5.TabIndex = 22;
            this.label5.Text = "Path Length";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(16, 32);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 17);
            this.label4.TabIndex = 21;
            this.label4.Text = "Site Name";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.groupBox13);
            this.tabPage4.Controls.Add(this.groupBox12);
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage4.Size = new System.Drawing.Size(1183, 615);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Data Summary";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // groupBox13
            // 
            this.groupBox13.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox13.Controls.Add(this.lblDataSummary);
            this.groupBox13.Controls.Add(this.button5);
            this.groupBox13.Controls.Add(this.label29);
            this.groupBox13.Controls.Add(this.label28);
            this.groupBox13.Controls.Add(this.label19);
            this.groupBox13.Controls.Add(this.txtSigStrTreshold);
            this.groupBox13.Controls.Add(this.txtConcTreshold);
            this.groupBox13.Controls.Add(this.cmbGases);
            this.groupBox13.Controls.Add(this.button4);
            this.groupBox13.Location = new System.Drawing.Point(19, 15);
            this.groupBox13.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox13.Size = new System.Drawing.Size(1144, 128);
            this.groupBox13.TabIndex = 1;
            this.groupBox13.TabStop = false;
            // 
            // lblDataSummary
            // 
            this.lblDataSummary.AutoSize = true;
            this.lblDataSummary.Location = new System.Drawing.Point(8, 105);
            this.lblDataSummary.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDataSummary.Name = "lblDataSummary";
            this.lblDataSummary.Size = new System.Drawing.Size(0, 17);
            this.lblDataSummary.TabIndex = 8;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(629, 39);
            this.button5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(89, 26);
            this.button5.TabIndex = 7;
            this.button5.Text = "Browse...";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(249, 18);
            this.label29.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(156, 17);
            this.label29.TabIndex = 6;
            this.label29.Text = "Concentration Treshold";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(445, 20);
            this.label28.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(161, 17);
            this.label28.TabIndex = 5;
            this.label28.Text = "Signal Strengh Treshold";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(8, 20);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(109, 17);
            this.label19.TabIndex = 4;
            this.label19.Text = "Gases to Graph";
            // 
            // txtSigStrTreshold
            // 
            this.txtSigStrTreshold.Location = new System.Drawing.Point(449, 41);
            this.txtSigStrTreshold.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSigStrTreshold.Name = "txtSigStrTreshold";
            this.txtSigStrTreshold.Size = new System.Drawing.Size(155, 22);
            this.txtSigStrTreshold.TabIndex = 3;
            // 
            // txtConcTreshold
            // 
            this.txtConcTreshold.Location = new System.Drawing.Point(253, 39);
            this.txtConcTreshold.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtConcTreshold.Name = "txtConcTreshold";
            this.txtConcTreshold.Size = new System.Drawing.Size(151, 22);
            this.txtConcTreshold.TabIndex = 2;
            // 
            // cmbGases
            // 
            this.cmbGases.FormattingEnabled = true;
            this.cmbGases.Location = new System.Drawing.Point(8, 39);
            this.cmbGases.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbGases.Name = "cmbGases";
            this.cmbGases.Size = new System.Drawing.Size(203, 24);
            this.cmbGases.TabIndex = 1;
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.BackgroundImage = global::UV_Quant.Properties.Resources.argos_logo_small;
            this.button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(980, 16);
            this.button4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(133, 105);
            this.button4.TabIndex = 0;
            this.button4.Text = "Load";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click_1);
            // 
            // groupBox12
            // 
            this.groupBox12.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox12.Controls.Add(this.gas_graph);
            this.groupBox12.Location = new System.Drawing.Point(19, 153);
            this.groupBox12.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox12.Size = new System.Drawing.Size(1144, 444);
            this.groupBox12.TabIndex = 0;
            this.groupBox12.TabStop = false;
            // 
            // gas_graph
            // 
            this.gas_graph.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gas_graph.Location = new System.Drawing.Point(24, 23);
            this.gas_graph.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.gas_graph.Name = "gas_graph";
            this.gas_graph.ScrollGrace = 0D;
            this.gas_graph.ScrollMaxX = 0D;
            this.gas_graph.ScrollMaxY = 0D;
            this.gas_graph.ScrollMaxY2 = 0D;
            this.gas_graph.ScrollMinX = 0D;
            this.gas_graph.ScrollMinY = 0D;
            this.gas_graph.ScrollMinY2 = 0D;
            this.gas_graph.Size = new System.Drawing.Size(1089, 391);
            this.gas_graph.TabIndex = 2;
            // 
            // data_summary_file_dlg
            // 
            this.data_summary_file_dlg.FileName = "openDSFile";
            // 
            // timer_low_ox
            // 
            this.timer_low_ox.Tick += new System.EventHandler(this.timer_low_ox_Tick);
            // 
            // timer_high_ox
            // 
            this.timer_high_ox.Tick += new System.EventHandler(this.timer_high_ox_Tick);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 9;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 86F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 108F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 86F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.textBox1, 8, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(200, 100);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(609, 3);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.tableLayoutPanel1.SetRowSpan(this.textBox1, 4);
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(1, 94);
            this.textBox1.TabIndex = 42;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.BackColor = System.Drawing.Color.Transparent;
            this.label30.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.Location = new System.Drawing.Point(3, 10);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(138, 20);
            this.label30.TabIndex = 24;
            this.label30.Text = "Site Information";
            this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mainMenu
            // 
            this.mainMenu.BackColor = System.Drawing.Color.Transparent;
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.mainMenu.Size = new System.Drawing.Size(1220, 28);
            this.mainMenu.TabIndex = 21;
            this.mainMenu.Text = "menuStrip1";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectChemicalsToolStripMenuItem,
            this.setGasLimitsToolStripMenuItem,
            this.sampleTimeToolStripMenuItem,
            this.siteInformationToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(126, 24);
            this.editToolStripMenuItem.Text = "UV Quant Setup";
            // 
            // selectChemicalsToolStripMenuItem
            // 
            this.selectChemicalsToolStripMenuItem.Name = "selectChemicalsToolStripMenuItem";
            this.selectChemicalsToolStripMenuItem.Size = new System.Drawing.Size(189, 24);
            this.selectChemicalsToolStripMenuItem.Text = "Select Chemicals";
            this.selectChemicalsToolStripMenuItem.Click += new System.EventHandler(this.selectChemicalsToolStripMenuItem_Click);
            // 
            // setGasLimitsToolStripMenuItem
            // 
            this.setGasLimitsToolStripMenuItem.Name = "setGasLimitsToolStripMenuItem";
            this.setGasLimitsToolStripMenuItem.Size = new System.Drawing.Size(189, 24);
            this.setGasLimitsToolStripMenuItem.Text = "Set Gas Limits";
            this.setGasLimitsToolStripMenuItem.Click += new System.EventHandler(this.setGasLimitsToolStripMenuItem_Click);
            // 
            // sampleTimeToolStripMenuItem
            // 
            this.sampleTimeToolStripMenuItem.Name = "sampleTimeToolStripMenuItem";
            this.sampleTimeToolStripMenuItem.Size = new System.Drawing.Size(189, 24);
            this.sampleTimeToolStripMenuItem.Text = "Sample Time";
            this.sampleTimeToolStripMenuItem.Click += new System.EventHandler(this.sampleTimeToolStripMenuItem_Click);
            // 
            // siteInformationToolStripMenuItem
            // 
            this.siteInformationToolStripMenuItem.Name = "siteInformationToolStripMenuItem";
            this.siteInformationToolStripMenuItem.Size = new System.Drawing.Size(189, 24);
            this.siteInformationToolStripMenuItem.Text = "Site Information";
            this.siteInformationToolStripMenuItem.Click += new System.EventHandler(this.siteInformationToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.BackgroundImage = global::UV_Quant.Properties.Resources.argos_logo_small;
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uVQuantHelpToolStripMenuItem,
            this.registerToolStripMenuItem,
            this.aboutUVQuantToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // uVQuantHelpToolStripMenuItem
            // 
            this.uVQuantHelpToolStripMenuItem.Name = "uVQuantHelpToolStripMenuItem";
            this.uVQuantHelpToolStripMenuItem.Size = new System.Drawing.Size(186, 24);
            this.uVQuantHelpToolStripMenuItem.Text = "UV Quant Help";
            this.uVQuantHelpToolStripMenuItem.Click += new System.EventHandler(this.uVQuantHelpToolStripMenuItem_Click);
            // 
            // registerToolStripMenuItem
            // 
            this.registerToolStripMenuItem.Name = "registerToolStripMenuItem";
            this.registerToolStripMenuItem.Size = new System.Drawing.Size(186, 24);
            this.registerToolStripMenuItem.Text = "Register";
            this.registerToolStripMenuItem.Click += new System.EventHandler(this.registerToolStripMenuItem_Click);
            // 
            // aboutUVQuantToolStripMenuItem
            // 
            this.aboutUVQuantToolStripMenuItem.Name = "aboutUVQuantToolStripMenuItem";
            this.aboutUVQuantToolStripMenuItem.Size = new System.Drawing.Size(186, 24);
            this.aboutUVQuantToolStripMenuItem.Text = "About UV Quant";
            this.aboutUVQuantToolStripMenuItem.Click += new System.EventHandler(this.aboutUVQuantToolStripMenuItem_Click);
            // 
            // command_timer
            // 
            this.command_timer.Interval = 1000;
            this.command_timer.Tick += new System.EventHandler(this.command_timer_Tick);
            // 
            // metSerialPort
            // 
            this.metSerialPort.BaudRate = 38400;
            this.metSerialPort.PortName = "COM2";
            // 
            // autorun_timer
            // 
            this.autorun_timer.Interval = 1000;
            // 
            // MainConsole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1220, 720);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.mainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenu;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimumSize = new System.Drawing.Size(981, 482);
            this.Name = "MainConsole";
            this.Text = "UV Quant Suite";
            this.Load += new System.EventHandler(this.MainConsole_Load);
            this.Resize += new System.EventHandler(this.OnResize);
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.systemStatusPanel.ResumeLayout(false);
            this.systemStatusPanel.PerformLayout();
            this.sigStrePanel.ResumeLayout(false);
            this.sigStrePanel.PerformLayout();
            this.lsPanel.ResumeLayout(false);
            this.lsPanel.PerformLayout();
            this.groupBox11.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cm_datagrid)).EndInit();
            this.statusTbl.ResumeLayout(false);
            this.statusTbl.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rep_datagrid)).EndInit();
            this.repControlBox.ResumeLayout(false);
            this.repControlBox.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.groupBox14.ResumeLayout(false);
            this.groupBox14.PerformLayout();
            this.grpPasswordManager.ResumeLayout(false);
            this.grpPasswordManager.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.groupBox13.ResumeLayout(false);
            this.groupBox13.PerformLayout();
            this.groupBox12.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        /// <summary>
        /// Used to only save settings to the registry right before the form is closed
        /// </summary>
        private void MainConsole_FormClosing(Object sender, FormClosingEventArgs e)
        {
            saveRegistryEntries();
        }

        /// <summary>
        /// This method gets called right before the main form is loaded and displayed. 
        /// All pre processings such as library loading, parameter loading and display
        /// setups are performed int this method
        /// </summary>
        private void MainConsole_Load(object sender, System.EventArgs e)
        {
            string fake_serial = "MasterSerial";
            string fake_pass = Util.getPasswordFromSerial(fake_serial);

            if (Util.isExpired())
            {
                installationHasExpired();
                this.Text += " - Expired!";
                MessageBox.Show("Your installation has expired!");
            }
            else if (!Util.isActivated())
            {
                this.Text += " - Trial Version";
            }
            else
            {

                registerToolStripMenuItem.Enabled = false;
                // Making sure to alert the user that the program will expire - 2 month notice
                DateTime current_time = System.DateTime.Now;
                DateTime year_end = new DateTime(current_time.Year, 12, 31);
                System.TimeSpan date_difference = year_end - current_time;

                if (date_difference.Days < 60)
                {
                    MessageBox.Show("Please note that your software will expire in the next " + date_difference.Days.ToString()
                        + " days.\nPlease contact Argos for a new activation code");
                }
            }
           

           
           System.Drawing.Graphics formGraphics = this.CreateGraphics();
           string drawString = "SIGNAL STRENGTH";
           System.Drawing.Font drawFont = new System.Drawing.Font("Arial", 16);
           System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
           float x = 100;
           float y = 100;
           System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat(StringFormatFlags.DirectionVertical);
           formGraphics.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
           drawFont.Dispose();
           drawBrush.Dispose();
           formGraphics.Dispose();

           this.cleanErrorDisplay();
           rep_integration_period.Text = "";
           about_uv_quant.ShowSplashScreen();
           about_uv_quant.status = "Updating renderer...";

           ToolStripManager.Renderer = new Office2007Renderer();
           about_uv_quant.status = "Initializing graphing objects...";
           rep_graph.GraphPane = new GraphPane(rep_graph.GraphPane.Rect, "Argos Scientific Light Data", "Wavelength (nm)", "Light count");
           cm_graph.GraphPane = new GraphPane(cm_graph.GraphPane.Rect, "Argos Scientific Light Data", "Wavelength (nm)", "Light count");
           gas_graph.GraphPane = new GraphPane(gas_graph.GraphPane.Rect, "Gas Data", "Date", "PPB");

           //CreateChart(gas_graph);

           about_uv_quant.status = "Retreiving registry entries...";

           retrieveRegistryEntries();
           selectAppropriateHardware();

           processor.init();
           processor.loadLibraries();
           processor.loadCachedBackgrounds();
           processor.createTable(cm_datagrid, rep_datagrid);
           if (processor is PLSProcessor)
           {
               updateGasSelector();
               cmbLibrary.DataSource = processor.getLibrayForComboBox();
           }
           else
           {
               groupBox9.Visible = false;
           }

           processor.current_spectrometer = new Spectrometer(3, 0);

           if (connect_to_spectrometer(1))
           {
                spectrometer_attached();
           }
           else
           {
                spectrometer_disconnected();
           }

           about_uv_quant.status = "Updating state...";
           updateSignalStrengthDisplay();
           updateLShDisplay();

           if (ConsoleState.CONTINUOUS_MONITORING == processor.console_mode)
           {
               //w.WriteLine("contintous");
               //optCM.Checked = true;
               this.tabControl1.SelectedIndex = 0;
               current_tab_index = 0;
               this.tabControl1.TabPages[0].Focus();
               this.tabControl1.TabPages[0].Select();
           }
           else
           {
               this.tabControl1.SelectedIndex = 1;
               current_tab_index = 1;
               this.tabControl1.TabPages[1].Focus();
               this.tabControl1.TabPages[1].Select();
           }

            //we always want to go to the continuous monitoring page
           this.tabControl1.SelectedIndex = 0;
           current_tab_index = 0;

           form_width_min = ClientRectangle.Width;
           form_height_min = ClientRectangle.Height;

           Image image = Bitmap.FromFile(Application.StartupPath + "/argos_logo.JPG");

           // Fill the pane background with the image
           TextureBrush texBrush = new TextureBrush(image);
           rep_graph.GraphPane.Fill = new Fill(texBrush);

           rep_graph.GraphPane.Chart.Fill.IsVisible = false;
           // Hide the legend
           rep_graph.GraphPane.Legend.IsVisible = false;

           cm_graph.GraphPane.Fill = new Fill(texBrush);

           cm_graph.GraphPane.Chart.Fill.IsVisible = false;
           // Hide the legend
           cm_graph.GraphPane.Legend.IsVisible = false;

           gas_graph.GraphPane.Fill = new Fill(texBrush);
           gas_graph.GraphPane.Chart.Fill.IsVisible = false;
           gas_graph.GraphPane.Legend.IsVisible = true;

           lblDataSummary.Text = processor.ds_file;  

           about_uv_quant.status = "Preparing correct mode...";
           if (processor.admin_mode == 0)
           {
               txtOperator.Enabled = false;
               txtPathLength.Enabled = false;
               txtSiteName.Enabled = false;
               btnSpect.Visible = false;
               btnSettings.Visible = false;
               lblSettings.Visible = false;
           }
           else
           {
               txtOperator.Enabled = true;
               txtPathLength.Enabled = true;
               txtSiteName.Enabled = true;
               btnSpect.Visible = true;
               btnSettings.Visible = true;
               lblSettings.Visible = true;
           }

           resizer();

           about_uv_quant.CloseForm();

           //if password is not enabled, no need to have the manager
           if (!enable_password)
           {
               grpPasswordManager.Visible = false;
           }

           if (processor is OxygenProcessor)
           {
               this.tabControl1.Controls.Remove(this.tabPage4);
               this.tabControl1.Controls.Remove(this.tabPage2);
               MainMenuStrip.Items.Remove(MainMenuStrip.Items[0]);
               btnOxySetting.Visible = true;
           }
           else
           {
               btnOxySetting.Visible = false;
           }

           
            checkComamndBasedProcess();
           
            updateMainStatus("Waiting...", processor.site_name, txtStatus, statusBar);
           
            processor.finished_loading = true;

            if (processor.autorun && connect_to_spectrometer(1))
            {
                autorun_timer.Start();
                autorun_timer.Tick += new EventHandler(Timer_Tick);
            }

        }

        public void Timer_Tick(object sender, EventArgs eArgs)
        {                  
            if (sender == autorun_timer)
            {
                if (countdown == 0)
                {
                    autorun_timer.Stop();
                    btnMonitor.PerformClick();
                }
                else
                    updateMainStatus("Scanning to begin automatically in: " + (--countdown) + " seconds", processor.site_name, txtStatus, statusBar);
            }
        }


        /// <summary>
        /// This method is called once reprocessing is done
        /// </summary>
        private void doneProcessing()
        {
            processor.processing = false;
            button1.Text = "Process!";
        }

        /// <summary>
        /// This method is called by the .NET framework everytime the
        /// main form is resized. From this method, we inturn call
        /// our custom method <code>resizer()</code>
        /// </summary>
        private void OnResize(object sender, System.EventArgs e)
        {
            resizer();
        }

        /// <summary>
        /// This method resizes the components of the form based on the
        /// current size of the form
        /// </summary>
        private void resizer()
        {
            updateSignalStrengthDisplay();
            updateLShDisplay();
            int width = cm_datagrid.Width;

            if (false)//!tab_changing)
            {
                int current_width;
                int current_height;
                current_width = this.ClientRectangle.Width;
                current_height = this.ClientRectangle.Height;

                rep_graph.Size = new Size(2 * (current_width - 100) / 3, rep_graph.Size.Height);
                rep_datagrid.Size = new Size((current_width - 20) / 3, rep_datagrid.Height);
                rep_graph.Location = new System.Drawing.Point(rep_datagrid.Width + 20, rep_graph.Location.Y);
                for (int j = 0; j < rep_datagrid.Columns.Count; ++j)
                {
                    rep_datagrid.Columns[j].Width = rep_datagrid.Width / 3 - 30;
                    int k = rep_datagrid.Location.X;
                    int d = rep_datagrid[0, 0].DataGridView.Location.X;
                }
                rep_datagrid.Columns[2].Width = 30;

                cm_graph.Size = new Size(2 * (current_width - 100) / 3, cm_graph.Size.Height);
                cm_datagrid.Size = new Size((current_width - 20) / 3, cm_datagrid.Height);
                cm_graph.Location = new System.Drawing.Point(cm_datagrid.Width, cm_graph.Location.Y);
                for (int j = 0; j < cm_datagrid.Columns.Count; ++j)
                {
                    cm_datagrid.Columns[j].Width = cm_datagrid.Width / 3 - 30;
                    int k = cm_datagrid.Location.X;
                    int d = cm_datagrid[0, 0].DataGridView.Location.X;
                }
                cm_datagrid.Columns[2].Width = 30;

                //cmControlBox.Width = current_width;
            }
            processor.tab_changing = false;
        }


        /// <summary>
        /// This inappropriately called method is called when the start Reprocessing
        /// button is clicked.
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            if (processor.processing)
            {
                processor.processing = false;
                button1.Text = "Process!";
            }
            else
            {
                processor.bkgd_file = bgkdFile.Text;
                processor.data_folder = dataFolder.Text;

                processor.processing = true;
                button1.Text = "Stop!";
                processor.reprocessData(cm_datagrid, rep_datagrid, cm_graph, rep_graph, txtStatus, statusBar);
                doneProcessing();
            }
        }

        /// <summary>
        /// This method is called when the browse button for the data folder in reprocessing mode
        /// is selected
        /// </summary>
        private void button3_Click(object sender, EventArgs e)
        {
            if (!singleFile.Checked)
            {
                data_folder_source.ShowDialog();
                dataFolder.Text = data_folder_source.SelectedPath;
                processor.data_folder = dataFolder.Text;
            }
            else
            {
                OpenFileDialog data_file_source = new OpenFileDialog();
                data_file_source.Filter = "CSV Files|UVQuant*.csv";
                data_file_source.ShowDialog();
                dataFolder.Text = data_file_source.FileName;
                processor.data_folder = dataFolder.Text;
            }
        }

        /// <summary>
        /// This method is called when the browse button for the background file in reprocessing
        /// mod is slected
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog bkgd_file_source = new OpenFileDialog();
            bkgd_file_source.Filter = "CSV Files|*.csv";
            bkgd_file_source.ShowDialog();
            bgkdFile.Text = bkgd_file_source.FileName;
            processor.bkgd_file = bgkdFile.Text;
            bkgd_file_source.Dispose();
        }

         /// <summary>
        /// All parameters of UV Quant are stored in the registery. This utility method
        /// stores all the current instances into the registery at: 
        /// <code>HKEY_LOCAL_MACHINE\SOFTWARE\ArogsQuant</code>
        /// </summary>
        public void saveRegistryEntries()
        {
            processor.saveRegistryEntries();
        }


         /// <summary>
        /// This method is used to retrieve all the saved parameters from the registery
        /// at location:
        /// <code>HKEY_LOCAL_MACHINE\SOFTWARE\ArogsQuant</code>
        /// </summary>
        public void retrieveRegistryEntries()
        {
            RegistryKey regKey = Registry.LocalMachine.OpenSubKey(@"Software\ArgosQuant");
            if (regKey == null)
            {
                regKey = Registry.LocalMachine.CreateSubKey(@"Software\ArgosQuant");
            }

            processor.retrieveRegistryEntries();
            processor.console_mode = 0;
            lblAverages.Text = Convert.ToString(processor.collection_time);
            lblBoxCar.Text = Convert.ToString(processor.box_car);
            txtOperator.Text = processor.uvs_operator;
            lblOperator.Text = processor.uvs_operator;
            txtPathLength.Text = Convert.ToString(processor.path_length);
            lblPathLength.Text = Convert.ToString(processor.path_length);
            lblShiftAmount.Text = Convert.ToString(processor.shift_amount);
            txtSiteName.Text = processor.site_name;
            lblSiteName.Text = processor.site_name;

            if (!optUseLibraryOnFile.Checked)
            {
                lblLibraryName2.Text = processor.loaded_library_name;
                cmbLibrary.Text = processor.loaded_library_name;
            }

            if (processor.dynamic_integration == 1)
            {
                lblDynamicIntTime.Text = "On";
            }
            else
            {
                lblDynamicIntTime.Text = "Off";
            }

            if (processor.dynamic_background == 1)
            {
                lblBackgroundUpdate.Text = "On";
            }
            else
            {
                lblBackgroundUpdate.Text = "Off";
            }

            bgkdFile.Text = processor.bkgd_file;
            dataFolder.Text = processor.data_folder;
            txtMinIntValue.Text = Convert.ToString(processor.min_int_value);
            txtMaxIntValue.Text = Convert.ToString(processor.max_int_value);

            statusBar.Items["file_count"].Text = "File Number: " + processor.file_number;

            if (processor is OxygenProcessor)
            {
                timer_high_ox.Interval = processor.calibration_duration * 60 * 1000;
                timer_low_ox.Interval = processor.calibration_interval * 60 * 1000;
            }
        }

        /// <summary>
        /// This method is used to retrieve information from the GUI
        /// and update our variables.
        /// </summary>
        public void updateVariablesFromGUI()
        {
            try
            {
                //shift_amount = Int32.Parse(shift.Text);
                processor.uvs_operator = txtOperator.Text;
                processor.path_length = Double.Parse(txtPathLength.Text);
                processor.site_name = txtSiteName.Text;
            }
            catch (Exception exp)
            {
                logError(exp);
                MessageBox.Show("Please make sure you specify the correct \nShift amount,\nOperater,\nPath length and,\nSite Name!");
            }
        }

        /// <summary>
        /// This method is called by the .NET framework when we click on the continuous montiring button
        /// </summary>
        private void btnMonitor_Click(object sender, EventArgs e)
        {
            if (!processor.scanning)
            {
                updateMainStatus("Trying to validate parameters...", processor.site_name, txtStatus, statusBar);

                if (validateCM())
                {
                    cleanErrorDisplay();
                    updateMainStatus("Validated...", processor.site_name, txtStatus, statusBar);
                    processor.scanning = true;
                    updateMainStatus("Setting state...", processor.site_name, txtStatus, statusBar);
                    scanning_started();
                    updateMainStatus("Saving settings to the registry...", processor.site_name, txtStatus, statusBar);
                    updateVariablesFromGUI();
                    saveRegistryEntries();

                    if (processor.calibration_enabled)
                    {
                        updateMainStatus("Starting calibration timer...", processor.site_name, txtStatus, statusBar);
                        timer_low_ox.Start();
                    }
                    processor.adjustIntegrationTime(txtStatus, statusBar);
                    updateMainStatus("Starting main acquisition...", processor.site_name, txtStatus, statusBar);

                    Image image = Bitmap.FromFile(Application.StartupPath + "/argos_logo_small_running.jpg");
                    btnMonitor.BackgroundImage = image;
                    
                    scanData();

                    image = Bitmap.FromFile(Application.StartupPath + "/argos_logo_small.JPG");
                    btnMonitor.BackgroundImage = image;
                }
                else
                {
                    updateMainStatus("Validation failed...", processor.site_name, txtStatus, statusBar);
                    MessageBox.Show("Please specify correct parameters!");
                    updateMainStatus("Waiting...", processor.site_name, txtStatus, statusBar);
                }
            }
            else
            {
                processor.scanning = false;
                timer_low_ox.Stop();
                scanning_stopped();
                saveRegistryEntries();
                updateMainStatus("Waiting...", processor.site_name, txtStatus, statusBar);
            }
        }


        /// <summary>
        /// This method sets the appropriate variables 
        /// when a spectrometer is attached
        /// </summary>
        public void spectrometer_attached()
        {
            btnMonitor.Enabled = true;
            enableAlignButton();
            btnCollectBkgd.Enabled = true;
            btnCollectHighO2.Enabled = true;
            btnCollectLowO2.Enabled = true;
            btnCalibrate.Enabled = true;

            lblSerialNumber.Text = processor.current_spectrometer.serial_number;
            lblSpectStatus.Text = "Connected";
            lblSpectType.Text = processor.current_spectrometer.adc_type;
            if (processor.current_spectrometer.adc_type == BNWSpectrometer.SPECTROMETER_TYPE_BNWTEC)
            {
                lblBWSN.Text = processor.current_spectrometer.serial_number;
                lblBWStatus.Text = "Connected";
                lblBWSpectType.Text = processor.current_spectrometer.adc_type;
            }
            else if (processor.current_spectrometer.adc_type == AvantesSpectrometer.SPECTROMETER_TYPE_AVANTES)
            {
                lblAvSerial.Text = processor.current_spectrometer.serial_number;
                lblAvSpecStatus.Text = "Connected";
                lblAvSpecType.Text = processor.current_spectrometer.adc_type;
            }
            else
            {
                lblOOSN.Text = processor.current_spectrometer.serial_number;
                lblOOStatus.Text = "Connected";
                lblOOSpectType.Text = processor.current_spectrometer.adc_type;
            }
        }

        /// <summary>
        /// This method sets the appropriate variables 
        /// when a spectrometer is disconnected
        /// </summary>
        public void spectrometer_disconnected()
        {
            btnMonitor.Enabled = false;
            btnAlign.Enabled = false;
            btnCollectBkgd.Enabled = false;
            btnCollectLowO2.Enabled = false;
            btnCollectHighO2.Enabled = false;
            btnCalibrate.Enabled = false;

            lblSerialNumber.Text = "---";
            lblSpectStatus.Text = "Not Connected";
            lblSpectType.Text = "---";

            lblBWSN.Text = "---";
            lblBWStatus.Text = "Not Connected";
            lblBWSpectType.Text = "---";

            lblOOSN.Text = "---";
            lblOOStatus.Text = "Not Connected";
            lblOOSpectType.Text = "---";
        }

        /// <summary>
        /// This method sets the appropriate variables 
        /// when scanning is started (actually called right before
        /// scanning starts)
        /// </summary>
        public void scanning_started()
        {
            btnMonitor.Text = "STOP";
            btnAlign.Enabled = false;
            btnSpect.Enabled = false;
            btnSettings.Enabled = false;
            txtSiteName.Enabled = false;
            txtOperator.Enabled = false;
            txtPathLength.Enabled = false;
            btnCollectBkgd.Enabled = false;
            btnCollectHighO2.Enabled = false;
            btnCollectLowO2.Enabled = false;
            btnCalibrate.Enabled = false;
        }

        /// <summary>
        /// This method sets the appropriate variables 
        /// when scanning stops (actually called right after
        /// scanning stops)
        /// </summary>
        public void scanning_stopped()
        {
            btnMonitor.Text = "START";
            enableAlignButton();
            btnSpect.Enabled = true;
            btnSettings.Enabled = true;
            txtSiteName.Enabled = true;
            txtOperator.Enabled = true;
            txtPathLength.Enabled = true;
            btnCollectBkgd.Enabled = true;
            btnCollectLowO2.Enabled = true;
            btnCollectHighO2.Enabled = true;
            btnCalibrate.Enabled = true;
        }

       

        /// <summary>
        /// This method enables all conintuous monitoring components
        /// when the continuous monitoring tab is selected
        /// </summary>
        public void enableCM(bool enable)
        {
            if (enable)
            {
                //cmControlBox.Enabled = true;
                enableAlignButton();
                processor.console_mode = ConsoleState.CONTINUOUS_MONITORING;
            }
            else
            {
                //cmControlBox.Enabled = false;
                btnAlign.Enabled = false;
            }
        }

        /// <summary>
        /// This method enables all reprocessing components
        /// when the reprocessing tab is selected
        /// </summary>
        public void enableReprocessing(bool enable)
        {
            if (enable)
            {
                repControlBox.Enabled = true;
                processor.console_mode = ConsoleState.REPROCESSING;
            }
            else
            {
                repControlBox.Enabled = false;
            }
        }

        /// <summary>
        /// This method gets called when the continuous 
        /// monitoring option is selected
        /// </summary>
        private void optCM_CheckedChanged(object sender, EventArgs e)
        {
            enableCM(true);
            enableReprocessing(false);

            if (connect_to_spectrometer(1))
            {
                spectrometer_attached();
            }
            else
            {
                spectrometer_disconnected();
            }
        }

        /// <summary>
        /// This method is called when the reprocessing
        /// option is selected
        /// </summary>
        private void optRep_CheckedChanged()
        {
            enableCM(false);
            enableReprocessing(true);
        }


        /// <summary>
        /// This method is used to validate that all appropriate
        /// variables are set correctly for continuous monitoring
        /// purposes
        /// </summary>
        public bool validateCM()
        {
            if (txtPathLength.Text != "" && txtPathLength.Text != null &&
                txtSiteName.Text != "" && txtSiteName.Text != null &&
                txtOperator.Text != "" && txtSiteName.Text != null)
            {
                try
                {
                    processor.uvs_operator = txtOperator.Text;
                    processor.path_length = Double.Parse(txtPathLength.Text);
                    processor.site_name = txtSiteName.Text;
                }
                catch (Exception exp)
                {
                    logError(exp);
                    return false;
                }

                return true;
            }
            else
            {
                return false;
            }
        }
        
        /// <summary>
        /// This method is responsible for directing all real time data collection and
        /// processing. It acts the manager of the whole continuous monitoring.
        /// </summary>
        public void scanData()
        {
            while (processor.scanning)
            {
                try
                {
                    double[,] values = processor.scanData(statusBar, txtStatus);
                   
                    if (values == null)
                    {
                        scanning_stopped();
                        spectrometer_disconnected();
                        MessageBox.Show("Spectrometer Disconnected!!!");
                        return;
                    }

                    if (!processor.scanning)
                    {
                        updateMainStatus("Stopping scanning...", processor.site_name, txtStatus, statusBar);
                        return;
                    }

                    // Now you can increment file number
                    if (processor.shouldProcessData())
                    {
                        processor.incrementFileCount(statusBar);

                        updateSignalStrengthDisplay();
                        updateLShDisplay();

                        processor.compileData(values, txtStatus, statusBar);
                        processor.saveSingleBeam(txtStatus, statusBar);
                        processor.displayGraph(processor.current_data.getPointPairList(), cm_graph, rep_graph, txtStatus, statusBar);
                        processor.performQuantifications(values, cm_datagrid, rep_datagrid, txtStatus, statusBar);
                        processor.adjustIntegrationTime(txtStatus, statusBar);
                        processor.endIteration(statusBar, txtStatus);
                    }
                }
                catch (Exception exp)
                {
                    updateMainStatus("Error!", processor.site_name, txtStatus, statusBar);
                    logError(exp);
                }
            }
        }
        /// <summary>
        /// This method displays the form to enter user notes into
        /// and hanles the update of the <code>user_notes</code> with the
        /// note entered in the form
        /// </summary>
        private void btnNotes_Click(object sender, EventArgs e)
        {
            Image image = Bitmap.FromFile(Application.StartupPath + "/argos_logo_small_running.jpg");
            btnNotes.BackgroundImage = image;

            UserNotes usernotes = new UserNotes();
            usernotes.ShowDialog();
            
            RegistryKey regKey = Registry.LocalMachine.OpenSubKey(@"Software\ArgosQuant");
            if (regKey != null)
            {
                processor.user_notes = (string)regKey.GetValue("user_notes", "");
            }

            image = Bitmap.FromFile(Application.StartupPath + "/argos_logo_small.jpg");
            btnNotes.BackgroundImage = image;
        }

        /// <summary>
        /// This is a utility method used to update the stats displayed at the
        /// status bar of the main console (bottom of the screen)
        /// </summary>
        public static void updateMainStatus(
            string status_update, 
            string site_name, 
            TextBox txtStatus,
            StatusStrip statsu_strip)
        {
            if (!Directory.Exists("C:\\" + site_name))
            {
                Directory.CreateDirectory("C:\\" + site_name);
            }


            string site_dir = "C:\\" + site_name + "\\" +
                System.DateTime.Now.Year + "-" +
                System.DateTime.Now.Month + "-" +
                System.DateTime.Now.Day;

            if (!Directory.Exists(site_dir))
            {
                Directory.CreateDirectory(site_dir);
            }

            string log_file_name = "performance.log";

            StreamWriter w = File.AppendText(site_dir + "\\" + log_file_name);

            w.WriteLine(System.DateTime.Now.ToString() + '\t' + status_update);

            w.Close();
            statsu_strip.Items["current_status"].Text = status_update;
            if(txtStatus.Text.Trim()!="")
                txtStatus.Text = txtStatus.Text + Environment.NewLine + System.DateTime.Now.ToString() + '\t' + status_update;
            else
                txtStatus.Text = System.DateTime.Now.ToString() + '\t' + status_update;

            txtStatus.SelectionStart = txtStatus.Text.Length;
            txtStatus.ScrollToCaret();
        }


        /// <summary>
        /// This utility method is used to save an execptions encountered into
        /// an error log
        /// </summary>
        public void logError(Exception exp)
        {
            systemStatusPanel.BackgroundImage = red_circle;
            lblSystemStatus.Text = "Error!";

            updateMainStatus("Error!", processor.site_name, txtStatus, statusBar);
            statusBar.Items["error_status"].Text = "Error encountered!";
            if (!Directory.Exists("C:\\" + processor.site_name))
            {
                Directory.CreateDirectory("C:\\" + processor.site_name);
            }


            string site_dir = "C:\\" + processor.site_name + "\\" +
                System.DateTime.Now.Year + "-" +
                System.DateTime.Now.Month + "-" +
                System.DateTime.Now.Day;

            if (!Directory.Exists(site_dir))
            {
                Directory.CreateDirectory(site_dir);
            }

            string error_file_name = "error.log";

            StreamWriter w = File.AppendText(site_dir + "\\" + error_file_name);

            w.WriteLine(System.DateTime.Now.ToString() + '\t' + exp.Message + '\n' + exp.StackTrace);

            w.Close();
        }

        /// <summary>
        /// This method is used to clean out the error display at the bottom of the screen
        /// (on the status bar)
        /// </summary>
        public void cleanErrorDisplay()
        {
            statusBar.Items["error_status"].Text = "";
        }

        /// <summary>
        /// This method is called by the .NET framework everytime a user clicks
        /// on of the tabs in the main console. In this method, we take appropriate
        /// actions based on which tab the user has clicked and from which tab the user is coming
        /// from. 
        /// </summary>
        private void TabControl1_Selected(Object sender, TabControlEventArgs e)
        {
            if (e.TabPageIndex == current_tab_index)
            {
                // you don't need to do any more processing since you are staying in the same tab
                return;
            }
            if (processor.processing)
            {
                tabControl1.SelectedIndex = 1;
                current_tab_index = 1;
                return;
            }
            if (processor.scanning)
            {
                tabControl1.SelectedIndex = 0;
                current_tab_index =0;
                return;
            }

            if (e.TabPageIndex == 2 && enable_password)
            {
                PasswordInquiry password_inq = new PasswordInquiry();

                if (password_inq.ShowDialog() == DialogResult.OK)
                {
                    if (processor.current_password != password_inq.getPassword())
                    {
                        MessageBox.Show("That was an incorrect password!");
                        tabControl1.SelectedIndex = current_tab_index;
                        return;
                    }
                }
                else
                {
                    tabControl1.SelectedIndex = current_tab_index;
                    return;
                }
            }

            //  The user is trying to leave the control tab and we need to 
            //  verify that the passwords are correct
            if (current_tab_index == 2 )
            {
                if (txtOldPass.Text != processor.current_password &&
                    txtNewPass.Text != "")
                {
                    MessageBox.Show("Please specify the correct old password!");
                    tabControl1.SelectedIndex = 2;
                    txtOldPass.Text = "";
                    return;
                }
                else if ((txtNewPass.Text != txtConfirmNewPass.Text) &&
                    txtNewPass.Text != "")
                {
                    MessageBox.Show("The passwords you entered do not match!");
                    tabControl1.SelectedIndex = 2;
                    txtNewPass.Text = "";
                    txtConfirmNewPass.Text = "";
                    return;
                }
                else if(txtNewPass.Text!="")
                {
                    processor.current_password = txtNewPass.Text;
                    RegistryKey regKey = Registry.LocalMachine.CreateSubKey(@"Software\ArgosQuant");
                    if (regKey != null)
                    {
                        regKey.SetValue("current_password", processor.current_password);
                    }
                    txtNewPass.Text = "";
                    txtOldPass.Text = "";
                    txtConfirmNewPass.Text = "";
                }
            }

            processor.max_int_value = Int32.Parse(txtMaxIntValue.Text);
            processor.min_int_value = Int32.Parse(txtMinIntValue.Text);

            saveRegistryEntries();
            retrieveRegistryEntries();

            processor.tab_changing = true;
            int tab_page_index = e.TabPageIndex;

            if (tab_page_index == 0)
            {
                optCM_CheckedChanged();
            }
            else if (tab_page_index == 1)
            {
                optRep_CheckedChanged();
            }
            current_tab_index = e.TabPageIndex;

            checkComamndBasedProcess();
        }


        /// <summary>
        /// This method is called when the continuous monitoring option
        /// is slected
        /// </summary>
        private void optCM_CheckedChanged()
        {
            enableCM(true);
            enableReprocessing(false);

            if (connect_to_spectrometer(1))
            {
                spectrometer_attached();
            }
            else
            {
                spectrometer_disconnected();
            }
        }

        /// <summary>
        /// This method is called by the .NET framework everytime a user clicks on one of the concetration
        /// table cells of the reprocessing tab. In this method, we try to find out if a user has enabled treshold filtring for
        /// any of the gases
        /// </summary>
        private void rep_datagrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                rep_datagrid.EndEdit();
                if (rep_datagrid[e.ColumnIndex, e.RowIndex].Value != null &&
                    (bool)rep_datagrid[e.ColumnIndex, e.RowIndex].Value)
                {
                    //rep_datagrid[e.ColumnIndex + 1, e.RowIndex].ReadOnly = false;
                    rep_datagrid[e.ColumnIndex + 1, e.RowIndex].Style.BackColor = System.Drawing.Color.White;
                }
                else if (rep_datagrid[e.ColumnIndex, e.RowIndex].Value != null &&
                    !(bool)rep_datagrid[e.ColumnIndex, e.RowIndex].Value)
                {
                    //rep_datagrid[e.ColumnIndex + 1, e.RowIndex].ReadOnly = true;
                    rep_datagrid[e.ColumnIndex + 1, e.RowIndex].Style.BackColor = System.Drawing.Color.Gray;
                }
                else
                {
                    //rep_datagrid[e.ColumnIndex + 1, e.RowIndex].ReadOnly = true;
                    rep_datagrid[e.ColumnIndex + 1, e.RowIndex].Style.BackColor = System.Drawing.Color.Gray;
                }
            }

        }

        /// <summary>
        /// This method is called by the .NET framework everytime a user clicks on one of the concetration
        /// table cells of the continuous monitoring tab. In this method, we try to find out if a user has enabled treshold filtring for
        /// any of the gases
        /// </summary>
        private void cm_datagrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2 && e.RowIndex > -1 )
            {
                cm_datagrid.EndEdit();
                if (cm_datagrid[e.ColumnIndex, e.RowIndex].Value != null &&
                    (bool)cm_datagrid[e.ColumnIndex, e.RowIndex].Value)
                {
                    //cm_datagrid[e.ColumnIndex + 1, e.RowIndex].ReadOnly = false;                    
                    cm_datagrid[e.ColumnIndex + 1, e.RowIndex].Style.BackColor = System.Drawing.Color.White;
                }
                else if (cm_datagrid[e.ColumnIndex, e.RowIndex].Value != null &&
                    !(bool)cm_datagrid[e.ColumnIndex, e.RowIndex].Value)
                {
                    //cm_datagrid[e.ColumnIndex + 1, e.RowIndex].ReadOnly = true;
                    cm_datagrid[e.ColumnIndex + 1, e.RowIndex].Style.BackColor = System.Drawing.Color.Gray;
                }
                else
                {
                    //cm_datagrid[e.ColumnIndex + 1, e.RowIndex].ReadOnly = true;
                    cm_datagrid[e.ColumnIndex + 1, e.RowIndex].Style.BackColor = System.Drawing.Color.Gray;
                }
            }
        }

        /// <summary>
        /// This method is called by the .NET framework when a user clicks on the
        /// OO spectrometer slection button
        /// </summary>
        private void btnSpect_Click(object sender, EventArgs e)
        {
            try
            {
                processor.current_spectrometer = new Spectrometer(processor.integration_time, 0);
                
                SelectSpectrometer select_spectrometer = new SelectSpectrometer(ref processor.current_spectrometer);
                select_spectrometer.ShowDialog();

                int r = processor.current_spectrometer.TestForSpec(0);
                if (r == 0)
                {
                    processor.current_spectrometer.AttachToSpec();
                    processor.current_spectrometer.FillInWLArray();
                    saveRegistryEntries();
                    spectrometer_attached();
                }
                else
                {
                    spectrometer_disconnected();
                }
            }
            catch (Exception exp)
            {
                logError(exp);
                btnMonitor.Enabled = false;
            }
        }

        /// <summary>
        /// This method is called by the .NET framework when a user clicks
        /// on the settings button to setup user parameters such as averages and
        /// collection times
        /// </summary>
        private void btnSettings_Click_1(object sender, EventArgs e)
        {

            Settings settings = new Settings();

            settings.ShowDialog();

            RegistryKey regKey = Registry.LocalMachine.OpenSubKey(@"Software\ArgosQuant");
            if (regKey != null)
            {
                processor.collection_time = (int)regKey.GetValue("collection_time", 30);
                processor.box_car = (int)regKey.GetValue("box_car", 0);
                processor.current_spectrometer.box_car = (short)processor.box_car;
                processor.dynamic_integration = (int)regKey.GetValue("integration_update", 0);
                processor.dynamic_background = (int)regKey.GetValue("background_update", 1);
                processor.shift_amount = (int)regKey.GetValue("shift_amount", 0);
                processor.maximum_signal = Double.Parse((string)regKey.GetValue("maximum_signal", DefaultValues.MAXIMUM_SIGNAL));
                processor.minimum_signal = Double.Parse((string)regKey.GetValue("minimum_signal", DefaultValues.MINIMUM_SIGNAL));
                processor.autorun = Util.getBoolValue(regKey, "autorun", DefaultValues.AUTORUN);
                processor.flag_data = Util.getBoolValue(regKey, "flag_data", DefaultValues.FLAG_DATA);
            }
            else
            {
                processor.collection_time = 30;
            }

            processor.updateNumberOfAverages();

        }

        /// <summary>
        /// This method is called by the .NET framework when a user clicks on align button
        /// to display the align window. 
        /// </summary>
        private void btnAlign_Click(object sender, EventArgs e)
        {
            long m_handle = 0;
            AvaLightLed light = null;
            int success = -1;

            if (processor is OxygenProcessor && 
                processor.current_spectrometer is AvantesSpectrometer)
            {
                m_handle = ((AvantesSpectrometer)(processor.current_spectrometer)).m_DeviceHandle;
                light = new AvaLightLed();
                light.init(m_handle);
                success = light.turnLightOn();
                System.Threading.Thread.Sleep(30);//wait 30 milli seconds before collecting
            }

            Image image = Bitmap.FromFile(Application.StartupPath + "/argos_logo_small_running.jpg");
            btnAlign.BackgroundImage = image;

            align alignDlgBox = new align(ref processor.current_spectrometer);

            alignDlgBox.ShowDialog();

            image = Bitmap.FromFile(Application.StartupPath + "/argos_logo_small.jpg");
            btnAlign.BackgroundImage = image;

            processor.integration_time = processor.current_spectrometer.integration_time;
            alignDlgBox = null;
            saveRegistryEntries();

            if (processor.collection_time == 1)
                processor.averages = 1;
            else
                processor.averages = 
                    (int)(processor.collection_time / ((double)processor.integration_time / 1000));

            if (processor is OxygenProcessor &&
                processor.current_spectrometer is AvantesSpectrometer)
            {
                System.Threading.Thread.Sleep(30);//wait 30 milli seconds after collecting
                success = light.turnLightOff();
            }
        }

        /// <summary>
        /// This method is called by the .NET framework when a user clicks on the BNW spetrometer
        /// selection button
        /// </summary>
        private void button4_Click(object sender, EventArgs e)
        {
            processor.current_spectrometer = new BNWSpectrometer(processor.integration_time, 0);
            int r = processor.current_spectrometer.TestForSpec(1);
            if (r == 0)
            {
                processor.current_spectrometer.AttachToSpec();
                processor.current_spectrometer.FillInWLArray();
                saveRegistryEntries();
                btnMonitor.Enabled = true;
                if (processor.current_spectrometer.selected)
                {
                    lblSerialNumber.Text = processor.current_spectrometer.serial_number;
                    lblSpectStatus.Text = "Connected";
                    lblSpectType.Text = processor.current_spectrometer.adc_type;
                    if (processor.current_spectrometer.adc_type == BNWSpectrometer.SPECTROMETER_TYPE_BNWTEC)
                    {
                        lblBWSN.Text = processor.current_spectrometer.serial_number;
                        lblBWStatus.Text = "Connected";
                        lblBWSpectType.Text = processor.current_spectrometer.adc_type;
                    }
                }
            }
        }


        /// <summary>
        /// This method is called by the .NET framework when a user changes the library
        /// in the drop down menu of the library selection
        /// </summary>
        private void cmbLibrary_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (processor.finished_loading && 
                processor.library_mode!=LibraryMode.SELECTEDGASES)
            {
                processor.loaded_library_name = cmbLibrary.Text;
                if (processor.loaded_library_name != null)
                {
                    String current_library_data = 
                        Libraries.getLibrary(processor.loaded_library_name);
                    processor.lib_matrix = 
                        new LibraryMatrix(current_library_data);
                    processor.lib_matrix.library_name = processor.loaded_library_name;
                    processor.createTable(cm_datagrid,rep_datagrid);
                    lblLibraryName2.Text = processor.loaded_library_name;
                    cmbLibrary.Text = processor.loaded_library_name;

                    resizer();
                }
                processor.multiple_libraries = false;
            }
        }


        /// <summary>
        /// This method is a utility method used to update the signal strength 
        /// bar display and text display
        /// </summary>
        private void updateSignalStrengthDisplay()
        {
            if (processor.current_spectrometer != null)
            {
                double sig_str_percentile = (1 - ((4000 - processor.current_spectrometer.max_uv_count) / 4000)) * 100;
                lblSigStre.Text = sig_str_percentile.ToString("0.000") +"%";

                if (processor.current_spectrometer.max_uv_count > 3700)
                {
                    sigStrePanel.BackgroundImage = red_circle;
                }
                else if (processor.current_spectrometer.max_uv_count >= 3000)
                {
                    sigStrePanel.BackgroundImage = green_circle;
                }
                else if (processor.current_spectrometer.max_uv_count >= 2500)
                {
                    sigStrePanel.BackgroundImage = yellow_circle;
                }
                else
                {
                    sigStrePanel.BackgroundImage = red_circle;
                }
            }
            else
            {
                sigStrePanel.BackgroundImage = red_circle;
                lblSigStre.Text = "0%";
            }
        }

        /// <summary>
        /// This method is a utility method used to update the light source 
        /// indicators color and text
        /// </summary>
        private void updateLShDisplay()
        {
            if (processor.current_spectrometer != null && processor.current_spectrometer.selected)
            {
                double ls_percentile = (1000 - processor.current_spectrometer.integration_time) * 100 / 997;
                lblLsh.Text = ls_percentile.ToString("0.000")+ "%";
                if (processor.current_spectrometer.integration_time > 750)
                {
                    lsPanel.BackgroundImage = red_circle;
                }
                else if (processor.current_spectrometer.integration_time >= 250)
                {
                    lsPanel.BackgroundImage = yellow_circle;
                }
                else
                {
                    lsPanel.BackgroundImage = green_circle;
                }
            }
            else
            {
                lblLsh.Text = "0%";
                lsPanel.BackgroundImage = red_circle;
            }
        }

        


        /// <summary>
        /// This method gets called by the .NET framework when the gas selection button is
        /// clicked
        /// Note: This method has to be used only with PLSProcessor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectGases_Click(object sender, EventArgs e)
        {
            DialogResult dr = new DialogResult ();
            gas_selector gasselector = new gas_selector(processor.selected_gases);
            dr = gasselector.ShowDialog();

            if (DialogResult.Cancel == dr)
            {
                return;
            }

            //for each check box, whose name is in the gas_values string array
            //check whether it is checked or not, if it is, add it to the gas library array
            int current_lib_index = 0;
            
            ArrayList temp_selected_gases = new ArrayList();

            processor.selected_gases = new int[processor.libraries.Length][];
            for (int i = 0; i < gasselector.gas_values.Length; ++i)
            {
                CheckBox temp_check_box = (CheckBox)(gasselector.Controls.Find(gasselector.gas_values[i], true)[0]);
                if(temp_check_box.Checked)
                {
                    processor.multiple_libraries = true;
                    processor.library_mode = LibraryMode.SELECTEDGASES;
                    string[] lib_gas_index = gasselector.gas_values[i].Split(new char[] { '_' });
                    if (current_lib_index != int.Parse(lib_gas_index[0]))
                    {
                        if (temp_selected_gases.Count != 0)
                        {
                            int[] temp = new int[temp_selected_gases.Count];
                            temp_selected_gases.CopyTo(temp);
                            processor.selected_gases[current_lib_index] = temp;
                        }
                        current_lib_index = int.Parse(lib_gas_index[0]);
                        temp_selected_gases = new ArrayList();
                    }

                    temp_selected_gases.Add(int.Parse(lib_gas_index[1]));
     
                }
            }
            if (temp_selected_gases.Count != 0)
            {
                int[] temp = new int[temp_selected_gases.Count];
                temp_selected_gases.CopyTo(temp);
                processor.selected_gases[current_lib_index] = temp;
            }
            processor.createTable(cm_datagrid,rep_datagrid);
            gasselector.Dispose();
        }

        /// <summary>
        /// This method tries to find a spectrometer currently
        /// connected to the computer and tries to connect it.
        /// quiteMode -     1 (it will display a pop out message saying not connected in the case of an error)
        ///                 0 (it will not display any error messages)
        /// </summary>
        public bool connect_to_spectrometer(int quietMode)
        {
            try
            {
                processor.current_spectrometer = new Spectrometer(processor.integration_time, 0);
                int r = processor.current_spectrometer.TestForSpec(quietMode);
                if (r != 0)
                {
                    processor.current_spectrometer = new BNWSpectrometer(processor.integration_time, 0);
                    r = processor.current_spectrometer.TestForSpec(quietMode);
                }

                if (r != 0)
                {
                    processor.current_spectrometer = new AvantesSpectrometer(processor.integration_time, 0);
                    ((AvantesSpectrometer)processor.current_spectrometer).setWindowsHandle(this.Handle);
                    r = processor.current_spectrometer.TestForSpec(quietMode);
                    if (r == 0)
                    {
                        ((AvantesSpectrometer)processor.current_spectrometer).connectToSpectrometer();
                    }
                }

                if (r == 0)
                {
                    processor.current_spectrometer.AttachToSpec();
                    processor.current_spectrometer.FillInWLArray();

                    if (Util.isActivated() &&
                        processor.current_spectrometer.serial_number != processor.spect_serial_number)
                    {
                        RegistryKey regKey = Registry.LocalMachine.OpenSubKey(@"Software\ArgosQuant");
                        if (regKey == null)
                        {
                            regKey = Registry.LocalMachine.CreateSubKey(@"Software\ArgosQuant");
                        }
                        
                        string current_pass = (string)regKey.GetValue("soft_serial", 0);
                        if(!Util.isMasterSerial(current_pass))
                        {
                            MessageBox.Show("You have connected an unregistered spectrometer!");
                            registerToolStripMenuItem.Enabled = true;
                            return false;
                        }
                    }
                    else if (Util.isActivated())
                    {
                        registerToolStripMenuItem.Enabled = false;
                    }

                    saveRegistryEntries();

                    if (processor.current_spectrometer.selected)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception exp)
            {
                logError(exp);
                return false;
            }
        }

        /// <summary>
        /// This method is called by the .NET framework when a user selects the option
        /// to use the library in file instead of the hard coded ones.
        /// Note: This method is used only with PLSProcessor
        /// </summary>
        private void optUseLibraryOnFile_CheckedChanged(object sender, EventArgs e)
        {
            if (optUseLibraryOnFile.Checked)
            {
                if (processor.finished_loading)
                {
                    FileInfo main_lib_file = new FileInfo(Directory.GetCurrentDirectory() + "\\ArgosPlsLibrary.csv");

                    processor.lib_matrix = new LibraryMatrix(main_lib_file);
                    processor.lib_matrix.library_name = PLSProcessor.ON_FILE;

                    //lblLibraryName2.Text = "";
                    //cmbLibrary.Text = "";
                    processor.multiple_libraries = false;
                    processor.createTable(cm_datagrid,rep_datagrid);
                    resizer();
                    cmbLibrary.Enabled = false;
                    btnSelectGases.Enabled = false;
                    selectChemicalsToolStripMenuItem.Enabled = false;
                }
                processor.multiple_libraries = false;
                processor.library_mode = LibraryMode.ONFILE;
            }
            else
            {
                cmbLibrary.Enabled = true;
            }
        }

        /// <summary>
        /// This method gets called by the .NET framework when the select_gases button
        /// is clicked by the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void optSelectGases_CheckedChanged(object sender, EventArgs e)
        {
            selectChemicalsToolStripMenuItem.Enabled = true;
            btnSelectGases.Enabled = true;
            cmbLibrary.Enabled = false;
            processor.multiple_libraries = true;
            processor.library_mode = LibraryMode.SELECTEDGASES;
            processor.createTable(cm_datagrid, rep_datagrid);
        }

        /// <summary>
        /// This method gets called when 
        /// the user clicks on the radio button
        /// that allows the selection of predefined libraries
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void optPreDefinedLibrary_CheckedChanged(object sender, EventArgs e)
        {
            if (processor.finished_loading)
            {
                processor.loaded_library_name = cmbLibrary.Text;

                if (processor.loaded_library_name == null ||
                    processor.loaded_library_name == "")
                {
                   cmbLibrary.Text = (string)cmbLibrary.Items[0];
                   processor.loaded_library_name = cmbLibrary.Text;
                }

                btnSelectGases.Enabled = false;
                selectChemicalsToolStripMenuItem.Enabled = false;
                cmbLibrary.Enabled = true;
                processor.multiple_libraries = false;

                String current_library_data = Libraries.getLibrary(processor.loaded_library_name);
                processor.lib_matrix = new LibraryMatrix(current_library_data);
                processor.lib_matrix.library_name = processor.loaded_library_name;

                processor.createTable(cm_datagrid, rep_datagrid);
                lblLibraryName2.Text = processor.loaded_library_name;
                cmbLibrary.Text = processor.loaded_library_name;

                resizer();
                processor.multiple_libraries = false;
                processor.library_mode = LibraryMode.PREDEFINED;
            }
        }

       
        /// <summary>
        /// This method updates the gas selector states
        /// </summary>
        public void updateGasSelector()
        {
            switch(processor.library_mode)
            {
                case LibraryMode.ONFILE:
                    optUseLibraryOnFile.Checked = true;
                    optSelectGases.Checked = false;
                    optPreDefinedLibrary.Checked = false;
                    btnSelectGases.Enabled = false;
                    selectChemicalsToolStripMenuItem.Enabled = false;
                    cmbLibrary.Enabled = false;
                    break;
                case LibraryMode.PREDEFINED:
                    optUseLibraryOnFile.Checked = false;
                    optSelectGases.Checked = false;
                    optPreDefinedLibrary.Checked = true;
                    btnSelectGases.Enabled = false;
                    selectChemicalsToolStripMenuItem.Enabled = false;
                    cmbLibrary.Enabled = true;
                    break;
                case LibraryMode.SELECTEDGASES:
                    optUseLibraryOnFile.Checked = false;
                    optSelectGases.Checked = true;
                    optPreDefinedLibrary.Checked = false;
                    btnSelectGases.Enabled = true;
                    selectChemicalsToolStripMenuItem.Enabled = true;
                    cmbLibrary.Enabled = false;
                    break;
                default:
                    optUseLibraryOnFile.Checked = false;
                    optSelectGases.Checked = false;
                    optPreDefinedLibrary.Checked = true;
                    btnSelectGases.Enabled = false;
                    selectChemicalsToolStripMenuItem.Enabled = false;
                    cmbLibrary.Enabled = true;
                    break;
            }
        }
        /// <summary>
        /// This gets called to load the gas tweaker gui
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLibraryTweaker_Click(object sender, EventArgs e)
        {
            LibraryTweaker librarytweaker = new LibraryTweaker(processor.libraries);
            librarytweaker.ShowDialog();
            ((PLSProcessor)((AProcessor)processor)).setCurrentConcentrations(librarytweaker.temp_concetrations);
            ((PLSProcessor)((AProcessor)processor)).setPixelShifts(librarytweaker.temp_pixel_shifts);
            librarytweaker.Dispose();
        }

        /// <summary>
        /// This allows to force collect data and use it as background
        /// There are no quantifications used here.
        /// </summary>
        private void forceUpdateBackground()
        {
            processor.adjustIntegrationTime(txtStatus, statusBar);
            if (!processor.scanning)
            {
                return;
            }
            processor.scanning = true;
            double[,] values = processor.scanData(statusBar, txtStatus);
            if (!processor.scanning)
            {
                return;
            }
            processor.incrementFileCount(statusBar);
            updateSignalStrengthDisplay();
            updateLShDisplay();

            processor.compileData(values, txtStatus, statusBar);
            processor.saveSingleBeam(txtStatus, statusBar);
            processor.displayGraph(processor.current_data.getPointPairList(), cm_graph, rep_graph, txtStatus, statusBar);

            if (processor.current_spectrometer.max_uv_count < processor.minimum_signal ||
                processor.current_spectrometer.max_uv_count > processor.maximum_signal)
            {
                MessageBox.Show(
                    "Please make sure you have aligned your system properly.\n" +
                    "Currently your signal intensity is: "
                    + processor.current_spectrometer.max_uv_count);
                processor.scanning = false;
            }
            else
            {
                processor.updateBackground(processor.current_data, cm_datagrid, rep_datagrid, true);
                processor.adjustIntegrationTime(txtStatus, statusBar);
                processor.scanning = false;
                int recorded_file_number = processor.file_number;
                MessageBox.Show(
                    "Done updating your background with file number: "
                    + recorded_file_number);
            }

        }

        /// <summary>
        /// This method is called when the collect background button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCollectBkgd_Click(object sender, EventArgs e)
        {

            Image image = Bitmap.FromFile(Application.StartupPath + "/argos_logo_small_running.jpg");
            btnCollectBkgd.BackgroundImage = image;

            if (!processor.scanning)
            {
                updateMainStatus("Collecting background manually...", processor.site_name, txtStatus, statusBar);
                processor.scanning = true;
                processor.current_spectrometer.scanning = true;
                forceUpdateBackground();

                image = Bitmap.FromFile(Application.StartupPath + "/argos_logo_small.jpg");
                btnCollectBkgd.BackgroundImage = image;
            }
            else
            {
                updateMainStatus("Stopping background collection...", processor.site_name, txtStatus, statusBar);
                processor.scanning = false;
                processor.current_spectrometer.scanning = false;
                image = Bitmap.FromFile(Application.StartupPath + "/argos_logo_small.jpg");
                btnCollectBkgd.BackgroundImage = image;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            data_summary_file_dlg.FileName = "";
            data_summary_file_dlg.Filter = "Data Summary Files|*.csv";
            DialogResult dr = data_summary_file_dlg.ShowDialog();
            
            if (dr == DialogResult.OK)
            {
                if (data_summary_file_dlg.FileName != null &&
                    data_summary_file_dlg.FileName != "")
                {
                    lblDataSummary.Text = data_summary_file_dlg.FileName;
                    processor.ds_file = data_summary_file_dlg.FileName;

                    RegistryKey regKey = Registry.LocalMachine.CreateSubKey(@"Software\ArgosQuant");
                    if (regKey != null)
                    {
                        regKey.SetValue("data_summary", processor.ds_file);
                    }
                    current_ds = new DataSummary(processor.ds_file);

                    cmbGases.DataSource = current_ds.getGasNamesForCmbBox();
                }
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            if (cmbGases.Text != null &&
                "" != cmbGases.Text.Trim())
            {
                double sig_str_treshold = -1;
                double conc_treshold = -1;

                if (Util.isNumeric(txtConcTreshold.Text.Trim()))
                {
                    conc_treshold = double.Parse(txtConcTreshold.Text.Trim());
                }

                if (Util.isNumeric(txtSigStrTreshold.Text.Trim()))
                {
                    sig_str_treshold = double.Parse(txtSigStrTreshold.Text.Trim());
                }


                ZedGraph.PointPairList list = 
                    current_ds.getPointPairList(
                    cmbGases.Text.Trim(), 
                    sig_str_treshold,
                    conc_treshold);

                gas_graph.GraphPane.CurveList.Clear();// = "Argos Scientific Inc.";
                ZedGraph.GraphPane graph_pane = gas_graph.GraphPane;

                LineItem myCurve = graph_pane.AddCurve(cmbGases.Text.Trim(), list, Color.Red,
                                               ZedGraph.SymbolType.None);
                myCurve.Line.Width = 1.5F;

                graph_pane.XAxis.Type = AxisType.Date;

                gas_graph.AxisChange();
                gas_graph.Refresh();
            }
        }

        // Call this method from the Form_Load method, passing your ZedGraphControl
        public void CreateChart(ZedGraphControl zgc)
        {
            GraphPane myPane = zgc.GraphPane;

            // Set the titles and axis labels
            myPane.Title.Text = "My Test Date Graph";
            myPane.XAxis.Title.Text = "Date";
            myPane.YAxis.Title.Text = "My Y Axis";

            // Make up some data points based on the Sine function
            PointPairList list = new PointPairList();
            for (int i = 0; i < 36; i++)
            {
                double x = (double)new XDate(2008,11,1,2,i,i);
                double y = Math.Sin((double)i * Math.PI / 15.0);
                list.Add(x, y);
            }
            double s = (double)new XDate(2008, 11, 25, 5, 5,5);
            double d = 10;
            list.Add(s, d);
            // Generate a red curve with diamond
            // symbols, and "My Curve" in the legend
            LineItem myCurve = myPane.AddCurve("My Curve",
               list, Color.Red, SymbolType.Diamond);

            // Set the XAxis to date type
            myPane.XAxis.Type = AxisType.Date;

            // Calculate the Axis Scale Ranges
            zgc.AxisChange();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DeleteMe delete_me = new DeleteMe();

            delete_me.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                processor.current_spectrometer = new AvantesSpectrometer(processor.integration_time, 0);
                ((AvantesSpectrometer)processor.current_spectrometer).setWindowsHandle(this.Handle);

                SelectSpectrometer select_spectrometer = new SelectSpectrometer(ref processor.current_spectrometer);
                select_spectrometer.ShowDialog();

                int r = processor.current_spectrometer.TestForSpec(0);
                if (r == 0)
                {
                   ((AvantesSpectrometer)processor.current_spectrometer).connectToSpectrometer();
                    processor.current_spectrometer.AttachToSpec();
                    processor.current_spectrometer.FillInWLArray();
                    saveRegistryEntries();
                    spectrometer_attached();
                }
                else
                {
                    spectrometer_disconnected();
                }
            }
            catch (Exception exp)
            {
                logError(exp);
                btnMonitor.Enabled = false;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            OxyParameters oxy_parameters = new OxyParameters();

            DialogResult rslt = oxy_parameters.ShowDialog();

            if (rslt == DialogResult.OK)
            {
                RegistryKey regKey = Registry.LocalMachine.OpenSubKey(@"Software\ArgosQuant");
                if (regKey != null)
                {
                    processor.start_integral_pixel = (int)regKey.GetValue("start_integral_pixel", 0);
                    processor.end_integral_pixel = (int)regKey.GetValue("end_integral_pixel", 0);
                    processor.OXLowRef = Double.Parse((string)regKey.GetValue("OXLowRef", "0"));
                    processor.OXHighRef = Double.Parse((string)regKey.GetValue("OXHighRef", "0"));
                    processor.calibration_interval = (int)regKey.GetValue("calibration_interval", 240);
                    processor.calibration_duration = (int)regKey.GetValue("calibration_duration", 5);
                    processor.calibration_enabled = Util.getBoolValue(regKey, "calibration_enabled", false);
                    processor.data_interval = (int)regKey.GetValue("data_interval", 0);
                    processor.command_timer = (int)regKey.GetValue("command_timer", 1000);
                    processor.command_processing = Util.getBoolValue(regKey, "command_processing", false);
                    processor.command_file = (string)regKey.GetValue("command_file", "");

                    if (!processor.command_processing)
                    {
                        command_timer.Enabled = false;
                    }

                    timer_high_ox.Interval = processor.calibration_duration * 60 * 1000;
                    timer_low_ox.Interval = processor.calibration_interval * 60 * 1000;
                }
            }
        }

        /// <summary>
        /// This timer is fired at the begining of a calibration session and 
        /// will notiffy the Oxygen processor that the next acquisision should be
        /// counted as a low oxygen data set
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_low_ox_Tick(object sender, EventArgs e)
        {
            updateMainStatus("Setting up low oxygen acquisition...", processor.site_name, txtStatus, statusBar);
            processor.current_mode = OxygenProcessorMode.LOW_OXYGEN;
            timer_high_ox.Start();
        }

        /// <summary>
        /// This timer is fired at the end of the calibration session and will
        /// notify the oxyger processor that the next acquisision should be 
        /// counted as a high oxygen data set. Since we do not collect unless high
        /// oxygen data set unless the low oxygen is collected, the timer is turned off
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_high_ox_Tick(object sender, EventArgs e)
        {
            updateMainStatus("Setting up high oxygen acquisition...", processor.site_name, txtStatus, statusBar);
            processor.current_mode = OxygenProcessorMode.HIGH_OXYGEN;
            timer_high_ox.Stop();
        }


        /// <summary>
        /// collects Normal O2
        /// </summary>
        public void collectNormalO2()
        {
            if (processor.scanning)
            {
                processor.scanning = false;
                btnMonitor.Enabled = true;
                enableAlignButton();
                btnCollectHighO2.Enabled = true;
                btnCollectLowO2.Text = "Collect O2 Data";
                processor.current_mode = OxygenProcessorMode.NORMAL;

            }
            else
            {
                btnMonitor.Enabled = false;
                btnAlign.Enabled = false;
                btnCollectHighO2.Enabled = false;
                btnCollectLowO2.Text = "STOP";
                updateMainStatus("Setting oxygen acquisition...", processor.site_name, txtStatus, statusBar);
                processor.current_mode = OxygenProcessorMode.NORMAL;
                processor.scanning = true;
                double[,] values = processor.scanData(statusBar, txtStatus);
                processor.incrementFileCount(statusBar);
                updateSignalStrengthDisplay();
                updateLShDisplay();
                processor.compileData(values, txtStatus, statusBar);
                processor.saveSingleBeam(txtStatus, statusBar);
                processor.performQuantifications(values, cm_datagrid, rep_datagrid, txtStatus, statusBar);
                processor.displayGraph(processor.current_data.getPointPairList(), cm_graph, rep_graph, txtStatus, statusBar);
                processor.scanning = false;
                btnCollectLowO2.Text = "Collect Low O2";
                btnMonitor.Enabled = true;
                enableAlignButton();
                btnCollectHighO2.Enabled = true;
            }
        }

        /// <summary>
        /// This method is used to collect a low oxygen data for calibration purposes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCollectLowO2_Click(object sender, EventArgs e)
        {
            Image image = Bitmap.FromFile(Application.StartupPath + "/argos_logo_small_running.jpg");
            btnAlign.BackgroundImage = image;
            collectLowO2();
            image = Bitmap.FromFile(Application.StartupPath + "/argos_logo_small.jpg");
            btnAlign.BackgroundImage = image;
        }


        /// <summary>
        /// Collects Low O2 setting
        /// </summary>
        public void collectLowO2()
        {
            if (processor.scanning)
            {
                processor.scanning = false;
                btnMonitor.Enabled = true;
                enableAlignButton();
                btnCollectHighO2.Enabled = true;
                btnCollectLowO2.Text = "Collect Low O2";
                processor.current_mode = OxygenProcessorMode.NORMAL;

            }
            else
            {
                btnMonitor.Enabled = false;
                btnAlign.Enabled = false;
                btnCollectHighO2.Enabled = false;
                btnCollectLowO2.Text = "STOP";
                updateMainStatus("Setting up low oxygen acquisition...", processor.site_name, txtStatus, statusBar);
                processor.current_mode = OxygenProcessorMode.LOW_OXYGEN;
                processor.scanning = true;
                double[,] values = processor.scanData(statusBar, txtStatus);
                processor.incrementFileCount(statusBar);
                updateSignalStrengthDisplay();
                updateLShDisplay();
                processor.compileData(values, txtStatus, statusBar);
                processor.saveSingleBeam(txtStatus, statusBar);
                processor.performQuantifications(values, cm_datagrid, rep_datagrid, txtStatus, statusBar);
                processor.displayGraph(processor.current_data.getPointPairList(), cm_graph, rep_graph, txtStatus, statusBar);
                processor.scanning = false;
                btnCollectLowO2.Text = "Collect Low O2";
                btnMonitor.Enabled = true;
                enableAlignButton();
                btnCollectHighO2.Enabled = true;
            }
        }

        /// <summary>
        /// This method is used to collect a high oxygen data for calibration purposes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCollectHighO2_Click(object sender, EventArgs e)
        {
            Image image = Bitmap.FromFile(Application.StartupPath + "/argos_logo_small_running.jpg");
            btnAlign.BackgroundImage = image;
            collectHighO2();
            image = Bitmap.FromFile(Application.StartupPath + "/argos_logo_small.jpg");
            btnAlign.BackgroundImage = image;
        }

        /// <summary>
        /// collects HIgh O2 settings
        /// </summary>
        public void collectHighO2()
        {
            if (processor.scanning)
            {
                processor.scanning = false;
                btnMonitor.Enabled = true;
                enableAlignButton();
                btnCollectLowO2.Enabled = true;
                btnCollectHighO2.Text = "Collect High O2";
            }
            else
            {
                if (processor.current_mode == OxygenProcessorMode.INTERMIDIATE)
                {
                    btnMonitor.Enabled = false;
                    btnAlign.Enabled = false;
                    btnCollectLowO2.Enabled = false;
                    btnCollectHighO2.Text = "STOP";
                    updateMainStatus("Setting up high oxygen acquisition...", processor.site_name, txtStatus, statusBar);
                    processor.current_mode = OxygenProcessorMode.HIGH_OXYGEN;
                    processor.scanning = true;
                    double[,] values = processor.scanData(statusBar, txtStatus);
                    processor.incrementFileCount(statusBar);
                    updateSignalStrengthDisplay();
                    updateLShDisplay();
                    processor.compileData(values, txtStatus, statusBar);
                    processor.saveSingleBeam(txtStatus, statusBar);
                    processor.performQuantifications(values, cm_datagrid, rep_datagrid, txtStatus, statusBar);
                    processor.displayGraph(processor.current_data.getPointPairList(), cm_graph, rep_graph, txtStatus, statusBar);
                    processor.scanning = false;
                    btnCollectHighO2.Text = "Collect High O2";
                    btnMonitor.Enabled = true;
                    enableAlignButton();
                    btnCollectLowO2.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Please collect Low Oxygen data first");
                }
            }
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            if (!light_on)
            {
                long m_handle = ((AvantesSpectrometer)processor.current_spectrometer).m_DeviceHandle;
                AvaLightLed light = new AvaLightLed();
                light.init(m_handle);
                int success = light.turnLightOn();
                light_on = true;
            }
            else
            {
                long m_handle = ((AvantesSpectrometer)processor.current_spectrometer).m_DeviceHandle;
                AvaLightLed light = new AvaLightLed();
                light.init(m_handle);
                int success = light.turnLightOff();
                light_on = false;
            }
        }


        /// <summary>
        /// We use this to paint an alternating shade to our table view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void statusTbl_CellPaint(Object sender, TableLayoutCellPaintEventArgs e)
        {
            if (e.Row % 2 == 0 && e.Column != 8)
            {
                e.Graphics.FillRectangle(Brushes.LightGray, e.CellBounds);
            }
            else if (e.Column != 8)
            {
                e.Graphics.FillRectangle(Brushes.White, e.CellBounds);
            }
            else if (e.Column == 8)
            {
                e.Graphics.FillRectangle(Brushes.LightGray, e.CellBounds);
            }
            else
            {
                table_painted = false;
            }
        }

        /// <summary>
        /// this method ensures that the user cannot access anything if the installation has
        /// expired
        /// </summary>
        private void installationHasExpired()
        {
            //simply disable everything except the registeration menu
            editToolStripMenuItem.Enabled = false;
            tabControl1.Enabled = false;
        }

        /// <summary>
        /// this method enables everything once the software has been activated
        /// </summary>
        private void installationHasBeenActivated()
        {
            this.Text = "UV Quant Suite";
            if (processor.current_spectrometer != null &&
                processor.current_spectrometer.serial_number != null &&
                processor.current_spectrometer.serial_number == processor.spect_serial_number)
            {
                registerToolStripMenuItem.Enabled = false;
            }
            editToolStripMenuItem.Enabled = true;
            tabControl1.Enabled = true;
        }

        private void registerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string current_spect_serial = null;
            current_spect_serial = processor.current_spectrometer.serial_number;
            
            Register register = new Register(current_spect_serial);
            register.ShowDialog();

            if (Util.isActivated())
            {
                installationHasBeenActivated();
            }
        }

        private void selectChemicalsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = new DialogResult();
            gas_selector gasselector = new gas_selector(processor.selected_gases);
            dr = gasselector.ShowDialog();

            if (DialogResult.Cancel == dr)
            {
                return;
            }

            //for each check box, whose name is in the gas_values string array
            //check whether it is checked or not, if it is, add it to the gas library array
            int current_lib_index = 0;

            ArrayList temp_selected_gases = new ArrayList();

            processor.selected_gases = new int[processor.libraries.Length][];
            for (int i = 0; i < gasselector.gas_values.Length; ++i)
            {
                CheckBox temp_check_box = (CheckBox)(gasselector.Controls.Find(gasselector.gas_values[i], true)[0]);
                if (temp_check_box.Checked)
                {
                    processor.multiple_libraries = true;
                    processor.library_mode = LibraryMode.SELECTEDGASES;
                    string[] lib_gas_index = gasselector.gas_values[i].Split(new char[] { '_' });
                    if (current_lib_index != int.Parse(lib_gas_index[0]))
                    {
                        if (temp_selected_gases.Count != 0)
                        {
                            int[] temp = new int[temp_selected_gases.Count];
                            temp_selected_gases.CopyTo(temp);
                            processor.selected_gases[current_lib_index] = temp;
                        }
                        current_lib_index = int.Parse(lib_gas_index[0]);
                        temp_selected_gases = new ArrayList();
                    }

                    temp_selected_gases.Add(int.Parse(lib_gas_index[1]));

                }
            }
            if (temp_selected_gases.Count != 0)
            {
                int[] temp = new int[temp_selected_gases.Count];
                temp_selected_gases.CopyTo(temp);
                processor.selected_gases[current_lib_index] = temp;
            }
            processor.createTable(cm_datagrid, rep_datagrid);
            gasselector.Dispose();
        }

        private void button8_Click_2(object sender, EventArgs e)
        {
            string my_serial = processor.current_spectrometer.serial_number;
            string my_soft_serial = Util.getPasswordFromSerial(my_serial);
            MessageBox.Show(my_soft_serial);
        }

        private void aboutUVQuantToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = new DialogResult();
            AboutBox1 about_me = new AboutBox1();
            dr = about_me.ShowDialog();
        }


        /// <summary>
        /// This method checks if this is a command based process and
        /// takes appropriate actions
        /// </summary>
        /// <returns></returns>
        public void checkComamndBasedProcess()
        {
            if (processor is OxygenProcessor)
            {

                if (processor.command_processing) //command processing is enabled - this works only for oxygen processor for now
                {
                    btnCollectHighO2.Visible = false;
                    btnCollectLowO2.Visible = false;
                    btnMonitor.Visible = false;
                    command_timer.Interval = processor.command_timer;
                    command_timer.Enabled = true;
                }
                else
                {
                    btnCollectHighO2.Visible = true;
                    btnCollectLowO2.Visible = true;
                    btnMonitor.Visible = true;
                    command_timer.Interval = processor.command_timer;
                    command_timer.Enabled = false;
                }
            }
        }

        /// <summary>
        /// Based on the interval set, this method gets called to check if any caommdn has been passed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void command_timer_Tick(object sender, EventArgs e)
        {
            try
            {
                Command current_command = new Command(processor.command_file);
                if (current_command.CurrentCommand != Command.Commands.SIT_IDLE &&
                    current_command.CurrentCommand != Command.Commands.ERROR)
                {
                    command_timer.Enabled = false;
                    processCommand(current_command);
                    command_timer.Enabled = true;
                }

                current_command.processedCommand(Command.Commands.SIT_IDLE);
            }
            catch (Exception ex)
            {
                logError(ex);
            }
        }

        private void processCommand(Command command)
        {
            if (command.CurrentCommand == Command.Commands.COLLECT_DATA)
            {
                collectNormalO2();
            }
            else if (command.CurrentCommand == Command.Commands.COLLECT_HIGH_O2)
            {
                collectHighO2();
            }
            else if (command.CurrentCommand == Command.Commands.COLLECT_LOW_O2)
            {
                collectLowO2();
            }
        }

        /// <summary>
        /// Opens the site information form to collect site information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void siteInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SiteInformation site_info = new SiteInformation();

            DialogResult diag_result = site_info.ShowDialog();

            if (diag_result == DialogResult.OK)
            {
                processor.site_name = Util.getStringInRegistry(Constants.UVQuantRegistryKeys.SITE_NAME, "");
                processor.uvs_operator = Util.getStringInRegistry(Constants.UVQuantRegistryKeys.USER_NAME, "");
                processor.path_length = Util.getDoubleInRegistry(Constants.UVQuantRegistryKeys.PATH_LENGTH, 0);

                txtSiteName.Text = processor.site_name;
                lblSiteName.Text = processor.site_name;

                txtPathLength.Text = processor.path_length.ToString();
                lblPathLength.Text = processor.path_length.ToString();

                txtOperator.Text = processor.uvs_operator;
                lblOperator.Text = processor.uvs_operator;

                //we need to update the table because the path length affects
                //the detection limit
                processor.createTable(cm_datagrid, rep_datagrid);
            }
        }

        /// <summary>
        /// Open path hardware selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void optOpenPath_CheckedChanged(object sender, EventArgs e)
        {
            if (optOpenPath.Checked)
            {
                processor.hardware_type = Constants.HardWareType.OPEN_PATH;
                Util.saveValueInRegistry(
                    Constants.HardWareType.OPEN_PATH,
                    Constants.UVQuantRegistryKeys.HARDWARE_TYPE);
                lblHardware.Text = Constants.HardWareType.OPEN_PATH_DISPLAY;
            }

        }

        /// <summary>
        /// Hound hardware selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void optHound_CheckedChanged(object sender, EventArgs e)
        {
            if (optHound.Checked)
            {
                processor.hardware_type = Constants.HardWareType.HOUND;
                Util.saveValueInRegistry(
                    Constants.HardWareType.HOUND,
                    Constants.UVQuantRegistryKeys.HARDWARE_TYPE);
                lblHardware.Text = Constants.HardWareType.HOUND_DISPLAY;
                Util.saveValueInRegistry(Double.Parse("17"), Constants.UVQuantRegistryKeys.PATH_LENGTH);
                processor.path_length = Util.getDoubleInRegistry(Constants.UVQuantRegistryKeys.PATH_LENGTH, 0);
            }
        }

        /// <summary>
        /// light stick hardware selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void optLightStick_CheckedChanged(object sender, EventArgs e)
        {
            if (optLightStick.Checked)
            {
                processor.hardware_type = Constants.HardWareType.LIGHT_STICK;
                Util.saveValueInRegistry(
                    Constants.HardWareType.LIGHT_STICK,
                    Constants.UVQuantRegistryKeys.HARDWARE_TYPE);
                lblHardware.Text = Constants.HardWareType.LIGHT_STICK_DISPLAY;
            }
        }

        /// <summary>
        /// extractive hardware selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void optExtractive_CheckedChanged(object sender, EventArgs e)
        {
            if (optExtractive.Checked)
            {
                processor.hardware_type = Constants.HardWareType.EXTRACTIVE;
                Util.saveValueInRegistry(
                    Constants.HardWareType.EXTRACTIVE,
                    Constants.UVQuantRegistryKeys.HARDWARE_TYPE);
                lblHardware.Text = Constants.HardWareType.EXTRACTIVE_DISPLAY;
            }
        }

        /// <summary>
        /// Based on the hardware type retrieved, this method selects the
        /// appropriate radio buttons and intantiates the lables
        /// </summary>
        private void selectAppropriateHardware()
        {
            int type = processor.hardware_type;

            if (type == Constants.HardWareType.EXTRACTIVE)
            {
                optExtractive.Checked = true;
                optHound.Checked = false;
                optOpenPath.Checked = false;
                optLightStick.Checked = false;
                lblHardware.Text = Constants.HardWareType.EXTRACTIVE_DISPLAY;
            }
            else if (type == Constants.HardWareType.HOUND)
            {
                optExtractive.Checked = false;
                optHound.Checked = true;
                optOpenPath.Checked = false;
                optLightStick.Checked = false;
                lblHardware.Text = Constants.HardWareType.HOUND_DISPLAY;
            }
            else if (type == Constants.HardWareType.LIGHT_STICK)
            {
                optExtractive.Checked = false;
                optHound.Checked = false;
                optOpenPath.Checked = false;
                optLightStick.Checked = true;
                lblHardware.Text = Constants.HardWareType.LIGHT_STICK_DISPLAY;
            }
            else if (type == Constants.HardWareType.OPEN_PATH)
            {
                optExtractive.Checked = false;
                optHound.Checked = false;
                optOpenPath.Checked = true;
                optLightStick.Checked = false;
                lblHardware.Text = Constants.HardWareType.OPEN_PATH_DISPLAY;
            }

        }

        /// <summary>
        /// enableds the align button if the hardware type being used 
        /// is an open path, if not, it will disable it
        /// </summary>
        private void enableAlignButton()
        {
            if (processor.hardware_type == Constants.HardWareType.OPEN_PATH)
            {
                btnAlign.Enabled = true;
            }
            else
            {
                btnAlign.Enabled = false;
            }
        }

        /// <summary>
        /// This button fires up the treshold setup GUI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTresholds_Click(object sender, EventArgs e)
        {
            GasTreshold gas_treshold = new GasTreshold(processor.libraries);
            DialogResult rslt = gas_treshold.ShowDialog();

            if (rslt == DialogResult.OK)
            {
                processor.createTable(cm_datagrid, rep_datagrid);
            }
        }

        private void uVQuantHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = "ug_uvquant.chm";
            p.Start();
        }

        /// <summary>
        /// This is called when the user clicks on the Sample Time menu item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sampleTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {

            SampleTime sample_time = new SampleTime();

            sample_time.ShowDialog();

            RegistryKey regKey = Registry.LocalMachine.OpenSubKey(@"Software\ArgosQuant");
            if (regKey != null)
            {
                processor.collection_time = (int)regKey.GetValue("collection_time", 30);
            }
            else
            {
                processor.collection_time = 30;
            }

            processor.updateNumberOfAverages();
        }

        private void button8_Click_3(object sender, EventArgs e)
        {
            RegistryKey regKey = Registry.LocalMachine.OpenSubKey(@"Software\ArgosQuant");
            RegistryKey regKey2 = Registry.LocalMachine.CreateSubKey(@"Software\ArgosQuant");
            string string_selected_gases = null;
            int collection_time = 30;

            if (regKey != null)
            {                
                string_selected_gases = (string)regKey.GetValue(Constants.UVQuantRegistryKeys.CALIBRATION_GASES, "");
            }

            if (string_selected_gases == "")
            {
                MessageBox.Show("Please make sure that you have selected at least one gas before running the calibration module.");
                return;
            }
            else
            {
                Image image = Bitmap.FromFile(Application.StartupPath + "/argos_logo_small_running.jpg");
                btnCalibrate.BackgroundImage = image;
                
                collection_time = (int)regKey.GetValue("collection_time", 30);
                
                regKey2.SetValue("collection_time", 30);

                SmartQA smart_qa = new SmartQA(
                                        (PLSProcessor)((AProcessor)processor),
                                        statusBar,
                                        txtStatus,
                                        cm_datagrid,
                                        rep_datagrid);

                smart_qa.ShowDialog();


                regKey2.SetValue("collection_time", collection_time);
                processor.collection_time = collection_time;
                image = Bitmap.FromFile(Application.StartupPath + "/argos_logo_small.jpg");
                btnCalibrate.BackgroundImage = image;
            }
        }

        /// <summary>
        /// This method fires up the calibration gases selection GUI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCalibGases_Click(object sender, EventArgs e)
        {
            CalibrationGases calib_gases = new CalibrationGases(processor.libraries);
            DialogResult rslt = calib_gases.ShowDialog();
        }

        private void bkgdSelect_Click(object sender, EventArgs e)
        {            
            OpenFileDialog selectBkgdFile = new OpenFileDialog();
            selectBkgdFile.Filter = "CSV Files|*.csv";
            if (selectBkgdFile.ShowDialog() == DialogResult.OK)
            {
                //            processor.bkgd_file = selectBkgdFile.FileName;
                processor.background_data = new DataMatrix(new FileInfo(selectBkgdFile.FileName));

                if (!processor.multiple_libraries)
                {
                    processor.start_wl = processor.lib_matrix.startWL;
                    processor.end_wl = processor.lib_matrix.endWL;
                    processor.num_entries = processor.lib_matrix.numOfEntries;
                    DataMatrix splined_data = Util.spline(processor.background_data, processor.start_wl, processor.end_wl, (short)processor.num_entries, 0);
                    processor.background_data = splined_data;
                }
                else
                {
                    for (int i = 0; i < processor.libraries.Length; ++i)
                    {
                        processor.start_wl = processor.libraries[i].startWL;
                        processor.end_wl = processor.libraries[i].endWL;
                        processor.num_entries = processor.libraries[i].numOfEntries;
                        DataMatrix splined_data = Util.spline(processor.background_data, processor.start_wl, processor.end_wl, (short)processor.num_entries, 0);
                        processor.backgrounds[i] = splined_data;
                    }
                }
            }
            selectBkgdFile.Dispose();         
            return;            
        }

        private void setGasLimitsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GasTreshold gas_treshold = new GasTreshold(processor.libraries);
            DialogResult rslt = gas_treshold.ShowDialog();

            if (rslt == DialogResult.OK)
            {
                processor.createTable(cm_datagrid, rep_datagrid);
            }
        }

        private void btnMet_Click(object sender, EventArgs e)
        {

            //Application.Run(new frmTerminal());
            frmTerminal met_system = new frmTerminal();
            DialogResult rslt = met_system.ShowDialog();

//            if (rslt == DialogResult.OK)
//            {                
                
//            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            wizard newWizard = new wizard();
            newWizard.ShowDialog();
        }

        /// <summary>
        /// This method performs the calibration on all the gasses in the
        /// current library that is selected:
        /// 1.  Inputs the path length being used by the user for the calibration
        ///     purposes and set the collection time to 30 seconds.
        /// 2.  Performs 16 back to back scans with each previous scan serving as
        ///     the background for the next one
        /// 3.  3*SDE = Minimum Detection Limit (MDL)
        /// 4.  9*SDE = LOQ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAutoCalibrate_Click(object sender, EventArgs e)
        {
            
            if (processor.scanning)
            {
                btnAutoCalibrate.Enabled = false;
                processor.scanning = false;
                calb_aborted = true;
            }
            else
            {
                // 1. indicate calibration has started
                // 2. Save current bkgd
                // 3. Set collection time to 30 sec (save the current one first)
                //Get the calibration process path length
                double path_length = -1;
                PathLengthForCalibration path_length_inq = new PathLengthForCalibration();

                if (path_length_inq.ShowDialog() == DialogResult.OK)
                {
                    path_length = Double.Parse(path_length_inq.getPathLength());
                }

                calb_aborted = false;
                updateMainStatus("Calibrating current library...", processor.site_name, txtStatus, statusBar);
                DataMatrix original_bkgd = processor.background_data;
                DataMatrix[] original_bkgds = processor.backgrounds;
                int original_collection_time = processor.collection_time;
                double original_path_length = processor.path_length;
                processor.path_length = path_length;

                processor.collection_time = 30;
                processor.updateNumberOfAverages();
                try
                {
                    Image image = Bitmap.FromFile(Application.StartupPath + "/argos_logo_small_running.jpg");
                    btnAutoCalibrate.BackgroundImage = image;
                    btnAutoCalibrate.Text = "Stop";

                    btnTresholds.Enabled = false;
                    btnCalibGases.Enabled = false;
                    btnLibraryTweaker.Enabled = false;
                    bkgdSelect.Enabled = false;

                    processor.scanning = true;
                    int num_gasses = cm_datagrid.RowCount;
                    Matrix concentrations = new Matrix(16, num_gasses);

                    for (int i = 0; (i < 16) && processor.scanning; ++i)
                    {
                        double[,] values = processor.scanData(statusBar, txtStatus);
                        processor.incrementFileCount(statusBar);
                        processor.compileData(values, txtStatus, statusBar);
                        processor.saveSingleBeam(txtStatus, statusBar);
                        
                        //Now, for all the libraries (processor.libraries), save the values.
                        Matrix[] result_set = processor.getResultMatrix(values, cm_datagrid, rep_datagrid, txtStatus, statusBar);
                        double[] conc = processor.getConcentrationsForCalibration(values, cm_datagrid, rep_datagrid, txtStatus, statusBar);

                        for (int j = 0; j < num_gasses; ++j)
                        {
                            concentrations[i, j] = conc[j];
                        }

                        if (!processor.scanning)
                        {
                            break;
                        }

                        processor.adjustIntegrationTime(txtStatus, statusBar);
                    }
                    processor.scanning = false;

                    if (!calb_aborted)
                    {
                        //Now get the SDE of each gas
                        double[] sde = new double[num_gasses];
                        for (int i = 0; i < 16; ++i)
                        {
                            for (int j = 0; j < num_gasses; ++j)
                            {
                                sde[j] += concentrations[i, j];
                            }
                        }
                        //now that we have the sum, we will compute the mean
                        for (int i = 0; i < num_gasses; ++i)
                        {
                            sde[i] /= 16;
                        }

                        for (int j = 0; j < num_gasses; ++j)
                        {
                            double sq_sum = 0;
                            for (int i = 0; i < 16; ++i)
                            {
                                double temp = sde[j] - concentrations[i, j];
                                sq_sum += temp * temp;
                            }
                            sq_sum /= 16;
                            sde[j] = Math.Sqrt(sq_sum);
                        }
                        /*
                        //Now that we have the sandard deviations, we can compute the
                        //MDL and LOQ
                        double[] mdl = new double[num_gasses];
                        for (int i = 0; i < num_gasses; ++i)
                        {
                            mdl[i] = 3 * sde[i];
                        }
                        double[] loq = new double[num_gasses];
                        for (int i = 0; i < num_gasses; ++i)
                        {
                            loq[i] = 9 * sde[i];
                        }
                        */
                        //FactorInputForAutoCalibration
                        String[] lib_names = new String[cm_datagrid.RowCount];
                        String[] lib_keys = new String[cm_datagrid.RowCount];
                        Random rand = new Random();
                        for (int i = 0; i < cm_datagrid.RowCount; ++i)
                        {
                            lib_names[i] = (String)cm_datagrid[0, i].Value;
                            lib_keys[i] = (String)cm_datagrid[3, i].Tag;
                            //TODO:delete
                            //sde[i] = rand.NextDouble();
                        }

                        FactorInputForAutoCalibration factor_input = new FactorInputForAutoCalibration(lib_names, lib_keys, sde, path_length);
                        DialogResult rslt = factor_input.ShowDialog();
                        /*
                        //Now update the limit in memory
                        string value_to_save = cm_datagrid[3, 0].Tag + "_" + mdl[0].ToString(); ;
                        for (int i = 1; i < num_gasses; ++i)
                        {
                            // cm_datagrid[3, i].Value = mdl[i].ToString();
                            value_to_save += Constants.Delimiters.SET_DELIMITER[0] + (string)cm_datagrid[3, i].Tag + "_" + mdl[i].ToString();
                        }
                        Util.saveValueInRegistry(value_to_save, Constants.UVQuantRegistryKeys.TRESHOLDS);
                        String updated_thresholds = "Calibrated Limits\n\n";
                        for (int i = 0; i < cm_datagrid.RowCount; ++i)
                        {
                            updated_thresholds += cm_datagrid[0, i].Value.ToString() + " --> " + "MDL = " + mdl[i].ToString() + ", LOQ = " + loq[i].ToString() + "\n";
                        }
                        MessageBox.Show(updated_thresholds);
                        //Once we save it in memory, we will recreate the table to read the new settings
                         * */
                        if (rslt == DialogResult.OK)
                        {
                            processor.createTable(cm_datagrid, rep_datagrid);
                        }
                    }

                    updateMainStatus("Done calibrating...", processor.site_name, txtStatus, statusBar);
                    processor.background_data = original_bkgd;
                    processor.backgrounds = original_bkgds;
                    processor.collection_time = original_collection_time;
                    processor.path_length = original_path_length;
                    
                    processor.updateNumberOfAverages();

                    image = Bitmap.FromFile(Application.StartupPath + "/argos_logo_small.JPG");
                    btnAutoCalibrate.BackgroundImage = image;
                    btnAutoCalibrate.Text = "Calibrate";

                    btnTresholds.Enabled = true;
                    btnAutoCalibrate.Enabled = true;
                    btnCalibGases.Enabled = true;
                    btnLibraryTweaker.Enabled = true;
                    bkgdSelect.Enabled = true;
                }
                catch (Exception exp)
                {
                    updateMainStatus("Error!", processor.site_name, txtStatus, statusBar);
                    logError(exp);
                }
            }
        }
    }
}