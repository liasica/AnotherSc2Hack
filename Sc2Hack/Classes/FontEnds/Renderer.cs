using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Sc2Hack.Classes.BackEnds;

namespace Sc2Hack.Classes.FontEnds
{
    public partial class Renderer : Form
    {
        #region Variables 

        #region Private

        private readonly PredefinedTypes.RenderForm _rRenderForm = PredefinedTypes.RenderForm.Dummy;    //To check what renderform is called
        private readonly MainHandler _hMainHandler = null;                                              //Mainhandler - handles access to the Engine
        private Point _ptMousePosition = new Point(0,0);                                                //Position for the Moving of the Panel

        private Image _imgMinerals = Properties.Resources.Mineral_Protoss,
                      _imgGas = Properties.Resources.Gas_Protoss,
                      _imgSupply = Properties.Resources.Supply_Protoss,
                      _imgWorker = Properties.Resources.P_Probe;

        private Boolean _bChangingPosition = false;
        private Boolean _bDraw = true;
        private Boolean _bSurpressForeground = false;
        private Stopwatch _swMainWatch = new Stopwatch();
        private Boolean _bInvalidate = true;



        #endregion

        #region Public

        public PredefinedTypes.Gameinformation GInfo = new PredefinedTypes.Gameinformation();
        public PredefinedTypes.Map GMap = new PredefinedTypes.Map();
        public List<PredefinedTypes.Player> LPlayer = new List<PredefinedTypes.Player>();
        public List<PredefinedTypes.Unit> LUnit = new List<PredefinedTypes.Unit>();

        #endregion

        #endregion

        #region UnitCounter - Count all objects per player

        #region Terran

        List<PredefinedTypes.UnitCount> _lTbCommandCenter = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lTbPlanetaryFortress = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lTbOrbitalCommand = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lTbBarracks = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lTbSupply = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lTbEbay = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lTbRefinery = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lTbBunker = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lTbTurrent = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lTbSensorTower = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lTbFactory = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lTbStarport = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lTbArmory = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lTbGhostAcademy = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lTbFusionCore = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lTbTechlab = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lTbReactor = new List<PredefinedTypes.UnitCount>();


        List<PredefinedTypes.UnitCount> _lTuScv = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lTuMule = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lTuMarine = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lTuMarauder = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lTuReaper = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lTuGhost = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lTuWidowMine = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lTuSiegetank = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lTuHellion = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lTuHellbat = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lTuThor = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lTuViking = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lTuBanshee = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lTuMedivac = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lTuBattlecruiser = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lTuRaven = new List<PredefinedTypes.UnitCount>();

        #endregion

        #region Protoss

        List<PredefinedTypes.UnitCount> _lPbNexus = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lPbPylong = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lPbGateway = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lPbForge = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lPbCybercore = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lPbWarpgate = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lPbCannon = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lPbAssimilator = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lPbTwilight = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lPbStargate = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lPbRobotics = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lPbRoboticsSupport = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lPbFleetbeacon = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lPbTemplarArchives = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lPbDarkshrine = new List<PredefinedTypes.UnitCount>();

        List<PredefinedTypes.UnitCount> _lPuProbe = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lPuStalker = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lPuZealot = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lPuSentry = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lPuDt = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lPuHt = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lPuMothership = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lPuMothershipcore = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lPuArchon = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lPuWarpprism = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lPuObserver = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lPuColossus = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lPuImmortal = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lPuPhoenix = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lPuVoidray = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lPuOracle = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lPuTempest = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lPuCarrier = new List<PredefinedTypes.UnitCount>();

        #endregion

        #region Zerg

        List<PredefinedTypes.UnitCount> _lZbHatchery = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lZbLair = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lZbHive = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lZbSpawningpool = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lZbRoachwarren = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lZbEvochamber = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lZbSpine = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lZbSpore = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lZbBanelingnest = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lZbExtractor = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lZbHydraden = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lZbSpire = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lZbNydusbegin = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lZbNydusend = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lZbUltracavern = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lZbGreaterspire = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lZbInfestationpit = new List<PredefinedTypes.UnitCount>();

        List<PredefinedTypes.UnitCount> _lZuLarva = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lZuDrone = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lZuOverlord = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lZuZergling = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lZuBaneling = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lZuBanelingCocoon = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lZuBroodlordCocoon = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lZuRoach = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lZuHydra = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lZuInfestor = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lZuQueen = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lZuOverseer = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lZuOverseerCocoon = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lZuMutalisk = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lZuCorruptor = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lZuBroodlord = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lZuUltralist = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lZuSwarmhost = new List<PredefinedTypes.UnitCount>();
        List<PredefinedTypes.UnitCount> _lZuViper = new List<PredefinedTypes.UnitCount>();

        #endregion

        #region Images

        private readonly Image _imgTuScv = Properties.Resources.tu_scv,
                               _imgTuMule = Properties.Resources.tu_Mule,
                               _imgTuMarine = Properties.Resources.tu_marine,
                               _imgTuMarauder = Properties.Resources.tu_marauder,
                               _imgTuReaper = Properties.Resources.tu_reaper,
                               _imgTuGhost = Properties.Resources.tu_ghost,
                               _imgTuHellion = Properties.Resources.tu_hellion,
                               _imgTuHellbat = Properties.Resources.tu_battlehellion,
                               _imgTuSiegetank = Properties.Resources.tu_tank,
                               _imgTuThor = Properties.Resources.tu_thor,
                               _imgTuWidowMine = Properties.Resources.tu_widowmine,
                               _imgTuViking = Properties.Resources.tu_vikingAir,
                               _imgTuRaven = Properties.Resources.tu_raven,
                               _imgTuMedivac = Properties.Resources.tu_medivac,
                               _imgTuBattlecruiser = Properties.Resources.tu_battlecruiser,
                               _imgTuBanshee = Properties.Resources.tu_banshee;

        private readonly Image _imgTbCc = Properties.Resources.tb_cc,
                               _imgTbOc = Properties.Resources.tb_oc,
                               _imgTbPf = Properties.Resources.tb_pf,
                               _imgTbSupply = Properties.Resources.tb_supply,
                               _imgTbRefinery = Properties.Resources.tb_refinery,
                               _imgTbBarracks = Properties.Resources.tb_rax,
                               _imgTbEbay = Properties.Resources.tb_ebay,
                               _imgTbTurrent = Properties.Resources.tb_turret,
                               _imgTbSensorTower = Properties.Resources.tb_sensor,
                               _imgTbFactory = Properties.Resources.tb_fax,
                               _imgTbStarport = Properties.Resources.tb_starport,
                               _imgTbGhostacademy = Properties.Resources.tb_ghostacademy,
                               _imgTbArmory = Properties.Resources.tb_Armory,
                               _imgTbBunker = Properties.Resources.tb_bunker,
                               _imgTbFusioncore = Properties.Resources.tb_fusioncore,
                               _imgTbTechlab = Properties.Resources.tb_techlab,
                               _imgTbReactor = Properties.Resources.tb_reactor;

        private readonly Image _imgPuProbe = Properties.Resources.pu_probe,
                               _imgPuZealot = Properties.Resources.pu_Zealot,
                               _imgPuStalker = Properties.Resources.pu_Stalker,
                               _imgPuSentry = Properties.Resources.pu_sentry,
                               _imgPuDarkTemplar = Properties.Resources.pu_DarkTemplar,
                               _imgPuHighTemplar = Properties.Resources.pu_ht,
                               _imgPuColossus = Properties.Resources.pu_Colossus,
                               _imgPuImmortal = Properties.Resources.pu_immortal,
                               _imgPuWapprism = Properties.Resources.pu_warpprism,
                               _imgPuObserver = Properties.Resources.pu_Observer,
                               _imgPuOracle = Properties.Resources.pu_oracle,
                               _imgPuTempest = Properties.Resources.pu_tempest,
                               _imgPuPhoenix = Properties.Resources.pu_pheonix,
                               _imgPuVoidray = Properties.Resources.pu_Voidray,
                               _imgPuCarrier = Properties.Resources.pu_carrier,
                               _imgPuMothershipcore = Properties.Resources.pu_mothershipcore,
                               _imgPuMothership = Properties.Resources.pu_Mothership,
                               _imgPuArchon = Properties.Resources.pu_Archon;

        private readonly Image _imgPbNexus = Properties.Resources.pb_Nexus,
                               _imgPbPylon = Properties.Resources.pb_Pylon,
                               _imgPbGateway = Properties.Resources.pb_gateway,
                               _imgPbWarpgate = Properties.Resources.pb_warpgate,
                               _imgPbAssimilator = Properties.Resources.pb_Assimilator,
                               _imgPbForge = Properties.Resources.pb_forge,
                               _imgPbCannon = Properties.Resources.pb_Cannon,
                               _imgPbCybercore = Properties.Resources.pb_cybercore,
                               _imgPbStargate = Properties.Resources.pb_stargate,
                               _imgPbRobotics = Properties.Resources.pb_robotics,
                               _imgPbRoboticsSupport = Properties.Resources.pb_roboticssupport,
                               _imgPbTwillightCouncil = Properties.Resources.pb_twillightCouncil,
                               _imgPbDarkShrine = Properties.Resources.pb_DarkShrine,
                               _imgPbTemplarArchives = Properties.Resources.pb_templararchives,
                               _imgPbFleetBeacon = Properties.Resources.pb_FleetBeacon;

        private readonly Image _imgZuDrone = Properties.Resources.zu_drone,
                               _imgZuLarva = Properties.Resources.zu_larva,
                               _imgZuZergling = Properties.Resources.zu_zergling,
                               _imgZuBaneling = Properties.Resources.zu_baneling,
                               _imgZuBanelingCocoon = Properties.Resources.zu_banelingcocoon,
                               _imgZuRoach = Properties.Resources.zu_roach,
                               _imgZuHydra = Properties.Resources.zu_hydra,
                               _imgZuMutalisk = Properties.Resources.zu_mutalisk,
                               _imgZuUltra = Properties.Resources.zu_ultra,
                               _imgZuViper = Properties.Resources.zu_viper,
                               _imgZuSwarmhost = Properties.Resources.zu_swarmhost,
                               _imgZuInfestor = Properties.Resources.zu_infestor,
                               _imgZuCorruptor = Properties.Resources.zu_corruptor,
                               _imgZuBroodlord = Properties.Resources.zu_broodlord,
                               _imgZuBroodlordCocoon = Properties.Resources.zu_broodlordcocoon,
                               _imgZuQueen = Properties.Resources.zu_queen,
                               _imgZuOverlord = Properties.Resources.zu_overlord,
                               _imgZuOverseer = Properties.Resources.zu_overseer,
                               _imgZuOvserseerCocoon = Properties.Resources.zu_overseercocoon;

        private readonly Image _imgZbHatchery = Properties.Resources.zb_hatchery,
                               _imgZbLair = Properties.Resources.zb_lair,
                               _imgZbHive = Properties.Resources.zb_hive,
                               _imgZbSpawningpool = Properties.Resources.zb_spawningpool,
                               _imgZbExtractor = Properties.Resources.zb_extactor,
                               _imgZbEvochamber = Properties.Resources.zb_evochamber,
                               _imgZbSpinecrawler = Properties.Resources.zb_spine,
                               _imgZbSporecrawler = Properties.Resources.zb_spore,
                               _imgZbRoachwarren = Properties.Resources.zb_roachwarren,
                               _imgZbGreaterspire = Properties.Resources.zb_greaterspire,
                               _imgZbSpire = Properties.Resources.zb_spire,
                               _imgZbNydusNetwork = Properties.Resources.zb_nydusnetwork,
                               _imgZbNydusWorm = Properties.Resources.zb_nydusworm,
                               _imgZbHydraden = Properties.Resources.zb_hydraden,
                               _imgZbInfestationpit = Properties.Resources.zb_infestationpit,
                               _imgZbUltracavern = Properties.Resources.zb_ultracavery,
                               _imgZbBanelingnest = Properties.Resources.zb_banelingnest;


        #endregion

        #endregion

        public Renderer(PredefinedTypes.RenderForm rnd, MainHandler hnd)
        {
            _rRenderForm = rnd;
            _hMainHandler = hnd;

            SetStyle(ControlStyles.DoubleBuffer |
            ControlStyles.UserPaint |
            ControlStyles.OptimizedDoubleBuffer |
            ControlStyles.AllPaintingInWmPaint, true);

            InitializeComponent();

           
        }

        private void Renderer_2_Load(object sender, EventArgs e)
        {
            LoadPreferencesIntoControls();

            if (_rRenderForm.Equals(PredefinedTypes.RenderForm.ExportIdsToFile))
                ExportUnitIdsToFile();

            TopMost = true;

            BackColor = Color.FromArgb(255, 50, 50, 50);
            TransparencyKey = Color.FromArgb(255, 50, 50, 50);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var context = new BufferedGraphicsContext();
            context.MaximumBuffer = ClientSize;

            using (BufferedGraphics buffer = context.Allocate(e.Graphics, ClientRectangle))
            {
                buffer.Graphics.Clear(BackColor);
                buffer.Graphics.CompositingMode = CompositingMode.SourceOver;
                buffer.Graphics.CompositingQuality = CompositingQuality.HighSpeed;
                buffer.Graphics.PixelOffsetMode = PixelOffsetMode.HighSpeed;
                buffer.Graphics.SmoothingMode = SmoothingMode.HighSpeed;

                if (GInfo.IsIngame)
                {
                    if (_hMainHandler.PSettings.GlobalDrawOnlyInForeground && !_bSurpressForeground)
                    {
                        if (InteropCalls.GetForegroundWindow().Equals(_hMainHandler.PSc2Process.MainWindowHandle))
                            _bDraw = true;

                        else
                            _bDraw = false;
                    }

                    else
                    {
                        _bDraw = true;

                        if (InteropCalls.GetForegroundWindow().Equals(_hMainHandler.PSc2Process.MainWindowHandle))
                        {
                            InteropCalls.SetActiveWindow(Handle);
                        }
                    }

                    if (_bDraw)
                    {
                        if (_rRenderForm.Equals(PredefinedTypes.RenderForm.Maphack))
                            DrawMinimap(buffer);

                        else if (_rRenderForm.Equals(PredefinedTypes.RenderForm.Units))
                            DrawUnits(buffer);

                        else if (_rRenderForm.Equals(PredefinedTypes.RenderForm.Resources))
                            DrawResources(buffer);
                        

                        else if (_rRenderForm.Equals(PredefinedTypes.RenderForm.Income))
                            DrawIncome(buffer);

                        else if (_rRenderForm.Equals(PredefinedTypes.RenderForm.Army))
                            DrawArmy(buffer);

                        else if (_rRenderForm.Equals(PredefinedTypes.RenderForm.Apm))
                            DrawApm(buffer);

                        else if (_rRenderForm.Equals(PredefinedTypes.RenderForm.Worker))
                            DrawWorker(buffer);

                        else if (_rRenderForm.Equals(PredefinedTypes.RenderForm.Production))
                            DrawProductionPanel(buffer);
                    }

                }

                

                buffer.Render();
            }

            context.Dispose();
        }

        /* Counts the most common units */
        private void CountUnits(PredefinedTypes.TargetFilterFlag flag)
        {
            #region Clear the Lists

            #region Terran

            _lTbCommandCenter.Clear();
            _lTbOrbitalCommand.Clear();
            _lTbPlanetaryFortress.Clear();
            _lTbBarracks.Clear();
            _lTbSupply.Clear();
            _lTbRefinery.Clear();
            _lTbBunker.Clear();
            _lTbTurrent.Clear();
            _lTbSensorTower.Clear();
            _lTbEbay.Clear();
            _lTbStarport.Clear();
            _lTbFactory.Clear();
            _lTbArmory.Clear();
            _lTbFusionCore.Clear();
            _lTbGhostAcademy.Clear();
            _lTbReactor.Clear();
            _lTbTechlab.Clear();

            _lTuScv.Clear();
            _lTuMule.Clear();
            _lTuMarine.Clear();
            _lTuMarauder.Clear();
            _lTuReaper.Clear();
            _lTuGhost.Clear();
            _lTuWidowMine.Clear();
            _lTuSiegetank.Clear();
            _lTuHellbat.Clear();
            _lTuHellion.Clear();
            _lTuThor.Clear();
            _lTuBanshee.Clear();
            _lTuBattlecruiser.Clear();
            _lTuViking.Clear();
            _lTuRaven.Clear();
            _lTuMedivac.Clear();

            #endregion

            #region Protoss

            _lPbAssimilator.Clear();
            _lPbNexus.Clear();
            _lPbPylong.Clear();
            _lPbGateway.Clear();
            _lPbWarpgate.Clear();
            _lPbForge.Clear();
            _lPbCannon.Clear();
            _lPbTwilight.Clear();
            _lPbTemplarArchives.Clear();
            _lPbDarkshrine.Clear();
            _lPbRobotics.Clear();
            _lPbRoboticsSupport.Clear();
            _lPbFleetbeacon.Clear();
            _lPbCybercore.Clear();
            _lPbStargate.Clear();

            _lPuArchon.Clear();
            _lPuCarrier.Clear();
            _lPuColossus.Clear();
            _lPuDt.Clear();
            _lPuHt.Clear();
            _lPuImmortal.Clear();
            _lPuMothership.Clear();
            _lPuMothershipcore.Clear();
            _lPuObserver.Clear();
            _lPuOracle.Clear();
            _lPuPhoenix.Clear();
            _lPuProbe.Clear();
            _lPuSentry.Clear();
            _lPuStalker.Clear();
            _lPuTempest.Clear();
            _lPuVoidray.Clear();
            _lPuWarpprism.Clear();
            _lPuZealot.Clear();

            #endregion

            #region Zerg

            _lZbBanelingnest.Clear();
            _lZbEvochamber.Clear();
            _lZbExtractor.Clear();
            _lZbGreaterspire.Clear();
            _lZbHatchery.Clear();
            _lZbHive.Clear();
            _lZbHydraden.Clear();
            _lZbInfestationpit.Clear();
            _lZbLair.Clear();
            _lZbNydusbegin.Clear();
            _lZbNydusend.Clear();
            _lZbRoachwarren.Clear();
            _lZbSpawningpool.Clear();
            _lZbSpine.Clear();
            _lZbSpire.Clear();
            _lZbSpore.Clear();
            _lZbUltracavern.Clear();

            _lZuBaneling.Clear();
            _lZuBroodlord.Clear();
            _lZuCorruptor.Clear();
            _lZuDrone.Clear();
            _lZuHydra.Clear();
            _lZuBanelingCocoon.Clear();
            _lZuBroodlordCocoon.Clear();
            _lZuInfestor.Clear();
            _lZuLarva.Clear();
            _lZuMutalisk.Clear();
            _lZuOverlord.Clear();
            _lZuOverseer.Clear();
            _lZuQueen.Clear();
            _lZuRoach.Clear();
            _lZuSwarmhost.Clear();
            _lZuUltralist.Clear();
            _lZuViper.Clear();
            _lZuZergling.Clear();
            _lZuOverseerCocoon.Clear();

            #endregion

            #endregion


            for (var i = 0; i < LPlayer.Count; i++)
            {
                #region Define local variables

                

                #region Terran

                #region Structures

                var uTbcc = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uTboc = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uTbpf = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uTbRax = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uTbSupply = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uTbRefinery = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uTbBunker = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uTbTurrent = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uTbSensorTower = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uTbEbay = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uTbStarport = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uTbFactory = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uTbArmory = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uTbGhostAcademy = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uTbFusionCore = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uTbReactor = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uTbTechLab = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                #endregion

                #region Units

                var uTuScv = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uTuMule = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uTuMarine = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uTuMarauder = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uTuReaper = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uTuGhost = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uTuHellion = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uTuHellbat = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uTuSiegetank = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uTuWidowMine = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uTuThor = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uTuBanshee = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uTuMedivac = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uTuRaven = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uTuViking = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uTuBattlecruiser = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                #endregion

                #region Temporary values for the construction- state

                var fTbcc = new List<float>();
                var fTboc = new List<float>();
                var fTbpf = new List<float>();
                var fTbSupply = new List<float>();
                var fTbRefinery = new List<float>();
                var fTbRax = new List<float>();
                var fTbEbay = new List<float>();
                var fTbTurrent = new List<float>();
                var fTbBunker = new List<float>();
                var fTbSensorTower = new List<float>();
                var fTbStarport = new List<float>();
                var fTbFactory = new List<float>();
                var fTbGhostAcademy = new List<float>();
                var fTbFusionCore = new List<float>();
                var fTbArmory = new List<float>();
                var fTbTechlab = new List<float>();
                var fTbReactor = new List<float>();

                #endregion



                #endregion

                #region Protoss

                #region Structures

                var uPbNexus = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uPbPylon = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uPbGateway = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uPbAssimilator = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uPbForge = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uPbCybercore = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uPbCannon = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uPbTwilight = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uPbStargate = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uPbRobotics = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uPbRoboticsSupport = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uPbFleetBeacon = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uPbDarkShrine = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uPbTemplarArchives = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uPbWarpGate = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                #endregion

                #region Units

                var uPuProbe = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uPuZealot = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uPuStalker = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uPuSentry = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uPuHighTemplar = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uPuDarkTemplar = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uPuArchon = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uPuMothership = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uPuMothershipCore = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uPuVoidRay = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uPuPhoenix = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uPuOracle = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uPuCarrier = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uPuTempest = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uPuImmortal = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uPuObserver = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uPuWarpprism = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uPuColossus = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                #endregion

                #region Temporary values for the construction- state

                var fPbNexus = new List<float>();
                var fPbPylon = new List<float>();
                var fPbAssimilator = new List<float>();
                var fPbGateway = new List<float>();
                var fPbWarpgate = new List<float>();
                var fPbForge = new List<float>();
                var fPbCannon = new List<float>();
                var fPbCybercore = new List<float>();
                var fPbTwilight = new List<float>();
                var fPbDarkShrine = new List<float>();
                var fPbTemplarArchives = new List<float>();
                var fPbFleetBeacon = new List<float>();
                var fPbStargate = new List<float>();
                var fPbRobotics = new List<float>();
                var fPbRoboticsSupport = new List<float>();

                #endregion

                #endregion

                #region Zerg

                #region Structures

                var uZbHatchery = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uZbLiar = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uZbHive = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uZbSpawiningPool = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uZbEvolutionChamber = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uZbSpineCrawler = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uZbSporeCrawler = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uZbExtractor = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uZbBanelingNest = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uZbRoachWarren = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uZbSpire = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uZbGreaterSpire = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uZbUltraliskCavern = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uZbInfestationPit = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uZbHydraDen = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uZbNydusWorm = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uZbNydusNetwork = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                #endregion

                #region Units

                var uZuLarva = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uZuDrone = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uZuOverlord = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uZuOvserseer = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uZuZergling = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uZuBaneling = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uZuRoach = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uZuHydra = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uZuInfestor = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uZuMuta = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uZuUltra = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uZuViper = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uZuSwarmHost = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uZuCorruptor = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uZuBroodlord = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uZuOvserseerCocoon = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uZuBanelingCocoon = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uZuBroodlordCocoon = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                var uZuQueen = new PredefinedTypes.UnitCount
                    {
                        ConstructionState = 100f,
                        UnitAmount = 0,
                        UnitUnderConsruction = 0
                    };

                #endregion

                #region Temporary values for the construction- state

                var fZbHatchery = new List<float>();
                var fZbLair = new List<float>();
                var fZbHive = new List<float>();
                var fZbSpawingpool = new List<float>();
                var fZbExtractor = new List<float>();
                var fZbBanelingnest = new List<float>();
                var fZbRoachWarren = new List<float>();
                var fZbEvolutionChamber = new List<float>();
                var fZbSpineCrawler = new List<float>();
                var fZbSporeCrawler = new List<float>();
                var fZbSpire = new List<float>();
                var fZbGreaterSpire = new List<float>();
                var fZbInfestationPit = new List<float>();
                var fZbHydraDen = new List<float>();
                var fZbUltraliskCavern = new List<float>();
                var fZbNydusNetwork = new List<float>();
                var fZbNydusWorm = new List<float>();

                #endregion

                #endregion


                #endregion

                for (var j = 0; j < LUnit.Count; j++)
                {
                    if (i != LUnit[j].Owner)
                        continue;


                    #region Alive

                    if (LUnit[j].IsAlive)
                    {
                        #region Terran

                        #region Structures

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TbCcGround ||
                            LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TbCcAir)
                            uTbcc.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TbOrbitalAir ||
                            LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TbOrbitalGround)
                            uTboc.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TbRaxAir ||
                            LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TbBarracksGround)
                            uTbRax.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TbBunker)
                            uTbBunker.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TbTurret)
                            uTbTurrent.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TbRefinery)
                            uTbRefinery.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TbSensortower)
                            uTbSensorTower.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TbPlanetary)
                            uTbpf.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TbEbay)
                            uTbEbay.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TbFactoryAir ||
                            LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TbFactoryGround)
                            uTbFactory.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TbStarportAir ||
                            LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TbStarportGround)
                            uTbStarport.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TbSupplyGround ||
                            LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TbSupplyHidden)
                            uTbSupply.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TbGhostacademy)
                            uTbGhostAcademy.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TbFusioncore)
                            uTbFusionCore.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TbArmory)
                            uTbArmory.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TbTechlab ||
                            LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TbTechlabFactory ||
                            LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TbTechlabRax ||
                            LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TbTechlabStarport)
                            uTbTechLab.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TbReactor ||
                            LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TbReactorFactory ||
                            LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TbReactorRax ||
                            LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TbReactorStarport)
                            uTbReactor.UnitAmount++;

                        #endregion

                        #region Units

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TuScv)
                            uTuScv.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TuMule)
                            uTuMule.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TuMarine)
                            uTuMarine.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TuMarauder)
                            uTuMarauder.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TuReaper)
                            uTuReaper.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TuGhost)
                            uTuGhost.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TuWidowMine ||
                            LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TuWidowMineBurrow)
                            uTuWidowMine.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TuSiegetank ||
                            LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TuSiegetankSieged)
                            uTuSiegetank.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TuThor)
                            uTuThor.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TuHellbat)
                            uTuHellbat.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TuHellion)
                            uTuHellion.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TuBanshee)
                            uTuBanshee.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TuBattlecruiser)
                            uTuBattlecruiser.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TuMedivac)
                            uTuMedivac.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TuRaven)
                            uTuRaven.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TuVikingAir ||
                            LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TuVikingGround)
                            uTuViking.UnitAmount++;

                        #endregion

                        #endregion

                        #region Protoss

                        #region Structures

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PbNexus)
                            uPbNexus.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PbPylon)
                            uPbPylon.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PbAssimilator)
                            uPbAssimilator.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PbCannon)
                            uPbCannon.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PbCybercore)
                            uPbCybercore.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PbDarkshrine)
                            uPbDarkShrine.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PbFleetbeacon)
                            uPbFleetBeacon.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PbForge)
                            uPbForge.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PbGateway)
                            uPbGateway.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PbRoboticsbay)
                            uPbRobotics.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PbRoboticssupportbay)
                            uPbRoboticsSupport.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PbStargate)
                            uPbStargate.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PbTemplararchives)
                            uPbTemplarArchives.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PbTwilightcouncil)
                            uPbTwilight.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PbWarpgate)
                            uPbWarpGate.UnitAmount++;

                        #endregion

                        #region Units

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PuArchon)
                            uPuArchon.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PuCarrier)
                            uPuCarrier.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PuColossus)
                            uPuColossus.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PuDarktemplar)
                            uPuDarkTemplar.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PuHightemplar)
                            uPuHighTemplar.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PuImmortal)
                            uPuImmortal.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PuMothership)
                            uPuMothership.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PuMothershipCore)
                            uPuMothershipCore.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PuObserver)
                            uPuObserver.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PuOracle)
                            uPuOracle.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PuPhoenix)
                            uPuPhoenix.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PuProbe)
                            uPuProbe.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PuSentry)
                            uPuSentry.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PuStalker)
                            uPuStalker.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PuTempest)
                            uPuTempest.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PuVoidray)
                            uPuVoidRay.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PuWarpprismPhase ||
                            LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PuWarpprismTransport)
                            uPuWarpprism.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PuZealot)
                            uPuZealot.UnitAmount++;

                        #endregion

                        #endregion

                        #region Zerg

                        #region Structures

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZbBanelingNest)
                            uZbBanelingNest.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZbEvolutionChamber)
                            uZbEvolutionChamber.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZbExtractor)
                            uZbExtractor.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZbGreaterspire)
                            uZbGreaterSpire.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZbHatchery)
                            uZbHatchery.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZbHive)
                            uZbHive.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZbHydraDen)
                            uZbHydraDen.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZbInfestationPit)
                            uZbInfestationPit.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZbLiar)
                            uZbLiar.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZbNydusNetwork)
                            uZbNydusNetwork.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZbNydusWorm)
                            uZbNydusWorm.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZbRoachWarren)
                            uZbRoachWarren.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZbSpawningPool)
                            uZbSpawiningPool.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZbSpineCrawler ||
                            LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZbSpineCrawlerUnrooted)
                            uZbSpineCrawler.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZbSpire)
                            uZbSpire.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZbSporeCrawler ||
                            LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZbSporeCrawlerUnrooted)
                            uZbSporeCrawler.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZbUltraCavern)
                            uZbUltraliskCavern.UnitAmount++;

                        #endregion

                        #region Units

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZuBaneling ||
                            LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZuBanelingBurrow)
                            uZuBaneling.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZuBanelingCocoon)
                            uZuBanelingCocoon.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZuBroodlord)
                            uZuBroodlord.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZuBroodlordCocoon)
                            uZuBroodlordCocoon.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZuCorruptor)
                            uZuCorruptor.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZuDrone ||
                            LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZuDroneBurrow)
                            uZuDrone.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZuHydraBurrow ||
                            LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZuHydralisk)
                            uZuHydra.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZuInfestor ||
                            LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZuInfestorBurrow)
                            uZuInfestor.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZuLarva)
                            uZuLarva.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZuMutalisk)
                            uZuMuta.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZuOverlord)
                            uZuOverlord.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZuOverseer)
                            uZuOvserseer.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZuOverseerCocoon)
                            uZuOvserseerCocoon.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZuQueen ||
                            LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZuQueenBurrow)
                            uZuQueen.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZuRoach ||
                            LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZuRoachBurrow)
                            uZuRoach.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZuSwarmHost ||
                            LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZuSwarmHostBurrow)
                            uZuSwarmHost.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZuUltra ||
                            LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZuUltraBurrow)
                            uZuUltra.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZuViper)
                            uZuViper.UnitAmount++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZuZergling ||
                            LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZuZerglingBurrow)
                            uZuZergling.UnitAmount++;

                        #endregion

                        #endregion
                    }

                    #endregion

                    #region Under Construction

                    if (LUnit[j].IsUnderConstruction || LUnit[j].BuildingState == 512)
                    {
                        #region Terran

                        #region Structures

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TbCcGround ||
                            LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TbCcAir)
                        {
                            uTbcc.UnitUnderConsruction++;

                            var tmp = (float)Math.Round(((LUnit[j].CustomStruct.MaximumHealth - LUnit[j].DamageTaken) /
                                                          (float)LUnit[j].CustomStruct.MaximumHealth) * 100, 1);
                            fTbcc.Add(tmp);
                        }

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TbOrbitalAir ||
                            LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TbOrbitalGround)
                        {
                            uTboc.UnitUnderConsruction++;

                            var tmp = (float)Math.Round(((LUnit[j].CustomStruct.MaximumHealth - LUnit[j].DamageTaken) /
                                                          (float)LUnit[j].CustomStruct.MaximumHealth) * 100, 1);
                            fTboc.Add(tmp);
                        }

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TbRaxAir ||
                            LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TbBarracksGround)
                        {
                            uTbRax.UnitUnderConsruction++;

                            var tmp = (float)Math.Round(((LUnit[j].CustomStruct.MaximumHealth - LUnit[j].DamageTaken) /
                                                          (float)LUnit[j].CustomStruct.MaximumHealth) * 100, 1);
                            fTbRax.Add(tmp);
                        }

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TbBunker)
                        {
                            uTbBunker.UnitUnderConsruction++;

                            var tmp = (float)Math.Round(((LUnit[j].CustomStruct.MaximumHealth - LUnit[j].DamageTaken) /
                                                          (float)LUnit[j].CustomStruct.MaximumHealth) * 100, 1);
                            fTbBunker.Add(tmp);
                        }

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TbTurret)
                        {
                            uTbTurrent.UnitUnderConsruction++;

                            var tmp = (float)Math.Round(((LUnit[j].CustomStruct.MaximumHealth - LUnit[j].DamageTaken) /
                                                          (float)LUnit[j].CustomStruct.MaximumHealth) * 100, 1);
                            fTbTurrent.Add(tmp);
                        }

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TbRefinery)
                        {
                            uTbRefinery.UnitUnderConsruction++;

                            var tmp = (float)Math.Round(((LUnit[j].CustomStruct.MaximumHealth - LUnit[j].DamageTaken) /
                                                          (float)LUnit[j].CustomStruct.MaximumHealth) * 100, 1);
                            fTbRefinery.Add(tmp);
                        }

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TbSensortower)
                        {
                            uTbSensorTower.UnitUnderConsruction++;

                            var tmp = (float)Math.Round(((LUnit[j].CustomStruct.MaximumHealth - LUnit[j].DamageTaken) /
                                                          (float)LUnit[j].CustomStruct.MaximumHealth) * 100, 1);
                            fTbSensorTower.Add(tmp);
                        }

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TbPlanetary)
                        {
                            uTbpf.UnitUnderConsruction++;

                            var tmp = (float)Math.Round(((LUnit[j].CustomStruct.MaximumHealth - LUnit[j].DamageTaken) /
                                                          (float)LUnit[j].CustomStruct.MaximumHealth) * 100, 1);
                            fTbpf.Add(tmp);
                        }

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TbEbay)
                        {
                            uTbEbay.UnitUnderConsruction++;

                            var tmp = (float)Math.Round(((LUnit[j].CustomStruct.MaximumHealth - LUnit[j].DamageTaken) /
                                                          (float)LUnit[j].CustomStruct.MaximumHealth) * 100, 1);
                            fTbEbay.Add(tmp);
                        }

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TbFactoryAir ||
                            LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TbFactoryGround)
                        {
                            uTbFactory.UnitUnderConsruction++;

                            var tmp = (float)Math.Round(((LUnit[j].CustomStruct.MaximumHealth - LUnit[j].DamageTaken) /
                                                          (float)LUnit[j].CustomStruct.MaximumHealth) * 100, 1);
                            fTbFactory.Add(tmp);
                        }

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TbStarportAir ||
                            LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TbStarportGround)
                        {
                            uTbStarport.UnitUnderConsruction++;

                            var tmp = (float)Math.Round(((LUnit[j].CustomStruct.MaximumHealth - LUnit[j].DamageTaken) /
                                                          (float)LUnit[j].CustomStruct.MaximumHealth) * 100, 1);
                            fTbStarport.Add(tmp);
                        }

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TbSupplyGround ||
                            LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TbSupplyHidden)
                        {
                            uTbSupply.UnitUnderConsruction++;

                            var tmp = (float) Math.Round(((LUnit[j].CustomStruct.MaximumHealth - LUnit[j].DamageTaken)/
                                                          (float) LUnit[j].CustomStruct.MaximumHealth)*100, 1);
                            fTbSupply.Add(tmp);
                        }

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TbGhostacademy)
                        {
                            uTbGhostAcademy.UnitUnderConsruction++;

                            var tmp = (float)Math.Round(((LUnit[j].CustomStruct.MaximumHealth - LUnit[j].DamageTaken) /
                                                          (float)LUnit[j].CustomStruct.MaximumHealth) * 100, 1);
                            fTbGhostAcademy.Add(tmp);
                        }

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TbFusioncore)
                        {
                            uTbFusionCore.UnitUnderConsruction++;

                            var tmp = (float)Math.Round(((LUnit[j].CustomStruct.MaximumHealth - LUnit[j].DamageTaken) /
                                                          (float)LUnit[j].CustomStruct.MaximumHealth) * 100, 1);
                            fTbFusionCore.Add(tmp);
                        }

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TbArmory)
                        {
                            uTbArmory.UnitUnderConsruction++;

                            var tmp = (float)Math.Round(((LUnit[j].CustomStruct.MaximumHealth - LUnit[j].DamageTaken) /
                                                          (float)LUnit[j].CustomStruct.MaximumHealth) * 100, 1);
                            fTbArmory.Add(tmp);
                        }

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TbTechlab ||
                            LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TbTechlabFactory ||
                            LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TbTechlabRax ||
                            LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TbTechlabStarport)
                        {
                            uTbTechLab.UnitUnderConsruction++;

                            var tmp = (float)Math.Round(((LUnit[j].CustomStruct.MaximumHealth - LUnit[j].DamageTaken) /
                                                          (float)LUnit[j].CustomStruct.MaximumHealth) * 100, 1);
                            fTbTechlab.Add(tmp);
                        }

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TbReactor ||
                            LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TbReactorFactory ||
                            LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TbReactorRax ||
                            LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TbReactorStarport)
                        {
                            uTbReactor.UnitUnderConsruction++;

                            var tmp = (float)Math.Round(((LUnit[j].CustomStruct.MaximumHealth - LUnit[j].DamageTaken) /
                                                          (float)LUnit[j].CustomStruct.MaximumHealth) * 100, 1);
                            fTbReactor.Add(tmp);
                        }

                        #endregion

                        #region Units

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TuScv)
                            uTuScv.UnitUnderConsruction++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TuMule)
                            uTuMule.UnitUnderConsruction++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TuMarine)
                            uTuMarine.UnitUnderConsruction++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TuMarauder)
                            uTuMarauder.UnitUnderConsruction++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TuReaper)
                            uTuReaper.UnitUnderConsruction++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TuGhost)
                            uTuGhost.UnitUnderConsruction++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TuWidowMine ||
                            LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TuWidowMineBurrow)
                            uTuWidowMine.UnitUnderConsruction++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TuSiegetank ||
                            LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TuSiegetankSieged)
                            uTuSiegetank.UnitUnderConsruction++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TuThor)
                            uTuThor.UnitUnderConsruction++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TuHellbat)
                            uTuHellbat.UnitUnderConsruction++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TuHellion)
                            uTuHellion.UnitUnderConsruction++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TuBanshee)
                            uTuBanshee.UnitUnderConsruction++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TuBattlecruiser)
                            uTuBattlecruiser.UnitUnderConsruction++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TuMedivac)
                            uTuMedivac.UnitUnderConsruction++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TuRaven)
                            uTuRaven.UnitUnderConsruction++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TuVikingAir ||
                            LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.TuVikingGround)
                            uTuViking.UnitUnderConsruction++;



                        #endregion

                        #endregion

                        #region Protoss

                        #region Structures

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PbNexus)
                        {
                            uPbNexus.UnitUnderConsruction++;

                            var tmp = (float)Math.Round(((LUnit[j].CustomStruct.MaximumHealth - LUnit[j].DamageTaken) /
                                                          (float)LUnit[j].CustomStruct.MaximumHealth) * 100, 1);
                            fPbNexus.Add(tmp);
                        }

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PbPylon)
                        {
                            uPbPylon.UnitUnderConsruction++;

                            var tmp = (float)Math.Round(((LUnit[j].CustomStruct.MaximumHealth - LUnit[j].DamageTaken) /
                                                          (float)LUnit[j].CustomStruct.MaximumHealth) * 100, 1);
                            fPbPylon.Add(tmp);
                        }

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PbAssimilator)
                        {
                            uPbAssimilator.UnitUnderConsruction++;

                            var tmp = (float)Math.Round(((LUnit[j].CustomStruct.MaximumHealth - LUnit[j].DamageTaken) /
                                                          (float)LUnit[j].CustomStruct.MaximumHealth) * 100, 1);
                            fPbAssimilator.Add(tmp);
                        }

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PbCannon)
                        {
                            uPbCannon.UnitUnderConsruction++;

                            var tmp = (float)Math.Round(((LUnit[j].CustomStruct.MaximumHealth - LUnit[j].DamageTaken) /
                                                          (float)LUnit[j].CustomStruct.MaximumHealth) * 100, 1);
                            fPbCannon.Add(tmp);
                        }

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PbCybercore)
                        {
                            uPbCybercore.UnitUnderConsruction++;

                            var tmp = (float)Math.Round(((LUnit[j].CustomStruct.MaximumHealth - LUnit[j].DamageTaken) /
                                                          (float)LUnit[j].CustomStruct.MaximumHealth) * 100, 1);
                            fPbCybercore.Add(tmp);
                        }

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PbDarkshrine)
                        {
                            uPbDarkShrine.UnitUnderConsruction++;
                            
                            var tmp = (float)Math.Round(((LUnit[j].CustomStruct.MaximumHealth - LUnit[j].DamageTaken) /
                                                          (float)LUnit[j].CustomStruct.MaximumHealth) * 100, 1);
                            fPbDarkShrine.Add(tmp);
                        
                        }

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PbFleetbeacon)
                        {
                            uPbFleetBeacon.UnitUnderConsruction++;

                            var tmp = (float)Math.Round(((LUnit[j].CustomStruct.MaximumHealth - LUnit[j].DamageTaken) /
                                                          (float)LUnit[j].CustomStruct.MaximumHealth) * 100, 1);
                            fPbFleetBeacon.Add(tmp);
                        }

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PbForge)
                        {
                            uPbForge.UnitUnderConsruction++;

                            var tmp = (float)Math.Round(((LUnit[j].CustomStruct.MaximumHealth - LUnit[j].DamageTaken) /
                                                          (float)LUnit[j].CustomStruct.MaximumHealth) * 100, 1);
                            fPbForge.Add(tmp);
                        }

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PbGateway)
                        {
                            uPbGateway.UnitUnderConsruction++;

                            var tmp = (float)Math.Round(((LUnit[j].CustomStruct.MaximumHealth - LUnit[j].DamageTaken) /
                                                          (float)LUnit[j].CustomStruct.MaximumHealth) * 100, 1);
                            fPbGateway.Add(tmp);
                        }

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PbRoboticsbay)
                        {
                            uPbRobotics.UnitUnderConsruction++;

                            var tmp = (float)Math.Round(((LUnit[j].CustomStruct.MaximumHealth - LUnit[j].DamageTaken) /
                                                          (float)LUnit[j].CustomStruct.MaximumHealth) * 100, 1);
                            fPbRobotics.Add(tmp);
                        }

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PbRoboticssupportbay)
                        {
                            uPbRoboticsSupport.UnitUnderConsruction++;
                            
                            var tmp = (float)Math.Round(((LUnit[j].CustomStruct.MaximumHealth - LUnit[j].DamageTaken) /
                                                          (float)LUnit[j].CustomStruct.MaximumHealth) * 100, 1);
                            fPbRoboticsSupport.Add(tmp);
                        }

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PbStargate)
                        {
                            uPbStargate.UnitUnderConsruction++;

                            var tmp = (float)Math.Round(((LUnit[j].CustomStruct.MaximumHealth - LUnit[j].DamageTaken) /
                                                          (float)LUnit[j].CustomStruct.MaximumHealth) * 100, 1);
                            fPbStargate.Add(tmp);
                        }

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PbTemplararchives)
                        {
                            uPbTemplarArchives.UnitUnderConsruction++;

                            var tmp = (float)Math.Round(((LUnit[j].CustomStruct.MaximumHealth - LUnit[j].DamageTaken) /
                                                          (float)LUnit[j].CustomStruct.MaximumHealth) * 100, 1);
                            fPbTemplarArchives.Add(tmp);
                        }

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PbTwilightcouncil)
                        {
                            uPbTwilight.UnitUnderConsruction++;

                            var tmp = (float)Math.Round(((LUnit[j].CustomStruct.MaximumHealth - LUnit[j].DamageTaken) /
                                                          (float)LUnit[j].CustomStruct.MaximumHealth) * 100, 1);
                            fPbTwilight.Add(tmp);
                        }

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PbWarpgate)
                        {
                            uPbWarpGate.UnitUnderConsruction++;

                            var tmp = (float)Math.Round(((LUnit[j].CustomStruct.MaximumHealth - LUnit[j].DamageTaken) /
                                                          (float)LUnit[j].CustomStruct.MaximumHealth) * 100, 1);
                            fPbWarpgate.Add(tmp);
                        }

                        #endregion

                        #region Units

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PuArchon)
                            uPuArchon.UnitUnderConsruction++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PuCarrier)
                            uPuCarrier.UnitUnderConsruction++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PuColossus)
                            uPuColossus.UnitUnderConsruction++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PuDarktemplar)
                            uPuDarkTemplar.UnitUnderConsruction++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PuHightemplar)
                            uPuHighTemplar.UnitUnderConsruction++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PuImmortal)
                            uPuImmortal.UnitUnderConsruction++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PuMothership)
                            uPuMothership.UnitUnderConsruction++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PuMothershipCore)
                            uPuMothershipCore.UnitUnderConsruction++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PuObserver)
                            uPuObserver.UnitUnderConsruction++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PuOracle)
                            uPuOracle.UnitUnderConsruction++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PuPhoenix)
                            uPuPhoenix.UnitUnderConsruction++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PuProbe)
                            uPuProbe.UnitUnderConsruction++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PuSentry)
                            uPuSentry.UnitUnderConsruction++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PuStalker)
                            uPuStalker.UnitUnderConsruction++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PuTempest)
                            uPuTempest.UnitUnderConsruction++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PuVoidray)
                            uPuVoidRay.UnitUnderConsruction++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PuWarpprismPhase ||
                            LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PuWarpprismTransport)
                            uPuWarpprism.UnitUnderConsruction++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.PuZealot)
                            uPuZealot.UnitUnderConsruction++;

                        #endregion

                        #endregion

                        #region Zerg

                        #region Structures

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZbBanelingNest)
                        {
                            uZbBanelingNest.UnitUnderConsruction++;

                            var tmp = (float)Math.Round(((LUnit[j].CustomStruct.MaximumHealth - LUnit[j].DamageTaken) /
                                                          (float)LUnit[j].CustomStruct.MaximumHealth) * 100, 1);
                            fZbBanelingnest.Add(tmp);
                        }

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZbEvolutionChamber)
                        {
                            uZbEvolutionChamber.UnitUnderConsruction++;

                            var tmp = (float)Math.Round(((LUnit[j].CustomStruct.MaximumHealth - LUnit[j].DamageTaken) /
                                                          (float)LUnit[j].CustomStruct.MaximumHealth) * 100, 1);
                            fZbEvolutionChamber.Add(tmp);
                        }

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZbExtractor)
                        {
                            uZbExtractor.UnitUnderConsruction++;

                            var tmp = (float)Math.Round(((LUnit[j].CustomStruct.MaximumHealth - LUnit[j].DamageTaken) /
                                                          (float)LUnit[j].CustomStruct.MaximumHealth) * 100, 1);
                            fZbExtractor.Add(tmp);
                        }

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZbGreaterspire)
                        {
                            uZbGreaterSpire.UnitUnderConsruction++;

                            var tmp = (float)Math.Round(((LUnit[j].CustomStruct.MaximumHealth - LUnit[j].DamageTaken) /
                                                          (float)LUnit[j].CustomStruct.MaximumHealth) * 100, 1);
                            fZbGreaterSpire.Add(tmp);
                        }

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZbHatchery)
                        {
                            uZbHatchery.UnitUnderConsruction++;

                            var tmp = (float)Math.Round(((LUnit[j].CustomStruct.MaximumHealth - LUnit[j].DamageTaken) /
                                                          (float)LUnit[j].CustomStruct.MaximumHealth) * 100, 1);
                            fZbHatchery.Add(tmp);
                        }

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZbHive)
                        {
                            uZbHive.UnitUnderConsruction++;

                            var tmp = (float)Math.Round(((LUnit[j].CustomStruct.MaximumHealth - LUnit[j].DamageTaken) /
                                                          (float)LUnit[j].CustomStruct.MaximumHealth) * 100, 1);
                            fZbHive.Add(tmp);
                        }

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZbHydraDen)
                        {
                            uZbHydraDen.UnitUnderConsruction++;

                            var tmp = (float)Math.Round(((LUnit[j].CustomStruct.MaximumHealth - LUnit[j].DamageTaken) /
                                                          (float)LUnit[j].CustomStruct.MaximumHealth) * 100, 1);
                            fZbHydraDen.Add(tmp);
                        }

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZbInfestationPit)
                        {
                            uZbInfestationPit.UnitUnderConsruction++;

                            var tmp = (float)Math.Round(((LUnit[j].CustomStruct.MaximumHealth - LUnit[j].DamageTaken) /
                                                          (float)LUnit[j].CustomStruct.MaximumHealth) * 100, 1);
                            fZbInfestationPit.Add(tmp);
                        }

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZbLiar)
                        {
                            uZbLiar.UnitUnderConsruction++;

                            var tmp = (float)Math.Round(((LUnit[j].CustomStruct.MaximumHealth - LUnit[j].DamageTaken) /
                                                          (float)LUnit[j].CustomStruct.MaximumHealth) * 100, 1);
                            fZbLair.Add(tmp);
                        }

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZbNydusNetwork)
                        {
                            uZbNydusNetwork.UnitUnderConsruction++;

                            var tmp = (float)Math.Round(((LUnit[j].CustomStruct.MaximumHealth - LUnit[j].DamageTaken) /
                                                          (float)LUnit[j].CustomStruct.MaximumHealth) * 100, 1);
                            fZbNydusNetwork.Add(tmp);
                        }

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZbNydusWorm)
                        {
                            uZbNydusWorm.UnitUnderConsruction++;

                            var tmp = (float)Math.Round(((LUnit[j].CustomStruct.MaximumHealth - LUnit[j].DamageTaken) /
                                                          (float)LUnit[j].CustomStruct.MaximumHealth) * 100, 1);
                            fZbNydusWorm.Add(tmp);
                        }

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZbRoachWarren)
                        {
                            uZbRoachWarren.UnitUnderConsruction++;

                            var tmp = (float)Math.Round(((LUnit[j].CustomStruct.MaximumHealth - LUnit[j].DamageTaken) /
                                                          (float)LUnit[j].CustomStruct.MaximumHealth) * 100, 1);
                            fZbRoachWarren.Add(tmp);
                        }

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZbSpawningPool)
                        {
                            uZbSpawiningPool.UnitUnderConsruction++;

                            var tmp = (float)Math.Round(((LUnit[j].CustomStruct.MaximumHealth - LUnit[j].DamageTaken) /
                                                          (float)LUnit[j].CustomStruct.MaximumHealth) * 100, 1);
                            fZbSpawingpool.Add(tmp);
                        }

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZbSpineCrawler ||
                            LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZbSpineCrawlerUnrooted)
                        {
                            uZbSpineCrawler.UnitUnderConsruction++;

                            var tmp = (float)Math.Round(((LUnit[j].CustomStruct.MaximumHealth - LUnit[j].DamageTaken) /
                                                          (float)LUnit[j].CustomStruct.MaximumHealth) * 100, 1);
                            fZbSpineCrawler.Add(tmp);
                        }

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZbSpire)
                        {
                            uZbSpire.UnitUnderConsruction++;

                            var tmp = (float)Math.Round(((LUnit[j].CustomStruct.MaximumHealth - LUnit[j].DamageTaken) /
                                                          (float)LUnit[j].CustomStruct.MaximumHealth) * 100, 1);
                            fZbSpire.Add(tmp);
                        }

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZbSporeCrawler ||
                            LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZbSporeCrawlerUnrooted)
                        {
                            uZbSporeCrawler.UnitUnderConsruction++;

                            var tmp = (float)Math.Round(((LUnit[j].CustomStruct.MaximumHealth - LUnit[j].DamageTaken) /
                                                          (float)LUnit[j].CustomStruct.MaximumHealth) * 100, 1);
                            fZbSporeCrawler.Add(tmp);
                        }

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZbUltraCavern)
                        {
                            uZbUltraliskCavern.UnitUnderConsruction++;

                            var tmp = (float)Math.Round(((LUnit[j].CustomStruct.MaximumHealth - LUnit[j].DamageTaken) /
                                                          (float)LUnit[j].CustomStruct.MaximumHealth) * 100, 1);
                            fZbUltraliskCavern.Add(tmp);
                        }

                        #endregion

                        #region Units

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZuBaneling ||
                            LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZuBanelingBurrow)
                            uZuBaneling.UnitUnderConsruction++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZuBanelingCocoon)
                            uZuBanelingCocoon.UnitUnderConsruction++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZuBroodlord)
                            uZuBroodlord.UnitUnderConsruction++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZuBroodlordCocoon)
                            uZuBroodlordCocoon.UnitUnderConsruction++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZuCorruptor)
                            uZuCorruptor.UnitUnderConsruction++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZuDrone ||
                            LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZuDroneBurrow)
                            uZuDrone.UnitUnderConsruction++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZuHydraBurrow ||
                            LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZuHydralisk)
                            uZuHydra.UnitUnderConsruction++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZuInfestor ||
                            LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZuInfestorBurrow)
                            uZuInfestor.UnitUnderConsruction++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZuLarva)
                            uZuLarva.UnitUnderConsruction++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZuMutalisk)
                            uZuMuta.UnitUnderConsruction++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZuOverlord)
                            uZuOverlord.UnitUnderConsruction++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZuOverseer)
                            uZuOvserseer.UnitUnderConsruction++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZuOverseerCocoon)
                            uZuOvserseerCocoon.UnitUnderConsruction++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZuQueen ||
                            LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZuQueenBurrow)
                            uZuQueen.UnitUnderConsruction++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZuRoach ||
                            LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZuRoachBurrow)
                            uZuRoach.UnitUnderConsruction++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZuSwarmHost ||
                            LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZuSwarmHostBurrow)
                            uZuSwarmHost.UnitUnderConsruction++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZuUltra ||
                            LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZuUltraBurrow)
                            uZuUltra.UnitUnderConsruction++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZuViper)
                            uZuViper.UnitUnderConsruction++;

                        if (LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZuZergling ||
                            LUnit[j].CustomStruct.Id == (Int32) PredefinedTypes.UnitId.ZuZerglingBurrow)
                            uZuZergling.UnitUnderConsruction++;

                        #endregion

                        #endregion
                    }

                    #endregion

                }

                #region Map Construction values

                #region Terran

                uTbcc.ConstructionState = SortAndMapConstructionStates(fTbcc);
                uTboc.ConstructionState = SortAndMapConstructionStates(fTboc);
                uTbpf.ConstructionState = SortAndMapConstructionStates(fTbpf);
                uTbRax.ConstructionState = SortAndMapConstructionStates(fTbRax);
                uTbRefinery.ConstructionState = SortAndMapConstructionStates(fTbRefinery);
                uTbBunker.ConstructionState = SortAndMapConstructionStates(fTbBunker);
                uTbTurrent.ConstructionState = SortAndMapConstructionStates(fTbTurrent);
                uTbSensorTower.ConstructionState = SortAndMapConstructionStates(fTbSensorTower);
                uTbEbay.ConstructionState = SortAndMapConstructionStates(fTbEbay);
                uTbSupply.ConstructionState = SortAndMapConstructionStates(fTbSupply);
                uTbFactory.ConstructionState = SortAndMapConstructionStates(fTbFactory);
                uTbStarport.ConstructionState = SortAndMapConstructionStates(fTbStarport);
                uTbArmory.ConstructionState = SortAndMapConstructionStates(fTbArmory);
                uTbFusionCore.ConstructionState = SortAndMapConstructionStates(fTbFusionCore);
                uTbGhostAcademy.ConstructionState = SortAndMapConstructionStates(fTbGhostAcademy);
                uTbReactor.ConstructionState = SortAndMapConstructionStates(fTbReactor);
                uTbTechLab.ConstructionState = SortAndMapConstructionStates(fTbTechlab);

                #endregion

                #region Protoss

                uPbNexus.ConstructionState = SortAndMapConstructionStates(fPbNexus);
                uPbPylon.ConstructionState = SortAndMapConstructionStates(fPbPylon);
                uPbAssimilator.ConstructionState = SortAndMapConstructionStates(fPbAssimilator);
                uPbGateway.ConstructionState = SortAndMapConstructionStates(fPbGateway);
                uPbWarpGate.ConstructionState = SortAndMapConstructionStates(fPbWarpgate);
                uPbCybercore.ConstructionState = SortAndMapConstructionStates(fPbCybercore);
                uPbForge.ConstructionState = SortAndMapConstructionStates(fPbForge);
                uPbCannon.ConstructionState = SortAndMapConstructionStates(fPbCannon);
                uPbTwilight.ConstructionState = SortAndMapConstructionStates(fPbTwilight);
                uPbTemplarArchives.ConstructionState = SortAndMapConstructionStates(fPbTemplarArchives);
                uPbDarkShrine.ConstructionState = SortAndMapConstructionStates(fPbDarkShrine);
                uPbFleetBeacon.ConstructionState = SortAndMapConstructionStates(fPbFleetBeacon);
                uPbStargate.ConstructionState = SortAndMapConstructionStates(fPbStargate);
                uPbRobotics.ConstructionState = SortAndMapConstructionStates(fPbRobotics);
                uPbRoboticsSupport.ConstructionState = SortAndMapConstructionStates(fPbRoboticsSupport);


                #endregion

                #region Zerg

                uZbHatchery.ConstructionState = SortAndMapConstructionStates(fZbHatchery);
                uZbLiar.ConstructionState = SortAndMapConstructionStates(fZbLair);
                uZbHive.ConstructionState = SortAndMapConstructionStates(fZbHive);
                uZbExtractor.ConstructionState = SortAndMapConstructionStates(fZbExtractor);
                uZbSpawiningPool.ConstructionState = SortAndMapConstructionStates(fZbSpawingpool);
                uZbBanelingNest.ConstructionState = SortAndMapConstructionStates(fZbBanelingnest);
                uZbRoachWarren.ConstructionState = SortAndMapConstructionStates(fZbRoachWarren);
                uZbEvolutionChamber.ConstructionState = SortAndMapConstructionStates(fZbEvolutionChamber);
                uZbSpineCrawler.ConstructionState = SortAndMapConstructionStates(fZbSpineCrawler);
                uZbSporeCrawler.ConstructionState = SortAndMapConstructionStates(fZbSporeCrawler);
                uZbHydraDen.ConstructionState = SortAndMapConstructionStates(fZbHydraDen);
                uZbInfestationPit.ConstructionState = SortAndMapConstructionStates(fZbInfestationPit);
                uZbSpire.ConstructionState = SortAndMapConstructionStates(fZbSpire);
                uZbUltraliskCavern.ConstructionState = SortAndMapConstructionStates(fZbUltraliskCavern);
                uZbGreaterSpire.ConstructionState = SortAndMapConstructionStates(fZbGreaterSpire);
                uZbNydusNetwork.ConstructionState = SortAndMapConstructionStates(fZbNydusNetwork);
                uZbNydusWorm.ConstructionState = SortAndMapConstructionStates(fZbNydusWorm);

                #endregion

                #endregion

                #region Map the Values to the Lists

                #region Terran

                _lTbCommandCenter.Add(uTbcc);
                _lTbOrbitalCommand.Add(uTboc);
                _lTbPlanetaryFortress.Add(uTbpf);
                _lTbBarracks.Add(uTbRax);
                _lTbSupply.Add(uTbSupply);
                _lTbRefinery.Add(uTbRefinery);
                _lTbBunker.Add(uTbBunker);
                _lTbTurrent.Add(uTbTurrent);
                _lTbSensorTower.Add(uTbSensorTower);
                _lTbEbay.Add(uTbEbay);
                _lTbStarport.Add(uTbStarport);
                _lTbFactory.Add(uTbFactory);
                _lTbArmory.Add(uTbArmory);
                _lTbFusionCore.Add(uTbFusionCore);
                _lTbGhostAcademy.Add(uTbGhostAcademy);
                _lTbTechlab.Add(uTbTechLab);
                _lTbReactor.Add(uTbReactor);

                _lTuScv.Add(uTuScv);
                _lTuMule.Add(uTuMule);
                _lTuMarine.Add(uTuMarine);
                _lTuMarauder.Add(uTuMarauder);
                _lTuReaper.Add(uTuReaper);
                _lTuGhost.Add(uTuGhost);
                _lTuWidowMine.Add(uTuWidowMine);
                _lTuSiegetank.Add(uTuSiegetank);
                _lTuHellbat.Add(uTuHellbat);
                _lTuHellion.Add(uTuHellion);
                _lTuThor.Add(uTuThor);
                _lTuBanshee.Add(uTuBanshee);
                _lTuBattlecruiser.Add(uTuBattlecruiser);
                _lTuViking.Add(uTuViking);
                _lTuRaven.Add(uTuRaven);
                _lTuMedivac.Add(uTuMedivac);

                #endregion

                #region Protoss

                _lPbAssimilator.Add(uPbAssimilator);
                _lPbNexus.Add(uPbNexus);
                _lPbPylong.Add(uPbPylon);
                _lPbGateway.Add(uPbGateway);
                _lPbWarpgate.Add(uPbWarpGate);
                _lPbStargate.Add(uPbStargate);
                _lPbCybercore.Add(uPbCybercore);
                _lPbForge.Add(uPbForge);
                _lPbCannon.Add(uPbCannon);
                _lPbTwilight.Add(uPbTwilight);
                _lPbTemplarArchives.Add(uPbTemplarArchives);
                _lPbDarkshrine.Add(uPbDarkShrine);
                _lPbRobotics.Add(uPbRobotics);
                _lPbRoboticsSupport.Add(uPbRoboticsSupport);
                _lPbFleetbeacon.Add(uPbFleetBeacon);

                _lPuArchon.Add(uPuArchon);
                _lPuCarrier.Add(uPuCarrier);
                _lPuColossus.Add(uPuColossus);
                _lPuDt.Add(uPuDarkTemplar);
                _lPuHt.Add(uPuHighTemplar);
                _lPuImmortal.Add(uPuImmortal);
                _lPuMothership.Add(uPuMothership);
                _lPuMothershipcore.Add(uPuMothershipCore);
                _lPuObserver.Add(uPuObserver);
                _lPuOracle.Add(uPuOracle);
                _lPuPhoenix.Add(uPuPhoenix);
                _lPuProbe.Add(uPuProbe);
                _lPuSentry.Add(uPuSentry);
                _lPuStalker.Add(uPuStalker);
                _lPuTempest.Add(uPuTempest);
                _lPuVoidray.Add(uPuVoidRay);
                _lPuWarpprism.Add(uPuWarpprism);
                _lPuZealot.Add(uPuZealot);


                #endregion

                #region Zerg

                _lZbBanelingnest.Add(uZbBanelingNest);
                _lZbEvochamber.Add(uZbEvolutionChamber);
                _lZbExtractor.Add(uZbExtractor);
                _lZbGreaterspire.Add(uZbGreaterSpire);
                _lZbHatchery.Add(uZbHatchery);
                _lZbHive.Add(uZbHive);
                _lZbHydraden.Add(uZbHydraDen);
                _lZbInfestationpit.Add(uZbInfestationPit);
                _lZbLair.Add(uZbLiar);
                _lZbNydusbegin.Add(uZbNydusNetwork);
                _lZbNydusend.Add(uZbNydusWorm);
                _lZbRoachwarren.Add(uZbRoachWarren);
                _lZbSpawningpool.Add(uZbSpawiningPool);
                _lZbSpine.Add(uZbSpineCrawler);
                _lZbSpire.Add(uZbSpire);
                _lZbSpore.Add(uZbSporeCrawler);
                _lZbUltracavern.Add(uZbUltraliskCavern);

                _lZuBaneling.Add(uZuBaneling);
                _lZuBanelingCocoon.Add(uZuBanelingCocoon);
                _lZuBroodlord.Add(uZuBroodlord);
                _lZuBroodlordCocoon.Add(uZuBroodlordCocoon);
                _lZuCorruptor.Add(uZuCorruptor);
                _lZuDrone.Add(uZuDrone);
                _lZuHydra.Add(uZuHydra);
                _lZuInfestor.Add(uZuInfestor);
                _lZuLarva.Add(uZuLarva);
                _lZuMutalisk.Add(uZuMuta);
                _lZuOverlord.Add(uZuOverlord);
                _lZuOverseer.Add(uZuOvserseer);
                _lZuOverseerCocoon.Add(uZuOvserseerCocoon);
                _lZuQueen.Add(uZuQueen);
                _lZuRoach.Add(uZuRoach);
                _lZuSwarmhost.Add(uZuSwarmHost);
                _lZuUltralist.Add(uZuUltra);
                _lZuViper.Add(uZuViper);
                _lZuZergling.Add(uZuZergling);

                #endregion

                #endregion

            }
        }

        /* Help method for the CountUnits (Sort and mapping) */
        private float SortAndMapConstructionStates(List<float> fStructure)
        {
            if (fStructure.Count > 0)
            {
                fStructure.Sort();
                return fStructure[fStructure.Count - 1];
            }

            return 100f;
        }

        /* Gameheart stuff */
        private Boolean CheckIfGameheart(PredefinedTypes.Player p)
        {
            if (p.CurrentBuildings == 0 &&
                p.Status.Equals(PredefinedTypes.PlayerStatus.Playing) && 
                p.SupplyMax == 0 &&
                p.SupplyMin == 0 &&
                p.Worker == 0 &&
                p.Minerals == 0 &&
                p.Gas == 0)
                return true;

            return false;
        }

        /* Draw the units */
        private void Helper_DrawUnits(Int32 counter, ref Int32 posX, Int32 posY, Int32 size, Image img, BufferedGraphics g, Color clPlayercolor)
        {
            if (counter > 0)
            {
                g.Graphics.DrawImage(img, posX, posY, size, size);
                g.Graphics.DrawString(counter.ToString(CultureInfo.InvariantCulture), Font, Brushes.White, posX + 5, posY + 5);
                g.Graphics.DrawRectangle(new Pen(new SolidBrush(clPlayercolor), 2), posX, posY, size, size);
                posX += size;
            }
        }

        /* Draw the units with production- table */
        private void Helper_DrawUnits(Int32 counter, ref Int32 posX, Int32 posY, Int32 size, Image img, BufferedGraphics g, Color clPlayercolor, float fProductionProcess)
        {
            if (counter > 0)
            {
                g.Graphics.DrawImage(img, posX, posY, size, size);
                g.Graphics.DrawString(counter.ToString(CultureInfo.InvariantCulture), Font, Brushes.White, posX + 5, posY + 5);

                /* Adjust relative size */
                float ftemp = size - 4;
                ftemp*=(fProductionProcess/100);

                /* Draw status- line */
                g.Graphics.DrawRectangle(new Pen(Brushes.Yellow, 2), posX + 2, posY + size - 8, (Int32)ftemp, 1);
                g.Graphics.DrawRectangle(Constants.PRed2, posX + 2, posY + size - 10, size - 4, 5);
                
                g.Graphics.DrawRectangle(new Pen(new SolidBrush(clPlayercolor), 2), posX, posY, size, size);
                posX += size;
            }
        }

        /* Draw the curretn Resources */
        private void DrawResources(BufferedGraphics g)
        {
            try
            {

                if (!GInfo.IsIngame)
                    return;

                var iValidPlayerCount = GInfo._IValidPlayerCount;

                if (iValidPlayerCount == 0)
                    return;

                Opacity = _hMainHandler.PSettings.ResourceOpacity;
                var iSingleHeight = Height/iValidPlayerCount;
                var fNewFontSize = (float) ((29.0/100)*iSingleHeight);
                var fInternalFont = new Font(_hMainHandler.PSettings.ResourceFontName, fNewFontSize, FontStyle.Bold);
                var fInternalFontNormal = new Font(fInternalFont.Name, fNewFontSize, FontStyle.Regular);

                if (!_bChangingPosition)
                {
                    Height = _hMainHandler.PSettings.ResourceHeight*iValidPlayerCount;
                    Width = _hMainHandler.PSettings.ResourceWidth;

                    
                }

                var iCounter = 0;
                for (var i = 0; i < LPlayer.Count; i++)
                {
                    var clPlayercolor = LPlayer[i].Color;

                    #region Teamcolor

                    if (GInfo.IsTeamcolor)
                    {
                        if (LPlayer[i].Localplayer < LPlayer.Count)
                        {
                            if (LPlayer[i].IsLocalplayer)
                                clPlayercolor = Color.Green;

                            else if (LPlayer[i].Team ==
                                     LPlayer[LPlayer[0].Localplayer].Team &&
                                     !LPlayer[i].IsLocalplayer)
                                clPlayercolor = Color.Yellow;

                            else if (LPlayer[LPlayer[0].Localplayer].Team !=
                                     LPlayer[i].Team)
                                clPlayercolor = Color.Red;

                            else
                                clPlayercolor = Color.White;
                        }
                    }

                    #endregion

                    #region Escape sequences

                    if (_hMainHandler.PSettings.ResourceRemoveAi)
                    {
                        if (LPlayer[i].Type.Equals(PredefinedTypes.PlayerType.Ai))
                            continue;
                    }

                    if (_hMainHandler.PSettings.ResourceRemoveNeutral)
                    {
                        if (LPlayer[i].Type.Equals(PredefinedTypes.PlayerType.Neutral))
                            continue;
                    }

                    if (_hMainHandler.PSettings.ResourceRemoveAllie)
                    {
                        if (LPlayer[i].Team == LPlayer[LPlayer[i].Localplayer].Team &&
                            !LPlayer[i].IsLocalplayer)
                            continue;
                    }

                    if (_hMainHandler.PSettings.ResourceRemoveLocalplayer)
                    {
                        if (LPlayer[i].IsLocalplayer)
                            continue;
                    }



                    if (LPlayer[i].Name.StartsWith("\0"))
                        continue;

                    if (LPlayer[i].Type.Equals(PredefinedTypes.PlayerType.Hostile))
                        continue;

                    if (LPlayer[i].Type.Equals(PredefinedTypes.PlayerType.Observer))
                        continue;

                    if (LPlayer[i].Type.Equals(PredefinedTypes.PlayerType.Referee))
                        continue;

                    if (CheckIfGameheart(LPlayer[i]))
                        continue;

                    #endregion

                    #region SetValidImages (Race)

                    if (LPlayer[i].Race.Equals(PredefinedTypes.Race.Terran))
                    {
                        _imgMinerals = Properties.Resources.Mineral_Terran;
                        _imgGas = Properties.Resources.Gas_Terran;
                        _imgSupply = Properties.Resources.Supply_Terran;
                        _imgWorker = Properties.Resources.T_SCV;
                    }

                    else if (LPlayer[i].Race.Equals(PredefinedTypes.Race.Protoss))
                    {
                        _imgMinerals = Properties.Resources.Mineral_Protoss;
                        _imgGas = Properties.Resources.Gas_Protoss;
                        _imgSupply = Properties.Resources.Supply_Protoss;
                        _imgWorker = Properties.Resources.P_Probe;
                    }

                    else
                    {
                        _imgMinerals = Properties.Resources.Mineral_Zerg;
                        _imgGas = Properties.Resources.Gas_Zerg;
                        _imgSupply = Properties.Resources.Supply_Zerg;
                        _imgWorker = Properties.Resources.Z_Drone;
                    }

                    #endregion

                    #region Draw Bounds and Background

                    if (_hMainHandler.PSettings.ResourceDrawBackground)
                    {
                        /* Background */
                        g.Graphics.FillRectangle(Brushes.Gray, 1, 1 + (iSingleHeight*iCounter), Width - 2,
                                                 iSingleHeight - 2);

                        /* Border */
                        g.Graphics.DrawRectangle(new Pen(new SolidBrush(clPlayercolor), 2), 1,
                                                 1 + (iSingleHeight*iCounter),
                                                 Width - 2, iSingleHeight - 2);
                    }

                    #endregion

                    #region Content Drawing

                    #region Name

                    g.Graphics.DrawString(LPlayer[i].Name, fInternalFont,
                                          new SolidBrush(clPlayercolor), (float) ((1.67/100)*Width),
                                          (float) ((24.0/100)*iSingleHeight) + iSingleHeight*iCounter);

                    #endregion

                    #region Team

                    g.Graphics.DrawString("#" + LPlayer[i].Team, fInternalFontNormal,
                                          new SolidBrush(Color.White), (float) ((21.67/100)*Width),
                                          (float) ((24.0/100)*iSingleHeight) + iSingleHeight*iCounter);

                    #endregion

                    #region Minerals

                    /* Icon */
                    g.Graphics.DrawImage(_imgMinerals, (float) ((30.0/100)*Width),
                                         (float) ((14.0/100)*iSingleHeight) + (Height/iValidPlayerCount)*iCounter,
                                         (float) ((70.0/100)*iSingleHeight), (float) ((70.0/100)*iSingleHeight));

                    /* Mineral Count */
                    g.Graphics.DrawString(LPlayer[i].Minerals.ToString(CultureInfo.InvariantCulture),
                                          fInternalFontNormal,
                                          new SolidBrush(Color.White), (float) ((36.67/100)*Width),
                                          (float) ((24.0/100)*iSingleHeight) + iSingleHeight*iCounter);

                    #endregion

                    #region Gas

                    /* Icon */
                    g.Graphics.DrawImage(_imgGas, (float) ((50.0/100)*Width),
                                         (float) ((14.0/100)*iSingleHeight) + (Height/iValidPlayerCount)*iCounter,
                                         (float) ((70.0/100)*iSingleHeight), (float) ((70.0/100)*iSingleHeight));

                    /* Gas Count */
                    g.Graphics.DrawString(LPlayer[i].Gas.ToString(CultureInfo.InvariantCulture), fInternalFontNormal,
                                          new SolidBrush(Color.White), (float) ((56.67/100)*Width),
                                          (float) ((24.0/100)*iSingleHeight) + iSingleHeight*iCounter);

                    #endregion

                    #region Supply

                    /* Icon */
                    g.Graphics.DrawImage(_imgSupply, (float) ((70.0/100)*Width),
                                         (float) ((14.0/100)*iSingleHeight) + (Height/iValidPlayerCount)*iCounter,
                                         (float) ((70.0/100)*iSingleHeight), (float) ((70.0/100)*iSingleHeight));

                    /* Mineral Count */
                    g.Graphics.DrawString(
                        LPlayer[i].SupplyMin.ToString(CultureInfo.InvariantCulture) + " / " +
                        LPlayer[i].SupplyMax, fInternalFontNormal,
                        new SolidBrush(Color.White), (float) ((76.67/100)*Width),
                        (float) ((24.0/100)*iSingleHeight) + iSingleHeight*iCounter);

                    #endregion

                    #endregion


                    iCounter++;
                }



                ///* Test- style */
                //g.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                //g.Graphics.CompositingQuality = CompositingQuality.HighQuality;
                //g.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                ///* Background */
                //HelpFunctions.Help_Graphics.DrawRoundedRectangle(g.Graphics,
                //                                                new Rectangle(2, 2,
                //                                                              _hMainHandler.PSettings.ResourceWidth - 4,
                //                                                              _hMainHandler.PSettings.ResourceHeight *
                //                                                              iCounter - 4), 30, new Pen(Brushes.DarkGray, 4));

                //HelpFunctions.Help_Graphics.DrawRoundedRectangle(g.Graphics,
                //                                                 new Rectangle(2, 2,
                //                                                               _hMainHandler.PSettings.ResourceWidth - 4,
                //                                                               _hMainHandler.PSettings.ResourceHeight *
                //                                                               iCounter - 4), 30, new Pen(Brushes.LightBlue, 2));

            }

            catch (Exception ex)
            {
                Messages.LogFile("DrawResource", "Over all", ex);
            }


            /* Draw a final bound around the panel */
            if (_bChangingPosition)
            {
                g.Graphics.DrawRectangle(Constants.PYellowGreen2,
                                            1,
                                            1,
                                            Width - 2,
                                            Height - 2);
            }
        }

        /* Draw Income */
        private void DrawIncome(BufferedGraphics g)
        {
            try
            {

                if (!GInfo.IsIngame)
                    return;

                var iValidPlayerCount = GInfo._IValidPlayerCount;

                if (iValidPlayerCount == 0)
                    return;

                Opacity = _hMainHandler.PSettings.IncomeOpacity;
                var iSingleHeight = Height/iValidPlayerCount;
                var fNewFontSize = (float) ((29.0/100)*iSingleHeight);
                var fInternalFont = new Font(_hMainHandler.PSettings.IncomeFontName, fNewFontSize, FontStyle.Bold);
                var fInternalFontNormal = new Font(fInternalFont.Name, fNewFontSize, FontStyle.Regular);

                if (!_bChangingPosition)
                {
                    Height = _hMainHandler.PSettings.IncomeHeight*iValidPlayerCount;
                    Width = _hMainHandler.PSettings.IncomeWidth;
                }

                var iCounter = 0;
                for (var i = 0; i < LPlayer.Count; i++)
                {
                    var clPlayercolor = LPlayer[i].Color;

                    #region Teamcolor

                    if (GInfo.IsTeamcolor)
                    {
                        if (LPlayer[i].Localplayer < LPlayer.Count)
                        {
                            if (LPlayer[i].IsLocalplayer)
                                clPlayercolor = Color.Green;

                            else if (LPlayer[i].Team ==
                                     LPlayer[LPlayer[0].Localplayer].Team &&
                                     !LPlayer[i].IsLocalplayer)
                                clPlayercolor = Color.Yellow;

                            else if (LPlayer[LPlayer[0].Localplayer].Team !=
                                     LPlayer[i].Team)
                                clPlayercolor = Color.Red;

                            else
                                clPlayercolor = Color.White;
                        }
                    }

                    #endregion

                    #region Escape sequences

                    if (_hMainHandler.PSettings.IncomeRemoveAi)
                    {
                        if (LPlayer[i].Type.Equals(PredefinedTypes.PlayerType.Ai))
                            continue;
                    }

                    if (_hMainHandler.PSettings.IncomeRemoveNeutral)
                    {
                        if (LPlayer[i].Type.Equals(PredefinedTypes.PlayerType.Neutral))
                            continue;
                    }

                    if (_hMainHandler.PSettings.IncomeRemoveAllie)
                    {
                        if (LPlayer[i].Team == LPlayer[LPlayer[i].Localplayer].Team &&
                            !LPlayer[i].IsLocalplayer)
                            continue;
                    }

                    if (_hMainHandler.PSettings.IncomeRemoveLocalplayer)
                    {
                        if (LPlayer[i].IsLocalplayer)
                            continue;
                    }

                    if (LPlayer[i].Name.StartsWith("\0"))
                        continue;

                    if (LPlayer[i].Type.Equals(PredefinedTypes.PlayerType.Hostile))
                        continue;

                    if (LPlayer[i].Type.Equals(PredefinedTypes.PlayerType.Observer))
                        continue;

                    if (LPlayer[i].Type.Equals(PredefinedTypes.PlayerType.Referee))
                        continue;

                    if (CheckIfGameheart(LPlayer[i]))
                        continue;

                    #endregion

                    #region SetValidImages (Race)

                    if (LPlayer[i].Race.Equals(PredefinedTypes.Race.Terran))
                    {
                        _imgMinerals = Properties.Resources.Mineral_Terran;
                        _imgGas = Properties.Resources.Gas_Terran;
                        _imgSupply = Properties.Resources.Supply_Terran;
                        _imgWorker = Properties.Resources.T_SCV;
                    }

                    else if (LPlayer[i].Race.Equals(PredefinedTypes.Race.Protoss))
                    {
                        _imgMinerals = Properties.Resources.Mineral_Protoss;
                        _imgGas = Properties.Resources.Gas_Protoss;
                        _imgSupply = Properties.Resources.Supply_Protoss;
                        _imgWorker = Properties.Resources.P_Probe;
                    }

                    else
                    {
                        _imgMinerals = Properties.Resources.Mineral_Zerg;
                        _imgGas = Properties.Resources.Gas_Zerg;
                        _imgSupply = Properties.Resources.Supply_Zerg;
                        _imgWorker = Properties.Resources.Z_Drone;
                    }

                    #endregion

                    #region Draw Bounds and Background

                    if (_hMainHandler.PSettings.IncomeDrawBackground)
                    {
                        /* Background */
                        g.Graphics.FillRectangle(Brushes.Gray, 1, 1 + (iSingleHeight*iCounter), Width - 2,
                                                 iSingleHeight - 2);

                        /* Border */
                        g.Graphics.DrawRectangle(new Pen(new SolidBrush(clPlayercolor), 2), 1,
                                                 1 + (iSingleHeight*iCounter),
                                                 Width - 2, iSingleHeight - 2);
                    }


                    #endregion

                    #region Content Drawing

                    #region Name

                    g.Graphics.DrawString(LPlayer[i].Name, fInternalFont,
                                          new SolidBrush(clPlayercolor), (float) ((1.67/100)*Width),
                                          (float) ((24.0/100)*iSingleHeight) + iSingleHeight*iCounter);

                    #endregion

                    #region Team

                    g.Graphics.DrawString("#" + LPlayer[i].Team, fInternalFontNormal,
                                          new SolidBrush(Color.White), (float) ((21.67/100)*Width),
                                          (float) ((24.0/100)*iSingleHeight) + iSingleHeight*iCounter);

                    #endregion

                    #region Minerals

                    /* Icon */
                    g.Graphics.DrawImage(_imgMinerals, (float) ((30.0/100)*Width),
                                         (float) ((14.0/100)*iSingleHeight) + (Height/iValidPlayerCount)*iCounter,
                                         (float) ((70.0/100)*iSingleHeight), (float) ((70.0/100)*iSingleHeight));

                    /* Mineral Count */
                    g.Graphics.DrawString(LPlayer[i].MineralsIncome.ToString(CultureInfo.InvariantCulture),
                                          fInternalFontNormal,
                                          new SolidBrush(Color.White), (float) ((36.67/100)*Width),
                                          (float) ((24.0/100)*iSingleHeight) + iSingleHeight*iCounter);

                    #endregion

                    #region Gas

                    /* Icon */
                    g.Graphics.DrawImage(_imgGas, (float) ((50.0/100)*Width),
                                         (float) ((14.0/100)*iSingleHeight) + (Height/iValidPlayerCount)*iCounter,
                                         (float) ((70.0/100)*iSingleHeight), (float) ((70.0/100)*iSingleHeight));

                    /* Gas Count */
                    g.Graphics.DrawString(LPlayer[i].GasIncome.ToString(CultureInfo.InvariantCulture),
                                          fInternalFontNormal,
                                          new SolidBrush(Color.White), (float) ((56.67/100)*Width),
                                          (float) ((24.0/100)*iSingleHeight) + iSingleHeight*iCounter);

                    #endregion

                    #region Workers

                    /* Icon */
                    g.Graphics.DrawImage(_imgWorker, (float) ((70.0/100)*Width),
                                         (float) ((14.0/100)*iSingleHeight) + (Height/iValidPlayerCount)*iCounter,
                                         (float) ((70.0/100)*iSingleHeight), (float) ((70.0/100)*iSingleHeight));

                    /* Mineral Count */
                    g.Graphics.DrawString(
                        LPlayer[i].Worker.ToString(CultureInfo.InvariantCulture), fInternalFontNormal,
                        new SolidBrush(Color.White), (float) ((76.67/100)*Width),
                        (float) ((24.0/100)*iSingleHeight) + iSingleHeight*iCounter);

                    #endregion

                    #endregion


                    iCounter++;
                }
            }

            catch (Exception ex)
            {
                Messages.LogFile("DrawIncome", "Over all", ex);
            }

            /* Draw a final bound around the panel */
            if (_bChangingPosition)
            {
                g.Graphics.DrawRectangle(Constants.PYellowGreen2,
                                            1,
                                            1,
                                            Width - 2,
                                            Height - 2);
            }
        }

        /* Draw Army */
        private void DrawArmy(BufferedGraphics g)
        {
            try
            {

                if (!GInfo.IsIngame)
                    return;

                var iValidPlayerCount = GInfo._IValidPlayerCount;

                if (iValidPlayerCount == 0)
                    return;

                Opacity = _hMainHandler.PSettings.ArmyOpacity;
                var iSingleHeight = Height/iValidPlayerCount;
                var fNewFontSize = (float) ((29.0/100)*iSingleHeight);
                var fInternalFont = new Font(_hMainHandler.PSettings.ArmyFontName, fNewFontSize, FontStyle.Bold);
                var fInternalFontNormal = new Font(fInternalFont.Name, fNewFontSize, FontStyle.Regular);

                if (!_bChangingPosition)
                {
                    Height = _hMainHandler.PSettings.ArmyHeight*iValidPlayerCount;
                    Width = _hMainHandler.PSettings.ArmyWidth;
                }

                var iCounter = 0;
                for (var i = 0; i < LPlayer.Count; i++)
                {
                    var clPlayercolor = LPlayer[i].Color;

                    #region Teamcolor

                    if (GInfo.IsTeamcolor)
                    {
                        if (LPlayer[i].Localplayer < LPlayer.Count)
                        {
                            if (LPlayer[i].IsLocalplayer)
                                clPlayercolor = Color.Green;

                            else if (LPlayer[i].Team ==
                                     LPlayer[LPlayer[0].Localplayer].Team &&
                                     !LPlayer[i].IsLocalplayer)
                                clPlayercolor = Color.Yellow;

                            else if (LPlayer[LPlayer[0].Localplayer].Team !=
                                     LPlayer[i].Team)
                                clPlayercolor = Color.Red;

                            else
                                clPlayercolor = Color.White;
                        }
                    }

                    #endregion

                    #region Escape sequences

                    if (_hMainHandler.PSettings.ArmyRemoveAi)
                    {
                        if (LPlayer[i].Type.Equals(PredefinedTypes.PlayerType.Ai))
                            continue;
                    }

                    if (_hMainHandler.PSettings.ArmyRemoveNeutral)
                    {
                        if (LPlayer[i].Type.Equals(PredefinedTypes.PlayerType.Neutral))
                            continue;
                    }

                    if (_hMainHandler.PSettings.ArmyRemoveAllie)
                    {
                        if (LPlayer[i].Team == LPlayer[LPlayer[i].Localplayer].Team &&
                            !LPlayer[i].IsLocalplayer)
                            continue;
                    }

                    if (_hMainHandler.PSettings.ArmyRemoveLocalplayer)
                    {
                        if (LPlayer[i].IsLocalplayer)
                            continue;
                    }

                    if (LPlayer[i].Name.StartsWith("\0"))
                        continue;

                    if (LPlayer[i].Type.Equals(PredefinedTypes.PlayerType.Hostile))
                        continue;

                    if (LPlayer[i].Type.Equals(PredefinedTypes.PlayerType.Observer))
                        continue;

                    if (LPlayer[i].Type.Equals(PredefinedTypes.PlayerType.Referee))
                        continue;

                    if (CheckIfGameheart(LPlayer[i]))
                        continue;

                    #endregion

                    #region SetValidImages (Race)

                    if (LPlayer[i].Race.Equals(PredefinedTypes.Race.Terran))
                    {
                        _imgMinerals = Properties.Resources.Mineral_Terran;
                        _imgGas = Properties.Resources.Gas_Terran;
                        _imgSupply = Properties.Resources.Supply_Terran;
                        _imgWorker = Properties.Resources.T_SCV;
                    }

                    else if (LPlayer[i].Race.Equals(PredefinedTypes.Race.Protoss))
                    {
                        _imgMinerals = Properties.Resources.Mineral_Protoss;
                        _imgGas = Properties.Resources.Gas_Protoss;
                        _imgSupply = Properties.Resources.Supply_Protoss;
                        _imgWorker = Properties.Resources.P_Probe;
                    }

                    else
                    {
                        _imgMinerals = Properties.Resources.Mineral_Zerg;
                        _imgGas = Properties.Resources.Gas_Zerg;
                        _imgSupply = Properties.Resources.Supply_Zerg;
                        _imgWorker = Properties.Resources.Z_Drone;
                    }

                    #endregion

                    #region Draw Bounds and Background

                    if (_hMainHandler.PSettings.ArmyDrawBackground)
                    {
                        /* Background */
                        g.Graphics.FillRectangle(Brushes.Gray, 1, 1 + (iSingleHeight*iCounter), Width - 2,
                                                 iSingleHeight - 2);

                        /* Border */
                        g.Graphics.DrawRectangle(new Pen(new SolidBrush(clPlayercolor), 2), 1,
                                                 1 + (iSingleHeight*iCounter),
                                                 Width - 2, iSingleHeight - 2);
                    }

                    #endregion

                    #region Content Drawing

                    #region Name

                    g.Graphics.DrawString(LPlayer[i].Name, fInternalFont,
                                          new SolidBrush(clPlayercolor), (float) ((1.67/100)*Width),
                                          (float) ((24.0/100)*iSingleHeight) + iSingleHeight*iCounter);

                    #endregion

                    #region Team

                    g.Graphics.DrawString("#" + LPlayer[i].Team, fInternalFontNormal,
                                          new SolidBrush(Color.White), (float) ((21.67/100)*Width),
                                          (float) ((24.0/100)*iSingleHeight) + iSingleHeight*iCounter);

                    #endregion

                    #region Minerals

                    /* Icon */
                    g.Graphics.DrawImage(_imgMinerals, (float) ((30.0/100)*Width),
                                         (float) ((14.0/100)*iSingleHeight) + (Height/iValidPlayerCount)*iCounter,
                                         (float) ((70.0/100)*iSingleHeight), (float) ((70.0/100)*iSingleHeight));

                    /* Mineral Count */
                    g.Graphics.DrawString(LPlayer[i].MineralsArmy.ToString(CultureInfo.InvariantCulture),
                                          fInternalFontNormal,
                                          new SolidBrush(Color.White), (float) ((36.67/100)*Width),
                                          (float) ((24.0/100)*iSingleHeight) + iSingleHeight*iCounter);

                    #endregion

                    #region Gas

                    /* Icon */
                    g.Graphics.DrawImage(_imgGas, (float) ((50.0/100)*Width),
                                         (float) ((14.0/100)*iSingleHeight) + (Height/iValidPlayerCount)*iCounter,
                                         (float) ((70.0/100)*iSingleHeight), (float) ((70.0/100)*iSingleHeight));

                    /* Gas Count */
                    g.Graphics.DrawString(LPlayer[i].GasArmy.ToString(CultureInfo.InvariantCulture), fInternalFontNormal,
                                          new SolidBrush(Color.White), (float) ((56.67/100)*Width),
                                          (float) ((24.0/100)*iSingleHeight) + iSingleHeight*iCounter);

                    #endregion

                    #region Supply

                    /* Icon */
                    g.Graphics.DrawImage(_imgSupply, (float) ((70.0/100)*Width),
                                         (float) ((14.0/100)*iSingleHeight) + (Height/iValidPlayerCount)*iCounter,
                                         (float) ((70.0/100)*iSingleHeight), (float) ((70.0/100)*iSingleHeight));

                    /* Mineral Count */
                    g.Graphics.DrawString(
                        LPlayer[i].SupplyMin.ToString(CultureInfo.InvariantCulture) + " / " +
                        LPlayer[i].SupplyMax, fInternalFontNormal,
                        new SolidBrush(Color.White), (float) ((76.67/100)*Width),
                        (float) ((24.0/100)*iSingleHeight) + iSingleHeight*iCounter);

                    #endregion

                    #endregion


                    iCounter++;
                }
            }

            catch (Exception ex)
            {
                Messages.LogFile("DrawArmy", "Over all", ex);
            }

            /* Draw a final bound around the panel */
            if (_bChangingPosition)
            {
                g.Graphics.DrawRectangle(Constants.PYellowGreen2,
                                            1,
                                            1,
                                            Width - 2,
                                            Height - 2);
            }
        }

        /* Draw Apm */
        private void DrawApm(BufferedGraphics g)
        {
            try
            {

                if (!GInfo.IsIngame)
                    return;

                var iValidPlayerCount = GInfo._IValidPlayerCount;

                if (iValidPlayerCount == 0)
                    return;

                Opacity = _hMainHandler.PSettings.ApmOpacity;
                var iSingleHeight = Height/iValidPlayerCount;
                var fNewFontSize = (float) ((29.0/100)*iSingleHeight);
                var fInternalFont = new Font(_hMainHandler.PSettings.ApmFontName, fNewFontSize, FontStyle.Bold);
                var fInternalFontNormal = new Font(fInternalFont.Name, fNewFontSize, FontStyle.Regular);

                if (!_bChangingPosition)
                {
                    Height = _hMainHandler.PSettings.ApmHeight*iValidPlayerCount;
                    Width = _hMainHandler.PSettings.ApmWidth;
                }

                var iCounter = 0;
                for (var i = 0; i < LPlayer.Count; i++)
                {
                    var clPlayercolor = LPlayer[i].Color;

                    #region Teamcolor

                    if (GInfo.IsTeamcolor)
                    {
                        if (LPlayer[i].Localplayer < LPlayer.Count)
                        {
                            if (LPlayer[i].IsLocalplayer)
                                clPlayercolor = Color.Green;

                            else if (LPlayer[i].Team ==
                                     LPlayer[LPlayer[0].Localplayer].Team &&
                                     !LPlayer[i].IsLocalplayer)
                                clPlayercolor = Color.Yellow;

                            else if (LPlayer[LPlayer[0].Localplayer].Team !=
                                     LPlayer[i].Team)
                                clPlayercolor = Color.Red;

                            else
                                clPlayercolor = Color.White;
                        }
                    }

                    #endregion

                    #region Escape sequences

                    if (_hMainHandler.PSettings.ApmRemoveAi)
                    {
                        if (LPlayer[i].Type.Equals(PredefinedTypes.PlayerType.Ai))
                            continue;
                    }

                    if (_hMainHandler.PSettings.ApmRemoveNeutral)
                    {
                        if (LPlayer[i].Type.Equals(PredefinedTypes.PlayerType.Neutral))
                            continue;
                    }

                    if (_hMainHandler.PSettings.ApmRemoveAllie)
                    {
                        if (LPlayer[i].Team == LPlayer[LPlayer[i].Localplayer].Team &&
                            !LPlayer[i].IsLocalplayer)
                            continue;
                    }

                    if (_hMainHandler.PSettings.ApmRemoveLocalplayer)
                    {
                        if (LPlayer[i].IsLocalplayer)
                            continue;
                    }

                    if (LPlayer[i].Name.StartsWith("\0"))
                        continue;

                    if (LPlayer[i].Type.Equals(PredefinedTypes.PlayerType.Hostile))
                        continue;

                    if (LPlayer[i].Type.Equals(PredefinedTypes.PlayerType.Observer))
                        continue;

                    if (LPlayer[i].Type.Equals(PredefinedTypes.PlayerType.Referee))
                        continue;

                    if (CheckIfGameheart(LPlayer[i]))
                        continue;

                    #endregion

                    #region Draw Bounds and Background

                    if (_hMainHandler.PSettings.ApmDrawBackground)
                    {
                        /* Background */
                        g.Graphics.FillRectangle(Brushes.Gray, 1, 1 + (iSingleHeight*iCounter), Width - 2,
                                                 iSingleHeight - 2);

                        /* Border */
                        g.Graphics.DrawRectangle(new Pen(new SolidBrush(clPlayercolor), 2), 1,
                                                 1 + (iSingleHeight*iCounter),
                                                 Width - 2, iSingleHeight - 2);
                    }

                    #endregion

                    #region Content Drawing

                    #region Name

                    g.Graphics.DrawString(LPlayer[i].Name, fInternalFont,
                                          new SolidBrush(clPlayercolor), (float) ((1.67/100)*Width),
                                          (float) ((24.0/100)*iSingleHeight) + iSingleHeight*iCounter);

                    #endregion

                    #region Team

                    g.Graphics.DrawString("#" + LPlayer[i].Team, fInternalFontNormal,
                                          new SolidBrush(Color.White), (float) ((21.67/100)*Width),
                                          (float) ((24.0/100)*iSingleHeight) + iSingleHeight*iCounter);

                    #endregion

                    #region Apm

                    /* Apm */
                    g.Graphics.DrawString("APM [" + LPlayer[i].Apm + "]", fInternalFontNormal,
                                          new SolidBrush(Color.White), (float) ((30.0/100)*Width),
                                          (float) ((24.0/100)*iSingleHeight) + iSingleHeight*iCounter);

                    #endregion

                    #region Epm

                    /* EPM Count */
                    g.Graphics.DrawString("EPM [" + LPlayer[i].Epm + "]", fInternalFontNormal,
                                          new SolidBrush(Color.White), (float) ((56.67/100)*Width),
                                          (float) ((24.0/100)*iSingleHeight) + iSingleHeight*iCounter);

                    #endregion

                    #endregion


                    iCounter++;
                }

            }

            catch (Exception ex)
            {
                Messages.LogFile("DrawApm", "Over all", ex);
            }

            /* Draw a final bound around the panel */
            if (_bChangingPosition)
            {
                g.Graphics.DrawRectangle(Constants.PYellowGreen2,
                                            1,
                                            1,
                                            Width - 2,
                                            Height - 2);
            }
        }

        /* Draw Worker */
        private void DrawWorker(BufferedGraphics g)
        {
            try
            {

                if (!GInfo.IsIngame)
                {
                    g.Graphics.Clear(BackColor);
                    return;
                }

                Opacity = _hMainHandler.PSettings.WorkerOpacity;
                var iSingleHeight = Height;
                var fNewFontSize = (float) ((29.0/100)*iSingleHeight);
                var fInternalFont = new Font(_hMainHandler.PSettings.WorkerFontName, fNewFontSize, FontStyle.Bold);

                Color clPlayercolor;

                if (LPlayer[0].Localplayer < LPlayer.Count)
                    clPlayercolor = LPlayer[LPlayer[0].Localplayer].Color;

                else
                    return;

                if (!_bChangingPosition)
                {
                    Height = _hMainHandler.PSettings.WorkerHeight;
                    Width = _hMainHandler.PSettings.WorkerWidth;
                }

                #region Teamcolor

                if (GInfo.IsTeamcolor)
                    clPlayercolor = Color.Green;

                #endregion

                #region Draw Bounds and Background

                if (_hMainHandler.PSettings.WorkerDrawBackground)
                {
                    /* Background */
                    g.Graphics.FillRectangle(Brushes.Gray, 1, 1, Width - 2, iSingleHeight - 2);

                    /* Border */
                    g.Graphics.DrawRectangle(new Pen(new SolidBrush(clPlayercolor), 2), 1, 1, Width - 2,
                                             iSingleHeight - 2);
                }

                #endregion

                #region Worker

                /* Text */
                g.Graphics.DrawString(LPlayer[LPlayer[0].Localplayer].Worker + "   Workers", fInternalFont,
                                      new SolidBrush(clPlayercolor), (float) ((16.67/100)*Width),
                                      (float) ((24.0/100)*iSingleHeight));

                #endregion

            }

            catch (Exception ex)
            {
                Messages.LogFile("DrawWorker", "Over all", ex);
            }

            /* Draw a final bound around the panel */
            if (_bChangingPosition)
            {
                g.Graphics.DrawRectangle(Constants.PYellowGreen2,
                                            1,
                                            1,
                                            Width - 2,
                                            Height - 2);
            }
        }

        /* Imitates the Minimap */
        private void DrawMinimap(BufferedGraphics g)
        {
            try
            {
                if (!GInfo.IsIngame)
                {
                    g.Graphics.Clear(BackColor);
                    return;
                }

                Opacity = _hMainHandler.PSettings.MaphackOpacity;

                if (!_bChangingPosition)
                {
                    Height = _hMainHandler.PSettings.MaphackHeigth;
                    Width = _hMainHandler.PSettings.MaphackWidth;
                }

                #region Introduction

                #region Variables

                float iScale,
                      iX,
                      iY;

                #endregion

                #region Get minimap Bounds

                double fa = Height/(float) Width;
                double fb = ((float) GMap.PlayableHeight/GMap.PlayableWidth);

                if (fa >= fb)
                {
                    iScale = (float) Width/GMap.PlayableWidth;
                    iX = 0;
                    iY = (Height - iScale*GMap.PlayableHeight)/2;
                }
                else
                {
                    iScale = (float) Height/GMap.PlayableHeight;
                    iY = 0;
                    iX = (Width - iScale*GMap.PlayableWidth)/2;
                }



                #endregion

                #region Draw Bounds

                if (!_hMainHandler.PSettings.MaphackRemoveVisionArea)
                {
                    /* Draw Rectangle */
                    g.Graphics.DrawRectangle(Constants.PBound, 0, 0, Width - Constants.PBound.Width,
                                             Height - Constants.PBound.Width);

                    /* Draw Playable Area */
                    g.Graphics.DrawRectangle(Constants.PArea, iX, iY, Width - iX*2 - Constants.PArea.Width,
                                             Height - iY*2 - Constants.PArea.Width);
                }

                #endregion

                #endregion

                #region Actual Drawing

                #region Draw Unit- destination

                if (!_hMainHandler.PSettings.MaphackDisableDestinationLine)
                {
                    for (var i = 0; i < LUnit.Count; i++)
                    {
                        var clDestination = _hMainHandler.PSettings.MaphackDestinationColor;

                        #region Scalling (Unitposition + UnitDestination)

                        var iUnitPosX = (LUnit[i].PositionX - GMap.Left)*iScale + iX;
                        var iUnitPosY = (GMap.Top - LUnit[i].PositionY)*iScale + iY;

                        var iUnitDestPosX = (LUnit[i].DestinationPositionX - GMap.Left)*iScale +
                                            iX;
                        var iUnitDestPosY = (GMap.Top - LUnit[i].DestinationPositionY)*iScale +
                                            iY;

                        if (float.IsNaN(iUnitPosX) ||
                            float.IsNaN(iUnitPosY) ||
                            float.IsNaN(iUnitDestPosX) ||
                            float.IsNaN(iUnitDestPosY))
                        {
                            continue;
                        }


                        #endregion

                        #region Escape Sequences

                        /* Ai */
                        if (_hMainHandler.PSettings.MaphackRemoveAi)
                        {
                            if (
                                LPlayer[LUnit[i].Owner].Type.Equals(
                                    PredefinedTypes.PlayerType.Ai))
                                continue;
                        }

                        /* Allie */
                        if (_hMainHandler.PSettings.MaphackRemoveAllie)
                        {
                            if (LPlayer[0].Localplayer < LPlayer.Count)
                            {
                                if (LPlayer[LUnit[i].Owner].Team ==
                                    LPlayer[LPlayer[0].Localplayer].Team &&
                                    !LPlayer[LUnit[i].Owner].IsLocalplayer)
                                    continue;
                            }
                        }

                        /* Localplayer Units */
                        if (_hMainHandler.PSettings.MaphackRemoveLocalplayer)
                        {
                            if (LUnit[i].Owner == LPlayer[0].Localplayer)
                                continue;
                        }

                        /* Neutral Units */
                        if (_hMainHandler.PSettings.MaphackRemoveNeutral)
                        {
                            if (
                                LPlayer[LUnit[i].Owner].Type.Equals(
                                    PredefinedTypes.PlayerType.Neutral))
                                continue;
                        }

                        /* Dead Units */
                        if ((LUnit[i].TargetFilter & (ulong) PredefinedTypes.TargetFilterFlag.Dead) > 0)
                            continue;


                        /* Moving- state */
                        if (LUnit[i].Movestate.Equals(0))
                            continue;




                        #endregion

                        g.Graphics.CompositingQuality = CompositingQuality.HighQuality;
                        g.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        g.Graphics.SmoothingMode = SmoothingMode.HighQuality;

                        /* Draws the Line */
                        if (LUnit[i].DestinationPositionX > 10 &&
                            LUnit[i].DestinationPositionY > 10)
                            g.Graphics.DrawLine(new Pen(new SolidBrush(clDestination)), iUnitPosX, iUnitPosY,
                                                iUnitDestPosX,
                                                iUnitDestPosY);

                        g.Graphics.CompositingQuality = CompositingQuality.HighSpeed;
                        g.Graphics.PixelOffsetMode = PixelOffsetMode.HighSpeed;
                        g.Graphics.SmoothingMode = SmoothingMode.HighSpeed;

                    }
                }

                #endregion

                #region Draw Unit (Border/ outer Rectangle)

                for (var i = 0; i < LUnit.Count; i++)
                {
                    var clUnitBound = Color.Black;

                    if (LUnit[i].Owner >= (LPlayer.Count))
                        continue;

                    #region Scalling (Unitposition)

                    var iUnitPosX = (LUnit[i].PositionX - GMap.Left)*iScale + iX;
                    var iUnitPosY = (GMap.Top - LUnit[i].PositionY)*iScale + iY;


                    if (float.IsNaN(iUnitPosX) ||
                        float.IsNaN(iUnitPosY))
                    {
                        continue;
                    }


                    #endregion

                    #region Escape Sequences

                    /* Ai */
                    if (_hMainHandler.PSettings.MaphackRemoveAi)
                    {
                        if (LPlayer[LUnit[i].Owner].Type.Equals(PredefinedTypes.PlayerType.Ai))
                            continue;
                    }

                    /* Allie */
                    if (_hMainHandler.PSettings.MaphackRemoveAllie)
                    {
                        if (LPlayer[0].Localplayer < LPlayer.Count)
                        {
                            if (LPlayer[LUnit[i].Owner].Team ==
                                LPlayer[LPlayer[0].Localplayer].Team &&
                                !LPlayer[LUnit[i].Owner].IsLocalplayer)
                                continue;
                        }
                    }

                    /* Localplayer Units */
                    if (_hMainHandler.PSettings.MaphackRemoveLocalplayer)
                    {
                        if (LUnit[i].Owner == LPlayer[0].Localplayer)
                            continue;
                    }

                    /* Neutral Units */
                    if (_hMainHandler.PSettings.MaphackRemoveNeutral)
                    {
                        if (LPlayer[LUnit[i].Owner].Type.Equals(PredefinedTypes.PlayerType.Neutral))
                            continue;
                    }


                    /* Dead Units */
                    if ((LUnit[i].TargetFilter & (ulong) PredefinedTypes.TargetFilterFlag.Dead) > 0)
                        continue;
                 
                   




                    #endregion


                    var fUnitSize = LUnit[i].CustomStruct.Size;
                    var size = 2.0f;

                    if (fUnitSize >= 0.875)
                        size = 4;

                    if (fUnitSize >= 1.5)
                        size = 6;

                    if (fUnitSize >= 2.0)
                        size = 8;

                    if (fUnitSize >= 2.5)
                        size = 10;

                    size += 0.5f;


                    #region Border special Units

                    g.Graphics.PixelOffsetMode = PixelOffsetMode.HighSpeed;
                    g.Graphics.SmoothingMode = SmoothingMode.HighSpeed;
                    g.Graphics.CompositingQuality = CompositingQuality.HighSpeed;


                    g.Graphics.DrawRectangle(new Pen(new SolidBrush(clUnitBound)), iUnitPosX - size/2,
                                             iUnitPosY - size/2, size, size);


                    g.Graphics.PixelOffsetMode = PixelOffsetMode.HighSpeed;
                    g.Graphics.SmoothingMode = SmoothingMode.HighSpeed;
                    g.Graphics.CompositingQuality = CompositingQuality.HighSpeed;

                    #endregion
                }

                #endregion

                #region Draw Unit (Inner Rectangle)

                for (var i = 0; i < LUnit.Count; i++)
                {
                    //Color clUnit = LUnit[i].Owner > LPlayer.Count ? Color.Transparent : LPlayer[LUnit[i].Owner].Color;

                    if (LUnit[i].Owner >= LPlayer.Count)
                        continue;


                    var clUnit = LPlayer[LUnit[i].Owner].Color;
                   


                    #region Teamcolor

                    if (GInfo.IsTeamcolor)
                    {
                        if (LPlayer[0].Localplayer < LPlayer.Count)
                        {
                            if (LPlayer[LUnit[i].Owner].IsLocalplayer)
                                clUnit = Color.Green;

                            else if (LPlayer[LUnit[i].Owner].Team ==
                                     LPlayer[LPlayer[0].Localplayer].Team &&
                                     !LPlayer[LUnit[i].Owner].IsLocalplayer)
                                clUnit = Color.Yellow;

                            else if (LPlayer[LUnit[i].Owner].Type.Equals(PredefinedTypes.PlayerType.Neutral))
                                clUnit = Color.White;

                            else
                                clUnit = Color.Red;
                        }
                    }

                    #endregion

                    #region Scalling (Unitposition)

                    var iUnitPosX = (LUnit[i].PositionX - GMap.Left)*iScale + iX;
                    var iUnitPosY = (GMap.Top - LUnit[i].PositionY)*iScale + iY;


                    if (float.IsNaN(iUnitPosX) ||
                        float.IsNaN(iUnitPosY))
                    {
                        continue;
                    }

                    #endregion

                    #region Escape Sequences

                    /* Ai */
                    if (_hMainHandler.PSettings.MaphackRemoveAi)
                    {
                        if (LPlayer[LUnit[i].Owner].Type.Equals(PredefinedTypes.PlayerType.Ai))
                            continue;
                    }

                    /* Allie */
                    if (_hMainHandler.PSettings.MaphackRemoveAllie)
                    {
                        if (LPlayer[0].Localplayer < LPlayer.Count)
                        {
                            if (LPlayer[LUnit[i].Owner].Team ==
                                LPlayer[LPlayer[0].Localplayer].Team &&
                                !LPlayer[LUnit[i].Owner].IsLocalplayer)
                                continue;
                        }
                    }

                    /* Localplayer Units */
                    if (_hMainHandler.PSettings.MaphackRemoveLocalplayer)
                    {
                        if (LUnit[i].Owner == LPlayer[0].Localplayer)
                            continue;
                    }

                    /* Neutral Units */
                    if (_hMainHandler.PSettings.MaphackRemoveNeutral)
                    {
                        if (LPlayer[LUnit[i].Owner].Type.Equals(PredefinedTypes.PlayerType.Neutral))
                            continue;
                    }


                    /* Dead Units */
                    if ((LUnit[i].TargetFilter & (ulong) PredefinedTypes.TargetFilterFlag.Dead) > 0)
                        continue;




                    #endregion

                    var fUnitSize = LUnit[i].CustomStruct.Size;
                    var size = 2.0f;

                    if (fUnitSize >= 0.875)
                        size = 4;

                    if (fUnitSize >= 1.5)
                        size = 6;

                    if (fUnitSize >= 2.0)
                        size = 8;

                    if (fUnitSize >= 2.5)
                        size = 10;

                    size -= 0.5f;


                    g.Graphics.PixelOffsetMode = PixelOffsetMode.HighSpeed;
                    g.Graphics.SmoothingMode = SmoothingMode.HighSpeed;
                    g.Graphics.CompositingQuality = CompositingQuality.HighSpeed;

                    /* Draw the Unit (Actual Unit) */
                    g.Graphics.FillRectangle(new SolidBrush(clUnit), iUnitPosX - size/2, iUnitPosY - size/2, size, size);

                    g.Graphics.PixelOffsetMode = PixelOffsetMode.HighSpeed;
                    g.Graphics.SmoothingMode = SmoothingMode.HighSpeed;
                    g.Graphics.CompositingQuality = CompositingQuality.HighSpeed;

                }

                #endregion

                #region Draw Border of special Units

                for (var i = 0; i < LUnit.Count; i++)
                {
                    var clUnitBoundBorder = Color.Black;

                    if (LUnit[i].Owner >= (LPlayer.Count))
                        continue;


                    #region Scalling (Unitposition)

                    var iUnitPosX = (LUnit[i].PositionX - GMap.Left)*iScale + iX;
                    var iUnitPosY = (GMap.Top - LUnit[i].PositionY)*iScale + iY;


                    if (float.IsNaN(iUnitPosX) ||
                        float.IsNaN(iUnitPosY))
                    {
                        continue;
                    }

                    #endregion

                    #region Escape Sequences

                    /* Ai */
                    if (_hMainHandler.PSettings.MaphackRemoveAi)
                    {
                        if (LPlayer[LUnit[i].Owner].Type.Equals(PredefinedTypes.PlayerType.Ai))
                            continue; //clUnitBoundBorder = Color.Transparent;

                    }

                    /* Allie */
                    if (_hMainHandler.PSettings.MaphackRemoveAllie)
                    {
                        if (LPlayer[0].Localplayer < LPlayer.Count)
                        {
                            if (LPlayer[LUnit[i].Owner].Team ==
                                LPlayer[LPlayer[0].Localplayer].Team &&
                                !LPlayer[LUnit[i].Owner].IsLocalplayer)
                                continue; //clUnitBoundBorder = Color.Transparent;

                        }
                    }

                    /* Localplayer Units */
                    if (_hMainHandler.PSettings.MaphackRemoveLocalplayer)
                    {
                        if (LUnit[i].Owner == LPlayer[0].Localplayer)
                            continue; //clUnitBoundBorder = Color.Transparent;

                    }

                    /* Neutral Units */
                    if (_hMainHandler.PSettings.MaphackRemoveNeutral)
                    {
                        if (LPlayer[LUnit[i].Owner].Type.Equals(PredefinedTypes.PlayerType.Neutral))
                            continue; //clUnitBoundBorder = Color.Transparent;

                    }


                    /* Dead Units */
                    if ((LUnit[i].TargetFilter & (ulong) PredefinedTypes.TargetFilterFlag.Dead) > 0)
                        continue;






                    #endregion


                    var fUnitSize = LUnit[i].CustomStruct.Size;
                    var size = 2.0f;

                    if (fUnitSize >= 0.875)
                        size = 4;

                    if (fUnitSize >= 1.5)
                        size = 6;

                    if (fUnitSize >= 2.0)
                        size = 8;

                    if (fUnitSize >= 2.5)
                        size = 10;

                    size += 0.5f;


                    #region Border special Units

                    #region Self created Units

                    for (var j = 0; j < _hMainHandler.PSettings.MaphackUnitIds.Count; j++)
                    {
                        if (LUnit[i].CustomStruct.Id == (int) _hMainHandler.PSettings.MaphackUnitIds[j])
                        {
                            if (_hMainHandler.PSettings.MaphackUnitColors[j] != Color.Transparent)
                            {
                                var clUnit = _hMainHandler.PSettings.MaphackUnitColors[j];
                                if (!LUnit[i].IsAlive)
                                    continue;

                                if (_hMainHandler.PSettings.MaphackRemoveLocalplayer)
                                {
                                    if (LUnit[i].Owner == LPlayer[0].Localplayer)
                                        continue;
                                }

                                g.Graphics.DrawRectangle(
                                    new Pen(new SolidBrush(clUnit), 1.5f),
                                    (iUnitPosX - size/2), (iUnitPosY - size/2), size, size);

                                g.Graphics.DrawRectangle(new Pen(new SolidBrush(clUnitBoundBorder)),
                                                         iUnitPosX - ((size/2) + 0.75f),
                                                         iUnitPosY - ((size/2) + 0.75f), size + 1.75f, size + 1.75f);
                            }
                        }
                    }

                    #endregion

                    #region CreepTumors

                    if (LUnit[i].CustomStruct.Id == (int) PredefinedTypes.UnitId.ZbCreeptumor)
                    {
                        g.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Gray), 1.5f),
                                                 (iUnitPosX - size/2), (iUnitPosY - size/2), size, size);

                        g.Graphics.DrawRectangle(new Pen(new SolidBrush(clUnitBoundBorder)),
                                                 iUnitPosX - ((size/2) + 0.75f),
                                                 iUnitPosY - ((size/2) + 0.75f), size + 1.75f, size + 1.75f);
                    }

                    #endregion

                    #region Unitgroup I - Defensive Buildings

                    if (_hMainHandler.PSettings.MaphackColorDefensivestructuresYellow)
                    {
                        if (LUnit[i].CustomStruct.Id == (int) PredefinedTypes.UnitId.TbTurret ||
                            LUnit[i].CustomStruct.Id == (int) PredefinedTypes.UnitId.TbBunker ||
                            LUnit[i].CustomStruct.Id == (int) PredefinedTypes.UnitId.TbPlanetary ||
                            LUnit[i].CustomStruct.Id == (int) PredefinedTypes.UnitId.ZbSpineCrawler ||
                            LUnit[i].CustomStruct.Id == (int) PredefinedTypes.UnitId.ZbSpineCrawlerUnrooted ||
                            LUnit[i].CustomStruct.Id == (int) PredefinedTypes.UnitId.ZbSporeCrawler ||
                            LUnit[i].CustomStruct.Id == (int) PredefinedTypes.UnitId.ZbSporeCrawlerUnrooted ||
                            LUnit[i].CustomStruct.Id == (int) PredefinedTypes.UnitId.PbCannon)
                        {
                            var clUnitBound = Color.Yellow;


                            if ((LUnit[i].TargetFilter & (UInt64) PredefinedTypes.TargetFilterFlag.Dead) > 0)
                                continue;


                            if (_hMainHandler.PSettings.MaphackRemoveLocalplayer)
                            {
                                if (LUnit[i].Owner == LPlayer[0].Localplayer)
                                    continue;

                            }

                            g.Graphics.DrawRectangle(new Pen(new SolidBrush(clUnitBound), 1.5f),
                                                     (iUnitPosX - size/2), (iUnitPosY - size/2), size, size);

                            g.Graphics.DrawRectangle(new Pen(new SolidBrush(clUnitBoundBorder)),
                                                     iUnitPosX - ((size/2) + 0.75f),
                                                     iUnitPosY - ((size/2) + 0.75f), size + 1.75f, size + 1.75f);
                        }
                    }

                    #endregion

                    #endregion

                }

                #endregion

                #region Draw Player camera

                if (!_hMainHandler.PSettings.MaphackRemoveCamera)
                {
                    for (var i = 0; i < LPlayer.Count; i++)
                    {
                        var clPlayercolor = LPlayer[i].Color;

                        #region Teamcolor

                        if (GInfo.IsTeamcolor)
                        {
                            if (LPlayer[0].Localplayer < LPlayer.Count)
                            {
                                if (LPlayer[i].IsLocalplayer)
                                    clPlayercolor = Color.Green;

                                else if (LPlayer[i].Team ==
                                         LPlayer[LPlayer[0].Localplayer].Team &&
                                         !LPlayer[i].IsLocalplayer)
                                    clPlayercolor = Color.Yellow;

                                else
                                    clPlayercolor = Color.Red;
                            }
                        }

                        #endregion

                        #region Escape Sequences

                        /* Ai - Works */
                        if (_hMainHandler.PSettings.MaphackRemoveAi)
                        {
                            if (LPlayer[i].Type.Equals(PredefinedTypes.PlayerType.Ai))
                                continue;
                        }

                        /* Observer */
                        if (_hMainHandler.PSettings.MaphackRemoveObserver)
                        {
                            if (LPlayer[i].Type.Equals(PredefinedTypes.PlayerType.Observer))
                                continue;
                        }

                        /* Referee */
                        if (_hMainHandler.PSettings.MaphackRemoveReferee)
                        {
                            if (LPlayer[i].Type.Equals(PredefinedTypes.PlayerType.Referee))
                                continue;
                        }

                        /* Localplayer - Works */
                        if (_hMainHandler.PSettings.MaphackRemoveLocalplayer)
                        {
                            if (LPlayer[i].IsLocalplayer)
                                continue;
                        }

                        /* Allie */
                        if (_hMainHandler.PSettings.MaphackRemoveAllie)
                        {
                            if (LPlayer[0].Localplayer < LPlayer.Count)
                            {
                                if (LPlayer[i].Team ==
                                    LPlayer[LPlayer[i].Localplayer].Team &&
                                    !LPlayer[i].IsLocalplayer)
                                    continue;
                            }
                        }

                        /* Neutral */
                        if (_hMainHandler.PSettings.MaphackRemoveNeutral)
                        {
                            if (LPlayer[i].Type.Equals(PredefinedTypes.PlayerType.Neutral))
                                continue;
                        }

                        /* Hosile */
                        if (LPlayer[i].Type.Equals(PredefinedTypes.PlayerType.Hostile))
                            continue;

                        if (float.IsInfinity(iScale))
                            continue;

                        if (CheckIfGameheart(LPlayer[i]))
                            continue;

                        #endregion

                        #region Drawing

                        //The actrual position of the Cameras
                        var iPlayerX = (LPlayer[i].CameraPositionX - GMap.Left)*iScale + iX;
                        var iPlayerY = (GMap.Top - LPlayer[i].CameraPositionY)*iScale + iY;

                        if (iPlayerX <= 0 || iPlayerX >= Width ||
                            iPlayerY <= 0 || iPlayerY >= Height)
                            continue;

                        //if (iPlayerX <= 0 ||
                        //    iPlayerY <= 0)
                        //    continue;

                        var ptPoints = new PointF[4];
                        ptPoints[0] = new PointF((int) iPlayerX - 35, (int) iPlayerY - 24);
                        ptPoints[1] = new PointF((int) iPlayerX + 35, (int) iPlayerY - 24);
                        ptPoints[2] = new PointF((int) iPlayerX + 24, (int) iPlayerY + 10);
                        ptPoints[3] = new PointF((int) iPlayerX - 24, (int) iPlayerY + 10);




                        g.Graphics.DrawPolygon(new Pen(new SolidBrush(clPlayercolor), 2), ptPoints);

                        #endregion

                    }
                }

                #endregion

                #endregion

            }

            catch (Exception ex)
            {
                Messages.LogFile("DrawMinimap", "Over all", ex);
            }

            /* Draw a final bound around the panel */
            if (_bChangingPosition)
            {
                g.Graphics.DrawRectangle(Constants.PYellowGreen2,
                                            1,
                                            1,
                                            Width - 2,
                                            Height - 2);
            }
        }

        /* Count the Units/ structures */
        private void DrawUnits(BufferedGraphics g)
        {
            try
            {

                if (!GInfo.IsIngame)
                    return;

                Opacity = _hMainHandler.PSettings.UnitTabOpacity;

                /* Add the feature that the window (in case you have all races and more units than your display can hold) 
                 * will split the units to the next line */

                /* Count all included units */
                CountUnits(PredefinedTypes.TargetFilterFlag.Dead);

                var iHavetoadd = 0; //Adds +1 when a neutral player is on position 0
                Int32 iSize = _hMainHandler.PSettings.UnitPictureSize;
                var iPosY = 30;
                var iPosX = 0;
                var iMaximumWidth = 0;


                /* Fix the size of the icons to 25x25 */
                for (var i = 0; i < LPlayer.Count; i++)
                {
                    var clPlayercolor = LPlayer[i].Color;

                    #region Teamcolor

                    if (GInfo.IsTeamcolor)
                    {
                        if (LPlayer[i].Localplayer < LPlayer.Count)
                        {
                            if (LPlayer[i].IsLocalplayer)
                                clPlayercolor = Color.Green;

                            else if (LPlayer[i].Team ==
                                     LPlayer[LPlayer[0].Localplayer].Team &&
                                     !LPlayer[i].IsLocalplayer)
                                clPlayercolor = Color.Yellow;

                            else
                                clPlayercolor = Color.Red;
                        }
                    }

                    #endregion

                    #region Exceptions - Throw out players

                    /* Remove Ai - Works */
                    if (_hMainHandler.PSettings.UnitTabRemoveAi)
                    {
                        if (LPlayer[i].Type == PredefinedTypes.PlayerType.Ai)
                        {
                            continue;
                        }
                    }

                    /* Remove Referee - Not Tested */
                    if (_hMainHandler.PSettings.UnitTabRemoveReferee)
                    {
                        if (LPlayer[i].Type == PredefinedTypes.PlayerType.Referee)
                        {
                            continue;
                        }
                    }

                    /* Remove Observer - Not Tested */
                    if (_hMainHandler.PSettings.UnitTabRemoveObserver)
                    {
                        if (LPlayer[i].Type == PredefinedTypes.PlayerType.Observer)
                        {
                            continue;
                        }
                    }

                    /* Remove Neutral - Works */
                    if (_hMainHandler.PSettings.UnitTabRemoveNeutral)
                    {
                        if (LPlayer[i].Type == PredefinedTypes.PlayerType.Neutral)
                        {
                            continue;
                        }
                    }

                    /* Remove Localplayer - Works */
                    if (_hMainHandler.PSettings.UnitTabRemoveLocalplayer)
                    {
                        if (LPlayer[i].IsLocalplayer)
                        {
                            continue;
                        }
                    }

                    /* Remove Allie - Works */
                    if (_hMainHandler.PSettings.UnitTabRemoveAllie)
                    {
                        if (LPlayer[i].Team == LPlayer[LPlayer[i].Localplayer].Team &&
                            !LPlayer[i].IsLocalplayer)
                        {
                            continue;
                        }
                    }

                    if (LPlayer[i].Type == PredefinedTypes.PlayerType.Hostile)
                        continue;



                    #endregion

                    if (LPlayer[i].Name.Length <= 0 ||
                        LPlayer[i].Name.StartsWith("\0"))
                        continue;

                    if (CheckIfGameheart(LPlayer[i]))
                        continue;

                    iPosX = 0;

                    /* Draw Name in front of Icons */
                    g.Graphics.DrawString(LPlayer[i].Name, Constants.FArial1, new SolidBrush(clPlayercolor), iPosX + 10,
                                          iPosY + 10);
                    iPosX = 150;

                    #region Draw Units

                    /* Terran */
                    Helper_DrawUnits(_lTuScv[i].UnitAmount, ref iPosX, iPosY, iSize, _imgTuScv, g, clPlayercolor);
                    Helper_DrawUnits(_lTuMarine[i].UnitAmount, ref iPosX, iPosY, iSize, _imgTuMarine, g, clPlayercolor);
                    Helper_DrawUnits(_lTuMarauder[i].UnitAmount, ref iPosX, iPosY, iSize, _imgTuMarauder, g,
                                     clPlayercolor);
                    Helper_DrawUnits(_lTuReaper[i].UnitAmount, ref iPosX, iPosY, iSize, _imgTuReaper, g, clPlayercolor);
                    Helper_DrawUnits(_lTuGhost[i].UnitAmount, ref iPosX, iPosY, iSize, _imgTuGhost, g, clPlayercolor);
                    Helper_DrawUnits(_lTuMule[i].UnitAmount, ref iPosX, iPosY, iSize, _imgTuMule, g, clPlayercolor);
                    Helper_DrawUnits(_lTuHellion[i].UnitAmount, ref iPosX, iPosY, iSize, _imgTuHellion, g, clPlayercolor);
                    Helper_DrawUnits(_lTuHellbat[i].UnitAmount, ref iPosX, iPosY, iSize, _imgTuHellbat, g, clPlayercolor);
                    Helper_DrawUnits(_lTuWidowMine[i].UnitAmount, ref iPosX, iPosY, iSize, _imgTuWidowMine, g,
                                     clPlayercolor);
                    Helper_DrawUnits(_lTuSiegetank[i].UnitAmount, ref iPosX, iPosY, iSize, _imgTuSiegetank, g,
                                     clPlayercolor);
                    Helper_DrawUnits(_lTuThor[i].UnitAmount, ref iPosX, iPosY, iSize, _imgTuThor, g, clPlayercolor);
                    Helper_DrawUnits(_lTuMedivac[i].UnitAmount, ref iPosX, iPosY, iSize, _imgTuMedivac, g, clPlayercolor);
                    Helper_DrawUnits(_lTuBanshee[i].UnitAmount, ref iPosX, iPosY, iSize, _imgTuBanshee, g, clPlayercolor);
                    Helper_DrawUnits(_lTuViking[i].UnitAmount, ref iPosX, iPosY, iSize, _imgTuViking, g, clPlayercolor);
                    Helper_DrawUnits(_lTuRaven[i].UnitAmount, ref iPosX, iPosY, iSize, _imgTuRaven, g, clPlayercolor);
                    Helper_DrawUnits(_lTuBattlecruiser[i].UnitAmount, ref iPosX, iPosY, iSize, _imgTuBattlecruiser, g,
                                     clPlayercolor);

                    /* Protoss */
                    Helper_DrawUnits(_lPuProbe[i].UnitAmount, ref iPosX, iPosY, iSize, _imgPuProbe, g, clPlayercolor);
                    Helper_DrawUnits(_lPuZealot[i].UnitAmount, ref iPosX, iPosY, iSize, _imgPuZealot, g, clPlayercolor);
                    Helper_DrawUnits(_lPuStalker[i].UnitAmount, ref iPosX, iPosY, iSize, _imgPuStalker, g, clPlayercolor);
                    Helper_DrawUnits(_lPuSentry[i].UnitAmount, ref iPosX, iPosY, iSize, _imgPuSentry, g, clPlayercolor);
                    Helper_DrawUnits(_lPuDt[i].UnitAmount, ref iPosX, iPosY, iSize, _imgPuDarkTemplar, g, clPlayercolor);
                    Helper_DrawUnits(_lPuHt[i].UnitAmount, ref iPosX, iPosY, iSize, _imgPuHighTemplar, g, clPlayercolor);
                    Helper_DrawUnits(_lPuArchon[i].UnitAmount, ref iPosX, iPosY, iSize, _imgPuArchon, g, clPlayercolor);
                    Helper_DrawUnits(_lPuImmortal[i].UnitAmount, ref iPosX, iPosY, iSize, _imgPuImmortal, g,
                                     clPlayercolor);
                    Helper_DrawUnits(_lPuColossus[i].UnitAmount, ref iPosX, iPosY, iSize, _imgPuColossus, g,
                                     clPlayercolor);
                    Helper_DrawUnits(_lPuObserver[i].UnitAmount, ref iPosX, iPosY, iSize, _imgPuObserver, g,
                                     clPlayercolor);
                    Helper_DrawUnits(_lPuWarpprism[i].UnitAmount, ref iPosX, iPosY, iSize, _imgPuWapprism, g,
                                     clPlayercolor);
                    Helper_DrawUnits(_lPuPhoenix[i].UnitAmount, ref iPosX, iPosY, iSize, _imgPuPhoenix, g, clPlayercolor);
                    Helper_DrawUnits(_lPuVoidray[i].UnitAmount, ref iPosX, iPosY, iSize, _imgPuVoidray, g, clPlayercolor);
                    Helper_DrawUnits(_lPuOracle[i].UnitAmount, ref iPosX, iPosY, iSize, _imgPuOracle, g, clPlayercolor);
                    Helper_DrawUnits(_lPuCarrier[i].UnitAmount, ref iPosX, iPosY, iSize, _imgPuCarrier, g, clPlayercolor);
                    Helper_DrawUnits(_lPuTempest[i].UnitAmount, ref iPosX, iPosY, iSize, _imgPuTempest, g, clPlayercolor);
                    Helper_DrawUnits(_lPuMothershipcore[i].UnitAmount, ref iPosX, iPosY, iSize, _imgPuMothershipcore, g,
                                     clPlayercolor);
                    Helper_DrawUnits(_lPuMothership[i].UnitAmount, ref iPosX, iPosY, iSize, _imgPuMothership, g,
                                     clPlayercolor);

                    /* Zerg */
                    Helper_DrawUnits(_lZuLarva[i].UnitAmount, ref iPosX, iPosY, iSize, _imgZuLarva, g, clPlayercolor);
                    Helper_DrawUnits(_lZuDrone[i].UnitAmount, ref iPosX, iPosY, iSize, _imgZuDrone, g, clPlayercolor);
                    Helper_DrawUnits(_lZuOverlord[i].UnitAmount, ref iPosX, iPosY, iSize, _imgZuOverlord, g,
                                     clPlayercolor);
                    Helper_DrawUnits(_lZuQueen[i].UnitAmount, ref iPosX, iPosY, iSize, _imgZuQueen, g, clPlayercolor);
                    Helper_DrawUnits(_lZuZergling[i].UnitAmount, ref iPosX, iPosY, iSize, _imgZuZergling, g,
                                     clPlayercolor);
                    Helper_DrawUnits(_lZuBaneling[i].UnitAmount, ref iPosX, iPosY, iSize, _imgZuBaneling, g,
                                     clPlayercolor);
                    Helper_DrawUnits(_lZuBanelingCocoon[i].UnitAmount, ref iPosX, iPosY, iSize, _imgZuBanelingCocoon, g,
                                     clPlayercolor);
                    Helper_DrawUnits(_lZuRoach[i].UnitAmount, ref iPosX, iPosY, iSize, _imgZuRoach, g, clPlayercolor);
                    Helper_DrawUnits(_lZuHydra[i].UnitAmount, ref iPosX, iPosY, iSize, _imgZuHydra, g, clPlayercolor);
                    Helper_DrawUnits(_lZuMutalisk[i].UnitAmount, ref iPosX, iPosY, iSize, _imgZuMutalisk, g,
                                     clPlayercolor);
                    Helper_DrawUnits(_lZuInfestor[i].UnitAmount, ref iPosX, iPosY, iSize, _imgZuInfestor, g,
                                     clPlayercolor);
                    Helper_DrawUnits(_lZuOverseer[i].UnitAmount, ref iPosX, iPosY, iSize, _imgZuOverseer, g,
                                     clPlayercolor);
                    Helper_DrawUnits(_lZuOverseerCocoon[i].UnitAmount, ref iPosX, iPosY, iSize, _imgZuOvserseerCocoon, g,
                                     clPlayercolor);
                    Helper_DrawUnits(_lZuSwarmhost[i].UnitAmount, ref iPosX, iPosY, iSize, _imgZuSwarmhost, g,
                                     clPlayercolor);
                    Helper_DrawUnits(_lZuUltralist[i].UnitAmount, ref iPosX, iPosY, iSize, _imgZuUltra, g, clPlayercolor);
                    Helper_DrawUnits(_lZuViper[i].UnitAmount, ref iPosX, iPosY, iSize, _imgZuViper, g, clPlayercolor);
                    Helper_DrawUnits(_lZuCorruptor[i].UnitAmount, ref iPosX, iPosY, iSize, _imgZuCorruptor, g,
                                     clPlayercolor);
                    Helper_DrawUnits(_lZuBroodlord[i].UnitAmount, ref iPosX, iPosY, iSize, _imgZuBroodlord, g,
                                     clPlayercolor);
                    Helper_DrawUnits(_lZuBroodlordCocoon[i].UnitAmount, ref iPosX, iPosY, iSize, _imgZuBroodlordCocoon,
                                     g, clPlayercolor);

                    #endregion

                    #region - Split Units and Buildings -

                    if (_hMainHandler.PSettings.UnitTabSplitUnitsAndBuildings)
                    {
                        iHavetoadd = 0;


                        if (LPlayer[0].Type == PredefinedTypes.PlayerType.Neutral)
                            iHavetoadd += 1;

                        if (i == iHavetoadd)
                        {
                            iPosY += iSize + 2;
                            iPosX = 150;
                        }

                        else if (i > iHavetoadd)
                        {
                            iPosY += iSize + 2;
                            iPosX = 150;
                        }
                    }

                    #endregion

                    #region Draw Buildings

                    /* Terran */
                    Helper_DrawUnits(_lTbCommandCenter[i].UnitAmount, ref iPosX, iPosY, iSize, _imgTbCc, g,
                                     clPlayercolor);
                    Helper_DrawUnits(_lTbOrbitalCommand[i].UnitAmount, ref iPosX, iPosY, iSize, _imgTbOc, g,
                                     clPlayercolor);
                    Helper_DrawUnits(_lTbPlanetaryFortress[i].UnitAmount, ref iPosX, iPosY, iSize, _imgTbPf, g,
                                     clPlayercolor);
                    Helper_DrawUnits(_lTbSupply[i].UnitAmount, ref iPosX, iPosY, iSize, _imgTbSupply, g, clPlayercolor);
                    Helper_DrawUnits(_lTbRefinery[i].UnitAmount, ref iPosX, iPosY, iSize, _imgTbRefinery, g,
                                     clPlayercolor);
                    Helper_DrawUnits(_lTbBunker[i].UnitAmount, ref iPosX, iPosY, iSize, _imgTbBunker, g, clPlayercolor);
                    Helper_DrawUnits(_lTbTechlab[i].UnitAmount, ref iPosX, iPosY, iSize, _imgTbTechlab, g, clPlayercolor);
                    Helper_DrawUnits(_lTbReactor[i].UnitAmount, ref iPosX, iPosY, iSize, _imgTbReactor, g, clPlayercolor);
                    Helper_DrawUnits(_lTbTurrent[i].UnitAmount, ref iPosX, iPosY, iSize, _imgTbTurrent, g, clPlayercolor);
                    Helper_DrawUnits(_lTbSensorTower[i].UnitAmount, ref iPosX, iPosY, iSize, _imgTbSensorTower, g,
                                     clPlayercolor);
                    Helper_DrawUnits(_lTbEbay[i].UnitAmount, ref iPosX, iPosY, iSize, _imgTbEbay, g, clPlayercolor);
                    Helper_DrawUnits(_lTbGhostAcademy[i].UnitAmount, ref iPosX, iPosY, iSize, _imgTbGhostacademy, g,
                                     clPlayercolor);
                    Helper_DrawUnits(_lTbArmory[i].UnitAmount, ref iPosX, iPosY, iSize, _imgTbArmory, g, clPlayercolor);
                    Helper_DrawUnits(_lTbFusionCore[i].UnitAmount, ref iPosX, iPosY, iSize, _imgTbFusioncore, g,
                                     clPlayercolor);
                    Helper_DrawUnits(_lTbBarracks[i].UnitAmount, ref iPosX, iPosY, iSize, _imgTbBarracks, g,
                                     clPlayercolor);
                    Helper_DrawUnits(_lTbFactory[i].UnitAmount, ref iPosX, iPosY, iSize, _imgTbFactory, g, clPlayercolor);
                    Helper_DrawUnits(_lTbStarport[i].UnitAmount, ref iPosX, iPosY, iSize, _imgTbStarport, g,
                                     clPlayercolor);

                    /* Protoss */
                    Helper_DrawUnits(_lPbNexus[i].UnitAmount, ref iPosX, iPosY, iSize, _imgPbNexus, g, clPlayercolor);
                    Helper_DrawUnits(_lPbPylong[i].UnitAmount, ref iPosX, iPosY, iSize, _imgPbPylon, g, clPlayercolor);
                    Helper_DrawUnits(_lPbAssimilator[i].UnitAmount, ref iPosX, iPosY, iSize, _imgPbAssimilator, g,
                                     clPlayercolor);
                    Helper_DrawUnits(_lPbCannon[i].UnitAmount, ref iPosX, iPosY, iSize, _imgPbCannon, g, clPlayercolor);
                    Helper_DrawUnits(_lPbDarkshrine[i].UnitAmount, ref iPosX, iPosY, iSize, _imgPbDarkShrine, g,
                                     clPlayercolor);
                    Helper_DrawUnits(_lPbTemplarArchives[i].UnitAmount, ref iPosX, iPosY, iSize, _imgPbTemplarArchives,
                                     g, clPlayercolor);
                    Helper_DrawUnits(_lPbTwilight[i].UnitAmount, ref iPosX, iPosY, iSize, _imgPbTwillightCouncil, g,
                                     clPlayercolor);
                    Helper_DrawUnits(_lPbCybercore[i].UnitAmount, ref iPosX, iPosY, iSize, _imgPbCybercore, g,
                                     clPlayercolor);
                    Helper_DrawUnits(_lPbForge[i].UnitAmount, ref iPosX, iPosY, iSize, _imgPbForge, g, clPlayercolor);
                    Helper_DrawUnits(_lPbFleetbeacon[i].UnitAmount, ref iPosX, iPosY, iSize, _imgPbFleetBeacon, g,
                                     clPlayercolor);
                    Helper_DrawUnits(_lPbRoboticsSupport[i].UnitAmount, ref iPosX, iPosY, iSize, _imgPbRoboticsSupport,
                                     g, clPlayercolor);
                    Helper_DrawUnits(_lPbGateway[i].UnitAmount, ref iPosX, iPosY, iSize, _imgPbGateway, g, clPlayercolor);
                    Helper_DrawUnits(_lPbWarpgate[i].UnitAmount, ref iPosX, iPosY, iSize, _imgPbWarpgate, g,
                                     clPlayercolor);
                    Helper_DrawUnits(_lPbStargate[i].UnitAmount, ref iPosX, iPosY, iSize, _imgPbStargate, g,
                                     clPlayercolor);
                    Helper_DrawUnits(_lPbRobotics[i].UnitAmount, ref iPosX, iPosY, iSize, _imgPbRobotics, g,
                                     clPlayercolor);

                    /* Zerg */
                    Helper_DrawUnits(_lZbHatchery[i].UnitAmount, ref iPosX, iPosY, iSize, _imgZbHatchery, g,
                                     clPlayercolor);
                    Helper_DrawUnits(_lZbLair[i].UnitAmount, ref iPosX, iPosY, iSize, _imgZbLair, g, clPlayercolor);
                    Helper_DrawUnits(_lZbHive[i].UnitAmount, ref iPosX, iPosY, iSize, _imgZbHive, g, clPlayercolor);
                    Helper_DrawUnits(_lZbSpawningpool[i].UnitAmount, ref iPosX, iPosY, iSize, _imgZbSpawningpool, g,
                                     clPlayercolor);
                    Helper_DrawUnits(_lZbEvochamber[i].UnitAmount, ref iPosX, iPosY, iSize, _imgZbEvochamber, g,
                                     clPlayercolor);
                    Helper_DrawUnits(_lZbExtractor[i].UnitAmount, ref iPosX, iPosY, iSize, _imgZbExtractor, g,
                                     clPlayercolor);
                    Helper_DrawUnits(_lZbSpine[i].UnitAmount, ref iPosX, iPosY, iSize, _imgZbSpinecrawler, g,
                                     clPlayercolor);
                    Helper_DrawUnits(_lZbSpore[i].UnitAmount, ref iPosX, iPosY, iSize, _imgZbSporecrawler, g,
                                     clPlayercolor);
                    Helper_DrawUnits(_lZbHydraden[i].UnitAmount, ref iPosX, iPosY, iSize, _imgZbHydraden, g,
                                     clPlayercolor);
                    Helper_DrawUnits(_lZbRoachwarren[i].UnitAmount, ref iPosX, iPosY, iSize, _imgZbRoachwarren, g,
                                     clPlayercolor);
                    Helper_DrawUnits(_lZbSpire[i].UnitAmount, ref iPosX, iPosY, iSize, _imgZbSpire, g, clPlayercolor);
                    Helper_DrawUnits(_lZbGreaterspire[i].UnitAmount, ref iPosX, iPosY, iSize, _imgZbGreaterspire, g,
                                     clPlayercolor);
                    Helper_DrawUnits(_lZbUltracavern[i].UnitAmount, ref iPosX, iPosY, iSize, _imgZbUltracavern, g,
                                     clPlayercolor);
                    Helper_DrawUnits(_lZbInfestationpit[i].UnitAmount, ref iPosX, iPosY, iSize, _imgZbInfestationpit, g,
                                     clPlayercolor);
                    Helper_DrawUnits(_lZbBanelingnest[i].UnitAmount, ref iPosX, iPosY, iSize, _imgZbBanelingnest, g,
                                     clPlayercolor);
                    Helper_DrawUnits(_lZbNydusbegin[i].UnitAmount, ref iPosX, iPosY, iSize, _imgZbNydusNetwork, g,
                                     clPlayercolor);
                    Helper_DrawUnits(_lZbNydusend[i].UnitAmount, ref iPosX, iPosY, iSize, _imgZbNydusWorm, g,
                                     clPlayercolor);

                    #endregion

                    iPosY += iSize + 2;

                    if (iPosX + iSize >= iMaximumWidth)
                        iMaximumWidth = iPosX + iSize;

                    if (iHavetoadd > 0)
                        iMaximumWidth += iSize;
                }

                Width = iMaximumWidth + 1;
                Height = iPosY + iSize;

            }

            catch (Exception ex)
            {
                Messages.LogFile("DrawUnits", "Over all", ex);
            }

            /* Draw a final bound around the panel */
            if (_bChangingPosition)
            {
                g.Graphics.DrawRectangle(Constants.PYellowGreen2,
                                            1,
                                            1,
                                            Width - 2,
                                            Height - 2);
            }
        }

        /* Draws the productionpanel */
        private void DrawProductionPanel(BufferedGraphics g)
        {
            if (!GInfo.IsIngame)
                return;

            /* Count all included units */
            CountUnits(PredefinedTypes.TargetFilterFlag.UnderConstruction);

            var iHavetoadd = 0;           //Adds +1 when a neutral player is on position 0
            const Int32 iSize = 45;
            var iPosY = 30;
            var iPosX = 0;
            var iMaximumWidth = 0;


            /* Fix the size of the icons to 25x25 */
            for (var i = 0; i < LPlayer.Count; i++)
            {
                var clPlayercolor = LPlayer[i].Color;

                #region Teamcolor

                if (GInfo.IsTeamcolor)
                {
                    if (LPlayer[i].Localplayer < LPlayer.Count)
                    {
                        if (LPlayer[i].IsLocalplayer)
                            clPlayercolor = Color.Green;

                        else if (LPlayer[i].Team ==
                                 LPlayer[LPlayer[0].Localplayer].Team &&
                                 !LPlayer[i].IsLocalplayer)
                            clPlayercolor = Color.Yellow;

                        else
                            clPlayercolor = Color.Red;
                    }
                }

                #endregion

                #region Exceptions - Throw out players

                /* Remove Ai - Works */
                if (_hMainHandler.PSettings.UnitTabRemoveAi)
                {
                    if (LPlayer[i].Type == PredefinedTypes.PlayerType.Ai)
                    {
                        continue;
                    }
                }

                /* Remove Referee - Not Tested */
                if (_hMainHandler.PSettings.UnitTabRemoveReferee)
                {
                    if (LPlayer[i].Type == PredefinedTypes.PlayerType.Referee)
                    {
                        continue;
                    }
                }

                /* Remove Observer - Not Tested */
                if (_hMainHandler.PSettings.UnitTabRemoveObserver)
                {
                    if (LPlayer[i].Type == PredefinedTypes.PlayerType.Observer)
                    {
                        continue;
                    }
                }

                /* Remove Neutral - Works */
                if (_hMainHandler.PSettings.UnitTabRemoveNeutral)
                {
                    if (LPlayer[i].Type == PredefinedTypes.PlayerType.Neutral)
                    {
                        continue;
                    }
                }

                /* Remove Localplayer - Works */
                if (_hMainHandler.PSettings.UnitTabRemoveLocalplayer)
                {
                    if (LPlayer[i].IsLocalplayer)
                    {
                        continue;
                    }
                }

                /* Remove Allie - Works */
                if (_hMainHandler.PSettings.UnitTabRemoveAllie)
                {
                    if (LPlayer[i].Team == LPlayer[LPlayer[i].Localplayer].Team &&
                        !LPlayer[i].IsLocalplayer)
                    {
                        continue;
                    }
                }

                if (LPlayer[i].Type == PredefinedTypes.PlayerType.Hostile)
                    continue;



                #endregion

                if (LPlayer[i].Name.Length <= 0 ||
                    LPlayer[i].Name.StartsWith("\0"))
                    continue;

                iPosX = 0;

                /* Draw Name in front of Icons */
                g.Graphics.DrawString(LPlayer[i].Name, Constants.FArial1, new SolidBrush(clPlayercolor), iPosX + 10, iPosY + 10);
                iPosX = 150;

                #region Draw Units

                /* Terran */
                Helper_DrawUnits(_lTuScv[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgTuScv, g, clPlayercolor);
                Helper_DrawUnits(_lTuMarine[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgTuMarine, g, clPlayercolor);
                Helper_DrawUnits(_lTuMarauder[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgTuMarauder, g, clPlayercolor);
                Helper_DrawUnits(_lTuReaper[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgTuReaper, g, clPlayercolor);
                Helper_DrawUnits(_lTuGhost[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgTuGhost, g, clPlayercolor);
                Helper_DrawUnits(_lTuMule[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgTuMule, g, clPlayercolor);
                Helper_DrawUnits(_lTuHellion[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgTuHellion, g, clPlayercolor);
                Helper_DrawUnits(_lTuHellbat[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgTuHellbat, g, clPlayercolor);
                Helper_DrawUnits(_lTuWidowMine[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgTuWidowMine, g, clPlayercolor);
                Helper_DrawUnits(_lTuSiegetank[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgTuSiegetank, g, clPlayercolor);
                Helper_DrawUnits(_lTuThor[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgTuThor, g, clPlayercolor);
                Helper_DrawUnits(_lTuMedivac[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgTuMedivac, g, clPlayercolor);
                Helper_DrawUnits(_lTuBanshee[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgTuBanshee, g, clPlayercolor);
                Helper_DrawUnits(_lTuViking[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgTuViking, g, clPlayercolor);
                Helper_DrawUnits(_lTuRaven[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgTuRaven, g, clPlayercolor);
                Helper_DrawUnits(_lTuBattlecruiser[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgTuBattlecruiser, g, clPlayercolor);

                /* Protoss */
                Helper_DrawUnits(_lPuProbe[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgPuProbe, g, clPlayercolor);
                Helper_DrawUnits(_lPuZealot[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgPuZealot, g, clPlayercolor);
                Helper_DrawUnits(_lPuStalker[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgPuStalker, g, clPlayercolor);
                Helper_DrawUnits(_lPuSentry[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgPuSentry, g, clPlayercolor);
                Helper_DrawUnits(_lPuDt[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgPuDarkTemplar, g, clPlayercolor);
                Helper_DrawUnits(_lPuHt[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgPuHighTemplar, g, clPlayercolor);
                Helper_DrawUnits(_lPuArchon[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgPuArchon, g, clPlayercolor);
                Helper_DrawUnits(_lPuImmortal[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgPuImmortal, g, clPlayercolor);
                Helper_DrawUnits(_lPuColossus[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgPuColossus, g, clPlayercolor);
                Helper_DrawUnits(_lPuObserver[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgPuObserver, g, clPlayercolor);
                Helper_DrawUnits(_lPuWarpprism[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgPuWapprism, g, clPlayercolor);
                Helper_DrawUnits(_lPuPhoenix[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgPuPhoenix, g, clPlayercolor);
                Helper_DrawUnits(_lPuVoidray[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgPuVoidray, g, clPlayercolor);
                Helper_DrawUnits(_lPuOracle[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgPuOracle, g, clPlayercolor);
                Helper_DrawUnits(_lPuCarrier[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgPuCarrier, g, clPlayercolor);
                Helper_DrawUnits(_lPuTempest[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgPuTempest, g, clPlayercolor);
                Helper_DrawUnits(_lPuMothershipcore[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgPuMothershipcore, g, clPlayercolor);
                Helper_DrawUnits(_lPuMothership[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgPuMothership, g, clPlayercolor);

                /* Zerg */
                Helper_DrawUnits(_lZuLarva[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgZuLarva, g, clPlayercolor);
                Helper_DrawUnits(_lZuDrone[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgZuDrone, g, clPlayercolor);
                Helper_DrawUnits(_lZuOverlord[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgZuOverlord, g, clPlayercolor);
                Helper_DrawUnits(_lZuQueen[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgZuQueen, g, clPlayercolor);
                Helper_DrawUnits(_lZuZergling[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgZuZergling, g, clPlayercolor);
                Helper_DrawUnits(_lZuBaneling[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgZuBaneling, g, clPlayercolor);
                Helper_DrawUnits(_lZuRoach[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgZuRoach, g, clPlayercolor);
                Helper_DrawUnits(_lZuHydra[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgZuHydra, g, clPlayercolor);
                Helper_DrawUnits(_lZuMutalisk[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgZuMutalisk, g, clPlayercolor);
                Helper_DrawUnits(_lZuInfestor[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgZuInfestor, g, clPlayercolor);
                Helper_DrawUnits(_lZuOverseer[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgZuOverseer, g, clPlayercolor);
                Helper_DrawUnits(_lZuSwarmhost[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgZuSwarmhost, g, clPlayercolor);
                Helper_DrawUnits(_lZuUltralist[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgZuUltra, g, clPlayercolor);
                Helper_DrawUnits(_lZuViper[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgZuViper, g, clPlayercolor);
                Helper_DrawUnits(_lZuCorruptor[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgZuCorruptor, g, clPlayercolor);
                Helper_DrawUnits(_lZuBroodlord[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgZuBroodlord, g, clPlayercolor);

                #endregion

                #region - Split Units and Buildings -

                if (_hMainHandler.PSettings.UnitTabSplitUnitsAndBuildings)
                {
                    iHavetoadd = 0;


                    if (LPlayer[0].Type == PredefinedTypes.PlayerType.Neutral)
                        iHavetoadd += 1;

                    if (i == iHavetoadd)
                    {
                        iPosY += iSize + 2;
                        iPosX = 150;
                    }

                    else if (i > iHavetoadd)
                    {
                        iPosY += iSize + 2;
                        iPosX = 150;
                    }
                }

                #endregion

                #region Draw Buildings

                /* Terran */
                Helper_DrawUnits(_lTbCommandCenter[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgTbCc, g, clPlayercolor, _lTbCommandCenter[i].ConstructionState);
                Helper_DrawUnits(_lTbOrbitalCommand[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgTbOc, g, clPlayercolor, _lTbOrbitalCommand[i].ConstructionState);
                Helper_DrawUnits(_lTbPlanetaryFortress[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgTbPf, g, clPlayercolor, _lTbPlanetaryFortress[i].ConstructionState);
                Helper_DrawUnits(_lTbSupply[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgTbSupply, g, clPlayercolor, _lTbSupply[i].ConstructionState);
                Helper_DrawUnits(_lTbRefinery[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgTbRefinery, g, clPlayercolor, _lTbRefinery[i].ConstructionState);
                Helper_DrawUnits(_lTbBunker[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgTbBunker, g, clPlayercolor, _lTbBunker[i].ConstructionState);
                Helper_DrawUnits(_lTbTechlab[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgTbTechlab, g, clPlayercolor, _lTbTechlab[i].ConstructionState);
                Helper_DrawUnits(_lTbReactor[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgTbReactor, g, clPlayercolor, _lTbReactor[i].ConstructionState);
                Helper_DrawUnits(_lTbTurrent[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgTbTurrent, g, clPlayercolor, _lTbTurrent[i].ConstructionState);
                Helper_DrawUnits(_lTbSensorTower[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgTbSensorTower, g, clPlayercolor, _lTbSensorTower[i].ConstructionState);
                Helper_DrawUnits(_lTbEbay[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgTbEbay, g, clPlayercolor, _lTbEbay[i].ConstructionState);
                Helper_DrawUnits(_lTbGhostAcademy[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgTbGhostacademy, g, clPlayercolor, _lTbGhostAcademy[i].ConstructionState);
                Helper_DrawUnits(_lTbArmory[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgTbArmory, g, clPlayercolor, _lTbArmory[i].ConstructionState);
                Helper_DrawUnits(_lTbFusionCore[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgTbFusioncore, g, clPlayercolor, _lTbFusionCore[i].ConstructionState);
                Helper_DrawUnits(_lTbBarracks[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgTbBarracks, g, clPlayercolor, _lTbBarracks[i].ConstructionState);
                Helper_DrawUnits(_lTbFactory[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgTbFactory, g, clPlayercolor, _lTbFactory[i].ConstructionState);
                Helper_DrawUnits(_lTbStarport[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgTbStarport, g, clPlayercolor, _lTbStarport[i].ConstructionState);

                /* Protoss */
                Helper_DrawUnits(_lPbNexus[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgPbNexus, g, clPlayercolor, _lPbNexus[i].ConstructionState);
                Helper_DrawUnits(_lPbPylong[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgPbPylon, g, clPlayercolor, _lPbPylong[i].ConstructionState);
                Helper_DrawUnits(_lPbAssimilator[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgPbAssimilator, g, clPlayercolor, _lPbAssimilator[i].ConstructionState);
                Helper_DrawUnits(_lPbCannon[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgPbCannon, g, clPlayercolor, _lPbCannon[i].ConstructionState);
                Helper_DrawUnits(_lPbDarkshrine[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgPbDarkShrine, g, clPlayercolor, _lPbDarkshrine[i].ConstructionState);
                Helper_DrawUnits(_lPbTemplarArchives[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgPbTemplarArchives, g, clPlayercolor, _lPbTemplarArchives[i].ConstructionState);
                Helper_DrawUnits(_lPbTwilight[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgPbTwillightCouncil, g, clPlayercolor, _lPbTwilight[i].ConstructionState);
                Helper_DrawUnits(_lPbCybercore[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgPbCybercore, g, clPlayercolor, _lPbCybercore[i].ConstructionState);
                Helper_DrawUnits(_lPbForge[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgPbForge, g, clPlayercolor, _lPbForge[i].ConstructionState);
                Helper_DrawUnits(_lPbFleetbeacon[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgPbFleetBeacon, g, clPlayercolor, _lPbFleetbeacon[i].ConstructionState);
                Helper_DrawUnits(_lPbRoboticsSupport[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgPbRoboticsSupport, g, clPlayercolor, _lPbRoboticsSupport[i].ConstructionState);
                Helper_DrawUnits(_lPbGateway[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgPbGateway, g, clPlayercolor, _lPbGateway[i].ConstructionState);
                Helper_DrawUnits(_lPbWarpgate[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgPbWarpgate, g, clPlayercolor, _lPbWarpgate[i].ConstructionState);
                Helper_DrawUnits(_lPbStargate[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgPbStargate, g, clPlayercolor, _lPbStargate[i].ConstructionState);
                Helper_DrawUnits(_lPbRobotics[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgPbRobotics, g, clPlayercolor, _lPbRobotics[i].ConstructionState);

                /* Zerg */
                Helper_DrawUnits(_lZbHatchery[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgZbHatchery, g, clPlayercolor, _lZbHatchery[i].ConstructionState);
                Helper_DrawUnits(_lZbLair[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgZbLair, g, clPlayercolor, _lZbLair[i].ConstructionState);
                Helper_DrawUnits(_lZbHive[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgZbHive, g, clPlayercolor, _lZbHive[i].ConstructionState);
                Helper_DrawUnits(_lZbSpawningpool[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgZbSpawningpool, g, clPlayercolor, _lZbSpawningpool[i].ConstructionState);
                Helper_DrawUnits(_lZbEvochamber[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgZbEvochamber, g, clPlayercolor, _lZbEvochamber[i].ConstructionState);
                Helper_DrawUnits(_lZbExtractor[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgZbExtractor, g, clPlayercolor, _lZbExtractor[i].ConstructionState);
                Helper_DrawUnits(_lZbSpine[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgZbSpinecrawler, g, clPlayercolor, _lZbSpine[i].ConstructionState);
                Helper_DrawUnits(_lZbSpore[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgZbSporecrawler, g, clPlayercolor, _lZbSpore[i].ConstructionState);
                Helper_DrawUnits(_lZbHydraden[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgZbHydraden, g, clPlayercolor, _lZbHydraden[i].ConstructionState);
                Helper_DrawUnits(_lZbRoachwarren[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgZbRoachwarren, g, clPlayercolor, _lZbRoachwarren[i].ConstructionState);
                Helper_DrawUnits(_lZbSpire[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgZbSpire, g, clPlayercolor, _lZbSpire[i].ConstructionState);
                Helper_DrawUnits(_lZbGreaterspire[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgZbGreaterspire, g, clPlayercolor, _lZbGreaterspire[i].ConstructionState);
                Helper_DrawUnits(_lZbUltracavern[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgZbUltracavern, g, clPlayercolor, _lZbUltracavern[i].ConstructionState);
                Helper_DrawUnits(_lZbInfestationpit[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgZbInfestationpit, g, clPlayercolor, _lZbInfestationpit[i].ConstructionState);
                Helper_DrawUnits(_lZbBanelingnest[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgZbBanelingnest, g, clPlayercolor, _lZbBanelingnest[i].ConstructionState);
                Helper_DrawUnits(_lZbNydusbegin[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgZbNydusNetwork, g, clPlayercolor, _lZbNydusbegin[i].ConstructionState);
                Helper_DrawUnits(_lZbNydusend[i].UnitUnderConsruction, ref iPosX, iPosY, iSize, _imgZbNydusWorm, g, clPlayercolor, _lZbNydusend[i].ConstructionState);

                #endregion

                iPosY += iSize + 2;

                if (iPosX + iSize >= iMaximumWidth)
                    iMaximumWidth = iPosX + iSize;

                if (iHavetoadd > 0)
                    iMaximumWidth += iSize;
            }

            Width = iMaximumWidth + 1;
            Height = iPosY + iSize;
        }

        /* Refresges the drawing */
        private void tmrRefreshGraphic_Tick(object sender, EventArgs e)
        {
            Invalidate();


            ChangeWindowStyle();


            GetKeyboardInput();
            AdjustPanelPosition();
            AdjustPanelSize();

            //GInfo = _gInfomation.Gameinfo;
            //GMap = _gInfomation.Map;
            //LPlayer = _gInfomation.Player;
            //LUnit = _gInfomation.Unit;
        }

        /* Load Preferences into the controls */
        private void LoadPreferencesIntoControls()
        {
            /* Set mainform to max. Mainscreen size and position */
            if (_rRenderForm.Equals(PredefinedTypes.RenderForm.Resources))
            {
                Location = new Point(_hMainHandler.PSettings.ResourcePositionX,
                                     _hMainHandler.PSettings.ResourcePositionY);
                Size = new Size(_hMainHandler.PSettings.ResourceWidth, _hMainHandler.PSettings.ResourceHeight);
                Opacity = _hMainHandler.PSettings.ResourceOpacity;
            }

            else if (_rRenderForm.Equals(PredefinedTypes.RenderForm.Income))
            {
                Location = new Point(_hMainHandler.PSettings.IncomePositionX,
                                     _hMainHandler.PSettings.IncomePositionY);
                Size = new Size(_hMainHandler.PSettings.IncomeWidth, _hMainHandler.PSettings.IncomeHeight);
                Opacity = _hMainHandler.PSettings.IncomeOpacity;
            }

            else if (_rRenderForm.Equals(PredefinedTypes.RenderForm.Army))
            {
                Location = new Point(_hMainHandler.PSettings.ArmyPositionX,
                                     _hMainHandler.PSettings.ArmyPositionY);
                Size = new Size(_hMainHandler.PSettings.ArmyWidth, _hMainHandler.PSettings.ArmyHeight);
                Opacity = _hMainHandler.PSettings.ArmyOpacity;
            }

            else if (_rRenderForm.Equals(PredefinedTypes.RenderForm.Apm))
            {
                Location = new Point(_hMainHandler.PSettings.ApmPositionX,
                                     _hMainHandler.PSettings.ApmPositionY);
                Size = new Size(_hMainHandler.PSettings.ApmWidth, _hMainHandler.PSettings.ApmHeight);
                Opacity = _hMainHandler.PSettings.ApmOpacity;
            }

            else if (_rRenderForm.Equals(PredefinedTypes.RenderForm.Worker))
            {
                Location = new Point(_hMainHandler.PSettings.WorkerPositionX,
                                     _hMainHandler.PSettings.WorkerPositionY);
                Size = new Size(_hMainHandler.PSettings.WorkerWidth, _hMainHandler.PSettings.WorkerHeight);
                Opacity = _hMainHandler.PSettings.WorkerOpacity;
            }

            else if (_rRenderForm.Equals(PredefinedTypes.RenderForm.Maphack))
            {
                Location = new Point(_hMainHandler.PSettings.MaphackPositionX, _hMainHandler.PSettings.MaphackPositionY);
                Size = new Size(_hMainHandler.PSettings.MaphackWidth, _hMainHandler.PSettings.MaphackHeigth);
                Opacity = _hMainHandler.PSettings.MaphackOpacity;
            }

            else if (_rRenderForm.Equals(PredefinedTypes.RenderForm.Units))
            {
                Location = new Point(_hMainHandler.PSettings.UnitTabPositionX, _hMainHandler.PSettings.UnitTabPositionY);
                Size = new Size(_hMainHandler.PSettings.UnitTabWidth, _hMainHandler.PSettings.UnitTabHeigth);
                Opacity = _hMainHandler.PSettings.UnitTabOpacity;
            }

           
        }

        /* Change the windowstyle */
        private void ChangeWindowStyle()
        {   
            if (InteropCalls.GetAsyncKeyState(_hMainHandler.PSettings.GlobalChangeSizeAndPosition) <= -32767)
            {
                var initial = InteropCalls.GetWindowLong(Handle, (Int32) InteropCalls.Gwl.ExStyle);
                InteropCalls.SetWindowLong(Handle, (Int32) InteropCalls.Gwl.ExStyle,
                                            (IntPtr) (initial & ~(Int32) InteropCalls.Ws.ExTransparent));
                _bChangingPosition = true;
                _bSurpressForeground = true;
            }

            else
            {
                var initial = InteropCalls.GetWindowLong(Handle, (Int32) InteropCalls.Gwl.ExStyle);
                InteropCalls.SetWindowLong(Handle, (Int32) InteropCalls.Gwl.ExStyle,
                                            (IntPtr) (initial | (Int32) InteropCalls.Ws.ExTransparent));
                _bSurpressForeground = false;

                if (!_bMouseDown)
                    _bChangingPosition = false;
            }
           
        }

        /* Ajust Panelposition */
        private Boolean _bSetPosition;
        private Boolean _bToggle;
        private void AdjustPanelPosition()
        {
            #region Resources

            if (_rRenderForm.Equals(PredefinedTypes.RenderForm.Resources))
            {
                if (_bSetPosition)
                {
                    tmrRefreshGraphic.Interval = 20;

                    Location = Cursor.Position;
                    _hMainHandler.PSettings.ResourcePositionX = Cursor.Position.X;
                    _hMainHandler.PSettings.ResourcePositionY = Cursor.Position.Y;
                }

                var strInput = _strBackup;

                if (String.IsNullOrEmpty(strInput))
                    return;

                if (strInput.Contains('\0'))
                    strInput = strInput.Substring(0, strInput.IndexOf('\0'));

                if (strInput.Equals(_hMainHandler.PSettings.ResourceChangePositionPanel))
                {
                    if (_bToggle)
                    {
                        _bToggle = !_bToggle;

                        if (!_bSetPosition)
                            _bSetPosition = true;
                    }
                }

                if (HelpFunctions.HotkeysPressed(Keys.Enter, Keys.Enter, Keys.Enter))
                {
                    _bSetPosition = false;
                    _strBackup = string.Empty;
                    tmrRefreshGraphic.Interval = _hMainHandler.PSettings.GlobalDrawingRefresh;

                    /* Transfer to Mainform */
                    _hMainHandler.txtResPosX.Text = _hMainHandler.PSettings.ResourcePositionX.ToString(CultureInfo.InvariantCulture);
                    _hMainHandler.txtResPosY.Text = _hMainHandler.PSettings.ResourcePositionY.ToString(CultureInfo.InvariantCulture);
                }
            }

            #endregion

            #region Income

            else if (_rRenderForm.Equals(PredefinedTypes.RenderForm.Income))
            {
                if (_bSetPosition)
                {
                    tmrRefreshGraphic.Interval = 20;

                    Location = Cursor.Position;
                    _hMainHandler.PSettings.IncomePositionX = Cursor.Position.X;
                    _hMainHandler.PSettings.IncomePositionY = Cursor.Position.Y;
                }

                var strInput = _strBackup;

                if (String.IsNullOrEmpty(strInput))
                    return;

                if (strInput.Contains('\0'))
                    strInput = strInput.Substring(0, strInput.IndexOf('\0'));

                if (strInput.Equals(_hMainHandler.PSettings.IncomeChangePositionPanel))
                {
                    if (_bToggle)
                    {
                        _bToggle = !_bToggle;

                        if (!_bSetPosition)
                            _bSetPosition = true;
                    }
                }

                if (HelpFunctions.HotkeysPressed(Keys.Enter, Keys.Enter, Keys.Enter))
                {
                    _bSetPosition = false;
                    _strBackup = string.Empty;
                    tmrRefreshGraphic.Interval = _hMainHandler.PSettings.GlobalDrawingRefresh;

                    /* Transfer to Mainform */
                    _hMainHandler.txtIncPosX.Text = _hMainHandler.PSettings.IncomePositionX.ToString(CultureInfo.InvariantCulture);
                    _hMainHandler.txtIncPosY.Text = _hMainHandler.PSettings.IncomePositionY.ToString(CultureInfo.InvariantCulture);
                }
            }

            #endregion

            #region Worker

            else if (_rRenderForm.Equals(PredefinedTypes.RenderForm.Worker))
            {
                if (_bSetPosition)
                {
                    tmrRefreshGraphic.Interval = 20;

                    Location = Cursor.Position;
                    _hMainHandler.PSettings.WorkerPositionX = Cursor.Position.X;
                    _hMainHandler.PSettings.WorkerPositionY = Cursor.Position.Y;
                }

                var strInput = _strBackup;

                if (String.IsNullOrEmpty(strInput))
                    return;

                if (strInput.Contains('\0'))
                    strInput = strInput.Substring(0, strInput.IndexOf('\0'));

                if (strInput.Equals(_hMainHandler.PSettings.WorkerChangePositionPanel))
                {
                    if (_bToggle)
                    {
                        _bToggle = !_bToggle;

                        if (!_bSetPosition)
                            _bSetPosition = true;
                    }
                }

                if (HelpFunctions.HotkeysPressed(Keys.Enter, Keys.Enter, Keys.Enter))
                {
                    _bSetPosition = false;
                    _strBackup = string.Empty;
                    tmrRefreshGraphic.Interval = _hMainHandler.PSettings.GlobalDrawingRefresh;

                    /* Transfer to Mainform */
                    _hMainHandler.txtWorPosX.Text = _hMainHandler.PSettings.WorkerPositionX.ToString(CultureInfo.InvariantCulture);
                    _hMainHandler.txtWorPosY.Text = _hMainHandler.PSettings.WorkerPositionY.ToString(CultureInfo.InvariantCulture);
                }
            }

            #endregion

            #region Apm

            else if (_rRenderForm.Equals(PredefinedTypes.RenderForm.Apm))
            {
                if (_bSetPosition)
                {
                    tmrRefreshGraphic.Interval = 20;

                    Location = Cursor.Position;
                    _hMainHandler.PSettings.ApmPositionX = Cursor.Position.X;
                    _hMainHandler.PSettings.ApmPositionY = Cursor.Position.Y;
                }

                var strInput = _strBackup;

                if (String.IsNullOrEmpty(strInput))
                    return;

                if (strInput.Contains('\0'))
                    strInput = strInput.Substring(0, strInput.IndexOf('\0'));

                if (strInput.Equals(_hMainHandler.PSettings.ApmChangePositionPanel))
                {
                    if (_bToggle)
                    {
                        _bToggle = !_bToggle;

                        if (!_bSetPosition)
                            _bSetPosition = true;
                    }
                }

                if (HelpFunctions.HotkeysPressed(Keys.Enter, Keys.Enter, Keys.Enter))
                {
                    _bSetPosition = false;
                    _strBackup = string.Empty;
                    tmrRefreshGraphic.Interval = _hMainHandler.PSettings.GlobalDrawingRefresh;

                    /* Transfer to Mainform */
                    _hMainHandler.txtApmPosX.Text = _hMainHandler.PSettings.ApmPositionX.ToString(CultureInfo.InvariantCulture);
                    _hMainHandler.txtApmPosY.Text = _hMainHandler.PSettings.ApmPositionY.ToString(CultureInfo.InvariantCulture);
                }
            }

            #endregion

            #region Army

            else if (_rRenderForm.Equals(PredefinedTypes.RenderForm.Army))
            {
                if (_bSetPosition)
                {
                    tmrRefreshGraphic.Interval = 20;

                    Location = Cursor.Position;
                    _hMainHandler.PSettings.ArmyPositionX = Cursor.Position.X;
                    _hMainHandler.PSettings.ArmyPositionY = Cursor.Position.Y;
                }

                var strInput = _strBackup;

                if (String.IsNullOrEmpty(strInput))
                    return;

                if (strInput.Contains('\0'))
                    strInput = strInput.Substring(0, strInput.IndexOf('\0'));

                if (strInput.Equals(_hMainHandler.PSettings.ArmyChangePositionPanel))
                {
                    if (_bToggle)
                    {
                        _bToggle = !_bToggle;

                        if (!_bSetPosition)
                            _bSetPosition = true;
                    }
                }

                if (HelpFunctions.HotkeysPressed(Keys.Enter, Keys.Enter, Keys.Enter))
                {
                    _bSetPosition = false;
                    _strBackup = string.Empty;
                    tmrRefreshGraphic.Interval = _hMainHandler.PSettings.GlobalDrawingRefresh;

                    /* Transfer to Mainform */
                    _hMainHandler.txtArmPosX.Text = _hMainHandler.PSettings.ArmyPositionX.ToString(CultureInfo.InvariantCulture);
                    _hMainHandler.txtArmPosY.Text = _hMainHandler.PSettings.ArmyPositionY.ToString(CultureInfo.InvariantCulture);
                }
            }

            #endregion

            #region Maphack

            else if (_rRenderForm.Equals(PredefinedTypes.RenderForm.Maphack))
            {
                if (_bSetPosition)
                {
                    tmrRefreshGraphic.Interval = 20;

                    Location = Cursor.Position;
                    _hMainHandler.PSettings.MaphackPositionX = Cursor.Position.X;
                    _hMainHandler.PSettings.MaphackPositionY = Cursor.Position.Y;
                }

                var strInput = _strBackup;

                if (String.IsNullOrEmpty(strInput))
                    return;

                if (strInput.Contains('\0'))
                    strInput = strInput.Substring(0, strInput.IndexOf('\0'));

                if (strInput.Equals(_hMainHandler.PSettings.MaphackChangePositionPanel))
                {
                    if (_bToggle)
                    {
                        _bToggle = !_bToggle;

                        if (!_bSetPosition)
                            _bSetPosition = true;
                    }
                }

                if (HelpFunctions.HotkeysPressed(Keys.Enter, Keys.Enter, Keys.Enter))
                {
                    _bSetPosition = false;
                    _strBackup = string.Empty;
                    tmrRefreshGraphic.Interval = _hMainHandler.PSettings.GlobalDrawingRefresh;

                    /* Transfer to Mainform */
                    _hMainHandler.txtMapPosX.Text = _hMainHandler.PSettings.MaphackPositionX.ToString(CultureInfo.InvariantCulture);
                    _hMainHandler.txtMapPosY.Text = _hMainHandler.PSettings.MaphackPositionY.ToString(CultureInfo.InvariantCulture);
                }
            }

            #endregion

            #region Units

            else if (_rRenderForm.Equals(PredefinedTypes.RenderForm.Units))
            {
                if (_bSetPosition)
                {
                    tmrRefreshGraphic.Interval = 20;

                    Location = Cursor.Position;
                    _hMainHandler.PSettings.UnitTabPositionX = Cursor.Position.X;
                    _hMainHandler.PSettings.UnitTabPositionY = Cursor.Position.Y;
                }

                var strInput = _strBackup;

                if (String.IsNullOrEmpty(strInput))
                    return;

                if (strInput.Contains('\0'))
                    strInput = strInput.Substring(0, strInput.IndexOf('\0'));

                if (strInput.Equals(_hMainHandler.PSettings.UnitChangePositionPanel))
                {
                    if (_bToggle)
                    {
                        _bToggle = !_bToggle;

                        if (!_bSetPosition)
                            _bSetPosition = true;
                    }
                }

                if (HelpFunctions.HotkeysPressed(Keys.Enter, Keys.Enter, Keys.Enter))
                {
                    _bSetPosition = false;
                    _strBackup = string.Empty;
                    tmrRefreshGraphic.Interval = _hMainHandler.PSettings.GlobalDrawingRefresh;

                    /* Transfer to Mainform */
                    _hMainHandler.txtUniPosX.Text = _hMainHandler.PSettings.UnitTabPositionX.ToString(CultureInfo.InvariantCulture);
                    _hMainHandler.txtUniPosY.Text = _hMainHandler.PSettings.UnitTabPositionY.ToString(CultureInfo.InvariantCulture);
                }
            }

            #endregion
        }

        /* Adjust Panelsize */
        private Boolean _bSetSize;
        private Boolean _bToggleSize;
        private void AdjustPanelSize()
        {
            #region Resources

            if (_rRenderForm.Equals(PredefinedTypes.RenderForm.Resources))
            {
                if (_bSetSize)
                {
                    tmrRefreshGraphic.Interval = 20;

                    _hMainHandler.PSettings.ResourceWidth = Cursor.Position.X - Left;

                    if ((Cursor.Position.Y - Top)/GInfo._IValidPlayerCount >= 5)
                    {
                        _hMainHandler.PSettings.ResourceHeight = (Cursor.Position.Y - Top)/
                                                                 GInfo._IValidPlayerCount;
                    }

                    else
                        _hMainHandler.PSettings.ResourceHeight = 5;

                    
                }

                var strInput = _strBackupSize;

                if (String.IsNullOrEmpty(strInput))
                    return;

                if (strInput.Contains('\0'))
                    strInput = strInput.Substring(0, strInput.IndexOf('\0'));


                if (strInput.Equals(_hMainHandler.PSettings.ResourceChangeSizePanel))
                {
                    if (_bToggleSize)
                    {
                        _bToggleSize = !_bToggleSize;

                        if (!_bSetSize)
                            _bSetSize = true;
                    }
                }

                if (HelpFunctions.HotkeysPressed(Keys.Enter, Keys.Enter, Keys.Enter))
                {
                    tmrRefreshGraphic.Interval = _hMainHandler.PSettings.GlobalDrawingRefresh;

                    _bSetSize = false;
                    _strBackupSize = string.Empty;

                    /* Transfer to Mainform */
                    _hMainHandler.txtResWidth.Text = _hMainHandler.PSettings.ResourceWidth.ToString(CultureInfo.InvariantCulture);
                    _hMainHandler.txtResHeight.Text = _hMainHandler.PSettings.ResourceHeight.ToString(CultureInfo.InvariantCulture);
                }
            }

            #endregion

            #region Income

            else if (_rRenderForm.Equals(PredefinedTypes.RenderForm.Income))
            {
                if (_bSetSize)
                {
                    tmrRefreshGraphic.Interval = 20;

                    _hMainHandler.PSettings.IncomeWidth = Cursor.Position.X - Left;

                    if ((Cursor.Position.Y - Top)/GInfo._IValidPlayerCount >= 5)
                    {
                        _hMainHandler.PSettings.IncomeHeight = (Cursor.Position.Y - Top)/
                                                               GInfo._IValidPlayerCount;
                    }

                    else
                        _hMainHandler.PSettings.IncomeHeight = 5;
                }

                var strInput = _strBackupSize;

                if (String.IsNullOrEmpty(strInput))
                    return;

                if (strInput.Contains('\0'))
                    strInput = strInput.Substring(0, strInput.IndexOf('\0'));


                if (strInput.Equals(_hMainHandler.PSettings.IncomeChangeSizePanel))
                {
                    if (_bToggleSize)
                    {
                        _bToggleSize = !_bToggleSize;

                        if (!_bSetSize)
                            _bSetSize = true;
                    }
                }

                if (HelpFunctions.HotkeysPressed(Keys.Enter, Keys.Enter, Keys.Enter))
                {
                    tmrRefreshGraphic.Interval = _hMainHandler.PSettings.GlobalDrawingRefresh;

                    _bSetSize = false;
                    _strBackupSize = string.Empty;

                    /* Transfer to Mainform */
                    _hMainHandler.txtIncWidth.Text = _hMainHandler.PSettings.IncomeWidth.ToString(CultureInfo.InvariantCulture);
                    _hMainHandler.txtIncHeight.Text = _hMainHandler.PSettings.IncomeHeight.ToString(CultureInfo.InvariantCulture);
                }
            }

            #endregion

            #region Worker

            else if (_rRenderForm.Equals(PredefinedTypes.RenderForm.Worker))
            {
                if (_bSetSize)
                {
                    tmrRefreshGraphic.Interval = 20;

                    _hMainHandler.PSettings.WorkerWidth = Cursor.Position.X - Left;

                    if ((Cursor.Position.Y - Top) >= 5)
                        _hMainHandler.PSettings.WorkerHeight = (Cursor.Position.Y - Top);
                    

                    else
                        _hMainHandler.PSettings.WorkerHeight = 5;
                }

                var strInput = _strBackupSize;

                if (String.IsNullOrEmpty(strInput))
                    return;

                if (strInput.Contains('\0'))
                    strInput = strInput.Substring(0, strInput.IndexOf('\0'));


                if (strInput.Equals(_hMainHandler.PSettings.WorkerChangeSizePanel))
                {
                    if (_bToggleSize)
                    {
                        _bToggleSize = !_bToggleSize;

                        if (!_bSetSize)
                            _bSetSize = true;
                    }
                }

                if (HelpFunctions.HotkeysPressed(Keys.Enter, Keys.Enter, Keys.Enter))
                {
                    tmrRefreshGraphic.Interval = _hMainHandler.PSettings.GlobalDrawingRefresh;

                    _bSetSize = false;
                    _strBackupSize = string.Empty;

                    /* Transfer to Mainform */
                    _hMainHandler.txtWorWidth.Text = _hMainHandler.PSettings.WorkerWidth.ToString(CultureInfo.InvariantCulture);
                    _hMainHandler.txtWorHeight.Text = _hMainHandler.PSettings.WorkerHeight.ToString(CultureInfo.InvariantCulture);
                }
            }

            #endregion

            #region Apm

            else if (_rRenderForm.Equals(PredefinedTypes.RenderForm.Apm))
            {
                if (_bSetSize)
                {
                    tmrRefreshGraphic.Interval = 20;

                    _hMainHandler.PSettings.ApmWidth = Cursor.Position.X - Left;

                    if ((Cursor.Position.Y - Top)/GInfo._IValidPlayerCount >= 5)
                    {
                        _hMainHandler.PSettings.ApmHeight = (Cursor.Position.Y - Top)/
                                                            GInfo._IValidPlayerCount;
                    }

                    else
                        _hMainHandler.PSettings.MaphackHeigth = 5;
                }

                var strInput = _strBackupSize;

                if (String.IsNullOrEmpty(strInput))
                    return;

                if (strInput.Contains('\0'))
                    strInput = strInput.Substring(0, strInput.IndexOf('\0'));


                if (strInput.Equals(_hMainHandler.PSettings.ApmChangeSizePanel))
                {
                    if (_bToggleSize)
                    {
                        _bToggleSize = !_bToggleSize;

                        if (!_bSetSize)
                            _bSetSize = true;
                    }
                }

                if (HelpFunctions.HotkeysPressed(Keys.Enter, Keys.Enter, Keys.Enter))
                {
                    tmrRefreshGraphic.Interval = _hMainHandler.PSettings.GlobalDrawingRefresh;

                    _bSetSize = false;
                    _strBackupSize = string.Empty;

                    /* Transfer to Mainform */
                    _hMainHandler.txtApmWidth.Text = _hMainHandler.PSettings.ApmWidth.ToString(CultureInfo.InvariantCulture);
                    _hMainHandler.txtApmHeight.Text = _hMainHandler.PSettings.ApmHeight.ToString(CultureInfo.InvariantCulture);
                }
            }

            #endregion

            #region Army

            else if (_rRenderForm.Equals(PredefinedTypes.RenderForm.Army))
            {
                if (_bSetSize)
                {
                    tmrRefreshGraphic.Interval = 20;

                    _hMainHandler.PSettings.ArmyWidth = Cursor.Position.X - Left;

                    if ((Cursor.Position.Y - Top)/GInfo._IValidPlayerCount >= 5)
                    {
                        _hMainHandler.PSettings.ArmyHeight = (Cursor.Position.Y - Top)/
                                                             GInfo._IValidPlayerCount;
                    }
                    else
                        _hMainHandler.PSettings.ApmHeight = 5;
                }

                var strInput = _strBackupSize;

                if (String.IsNullOrEmpty(strInput))
                    return;

                if (strInput.Contains('\0'))
                    strInput = strInput.Substring(0, strInput.IndexOf('\0'));


                if (strInput.Equals(_hMainHandler.PSettings.ArmyChangeSizePanel))
                {
                    if (_bToggleSize)
                    {
                        _bToggleSize = !_bToggleSize;

                        if (!_bSetSize)
                            _bSetSize = true;
                    }
                }

                if (HelpFunctions.HotkeysPressed(Keys.Enter, Keys.Enter, Keys.Enter))
                {
                    tmrRefreshGraphic.Interval = _hMainHandler.PSettings.GlobalDrawingRefresh;

                    _bSetSize = false;
                    _strBackupSize = string.Empty;

                    /* Transfer to Mainform */
                    _hMainHandler.txtArmWidth.Text = _hMainHandler.PSettings.ArmyWidth.ToString(CultureInfo.InvariantCulture);
                    _hMainHandler.txtArmHeight.Text = _hMainHandler.PSettings.ArmyHeight.ToString(CultureInfo.InvariantCulture);
                }
            }

            #endregion

            #region Maphack

            else if (_rRenderForm.Equals(PredefinedTypes.RenderForm.Maphack))
            {
                if (_bSetSize)
                {
                    tmrRefreshGraphic.Interval = 20;

                    _hMainHandler.PSettings.MaphackWidth = Cursor.Position.X - Left;

                    if ((Cursor.Position.Y - Top) >= 5)
                        _hMainHandler.PSettings.MaphackHeigth = (Cursor.Position.Y - Top);

                    else
                        _hMainHandler.PSettings.MaphackHeigth = 5;

                }

                var strInput = _strBackupSize;

                if (String.IsNullOrEmpty(strInput))
                    return;
                

                if (strInput.Contains('\0'))
                    strInput = strInput.Substring(0, strInput.IndexOf('\0'));


                if (strInput.Equals(_hMainHandler.PSettings.MaphackChangeSizePanel))
                {
                    if (_bToggleSize)
                    {
                        _bToggleSize = !_bToggleSize;

                        if (!_bSetSize)
                            _bSetSize = true;
                    }
                }

                if (HelpFunctions.HotkeysPressed(Keys.Enter, Keys.Enter, Keys.Enter))
                {
                    tmrRefreshGraphic.Interval = _hMainHandler.PSettings.GlobalDrawingRefresh;
                    
                    _bSetSize = false;
                    _strBackupSize = string.Empty;

                    /* Transfer to Mainform */
                    _hMainHandler.txtMapWidth.Text = _hMainHandler.PSettings.MaphackWidth.ToString(CultureInfo.InvariantCulture);
                    _hMainHandler.txtMapHeight.Text = _hMainHandler.PSettings.MaphackHeigth.ToString(CultureInfo.InvariantCulture);
                }
            }

            #endregion

            #region Units

            else if (_rRenderForm.Equals(PredefinedTypes.RenderForm.Units))
            {
                if (_bSetSize)
                {
                    tmrRefreshGraphic.Interval = 20;

                    _hMainHandler.PSettings.UnitTabWidth = Cursor.Position.X - Left;

                    if ((Cursor.Position.Y - Top) >= 5)
                        _hMainHandler.PSettings.UnitTabHeigth = (Cursor.Position.Y - Top);

                    else
                        _hMainHandler.PSettings.UnitTabHeigth = 5;
                }

                var strInput = _strBackupSize;

                if (String.IsNullOrEmpty(strInput))
                    return;

                if (strInput.Contains('\0'))
                    strInput = strInput.Substring(0, strInput.IndexOf('\0'));


                if (strInput.Equals(_hMainHandler.PSettings.UnitChangeSizePanel))
                {
                    if (_bToggleSize)
                    {
                        _bToggleSize = !_bToggleSize;

                        if (!_bSetSize)
                            _bSetSize = true;
                    }
                }

                if (HelpFunctions.HotkeysPressed(Keys.Enter, Keys.Enter, Keys.Enter))
                {
                    tmrRefreshGraphic.Interval = _hMainHandler.PSettings.GlobalDrawingRefresh;

                    _bSetSize = false;
                    _strBackupSize = string.Empty;

                    /* Transfer to Mainform */
                    _hMainHandler.txtUniWidth.Text = _hMainHandler.PSettings.UnitTabWidth.ToString(CultureInfo.InvariantCulture);
                    _hMainHandler.txtUniHeight.Text = _hMainHandler.PSettings.UnitTabHeigth.ToString(CultureInfo.InvariantCulture);
                }
            }

            #endregion
        }

        private string _strBackup = string.Empty;
        private String _strBackupSize = String.Empty;
        public void GetKeyboardInput()
        {
            var sInput = GInfo.ChatInput;

            if (sInput != _strBackup)
                _bToggle = true;

            if (sInput != _strBackupSize)
                _bToggleSize = true;
            

            _strBackup = sInput;
            _strBackupSize = sInput;
        }

        /* Iterate through unitstruct and export UnitId's and Names */
        private List<PredefinedTypes.Unit> lUnitForUniqueness = new List<PredefinedTypes.Unit>(); 
        private void ExportUnitIdsToFile()
        {
            var sfdSaveFile = new SaveFileDialog();
            sfdSaveFile.Filter = "txt (Textfile)|*.txt|csv (Tablesheet)|*.csv";
            var result = sfdSaveFile.ShowDialog();

            if (!result.Equals(DialogResult.OK))
            {
                Close();
                return;
            }

            var sw = new StreamWriter(sfdSaveFile.FileName);

            if (Path.GetExtension(sfdSaveFile.FileName) == ".csv")
            {
                sw.WriteLine("ID; Name; Raw Name");
                for (var i = 0; i < LUnit.Count; i++)
                {
                    var bUnique = false;

                    if (lUnitForUniqueness.Count <= 0)
                        lUnitForUniqueness.Add(LUnit[i]);

                    else
                    {
                        for (var j = 0; j < lUnitForUniqueness.Count; j++)
                        {
                            if (lUnitForUniqueness[j].CustomStruct.Id != LUnit[i].CustomStruct.Id)
                                bUnique = true;

                            else
                            {
                                bUnique = false;
                                break;
                            }
                        }

                        if (bUnique)
                            lUnitForUniqueness.Add(LUnit[i]);  
                    }  
                }

                lUnitForUniqueness.Sort((x, y) => x.CustomStruct.Id.CompareTo(y.CustomStruct.Id));

                for (var i = 0; i < lUnitForUniqueness.Count; i++ )
                    sw.WriteLine(lUnitForUniqueness[i].CustomStruct.Id + ";" + lUnitForUniqueness[i].CustomStruct.Name + ";" +
                                             lUnitForUniqueness[i].CustomStruct.RawName);
                sw.Close();
                Close();
                return;
            }

            else
            {
                sw.WriteLine("public enum UnitId");
                sw.WriteLine("{");
                for (var i = 0; i < LUnit.Count; i++)
                {
                    var bUnique = false;

                    if (lUnitForUniqueness.Count <= 0)
                        lUnitForUniqueness.Add(LUnit[i]);

                    else
                    {
                        for (var j = 0; j < lUnitForUniqueness.Count; j++)
                        {
                            if (lUnitForUniqueness[j].CustomStruct.Id != LUnit[i].CustomStruct.Id)
                                bUnique = true;

                            else
                            {
                                bUnique = false;
                                break;
                            }
                        }

                        if (bUnique)
                            lUnitForUniqueness.Add(LUnit[i]);
                    }  
                }

                lUnitForUniqueness.Sort((x, y) => x.CustomStruct.Id.CompareTo(y.CustomStruct.Id));

                for (var i = 0; i < lUnitForUniqueness.Count; i++)
                {
                    if (i + 1 == lUnitForUniqueness.Count)
                        sw.WriteLine("\t" + lUnitForUniqueness[i].CustomStruct.Name + " = " + lUnitForUniqueness[i].CustomStruct.Id);

                    else
                        sw.WriteLine("\t" + lUnitForUniqueness[i].CustomStruct.Name + " = " + lUnitForUniqueness[i].CustomStruct.Id + ",");
                }


                sw.WriteLine("}");
                sw.Close();
                Close();
                return;
            }
        }

        #region Mouseactions

        private void Renderer_2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var mousePos = MousePosition;
                mousePos.Offset(-_ptMousePosition.X, -_ptMousePosition.Y);
                Location = mousePos;
            }
        }

        private Boolean _bMouseDown = false;
        private void Renderer_2_MouseDown(object sender, MouseEventArgs e)
        {
            _ptMousePosition = new Point(e.X, e.Y);

            _bMouseDown = true;
        }

        private void Renderer_2_MouseUp(object sender, MouseEventArgs e)
        {
            InteropCalls.SetForegroundWindow(_hMainHandler.PSc2Process.MainWindowHandle);

            if (_rRenderForm.Equals(PredefinedTypes.RenderForm.Units))
            {
                _hMainHandler.PSettings.UnitTabPositionX = Location.X;
                _hMainHandler.PSettings.UnitTabPositionY = Location.Y;
                _hMainHandler.PSettings.UnitTabHeigth = Height;
                _hMainHandler.PSettings.UnitTabWidth = Width;
                _hMainHandler.PSettings.UnitTabOpacity = Opacity;

                /* Transfer to Mainform */
                _hMainHandler.txtUniPosX.Text = Location.X.ToString(CultureInfo.InvariantCulture);
                _hMainHandler.txtUniPosY.Text = Location.Y.ToString(CultureInfo.InvariantCulture);
                _hMainHandler.txtUniWidth.Text = Width.ToString(CultureInfo.InvariantCulture);
                _hMainHandler.txtUniHeight.Text = Height.ToString(CultureInfo.InvariantCulture);
            }

            else if (_rRenderForm.Equals(PredefinedTypes.RenderForm.Maphack))
            {
                _hMainHandler.PSettings.MaphackPositionX = Location.X;
                _hMainHandler.PSettings.MaphackPositionY = Location.Y;
                _hMainHandler.PSettings.MaphackHeigth = Height;
                _hMainHandler.PSettings.MaphackWidth = Width;
                _hMainHandler.PSettings.MaphackOpacity = Opacity;

                /* Transfer to Mainform */
                _hMainHandler.txtMapPosX.Text = Location.X.ToString(CultureInfo.InvariantCulture);
                _hMainHandler.txtMapPosY.Text = Location.Y.ToString(CultureInfo.InvariantCulture);
                _hMainHandler.txtMapWidth.Text = Width.ToString(CultureInfo.InvariantCulture);
                _hMainHandler.txtMapHeight.Text = Height.ToString(CultureInfo.InvariantCulture);
            }

            else if (_rRenderForm.Equals(PredefinedTypes.RenderForm.Resources))
            {
                _hMainHandler.PSettings.ResourcePositionX = Location.X;
                _hMainHandler.PSettings.ResourcePositionY = Location.Y;
                _hMainHandler.PSettings.ResourceWidth = Width;
                _hMainHandler.PSettings.ResourceHeight = Height/HelpFunctions.GetValidPlayerCount(LPlayer);
                _hMainHandler.PSettings.ResourceOpacity = Opacity;

                /* Transfer to Mainform */
                _hMainHandler.txtResPosX.Text = Location.X.ToString(CultureInfo.InvariantCulture);
                _hMainHandler.txtResPosY.Text = Location.Y.ToString(CultureInfo.InvariantCulture);
                _hMainHandler.txtResWidth.Text = Width.ToString(CultureInfo.InvariantCulture);
                _hMainHandler.txtResHeight.Text = Height.ToString(CultureInfo.InvariantCulture);
            }

            else if (_rRenderForm.Equals(PredefinedTypes.RenderForm.Income))
            {
                _hMainHandler.PSettings.IncomePositionX = Location.X;
                _hMainHandler.PSettings.IncomePositionY = Location.Y;
                _hMainHandler.PSettings.IncomeWidth = Width;
                _hMainHandler.PSettings.IncomeHeight = Height / HelpFunctions.GetValidPlayerCount(LPlayer);
                _hMainHandler.PSettings.IncomeOpacity = Opacity;

                /* Transfer to Mainform */
                _hMainHandler.txtIncPosX.Text = Location.X.ToString(CultureInfo.InvariantCulture);
                _hMainHandler.txtIncPosY.Text = Location.Y.ToString(CultureInfo.InvariantCulture);
                _hMainHandler.txtIncWidth.Text = Width.ToString(CultureInfo.InvariantCulture);
                _hMainHandler.txtIncHeight.Text = Height.ToString(CultureInfo.InvariantCulture);
            }

            else if (_rRenderForm.Equals(PredefinedTypes.RenderForm.Army))
            {
                _hMainHandler.PSettings.ArmyPositionX = Location.X;
                _hMainHandler.PSettings.ArmyPositionY = Location.Y;
                _hMainHandler.PSettings.ArmyWidth = Width;
                _hMainHandler.PSettings.ArmyHeight = Height / HelpFunctions.GetValidPlayerCount(LPlayer);
                _hMainHandler.PSettings.ArmyOpacity = Opacity;

                /* Transfer to Mainform */
                _hMainHandler.txtArmPosX.Text = Location.X.ToString(CultureInfo.InvariantCulture);
                _hMainHandler.txtArmPosY.Text = Location.Y.ToString(CultureInfo.InvariantCulture);
                _hMainHandler.txtArmWidth.Text = Width.ToString(CultureInfo.InvariantCulture);
                _hMainHandler.txtArmHeight.Text = Height.ToString(CultureInfo.InvariantCulture);
            }

            else if (_rRenderForm.Equals(PredefinedTypes.RenderForm.Apm))
            {
                _hMainHandler.PSettings.ApmPositionX = Location.X;
                _hMainHandler.PSettings.ApmPositionY = Location.Y;
                _hMainHandler.PSettings.ApmWidth = Width;
                _hMainHandler.PSettings.ApmHeight = Height / HelpFunctions.GetValidPlayerCount(LPlayer);
                _hMainHandler.PSettings.ApmOpacity = Opacity;

                /* Transfer to Mainform */
                _hMainHandler.txtApmPosX.Text = Location.X.ToString(CultureInfo.InvariantCulture);
                _hMainHandler.txtApmPosY.Text = Location.Y.ToString(CultureInfo.InvariantCulture);
                _hMainHandler.txtApmWidth.Text = Width.ToString(CultureInfo.InvariantCulture);
                _hMainHandler.txtApmHeight.Text = Height.ToString(CultureInfo.InvariantCulture);
            }

            else if (_rRenderForm.Equals(PredefinedTypes.RenderForm.Worker))
            {
                _hMainHandler.PSettings.WorkerPositionX = Location.X;
                _hMainHandler.PSettings.WorkerPositionY = Location.Y;
                _hMainHandler.PSettings.WorkerWidth = Width;
                _hMainHandler.PSettings.WorkerHeight = Height; 
                _hMainHandler.PSettings.WorkerOpacity = Opacity;

                /* Transfer to Mainform */
                _hMainHandler.txtWorPosX.Text = Location.X.ToString(CultureInfo.InvariantCulture);
                _hMainHandler.txtWorPosY.Text = Location.Y.ToString(CultureInfo.InvariantCulture);
                _hMainHandler.txtWorWidth.Text = Width.ToString(CultureInfo.InvariantCulture);
                _hMainHandler.txtWorHeight.Text = Height.ToString(CultureInfo.InvariantCulture);
            }

            _bChangingPosition = false;
            _bMouseDown = false;
        }

        private void Renderer_2_MouseWheel(object sender, MouseEventArgs e)
        {
            if (Width >= Screen.PrimaryScreen.Bounds.Width &&
                e.Delta.Equals(120))
                return;

            if (e.Delta.Equals(120))
            {
                Width += 1;
                Height += 1;
            }

            else if (e.Delta.Equals(-120))
            {
                Width -= 1;
                Height -= 1;
            }
        }

        #endregion
    }
}
