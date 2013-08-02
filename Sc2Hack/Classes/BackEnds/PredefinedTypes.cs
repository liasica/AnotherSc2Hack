using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Sc2Hack.Classes.BackEnds
{
    public class PredefinedTypes
    {
        public enum UnitId
        {
            PuColossus = 38,            //Valid
            TbTechlab = 39,             //Valid
            TbReactor = 40,             //Valid
            ZuInfestedTerran = 42,      //Valid
            ZuBanelingCocoon = 43,      //Valid
            ZuBaneling = 44,            //Valid
            PuMothership = 45,          //Valid
            TuPdd = 46,                 //
            ZuChangeling = 47,          //Valid
            ZuChangelingZealot = 48,    //Valid
            ZuChangelingMarineShield = 49,  //Valid
            ZuChangelingMarine = 50,    //Valid
            ZuChangelingSpeedling = 51, //Valid
            ZuChangelingZergling = 52,  //Valid
            TbCcGround = 54,            //Valid
            TbSupplyGround = 55,        //Valid
            TbRefinery = 56,            //Valid
            TbBarracksGround = 57,      //Valid
            TbEbay = 58,                //Valid
            TbTurret = 59,              //Valid
            TbBunker = 60,              //Valid
            TbSensortower = 61,         //Valid
            TbGhostacademy = 62,        //Valid
            TbFactoryGround = 63,       //Valid
            TbStarportGround = 64,      //Valid
            TbArmory = 66,              //Valid
            TbFusioncore = 67,          //Valid
            TbAutoTurret = 68,          //Valid
            TuSiegetankSieged = 69,     //Valid
            TuSiegetank = 70,           //Valid
            TuVikingGround = 71,        //Valid
            TuVikingAir = 72,           //Valid
            TbCcAir = 73,               //Valid
            TbTechlabRax = 74,          //Not Sure
            TbReactorRax = 75,          //Not sure
            TbTechlabFactory = 76,      //Not sure
            TbReactorFactory = 77,      //Not sure
            TbTechlabStarport = 78,     //Not sure
            TbReactorStarport = 79,     //Not sure
            TbFactoryAir = 80,          //Valid
            TbStarportAir = 81,         //Valid
            TuScv = 82,                 //Valid
            TbRaxAir = 83,              //Valid
            TbSupplyHidden = 84,        //Valid
            TuMarine = 85,              //Valid
            TuReaper = 86,              //Valid
            TuGhost = 87,               //Valid
            TuMarauder = 88,            //Valid
            TuThor = 89,                //Valid
            TuHellion = 90,             //Valid
            TuMedivac = 91,             //Valid
            TuBanshee = 92,             //Valid
            TuRaven = 93,               //Valid
            TuBattlecruiser = 94,       //Valid
            PbNexus = 96,               //Valid
            PbPylon = 97,               //Valid
            PbAssimilator = 98,         //Valid
            PbGateway = 99,             //Valid
            PbForge = 100,              //Valid
            PbFleetbeacon = 101,        //Valid
            PbTwilightcouncil = 102,    //Valid
            PbCannon = 103,             //Valid
            PbStargate = 104,           //Valid
            PbTemplararchives = 105,    //Valid
            PbDarkshrine = 106,         //Valid
            PbRoboticssupportbay = 107, //Valid
            PbRoboticsbay = 108,        //Valid
            PbCybercore = 109,          //Valid
            PuZealot = 110,             //Valid 
            PuStalker = 111,            //Valid
            PuHightemplar = 112,        //Valid
            PuDarktemplar = 113,        //Valid
            PuSentry = 114,             //Valid
            PuPhoenix = 115,            //Valid
            PuCarrier = 116,            //Valid
            PuVoidray = 117,            //Valid
            PuWarpprismTransport = 118, //Valid
            PuObserver = 119,           //Valid
            PuImmortal = 120,           //Valid
            PuProbe = 121,              //Valid
            PuInterceptor = 122,        //Valid
            ZbHatchery = 123,           //Valid
            ZbExtractor = 125,          //Valid
            ZbSpawningPool = 126,       //Valid
            ZbEvolutionChamber = 127,   //Valid
            ZbHydraDen = 128,           //Valid
            ZbSpire = 129,              //Valid
            ZbUltraCavern = 130,        //Valid
            ZbInfestationPit = 131,     //Valid
            ZbNydusNetwork = 132,       //Valid
            ZbBanelingNest = 133,       //Valid
            ZbRoachWarren = 134,        //Valid
            ZbSpineCrawler = 135,       //Valid
            ZbSporeCrawler = 136,       //Valid
            ZbLiar = 137,               //Valid
            ZbHive = 138,               //Valid
            ZbGreaterspire = 139,       //Valid
            ZuEgg = 140,                //Valid
            ZuDrone = 141,               //Valid
            ZuZergling = 142,           //Valid
            ZuOverlord = 143,           //Valid
            ZuHydralisk = 144,          //Valid
            ZuMutalisk = 145,           //Valid
            ZuUltra = 146,              //Valid
            ZuRoach = 147,              //Valid
            ZuInfestor = 148,           //Valid
            ZuCorruptor = 149,          //Valid
            ZuBroodlordCocoon = 150,    //Valid
            ZuBroodlord = 151,          //Valid
            ZuBanelingBurrow = 152,     //Valid
            ZuDroneBurrow = 153,        //Valid
            ZuHydraBurrow = 154,        //Valid
            ZuRoachBurrow = 155,        //Valid
            ZuZerglingBurrow = 156,     //Valid
            ZuInfestedTerran2 = 157,    //Valid
            ZuQueenBurrow = 162,        //Valid
            ZuQueen = 163,              //Valid
            ZuInfestorBurrow = 164,     //Valid
            ZuOverseerCocoon = 165,     //Valid
            ZuOverseer = 166,           //Valid
            TbPlanetary = 167,          //Valid
            ZuUltraBurrow = 168,        //Valid
            TbOrbitalGround = 169,      //Valid
            PbWarpgate = 170,           //Valid
            TbOrbitalAir = 171,         //Valid
            PuWarpprismPhase = 173,     //Valid
            ZbCreeptumor = 174,         //Valid
            ZbSpineCrawlerUnrooted = 176,   //Valid
            ZbSporeCrawlerUnrooted = 177,   //Valid
            PuArchon = 178,             //Valid
            ZbNydusWorm = 179,          //Valid
            ZuBroodlordEscort = 180,    //Valid
            NbMineralRichPatch = 181,   //Valid
            NbXelNagaTower = 183,       //Valid
            ZuInfestedSwarmEgg = 187,   //Valid
            ZuLarva = 188,              //Valid
            TuMule = 196,               //Valid
            ZuBroodling = 219,          //Valid
            NbMineralPatch = 260,       //Valid
            NbGas = 261,                //Valid
            NbGasSpace = 262,           //Valid
            NbGasRich = 263,            //Valid
            TuHellbat = 407,            //Valid
            PuMothershipCore = 411,     //Valid
            ZuLocust = 415,             //Valid
            ZuSwarmHostBurrow = 419,    //Valid
            ZuSwarmHost = 420,          //Valid
            PuOracle = 421,             //Valid
            PuTempest = 422,            //Valid
            TuWarhound = 423,           //Valid
            TuWidowMine = 424,          //Valid
            ZuViper = 425,              //Valid
            TuWidowMineBurrow = 426     //Valid
        };

        public enum PlayerStatus
        {
            Playing = 0,
            Won = 1,
            Lost = 2,
            Tied = 3,
            NotDefined = 42
        };

        public enum PlayerColor
        {
            White = 0,
            Red = 1,
            Blue = 2,
            Teal = 3,
            Purple = 4,
            Yellow = 5,
            Orange = 6,
            Green = 7,
            LightPink = 8,
            Violet = 9,
            LightGray = 10,
            DarkGreen = 11,
            Brown = 12,
            LightGreen = 13,
            DarkGray = 14,
            Pink = 15
        };

        public enum Gamespeed
        {
            Slower = 426,
            Slow = 320,
            Normal = 256,
            Fast = 212,
            Faster = 182,
            Fasterx2 = 91,
            Fasterx4 = 45,
            Fasterx8 = 22
        };

        public enum WindowStyle
        {
            WindowedFullscreen = 262144,
            Windowed = 262400,
            Fullscreen = 262152
        };

        public enum Gametype
        {
            None = 0,
            Replay = 1,
            Challange = 2,
            VersusAi = 3,
            Ladder = 4
        };

        public enum PlayerType
        {
            Human = 1,
            Ai = 2,
            Neutral = 3,
            Hostile = 4,
            Referee = 5,
            Observer = 6,
            NotDefined = 7
        };

        //This work is based on Qazzies/ MrNukealizers research on the Targetfilter
        public enum TargetFilterFlag : ulong
        {
            Outer =             0x0000800000000000,
            Unstoppable =       0x0000400000000000,
            Summoned =          0x0000200000000000,
            Stunned =           0x0000100000000000,
            Radar =             0x0000080000000000,
            Detector =          0x0000040000000000,
            Passive =           0x0000020000000000,
            Benign =            0x0000010000000000,
            HasShields =        0x0000008000000000,
            HasEnergy =         0x0000004000000000,
            Invulnerable =      0x0000002000000000,
            Hallucination =     0x0000001000000000,
            Hidden =            0x0000000800000000,
            Revivable =         0x0000000400000000,
            Dead =              0x0000000200000000,
            UnderConstruction = 0x0000000100000000,
            Stasis =            0x0000000080000000,
            Visible =           0x0000000040000000,
            Cloaked =           0x0000000020000000,
            Buried =            0x0000000010000000,
            PreventReveal =     0x0000000008000000,
            PreventDefeat =     0x0000000004000000,
            CanHaveShields =    0x0000000002000000,
            CanHaveEnergy =     0x0000000001000000,
            Uncommandable =     0x0000000000800000,
            Item =              0x0000000000400000,
            Destructable =      0x0000000000200000,
            Missile =           0x0000000000100000,
            ResourcesHarvestable = 0x0000000000080000,
            ResourcesRaw =      0x0000000000040000,
            Worker =            0x0000000000020000,
            Heroic =            0x0000000000010000,
            Hover =             0x0000000000008000,
            Structure =         0x0000000000004000,
            Massive =           0x0000000000002000,
            Psionic =           0x0000000000001000,
            Mechanical =        0x0000000000000800,
            Robotic =           0x0000000000000400,
            Biological =        0x0000000000000200,
            Armored =           0x0000000000000100,
            Light =             0x0000000000000080,
            Ground =            0x0000000000000040,
            Air =               0x0000000000000020,
            Enemy =             0x0000000000000010,
            Neutral =           0x0000000000000008,
            Ally =              0x0000000000000004,
            Player =            0x0000000000000002,
            Self =              0x0000000000000001
        };

        public enum Race
        {
            Terran = 0,
            Protoss = 1,
            Zerg = 2,
            Other = 3
        };

        public struct Player
        {
            public Int32 CameraPositionX;
            public Int32 CameraPositionY;
            public Int32 CameraDistance;
            public Int32 CameraRotation;
            public PlayerStatus Status;
            public PlayerType Type;
            public Int32 NameLength;
            public String Name;
            public Color Color;
            public Int32 Apm;
            public Int32 Epm;
            public Int32 Worker;
            public Int32 SupplyMinRaw;
            public Int32 SupplyMaxRaw;
            public Int32 SupplyMin;
            public Int32 SupplyMax;
            public Int32 CurrentBuildings;
            public String AccountId;
            public Int32 Minerals;
            public Int32 Gas;
            public Int32 MineralsIncome;
            public Int32 GasIncome;
            public Int32 MineralsArmy;
            public Int32 GasArmy;
            public Int32 Team;
            public Race Race;
            public Int32 Localplayer;   //Makes no sense to put this into the struct but it makes stuff easier
            public Boolean IsLocalplayer;
            public Int32 ValidSize;
            public LeagueInfo LeagueInfo;
        };

        public struct Unit
        {
            public Int32 PositionX;
            public Int32 PositionY;
            public Int32 DestinationPositionX;
            public Int32 DestinationPositionY;
            public UInt64 TargetFilter;
            public Int32 DamageTaken;
            public Int32 Energy;
            public Int32 Owner;
            public Int32 State;
            public Int32 Movestate;
            public Int32 BuildingState;
            public UnitModelStruct CustomStruct;
            public Int32 ModelPointer;
            public Boolean IsAlive;
            public Boolean IsUnderConstruction;
            public Boolean IsCloaked;
            public Boolean IsStructure;
        };

        public struct UnitModelStruct
        {
            public float Size;
            public Int32 MaximumHealth;
            public Int32 Id;
            public Int32 NameLenght;
            public String RawName;
            public String Name;
        };

        public struct Map
        {
            public Int32 Left;
            public Int32 Top;
            public Int32 Right;
            public Int32 Bottom;
            public Int32 PlayableWidth;
            public Int32 PlayableHeight;
        };

        public struct Selection
        {
            public Int32 AmountSelectedUnits;
            public Int32 AmountSelectedTypes;
            public Int32 UnitType;
            public Int32 UnitIndex;
        };

        public struct Gameinformation
        {
            public Int32 Timer;
            public Boolean IsIngame;
            public Int32 Fps;
            public Gametype Type;
            public Gamespeed Speed;
            public WindowStyle Style;
            public String ChatInput;
            public Boolean IsTeamcolor;
            public Int32 _IValidPlayerCount;
            public Boolean Pause;
        };

        public struct LeagueInfo
        {
            public String LeagueName;
            public String LeaguePacement;
            public Int32 Points;
            public Int32 RankInDivision;
        };

        public struct Preferences
        {
            public Boolean UnitTabRemoveAi;
            public Boolean UnitTabRemoveAllie;
            public Boolean UnitTabRemoveReferee;
            public Boolean UnitTabRemoveObserver;
            public Boolean UnitTabRemoveNeutral;
            public Boolean UnitTabRemoveLocalplayer;
            public Boolean UnitTabSplitUnitsAndBuildings;
            public Int32 UnitTabPositionX;
            public Int32 UnitTabPositionY;
            public Int32 UnitTabWidth;
            public Int32 UnitTabHeigth;
            public Double UnitTabOpacity;
            public Int32 UnitPictureSize;
            public Boolean MaphackRemoveAi;
            public Boolean MaphackRemoveAllie;
            public Boolean MaphackRemoveReferee;
            public Boolean MaphackRemoveObserver;
            public Boolean MaphackRemoveNeutral;
            public Boolean MaphackRemoveLocalplayer;
            public Boolean MaphackRemoveVisionArea;
            public Boolean MaphackRemoveCamera;
            public Color MaphackDestinationColor;
            public Int32 MaphackPositionX;
            public Int32 MaphackPositionY;
            public Int32 MaphackWidth;
            public Int32 MaphackHeigth;
            public Color MaphackColorUnit1;
            public Color MaphackColorUnit2;
            public Color MaphackColorUnit3;
            public Color MaphackColorUnit4;
            public Color MaphackColorUnit5;
            public UnitId MaphackUnitId1;
            public UnitId MaphackUnitId2;
            public UnitId MaphackUnitId3;
            public UnitId MaphackUnitId4;
            public UnitId MaphackUnitId5;
            public Double MaphackOpacity;
            public Int32 ResourcePositionX;
            public Int32 ResourcePositionY;
            public Int32 ResourceWidth;
            public Int32 ResourceHeight;
            public Boolean ResourceRemoveAi;
            public Boolean ResourceRemoveNeutral;
            public Boolean ResourceRemoveAllie;
            public Boolean ResourceRemoveLocalplayer;
            public String ResourceFontName;
            public Double ResourceOpacity;
            public Int32 IncomePositionX;
            public Int32 IncomePositionY;
            public Int32 IncomeWidth;
            public Int32 IncomeHeight;
            public Boolean IncomeRemoveAi;
            public Boolean IncomeRemoveNeutral;
            public Boolean IncomeRemoveAllie;
            public Boolean IncomeRemoveLocalplayer;
            public String IncomeFontName;
            public Double IncomeOpacity;
            public Int32 ArmyPositionX;
            public Int32 ArmyPositionY;
            public Int32 ArmyWidth;
            public Int32 ArmyHeight;
            public Boolean ArmyRemoveAi;
            public Boolean ArmyRemoveNeutral;
            public Boolean ArmyRemoveAllie;
            public Boolean ArmyRemoveLocalplayer;
            public String ArmyFontName;
            public Double ArmyOpacity;
            public Int32 ApmPositionX;
            public Int32 ApmPositionY;
            public Int32 ApmWidth;
            public Int32 ApmHeight;
            public Boolean ApmRemoveAi;
            public Boolean ApmRemoveNeutral;
            public Boolean ApmRemoveAllie;
            public Boolean ApmRemoveLocalplayer;
            public String ApmFontName;
            public Double ApmOpacity;
            public Int32 WorkerPositionX;
            public Int32 WorkerPositionY;
            public Int32 WorkerWidth;
            public Int32 WorkerHeight;
            public String WorkerFontName;
            public Double WorkerOpacity;
            public Int32 GlobalDataRefresh;
            public Int32 GlobalDrawingRefresh;
            public Boolean GlobalDrawOnlyInForeground;
            public String ResourceTogglePanel;
            public String ResourceChangePositionPanel;
            public String ResourceChangeSizePanel;
            public Keys ResourceHotkey1;
            public Keys ResourceHotkey2;
            public Keys ResourceHotkey3;
            public String IncomeTogglePanel;
            public String IncomeChangePositionPanel;
            public String IncomeChangeSizePanel;
            public Keys IncomeHotkey1;
            public Keys IncomeHotkey2;
            public Keys IncomeHotkey3;
            public String WorkerTogglePanel;
            public String WorkerChangePositionPanel;
            public String WorkerChangeSizePanel;
            public Keys WorkerHotkey1;
            public Keys WorkerHotkey2;
            public Keys WorkerHotkey3;
            public String MaphackTogglePanel;
            public String MaphackChangePositionPanel;
            public String MaphackChangeSizePanel;
            public Keys MaphackHotkey1;
            public Keys MaphackHotkey2;
            public Keys MaphackHotkey3;
            public String ApmTogglePanel;
            public String ApmChangePositionPanel;
            public String ApmChangeSizePanel;
            public Keys ApmHotkey1;
            public Keys ApmHotkey2;
            public Keys ApmHotkey3;
            public String ArmyTogglePanel;
            public String ArmyChangePositionPanel;
            public String ArmyChangeSizePanel;
            public Keys ArmyHotkey1;
            public Keys ArmyHotkey2;
            public Keys ArmyHotkey3;
            public String UnitTogglePanel;
            public String UnitChangePositionPanel;
            public String UnitChangeSizePanel;
            public Keys UnitHotkey1;
            public Keys UnitHotkey2;
            public Keys UnitHotkey3;
            public Keys GlobalChangeSizeAndPosition;
            public Boolean MaphackDisableDestinationLine;
            public Boolean GlobalOnlyDrawWhenUnpaused;
            public Boolean ResourceDrawBackground;
            public Boolean IncomeDrawBackground;
            public Boolean WorkerDrawBackground;
            public Boolean ApmDrawBackground;
            public Boolean ArmyDrawBackground;
            public Boolean MaphackColorDefensivestructuresYellow;
            public Boolean StealUnits;
            public Boolean StealUnitsInstant;
            public Boolean StealUnitsHotkey;
            public Keys StealUnitsHotkey1;
            public Keys StealUnitsHotkey2;
            public Keys StealUnitsHotkey3;
            public List<UnitId> MaphackUnitIds;
            public List<Color> MaphackUnitColors;
        };

        public struct UnitAssigner
        {
            public Int32 Id;
            public float Size;
            public Int32 Pointer;
            public Int32 MaximumHealth;
            public UnitModelStruct CustomStruct;
        };

        public enum RenderForm
        {
            Dummy,
            Maphack,
            Units,
            Resources,
            Income,
            Worker,
            Apm,
            Army,
            Production,
            Trainer,
            ExportIdsToFile,
            Automation,
        };

        public struct UnitCount
        {
            public Int32 UnitAmount;
            public float ConstructionState;
            public Int32 UnitUnderConsruction;
        }
    }
}
