using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace Sc2Hack.Classes.BackEnds
{
    public class GameInfo
    {
        private IntPtr _hStarcraft = IntPtr.Zero;
        private Process _pStarcraft;
        private const string StrProcessName = "SC2";
        private Thread _thrWorker;
        private const Int32 MaxPlayerAmount = 16;
        private Stopwatch _swmainwatch = new Stopwatch();
        private readonly List<PredefinedTypes.UnitAssigner> _lUnitAssigner = new List<PredefinedTypes.UnitAssigner>();
        private bool _bSkip = false;

        readonly Offsets _of = new Offsets();

        /* Is able to shut the Worker- thread down */
        public void HandleThread(bool threadState)
        {
            if (threadState)
            {
                CThreadState = true;

                _thrWorker = new Thread(RefreshData);
                _thrWorker.Priority = ThreadPriority.Highest;
                _thrWorker.Name = "Worker \"RefreshData()\"";
                _thrWorker.Start();
            }

            else
            {
                if (_thrWorker != null)
                {
                    if (_thrWorker.IsAlive)
                        CThreadState = false;
                    
                }
            }
        }

        public GameInfo()
        {
            CSleepTime = 33;

            HandleThread(true);
        }

        /* Main- worker that refreshes data */
        private void RefreshData()
        {
            while (CThreadState)
            {
                #region Exceptions 

                /* Check if the Handle is unlocked */
                if (_hStarcraft == IntPtr.Zero)
                {
                    if (HelpFunctions.CheckProcess(Constants.StrStarcraft2ProcessName))
                    {
                        _pStarcraft = Process.GetProcessesByName(StrProcessName)[0];
                        _hStarcraft = InteropCalls.Help_OpenProcess((int) InteropCalls.ProcessAccess.VmRead, true,
                                                                    StrProcessName);
                        CStarcraft2 = _pStarcraft;

                        CWindowStyle = GetGWindowStyle();
                    }

                    else
                    {
                        Thread.Sleep(CSleepTime);
                        continue;
                    }
                }

                /* Check if ingame */
                if (!GetGIngame())
                {
                    Thread.Sleep(CSleepTime);
                    continue;
                }

                #endregion

                
                //DoMassiveTestScan();
                DoMassiveScan();

                //var stuff = GetGUnitProductionTime(1);

                Thread.Sleep(CSleepTime);
            }
        }

        /* Test purpose */
        private void DoMassiveTestScan()
        {
            
            /* Unit Buffer */
            var length = GetGUnitReadUnitCount();

            var thrWorkers = new Thread[Environment.ProcessorCount];
            for (var i = 0; i < thrWorkers.Length; i++)
            {
                thrWorkers[i] = new Thread(DoKernelCalls);
                thrWorkers[i].Start(length / Environment.ProcessorCount);
            }
        }

        /* Test purpose */
        private void DoKernelCalls(object lenght)
        {
            var length = (int) lenght;

            var size = length / Environment.ProcessorCount * _of.UnitSize;
            var chunk = InteropCalls.Help_ReadProcessMemory(_hStarcraft, _of.StructUnit, size);


            if (chunk.Length <= 0)
                return;



            var realUnitCount = chunk.Length / _of.UnitSize;

            if (realUnitCount <= 1)
                _lUnitAssigner.Clear();


            var lUnit = new List<PredefinedTypes.Unit>();
            for (var i = 0; i < realUnitCount; i++)
            {
                _bSkip = false;

                var u = new PredefinedTypes.Unit();

                u.PositionX = BitConverter.ToInt32(chunk, _of.RawUnitPosX + (i * _of.UnitSize));
                u.PositionY = BitConverter.ToInt32(chunk, _of.RawUnitPosY + (i * _of.UnitSize));
                u.DestinationPositionX = BitConverter.ToInt32(chunk, _of.RawUnitDestinationX + (i * _of.UnitSize));
                u.DestinationPositionY = BitConverter.ToInt32(chunk, _of.RawUnitDestinationY + (i * _of.UnitSize));
                u.DamageTaken = BitConverter.ToInt32(chunk, _of.RawUnitDamageTaken + (i * _of.UnitSize));
                u.TargetFilter = BitConverter.ToUInt64(chunk, _of.RawUnitTargetFilter + (i * _of.UnitSize));
                u.State = BitConverter.ToInt32(chunk, _of.RawUnitState + (i * _of.UnitSize));
                u.Owner = chunk[_of.RawUnitOwner + (i * _of.UnitSize)];
                u.Movestate = BitConverter.ToInt32(chunk, _of.RawUnitMovestate + (i * _of.UnitSize));
                u.Energy = BitConverter.ToInt32(chunk, _of.RawUnitEnergy + (i * _of.UnitSize));
                u.BuildingState = BitConverter.ToInt16(chunk, _of.RawUnitBuildingState + (i * _of.UnitSize));
                u.ModelPointer = BitConverter.ToInt32(chunk, _of.RawUnitModel + (i * _of.UnitSize));
                u.IsAlive = (u.TargetFilter & (UInt64)PredefinedTypes.TargetFilterFlag.Dead) == 0;
                u.IsUnderConstruction = (u.TargetFilter & (UInt64)PredefinedTypes.TargetFilterFlag.UnderConstruction) > 0;
                u.IsStructure = (u.TargetFilter & (UInt64)PredefinedTypes.TargetFilterFlag.Structure) > 0;



                for (var j = 0; j < _lUnitAssigner.Count; j++)
                {
                    if (_lUnitAssigner[j].Pointer == u.ModelPointer)
                    {
                        u.CustomStruct = _lUnitAssigner[j].CustomStruct;
                        lUnit.Add(u);
                        _bSkip = true;
                        break;
                    }
                }

                if (_bSkip)
                    continue;

                /* Assign a new Unit- assigner */
                var ua = new PredefinedTypes.UnitAssigner
                {
                    Pointer = u.ModelPointer,
                    CustomStruct = GetGUnitStruct(i)
                };


                _lUnitAssigner.Add(ua);

                lUnit.Add(u);
            }

            Unit = lUnit;
        }

        /* Test purpose */
        public static byte[] ObjectToByteArray(object _Object)
        {
            try
            {
                // create new memory stream
                System.IO.MemoryStream _MemoryStream = new System.IO.MemoryStream();

                // create new BinaryFormatter
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter _BinaryFormatter
                            = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                // Serializes an object, or graph of connected objects, to the given stream.
                _BinaryFormatter.Serialize(_MemoryStream, _Object);

                // convert stream to byte array and return
                return _MemoryStream.ToArray();
            }
            catch (Exception _Exception)
            {
                // Error
                Console.WriteLine("Exception caught in process: {0}", _Exception.ToString());
            }

            // Error occured, return null
            return null;
        }

        /* Check if a gigantic search is better */
        private void DoMassiveScan()
        {
            //_swmainwatch.Reset();
            //_swmainwatch.Start();

            #region Read all byteBuffers and store them

            /* Player Buffer */
            var playerLenght = MaxPlayerAmount*_of.Size;

            var playerChunk = InteropCalls.Help_ReadProcessMemory(_hStarcraft, _of.Struct, playerLenght);

            /* Unit Buffer */
            var length = GetGUnitReadUnitCount()*_of.UnitSize;

            var chunk = InteropCalls.Help_ReadProcessMemory(_hStarcraft, _of.StructUnit, length);

            /* Map Buffer */
            var mapLength = _of.RawMapTop + 4;

            var mapChunk = InteropCalls.Help_ReadProcessMemory(_hStarcraft, _of.StructMap, mapLength);

            /* Race Buffer */
            var racelenght = MaxPlayerAmount*_of.RaceSize;

            var raceChunk = InteropCalls.Help_ReadProcessMemory(_hStarcraft, _of.Race, racelenght);

            /* Structure Buffer */
            var structurelenght = GetStructureStructCount()*_of.StructureSize;

            var structureChunk = InteropCalls.Help_ReadProcessMemory(_hStarcraft, _of.StructureStruct, structurelenght);

            #endregion

            //_swmainwatch.Stop();
            //Debug.WriteLine("Time to read the entire buffer: " + _swmainwatch.ElapsedMilliseconds + " ms");

            //_swmainwatch.Reset();
            //_swmainwatch.Start();

            #region Playerinformation

            /* Create little race- array and catch all races */
            var lRace = new List<PredefinedTypes.Race>();
            for (var i = 0; i < MaxPlayerAmount; i++)
                lRace.Add(GetGPlayerRaceModified(Encoding.UTF8.GetString(raceChunk, i*_of.RaceSize, 4)));

            /* Counts the valid race- holders (Ai, Human) */
            var iRaceCounter = 0;

            if (playerChunk.Length <= 0)
                return;


            var lPlayer1 = new List<PredefinedTypes.Player>();
            for (var i = 0; i < MaxPlayerAmount; i++)
            {

                var u = new PredefinedTypes.Player();

                u.CameraPositionX = BitConverter.ToInt32(playerChunk, _of.RawCameraX + (i*_of.Size));
                u.CameraPositionY = BitConverter.ToInt32(playerChunk, _of.RawCameraY + (i*_of.Size));
                u.CameraDistance = BitConverter.ToInt32(playerChunk, _of.RawCameraDistance + (i*_of.Size));
                u.CameraRotation = BitConverter.ToInt32(playerChunk, _of.RawCameraRotation + (i*_of.Size));
                u.Status = GetGPlayerStatusModified(playerChunk[_of.RawStatus + (i*_of.Size)]);
                u.Type = GetGPlayerTypeModified(playerChunk[_of.RawPlayertype + (i*_of.Size)]);
                u.NameLength = BitConverter.ToInt32(playerChunk, _of.RawNamelenght + (i*_of.Size));
                u.Name = Encoding.UTF8.GetString(playerChunk, _of.RawName + (i*_of.Size), 16);
                u.Color = GetGPlayerColorModified(BitConverter.ToInt32(playerChunk, _of.RawColor + (i*_of.Size)));
                u.Apm = BitConverter.ToInt32(playerChunk, _of.RawApm + (i*_of.Size));
                u.Epm = BitConverter.ToInt32(playerChunk, _of.RawEpm + (i*_of.Size));
                u.Worker = BitConverter.ToInt32(playerChunk, _of.RawWorkers + (i*_of.Size));
                u.AccountId = Encoding.UTF8.GetString(playerChunk, _of.RawAccountId + (i*_of.Size), 16);
                u.SupplyMaxRaw = BitConverter.ToInt32(playerChunk, _of.RawSupplyMax + (i*_of.Size));
                u.SupplyMinRaw = BitConverter.ToInt32(playerChunk, _of.RawSupplyMin + (i*_of.Size));
                u.SupplyMax = u.SupplyMaxRaw/4096;
                u.SupplyMin = u.SupplyMinRaw/4096;
                u.CurrentBuildings = BitConverter.ToInt32(playerChunk, _of.RawCurrentBuildings + (i*_of.Size));
                u.Minerals = BitConverter.ToInt32(playerChunk, _of.RawMinerals + (i*_of.Size));
                u.Gas = BitConverter.ToInt32(playerChunk, _of.RawGas + (i*_of.Size));
                u.MineralsIncome = BitConverter.ToInt32(playerChunk, _of.RawMineralsIncome + (i*_of.Size));
                u.GasIncome = BitConverter.ToInt32(playerChunk, _of.RawGasIncome + (i*_of.Size));
                u.MineralsArmy = BitConverter.ToInt32(playerChunk, _of.RawMineralsArmy + (i*_of.Size));
                u.GasArmy = BitConverter.ToInt32(playerChunk, _of.RawGasArmy + (i*_of.Size));
                u.Team = playerChunk[_of.RawTeam + (i*_of.Size)];
                u.Localplayer = GetGPlayerLocalplayer();
                u.IsLocalplayer = u.Localplayer == i;

                if (u.Type == PredefinedTypes.PlayerType.Human ||
                    u.Type == PredefinedTypes.PlayerType.Ai)
                {
                    u.Race = lRace[iRaceCounter];
                    iRaceCounter++;
                }

                lPlayer1.Add(u);



            }

            Player = lPlayer1;

            #endregion

            //_swmainwatch.Stop();
            //Debug.WriteLine("Time to map the Playerstruct: " + _swmainwatch.ElapsedMilliseconds + " ms");

            //_swmainwatch.Reset();
            //_swmainwatch.Start();

            #region UnitInformation

            if (chunk.Length <= 0)
                return;



            var realUnitCount = chunk.Length/_of.UnitSize;

            if (realUnitCount <= 0)
                _lUnitAssigner.Clear();


            var lUnit = new List<PredefinedTypes.Unit>();
            for (var i = 0; i < realUnitCount; i++)
            {
                _bSkip = false;

                var u = new PredefinedTypes.Unit();

                u.PositionX = BitConverter.ToInt32(chunk, _of.RawUnitPosX + (i*_of.UnitSize));
                u.PositionY = BitConverter.ToInt32(chunk, _of.RawUnitPosY + (i*_of.UnitSize));
                u.DestinationPositionX = BitConverter.ToInt32(chunk, _of.RawUnitDestinationX + (i*_of.UnitSize));
                u.DestinationPositionY = BitConverter.ToInt32(chunk, _of.RawUnitDestinationY + (i*_of.UnitSize));
                u.DamageTaken = BitConverter.ToInt32(chunk, _of.RawUnitDamageTaken + (i*_of.UnitSize));
                u.TargetFilter = BitConverter.ToUInt64(chunk, _of.RawUnitTargetFilter + (i*_of.UnitSize));
                u.State = BitConverter.ToInt32(chunk, _of.RawUnitState + (i*_of.UnitSize));
                u.Owner = chunk[_of.RawUnitOwner + (i*_of.UnitSize)];
                u.Movestate = BitConverter.ToInt32(chunk, _of.RawUnitMovestate + (i*_of.UnitSize));
                u.Energy = BitConverter.ToInt32(chunk, _of.RawUnitEnergy + (i*_of.UnitSize));
                u.BuildingState = BitConverter.ToInt16(chunk, _of.RawUnitBuildingState + (i*_of.UnitSize));
                u.ModelPointer = BitConverter.ToInt32(chunk, _of.RawUnitModel + (i*_of.UnitSize));
                u.IsAlive = (u.TargetFilter & (UInt64) PredefinedTypes.TargetFilterFlag.Dead) == 0;
                u.IsUnderConstruction = (u.TargetFilter & (UInt64) PredefinedTypes.TargetFilterFlag.UnderConstruction) > 0;
                u.IsStructure = (u.TargetFilter & (UInt64) PredefinedTypes.TargetFilterFlag.Structure) > 0;

               

                for (var j = 0; j < _lUnitAssigner.Count; j++)
                {
                    if (_lUnitAssigner[j].Pointer == u.ModelPointer)
                    {
                        u.CustomStruct = _lUnitAssigner[j].CustomStruct;
                        lUnit.Add(u);
                        _bSkip = true;
                        break;
                    }
                }

                if (_bSkip)
                    continue;

                /* Assign a new Unit- assigner */
                var ua = new PredefinedTypes.UnitAssigner
                    {
                        Pointer = u.ModelPointer,
                        CustomStruct = GetGUnitStruct(i)
                    };


                _lUnitAssigner.Add(ua);

                lUnit.Add(u);
            }

            Unit = lUnit;

            #endregion

            //_swmainwatch.Stop();
            //Debug.WriteLine("Time to map the Unitstruct: " + _swmainwatch.ElapsedMilliseconds + " ms");

            //_swmainwatch.Reset();
            //_swmainwatch.Start();

            #region MapInformation

            var map = new PredefinedTypes.Map
                {
                    Bottom = BitConverter.ToInt32(mapChunk, _of.RawMapBottom),
                    Top = BitConverter.ToInt32(mapChunk, _of.RawMapTop),
                    Right = BitConverter.ToInt32(mapChunk, _of.RawMapRight),
                    Left = BitConverter.ToInt32(mapChunk, _of.RawMapLeft),
                };

            map.PlayableWidth = map.Right - map.Left;
            map.PlayableHeight = map.Top - map.Bottom;

            Map = map;

            #endregion

            //_swmainwatch.Stop();
            //Debug.WriteLine("Time to map the Mapstruct: " + _swmainwatch.ElapsedMilliseconds + " ms");

            #region StructureStruct



            #endregion

            //_swmainwatch.Reset();
            //_swmainwatch.Start();

            #region Gameinformation

            var gInfo = new PredefinedTypes.Gameinformation
                {
                    ChatInput = GetGChatInput(),
                    Timer = GetGTimer(),
                    IsIngame = GetGIngame(),
                    Fps = GetGFps(),
                    Speed = GetGGamespeed(),
                    IsTeamcolor = GetGTeamcolor(),
                    Style = GetGWindowStyle(),
                    _IValidPlayerCount = HelpFunctions.GetValidPlayerCount(lPlayer1),
                    Pause = GetGPause()
                };

            Gameinfo = gInfo;

            #endregion

            //_swmainwatch.Stop();
            //Debug.WriteLine("Time to map the Gameinfo struct: " + _swmainwatch.ElapsedMilliseconds + " ms");


        }

        /* Get internet information */
        private PredefinedTypes.LeagueInfo GetLeagueInformation(PredefinedTypes.Player p)
        {
            var lInfomation = new PredefinedTypes.LeagueInfo();

            var wcGetString = new WebClient();
            wcGetString.Proxy = null;

            var strDownloadContent = String.Empty;

            /* Build string */
            strDownloadContent = "http://sc2ranks.com/eu/" + p.AccountId.Substring(7) + "/" + p.Name;



            Debug.WriteLine(strDownloadContent);

            return lInfomation;
        }

        #region Functions to get Playerinformation


        /* 4 Bytes */
        private Int32 GetGPlayerCameraX(Int32 iPlayerNum)
        {
            return
                (BitConverter.ToInt32(
                    InteropCalls.Help_ReadProcessMemory(_hStarcraft, _of.CameraX + (_of.Size*iPlayerNum), sizeof (Int32)),
                    0));
        }

        /* 4 Bytes */
        private Int32 GetGPlayerCameraDistance(Int32 iPlayerNum)
        {
            return
                (BitConverter.ToInt32(
                    InteropCalls.Help_ReadProcessMemory(_hStarcraft, _of.CameraDistance + (_of.Size * iPlayerNum), sizeof(Int32)),
                    0));
        }

        /* 4 Bytes */
        private Int32 GetGPlayerCameraY(Int32 iPlayerNum)
        {
            return
                (BitConverter.ToInt32(
                    InteropCalls.Help_ReadProcessMemory(_hStarcraft, _of.CameraY + (_of.Size * iPlayerNum), sizeof(Int32)),
                    0));
        }

        /* 4 Bytes */
        private Int32 GetGPlayerCameraRotation(Int32 iPlayerNum)
        {
            return
                (BitConverter.ToInt32(
                    InteropCalls.Help_ReadProcessMemory(_hStarcraft, _of.CameraRotation + (_of.Size * iPlayerNum), sizeof(Int32)),
                    0));
        }

        private PredefinedTypes.PlayerStatus GetGPlayerStatusModified(Byte bValue)
        {
            switch (bValue)
            {
                case (Int32)PredefinedTypes.PlayerStatus.Lost:
                    return PredefinedTypes.PlayerStatus.Lost;

                case (Int32)PredefinedTypes.PlayerStatus.Playing:
                    return PredefinedTypes.PlayerStatus.Playing;

                case (Int32)PredefinedTypes.PlayerStatus.Tied:
                    return PredefinedTypes.PlayerStatus.Tied;

                case (Int32)PredefinedTypes.PlayerStatus.Won:
                    return PredefinedTypes.PlayerStatus.Won;

                default:
                    return PredefinedTypes.PlayerStatus.NotDefined;
            }
        }

        /* 1 Byte */
        private PredefinedTypes.PlayerType GetGPlayerType(Int32 iPlayerNum)
        {
            var iResult = InteropCalls.Help_ReadProcessMemory(_hStarcraft, _of.Playertype + (_of.Size*iPlayerNum), sizeof (byte))[0];
                  

            switch (iResult)
            {
                case (Int32)PredefinedTypes.PlayerType.Ai:
                    return PredefinedTypes.PlayerType.Ai;

                case (Int32)PredefinedTypes.PlayerType.Hostile:
                    return PredefinedTypes.PlayerType.Hostile;

                case (Int32)PredefinedTypes.PlayerType.Human:
                    return PredefinedTypes.PlayerType.Human;

                case (Int32)PredefinedTypes.PlayerType.Neutral:
                    return PredefinedTypes.PlayerType.Neutral;

                case (Int32)PredefinedTypes.PlayerType.Observer:
                    return PredefinedTypes.PlayerType.Observer;

                case (Int32)PredefinedTypes.PlayerType.Referee:
                    return PredefinedTypes.PlayerType.Referee;

                default:
                    return PredefinedTypes.PlayerType.NotDefined;
            }
        }

        /* Translates pure data into types */
        private PredefinedTypes.PlayerType GetGPlayerTypeModified(Byte bValue)
        {
            switch (bValue)
            {
                case (Int32)PredefinedTypes.PlayerType.Ai:
                    return PredefinedTypes.PlayerType.Ai;

                case (Int32)PredefinedTypes.PlayerType.Hostile:
                    return PredefinedTypes.PlayerType.Hostile;

                case (Int32)PredefinedTypes.PlayerType.Human:
                    return PredefinedTypes.PlayerType.Human;

                case (Int32)PredefinedTypes.PlayerType.Neutral:
                    return PredefinedTypes.PlayerType.Neutral;

                case (Int32)PredefinedTypes.PlayerType.Observer:
                    return PredefinedTypes.PlayerType.Observer;

                case (Int32)PredefinedTypes.PlayerType.Referee:
                    return PredefinedTypes.PlayerType.Referee;

                default:
                    return PredefinedTypes.PlayerType.NotDefined;
            }
        }

        /* 4 Bytes */
        private Int32 GetGPlayerNameLength(Int32 iPlayerNum)
        {
            return
                (BitConverter.ToInt32(
                    InteropCalls.Help_ReadProcessMemory(_hStarcraft, _of.NameLenght + (_of.Size * iPlayerNum), sizeof(Int32)),
                    0));
        }

        /* 16 Bytes? */
        private String GetGPlayerName(Int32 iPlayerNum)
        {
            var strName =
                Encoding.UTF8.GetString(
                    InteropCalls.Help_ReadProcessMemory(_hStarcraft, _of.Name + (_of.Size*iPlayerNum), 32));

            strName += "\0";
                

            return strName;
        }

        /* 4 Bytes */
        private Color GetGPlayerColor(Int32 iPlayerNum)
        {
            Int32 iResult =
                (BitConverter.ToInt32(
                    InteropCalls.Help_ReadProcessMemory(_hStarcraft, _of.Color + (_of.Size*iPlayerNum), sizeof (Int32)),
                    0));

            switch (iResult)
            {
                case (Int32)PredefinedTypes.PlayerColor.Blue:
                    return Color.Blue;

                case (Int32)PredefinedTypes.PlayerColor.Brown:
                    return Color.Brown;

                case (Int32)PredefinedTypes.PlayerColor.DarkGray:
                    return Color.DarkGray;

                case (Int32)PredefinedTypes.PlayerColor.DarkGreen:
                    return Color.DarkGreen;

                case (Int32)PredefinedTypes.PlayerColor.Green:
                    return Color.Green;

                case (Int32)PredefinedTypes.PlayerColor.LightGray:
                    return Color.LightGray;

                case (Int32)PredefinedTypes.PlayerColor.LightGreen:
                    return Color.LightGreen;

                case (Int32)PredefinedTypes.PlayerColor.LightPink:
                    return Color.LightPink;

                case (Int32)PredefinedTypes.PlayerColor.Orange:
                    return Color.Orange;

                case (Int32)PredefinedTypes.PlayerColor.Pink:
                    return Color.Pink;

                case (Int32)PredefinedTypes.PlayerColor.Purple:
                    return Color.Purple;

                case (Int32)PredefinedTypes.PlayerColor.Red:
                    return Color.Red;

                case (Int32)PredefinedTypes.PlayerColor.Teal:
                    return Color.Teal;

                case (Int32)PredefinedTypes.PlayerColor.Violet:
                    return Color.Violet;

                case (Int32)PredefinedTypes.PlayerColor.White:
                    return Color.White;

                default:
                    return Color.Yellow;
            }


        }

        /* Translates pure data into types */
        private Color GetGPlayerColorModified(Int32 iValue)
        {
            switch (iValue)
            {
                case (Int32)PredefinedTypes.PlayerColor.Blue:
                    return Color.Blue;

                case (Int32)PredefinedTypes.PlayerColor.Brown:
                    return Color.Brown;

                case (Int32)PredefinedTypes.PlayerColor.DarkGray:
                    return Color.DarkGray;

                case (Int32)PredefinedTypes.PlayerColor.DarkGreen:
                    return Color.DarkGreen;

                case (Int32)PredefinedTypes.PlayerColor.Green:
                    return Color.Green;

                case (Int32)PredefinedTypes.PlayerColor.LightGray:
                    return Color.LightGray;

                case (Int32)PredefinedTypes.PlayerColor.LightGreen:
                    return Color.LightGreen;

                case (Int32)PredefinedTypes.PlayerColor.LightPink:
                    return Color.LightPink;

                case (Int32)PredefinedTypes.PlayerColor.Orange:
                    return Color.Orange;

                case (Int32)PredefinedTypes.PlayerColor.Pink:
                    return Color.Pink;

                case (Int32)PredefinedTypes.PlayerColor.Purple:
                    return Color.Purple;

                case (Int32)PredefinedTypes.PlayerColor.Red:
                    return Color.Red;

                case (Int32)PredefinedTypes.PlayerColor.Teal:
                    return Color.Teal;

                case (Int32)PredefinedTypes.PlayerColor.Violet:
                    return Color.Violet;

                case (Int32)PredefinedTypes.PlayerColor.White:
                    return Color.White;

                default:
                    return Color.Yellow;
            }
        }

        /* 4 Bytes */ 
        private Int32 GetGPlayerApm(Int32 iPlayerNum)
        {
            return
                (BitConverter.ToInt32(
                    InteropCalls.Help_ReadProcessMemory(_hStarcraft, _of.Apm + (_of.Size * iPlayerNum), sizeof(Int32)),
                    0));
        }

        /* 4 Bytes */
        private Int32 GetGPlayerEpm(Int32 iPlayerNum)
        {
            return
                (BitConverter.ToInt32(
                    InteropCalls.Help_ReadProcessMemory(_hStarcraft, _of.Epm + (_of.Size * iPlayerNum), sizeof(Int32)),
                    0));
        }

        /* 4 Bytes */
        private Int32 GetGPlayerWorker(Int32 iPlayerNum)
        {
            return
                (BitConverter.ToInt32(
                    InteropCalls.Help_ReadProcessMemory(_hStarcraft, _of.Workers + (_of.Size * iPlayerNum), sizeof(Int32)),
                    0));
        }

        /* 4 Bytes */
        private Int32 GetGPlayerSupplyMinRaw(Int32 iPlayerNum)
        {
            return
                (BitConverter.ToInt32(
                    InteropCalls.Help_ReadProcessMemory(_hStarcraft, _of.SupplyMin + (_of.Size * iPlayerNum), sizeof(Int32)),
                    0));
        }

        /* 4 Bytes */
        private Int32 GetGPlayerSupplyMaxRaw(Int32 iPlayerNum)
        {
            return
                (BitConverter.ToInt32(
                    InteropCalls.Help_ReadProcessMemory(_hStarcraft, _of.SupplyMax + (_of.Size * iPlayerNum), sizeof(Int32)),
                    0));
        }

        /* 4 Bytes */
        private Int32 GetGPlayerMinerals(Int32 iPlayerNum)
        {
            return
                (BitConverter.ToInt32(
                    InteropCalls.Help_ReadProcessMemory(_hStarcraft, _of.MineralsCurrent + (_of.Size * iPlayerNum), sizeof(Int32)),
                    0));
        }

        /* 4 Bytes */
        private Int32 GetGPlayerGas(Int32 iPlayerNum)
        {
            return
                (BitConverter.ToInt32(
                    InteropCalls.Help_ReadProcessMemory(_hStarcraft, _of.GasCurrent + (_of.Size * iPlayerNum), sizeof(Int32)),
                    0));
        }

        /* 4 Bytes */
        private Int32 GetGPlayerMineralsIncome(Int32 iPlayerNum)
        {
            return
                (BitConverter.ToInt32(
                    InteropCalls.Help_ReadProcessMemory(_hStarcraft, _of.MineralsIncome + (_of.Size * iPlayerNum), sizeof(Int32)),
                    0));
        }

        /* 4 Bytes */
        private Int32 GetGPlayerGasIncome(Int32 iPlayerNum)
        {
            return
                (BitConverter.ToInt32(
                    InteropCalls.Help_ReadProcessMemory(_hStarcraft, _of.GasIncome + (_of.Size * iPlayerNum), sizeof(Int32)),
                    0));
        }

        /* 4 Bytes */
        private Int32 GetGPlayerMineralsArmy(Int32 iPlayerNum)
        {
            return
                (BitConverter.ToInt32(
                    InteropCalls.Help_ReadProcessMemory(_hStarcraft, _of.MineralsArmy + (_of.Size * iPlayerNum), sizeof(Int32)),
                    0));
        }

        /* 4 Bytes */
        private Int32 GetGPlayerGasArmy(Int32 iPlayerNum)
        {
            return
                (BitConverter.ToInt32(
                    InteropCalls.Help_ReadProcessMemory(_hStarcraft, _of.GasArmy + (_of.Size * iPlayerNum), sizeof(Int32)),
                    0));
        }

        /* 1 Bytes */
        private Int32 GetGPlayerTeam(Int32 iPlayerNum)
        {
            return
                    InteropCalls.Help_ReadProcessMemory(_hStarcraft, _of.Team + (_of.Size*iPlayerNum), sizeof (byte))[0];
        }

        /* 4 Bytes */
        private PredefinedTypes.Race GetGPlayerRace(Int32 iPlayerNum)
        {
            var strResult =
                Encoding.UTF8.GetString(
                    InteropCalls.Help_ReadProcessMemory(_hStarcraft, _of.Race + (_of.RaceSize * iPlayerNum), sizeof(Int32)));

            switch (strResult)
            {
                case "Terr":
                    return PredefinedTypes.Race.Terran;

                case "Zerg":
                    return PredefinedTypes.Race.Zerg;

                case "Prot":
                    return PredefinedTypes.Race.Protoss;

                default:
                    return PredefinedTypes.Race.Other;
            }
        }

        /* Translates pure data into types */
        private PredefinedTypes.Race GetGPlayerRaceModified(String strValue)
        {
            switch (strValue)
            {
                case "Terr":
                    return PredefinedTypes.Race.Terran;

                case "Zerg":
                    return PredefinedTypes.Race.Zerg;

                case "Prot":
                    return PredefinedTypes.Race.Protoss;

                default:
                    return PredefinedTypes.Race.Other;
            }
        }

        /* 1 Byte */
        private Int32 GetGPlayerLocalplayer()
        {
            return InteropCalls.Help_ReadProcessMemory(_hStarcraft, _of.Localplayer4, sizeof (byte))[0];
        }


        #endregion

        #region Functions to get Unitinformation

        /* 4 Bytes */
        private Int32 GetGUnitPositionX(Int32 iUnitNum)
        {
            return
                (BitConverter.ToInt32(
                    InteropCalls.Help_ReadProcessMemory(_hStarcraft, _of.UnitPosX + (_of.UnitSize * iUnitNum), sizeof(Int32)),
                    0));
        }

        /* 4 Bytes */
        private Int32 GetGUnitPositionY(Int32 iUnitNum)
        {
            return
                (BitConverter.ToInt32(
                    InteropCalls.Help_ReadProcessMemory(_hStarcraft, _of.UnitPosY + (_of.UnitSize * iUnitNum), sizeof(Int32)),
                    0));
        }

        /* 4 Bytes */
        private Int32 GetGUnitDestinationPositionX(Int32 iUnitNum)
        {
            return
                (BitConverter.ToInt32(
                    InteropCalls.Help_ReadProcessMemory(_hStarcraft, _of.UnitDestinationX + (_of.UnitSize * iUnitNum), sizeof(Int32)),
                    0));
        }

        /* 4 Bytes */
        private Int32 GetGUnitDestinationPositionY(Int32 iUnitNum)
        {
            return
                (BitConverter.ToInt32(
                    InteropCalls.Help_ReadProcessMemory(_hStarcraft, _of.UnitDestinationY + (_of.UnitSize * iUnitNum), sizeof(Int32)),
                    0));
        }

        /* 8 Bytes */
        private UInt64 GetGUnitTargetFilter(Int32 iUnitNum)
        {
            return
                (BitConverter.ToUInt64(
                    InteropCalls.Help_ReadProcessMemory(_hStarcraft, _of.UnitTargetFilter + (_of.UnitSize * iUnitNum), sizeof(UInt64)),
                    0));
        }

        /* 4 Bytes */
        private Int32 GetGUnitDamageTaken(Int32 iUnitNum)
        {
            return
                (BitConverter.ToInt32(
                    InteropCalls.Help_ReadProcessMemory(_hStarcraft, _of.UnitHp + (_of.UnitSize * iUnitNum), sizeof(Int32)),
                    0));
        }

        /* 4 Bytes */
        private Int32 GetGUnitEnergy(Int32 iUnitNum)
        {
            return
                (BitConverter.ToInt32(
                    InteropCalls.Help_ReadProcessMemory(_hStarcraft, _of.UnitEnergy + (_of.UnitSize * iUnitNum), sizeof(Int32)),
                    0));
        }

        /* 1 Byte */
        private Int32 GetGUnitOwner(Int32 iUnitNum)
        {
            return
                (BitConverter.ToInt32(
                    InteropCalls.Help_ReadProcessMemory(_hStarcraft, _of.UnitOwner + (_of.UnitSize * iUnitNum), sizeof(Int32)),
                    0));
        }

        /* 4 Bytes */
        private Int32 GetGUnitState(Int32 iUnitNum)
        {
            return
                (BitConverter.ToInt32(
                    InteropCalls.Help_ReadProcessMemory(_hStarcraft, _of.UnitState + (_of.UnitSize * iUnitNum), sizeof(Int32)),
                    0));
        }

        /* 4 Bytes */
        private Int32 GetGUnitMoveState(Int32 iUnitNum)
        {
            return
                (BitConverter.ToInt32(
                    InteropCalls.Help_ReadProcessMemory(_hStarcraft, _of.UnitMoveState + (_of.UnitSize * iUnitNum), sizeof(Int32)),
                    0));
        }

        /* 2 Bytes */
        private Int32 GetGUnitId(Int32 iUnitNum)
        {
            var iContentofUnitModel =
                BitConverter.ToInt32(
                    InteropCalls.Help_ReadProcessMemory(_hStarcraft,
                                         _of.UnitModel + _of.UnitSize*iUnitNum, 4), 0);

            var iContentofUnitModelShifted = (iContentofUnitModel << 5) & 0xFFFFFFFF;

            var id =
                BitConverter.ToInt16(
                    InteropCalls.Help_ReadProcessMemory(_hStarcraft, _of.UnitModelId + (int)iContentofUnitModelShifted, 2),
                    0);

            return id;
        }

        /* Get the name */
        private PredefinedTypes.UnitModelStruct GetGUnitStruct(Int32 iUnitNum)
        {
            var iContentofUnitModel =
                BitConverter.ToInt32(
                    InteropCalls.Help_ReadProcessMemory(_hStarcraft,
                                         _of.UnitModel + _of.UnitSize * iUnitNum, 4), 0);

            var iContentofUnitModelShifted = (iContentofUnitModel << 5) & 0xFFFFFFFF;

            /* Id - 2 Byte*/
            var id =
                BitConverter.ToInt16(
                    InteropCalls.Help_ReadProcessMemory(_hStarcraft, _of.UnitModelId + (int)iContentofUnitModelShifted, 2),
                    0);

            
            /* Size - 4 Byte*/
            var size =
                (float)BitConverter.ToInt32(
                    InteropCalls.Help_ReadProcessMemory(_hStarcraft, _of.UnitModelSize + (int)iContentofUnitModelShifted, 4),
                    0);

            size /= 4096;

            /* Maximum Health - 4 Byte */
            var health =
                BitConverter.ToInt32(
                    InteropCalls.Help_ReadProcessMemory(_hStarcraft, _of.UnitMaxHealth + (int)iContentofUnitModelShifted, 4),
                    0);

            

            /* Pointer to the name struct */
            var iStringStruct =
                BitConverter.ToInt32(
                    InteropCalls.Help_ReadProcessMemory(_hStarcraft, _of.UnitStringStruct + (int)iContentofUnitModelShifted, 4),
                    0);

            /* Namelenght */
            var iNameLenght =
                BitConverter.ToInt32(
                    InteropCalls.Help_ReadProcessMemory(_hStarcraft, iStringStruct, 4),
                    0);

            /* Name */
            var sName =
                Encoding.UTF8.GetString(InteropCalls.Help_ReadProcessMemory(_hStarcraft, iNameLenght + _of.RawUnitString,
                                                                            /*iNameLenght*/50));

            if (sName.Contains("\0"))
                sName = sName.Substring(0, sName.IndexOf('\0'));
               

            var str = new PredefinedTypes.UnitModelStruct();
            str.NameLenght = sName.Length;
            str.RawName = sName;
            str.Id = id;
            str.MaximumHealth = health;
            str.Size = size;

            if (sName.Contains("Unit/Name"))
                str.Name = sName.Substring(10);

            return str;
        }

        /* 4 Bytes */
        private Int32 GetGUnitMaximumHealth(Int32 iUnitNum)
        {
            var iContentofUnitModel =
                BitConverter.ToInt32(
                    InteropCalls.Help_ReadProcessMemory(_hStarcraft,
                                         _of.UnitModel + _of.UnitSize * iUnitNum, 4), 0);

            var iContentofUnitModelShifted = (iContentofUnitModel << 5) & 0xFFFFFFFF;

            var health =
                BitConverter.ToInt32(
                    InteropCalls.Help_ReadProcessMemory(_hStarcraft, _of.UnitMaxHealth + (int)iContentofUnitModelShifted, 4),
                    0);

            return health;
        }

        private Int32 GetGUnitCount()
        {
            var iU = 1;
            var icounter = 0;

            while (iU != 0)
            {
                iU =
                    BitConverter.ToInt32(
                        InteropCalls.Help_ReadProcessMemory(_hStarcraft,
                                             _of.StructUnit + _of.UnitSize * icounter, 4), 0);
                icounter++;
            }

            return icounter;
            
        }

        /* 4 Bytes */
        private Int32 GetGUnitReadUnitCount()
        {
            return
               (BitConverter.ToInt32(
                   InteropCalls.Help_ReadProcessMemory(_hStarcraft, _of.UnitTotal, sizeof(Int32)),
                   0));
        }

        #endregion

        private Int32[] GetGUnitProductionTime(Int32 iUnitNum)
        {
            /* Struct */ 
            var iUnitStruct =
                BitConverter.ToInt32(
                    InteropCalls.Help_ReadProcessMemory(_hStarcraft, _of.StructUnit + _of.UnitSize*iUnitNum, 4), 0);

            /* Content of Abilities */
            var iUnitAbilitiesPointer =
                BitConverter.ToUInt32(InteropCalls.Help_ReadProcessMemory(_hStarcraft, _of.StructUnit + 0xD8 + _of.UnitSize * iUnitNum, 4), 0);

            /* Bitwise AND */
            iUnitAbilitiesPointer = iUnitAbilitiesPointer & 0xFFFFFFFC;


            var iInfoQueue =
                BitConverter.ToInt32(InteropCalls.Help_ReadProcessMemory(_hStarcraft, (int)iUnitAbilitiesPointer + 0x24, 4),
                                     0);

            var iQueuInformation = BitConverter.ToInt32(
                InteropCalls.Help_ReadProcessMemory(_hStarcraft, iInfoQueue + 0x34, 4), 0);

            var iPtrQueInfo = BitConverter.ToInt32(
                InteropCalls.Help_ReadProcessMemory(_hStarcraft, iQueuInformation, 4), 0);


            var iMaximumTime =
                BitConverter.ToInt32(InteropCalls.Help_ReadProcessMemory(_hStarcraft, iPtrQueInfo + 0x68, 4), 0);

            var iLimeLeftTime =
                BitConverter.ToInt32(InteropCalls.Help_ReadProcessMemory(_hStarcraft, iPtrQueInfo + 0x6C, 4), 0);

            Int32[] iContent = {iMaximumTime, iLimeLeftTime};

            return iContent;
        }

        #region Functions to get the Selection- stuff

        private Int32 GetGSelectionCount()
        {
            return
                (BitConverter.ToInt16(InteropCalls.Help_ReadProcessMemory(_hStarcraft, _of.UiTotalSelectedUnits, 2), 0));
        }

        #endregion

        private Int32 GetStructureStructCount()
        {
            return BitConverter.ToInt32(InteropCalls.Help_ReadProcessMemory(_hStarcraft, _of.StructureCount, 4), 0);
        }


        #region Functions to get Gameinformation

        //Max length is 255
        private string GetGChatInput()
        {
            var i1 = BitConverter.ToInt32(InteropCalls.Help_ReadProcessMemory(_hStarcraft, _of.ChatBase, 4), 0);
            var i2 = BitConverter.ToInt32(InteropCalls.Help_ReadProcessMemory(_hStarcraft, _of.ChatOff0 + i1, 4), 0);
            var i3 = BitConverter.ToInt32(InteropCalls.Help_ReadProcessMemory(_hStarcraft, _of.ChatOff1 + i2, 4), 0);
            var i4 = BitConverter.ToInt32(InteropCalls.Help_ReadProcessMemory(_hStarcraft, _of.ChatOff2 + i3, 4), 0);
            var i5 = BitConverter.ToInt32(InteropCalls.Help_ReadProcessMemory(_hStarcraft, _of.ChatOff3 + i4, 4), 0);

            var i6 = i5 + _of.ChatOff4;    //<-- Result

            return Encoding.UTF8.GetString(InteropCalls.Help_ReadProcessMemory(_hStarcraft, i6, 255));
        }

        /* 4 Bytes */
        private Int32 GetGTimer()
        {
            Debug.WriteLine("TimerData: " + (BitConverter.ToInt32(InteropCalls.Help_ReadProcessMemory(_hStarcraft, _of.TimerData, sizeof(Int32)), 0) / 4096).ToString());
            return
                (BitConverter.ToInt32(InteropCalls.Help_ReadProcessMemory(_hStarcraft, _of.TimerData, sizeof (Int32)), 0) / 4096);
        }

        /* Gathered from Timerdata */
        private Boolean GetGIngame()
        {
            return (GetGTimer() == 0 ? false : true);
        }

        /* 4 Bytes */
        private Int32 GetGFps()
        {
            return
                (BitConverter.ToInt32(
                    InteropCalls.Help_ReadProcessMemory(_hStarcraft, _of.FramesPerSecond, sizeof (Int32)), 0));
        }

        /* 4 Bytes */
        private PredefinedTypes.Gamespeed GetGGamespeed()
        {
            var iBuffer =
                BitConverter.ToInt32(InteropCalls.Help_ReadProcessMemory(_hStarcraft, _of.Gamespeed, sizeof (Int32)), 0);

            return (PredefinedTypes.Gamespeed) iBuffer;
        }

        /* 1 Byte */
        private Boolean GetGTeamcolor()
        {
            var iBuffer = InteropCalls.Help_ReadProcessMemory(_hStarcraft, _of.TeamColor1, sizeof (Byte))[0];

            if (iBuffer == 0)
                return false;

            return true;
        }

        /* 4 Bytes - No memory read */
        private PredefinedTypes.WindowStyle GetGWindowStyle()
        {
            var iBuffer = InteropCalls.GetWindowLongPtr(_pStarcraft.MainWindowHandle, (Int32)InteropCalls.Gwl.ExStyle);

            return (PredefinedTypes.WindowStyle) iBuffer;
        }

        /* 4 Bytes */
        private Boolean GetGPause()
        {
            return (BitConverter.ToInt32(InteropCalls.Help_ReadProcessMemory(_hStarcraft, _of.PauseEnabled, 4), 0) > 0);
        }

        #endregion

        public Int32 CSleepTime { get; set; }
        public Boolean CThreadState { get; set; }
        public Process CStarcraft2 { get; set; }
        public PredefinedTypes.WindowStyle CWindowStyle { get; set; }

        public List<PredefinedTypes.Player> Player { get; set; }
        public List<PredefinedTypes.Unit> Unit { get; set; }
        public PredefinedTypes.Map Map { get; set; }
        public PredefinedTypes.Gameinformation Gameinfo { get; set; }
        public List<PredefinedTypes.Selection> Selection { get; set; } 
    }
}
