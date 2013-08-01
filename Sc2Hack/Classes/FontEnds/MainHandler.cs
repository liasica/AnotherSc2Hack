using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Net.NetworkInformation;
using System.Security;
using System.Windows.Forms;
using System.Diagnostics;
using Sc2Hack.Classes.BackEnds;
using System.Threading;

namespace Sc2Hack.Classes.FontEnds
{
    public partial class MainHandler : Form
    {
        #region Variables 

        #region Private 

        private GameInfo _gInformation = new GameInfo();                               //Refrehes itself
        private Renderer _rMaphack;
        private Renderer _rUnit;
        private Renderer _rResources;
        private Renderer _rIncome;
        private Renderer _rWorker;
        private Renderer _rApm;
        private Renderer _rArmy;
        private Renderer _rProduction;
        private Renderer _rTrainer;

        private List<Renderer> _lRenderer = new List<Renderer>(); 

        private const String StrOnlinePath =
            "https://dl.dropboxusercontent.com/u/62845853/AnotherSc2Hack/UpdateFiles/Sc2Hack_Version";

        private const String StrOnlinePathUpdater =
            "https://dl.dropboxusercontent.com/u/62845853/AnotherSc2Hack/UpdateFiles/Sc2Hack_Updater";

        private const String StrOnlinePublicInformation =
            "https://dl.dropboxusercontent.com/u/62845853/AnotherSc2Hack/UpdateFiles/Sc2Hack_PublicInformation";

        private String _strDownloadString = String.Empty;

        private Boolean _bProcessSet = false;

        private CustomToolTip _cTooltip = new CustomToolTip();

        #endregion

        #region Public

        public List<PredefinedTypes.Player> LPlayer = new List<PredefinedTypes.Player>();       //Will be accessable
        public List<PredefinedTypes.Unit> LUnit = new List<PredefinedTypes.Unit>();             //Will be accessable
        public PredefinedTypes.Map MMap = new PredefinedTypes.Map();                            //Will be accessable
        public PredefinedTypes.Gameinformation GInfo = new PredefinedTypes.Gameinformation();   //Will be accessable
        public PredefinedTypes.Preferences PSettings = new PredefinedTypes.Preferences();       //Will be accessable
        public Process PSc2Process = null;                                                      //Will be accessable

        #endregion

        #endregion

        public MainHandler()
        {
            InitializeComponent();

            //Clipboard.SetText((606665725 >> 5).ToString());
            //Clipboard.SetText((606665725 & 0xFFFFFFFC).ToString());
            //PSettings = HelpFunctions.GetPreferences();
            PSettings = HelpFunctions.LoadPreferences();
            PSc2Process = _gInformation.CStarcraft2;
            _gInformation.CSleepTime = 33;

            SetImageCombolist();
            LoadSettingsIntoControls();
            //_rTrainer = new Renderer(PredefinedTypes.RenderForm.Trainer, this);
            //_rTrainer.Show();
         
            /* Stuff that gets downloaded.. */
            new Thread(InitSearch).Start();             //Thread for the Updater [Mainapplication]
            new Thread(InitSearchUpdater).Start();      //Thread for the Updater [Updater]
            new Thread(GetPublicInformation).Start();   //Thread for public information [Mainapplication]
            

            HelpFunctions.CheckIfWindowStyleIsFullscreen(_gInformation.CWindowStyle);
            HelpFunctions.CheckIfDwmIsEnabled();


            /* Set title for Panels */
            Text = HelpFunctions.SetWindowTitle();


            /* Render forms into Renderlist */
            _lRenderer.Add(_rApm);
            _lRenderer.Add(_rArmy);
            _lRenderer.Add(_rIncome);
            _lRenderer.Add(_rMaphack);
            _lRenderer.Add(_rProduction);
            _lRenderer.Add(_rResources);
            _lRenderer.Add(_rTrainer);
            _lRenderer.Add(_rUnit);
            _lRenderer.Add(_rWorker);
        }

        public override sealed string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        #region Launch Panels

        #region Buttons

        private void btnMaphack_Click(object sender, EventArgs e)
        {
            HandleButtonClicks(ref _rMaphack, PredefinedTypes.RenderForm.Maphack, btnMaphack, "Maphack");
        }

        private void btnUnit_Click(object sender, EventArgs e)
        {
            HandleButtonClicks(ref _rUnit, PredefinedTypes.RenderForm.Units, btnUnit, "Unit");
        }

        private void btnResources_Click(object sender, EventArgs e)
        {
            HandleButtonClicks(ref _rResources, PredefinedTypes.RenderForm.Resources, btnResources, "Resources");
        }

        private void btnIncome_Click(object sender, EventArgs e)
        {
            HandleButtonClicks(ref _rIncome, PredefinedTypes.RenderForm.Income, btnIncome, "Income");
        }

        private void btnArmy_Click(object sender, EventArgs e)
        {
            HandleButtonClicks(ref _rArmy, PredefinedTypes.RenderForm.Army, btnArmy, "Army");
        }

        private void btnApm_Click(object sender, EventArgs e)
        {
            HandleButtonClicks(ref _rApm, PredefinedTypes.RenderForm.Apm, btnApm, "Apm");
        }

        private void btnWorker_Click(object sender, EventArgs e)
        {
            HandleButtonClicks(ref _rWorker, PredefinedTypes.RenderForm.Worker, btnWorker, "Worker");
        }

        private void btnProduction_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Not yet implemented!", "Soon (tm)");
            HandleButtonClicks(ref _rProduction, PredefinedTypes.RenderForm.Production, btnProduction, "Production");
        }

        #endregion

        #region Chat Input / Hotkeys / Button- method

        private void LaunchPanels(ref Renderer ren, PredefinedTypes.RenderForm typo, String shortcut, Keys a, Keys b,
                                  Keys c, Button btn, String panelname)
        {

            #region Hotkeys

            if (HelpFunctions.HotkeysPressed(a, b, c))
            {
                if (ren == null)
                    ren = new Renderer(typo, this);

                if (ren.Created)
                {
                    ren.Close();

                    btn.Text = panelname + " Offline";
                    btn.ForeColor = Color.Red;
                }

                else
                {
                    ren = new Renderer(typo, this);
                    ren.Text = HelpFunctions.SetWindowTitle();
                    ren.Show();

                    btn.Text = panelname + " Online";
                    btn.ForeColor = Color.Green;
                }

                Thread.Sleep(200);
            }

            #endregion

            if (PSc2Process == null)
                return;

            #region Chatinput

            var strInput = _gInformation.Gameinfo.ChatInput;



            if (String.IsNullOrEmpty(strInput))
                return;

            if (strInput.Contains('\0'))
                strInput = strInput.Substring(0, strInput.IndexOf('\0'));

            if (strInput.Equals(shortcut))
            {
                if (ren == null)
                    ren = new Renderer(typo, this);

                if (ren.Created)
                {
                    ren.Close();

                    btn.Text = panelname + " Offline";
                    btn.ForeColor = Color.Red;
                }

                else
                {
                    ren = new Renderer(typo, this);
                    ren.Text = HelpFunctions.SetWindowTitle();
                    ren.Show();

                    btn.Text = panelname + " Online";
                    btn.ForeColor = Color.Green;
                }

                Simulation.Keyboard_SimulateKey(PSc2Process.MainWindowHandle, Keys.Enter, 3);
            }



            #endregion
        }

        private void HandleButtonClicks(ref Renderer ren, PredefinedTypes.RenderForm typo, Button btn, String panelname)
        {
            if (ren == null)
                ren = new Renderer(typo, this);

            if (ren.Created)
            {
                ren.Close();

                btn.Text = panelname + " Offline";
                btn.ForeColor = Color.Red;
            }

            else
            {
                ren = new Renderer(typo, this);
                ren.Text = HelpFunctions.SetWindowTitle();
                ren.Show();

                btn.Text = panelname + " Online";
                btn.ForeColor = Color.Green;
            }
        }

        #endregion

        #endregion

        #region - Controls -

        #region Resources

        private void cmBxResRemAi_SelectedIndexChanged(object sender, EventArgs e)
        {
            PSettings.ResourceRemoveAi = Convert.ToBoolean(cmBxResRemAi.Text);
        }

        private void cmBxResRemAllie_SelectedIndexChanged(object sender, EventArgs e)
        {
            PSettings.ResourceRemoveAllie = Convert.ToBoolean(cmBxResRemAllie.Text);
        }

        private void cmBxResRemNeutral_SelectedIndexChanged(object sender, EventArgs e)
        {
            PSettings.ResourceRemoveNeutral = Convert.ToBoolean(cmBxResRemNeutral.Text);
        }

        private void cmBxResRemLocalplayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            PSettings.ResourceRemoveLocalplayer = Convert.ToBoolean(cmBxResRemLocalplayer.Text);
        }

        private void btnResFontName_Click(object sender, EventArgs e)
        {
            var fd = new FontDialog();
            fd.Font = new Font(btnResFontName.Text, 15);
            var result = fd.ShowDialog();

            if (result.Equals(DialogResult.OK))
            {
                btnResFontName.Text = fd.Font.Name;
                PSettings.ResourceFontName = fd.Font.Name;
            }
        }

        private void tbResOpacity_Scroll(object sender, EventArgs e)
        {
            if (tbResOpacity.Value > 0)
                lblResOpacity.Text = "Opacity: " + (tbResOpacity.Value * 1).ToString() + "%";

            else
                tbResOpacity.Value = 1;

            PSettings.ResourceOpacity = (double)tbResOpacity.Value / 100;
        }

        private void txtResTogglePanel_TextChanged(object sender, EventArgs e)
        {
            if (txtResTogglePanel.Text.Length > 0)
                PSettings.ResourceTogglePanel = txtResTogglePanel.Text;
        }

        private void txtResPositionPanel_TextChanged(object sender, EventArgs e)
        {
            if (txtResPositionPanel.Text.Length > 0)
                PSettings.ResourceChangePositionPanel = txtResPositionPanel.Text;
        }

        private void txtResChangeSizePanel_TextChanged(object sender, EventArgs e)
        {
            if (txtResChangeSizePanel.Text.Length > 0)
                PSettings.ResourceChangeSizePanel = txtResChangeSizePanel.Text;
        }

        private void txtResHotkey1_KeyDown(object sender, KeyEventArgs e)
        {
            txtResHotkey1.Text = e.KeyCode.ToString();
            PSettings.ResourceHotkey1 = e.KeyCode;
            e.SuppressKeyPress = true;
        }

        private void txtResHotkey2_KeyDown(object sender, KeyEventArgs e)
        {
            txtResHotkey2.Text = e.KeyCode.ToString();
            PSettings.ResourceHotkey2 = e.KeyCode;
            e.SuppressKeyPress = true;
        }

        private void txtResHotkey3_KeyDown(object sender, KeyEventArgs e)
        {
            txtResHotkey3.Text = e.KeyCode.ToString();
            PSettings.ResourceHotkey3 = e.KeyCode;
            e.SuppressKeyPress = true;
        }

        private void chBxResDrawBackground_CheckedChanged(object sender, EventArgs e)
        {
            PSettings.ResourceDrawBackground = chBxResDrawBackground.Checked;
        }

        #endregion

        #region Income

        private void cmBxIncRemAi_SelectedIndexChanged(object sender, EventArgs e)
        {
            PSettings.IncomeRemoveAi = Convert.ToBoolean(cmBxIncRemAi.Text);
        }

        private void cmBxIncRemAllie_SelectedIndexChanged(object sender, EventArgs e)
        {
            PSettings.IncomeRemoveAllie = Convert.ToBoolean(cmBxIncRemAllie.Text);
        }

        private void cmBxIncRemNeutral_SelectedIndexChanged(object sender, EventArgs e)
        {
            PSettings.IncomeRemoveNeutral = Convert.ToBoolean(cmBxIncRemNeutral.Text);
        }

        private void cmBxIncRemLocalplayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            PSettings.IncomeRemoveLocalplayer = Convert.ToBoolean(cmBxIncRemLocalplayer.Text);
        }

        private void btnIncFontName_Click(object sender, EventArgs e)
        {
            var fd = new FontDialog();
            fd.Font = new Font(btnIncFontName.Text, 15);
            var result = fd.ShowDialog();

            if (result.Equals(DialogResult.OK))
            {
                btnIncFontName.Text = fd.Font.Name;
                PSettings.IncomeFontName = fd.Font.Name;
            }
        }

        private void tbIncOpacity_Scroll(object sender, EventArgs e)
        {
            if (tbIncOpacity.Value > 0)
                lblIncOpacity.Text = "Opacity: " + (tbIncOpacity.Value * 1).ToString() + "%";

            else
                tbIncOpacity.Value = 1;

            PSettings.IncomeOpacity = (double)tbIncOpacity.Value / 100;
        }

        private void txtIncTogglePanel_TextChanged(object sender, EventArgs e)
        {
            if (txtIncTogglePanel.Text.Length > 0)
                PSettings.IncomeTogglePanel = txtIncTogglePanel.Text;
        }

        private void txtIncPositionPanel_TextChanged(object sender, EventArgs e)
        {
            if (txtIncPositionPanel.Text.Length > 0)
                PSettings.IncomeChangePositionPanel = txtIncPositionPanel.Text;
        }

        private void txtIncChangeSizePanel_TextChanged(object sender, EventArgs e)
        {
            if (txtIncChangeSizePanel.Text.Length > 0)
                PSettings.IncomeChangeSizePanel = txtIncChangeSizePanel.Text;
        }

        private void txtIncHotkey1_KeyDown(object sender, KeyEventArgs e)
        {
            txtIncHotkey1.Text = e.KeyCode.ToString();
            PSettings.IncomeHotkey1 = e.KeyCode;
            e.SuppressKeyPress = true;
        }

        private void txtIncHotkey2_KeyDown(object sender, KeyEventArgs e)
        {
            txtIncHotkey2.Text = e.KeyCode.ToString();
            PSettings.IncomeHotkey2 = e.KeyCode;
            e.SuppressKeyPress = true;
        }

        private void txtIncHotkey3_KeyDown(object sender, KeyEventArgs e)
        {
            txtIncHotkey3.Text = e.KeyCode.ToString();
            PSettings.IncomeHotkey3 = e.KeyCode;
            e.SuppressKeyPress = true;
        }

        private void chBxIncDrawBackground_CheckedChanged(object sender, EventArgs e)
        {
            PSettings.IncomeDrawBackground = chBxIncDrawBackground.Checked;
        }

        #endregion

        #region Worker

        private void btnWorFontName_Click(object sender, EventArgs e)
        {
            var fd = new FontDialog();
            fd.Font = new Font(btnWorFontName.Text, 15);
            var result = fd.ShowDialog();

            if (result.Equals(DialogResult.OK))
            {
                btnWorFontName.Text = fd.Font.Name;
                PSettings.WorkerFontName = fd.Font.Name;
            }
        }

        private void tbWorOpacity_Scroll(object sender, EventArgs e)
        {
            if (tbWorOpacity.Value > 0)
                lblWorOpacity.Text = "Opacity: " + (tbWorOpacity.Value * 1).ToString() + "%";

            else
                tbWorOpacity.Value = 1;

            PSettings.WorkerOpacity = (double)tbWorOpacity.Value / 100;
        }

        private void txtWorTogglePanel_TextChanged(object sender, EventArgs e)
        {
            if (txtWorTogglePanel.Text.Length > 0)
                PSettings.WorkerTogglePanel = txtWorTogglePanel.Text;
        }

        private void txtWorPositionPanel_TextChanged(object sender, EventArgs e)
        {
            if (txtWorPositionPanel.Text.Length > 0)
                PSettings.WorkerChangePositionPanel = txtWorPositionPanel.Text;
        }

        private void txtWorChangeSizePanel_TextChanged(object sender, EventArgs e)
        {
            if (txtWorChangeSizePanel.Text.Length > 0)
                PSettings.WorkerChangeSizePanel = txtWorChangeSizePanel.Text;
        }

        private void txtWorHotkey1_KeyDown(object sender, KeyEventArgs e)
        {
            txtWorHotkey1.Text = e.KeyCode.ToString();
            PSettings.WorkerHotkey1 = e.KeyCode;
            e.SuppressKeyPress = true;
        }

        private void txtWorHotkey2_KeyDown(object sender, KeyEventArgs e)
        {
            txtWorHotkey2.Text = e.KeyCode.ToString();
            PSettings.WorkerHotkey2 = e.KeyCode;
            e.SuppressKeyPress = true;
        }

        private void txtWorHotkey3_KeyDown(object sender, KeyEventArgs e)
        {
            txtWorHotkey3.Text = e.KeyCode.ToString();
            PSettings.WorkerHotkey3 = e.KeyCode;
            e.SuppressKeyPress = true;
        }

        private void chBxWorDrawBackground_CheckedChanged(object sender, EventArgs e)
        {
            PSettings.WorkerDrawBackground = chBxWorDrawBackground.Checked;
        }

        #endregion

        #region Apm

        private void cmBxApmRemAi_SelectedIndexChanged(object sender, EventArgs e)
        {
            PSettings.ApmRemoveAi = Convert.ToBoolean(cmBxApmRemAi.Text);
        }

        private void cmBxApmRemAllie_SelectedIndexChanged(object sender, EventArgs e)
        {
            PSettings.ApmRemoveAllie = Convert.ToBoolean(cmBxApmRemAllie.Text);
        }

        private void cmBxApmRemNeutral_SelectedIndexChanged(object sender, EventArgs e)
        {
            PSettings.ApmRemoveNeutral = Convert.ToBoolean(cmBxApmRemNeutral.Text);
        }

        private void cmBxApmRemLocalplayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            PSettings.ApmRemoveLocalplayer = Convert.ToBoolean(cmBxApmRemLocalplayer.Text);
        }

        private void btnApmFontName_Click(object sender, EventArgs e)
        {
            var fd = new FontDialog();
            fd.Font = new Font(btnApmFontName.Text, 15);
            var result = fd.ShowDialog();

            if (result.Equals(DialogResult.OK))
            {
                btnApmFontName.Text = fd.Font.Name;
                PSettings.ApmFontName = fd.Font.Name;
            }
        }

        private void tbApmOpacity_Scroll(object sender, EventArgs e)
        {
            if (tbApmOpacity.Value > 0)
                lblApmOpacity.Text = "Opacity: " + (tbApmOpacity.Value * 1).ToString() + "%";

            else
                tbApmOpacity.Value = 1;

            PSettings.ApmOpacity = (double)tbApmOpacity.Value / 100;
        }

        private void txtApmTogglePanel_TextChanged(object sender, EventArgs e)
        {
            if (txtApmTogglePanel.Text.Length > 0)
                PSettings.ApmTogglePanel = txtApmTogglePanel.Text;
        }

        private void txtApmPositionPanel_TextChanged(object sender, EventArgs e)
        {
            if (txtApmPositionPanel.Text.Length > 0)
                PSettings.ApmChangePositionPanel = txtApmPositionPanel.Text;
        }

        private void txtApmChangeSizePanel_TextChanged(object sender, EventArgs e)
        {
            if (txtApmChangeSizePanel.Text.Length > 0)
                PSettings.ApmChangeSizePanel = txtApmChangeSizePanel.Text;
        }

        private void txtApmHotkey1_KeyDown(object sender, KeyEventArgs e)
        {
            txtApmHotkey1.Text = e.KeyCode.ToString();
            PSettings.ApmHotkey1 = e.KeyCode;
            e.SuppressKeyPress = true;
        }

        private void txtApmHotkey2_KeyDown(object sender, KeyEventArgs e)
        {
            txtApmHotkey2.Text = e.KeyCode.ToString();
            PSettings.ApmHotkey2 = e.KeyCode;
            e.SuppressKeyPress = true;
        }

        private void txtApmHotkey3_KeyDown(object sender, KeyEventArgs e)
        {
            txtApmHotkey3.Text = e.KeyCode.ToString();
            PSettings.ApmHotkey3 = e.KeyCode;
            e.SuppressKeyPress = true;
        }

        private void chBxApmDrawBackground_CheckedChanged(object sender, EventArgs e)
        {
            PSettings.ApmDrawBackground = chBxApmDrawBackground.Checked;
        }

        #endregion

        #region Army

        private void cmBxArmRemAi_SelectedIndexChanged(object sender, EventArgs e)
        {
            PSettings.ArmyRemoveAi = Convert.ToBoolean(cmBxArmRemAi.Text);
        }

        private void cmBxArmRemAllie_SelectedIndexChanged(object sender, EventArgs e)
        {
            PSettings.ArmyRemoveAllie = Convert.ToBoolean(cmBxArmRemAllie.Text);
        }

        private void cmBxArmRemNeutral_SelectedIndexChanged(object sender, EventArgs e)
        {
            PSettings.ArmyRemoveNeutral = Convert.ToBoolean(cmBxArmRemNeutral.Text);
        }

        private void cmBxArmRemLocalplayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            PSettings.ArmyRemoveLocalplayer = Convert.ToBoolean(cmBxArmRemLocalplayer.Text);
        }

        private void btnArmFontName_Click(object sender, EventArgs e)
        {
            var fd = new FontDialog();
            fd.Font = new Font(btnArmFontName.Text, 15);
            var result = fd.ShowDialog();

            if (result.Equals(DialogResult.OK))
            {
                btnArmFontName.Text = fd.Font.Name;
                PSettings.ArmyFontName = fd.Font.Name;
            }
        }

        private void tbArmOpacity_Scroll(object sender, EventArgs e)
        {
            if (tbArmOpacity.Value > 0)
                lblArmOpacity.Text = "Opacity: " + (tbArmOpacity.Value * 1).ToString() + "%";

            else
                tbArmOpacity.Value = 1;

            PSettings.ArmyOpacity = (double)tbArmOpacity.Value / 100;
        }

        private void txtArmTogglePanel_TextChanged(object sender, EventArgs e)
        {
            if (txtArmTogglePanel.Text.Length > 0)
                PSettings.ArmyTogglePanel = txtArmTogglePanel.Text;
        }

        private void txtArmPositionPanel_TextChanged(object sender, EventArgs e)
        {
            if (txtApmPositionPanel.Text.Length > 0)
                PSettings.ArmyChangePositionPanel = txtApmPositionPanel.Text;
        }

        private void txtArmChangeSizePanel_TextChanged(object sender, EventArgs e)
        {
            if (txtArmChangeSizePanel.Text.Length > 0)
                PSettings.ArmyChangeSizePanel = txtArmChangeSizePanel.Text;
        }

        private void txtArmHotkey1_KeyDown(object sender, KeyEventArgs e)
        {
            txtArmHotkey1.Text = e.KeyCode.ToString();
            PSettings.ArmyHotkey1 = e.KeyCode;
            e.SuppressKeyPress = true;
        }

        private void txtArmHotkey2_KeyDown(object sender, KeyEventArgs e)
        {
            txtArmHotkey2.Text = e.KeyCode.ToString();
            PSettings.ArmyHotkey2 = e.KeyCode;
            e.SuppressKeyPress = true;
        }

        private void txtArmHotkey3_KeyDown(object sender, KeyEventArgs e)
        {
            txtArmHotkey3.Text = e.KeyCode.ToString();
            PSettings.ArmyHotkey3 = e.KeyCode;
            e.SuppressKeyPress = true;
        }

        private void chBxArmDrawBackground_CheckedChanged(object sender, EventArgs e)
        {
            PSettings.ArmyDrawBackground = chBxArmDrawBackground.Checked;
        }

        #endregion

        #region Unittab

        private void cmBxUniRemAi_SelectedIndexChanged(object sender, EventArgs e)
        {
            PSettings.UnitTabRemoveAi = Convert.ToBoolean(cmBxUniRemAi.Text);
        }

        private void cmBxUniRemAllie_SelectedIndexChanged(object sender, EventArgs e)
        {
            PSettings.UnitTabRemoveAllie = Convert.ToBoolean(cmBxUniRemAllie.Text);
        }

        private void cmBxUniRemNeutral_SelectedIndexChanged(object sender, EventArgs e)
        {
            PSettings.UnitTabRemoveNeutral = Convert.ToBoolean(cmBxUniRemNeutral.Text);
        }

        private void cmBxUniRemLocalplayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            PSettings.UnitTabRemoveLocalplayer = Convert.ToBoolean(cmBxUniRemLocalplayer.Text);
        }

        private void cmBxUniSplitBuildings_SelectedIndexChanged(object sender, EventArgs e)
        {
            PSettings.UnitTabSplitUnitsAndBuildings = Convert.ToBoolean(cmBxUniSplitBuildings.Text);
        }

        private void tbUniOpacity_Scroll(object sender, EventArgs e)
        {
            if (tbUniOpacity.Value > 0)
                lblUniOpacity.Text = "Opacity: " + (tbUniOpacity.Value * 1).ToString() + "%";

            else
                tbUniOpacity.Value = 1;

            PSettings.UnitTabOpacity = (double)tbUniOpacity.Value / 100;
        }

        private void txtUnitTogglePanel_TextChanged(object sender, EventArgs e)
        {
            if (txtUnitTogglePanel.Text.Length > 0)
                PSettings.UnitTogglePanel = txtUnitTogglePanel.Text;
        }

        private void txtUnitPositionPanel_TextChanged(object sender, EventArgs e)
        {
            if (txtUnitPositionPanel.Text.Length > 0)
                PSettings.UnitChangePositionPanel = txtUnitPositionPanel.Text;
        }

        private void txtUnitChangeSizePanel_TextChanged(object sender, EventArgs e)
        {
            if (txtUnitChangeSizePanel.Text.Length > 0)
                PSettings.UnitChangeSizePanel = txtUnitChangeSizePanel.Text;
        }

        private void txtUnitHotkey1_KeyDown(object sender, KeyEventArgs e)
        {
            txtUnitHotkey1.Text = e.KeyCode.ToString();
            PSettings.UnitHotkey1 = e.KeyCode;
            e.SuppressKeyPress = true;
        }

        private void txtUnitHotkey2_KeyDown(object sender, KeyEventArgs e)
        {
            txtUnitHotkey2.Text = e.KeyCode.ToString();
            PSettings.UnitHotkey2 = e.KeyCode;
            e.SuppressKeyPress = true;
        }

        private void txtUnitHotkey3_KeyDown(object sender, KeyEventArgs e)
        {
            txtUnitHotkey3.Text = e.KeyCode.ToString();
            PSettings.UnitHotkey3 = e.KeyCode;
            e.SuppressKeyPress = true;
        }

        #endregion

        #region Maphack

        private void lstMapUnits_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button.Equals(MouseButtons.Right))
            {
                if (lstMapUnits.IndexFromPoint(e.Location) == -1)
                    return;

                var item = lstMapUnits.Items[lstMapUnits.IndexFromPoint(e.Location)];
                PSettings.MaphackUnitIds.Remove((PredefinedTypes.UnitId)Enum.Parse(typeof(PredefinedTypes.UnitId), item.ToString()));
                lstMapUnits.Items.Remove(item);
            }
        }

        private void lstMapUnits_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstMapUnits.SelectedItems.Count == 1)
            {
                 btnMapUnitColor.BackColor = PSettings.MaphackUnitColors[lstMapUnits.SelectedIndex];
                 icbMapUnit.Text = lstMapUnits.SelectedItem.ToString();
            }
        }

        private void btnMapAddUnit_Click(object sender, EventArgs e)
        {
            /* Add a random entry */
            var rnd = new Random();
            lstMapUnits.Items.Add(rnd.Next(0,80000));

            var id = PredefinedTypes.UnitId.TuScv;

            if (PSettings.MaphackUnitIds == null)
                PSettings.MaphackUnitIds = new List<PredefinedTypes.UnitId>();

            if (PSettings.MaphackUnitColors == null)
                PSettings.MaphackUnitColors = new List<Color>();


            PSettings.MaphackUnitIds.Add(id);
            PSettings.MaphackUnitColors.Add(Color.Blue);
        }

        private void icbMapUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (var i = 0; i < lstMapUnits.Items.Count; i++)
            {
                for (var j = 0; j < lstMapUnits.SelectedItems.Count; j++)
                {
                    if (lstMapUnits.Items[i].Equals(lstMapUnits.SelectedItems[j]))
                    {
                        PSettings.MaphackUnitIds[i] = (PredefinedTypes.UnitId)Enum.Parse(typeof(PredefinedTypes.UnitId), icbMapUnit.Text);
                        //PSettings.MaphackUnitColors[i] = btnMapUnitColor.BackColor;
                        lstMapUnits.Items[i] = icbMapUnit.Text;
                    }
                }
            }

            
        }

        private void btnMapUnitColor_MouseDown(object sender, MouseEventArgs e)
        {
            var clDia = new ColorDialog();

            var result = new DialogResult();


            if (e.Button.Equals(MouseButtons.Left))
                result = clDia.ShowDialog();

            for (var i = 0; i < lstMapUnits.Items.Count; i++)
            {
                for (var j = 0; j < lstMapUnits.SelectedItems.Count; j++)
                {
                    if (lstMapUnits.Items[i].Equals(lstMapUnits.SelectedItems[j]))
                    {
                        if (e.Button.Equals(MouseButtons.Right))
                        {
                            btnMapUnitColor.BackColor = Color.Transparent;
                            PSettings.MaphackUnitColors[i] = Color.Transparent;
                        }

                        else if (e.Button.Equals(MouseButtons.Left))
                        {
                            if (result.Equals(DialogResult.OK))
                                PSettings.MaphackUnitColors[i] = clDia.Color;

                            btnMapUnitColor.BackColor = PSettings.MaphackUnitColors[i];
                        }
                    }
                }
            }

            lstMapUnits.Invalidate();
        }

        private void cmBxMapRemAi_SelectedIndexChanged(object sender, EventArgs e)
        {
            PSettings.MaphackRemoveAi = Convert.ToBoolean(cmBxMapRemAi.Text);
        }

        private void cmBxMapRemAllie_SelectedIndexChanged(object sender, EventArgs e)
        {
            PSettings.MaphackRemoveAllie = Convert.ToBoolean(cmBxMapRemAllie.Text);
        }

        private void cmBxMapRemNeutral_SelectedIndexChanged(object sender, EventArgs e)
        {
            PSettings.MaphackRemoveNeutral = Convert.ToBoolean(cmBxMapRemNeutral.Text);
        }

        private void cmBxMapRemLocalplayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            PSettings.MaphackRemoveLocalplayer = Convert.ToBoolean(cmBxMapRemLocalplayer.Text);
        }

        private void chBxDisableDestinationLine_CheckedChanged(object sender, EventArgs e)
        {
            PSettings.MaphackDisableDestinationLine = chBxDisableDestinationLine.Checked;
        }

        private void tbMapOpacity_Scroll(object sender, EventArgs e)
        {
            if (tbMapOpacity.Value > 0)
                lblMapOpacity.Text = "Opacity: " + (tbMapOpacity.Value * 1).ToString() + "%";

            else
                tbMapOpacity.Value = 1;

            PSettings.MaphackOpacity = (double)tbMapOpacity.Value / 100;
        }

        private void lstMapUnits_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            if (e.Index > -1)
                e.Graphics.DrawString(lstMapUnits.Items[e.Index].ToString(), new Font(Font.Name, Font.Size, FontStyle.Bold), new SolidBrush(PSettings.MaphackUnitColors[e.Index]), e.Bounds);

            e.DrawFocusRectangle();
        }

        private void btnMaphackDestinationLine_Click(object sender, EventArgs e)
        {
            var clDia = new ColorDialog();

            var cl = clDia.ShowDialog();

            if (cl.Equals(DialogResult.OK))
                PSettings.MaphackDestinationColor = clDia.Color;

            btnMaphackDestinationLine.BackColor = PSettings.MaphackDestinationColor;
        }

        private void txtMapTogglePanel_TextChanged(object sender, EventArgs e)
        {
            if (txtMapTogglePanel.Text.Length > 0)
                PSettings.MaphackTogglePanel = txtMapTogglePanel.Text;
        }

        private void txtMapPositionPanel_TextChanged(object sender, EventArgs e)
        {
            if (txtMapPositionPanel.Text.Length > 0)
                PSettings.MaphackChangePositionPanel = txtMapPositionPanel.Text;
        }

        private void txtMapChangeSizePanel_TextChanged(object sender, EventArgs e)
        {
            if (txtMapChangeSizePanel.Text.Length > 0)
                PSettings.MaphackChangeSizePanel = txtMapChangeSizePanel.Text;
        }

        private void txtMapHotkey1_KeyDown(object sender, KeyEventArgs e)
        {
            txtMapHotkey1.Text = e.KeyCode.ToString();
            PSettings.MaphackHotkey1 = e.KeyCode;
            e.SuppressKeyPress = true;
        }

        private void txtMapHotkey2_KeyDown(object sender, KeyEventArgs e)
        {
            txtMapHotkey2.Text = e.KeyCode.ToString();
            PSettings.MaphackHotkey2 = e.KeyCode;
            e.SuppressKeyPress = true;
        }

        private void txtMapHotkey3_KeyDown(object sender, KeyEventArgs e)
        {
            txtMapHotkey3.Text = e.KeyCode.ToString();
            PSettings.MaphackHotkey3 = e.KeyCode;
            e.SuppressKeyPress = true;
        }

        private void chBxMaphackColorDefensiveStructuresYellow_CheckedChanged(object sender, EventArgs e)
        {
            PSettings.MaphackColorDefensivestructuresYellow = chBxMaphackColorDefensiveStructuresYellow.Checked;
        }

        private void chBxMaphackRemVisionArea_CheckedChanged(object sender, EventArgs e)
        {
            PSettings.MaphackRemoveVisionArea = chBxMaphackRemVisionArea.Checked;
        }

        private void chBxMapRemCamera_CheckedChanged(object sender, EventArgs e)
        {
            PSettings.MaphackRemoveCamera = chBxMapRemCamera.Checked;
        }


        #endregion

        #region Trainer

        private void chBxTrainerStealUnits_CheckedChanged(object sender, EventArgs e)
        {
            rdBtnTrainerInstant.Enabled = chBxTrainerStealUnits.Checked;
            rdBtnTrainerKeypress.Enabled = chBxTrainerStealUnits.Checked;

            lblTrainerHotkey1.Enabled = chBxTrainerStealUnits.Checked;
            lblTrainerHotkey2.Enabled = chBxTrainerStealUnits.Checked;
            lblTrainerHotkey3.Enabled = chBxTrainerStealUnits.Checked;
            txtTrainerHotkey1.Enabled = chBxTrainerStealUnits.Checked;
            txtTrainerHotkey2.Enabled = chBxTrainerStealUnits.Checked;
            txtTrainerHotkey3.Enabled = chBxTrainerStealUnits.Checked;

            PSettings.StealUnits = chBxTrainerStealUnits.Checked;
        }

        private void rdBtnTrainerKeypress_CheckedChanged(object sender, EventArgs e)
        {
            lblTrainerHotkey1.Enabled = rdBtnTrainerKeypress.Checked;
            lblTrainerHotkey2.Enabled = rdBtnTrainerKeypress.Checked;
            lblTrainerHotkey3.Enabled = rdBtnTrainerKeypress.Checked;
            txtTrainerHotkey1.Enabled = rdBtnTrainerKeypress.Checked;
            txtTrainerHotkey2.Enabled = rdBtnTrainerKeypress.Checked;
            txtTrainerHotkey3.Enabled = rdBtnTrainerKeypress.Checked;

            PSettings.StealUnitsHotkey = rdBtnTrainerKeypress.Checked;
            PSettings.StealUnitsInstant = !rdBtnTrainerKeypress.Checked;
        }

        private void rdBtnTrainerInstant_CheckedChanged(object sender, EventArgs e)
        {
            lblTrainerHotkey1.Enabled = false;
            lblTrainerHotkey2.Enabled = false;
            lblTrainerHotkey3.Enabled = false;
            txtTrainerHotkey1.Enabled = false;
            txtTrainerHotkey2.Enabled = false;
            txtTrainerHotkey3.Enabled = false;

            PSettings.StealUnitsHotkey = rdBtnTrainerKeypress.Checked;
            PSettings.StealUnitsInstant = !rdBtnTrainerKeypress.Checked;
        }

        private void txtTrainerHotkey1_KeyDown(object sender, KeyEventArgs e)
        {
            txtTrainerHotkey1.Text = e.KeyCode.ToString();
            PSettings.StealUnitsHotkey1 = e.KeyCode;
            e.SuppressKeyPress = true;
        }

        private void txtTrainerHotkey2_KeyDown(object sender, KeyEventArgs e)
        {
            txtTrainerHotkey2.Text = e.KeyCode.ToString();
            PSettings.StealUnitsHotkey2 = e.KeyCode;
            e.SuppressKeyPress = true;
        }

        private void txtTrainerHotkey3_KeyDown(object sender, KeyEventArgs e)
        {
            txtTrainerHotkey3.Text = e.KeyCode.ToString();
            PSettings.StealUnitsHotkey3 = e.KeyCode;
            e.SuppressKeyPress = true;
        }

        #endregion

        #region Debug

        /* Export a file with all information (ID's and Names) of units */
        private void btnExportFile_Click(object sender, EventArgs e)
        {
            var rExportFile = new Renderer(PredefinedTypes.RenderForm.ExportIdsToFile, this)
                {
                    GInfo = _gInformation.Gameinfo,
                    LPlayer = _gInformation.Player,
                    LUnit = _gInformation.Unit,
                    GMap = _gInformation.Map
                };

            /* Crashes because the element wasn't used... 
             * But it was used. Weird shit */
            try
            {
                rExportFile.Show();
            }

            catch { }
        }

        #endregion

        #region Global

        private void txtDataInterval_TextChanged(object sender, EventArgs e)
        {
            if (txtDataInterval.Text.Length <= 0)
                return;

            var iDummy = 0;
            if (Int32.TryParse(txtDataInterval.Text, out iDummy))
            {
                if (iDummy == 0)
                {
                    txtDataInterval.Text = "1";
                    iDummy = 1;
                }

                PSettings.GlobalDataRefresh = iDummy;
                _gInformation.CSleepTime = iDummy;
                tmrGatherInformation.Interval = iDummy;
            }
        }
        
        private void txtDrawingInterval_TextChanged(object sender, EventArgs e)
        {
            if (txtDrawingInterval.Text.Length <= 0)
                return;

            var iDummy = 0;
            if (Int32.TryParse(txtDrawingInterval.Text, out iDummy))
            {
                if (iDummy == 0)
                {
                    txtDrawingInterval.Text = "1";
                    iDummy = 1;
                }

                PSettings.GlobalDrawingRefresh = iDummy;

                /* Adjust drawing refreshrate */
                SetDrawingRefresh(_rApm, iDummy);
                SetDrawingRefresh(_rArmy, iDummy);
                SetDrawingRefresh(_rIncome, iDummy);
                SetDrawingRefresh(_rMaphack, iDummy);
                SetDrawingRefresh(_rProduction, iDummy);
                SetDrawingRefresh(_rResources, iDummy);
                SetDrawingRefresh(_rTrainer, iDummy);
                SetDrawingRefresh(_rUnit, iDummy);
                SetDrawingRefresh(_rWorker, iDummy);

            }
        }

        private void MainHandler_FormClosing(object sender, FormClosingEventArgs e)
        {
            HelpFunctions.WritePreferences(PSettings);

            tmrGatherInformation.Enabled = false;
            _gInformation.HandleThread(false);
            
        }

        private void MainHandler_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void tmrGatherInformation_Tick(object sender, EventArgs e)
        {
            ThrowInformationToPanels(_rResources);
            ThrowInformationToPanels(_rIncome);
            ThrowInformationToPanels(_rWorker);
            ThrowInformationToPanels(_rApm);
            ThrowInformationToPanels(_rArmy);
            ThrowInformationToPanels(_rMaphack);
            ThrowInformationToPanels(_rUnit);
            ThrowInformationToPanels(_rProduction);

            #region Launch Panels

            LaunchPanels(ref _rResources, PredefinedTypes.RenderForm.Resources, PSettings.ResourceTogglePanel, PSettings.ResourceHotkey1, PSettings.ResourceHotkey2, PSettings.ResourceHotkey3, btnResources, "Resource");
            LaunchPanels(ref _rIncome, PredefinedTypes.RenderForm.Income, PSettings.IncomeTogglePanel, PSettings.IncomeHotkey1, PSettings.IncomeHotkey2, PSettings.IncomeHotkey3, btnIncome, "Income");
            LaunchPanels(ref _rWorker, PredefinedTypes.RenderForm.Worker, PSettings.WorkerTogglePanel, PSettings.WorkerHotkey1, PSettings.WorkerHotkey2, PSettings.WorkerHotkey3, btnWorker, "Worker");
            LaunchPanels(ref _rMaphack, PredefinedTypes.RenderForm.Maphack, PSettings.MaphackTogglePanel, PSettings.MaphackHotkey1, PSettings.MaphackHotkey2, PSettings.MaphackHotkey3, btnMaphack, "Maphack");
            LaunchPanels(ref _rApm, PredefinedTypes.RenderForm.Apm, PSettings.ApmTogglePanel, PSettings.ApmHotkey1, PSettings.ApmHotkey2, PSettings.ApmHotkey3, btnApm, "Apm");
            LaunchPanels(ref _rArmy, PredefinedTypes.RenderForm.Army, PSettings.ArmyTogglePanel, PSettings.ArmyHotkey1, PSettings.ArmyHotkey2, PSettings.ArmyHotkey3, btnArmy, "Army");
            LaunchPanels(ref _rUnit, PredefinedTypes.RenderForm.Units, PSettings.UnitTogglePanel, PSettings.UnitHotkey1, PSettings.UnitHotkey2, PSettings.UnitHotkey3, btnUnit, "Unit");

            #endregion

            #region Reset Process and gameinfo if Sc2 is not started

            if (!HelpFunctions.CheckProcess(Constants.StrStarcraft2ProcessName))
            {
                ChangeVisibleState(false);
                _bProcessSet = false;

                tmrGatherInformation.Interval = 300;
                Debug.WriteLine("Process not found - 300ms Delay!");
            }


            else
            {
                if (!_bProcessSet)
                {
                    _bProcessSet = true;

                    PSc2Process = HelpFunctions.GetProcess(Constants.StrStarcraft2ProcessName);
                    _gInformation.HandleThread(false);

                    _gInformation = new GameInfo();

                    ChangeVisibleState(true);
                    tmrGatherInformation.Interval = PSettings.GlobalDataRefresh;

                    Debug.WriteLine("Process found - " + PSettings.GlobalDataRefresh + "ms Delay!");
                }
            }

            #endregion
        }

        private void chBxForegroundDraw_CheckedChanged(object sender, EventArgs e)
        {
            PSettings.GlobalDrawOnlyInForeground = chBxForegroundDraw.Checked;
        }

        private void txtGlobalAdjustKey_KeyDown(object sender, KeyEventArgs e)
        {
            txtGlobalAdjustKey.Text = e.KeyCode.ToString();
            PSettings.GlobalChangeSizeAndPosition = e.KeyCode;
            e.SuppressKeyPress = true;
        }

        /* Draw the credits */
        private void tcCredits_Paint(object sender, PaintEventArgs e)
        {
            var iPosYCycle = 75;
            var iPosYString = 70;
            const Int32 iPosXCycle = 220;
            const Int32 iPosXString = 235;

            e.Graphics.FillEllipse(Brushes.Black, iPosXCycle, iPosYCycle, 7, 7);
            e.Graphics.DrawString("Beaving (D3Scene) - Various hacking information, Concepts and Suggestions", Constants.FCenturyGothic12, Brushes.Black, iPosXString, iPosYString);
            iPosYCycle += 30;
            iPosYString += 30;

            e.Graphics.FillEllipse(Brushes.Black, iPosXCycle, iPosYCycle, 7, 7);
            e.Graphics.DrawString("Mr Nukealizer (D3Scene) - Open Source MH, Ideas and Concepts", Constants.FCenturyGothic12, Brushes.Black, iPosXString, iPosYString);
            iPosYCycle += 30;
            iPosYString += 30;

            e.Graphics.FillEllipse(Brushes.Black, iPosXCycle, iPosYCycle, 7, 7);
            e.Graphics.DrawString("RHCP (D3Scene) - Open Source MH, Gameinteraction, Minimap drawing", Constants.FCenturyGothic12, Brushes.Black, iPosXString, iPosYString);
            iPosYCycle += 30;
            iPosYString += 30;

            e.Graphics.FillEllipse(Brushes.Black, iPosXCycle, iPosYCycle, 7, 7);
            e.Graphics.DrawString("mr_ice (D3Scene) - Graphical help", Constants.FCenturyGothic12, Brushes.Black, iPosXString, iPosYString);
            iPosYCycle += 30;
            iPosYString += 30;

            e.Graphics.FillEllipse(Brushes.Black, iPosXCycle, iPosYCycle, 7, 7);
            e.Graphics.DrawString("Tracky (D3Scene) - Providing the community, Suggestions/ Ideas", Constants.FCenturyGothic12, Brushes.Black, iPosXString, iPosYString);
            iPosYCycle += 30;
            iPosYString += 30;

            e.Graphics.FillEllipse(Brushes.Black, iPosXCycle, iPosYCycle, 7, 7);
            e.Graphics.DrawString("Dark Mage- (Blizzhackers) - Providing Mirrorfiles, Providing the community", Constants.FCenturyGothic12, Brushes.Black, iPosXString, iPosYString);
            iPosYCycle += 30;
            iPosYString += 30;

            e.Graphics.FillEllipse(Brushes.Black, iPosXCycle, iPosYCycle, 7, 7);
            e.Graphics.DrawString("The unmentioned crowd that give Ideas, Suggestions and Bugreports", Constants.FCenturyGothic12, Brushes.Black, iPosXString, iPosYString);
            iPosYCycle += 30;
            iPosYString += 30;
        }

        /* Initiate download... */
        private void GetPublicInformation()
        {
            var wc = new WebClient {Proxy = null};
            var ping = new Ping();

            try
            {
                var res = ping.Send("Dropbox.com", 10);

                if (res == null || !res.Status.Equals(IPStatus.Success)) return;

                wc.DownloadStringAsync(new Uri(StrOnlinePublicInformation));
                wc.DownloadStringCompleted += wc_PublicInformation_DownloadStringComplete;
            }

            catch
            {
                MethodInvoker inv = delegate
                    {
                        rtbPublicInformation.Text = "No Connection!";
                    };

                try
                {
                    Invoke(inv);
                }

                catch
                {
                    /* Do nothing */
                }
            }
        }

        /* When the string is finally downloaded */
        private void wc_PublicInformation_DownloadStringComplete(object sender, DownloadStringCompletedEventArgs downloadStringCompletedEventArgs)
        {
            /* Quick 'n' Dirty */
            MethodInvoker inv = delegate
                {
                    rtbPublicInformation.Text = downloadStringCompletedEventArgs.Result;
                };

            try
            {
                Invoke(inv);
            }

            catch {}
        }

        #endregion

        #region Send Email

        /* Doesn't work anymore.. :/ */
        private void btnEmailSend_Click(object sender, EventArgs e)
        {
            if (cmBxEmailSubject.Text.Equals(string.Empty))
            {
                MessageBox.Show("Select an Item!");
                return;
            }

            if (cmBxEmailSubject.SelectedItem.Equals(String.Empty))
            {
                MessageBox.Show("Select a subject!");
                return;
            }

            if (txtEmailBody.Text.Length <= 0)
            {
                MessageBox.Show("Enter a text and tell what's wrong!");
                return;
            }

            var ssSecure = new SecureString();
            ssSecure.AppendChar('s');
            ssSecure.AppendChar('u');
            ssSecure.AppendChar('s');
            ssSecure.AppendChar('i');
            ssSecure.AppendChar('1');
            ssSecure.AppendChar('2');
            ssSecure.AppendChar('3');
            ssSecure.AppendChar('S');
            ssSecure.AppendChar('e');
            ssSecure.AppendChar('x');

            Messages.SendEmail("smtp.mail.yahoo.com",
                "ww1ww2worm", 
                ssSecure,
                new MailAddress("ww1ww2worm@yahoo.com"),
                txtEmailSubject.Text,
                txtEmailBody.Text,
                "Nothing");
          

            MessageBox.Show("The Email was sent successfully!", "Email sent");
        }

        private void txtEmailBody_TextChanged(object sender, EventArgs e)
        {
            btnEmailSend.Enabled = txtEmailBody.Text.Length > 0 &&
                                   txtEmailSubject.Text.Length > 0;
        }

        private void txtEmailSubject_TextChanged(object sender, EventArgs e)
        {
            btnEmailSend.Enabled = txtEmailBody.Text.Length > 0 &&
                                   txtEmailSubject.Text.Length > 0;
        }

        private void cmBxEmailSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmBxEmailSubject.SelectedItem.Equals("Other"))
                txtEmailSubject.Enabled = true;

            else
                txtEmailSubject.Enabled = false;

            /* Set title */
            txtEmailSubject.Text = cmBxEmailSubject.Text;

            /* Enable/ Disable mailbutton */
            btnEmailSend.Enabled = txtEmailBody.Text.Length > 0;
        }

        /* Because I don't know how to send anonymous email to keep my privacy and the users.. */
        private void btnCreateNewPost_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.d3scene.com/forum/newreply.php?p=487274&noquote=1");
        }

        #endregion

        #endregion

        /* Migrates Information to the Renderforms */
        private void ThrowInformationToPanels(Renderer ren)
        {
            if (ren == null)
                return;

            if (!ren.Created)
                return;

            ren.GInfo = _gInformation.Gameinfo;
            ren.GMap = _gInformation.Map;
            ren.LPlayer = _gInformation.Player;
            ren.LUnit = _gInformation.Unit;
        }

        /* Make panels in/ visible */
        private void ChangeVisibleState(Boolean state)
        {
            if (_rApm != null)
                _rApm.Visible = state;

            if (_rArmy != null)
                _rArmy.Visible = state;

            if (_rIncome != null)
                _rIncome.Visible = state;

            if (_rMaphack != null)
                _rMaphack.Visible = state;

            if (_rProduction != null)
                _rProduction.Visible = state;

            if (_rResources != null)
                _rResources.Visible = state;

            if (_rUnit != null)
                _rUnit.Visible = state;

            if (_rWorker != null)
                _rWorker.Visible = state;
        }

        /* Change Textbox Content (Width, Height, PosX, PosY) */
        private void ChangeTextboxInformation()
        {
            /* Resource */
            txtResPosX.Text = PSettings.ResourcePositionX.ToString();
            txtResPosY.Text = PSettings.ResourcePositionY.ToString();
            txtResWidth.Text = PSettings.ResourceWidth.ToString();
            txtResHeight.Text = PSettings.ResourceHeight.ToString();

            /* Income */
            txtIncPosX.Text = PSettings.IncomePositionX.ToString();
            txtIncPosY.Text = PSettings.IncomePositionY.ToString();
            txtIncWidth.Text = PSettings.IncomeWidth.ToString();
            txtIncHeight.Text = PSettings.IncomeHeight.ToString();

            /* Worker */
            txtWorPosX.Text = PSettings.WorkerPositionX.ToString();
            txtWorPosY.Text = PSettings.WorkerPositionY.ToString();
            txtWorWidth.Text = PSettings.WorkerWidth.ToString();
            txtWorHeight.Text = PSettings.WorkerHeight.ToString();

            /* Maphack */
            txtMapPosX.Text = PSettings.MaphackPositionX.ToString();
            txtMapPosY.Text = PSettings.MaphackPositionY.ToString();
            txtMapWidth.Text = PSettings.MaphackWidth.ToString();
            txtMapHeight.Text = PSettings.MaphackHeigth.ToString();

            /* Apm */
            txtApmPosX.Text = PSettings.ApmPositionX.ToString();
            txtApmPosY.Text = PSettings.ApmPositionY.ToString();
            txtApmWidth.Text = PSettings.ApmWidth.ToString();
            txtApmHeight.Text = PSettings.ApmHeight.ToString();

            /* Army */
            txtArmPosX.Text = PSettings.ArmyPositionX.ToString();
            txtArmPosY.Text = PSettings.ArmyPositionY.ToString();
            txtArmWidth.Text = PSettings.ArmyWidth.ToString();
            txtArmHeight.Text = PSettings.ArmyHeight.ToString();

            /* UnitTab */
            txtUniPosX.Text = PSettings.UnitTabPositionX.ToString();
            txtUniPosY.Text = PSettings.UnitTabPositionY.ToString();
            txtUniWidth.Text = PSettings.UnitTabWidth.ToString();
            txtUniHeight.Text = PSettings.UnitTabHeigth.ToString();
        }

        /* SetDrawingrefresh- rate */
        private void SetDrawingRefresh(Renderer renderForm, int iDummy)
        {
            if (renderForm != null &&
                renderForm.Created)
            {
                renderForm.tmrRefreshGraphic.Interval = iDummy;
            }

        }

        #region Help Methods for local Controls

        /* Load all control- settings into the form */
        private void LoadSettingsIntoControls()
        {
            /* Resources */
            cmBxResRemAi.Text = PSettings.ResourceRemoveAi.ToString();
            cmBxResRemAllie.Text = PSettings.ResourceRemoveAllie.ToString();
            cmBxResRemNeutral.Text = PSettings.ResourceRemoveNeutral.ToString();
            cmBxResRemLocalplayer.Text = PSettings.ResourceRemoveLocalplayer.ToString();
            btnResFontName.Text = PSettings.ResourceFontName;
            tbResOpacity.Value = (Int32)(PSettings.ResourceOpacity*100);
            lblResOpacity.Text = "Opacity: " + tbResOpacity.Value.ToString() + "%";
            txtResTogglePanel.Text = PSettings.ResourceTogglePanel;
            txtResPositionPanel.Text = PSettings.ResourceChangePositionPanel;
            txtResChangeSizePanel.Text = PSettings.ResourceChangeSizePanel;
            txtResHotkey1.Text = PSettings.ResourceHotkey1.ToString();
            txtResHotkey2.Text = PSettings.ResourceHotkey2.ToString();
            txtResHotkey3.Text = PSettings.ResourceHotkey3.ToString();
            chBxResDrawBackground.Checked = PSettings.ResourceDrawBackground;

            /* Income */
            cmBxIncRemAi.Text = PSettings.IncomeRemoveAi.ToString();
            cmBxIncRemAllie.Text = PSettings.IncomeRemoveAllie.ToString();
            cmBxIncRemNeutral.Text = PSettings.IncomeRemoveNeutral.ToString();
            cmBxIncRemLocalplayer.Text = PSettings.IncomeRemoveLocalplayer.ToString();
            btnIncFontName.Text = PSettings.IncomeFontName;
            tbIncOpacity.Value = (Int32)(PSettings.IncomeOpacity * 100);
            lblIncOpacity.Text = "Opacity: " + tbIncOpacity.Value.ToString() + "%";
            txtIncTogglePanel.Text = PSettings.IncomeTogglePanel;
            txtIncPositionPanel.Text = PSettings.IncomeChangePositionPanel;
            txtIncChangeSizePanel.Text = PSettings.IncomeChangeSizePanel;
            txtIncHotkey1.Text = PSettings.IncomeHotkey1.ToString();
            txtIncHotkey2.Text = PSettings.IncomeHotkey2.ToString();
            txtIncHotkey3.Text = PSettings.IncomeHotkey3.ToString();
            chBxIncDrawBackground.Checked = PSettings.IncomeDrawBackground;

            /* Army */
            cmBxArmRemAi.Text = PSettings.ArmyRemoveAi.ToString();
            cmBxArmRemAllie.Text = PSettings.ArmyRemoveAllie.ToString();
            cmBxArmRemNeutral.Text = PSettings.ArmyRemoveNeutral.ToString();
            cmBxArmRemLocalplayer.Text = PSettings.ArmyRemoveLocalplayer.ToString();
            btnArmFontName.Text = PSettings.ArmyFontName;
            tbArmOpacity.Value = (Int32)(PSettings.ArmyOpacity * 100);
            lblArmOpacity.Text = "Opacity: " + tbArmOpacity.Value.ToString() + "%";
            txtArmTogglePanel.Text = PSettings.ArmyTogglePanel;
            txtArmPositionPanel.Text = PSettings.ArmyChangePositionPanel;
            txtArmChangeSizePanel.Text = PSettings.ArmyChangeSizePanel;
            txtArmHotkey1.Text = PSettings.ArmyHotkey1.ToString();
            txtArmHotkey2.Text = PSettings.ArmyHotkey2.ToString();
            txtArmHotkey3.Text = PSettings.ArmyHotkey3.ToString();
            chBxArmDrawBackground.Checked = PSettings.ArmyDrawBackground;

            /* Apm */
            cmBxApmRemAi.Text = PSettings.ApmRemoveAi.ToString();
            cmBxApmRemAllie.Text = PSettings.ApmRemoveAllie.ToString();
            cmBxApmRemNeutral.Text = PSettings.ApmRemoveNeutral.ToString();
            cmBxApmRemLocalplayer.Text = PSettings.ApmRemoveLocalplayer.ToString();
            btnApmFontName.Text = PSettings.ApmFontName;
            tbApmOpacity.Value = (Int32)(PSettings.ApmOpacity * 100);
            lblApmOpacity.Text = "Opacity: " + tbApmOpacity.Value.ToString() + "%";
            txtApmTogglePanel.Text = PSettings.ApmTogglePanel;
            txtApmPositionPanel.Text = PSettings.ApmChangePositionPanel;
            txtApmChangeSizePanel.Text = PSettings.ApmChangeSizePanel;
            txtApmHotkey1.Text = PSettings.ApmHotkey1.ToString();
            txtApmHotkey2.Text = PSettings.ApmHotkey2.ToString();
            txtApmHotkey3.Text = PSettings.ApmHotkey3.ToString();
            chBxApmDrawBackground.Checked = PSettings.ApmDrawBackground;

            /* Worker */
            btnWorFontName.Text = PSettings.WorkerFontName;
            tbWorOpacity.Value = (Int32)(PSettings.WorkerOpacity * 100);
            lblWorOpacity.Text = "Opacity: " + tbWorOpacity.Value.ToString() + "%";
            txtWorTogglePanel.Text = PSettings.WorkerTogglePanel;
            txtWorPositionPanel.Text = PSettings.WorkerChangePositionPanel;
            txtWorChangeSizePanel.Text = PSettings.WorkerChangeSizePanel;
            txtWorHotkey1.Text = PSettings.WorkerHotkey1.ToString();
            txtWorHotkey2.Text = PSettings.WorkerHotkey2.ToString();
            txtWorHotkey3.Text = PSettings.WorkerHotkey3.ToString();
            chBxWorDrawBackground.Checked = PSettings.WorkerDrawBackground;

            /* Maphack */
            cmBxMapRemAi.Text = PSettings.MaphackRemoveAi.ToString();
            cmBxMapRemAllie.Text = PSettings.MaphackRemoveAllie.ToString();
            cmBxMapRemNeutral.Text = PSettings.MaphackRemoveNeutral.ToString();
            cmBxMapRemLocalplayer.Text = PSettings.MaphackRemoveLocalplayer.ToString();
            tbMapOpacity.Value = (Int32)(PSettings.MaphackOpacity * 100);
            lblMapOpacity.Text = "Opacity: " + tbMapOpacity.Value.ToString() + "%";
            btnMaphackDestinationLine.BackColor = PSettings.MaphackDestinationColor;
            txtMapTogglePanel.Text = PSettings.MaphackTogglePanel;
            txtMapPositionPanel.Text = PSettings.MaphackChangePositionPanel;
            txtMapChangeSizePanel.Text = PSettings.MaphackChangeSizePanel;
            txtMapHotkey1.Text = PSettings.MaphackHotkey1.ToString();
            txtMapHotkey2.Text = PSettings.MaphackHotkey2.ToString();
            txtMapHotkey3.Text = PSettings.MaphackHotkey3.ToString();
            chBxDisableDestinationLine.Checked = PSettings.MaphackDisableDestinationLine;
            chBxMaphackColorDefensiveStructuresYellow.Checked = PSettings.MaphackColorDefensivestructuresYellow;
            chBxMaphackRemVisionArea.Checked = PSettings.MaphackRemoveVisionArea;
            chBxMapRemCamera.Checked = PSettings.MaphackRemoveCamera;

            /* UnitIds */
            if (PSettings.MaphackUnitIds != null &&
                PSettings.MaphackUnitColors != null)
            {
                if (PSettings.MaphackUnitIds.Count > 0 &&
                    PSettings.MaphackUnitColors.Count > 0)
                {

                    for (var i = 0; i < PSettings.MaphackUnitIds.Count; i++)
                        lstMapUnits.Items.Add(PSettings.MaphackUnitIds[i].ToString());
                }
            }

      

            /* Unittab */
            cmBxUniRemAi.Text = PSettings.UnitTabRemoveAi.ToString();
            cmBxUniRemAllie.Text = PSettings.UnitTabRemoveAllie.ToString();
            cmBxUniRemNeutral.Text = PSettings.UnitTabRemoveNeutral.ToString();
            cmBxUniRemLocalplayer.Text = PSettings.UnitTabRemoveLocalplayer.ToString();
            cmBxUniSplitBuildings.Text = PSettings.UnitTabSplitUnitsAndBuildings.ToString();
            tbUniOpacity.Value = (Int32) PSettings.UnitTabOpacity*100;
            lblUniOpacity.Text = "Opacity: " + tbUniOpacity.Value.ToString() + "%";
            txtUnitTogglePanel.Text = PSettings.UnitTogglePanel;
            txtUnitPositionPanel.Text = PSettings.UnitChangePositionPanel;
            txtUnitChangeSizePanel.Text = PSettings.UnitChangeSizePanel;
            txtUnitHotkey1.Text = PSettings.UnitHotkey1.ToString();
            txtUnitHotkey2.Text = PSettings.UnitHotkey2.ToString();
            txtUnitHotkey3.Text = PSettings.UnitHotkey3.ToString();

            /* Trainer */
            chBxTrainerStealUnits.Checked = PSettings.StealUnits;
            rdBtnTrainerKeypress.Checked = PSettings.StealUnitsHotkey;
            rdBtnTrainerInstant.Checked = PSettings.StealUnitsInstant;
            txtTrainerHotkey1.Text = PSettings.StealUnitsHotkey1.ToString();
            txtTrainerHotkey2.Text = PSettings.StealUnitsHotkey2.ToString();
            txtTrainerHotkey3.Text = PSettings.StealUnitsHotkey3.ToString();

            /* Global */
            txtDataInterval.Text = PSettings.GlobalDataRefresh.ToString();
            txtDrawingInterval.Text = PSettings.GlobalDrawingRefresh.ToString();
            chBxForegroundDraw.Checked = PSettings.GlobalDrawOnlyInForeground;
            txtGlobalAdjustKey.Text = PSettings.GlobalChangeSizeAndPosition.ToString();
            
            
            /* - Non settings - */
            lblMainApplication.Text = "[" + Application.ProductName + "] - Ver.: " +
                                      System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

            if (File.Exists("Sc2Hack UpdateManager.exe"))
            {
                var fvInfo = FileVersionInfo.GetVersionInfo("Sc2Hack UpdateManager.exe");

                lblUpdaterApplication.Text = "[" + fvInfo.ProductName + "] - Ver.: " +
                                             fvInfo.FileVersion;
            }

            /* Load the final positions and textboxes */
            ChangeTextboxInformation();
        }

        #region Maphack

        /* Throw images and Strings into the Imageboxes [Maphack] */
        private void SetImageCombolist()
        {
            #region Image Combobox Global Units

            /* Terran Buildings */
            icbMapUnit.Items.Add(new ImageComboItem("TbCcGround", 50, true));
            icbMapUnit.Items.Add(new ImageComboItem("TbOrbitalGround", 51, true));
            icbMapUnit.Items.Add(new ImageComboItem("TbPlanetary", 52, true));
            icbMapUnit.Items.Add(new ImageComboItem("TbSupplyGround", 53, true));
            icbMapUnit.Items.Add(new ImageComboItem("TbBarracksGround", 54, true));
            icbMapUnit.Items.Add(new ImageComboItem("TbRefinery", 55, true));
            icbMapUnit.Items.Add(new ImageComboItem("TbEbay", 56, true));
            icbMapUnit.Items.Add(new ImageComboItem("TbBunker", 57, true));
            icbMapUnit.Items.Add(new ImageComboItem("TbTurret", 58, true));
            icbMapUnit.Items.Add(new ImageComboItem("TbSensortower", 59, true));
            icbMapUnit.Items.Add(new ImageComboItem("TbGhostacademy", 60, true));
            icbMapUnit.Items.Add(new ImageComboItem("TbFactoryGround", 61, true));
            icbMapUnit.Items.Add(new ImageComboItem("TbStarportGround", 62, true));
            icbMapUnit.Items.Add(new ImageComboItem("TbArmory", 63, true));
            icbMapUnit.Items.Add(new ImageComboItem("TbFusioncore", 64, true));

            /* Protoss Buildings */
            icbMapUnit.Items.Add(new ImageComboItem("PbNexus", 65, true));
            icbMapUnit.Items.Add(new ImageComboItem("PbPylon", 66, true));
            icbMapUnit.Items.Add(new ImageComboItem("PbAssimilator", 67, true));
            icbMapUnit.Items.Add(new ImageComboItem("PbGateway", 68, true));
            icbMapUnit.Items.Add(new ImageComboItem("PbWarpgate", 69, true));
            icbMapUnit.Items.Add(new ImageComboItem("PbForge", 70, true));
            icbMapUnit.Items.Add(new ImageComboItem("PbCybercore", 71, true));
            icbMapUnit.Items.Add(new ImageComboItem("PbCannon", 72, true));
            icbMapUnit.Items.Add(new ImageComboItem("PbRoboticsbay", 73, true));
            icbMapUnit.Items.Add(new ImageComboItem("PbRoboticssupportbay", 74, true));
            icbMapUnit.Items.Add(new ImageComboItem("PbStargate", 75, true));
            icbMapUnit.Items.Add(new ImageComboItem("PbFleetbeacon", 76, true));
            icbMapUnit.Items.Add(new ImageComboItem("PbTwilightcouncil", 77, true));
            icbMapUnit.Items.Add(new ImageComboItem("PbDarkshrine", 78, true));
            icbMapUnit.Items.Add(new ImageComboItem("PbTemplararchives", 79, true));

            /* Zerg Buildings */
            icbMapUnit.Items.Add(new ImageComboItem("ZbHatchery", 80, true));
            icbMapUnit.Items.Add(new ImageComboItem("ZbLiar", 81, true));
            icbMapUnit.Items.Add(new ImageComboItem("ZbHive", 82, true));
            icbMapUnit.Items.Add(new ImageComboItem("ZbSpawningPool", 83, true));
            icbMapUnit.Items.Add(new ImageComboItem("ZbBanelingNest", 84, true));
            icbMapUnit.Items.Add(new ImageComboItem("ZbExtractor", 85, true));
            icbMapUnit.Items.Add(new ImageComboItem("ZbEvolutionChamber", 86, true));
            icbMapUnit.Items.Add(new ImageComboItem("ZbSporeCrawler", 87, true));
            icbMapUnit.Items.Add(new ImageComboItem("ZbSpineCrawler", 88, true));
            icbMapUnit.Items.Add(new ImageComboItem("ZbRoachWarren", 89, true));
            icbMapUnit.Items.Add(new ImageComboItem("ZbSpire", 90, true));
            icbMapUnit.Items.Add(new ImageComboItem("ZbHydraDen", 91, true));
            icbMapUnit.Items.Add(new ImageComboItem("ZbInfestationPit", 92, true));
            icbMapUnit.Items.Add(new ImageComboItem("ZbNydusWorm", 93, true));
            icbMapUnit.Items.Add(new ImageComboItem("ZbNydusNetwork", 94, true));
            icbMapUnit.Items.Add(new ImageComboItem("ZbUltraCavern", 95, true));
            icbMapUnit.Items.Add(new ImageComboItem("ZbGreaterspire", 96, true));
            icbMapUnit.Items.Add(new ImageComboItem("ZbCreeptumor", 97, true));

            /* Terran Units */
            icbMapUnit.Items.Add(new ImageComboItem("TuScv", 0, true));
            icbMapUnit.Items.Add(new ImageComboItem("TuMule", 1, true));
            icbMapUnit.Items.Add(new ImageComboItem("TuMarine", 2, true));
            icbMapUnit.Items.Add(new ImageComboItem("TuMarauder", 3, true));
            icbMapUnit.Items.Add(new ImageComboItem("TuGhost", 4, true));
            icbMapUnit.Items.Add(new ImageComboItem("TuReaper", 5, true));
            icbMapUnit.Items.Add(new ImageComboItem("TuHellion", 6, true));
            icbMapUnit.Items.Add(new ImageComboItem("TuHellbat", 7, true));
            icbMapUnit.Items.Add(new ImageComboItem("TuWidowMine", 8, true));
            icbMapUnit.Items.Add(new ImageComboItem("TuSiegetank", 9, true));
            icbMapUnit.Items.Add(new ImageComboItem("TuThor", 10, true));
            icbMapUnit.Items.Add(new ImageComboItem("TuMedivac", 11, true));
            icbMapUnit.Items.Add(new ImageComboItem("TuBanshee", 12, true));
            icbMapUnit.Items.Add(new ImageComboItem("TuVikingAir", 13, true));
            icbMapUnit.Items.Add(new ImageComboItem("TuRaven", 14, true));
            icbMapUnit.Items.Add(new ImageComboItem("TuBattlecruiser", 15, true));

            /* Protoss Units */
            icbMapUnit.Items.Add(new ImageComboItem("PuProbe", 16, true));
            icbMapUnit.Items.Add(new ImageComboItem("PuMothershipCore", 17, true));
            icbMapUnit.Items.Add(new ImageComboItem("PuZealot", 18, true));
            icbMapUnit.Items.Add(new ImageComboItem("PuStalker", 19, true));
            icbMapUnit.Items.Add(new ImageComboItem("PuSentry", 20, true));
            icbMapUnit.Items.Add(new ImageComboItem("PuHightemplar", 21, true));
            icbMapUnit.Items.Add(new ImageComboItem("PuDarktemplar", 22, true));
            icbMapUnit.Items.Add(new ImageComboItem("PuArchon", 23, true));
            icbMapUnit.Items.Add(new ImageComboItem("PuImmortal", 24, true));
            icbMapUnit.Items.Add(new ImageComboItem("PuObserver", 25, true));
            icbMapUnit.Items.Add(new ImageComboItem("PuWarpprismTransport", 26, true));
            icbMapUnit.Items.Add(new ImageComboItem("PuColossus", 27, true));
            icbMapUnit.Items.Add(new ImageComboItem("PuPhoenix", 28, true));
            icbMapUnit.Items.Add(new ImageComboItem("PuOracle", 29, true));
            icbMapUnit.Items.Add(new ImageComboItem("PuVoidray", 30, true));
            icbMapUnit.Items.Add(new ImageComboItem("PuCarrier", 31, true));
            icbMapUnit.Items.Add(new ImageComboItem("PuTempest", 32, true));
            icbMapUnit.Items.Add(new ImageComboItem("PuMothership", 33, true));

            /* Zerg Units */
            icbMapUnit.Items.Add(new ImageComboItem("ZuLarva", 34, true));
            icbMapUnit.Items.Add(new ImageComboItem("ZuDrone", 35, true));
            icbMapUnit.Items.Add(new ImageComboItem("ZuOverlord", 36, true));
            icbMapUnit.Items.Add(new ImageComboItem("ZuQueen", 37, true));
            icbMapUnit.Items.Add(new ImageComboItem("ZuZergling", 38, true));
            icbMapUnit.Items.Add(new ImageComboItem("ZuBanelingCocoon", 98, true));
            icbMapUnit.Items.Add(new ImageComboItem("ZuBaneling", 39, true));
            icbMapUnit.Items.Add(new ImageComboItem("ZuRoach", 40, true));
            icbMapUnit.Items.Add(new ImageComboItem("ZuHydralisk", 41, true));
            icbMapUnit.Items.Add(new ImageComboItem("ZuOverseerCocoon", 100, true));
            icbMapUnit.Items.Add(new ImageComboItem("ZuOverseer", 42, true));
            icbMapUnit.Items.Add(new ImageComboItem("ZuMutalisk", 43, true));
            icbMapUnit.Items.Add(new ImageComboItem("ZuCorruptor", 44, true));
            icbMapUnit.Items.Add(new ImageComboItem("ZuInfestor", 45, true));
            icbMapUnit.Items.Add(new ImageComboItem("ZuBroodlordCocoon", 99, true));
            icbMapUnit.Items.Add(new ImageComboItem("ZuBroodlord", 46, true));
            icbMapUnit.Items.Add(new ImageComboItem("ZuUltra", 47, true));
            icbMapUnit.Items.Add(new ImageComboItem("ZuViper", 48, true));
            icbMapUnit.Items.Add(new ImageComboItem("ZuSwarmHost", 49, true));

            #endregion
        }

        #endregion

        #endregion

        #region - Methods for the updater -

        #region Check for an Update for the Main Application

        /* Get new Updates */
        private void btnGetUpdate_Click(object sender, EventArgs e)
        {
            if (File.Exists("Sc2Hack UpdateManager.exe"))
                Process.Start("Sc2Hack UpdateManager.exe");

            else
            {
                var wc = new WebClient();
                wc.DownloadFileAsync(new Uri("https://dl.dropboxusercontent.com/u/62845853/AnotherSc2Hack/Binaries/AnotherSc2Hack_0.2.2.0/Sc2Hack%20UpdateManager.exe"), "Sc2Hack UpdateManager.exe");

                wc.DownloadFileCompleted += wc_DownloadFileCompleted;

            }
        }

        /* In case the file is lost.. */
        void wc_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (File.Exists("Sc2Hack UpdateManager.exe"))
                Process.Start("Sc2Hack UpdateManager.exe");

            else 
                btnGetUpdate.PerformClick();
            
        }

        /* Initial search for updates */
        public void InitSearch()
        {
            var iCountTimeOuts = 0;

        TryAnotherRound:

            /* We ping the Server first to exclude exceptions! */
            var myPing = new Ping();

            PingReply myResult;


            try
            {
                myResult = myPing.Send("Dropbox.com", 10);
                Debug.WriteLine("Sent ping! (" + iCountTimeOuts.ToString() + ")");
            }

            catch
            {
                    var iCounter = 0;
                TryAnotherInvoke:

                    MethodInvoker inv =
                    delegate
                    {
                        btnGetUpdate.ForeColor = Color.Red;
                        btnGetUpdate.Text = "No Connection!";
                        btnGetUpdate.Enabled = false;
                    };

                

                    try
                    {
                        Invoke(inv);
                    }
                    catch
                    {
                        if (iCounter > 5)
                        return;

                        iCounter += 1;
                        goto TryAnotherInvoke;
                    }

                    return;
            }

            if (myResult.Status != IPStatus.Success)
            {
                if (iCountTimeOuts >= 10)
                {
                    MessageBox.Show("Can not reach Server!\n\nTry later!", "FAILED");
                    return;
                }

                iCountTimeOuts++;
                goto TryAnotherRound;

            }

            Debug.WriteLine("Initiate Webclient!");

            /* Reset Title */
            MethodInvoker invBtn = delegate
                {
                    btnGetUpdate.Text = "- Searching - ";
                    btnGetUpdate.ForeColor = Color.Black;
                    btnGetUpdate.Enabled = false;
                };

            try
            {
                Invoke(invBtn);
            }

            catch {}

            /* Connect to server */
            var privateWebClient = new WebClient();
            privateWebClient.Proxy = null;

            string strSource = string.Empty;

            try
            {
                Debug.WriteLine("Downloading String");
                strSource = privateWebClient.DownloadString(StrOnlinePath);
                //privateWebClient.DownloadFile(StrOnlinePath, Application.StartupPath + "\\tmp");
                //StreamReader sr = new StreamReader(Application.StartupPath + "\\tmp");
                //strSource = sr.ReadToEnd();
                //sr.Close();

                Debug.WriteLine("String downloaded");
            }

            catch
            {
                MessageBox.Show("Can not reach Server!\n\nTry later!", "FAILED");
                Close();
                return;
            }

            /* Build version from this file */
            var curVer = new Version(System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());

            /* Build version from online- file (string) */
            var newVer = new Version(GetStringItems(0, strSource));

            /* Version- check */
            if (newVer > curVer)
            {
                //_strDownloadString = GetStringItems(1, strSource);

                MethodInvoker inv =
                    delegate
                        {
                            btnGetUpdate.ForeColor = Color.Green;
                            btnGetUpdate.Text = "Get Update!";
                            btnGetUpdate.Enabled = true;
                        };

                byte bCounter = 0;
            InvokeAgain:
                try
                {
                    Invoke(inv);
                }

                catch
                {
                    bCounter++;
                    if (bCounter >= 5)
                        return;

                    goto InvokeAgain;
                }

                return;
            }

           

            MethodInvoker inv2 =
                delegate
                    {
                        btnGetUpdate.Text = "Up-To-Date!";
                    };

            byte bCounter2 = 0;
        InvokeAgain2:
            try
            {
                Invoke(inv2);
            }

            catch
            {
                bCounter2++;
                if (bCounter2 >= 5)
                    return;

                goto InvokeAgain2;
            }
        }

        /* Parses out a string of Line x */
        private string GetStringItems(int line, string source)
        {
            /* Is Like
              1  Version=0.0.1.0
              2  Path=https://dl.dropbox.com/u/62845853/AnotherSc2Hack/Binaries/Another%20SC2%20Hack.exe
              3  Changes=https://dl.dropbox.com/u/62845853/AnotherSc2Hack/UpdateFiles/Changes */

            var strmoreSource = source.Split('\n');
            if (strmoreSource[line].Contains("\r"))
                strmoreSource[line] = strmoreSource[line].Substring(0, strmoreSource[line].IndexOf('\r'));

            return strmoreSource[line];
        }

        #endregion

        #region Check for an Update for the Updater

        /* Initial search for updates */
        public void InitSearchUpdater()
        {
            var iCountTimeOuts = 0;

        TryAnotherRound:

            /* We ping the Server first to exclude exceptions! */
            var myPing = new Ping();

            PingReply myResult;


            try
            {
                myResult = myPing.Send("Dropbox.com", 10);
                Debug.WriteLine("Sent ping! (" + iCountTimeOuts.ToString() + ")");
            }

            catch
            {
                iCountTimeOuts++;
                goto TryAnotherRound;
            }

            if (myResult.Status != IPStatus.Success)
            {
                if (iCountTimeOuts >= 10)
                {
                    MessageBox.Show("Can not reach Server!\n\nTry later!", "FAILED");
                    return;
                }

                iCountTimeOuts++;
                goto TryAnotherRound;

            }

            Debug.WriteLine("Initiate Webclient!");


            /* Connect to server */
            var privateWebClient = new WebClient();
            privateWebClient.Proxy = null;

            string strSource = string.Empty;

            try
            {
                Debug.WriteLine("Downloading String");
                strSource = privateWebClient.DownloadString(StrOnlinePathUpdater);
                //privateWebClient.DownloadFile(StrOnlinePath, Application.StartupPath + "\\tmp");
                //StreamReader sr = new StreamReader(Application.StartupPath + "\\tmp");
                //strSource = sr.ReadToEnd();
                //sr.Close();

                Debug.WriteLine("String downloaded");
            }

            catch
            {
                MessageBox.Show("Can not reach Server!\n\nTry later!", "FAILED");
                Close();
                return;
            }


            /* Build version from this file */
            FileVersionInfo inf;
            Version curVer;
            if (File.Exists(Constants.StrUpdaterPath))
            {
                inf = FileVersionInfo.GetVersionInfo(Constants.StrUpdaterPath);
                curVer = new Version(inf.FileVersion);
            }

            else
            {
                curVer = new Version(0,0,0,1);
            }
            

            /* Build version from online- file (string) */
            var newVer = new Version(GetStringItems(0, strSource));

            /* Version- check */
            if (newVer > curVer)
            {
                _strDownloadString = GetStringItems(1, strSource);

                privateWebClient.DownloadFileAsync(new Uri(_strDownloadString), Constants.StrUpdaterPath);
                privateWebClient.DownloadFileCompleted += privateWebClient_DownloadFileCompleted;
            }
        }

        void privateWebClient_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            /* Quick and dirty.. */
            if (File.Exists("Sc2Hack UpdateManager.exe"))
            {
                var fvInfo = FileVersionInfo.GetVersionInfo("Sc2Hack UpdateManager.exe");

                MethodInvoker inf = delegate
                    {
                        lblUpdaterApplication.Text = "[" + fvInfo.ProductName + "] - Ver.: " +
                                                     fvInfo.FileVersion;
                    };

                Invoke(inf);
            }
        }

        #endregion

       

        

        #endregion

       
    }
}
