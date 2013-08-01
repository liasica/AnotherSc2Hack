using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Sc2Hack.Classes.BackEnds
{
    class Offsets
    {
        private const string StrStarCraftProcessName = "SC2";
        private Process[] _pSc2;

        #region create variables for Addresses

        public int Struct = 0;
        public int StructUnit = 0;
        public int StructMap = 0;

        public Int32 RawCameraX = 0,
                     RawCameraY = 0,
                     RawCameraRotation = 0,
                     RawCameraDistance = 0,
                     RawPlayertype = 0,
                     RawStatus = 0,
                     RawNamelenght = 0,
                     RawName = 0,
                     RawColor = 0,
                     RawAccountId = 0,
                     RawApm = 0,
                     RawEpm = 0,
                     RawTeam = 0,
                     RawWorkers = 0,
                     RawSupplyMin = 0,
                     RawSupplyMax = 0,
                     RawMinerals = 0,
                     RawGas = 0,
                     RawMineralsIncome = 0,
                     RawGasIncome = 0,
                     RawMineralsArmy = 0,
                     RawGasArmy = 0,
                     RawLocalplayer = 0,
                     RawCurrentBuildings = 0,
                     RawSize = 0;

        public int CameraX = 0,
                            CameraY = 0,
                            CameraDistance = 0,
                            CameraRotation = 0,
                            Playertype = 0,
                            Status = 0,
                            NameLenght = 0,
                            Name = 0,
                            AccountId = 0,
                            Color = 0,
                            Apm = 0,
                            Epm = 0,
                            Workers = 0,
                            SupplyMin = 0,
                            SupplyMax = 0,
                            MineralsCurrent = 0,
                            GasCurrent = 0,
                            MineralsIncome = 0,
                            CurrentBuildings = 0,
                            GasIncome = 0,
                            MineralsArmy = 0,
                            GasArmy = 0,
                            Size = 0;

        public int Race = 0,
                            RaceSize = 0,
                            Team = 0,
                            TeamSize = 0;

        public int ChatBase = 0x017AB3C8,
                            ChatOff0 = 0x398,
                            ChatOff1 = 0x21C,
                            ChatOff2 = 0x004,
                            ChatOff3 = 0x004,
                            ChatOff4 = 0x014;

        public int Localplayer1 = 0,
                            Localplayer2 = 0,
                            Localplayer3 = 0,
                            Localplayer4 = 0;

        public Int32 RawUnitPosX = 0,
                     RawUnitPosY = 0,
                     RawUnitTargetFilter = 0,
                     RawUnitDestinationX = 0,
                     RawUnitDestinationY = 0,
                     RawUnitEnergy = 0,
                     RawUnitDamageTaken = 0,
                     RawUnitBuildingState = 0,
                     RawUnitOwner = 0,
                     RawUnitState = 0,
                     RawUnitModel = 0,
                     RawUnitMovestate = 0,
                     RawUnitStringStruct = 0,
                     RawUnitString = 0,
                     RawUnitSize = 0,
                     RawUnitModelId = 0,
                     RawUnitModelSize = 0;

        public int UnitPosX = 0,
                   UnitPosY = 0,
                   UnitTargetFilter = 0,
                   UnitTotal = 0,
                   UnitDeathType = 0,
                   UnitDestinationX = 0,
                   UnitDestinationY = 0,
                   UnitEnergy = 0,
                   UnitHp = 0,
                   UnitOwner = 0,
                   UnitState = 0,
                   UnitModel = 0,
                   UnitBeeingPuked = 0,
                   UnitMoveState = 0,
                   UnitStringStruct = 0,
                   UnitString = 0,
                   UnitSize = 0,
                   UnitModelId = 0,
                   UnitMaxHealth = 0,
                   UnitModelSize = 0;

        public Int32 StructureStruct = 0,
                     StructurePointerToUnitStruct = 0,
                     StructureHarvesterCount = 0,
                     StructureCount = 0,
                     StructureSize = 0;

        public Int32 RawStructureHarvesterCount = 0;

        public int MapIngame = 0,
                   MapTop = 0,
                   MapBottom = 0,
                   MapLeft = 0,
                   MapRight = 0,
                   MapFileInfoName = 0;

        public Int32 RawMapTop = 0,
                     RawMapBottom = 0,
                     RawMapRight = 0,
                     RawMapLeft = 0;

        public int UiSelectionStruct = 0,
                   UiTotalSelectedUnits = 0,
                   UiTotalSelectedTypes = 0,
                   UiSelectedType = 0,
                   UiSelectedIndex = 0,
                   UiSize = 0;

        public Int32 UiRawSelectionStruct = 0,
                     UiRawTotalSelectedUnits = 0,
                     UiRawTotalSelectedTypes = 0,
                     UiRawSelectedType = 0,
                     UiRawSelectedIndex = 0;

        public int TeamColor1 = 0,
                   TeamColor2 = 0;

        public int TimerData = 0;

        public int PauseEnabled = 0;

        public int Gamespeed = 0;

        public int FramesPerSecond = 0;

        public int Gametype = 0;

        #endregion

        public Offsets()
        {
            if (AdditionalMethods.GetProcess(StrStarCraftProcessName))
            {
                _pSc2 = Process.GetProcessesByName(StrStarCraftProcessName);
                AssignAddresses(_pSc2[0]);
            }

     
        }

        private void SetLastWorkingValues(Process starcraft)
        {
            //Playerinfo
            Struct = (int)starcraft.MainModule.BaseAddress + 0x0257BDA8; //V
            CameraX = Struct + 0x008; //
            CameraDistance = Struct + 0x00A; //
            CameraY = Struct + 0x00C; //
            Team = Struct + 0x01C; //
            Playertype = Struct + 0x01D; //
            Status = Struct + 0x01E; //
            Name = Struct + 0x058; //
            Color = Struct + 0x158; //
            Apm = Struct + 0x580; //
            Epm = Struct + 0x5C0; //
            CurrentBuildings = Struct + 0x6B0;
            AccountId = Struct + 0x1A8;
            Workers = Struct + 0x770; //
            SupplyMin = Struct + 0x848; //
            SupplyMax = Struct + 0x830; //
            MineralsCurrent = Struct + 0x880; //
            GasCurrent = Struct + 0x888; //
            MineralsIncome = Struct + 0x900; //
            GasIncome = Struct + 0x908; //
            MineralsArmy = Struct + 0xB68; //
            GasArmy = Struct + 0xB88; //
            Size = 0xCE0; //

            //Raw Playerdata
            RawCameraX = 0x008;
            RawCameraDistance = 0x00A;
            RawCameraY = 0x00C;
            RawTeam = 0x01C;
            RawPlayertype = 0x01D;
            RawStatus = 0x01E;
            RawName = 0x058;
            RawColor = 0x158;
            RawAccountId = 0x1A8;
            RawApm = 0x580;
            RawEpm = 0x5C0;
            RawCurrentBuildings = 0x6B0;
            RawWorkers = 0x770;
            RawSupplyMin = 0x848;
            RawSupplyMax = 0x830;
            RawMinerals = 0x880;
            RawGas = 0x888;
            RawMineralsIncome = 0x900;
            RawGasIncome = 0x908;
            RawMineralsArmy = 0xB68;
            RawGasArmy = 0xB88;
            RawSize = 0xCE0;
            //Race
            Race = (int)starcraft.MainModule.BaseAddress + 0x01ED0F00; //V
            RaceSize = 0x10; //V

            //ChatInput
            ChatBase = (int)starcraft.MainModule.BaseAddress + 0x0209C3C8; //V
            ChatOff0 = 0x3D4;
            ChatOff1 = 0x22C;
            ChatOff2 = 0x000;
            ChatOff3 = 0x000;
            ChatOff4 = 0x014;

            //Localplayer
            Localplayer4 = (int)starcraft.MainModule.BaseAddress + 0x010EB938; //V


            //Unitinfo
            StructUnit = (int)starcraft.MainModule.BaseAddress + 0x025F9300; //V
            UnitPosX = StructUnit + 0x44 + 4; //
            UnitPosY = StructUnit + 0x48 + 4; //
            UnitTargetFilter = StructUnit + 0x14; //
            UnitTotal = (int)starcraft.MainModule.BaseAddress + 0x025F92C0; //
            UnitDestinationX = StructUnit + 0x78 + 4; //
            UnitDestinationY = StructUnit + 0x7C + 4; //
            UnitHp = StructUnit + 0x110; //
            UnitEnergy = StructUnit + 0x114 + 4; //
            UnitOwner = StructUnit + 0x3D; //
            UnitState = StructUnit + 0x23 + 4; //
            UnitBeeingPuked = StructUnit + 0xD4 + 4; //
            UnitMoveState = StructUnit + 0x5C; //
            UnitStringStruct = 0x7DC;
            UnitString = 0x020;
            UnitModel = StructUnit + 8; //                                     
            UnitModelId = 0x06; //
            UnitModelSize = 0x39C; //
            UnitMaxHealth = 0x7F8;  //Not sure tho
            UnitSize = 0x1C0; //

            //Raw Unitdata
            RawUnitPosX = 0x48;
            RawUnitPosY = 0x4C;
            RawUnitDestinationX = 0x7C;
            RawUnitDestinationY = 0x80;
            RawUnitTargetFilter = 0x14;
            RawUnitDamageTaken = 0x110;
            RawUnitEnergy = 0x118;
            RawUnitOwner = 0x3D;
            RawUnitState = 0x27;
            RawUnitMovestate = 0x5C;
            RawUnitBuildingState = 0x30;
            RawUnitModel = 8;
            RawUnitModelId = 6;
            RawUnitStringStruct = 0x7DC;
            RawUnitString = 0x020;
            RawUnitModelSize = 0x39C;
            RawUnitSize = 0x1C0;

            /* Structure Struct */
            StructureStruct = (int)starcraft.MainModule.BaseAddress + 0x0329029C;
            StructureHarvesterCount = StructureStruct + 0x4C;
            StructureCount = (int)starcraft.MainModule.BaseAddress + 0x03290288;
            StructureSize = 0x94;

            //Mapinfo 
            StructMap = (int)starcraft.MainModule.BaseAddress + 0x024C9E30; //V
            MapLeft = StructMap + 0x128; //
            MapBottom = StructMap + 0x12C; //
            MapRight = StructMap + 0x130; //
            MapTop = StructMap + 0x134; //
            MapFileInfoName = 0x2A0;

            //Raw Mapadata
            RawMapLeft = 0x128;
            RawMapBottom = 0x12C;
            RawMapRight = 0x130;
            RawMapTop = 0x134;

            //Selected stuff
            UiSelectionStruct = (int)starcraft.MainModule.BaseAddress + 0x215FB50; //V
            UiTotalSelectedUnits = UiSelectionStruct + 0x0; //
            UiTotalSelectedTypes = UiSelectionStruct + 0x2; //
            UiSelectedType = UiSelectionStruct + 0x4; //
            UiSelectedIndex = UiSelectionStruct + 0x8; //
            UiSize = 4; //147

            UiRawTotalSelectedUnits = 0x0;
            UiRawTotalSelectedTypes = 0x2;
            UiRawSelectedType = 0x4;

            /* Is in a loop, has to be like this */
            UiRawSelectedIndex = 0x4;

            //TeamColor 
            TeamColor1 = (int)starcraft.MainModule.BaseAddress + 0x0209D4E4; //V
            TeamColor2 = (int)starcraft.MainModule.BaseAddress + 0x03ED4BF8; //V

            //Ingame Timer 
            TimerData = (int)starcraft.MainModule.BaseAddress + 0x0209C974;

            //Pause 
            PauseEnabled = (int)starcraft.MainModule.BaseAddress + 0x024C9E38; /* 0x022ab7b8
                                               * 0x25ef0b8 
                                               * 0x3de06e4 
                                               * 0x3e0fc20 */

            //Gamespeed 
            Gamespeed = (int)starcraft.MainModule.BaseAddress + 0x03E18628; //V

            //Fps VALID
            FramesPerSecond = (int)
                              starcraft.MainModule.BaseAddress + 0x03ED54DC; //V

            //Gametype NOT VALID
            Gametype = 0x0176DCC8;
        }

        public void AssignAddresses(Process starcraft)
        {
            #region HotS - 2.0.7.25293

            if (starcraft.MainModule.FileVersionInfo.FileVersion == "2.0.7.25293")
            {
                //Playerinfo
                Struct = (int) starcraft.MainModule.BaseAddress + 0x025771E0; //V
                CameraX = Struct + 0x008; //
                CameraDistance = Struct + 0x00A; //
                CameraY = Struct + 0x00C; //
                Team = Struct + 0x01C; //
                Playertype = Struct + 0x01D; //
                Status = Struct + 0x01E; //
                Name = Struct + 0x058; //
                Color = Struct + 0x158; //
                Apm = Struct + 0x580; //
                Epm = Struct + 0x5C0; //
                Workers = Struct + 0x770; //
                SupplyMin = Struct + 0x848; //
                SupplyMax = Struct + 0x830; //
                MineralsCurrent = Struct + 0x880; //
                GasCurrent = Struct + 0x888; //
                MineralsIncome = Struct + 0x900; //
                GasIncome = Struct + 0x908; //
                MineralsArmy = Struct + 0xB68; //
                GasArmy = Struct + 0xB88; //
                Size = 0xCE0; //

                //Raw Playerdata
                RawCameraX = 0x008;
                RawCameraDistance = 0x00A;
                RawCameraY = 0x00C;
                RawTeam = 0x01C;
                RawPlayertype = 0x01D;
                RawStatus = 0x01E;
                RawName = 0x058;
                RawColor = 0x158;
                RawApm = 0x580;
                RawEpm = 0x5C0;
                RawWorkers = 0x770;
                RawSupplyMin = 0x848;
                RawSupplyMax = 0x830;
                RawMinerals = 0x880;
                RawGas = 0x888;
                RawMineralsIncome = 0x900;
                RawGasIncome = 0x908;
                RawMineralsArmy = 0xB68;
                RawGasArmy = 0xB88;
                RawSize = 0xCE0;
                //Race
                Race = (int) starcraft.MainModule.BaseAddress + 0x01EC7E00; //V
                RaceSize = 0x10; //V

                //ChatInput
                ChatBase = (int) starcraft.MainModule.BaseAddress + 0x02097818; //V
                ChatOff0 = 0x3D0;
                ChatOff1 = 0x078;
                ChatOff2 = 0x224;
                ChatOff3 = 0x000;
                ChatOff4 = 0x014;

                //Localplayer
                Localplayer4 = (int) starcraft.MainModule.BaseAddress + 0x010E29C8; //V

                //Unitinfo
                StructUnit = (int) starcraft.MainModule.BaseAddress + 0x025F4740; //V
                UnitPosX = StructUnit + 0x44 + 4; //
                UnitPosY = StructUnit + 0x48 + 4; //
                UnitTargetFilter = StructUnit + 0x14; //
                UnitTotal = (int) starcraft.MainModule.BaseAddress + 0x02606B78; //
                UnitDestinationX = StructUnit + 0x78 + 4; //
                UnitDestinationY = StructUnit + 0x7C + 4; //
                UnitHp = StructUnit + 0x110; //
                UnitEnergy = StructUnit + 0x114 + 4; //
                UnitOwner = StructUnit + 0x3D; //
                UnitState = StructUnit + 0x23 + 4; //
                UnitBeeingPuked = StructUnit + 0xD4 + 4; //
                UnitMoveState = StructUnit + 0x5C; //
                UnitModel = StructUnit + 8; //                                     
                UnitModelId = 0x06; //
                UnitModelSize = 0x39C; //
                UnitSize = 0x1C0; //

                //Raw Unitdata
                RawUnitPosX = 0x48;
                RawUnitPosY = 0x4C;
                RawUnitDestinationX = 0x7C;
                RawUnitDestinationY = 0x80;
                RawUnitTargetFilter = 0x14;
                RawUnitDamageTaken = 0x110;
                RawUnitEnergy = 0x118;
                RawUnitOwner = 0x3D;
                RawUnitState = 0x27;
                RawUnitMovestate = 0x5C;
                RawUnitModel = 8;
                RawUnitModelId = 6;
                RawUnitModelSize = 0x39C;
                RawUnitSize = 0x1C0;

                //Mapinfo 
                StructMap = (int) starcraft.MainModule.BaseAddress + 0x024C52B4; //V
                MapLeft = StructMap + 0xDC; //
                MapBottom = StructMap + 0xE0; //
                MapRight = StructMap + 0xE4; //
                MapTop = StructMap + 0xE8; //

                //Raw Mapadata
                RawMapTop = 0xE8;
                RawMapRight = 0xE4;
                RawMapLeft = 0xDC;
                RawMapBottom = 0xE0;

                //Selected stuff
                UiSelectionStruct = (int) starcraft.MainModule.BaseAddress + 0x0215AF90; //V
                UiTotalSelectedUnits = UiSelectionStruct + 0x0; //
                UiTotalSelectedTypes = UiSelectionStruct + 0x2; //
                UiSelectedType = UiSelectionStruct + 0x4; //
                UiSelectedIndex = UiSelectionStruct + 0xA; //
                UiSize = 4; //

                //TeamColor 
                TeamColor1 = (int) starcraft.MainModule.BaseAddress + 0x0209892C; //V
                TeamColor2 = (int) starcraft.MainModule.BaseAddress + 0x03EC6768; //V

                //Ingame Timer 
                TimerData = (int) starcraft.MainModule.BaseAddress + 0x024C531C;

                //Pause 
                PauseEnabled = (int) starcraft.MainModule.BaseAddress + 0x25799C8; /* 0x022ab7b8
                                               * 0x25ef0b8 
                                               * 0x3de06e4 
                                               * 0x3e0fc20 */

                //Gamespeed 
                Gamespeed = (int) starcraft.MainModule.BaseAddress + 0x03E13648; //V

                //Fps VALID
                FramesPerSecond = (int)
                                  starcraft.MainModule.BaseAddress + 0x03EC704C; //V

                //Gametype NOT VALID
                Gametype = 0x0176DCC8;
            }

            #endregion

            #region HotS - 2.0.8.25604

            else if (starcraft.MainModule.FileVersionInfo.FileVersion == "2.0.8.25604")
            {
                //Playerinfo
                Struct = (int)starcraft.MainModule.BaseAddress + 0x0257BDA8; //V
                CameraX = Struct + 0x008; //
                CameraDistance = Struct + 0x00A; //
                CameraY = Struct + 0x00C; //
                Team = Struct + 0x01C; //
                Playertype = Struct + 0x01D; //
                Status = Struct + 0x01E; //
                Name = Struct + 0x058; //
                Color = Struct + 0x158; //
                Apm = Struct + 0x580; //
                Epm = Struct + 0x5C0; //
                CurrentBuildings = Struct + 0x6B0;
                Workers = Struct + 0x770; //
                SupplyMin = Struct + 0x848; //
                SupplyMax = Struct + 0x830; //
                MineralsCurrent = Struct + 0x880; //
                GasCurrent = Struct + 0x888; //
                MineralsIncome = Struct + 0x900; //
                GasIncome = Struct + 0x908; //
                MineralsArmy = Struct + 0xB68; //
                GasArmy = Struct + 0xB88; //
                Size = 0xCE0; //

                //Raw Playerdata
                RawCameraX = 0x008;
                RawCameraDistance = 0x00A;
                RawCameraY = 0x00C;
                RawTeam = 0x01C;
                RawPlayertype = 0x01D;
                RawStatus = 0x01E;
                RawName = 0x058;
                RawColor = 0x158;
                RawApm = 0x580;
                RawEpm = 0x5C0;
                RawCurrentBuildings = 0x6B0;
                RawWorkers = 0x770;
                RawSupplyMin = 0x848;
                RawSupplyMax = 0x830;
                RawMinerals = 0x880;
                RawGas = 0x888;
                RawMineralsIncome = 0x900;
                RawGasIncome = 0x908;
                RawMineralsArmy = 0xB68;
                RawGasArmy = 0xB88;
                RawSize = 0xCE0;
                //Race
                Race = (int)starcraft.MainModule.BaseAddress + 0x01ED0F00; //V
                RaceSize = 0x10; //V

                //ChatInput
                ChatBase = (int)starcraft.MainModule.BaseAddress + 0x0209C3C8; //V
                ChatOff0 = 0x3D4;
                ChatOff1 = 0x22C;
                ChatOff2 = 0x000;
                ChatOff3 = 0x000;
                ChatOff4 = 0x014;

                //Localplayer
                Localplayer4 = (int)starcraft.MainModule.BaseAddress + 0x010EB938; //V
                

                //Unitinfo
                StructUnit = (int)starcraft.MainModule.BaseAddress + 0x025F9300; //V
                UnitPosX = StructUnit + 0x44 + 4; //
                UnitPosY = StructUnit + 0x48 + 4; //
                UnitTargetFilter = StructUnit + 0x14; //
                UnitTotal = (int)starcraft.MainModule.BaseAddress + 0x025F92C0; //
                UnitDestinationX = StructUnit + 0x78 + 4; //
                UnitDestinationY = StructUnit + 0x7C + 4; //
                UnitHp = StructUnit + 0x110; //
                UnitEnergy = StructUnit + 0x114 + 4; //
                UnitOwner = StructUnit + 0x3D; //
                UnitState = StructUnit + 0x23 + 4; //
                UnitBeeingPuked = StructUnit + 0xD4 + 4; //
                UnitMoveState = StructUnit + 0x5C; //
                UnitStringStruct = 0x7DC;
                UnitString = 0x020;
                UnitModel = StructUnit + 8; //                                     
                UnitModelId = 0x06; //
                UnitModelSize = 0x39C; //
                UnitMaxHealth = 0x7F8;  //Not sure tho
                UnitSize = 0x1C0; //

                //Raw Unitdata
                RawUnitPosX = 0x48;
                RawUnitPosY = 0x4C;
                RawUnitDestinationX = 0x7C;
                RawUnitDestinationY = 0x80;
                RawUnitTargetFilter = 0x14;
                RawUnitDamageTaken = 0x110;
                RawUnitEnergy = 0x118;
                RawUnitOwner = 0x3D;
                RawUnitState = 0x27;
                RawUnitMovestate = 0x5C;
                RawUnitBuildingState = 0x30;
                RawUnitModel = 8;
                RawUnitModelId = 6;
                RawUnitStringStruct = 0x7DC;
                RawUnitString = 0x020;
                RawUnitModelSize = 0x39C;
                RawUnitSize = 0x1C0;

                /* Structure Struct */
                StructureStruct = (int) starcraft.MainModule.BaseAddress + 0x0329029C;
                StructureHarvesterCount = StructureStruct + 0x4C;
                StructureCount = (int) starcraft.MainModule.BaseAddress + 0x03290288;
                StructureSize = 0x94;

                //Mapinfo 
                StructMap = (int)starcraft.MainModule.BaseAddress + 0x024C9E30; //V
                MapLeft = StructMap + 0x128; //
                MapBottom = StructMap + 0x12C; //
                MapRight = StructMap + 0x130; //
                MapTop = StructMap + 0x134; //
                MapFileInfoName = 0x2A0;

                //Raw Mapadata
                RawMapLeft = 0x128;
                RawMapBottom = 0x12C;
                RawMapRight = 0x130;
                RawMapTop = 0x134;

                //Selected stuff
                UiSelectionStruct = (int)starcraft.MainModule.BaseAddress + 0x215FB50; //V
                UiTotalSelectedUnits = UiSelectionStruct + 0x0; //
                UiTotalSelectedTypes = UiSelectionStruct + 0x2; //
                UiSelectedType = UiSelectionStruct + 0x4; //
                UiSelectedIndex = UiSelectionStruct + 0x8; //
                UiSize = 4; //147

                UiRawTotalSelectedUnits = 0x0;
                UiRawTotalSelectedTypes = 0x2;
                UiRawSelectedType = 0x4;

                /* Is in a loop, has to be like this */
                UiRawSelectedIndex = 0x4;

                //TeamColor 
                TeamColor1 = (int)starcraft.MainModule.BaseAddress + 0x0209D4E4; //V
                TeamColor2 = (int)starcraft.MainModule.BaseAddress + 0x03ED4BF8; //V

                //Ingame Timer 
                TimerData = (int)starcraft.MainModule.BaseAddress + 0x0209C974;

                //Pause 
                PauseEnabled = (int)starcraft.MainModule.BaseAddress + 0x024C9E38; /* 0x022ab7b8
                                               * 0x25ef0b8 
                                               * 0x3de06e4 
                                               * 0x3e0fc20 */

                //Gamespeed 
                Gamespeed = (int)starcraft.MainModule.BaseAddress + 0x03E18628; //V

                //Fps VALID
                FramesPerSecond = (int)
                                  starcraft.MainModule.BaseAddress + 0x03ED54DC; //V

                //Gametype NOT VALID
                Gametype = 0x0176DCC8;
            }

            #endregion

            #region HotS - 2.0.9.26147

            else if (starcraft.MainModule.FileVersionInfo.FileVersion == "2.0.9.26147")
            {
                //Playerinfo
                Struct = (int)starcraft.MainModule.BaseAddress + 0x0257BDA8; //V
                CameraX = Struct + 0x008; //
                CameraDistance = Struct + 0x00A; //
                CameraY = Struct + 0x00C; //
                Team = Struct + 0x01C; //
                Playertype = Struct + 0x01D; //
                Status = Struct + 0x01E; //
                Name = Struct + 0x058; //
                AccountId = Struct + 0x1A8;
                Color = Struct + 0x158; //
                Apm = Struct + 0x580; //
                Epm = Struct + 0x5C0; //
                CurrentBuildings = Struct + 0x6B0;
                Workers = Struct + 0x770; //
                SupplyMin = Struct + 0x848; //
                SupplyMax = Struct + 0x830; //
                MineralsCurrent = Struct + 0x880; //
                GasCurrent = Struct + 0x888; //
                MineralsIncome = Struct + 0x900; //
                GasIncome = Struct + 0x908; //
                MineralsArmy = Struct + 0xB68; //
                GasArmy = Struct + 0xB88; //
                Size = 0xCE0; //

                //Raw Playerdata
                RawCameraX = 0x008;
                RawCameraDistance = 0x00A;
                RawCameraY = 0x00C;
                RawTeam = 0x01C;
                RawPlayertype = 0x01D;
                RawStatus = 0x01E;
                RawName = 0x058;
                RawColor = 0x158;
                RawAccountId = 0x1A8;
                RawApm = 0x580;
                RawEpm = 0x5C0;
                RawCurrentBuildings = 0x6B0;
                RawWorkers = 0x770;
                RawSupplyMin = 0x848;
                RawSupplyMax = 0x830;
                RawMinerals = 0x880;
                RawGas = 0x888;
                RawMineralsIncome = 0x900;
                RawGasIncome = 0x908;
                RawMineralsArmy = 0xB68;
                RawGasArmy = 0xB88;
                RawSize = 0xCE0;
                //Race
                Race = (int)starcraft.MainModule.BaseAddress + 0x01ED0F00; //V
                RaceSize = 0x10; //V

                //ChatInput
                ChatBase = (int)starcraft.MainModule.BaseAddress + 0x0209C3C8; //V
                ChatOff0 = 0x3D4;
                ChatOff1 = 0x22C;
                ChatOff2 = 0x000;
                ChatOff3 = 0x000;
                ChatOff4 = 0x014;

                //Localplayer
                Localplayer4 = (int)starcraft.MainModule.BaseAddress + 0x010EB938; //V


                //Unitinfo
                StructUnit = (int)starcraft.MainModule.BaseAddress + 0x025F9300; //V
                UnitPosX = StructUnit + 0x44 + 4; //
                UnitPosY = StructUnit + 0x48 + 4; //
                UnitTargetFilter = StructUnit + 0x14; //
                UnitTotal = (int)starcraft.MainModule.BaseAddress + 0x025F92C0; //
                UnitDestinationX = StructUnit + 0x78 + 4; //
                UnitDestinationY = StructUnit + 0x7C + 4; //
                UnitHp = StructUnit + 0x110; //
                UnitEnergy = StructUnit + 0x114 + 4; //
                UnitOwner = StructUnit + 0x3D; //
                UnitState = StructUnit + 0x23 + 4; //
                UnitBeeingPuked = StructUnit + 0xD4 + 4; //
                UnitMoveState = StructUnit + 0x5C; //
                UnitStringStruct = 0x7DC;
                UnitString = 0x020;
                UnitModel = StructUnit + 8; //                                     
                UnitModelId = 0x06; //
                UnitModelSize = 0x39C; //
                UnitMaxHealth = 0x7F8;  //Not sure tho
                UnitSize = 0x1C0; //

                //Raw Unitdata
                RawUnitPosX = 0x48;
                RawUnitPosY = 0x4C;
                RawUnitDestinationX = 0x7C;
                RawUnitDestinationY = 0x80;
                RawUnitTargetFilter = 0x14;
                RawUnitDamageTaken = 0x110;
                RawUnitEnergy = 0x118;
                RawUnitOwner = 0x3D;
                RawUnitState = 0x27;
                RawUnitMovestate = 0x5C;
                RawUnitBuildingState = 0x30;
                RawUnitModel = 8;
                RawUnitModelId = 6;
                RawUnitStringStruct = 0x7DC;
                RawUnitString = 0x020;
                RawUnitModelSize = 0x39C;
                RawUnitSize = 0x1C0;

                /* Structure Struct */
                StructureStruct = (int)starcraft.MainModule.BaseAddress + 0x0329029C;
                StructureHarvesterCount = StructureStruct + 0x4C;
                StructureCount = (int)starcraft.MainModule.BaseAddress + 0x03290288;
                StructureSize = 0x94;

                //Mapinfo 
                StructMap = (int)starcraft.MainModule.BaseAddress + 0x024C9E30; //V
                MapLeft = StructMap + 0x128; //
                MapBottom = StructMap + 0x12C; //
                MapRight = StructMap + 0x130; //
                MapTop = StructMap + 0x134; //
                MapFileInfoName = 0x2A0;

                //Raw Mapadata
                RawMapLeft = 0x128;
                RawMapBottom = 0x12C;
                RawMapRight = 0x130;
                RawMapTop = 0x134;

                //Selected stuff
                UiSelectionStruct = (int)starcraft.MainModule.BaseAddress + 0x215FB50; //V
                UiTotalSelectedUnits = UiSelectionStruct + 0x0; //
                UiTotalSelectedTypes = UiSelectionStruct + 0x2; //
                UiSelectedType = UiSelectionStruct + 0x4; //
                UiSelectedIndex = UiSelectionStruct + 0x8; //
                UiSize = 4; //147

                UiRawTotalSelectedUnits = 0x0;
                UiRawTotalSelectedTypes = 0x2;
                UiRawSelectedType = 0x4;

                /* Is in a loop, has to be like this */
                UiRawSelectedIndex = 0x4;

                //TeamColor 
                TeamColor1 = (int)starcraft.MainModule.BaseAddress + 0x0209D4E4; //V
                TeamColor2 = (int)starcraft.MainModule.BaseAddress + 0x03ED4BF8; //V

                //Ingame Timer 
                TimerData = (int)starcraft.MainModule.BaseAddress + 0x0209C974;

                //Pause 
                PauseEnabled = (int)starcraft.MainModule.BaseAddress + 0x024C9E38; /* 0x022ab7b8
                                               * 0x25ef0b8 
                                               * 0x3de06e4 
                                               * 0x3e0fc20 */

                //Gamespeed 
                Gamespeed = (int)starcraft.MainModule.BaseAddress + 0x03E18628; //V

                //Fps VALID
                FramesPerSecond = (int)
                                  starcraft.MainModule.BaseAddress + 0x03ED54DC; //V

                //Gametype NOT VALID
                Gametype = 0x0176DCC8;
            }

            #endregion

            #region HotS - 2.0.10.26585

            else if (starcraft.MainModule.FileVersionInfo.FileVersion == "2.0.10.26585")
            {
                //Playerinfo
                Struct = (int)starcraft.MainModule.BaseAddress + 0x035E6E00; //V
                CameraX = Struct + 0x008; //V
                CameraDistance = Struct + 0x00A; //V
                CameraY = Struct + 0x00C; //V
                Team = Struct + 0x01C; //V
                Playertype = Struct + 0x01D; //V
                Status = Struct + 0x01E; //V
                Name = Struct + 0x060; //V
                AccountId = Struct + 0x1C0; //V
                Color = Struct + 0x160; //V
                Apm = Struct + 0x598; //V
                Epm = Struct + 0x5D8; //V
                CurrentBuildings = Struct + 0x6B0; //Not needed?
                Workers = Struct + 0x788; //V
                SupplyMin = Struct + 0x860; //V
                SupplyMax = Struct + 0x848; //V
                MineralsCurrent = Struct + 0x8A0; //V
                GasCurrent = Struct + 0x8A8; //V
                MineralsIncome = Struct + 0x920; //V
                GasIncome = Struct + 0x928; //V
                MineralsArmy = Struct + 0xC08; //V
                GasArmy = Struct + 0xC30; //V
                Size = 0xDC0; //V

                //Raw Playerdata
                RawCameraX = 0x008;
                RawCameraDistance = 0x00A;
                RawCameraY = 0x00C;
                RawTeam = 0x01C;
                RawPlayertype = 0x01D;
                RawStatus = 0x01E;
                RawName = 0x060;
                RawColor = 0x160;
                RawAccountId = 0x1C0;
                RawApm = 0x598;
                RawEpm = 0x5D8;
                RawCurrentBuildings = 0x6B0;    //?
                RawWorkers = 0x788;
                RawSupplyMin = 0x860;
                RawSupplyMax = 0x848;
                RawMinerals = 0x8A0;
                RawGas = 0x8A8;
                RawMineralsIncome = 0x920;
                RawGasIncome = 0x928;
                RawMineralsArmy = 0xC08;
                RawGasArmy = 0xC30;
                RawSize = 0xDC0;

                //Race
                Race = (int)starcraft.MainModule.BaseAddress + 0x02F6C850; //V
                RaceSize = 0x10; //V

                //ChatInput
                ChatBase = (int)starcraft.MainModule.BaseAddress + 0x0031073C0; //V
                ChatOff0 = 0x3B0;
                ChatOff1 = 0x208;
                ChatOff2 = 0x000;
                ChatOff3 = 0x000;
                ChatOff4 = 0x014;

                //Localplayer
                Localplayer4 = (int)starcraft.MainModule.BaseAddress + 0x011265D8; //? or 11265D9  or 1126E00


                //Unitinfo
                StructUnit = (int)starcraft.MainModule.BaseAddress + 0x03665140; //V
                UnitPosX = StructUnit + 0x4C; //V
                UnitPosY = StructUnit + 0x50; //V
                UnitTargetFilter = StructUnit + 0x14; //?
                UnitTotal = (int)starcraft.MainModule.BaseAddress + 0x036650E8; //?
                UnitDestinationX = StructUnit + 0x80; //V
                UnitDestinationY = StructUnit + 0x84; //V
                UnitHp = StructUnit + 0x114; //V
                UnitEnergy = StructUnit + 0x11C; //?
                UnitOwner = StructUnit + 0x41; //?
                UnitState = StructUnit + 0x2B; //?
                UnitBeeingPuked = StructUnit + 0xDC; //?
                UnitMoveState = StructUnit + 0x60; //V
                UnitStringStruct = 0x7DC; //?
                UnitString = 0x020; //?
                UnitModel = StructUnit + 8; //?                              
                UnitModelId = 0x06; //?
                UnitModelSize = 0x39C + 0x10; //?
                UnitMaxHealth = 0x368;//0x7F8;  //Not sure tho
                UnitSize = 0x1C0; //?

                //Raw Unitdata
                RawUnitPosX = 0x4C;
                RawUnitPosY = 0x50;
                RawUnitDestinationX = 0x80;
                RawUnitDestinationY = 0x84;
                RawUnitTargetFilter = 0x14;
                RawUnitDamageTaken = 0x114;
                RawUnitEnergy = 0x11C;
                RawUnitOwner = 0x41;
                RawUnitState = 0x2B;
                RawUnitMovestate = 0x60;
                RawUnitBuildingState = 0x34; //?
                RawUnitModel = 8; //?
                RawUnitModelId = 6; //?
                RawUnitStringStruct = 0x7DC; //?
                RawUnitString = 0x020; //?
                RawUnitModelSize = 0x39C; //?
                RawUnitSize = 0x1C0; //?

                /* Structure Struct - UNUSED */
                StructureStruct = (int)starcraft.MainModule.BaseAddress + 0x0329029C;
                StructureHarvesterCount = StructureStruct + 0x4C;
                StructureCount = (int)starcraft.MainModule.BaseAddress + 0x03290288;
                StructureSize = 0x94;

                //Mapinfo 
                StructMap = (int)starcraft.MainModule.BaseAddress + 0x03534E90; //V
                MapLeft = StructMap + 0x128; //
                MapBottom = StructMap + 0x12C; //
                MapRight = StructMap + 0x130; //
                MapTop = StructMap + 0x134; //
                MapFileInfoName = 0x2A0;

                //Raw Mapadata
                RawMapLeft = 0x128;
                RawMapBottom = 0x12C;
                RawMapRight = 0x130;
                RawMapTop = 0x134;

                //Selected stuff - UNUSED
                UiSelectionStruct = (int)starcraft.MainModule.BaseAddress + 0x215FB50; //
                UiTotalSelectedUnits = UiSelectionStruct + 0x0; //
                UiTotalSelectedTypes = UiSelectionStruct + 0x2; //
                UiSelectedType = UiSelectionStruct + 0x4; //
                UiSelectedIndex = UiSelectionStruct + 0x8; //
                UiSize = 4; //147

                UiRawTotalSelectedUnits = 0x0;
                UiRawTotalSelectedTypes = 0x2;
                UiRawSelectedType = 0x4;

                /* Is in a loop, has to be like this */
                UiRawSelectedIndex = 0x4;

                //TeamColor 
                TeamColor1 = (int)starcraft.MainModule.BaseAddress + 0x03108504; //V
                TeamColor2 = (int)starcraft.MainModule.BaseAddress + 0x04FA7800; //V

                //Ingame Timer 
                TimerData = (int)starcraft.MainModule.BaseAddress + 0x031082F0; //V

                //Pause 
                PauseEnabled = (int)starcraft.MainModule.BaseAddress + 0x024C9E38; /* 0x022ab7b8
                                               * 0x25ef0b8 
                                               * 0x3de06e4 
                                               * 0x3e0fc20 */

                //Gamespeed 
                Gamespeed = (int)starcraft.MainModule.BaseAddress + 0x04EEB184; //V

                //Fps VALID
                FramesPerSecond = (int)
                                  starcraft.MainModule.BaseAddress + 0x03ED54DC; //

                //Gametype NOT VALID
                Gametype = 0x0176DCC8;
            }

            #endregion

            else
            {
                MessageBox.Show("This tool is outdated.\n" +
                                "Please be so kind and create a post in the forum\n" +
                                "so I can update it!", "Ouch... new version!?");

                SetLastWorkingValues(starcraft);
                
            }
        }
    }

    static class AdditionalMethods
    {
        public static bool GetProcess(string processName)
        {
            var procs = Process.GetProcesses();

            foreach (var process in procs)
            {
                if (process.ProcessName.Equals(processName))
                    return true;
            }

            return false;
        }
    }
}
