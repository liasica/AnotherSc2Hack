using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace Sc2Hack.Classes.BackEnds
{
    class HelpFunctions
    {
        public static Process GetProcess(string strProcessName)
        {
            var procs = Process.GetProcesses();

            foreach (var process in procs)
            {
                if (process.ProcessName.Equals(strProcessName))
                    return process;
            }

            return Process.GetCurrentProcess();
        }

        public static bool CheckProcess(string strProcessName)
        {
            var procs = Process.GetProcesses();

            foreach (var process in procs)
            {
                if (process.ProcessName.Equals(strProcessName))
                    return true;
            }

            return false;
        }

        public static void CheckIfDwmIsEnabled()
        {
            if (Environment.OSVersion.Version.Major >= 6 &&
                InteropCalls.DwmIsCompositionEnabled())
            {
                //Do nothing
            }

            else if (Environment.OSVersion.Version.Major >= 6 &&
                     !InteropCalls.DwmIsCompositionEnabled())
            {
                MessageBox.Show("It seems like you have DWM (Desktop Window Manager)\n" +
                                "disabled. It's highly recommended to enable the DWM.\n" +
                                "To enable the DWM, see this tools thread or visit\n" +
                                "the Microsoft support website.", "Desktop Window Manager (DWM)");
            }
        }

        public static void CheckIfWindowStyleIsFullscreen(PredefinedTypes.WindowStyle w)
        {
            if (w.Equals(PredefinedTypes.WindowStyle.Fullscreen))
                MessageBox.Show("Your windowstyle seems to be \"Fullscreen\".\n" +
                                "If you want to use this tool, change the\n" +
                                "Windowstyle to \"Windowed\" or \"Windowed Fullscreen\"\n" +
                                "to have the best experience!", "Windowstyle");
        }

        public static bool HotkeysPressed(Keys ikeya, Keys ikeyb, Keys ikeyc)
        {
            return (InteropCalls.GetAsyncKeyState(ikeya) <= -32767 &&
                    InteropCalls.GetAsyncKeyState(ikeyb) <= -32767 &&
                    InteropCalls.GetAsyncKeyState(ikeyc) <= -32767);
        }

        //public static PredefinedTypes.Preferences GetPreferences()
        //{

        //    var pSettings = new PredefinedTypes.Preferences();

        //    if (!File.Exists(Constants.StrPreferencesFile))
        //        return GetStandardPreferences();
        //    try
        //    {
        //        using (var sr = new StreamReader(Constants.StrPreferencesFile))
        //        {
        //            pSettings.UnitTabRemoveAi = Convert.ToBoolean(sr.ReadLine());
        //            pSettings.UnitTabRemoveAllie = Convert.ToBoolean(sr.ReadLine());
        //            pSettings.UnitTabRemoveReferee = Convert.ToBoolean(sr.ReadLine());
        //            pSettings.UnitTabRemoveObserver = Convert.ToBoolean(sr.ReadLine());
        //            pSettings.UnitTabRemoveNeutral = Convert.ToBoolean(sr.ReadLine());
        //            pSettings.UnitTabRemoveLocalplayer = Convert.ToBoolean(sr.ReadLine());
        //            pSettings.UnitTabSplitUnitsAndBuildings = Convert.ToBoolean(sr.ReadLine());
        //            pSettings.UnitTabPositionX = Convert.ToInt32(sr.ReadLine());
        //            pSettings.UnitTabPositionY = Convert.ToInt32(sr.ReadLine());
        //            pSettings.UnitTabWidth = Convert.ToInt32(sr.ReadLine());
        //            pSettings.UnitTabHeigth = Convert.ToInt32(sr.ReadLine());
        //            pSettings.UnitTabOpacity = Convert.ToDouble(sr.ReadLine());
        //            pSettings.MaphackRemoveAi = Convert.ToBoolean(sr.ReadLine());
        //            pSettings.MaphackRemoveAllie = Convert.ToBoolean(sr.ReadLine());
        //            pSettings.MaphackRemoveReferee = Convert.ToBoolean(sr.ReadLine());
        //            pSettings.MaphackRemoveObserver = Convert.ToBoolean(sr.ReadLine());
        //            pSettings.MaphackRemoveNeutral = Convert.ToBoolean(sr.ReadLine());
        //            pSettings.MaphackRemoveLocalplayer = Convert.ToBoolean(sr.ReadLine());
        //            pSettings.MaphackDestinationColor = ColorTranslator.FromHtml(sr.ReadLine());
        //            pSettings.MaphackPositionX = Convert.ToInt32(sr.ReadLine());
        //            pSettings.MaphackPositionY = Convert.ToInt32(sr.ReadLine());
        //            pSettings.MaphackWidth = Convert.ToInt32(sr.ReadLine());
        //            pSettings.MaphackHeigth = Convert.ToInt32(sr.ReadLine());
        //            pSettings.MaphackColorUnit1 = ColorTranslator.FromHtml(sr.ReadLine());
        //            pSettings.MaphackColorUnit2 = ColorTranslator.FromHtml(sr.ReadLine());
        //            pSettings.MaphackColorUnit3 = ColorTranslator.FromHtml(sr.ReadLine());
        //            pSettings.MaphackColorUnit4 = ColorTranslator.FromHtml(sr.ReadLine());
        //            pSettings.MaphackColorUnit5 = ColorTranslator.FromHtml(sr.ReadLine());
        //            pSettings.MaphackUnitId1 =
        //                (PredefinedTypes.UnitId) Enum.Parse(typeof (PredefinedTypes.UnitId), sr.ReadLine());
        //            pSettings.MaphackUnitId2 =
        //                (PredefinedTypes.UnitId) Enum.Parse(typeof (PredefinedTypes.UnitId), sr.ReadLine());
        //            pSettings.MaphackUnitId3 =
        //                (PredefinedTypes.UnitId) Enum.Parse(typeof (PredefinedTypes.UnitId), sr.ReadLine());
        //            pSettings.MaphackUnitId4 =
        //                (PredefinedTypes.UnitId) Enum.Parse(typeof (PredefinedTypes.UnitId), sr.ReadLine());
        //            pSettings.MaphackUnitId5 =
        //                (PredefinedTypes.UnitId) Enum.Parse(typeof (PredefinedTypes.UnitId), sr.ReadLine());
        //            pSettings.MaphackOpacity = Convert.ToDouble(sr.ReadLine());
        //            pSettings.ResourcePositionX = Convert.ToInt32(sr.ReadLine());
        //            pSettings.ResourcePositionY = Convert.ToInt32(sr.ReadLine());
        //            pSettings.ResourceWidth = Convert.ToInt32(sr.ReadLine());
        //            pSettings.ResourceHeight = Convert.ToInt32(sr.ReadLine());
        //            pSettings.ResourceRemoveAi = Convert.ToBoolean(sr.ReadLine());
        //            pSettings.ResourceRemoveNeutral = Convert.ToBoolean(sr.ReadLine());
        //            pSettings.ResourceRemoveAllie = Convert.ToBoolean(sr.ReadLine());
        //            pSettings.ResourceRemoveLocalplayer = Convert.ToBoolean(sr.ReadLine());
        //            pSettings.ResourceFontName = sr.ReadLine();
        //            pSettings.ResourceOpacity = Convert.ToDouble(sr.ReadLine());
        //            pSettings.IncomePositionX = Convert.ToInt32(sr.ReadLine());
        //            pSettings.IncomePositionY = Convert.ToInt32(sr.ReadLine());
        //            pSettings.IncomeWidth = Convert.ToInt32(sr.ReadLine());
        //            pSettings.IncomeHeight = Convert.ToInt32(sr.ReadLine());
        //            pSettings.IncomeRemoveAi = Convert.ToBoolean(sr.ReadLine());
        //            pSettings.IncomeRemoveNeutral = Convert.ToBoolean(sr.ReadLine());
        //            pSettings.IncomeRemoveAllie = Convert.ToBoolean(sr.ReadLine());
        //            pSettings.IncomeRemoveLocalplayer = Convert.ToBoolean(sr.ReadLine());
        //            pSettings.IncomeFontName = sr.ReadLine();
        //            pSettings.IncomeOpacity = Convert.ToDouble(sr.ReadLine());
        //            pSettings.ArmyPositionX = Convert.ToInt32(sr.ReadLine());
        //            pSettings.ArmyPositionY = Convert.ToInt32(sr.ReadLine());
        //            pSettings.ArmyWidth = Convert.ToInt32(sr.ReadLine());
        //            pSettings.ArmyHeight = Convert.ToInt32(sr.ReadLine());
        //            pSettings.ArmyRemoveAi = Convert.ToBoolean(sr.ReadLine());
        //            pSettings.ArmyRemoveNeutral = Convert.ToBoolean(sr.ReadLine());
        //            pSettings.ArmyRemoveAllie = Convert.ToBoolean(sr.ReadLine());
        //            pSettings.ArmyRemoveLocalplayer = Convert.ToBoolean(sr.ReadLine());
        //            pSettings.ArmyFontName = sr.ReadLine();
        //            pSettings.ArmyOpacity = Convert.ToDouble(sr.ReadLine());
        //            pSettings.ApmPositionX = Convert.ToInt32(sr.ReadLine());
        //            pSettings.ApmPositionY = Convert.ToInt32(sr.ReadLine());
        //            pSettings.ApmWidth = Convert.ToInt32(sr.ReadLine());
        //            pSettings.ApmHeight = Convert.ToInt32(sr.ReadLine());
        //            pSettings.ApmRemoveAi = Convert.ToBoolean(sr.ReadLine());
        //            pSettings.ApmRemoveNeutral = Convert.ToBoolean(sr.ReadLine());
        //            pSettings.ApmRemoveAllie = Convert.ToBoolean(sr.ReadLine());
        //            pSettings.ApmRemoveLocalplayer = Convert.ToBoolean(sr.ReadLine());
        //            pSettings.ApmFontName = sr.ReadLine();
        //            pSettings.ApmOpacity = Convert.ToDouble(sr.ReadLine());
        //            pSettings.WorkerPositionX = Convert.ToInt32(sr.ReadLine());
        //            pSettings.WorkerPositionY = Convert.ToInt32(sr.ReadLine());
        //            pSettings.WorkerWidth = Convert.ToInt32(sr.ReadLine());
        //            pSettings.WorkerHeight = Convert.ToInt32(sr.ReadLine());
        //            pSettings.WorkerFontName = sr.ReadLine();
        //            pSettings.WorkerOpacity = Convert.ToDouble(sr.ReadLine());
        //            pSettings.GlobalDataRefresh = Convert.ToInt32(sr.ReadLine());
        //            pSettings.GlobalDrawingRefresh = Convert.ToInt32(sr.ReadLine());
        //            pSettings.GlobalDrawOnlyInForeground = Convert.ToBoolean(sr.ReadLine());
        //            pSettings.ResourceTogglePanel = sr.ReadLine();
        //            pSettings.ResourceChangePositionPanel = sr.ReadLine();
        //            pSettings.ResourceChangeSizePanel = sr.ReadLine();
        //            pSettings.ResourceHotkey1 = (Keys)Enum.Parse(typeof(Keys), sr.ReadLine());
        //            pSettings.ResourceHotkey2 = (Keys)Enum.Parse(typeof(Keys), sr.ReadLine());
        //            pSettings.ResourceHotkey3 = (Keys)Enum.Parse(typeof(Keys), sr.ReadLine());
        //            pSettings.IncomeTogglePanel = sr.ReadLine();
        //            pSettings.IncomeChangePositionPanel = sr.ReadLine();
        //            pSettings.IncomeChangeSizePanel = sr.ReadLine();
        //            pSettings.IncomeHotkey1 = (Keys)Enum.Parse(typeof(Keys), sr.ReadLine());
        //            pSettings.IncomeHotkey2 = (Keys)Enum.Parse(typeof(Keys), sr.ReadLine());
        //            pSettings.IncomeHotkey3 = (Keys)Enum.Parse(typeof(Keys), sr.ReadLine());
        //            pSettings.WorkerTogglePanel = sr.ReadLine();
        //            pSettings.WorkerChangePositionPanel = sr.ReadLine();
        //            pSettings.WorkerChangeSizePanel = sr.ReadLine();
        //            pSettings.WorkerHotkey1 = (Keys)Enum.Parse(typeof(Keys), sr.ReadLine());
        //            pSettings.WorkerHotkey2 = (Keys)Enum.Parse(typeof(Keys), sr.ReadLine());
        //            pSettings.WorkerHotkey3 = (Keys)Enum.Parse(typeof(Keys), sr.ReadLine());
        //            pSettings.MaphackTogglePanel = sr.ReadLine();
        //            pSettings.MaphackChangePositionPanel = sr.ReadLine();
        //            pSettings.MaphackChangeSizePanel = sr.ReadLine();
        //            pSettings.MaphackHotkey1 = (Keys)Enum.Parse(typeof(Keys), sr.ReadLine());
        //            pSettings.MaphackHotkey2 = (Keys)Enum.Parse(typeof(Keys), sr.ReadLine());
        //            pSettings.MaphackHotkey3 = (Keys)Enum.Parse(typeof(Keys), sr.ReadLine());
        //            pSettings.ApmTogglePanel = sr.ReadLine();
        //            pSettings.ApmChangePositionPanel = sr.ReadLine();
        //            pSettings.ApmChangeSizePanel = sr.ReadLine();
        //            pSettings.ApmHotkey1 = (Keys)Enum.Parse(typeof(Keys), sr.ReadLine());
        //            pSettings.ApmHotkey2 = (Keys)Enum.Parse(typeof(Keys), sr.ReadLine());
        //            pSettings.ApmHotkey3 = (Keys)Enum.Parse(typeof(Keys), sr.ReadLine());
        //            pSettings.ArmyTogglePanel = sr.ReadLine();
        //            pSettings.ArmyChangePositionPanel = sr.ReadLine();
        //            pSettings.ArmyChangeSizePanel = sr.ReadLine();
        //            pSettings.ArmyHotkey1 = (Keys)Enum.Parse(typeof(Keys), sr.ReadLine());
        //            pSettings.ArmyHotkey2 = (Keys)Enum.Parse(typeof(Keys), sr.ReadLine());
        //            pSettings.ArmyHotkey3 = (Keys)Enum.Parse(typeof(Keys), sr.ReadLine());
        //            pSettings.UnitTogglePanel = sr.ReadLine();
        //            pSettings.UnitChangePositionPanel = sr.ReadLine();
        //            pSettings.UnitChangeSizePanel = sr.ReadLine();
        //            pSettings.UnitHotkey1 = (Keys)Enum.Parse(typeof(Keys), sr.ReadLine());
        //            pSettings.UnitHotkey2 = (Keys)Enum.Parse(typeof(Keys), sr.ReadLine());
        //            pSettings.UnitHotkey3 = (Keys)Enum.Parse(typeof(Keys), sr.ReadLine());
        //            pSettings.GlobalChangeSizeAndPosition = (Keys)Enum.Parse(typeof(Keys), sr.ReadLine());
        //            pSettings.MaphackDisableDestinationLine = Convert.ToBoolean(sr.ReadLine());
        //            pSettings.GlobalOnlyDrawWhenUnpaused = Convert.ToBoolean(sr.ReadLine());
        //            pSettings.ResourceDrawBackground = Convert.ToBoolean(sr.ReadLine());
        //            pSettings.IncomeDrawBackground = Convert.ToBoolean(sr.ReadLine());
        //            pSettings.WorkerDrawBackground = Convert.ToBoolean(sr.ReadLine());
        //            pSettings.ApmDrawBackground = Convert.ToBoolean(sr.ReadLine());
        //            pSettings.ArmyDrawBackground = Convert.ToBoolean(sr.ReadLine());
        //            pSettings.MaphackColorDefensivestructuresYellow = Convert.ToBoolean(sr.ReadLine());
        //            pSettings.StealUnits = Convert.ToBoolean(sr.ReadLine());
        //            pSettings.StealUnitsInstant = Convert.ToBoolean(sr.ReadLine());
        //            pSettings.StealUnitsHotkey = Convert.ToBoolean(sr.ReadLine());
        //            pSettings.StealUnitsHotkey1 = (Keys)Enum.Parse(typeof(Keys), sr.ReadLine());
        //            pSettings.StealUnitsHotkey2 = (Keys)Enum.Parse(typeof(Keys), sr.ReadLine());
        //            pSettings.StealUnitsHotkey3 = (Keys)Enum.Parse(typeof(Keys), sr.ReadLine());


        //        }
        //    }

        //    catch
        //    {
        //        MessageBox.Show("Settingsfile corrupted, reset all settings!\n\n" + 
        //            "Backup of old file was created!");

        //        /* Make backup */
        //        var iCount = 0;

        //        TryAgain:
        //        if (File.Exists(Constants.StrPreferencesFile + "_BACKUP" + iCount.ToString()))
        //        {
        //            iCount++;
        //            goto TryAgain;
        //        }

        //        File.Copy(Constants.StrPreferencesFile, Constants.StrPreferencesFile + "_BACKUP" + iCount.ToString());

        //        return GetStandardPreferences();
        //    }



        //    return pSettings;
        //}

        //public static void SetPreferences(PredefinedTypes.Preferences pSettings)
        //{
        //    using (var sw = new StreamWriter(Constants.StrPreferencesFile))
        //    {
        //        sw.WriteLine(pSettings.UnitTabRemoveAi.ToString());
        //        sw.WriteLine(pSettings.UnitTabRemoveAllie.ToString());
        //        sw.WriteLine(pSettings.UnitTabRemoveReferee.ToString());
        //        sw.WriteLine(pSettings.UnitTabRemoveObserver.ToString());
        //        sw.WriteLine(pSettings.UnitTabRemoveNeutral.ToString());
        //        sw.WriteLine(pSettings.UnitTabRemoveLocalplayer.ToString());
        //        sw.WriteLine(pSettings.UnitTabSplitUnitsAndBuildings.ToString());
        //        sw.WriteLine(pSettings.UnitTabPositionX.ToString(CultureInfo.InvariantCulture));
        //        sw.WriteLine(pSettings.UnitTabPositionY.ToString(CultureInfo.InvariantCulture));
        //        sw.WriteLine(pSettings.UnitTabWidth.ToString(CultureInfo.InvariantCulture));
        //        sw.WriteLine(pSettings.UnitTabHeigth.ToString(CultureInfo.InvariantCulture));
        //        sw.WriteLine(pSettings.UnitTabOpacity.ToString(CultureInfo.InvariantCulture));
        //        sw.WriteLine(pSettings.MaphackRemoveAi.ToString());
        //        sw.WriteLine(pSettings.MaphackRemoveAllie.ToString());
        //        sw.WriteLine(pSettings.MaphackRemoveReferee.ToString());
        //        sw.WriteLine(pSettings.MaphackRemoveObserver.ToString());
        //        sw.WriteLine(pSettings.MaphackRemoveNeutral.ToString());
        //        sw.WriteLine(pSettings.MaphackRemoveLocalplayer.ToString());
        //        sw.WriteLine(ColorTranslator.ToHtml(pSettings.MaphackDestinationColor));
        //        sw.WriteLine(pSettings.MaphackPositionX.ToString(CultureInfo.InvariantCulture));
        //        sw.WriteLine(pSettings.MaphackPositionY.ToString(CultureInfo.InvariantCulture));
        //        sw.WriteLine(pSettings.MaphackWidth.ToString(CultureInfo.InvariantCulture));
        //        sw.WriteLine(pSettings.MaphackHeigth.ToString(CultureInfo.InvariantCulture));
        //        sw.WriteLine(ColorTranslator.ToHtml(pSettings.MaphackColorUnit1));
        //        sw.WriteLine(ColorTranslator.ToHtml(pSettings.MaphackColorUnit2));
        //        sw.WriteLine(ColorTranslator.ToHtml(pSettings.MaphackColorUnit3));
        //        sw.WriteLine(ColorTranslator.ToHtml(pSettings.MaphackColorUnit4));
        //        sw.WriteLine(ColorTranslator.ToHtml(pSettings.MaphackColorUnit5));
        //        sw.WriteLine(pSettings.MaphackUnitId1);
        //        sw.WriteLine(pSettings.MaphackUnitId2);
        //        sw.WriteLine(pSettings.MaphackUnitId3);
        //        sw.WriteLine(pSettings.MaphackUnitId4);
        //        sw.WriteLine(pSettings.MaphackUnitId5);
        //        sw.WriteLine(pSettings.MaphackOpacity.ToString());
        //        sw.WriteLine(pSettings.ResourcePositionX);
        //        sw.WriteLine(pSettings.ResourcePositionY);
        //        sw.WriteLine(pSettings.ResourceWidth);
        //        sw.WriteLine(pSettings.ResourceHeight);
        //        sw.WriteLine(pSettings.ResourceRemoveAi);
        //        sw.WriteLine(pSettings.ResourceRemoveNeutral);
        //        sw.WriteLine(pSettings.ResourceRemoveAllie);
        //        sw.WriteLine(pSettings.ResourceRemoveLocalplayer);
        //        sw.WriteLine(pSettings.ResourceFontName);
        //        sw.WriteLine(pSettings.ResourceOpacity);
        //        sw.WriteLine(pSettings.IncomePositionX);
        //        sw.WriteLine(pSettings.IncomePositionY);
        //        sw.WriteLine(pSettings.IncomeWidth);
        //        sw.WriteLine(pSettings.IncomeHeight);
        //        sw.WriteLine(pSettings.IncomeRemoveAi);
        //        sw.WriteLine(pSettings.IncomeRemoveNeutral);
        //        sw.WriteLine(pSettings.IncomeRemoveAllie);
        //        sw.WriteLine(pSettings.IncomeRemoveLocalplayer);
        //        sw.WriteLine(pSettings.IncomeFontName);
        //        sw.WriteLine(pSettings.IncomeOpacity);
        //        sw.WriteLine(pSettings.ArmyPositionX);
        //        sw.WriteLine(pSettings.ArmyPositionY);
        //        sw.WriteLine(pSettings.ArmyWidth);
        //        sw.WriteLine(pSettings.ArmyHeight);
        //        sw.WriteLine(pSettings.ArmyRemoveAi);
        //        sw.WriteLine(pSettings.ArmyRemoveNeutral);
        //        sw.WriteLine(pSettings.ArmyRemoveAllie);
        //        sw.WriteLine(pSettings.ArmyRemoveLocalplayer);
        //        sw.WriteLine(pSettings.ArmyFontName);
        //        sw.WriteLine(pSettings.ArmyOpacity);
        //        sw.WriteLine(pSettings.ApmPositionX);
        //        sw.WriteLine(pSettings.ApmPositionY);
        //        sw.WriteLine(pSettings.ApmWidth);
        //        sw.WriteLine(pSettings.ApmHeight);
        //        sw.WriteLine(pSettings.ApmRemoveAi);
        //        sw.WriteLine(pSettings.ApmRemoveNeutral);
        //        sw.WriteLine(pSettings.ApmRemoveAllie);
        //        sw.WriteLine(pSettings.ApmRemoveLocalplayer);
        //        sw.WriteLine(pSettings.ApmFontName);
        //        sw.WriteLine(pSettings.ApmOpacity);
        //        sw.WriteLine(pSettings.WorkerPositionX);
        //        sw.WriteLine(pSettings.WorkerPositionY);
        //        sw.WriteLine(pSettings.WorkerWidth);
        //        sw.WriteLine(pSettings.WorkerHeight);
        //        sw.WriteLine(pSettings.WorkerFontName);
        //        sw.WriteLine(pSettings.WorkerOpacity);
        //        sw.WriteLine(pSettings.GlobalDataRefresh);
        //        sw.WriteLine(pSettings.GlobalDrawingRefresh);
        //        sw.WriteLine(pSettings.GlobalDrawOnlyInForeground);
        //        sw.WriteLine(pSettings.ResourceTogglePanel);
        //        sw.WriteLine(pSettings.ResourceChangePositionPanel);
        //        sw.WriteLine(pSettings.ResourceChangeSizePanel);
        //        sw.WriteLine(pSettings.ResourceHotkey1);
        //        sw.WriteLine(pSettings.ResourceHotkey2);
        //        sw.WriteLine(pSettings.ResourceHotkey3);
        //        sw.WriteLine(pSettings.IncomeTogglePanel);
        //        sw.WriteLine(pSettings.IncomeChangePositionPanel);
        //        sw.WriteLine(pSettings.IncomeChangeSizePanel);
        //        sw.WriteLine(pSettings.IncomeHotkey1);
        //        sw.WriteLine(pSettings.IncomeHotkey2);
        //        sw.WriteLine(pSettings.IncomeHotkey3);
        //        sw.WriteLine(pSettings.WorkerTogglePanel);
        //        sw.WriteLine(pSettings.WorkerChangePositionPanel);
        //        sw.WriteLine(pSettings.WorkerChangeSizePanel);
        //        sw.WriteLine(pSettings.WorkerHotkey1);
        //        sw.WriteLine(pSettings.WorkerHotkey2);
        //        sw.WriteLine(pSettings.WorkerHotkey3);
        //        sw.WriteLine(pSettings.MaphackTogglePanel);
        //        sw.WriteLine(pSettings.MaphackChangePositionPanel);
        //        sw.WriteLine(pSettings.MaphackChangeSizePanel);
        //        sw.WriteLine(pSettings.MaphackHotkey1);
        //        sw.WriteLine(pSettings.MaphackHotkey2);
        //        sw.WriteLine(pSettings.MaphackHotkey3);
        //        sw.WriteLine(pSettings.ApmTogglePanel);
        //        sw.WriteLine(pSettings.ApmChangePositionPanel);
        //        sw.WriteLine(pSettings.ApmChangeSizePanel);
        //        sw.WriteLine(pSettings.ApmHotkey1);
        //        sw.WriteLine(pSettings.ApmHotkey2);
        //        sw.WriteLine(pSettings.ApmHotkey3);
        //        sw.WriteLine(pSettings.ArmyTogglePanel);
        //        sw.WriteLine(pSettings.ArmyChangePositionPanel);
        //        sw.WriteLine(pSettings.ArmyChangeSizePanel);
        //        sw.WriteLine(pSettings.ArmyHotkey1);
        //        sw.WriteLine(pSettings.ArmyHotkey2);
        //        sw.WriteLine(pSettings.ArmyHotkey3);
        //        sw.WriteLine(pSettings.UnitTogglePanel);
        //        sw.WriteLine(pSettings.UnitChangePositionPanel);
        //        sw.WriteLine(pSettings.UnitChangeSizePanel);
        //        sw.WriteLine(pSettings.UnitHotkey1);
        //        sw.WriteLine(pSettings.UnitHotkey2);
        //        sw.WriteLine(pSettings.UnitHotkey3);
        //        sw.WriteLine(pSettings.GlobalChangeSizeAndPosition);
        //        sw.WriteLine(pSettings.MaphackDisableDestinationLine);
        //        sw.WriteLine(pSettings.GlobalOnlyDrawWhenUnpaused);
        //        sw.WriteLine(pSettings.ResourceDrawBackground);
        //        sw.WriteLine(pSettings.IncomeDrawBackground);
        //        sw.WriteLine(pSettings.WorkerDrawBackground);
        //        sw.WriteLine(pSettings.ApmDrawBackground);
        //        sw.WriteLine(pSettings.ArmyDrawBackground);
        //        sw.WriteLine(pSettings.MaphackColorDefensivestructuresYellow);
        //        sw.WriteLine(pSettings.StealUnits);
        //        sw.WriteLine(pSettings.StealUnitsInstant);
        //        sw.WriteLine(pSettings.StealUnitsHotkey);
        //        sw.WriteLine(pSettings.StealUnitsHotkey1);
        //        sw.WriteLine(pSettings.StealUnitsHotkey2);
        //        sw.WriteLine(pSettings.StealUnitsHotkey3);
        //    }
        //}

        public static PredefinedTypes.Preferences GetStandardPreferences()
        {
            var pSettings = new PredefinedTypes.Preferences
                {
                    UnitTabRemoveAi = false,
                    UnitTabRemoveAllie = false,
                    UnitTabRemoveReferee = true,
                    UnitTabRemoveObserver = true,
                    UnitTabRemoveNeutral = true,
                    UnitTabRemoveLocalplayer = false,
                    UnitTabSplitUnitsAndBuildings = true,
                    UnitTabPositionX = 50,
                    UnitTabPositionY = 50,
                    UnitTabWidth = 300,
                    UnitTabHeigth = 50,
                    UnitTabOpacity = 1,
                    MaphackRemoveAi = false,
                    MaphackRemoveAllie = false,
                    MaphackRemoveReferee = true,
                    MaphackRemoveObserver = true,
                    MaphackRemoveNeutral = true,
                    MaphackRemoveLocalplayer = false,
                    MaphackDestinationColor = Color.Orange,
                    MaphackPositionX = 28,
                    MaphackPositionY = 811,
                    MaphackWidth = 261,
                    MaphackHeigth = 255,
                    MaphackColorUnit1 = Color.Transparent,
                    MaphackColorUnit2 = Color.Transparent,
                    MaphackColorUnit3 = Color.Transparent,
                    MaphackColorUnit4 = Color.Transparent,
                    MaphackColorUnit5 = Color.Transparent,
                    MaphackUnitId1 = PredefinedTypes.UnitId.TuScv,
                    MaphackUnitId2 = PredefinedTypes.UnitId.TuScv,
                    MaphackUnitId3 = PredefinedTypes.UnitId.TuScv,
                    MaphackUnitId4 = PredefinedTypes.UnitId.TuScv,
                    MaphackUnitId5 = PredefinedTypes.UnitId.TuScv,
                    MaphackOpacity = 1,
                    ResourcePositionX = 500,
                    ResourcePositionY = 50,
                    ResourceWidth = 600,
                    ResourceHeight = 50,
                    ResourceRemoveAi = false,
                    ResourceRemoveAllie = false,
                    ResourceRemoveLocalplayer = false,
                    ResourceRemoveNeutral = true,
                    ResourceFontName = "Century Gothic",
                    ResourceOpacity = 0.75,
                    IncomePositionX = 1500,
                    IncomePositionY = 300,
                    IncomeWidth = 600,
                    IncomeHeight = 50,
                    IncomeRemoveAi = false,
                    IncomeRemoveAllie = false,
                    IncomeRemoveLocalplayer = false,
                    IncomeRemoveNeutral = true,
                    IncomeFontName = "Century Gothic",
                    IncomeOpacity = 0.75,
                    ArmyPositionX = 500,
                    ArmyPositionY = 300,
                    ArmyWidth = 600,
                    ArmyHeight = 50,
                    ArmyRemoveAi = false,
                    ArmyRemoveAllie = false,
                    ArmyRemoveLocalplayer = false,
                    ArmyRemoveNeutral = true,
                    ArmyFontName = "Century Gothic",
                    ArmyOpacity = 0.75,
                    ApmPositionX = 500,
                    ApmPositionY = 300,
                    ApmWidth = 600,
                    ApmHeight = 50,
                    ApmRemoveAi = false,
                    ApmRemoveAllie = false,
                    ApmRemoveLocalplayer = false,
                    ApmRemoveNeutral = true,
                    ApmFontName = "Century Gothic",
                    ApmOpacity = 0.75,
                    WorkerPositionX = 500,
                    WorkerPositionY = 300,
                    WorkerWidth = 200,
                    WorkerHeight = 50,
                    WorkerFontName = "Century Gothic",
                    WorkerOpacity = 0.75,
                    GlobalDataRefresh = 33,
                    GlobalDrawingRefresh = 33,
                    GlobalDrawOnlyInForeground = false,
                    ResourceTogglePanel = "/res",
                    ResourceChangePositionPanel = "/rcp",
                    ResourceChangeSizePanel = "/rcs",
                    ResourceHotkey1 = Keys.ControlKey,
                    ResourceHotkey2 = Keys.Menu,
                    ResourceHotkey3 = Keys.NumPad1,
                    IncomeTogglePanel = "/inc",
                    IncomeChangePositionPanel = "/icp",
                    IncomeChangeSizePanel = "/ics",
                    IncomeHotkey1 = Keys.ControlKey,
                    IncomeHotkey2 = Keys.Menu,
                    IncomeHotkey3 = Keys.NumPad2,
                    WorkerTogglePanel = "/wor",
                    WorkerChangePositionPanel = "/wcp",
                    WorkerChangeSizePanel = "/wcs",
                    WorkerHotkey1 = Keys.ControlKey,
                    WorkerHotkey2 = Keys.Menu,
                    WorkerHotkey3 = Keys.NumPad3,
                    MaphackTogglePanel = "/map",
                    MaphackChangePositionPanel = "/mcp",
                    MaphackChangeSizePanel = "/mcs",
                    MaphackHotkey1 = Keys.ControlKey,
                    MaphackHotkey2 = Keys.Menu,
                    MaphackHotkey3 = Keys.NumPad5,
                    ApmTogglePanel = "/apm",
                    ApmChangePositionPanel = "/acp",
                    ApmChangeSizePanel = "/acs",
                    ApmHotkey1 = Keys.ControlKey,
                    ApmHotkey2 = Keys.Menu,
                    ApmHotkey3 = Keys.NumPad7,
                    ArmyTogglePanel = "/arm",
                    ArmyChangePositionPanel = "/amcp",
                    ArmyChangeSizePanel = "/amcs",
                    ArmyHotkey1 = Keys.ControlKey,
                    ArmyHotkey2 = Keys.Menu,
                    ArmyHotkey3 = Keys.NumPad8,
                    UnitTogglePanel = "/uni",
                    UnitChangePositionPanel = "/ucp",
                    UnitChangeSizePanel = "/ucs",
                    UnitHotkey1 = Keys.ControlKey,
                    UnitHotkey2 = Keys.Menu,
                    UnitHotkey3 = Keys.NumPad9,
                    GlobalChangeSizeAndPosition = Keys.NumPad0,
                    MaphackDisableDestinationLine = false,
                    GlobalOnlyDrawWhenUnpaused = true,
                    ResourceDrawBackground = true,
                    IncomeDrawBackground = true,
                    WorkerDrawBackground = true,
                    ApmDrawBackground = true,
                    ArmyDrawBackground = true,
                    MaphackColorDefensivestructuresYellow = true,
                    StealUnits = true,
                    StealUnitsInstant = false,
                    StealUnitsHotkey = true,
                    StealUnitsHotkey1 = Keys.ControlKey,
                    StealUnitsHotkey2 = Keys.Menu,
                    StealUnitsHotkey3 = Keys.Add
                };

            return pSettings;
        }

        public static Int32 GetValidPlayerCount(List<PredefinedTypes.Player> lPlayer)
        {
            var iValidSize = 0;
            for (var i = 0; i < lPlayer.Count; i++)
            {
                if (!lPlayer[i].Name.StartsWith("\0"))
                    iValidSize += 1;
            }

            return iValidSize;
        }

        public static PredefinedTypes.Preferences LoadPreferences()
        {
            #region Introduction


            var pSettings = new PredefinedTypes.Preferences();
            pSettings.MaphackUnitColors = new List<Color>();
            pSettings.MaphackUnitIds = new List<PredefinedTypes.UnitId>();

            if (!File.Exists(Constants.StrDummyPref))
                return GetStandardPreferences();

            var sr = new StreamReader(Constants.StrDummyPref);
            var strSource = sr.ReadToEnd();
            sr.Close();

            var strSplit = strSource.Split('\n');

            /* Defined variables 
             * '['  is the beginning of a keyword e.g. [Resource] 
             * ';'  is the indicator of a comment 
             * */

            /* remove redundant content */
            for (var i = 0; i < strSplit.Length; i++)
            {
                for (var j = 0; j < Constants.StrRedundantSigns.Length; j++)
                {
                    if (strSplit[i].Contains(Constants.StrRedundantSigns[j]))
                        strSplit[i] = strSplit[i].Remove(strSplit[i].IndexOf(Constants.StrRedundantSigns[j]),
                                                         Constants.StrRedundantSigns[j].Length);
                }
            }

            #endregion

            #region Important part 

            var strKeyword = string.Empty;
            for (var i = 0; i < strSplit.Length; i++)
            {
                var strInnerValue = strSplit[i];


                if (strInnerValue.StartsWith(";"))
                    continue;

                if (strInnerValue.StartsWith("["))
                {
                    strKeyword = strInnerValue.Substring(1, strInnerValue.Length - 2);
                    continue;
                }

                if (strInnerValue.Length <= 0)
                    continue;


                #region Global

                if (strKeyword.Equals(Constants.StrPreferenceKeywordGlobal))
                {
                    /* Draw Only when SC2 is foreground */
                    if (strInnerValue.StartsWith("DrawOnlyInForeground"))
                    {
                        strInnerValue = strInnerValue.Substring("DrawOnlyInForeground".Length,
                                                                strInnerValue.Length -
                                                                "DrawOnlyInForeground".Length);

                        bool bDrawOnlyInForeground;
                        Boolean.TryParse(strInnerValue, out bDrawOnlyInForeground);

                        pSettings.GlobalDrawOnlyInForeground = bDrawOnlyInForeground;
                    }

                    /* Data Refresh */
                    if (strInnerValue.StartsWith("Data Refresh"))
                    {
                        strInnerValue = strInnerValue.Substring("Data Refresh".Length,
                                                                strInnerValue.Length -
                                                                "Data Refresh".Length);

                        Int32 iDataRefresh;
                        Int32.TryParse(strInnerValue, out iDataRefresh);

                        pSettings.GlobalDataRefresh = iDataRefresh;
                    }

                    /* Drawing Refresh*/
                    if (strInnerValue.StartsWith("Drawing Refresh"))
                    {
                        strInnerValue = strInnerValue.Substring("Drawing Refresh".Length,
                                                                strInnerValue.Length -
                                                                "Drawing Refresh".Length);

                        Int32 iDrawingRefresh;
                        Int32.TryParse(strInnerValue, out iDrawingRefresh);

                        pSettings.GlobalDrawingRefresh = iDrawingRefresh;
                    }

                    /* Resize Hotkey */
                    if (strInnerValue.StartsWith("Hotkey Resize"))
                    {
                        strInnerValue = strInnerValue.Substring("Hotkey Resize".Length,
                                                                strInnerValue.Length -
                                                                "Hotkey Resize".Length);

                        Keys kResize;
                        Enum.TryParse(strInnerValue, out kResize);

                        pSettings.GlobalChangeSizeAndPosition = kResize;
                    }
                }

                #endregion

                #region Resource

                else if (strKeyword.Equals(Constants.StrPreferenceKeywordResource))
                {
                    #region Boolean values

                    /* Remove Ai */
                    if (strInnerValue.StartsWith(Constants.StrPreferenceRemoveAi))
                    {
                        strInnerValue = strInnerValue.Substring(Constants.StrPreferenceRemoveAi.Length,
                                                                strInnerValue.Length -
                                                                Constants.StrPreferenceRemoveAi.Length);

                        bool bRemoveAi;
                        Boolean.TryParse(strInnerValue, out bRemoveAi);

                        pSettings.ResourceRemoveAi = bRemoveAi;
                    }

                    /* Remove Localplayer */
                    if (strInnerValue.StartsWith(Constants.StrPreferenceRemoveLocalplayer))
                    {
                        strInnerValue = strInnerValue.Substring(Constants.StrPreferenceRemoveLocalplayer.Length,
                                                                strInnerValue.Length -
                                                                Constants.StrPreferenceRemoveLocalplayer.Length);

                        bool bRemoveLocalplayer;
                        Boolean.TryParse(strInnerValue, out bRemoveLocalplayer);

                        pSettings.ResourceRemoveLocalplayer = bRemoveLocalplayer;
                    }

                    /* Remove Neutral */
                    if (strInnerValue.StartsWith(Constants.StrPreferenceRemoveNeutral))
                    {
                        strInnerValue = strInnerValue.Substring(Constants.StrPreferenceRemoveNeutral.Length,
                                                                strInnerValue.Length -
                                                                Constants.StrPreferenceRemoveNeutral.Length);

                        bool bRemoveNeutral;
                        Boolean.TryParse(strInnerValue, out bRemoveNeutral);

                        pSettings.ResourceRemoveNeutral = bRemoveNeutral;
                    }

                    /* Remove Allie */
                    if (strInnerValue.StartsWith(Constants.StrPreferenceRemoveAllie))
                    {
                        strInnerValue = strInnerValue.Substring(Constants.StrPreferenceRemoveAllie.Length,
                                                                strInnerValue.Length -
                                                                Constants.StrPreferenceRemoveAllie.Length);

                        bool bRemoveAllie;
                        Boolean.TryParse(strInnerValue, out bRemoveAllie);

                        pSettings.ResourceRemoveAllie = bRemoveAllie;
                    }

                    /* Disable Background */
                    if (strInnerValue.StartsWith(Constants.StrPreferenceDisableBackground))
                    {
                        strInnerValue = strInnerValue.Substring(Constants.StrPreferenceDisableBackground.Length,
                                                                strInnerValue.Length -
                                                                Constants.StrPreferenceDisableBackground.Length);

                        bool bDisableBackground;
                        Boolean.TryParse(strInnerValue, out bDisableBackground);

                        pSettings.ResourceDrawBackground = bDisableBackground;
                    }

                    #endregion

                    #region Int32 values

                    /* Pos X */
                    if (strInnerValue.StartsWith(Constants.StrPreferencePosX))
                    {
                        strInnerValue = strInnerValue.Substring(Constants.StrPreferencePosX.Length,
                                                                strInnerValue.Length -
                                                                Constants.StrPreferencePosX.Length);

                        if (strInnerValue.Length > 0)
                        {
                            Int32 iPosX;
                            Int32.TryParse(strInnerValue, out iPosX);

                            pSettings.ResourcePositionX = iPosX;
                        }

                        else
                            pSettings.ResourcePositionX = 50;
                    }

                    /* Pos Y */
                    if (strInnerValue.StartsWith(Constants.StrPreferencePosY))
                    {
                        strInnerValue = strInnerValue.Substring(Constants.StrPreferencePosY.Length,
                                                                strInnerValue.Length -
                                                                Constants.StrPreferencePosY.Length);

                        if (strInnerValue.Length > 0)
                        {
                            Int32 iPosY;
                            Int32.TryParse(strInnerValue, out iPosY);

                            pSettings.ResourcePositionY = iPosY;
                        }

                        else
                            pSettings.ResourcePositionY = 50;
                    }

                    /* Width */
                    if (strInnerValue.StartsWith(Constants.StrPreferenceWidth))
                    {
                        strInnerValue = strInnerValue.Substring(Constants.StrPreferenceWidth.Length,
                                                                strInnerValue.Length -
                                                                Constants.StrPreferenceWidth.Length);

                        
                            Int32 iWidth;
                            Int32.TryParse(strInnerValue, out iWidth);

                        if (iWidth <= 10)
                            pSettings.ResourceWidth = 600;
                        
                        else 
                            pSettings.ResourceWidth = iWidth;
                       
                    }

                    /* Height */
                    if (strInnerValue.StartsWith(Constants.StrPreferenceHeight))
                    {
                        strInnerValue = strInnerValue.Substring(Constants.StrPreferenceHeight.Length,
                                                                strInnerValue.Length -
                                                                Constants.StrPreferenceHeight.Length);


                        Int32 iHeight;
                        Int32.TryParse(strInnerValue, out iHeight);

                        if (iHeight <= 10)
                            pSettings.ResourceHeight = 50;

                        else 
                            pSettings.ResourceHeight = iHeight;
                    }


                    #endregion

                    #region String

                    /* Font Name */
                    if (strInnerValue.StartsWith(Constants.StrPreferenceFontText))
                    {
                        strInnerValue = strInnerValue.Substring(Constants.StrPreferenceFontText.Length,
                                                                strInnerValue.Length -
                                                                Constants.StrPreferenceFontText.Length);


                        pSettings.ResourceFontName = strInnerValue.Length <= 0 ? "Century Gothic" : strInnerValue;
                    }

                    /* On/ Off Panel */
                    if (strInnerValue.StartsWith(Constants.StrPreferenceOnOffText))
                    {
                        strInnerValue = strInnerValue.Substring(Constants.StrPreferenceOnOffText.Length,
                                                                strInnerValue.Length -
                                                                Constants.StrPreferenceOnOffText.Length);


                        pSettings.ResourceTogglePanel = strInnerValue.Length <= 0 ? "/res" : strInnerValue;
                    }

                    /* Change Position */
                    if (strInnerValue.StartsWith(Constants.StrPreferenceChangePositionText))
                    {
                        strInnerValue = strInnerValue.Substring(Constants.StrPreferenceChangePositionText.Length,
                                                                strInnerValue.Length -
                                                                Constants.StrPreferenceChangePositionText.Length);


                        pSettings.ResourceChangePositionPanel = strInnerValue.Length <= 0 ? "/rcp" : strInnerValue;
                    }

                    /* Change Size */
                    if (strInnerValue.StartsWith(Constants.StrPreferenceChangeSizeText))
                    {
                        strInnerValue = strInnerValue.Substring(Constants.StrPreferenceChangeSizeText.Length,
                                                                strInnerValue.Length -
                                                                Constants.StrPreferenceChangeSizeText.Length);


                        pSettings.ResourceChangeSizePanel = strInnerValue.Length <= 0 ? "/rcs" : strInnerValue;
                    }

                    #endregion

                    #region Keys

                    /* Hotkeys 1 */
                    if (strInnerValue.StartsWith(Constants.StrPreferenceHotkey1))
                    {
                        strInnerValue = strInnerValue.Substring(Constants.StrPreferenceHotkey1.Length,
                                                                strInnerValue.Length -
                                                                Constants.StrPreferenceHotkey1.Length);

                        Keys kHotkey1;
                        Enum.TryParse(strInnerValue, out kHotkey1);

                        pSettings.ResourceHotkey1 = kHotkey1;
                    }

                    /* Hotkeys 2 */
                    if (strInnerValue.StartsWith(Constants.StrPreferenceHotkey2))
                    {
                        strInnerValue = strInnerValue.Substring(Constants.StrPreferenceHotkey2.Length,
                                                                strInnerValue.Length -
                                                                Constants.StrPreferenceHotkey2.Length);

                        Keys kHotkey2;
                        Enum.TryParse(strInnerValue, out kHotkey2);

                        pSettings.ResourceHotkey2 = kHotkey2;
                    }

                    /* Hotkeys 3 */
                    if (strInnerValue.StartsWith(Constants.StrPreferenceHotkey3))
                    {
                        strInnerValue = strInnerValue.Substring(Constants.StrPreferenceHotkey3.Length,
                                                                strInnerValue.Length -
                                                                Constants.StrPreferenceHotkey3.Length);

                        Keys kHotkey3;
                        Enum.TryParse(strInnerValue, out kHotkey3);

                        pSettings.ResourceHotkey3 = kHotkey3;
                    }

                    #endregion

                    #region Other

                    /* Opacity */
                    if (strInnerValue.StartsWith(Constants.StrPreferenceOpacity))
                    {
                        strInnerValue = strInnerValue.Substring(Constants.StrPreferenceOpacity.Length,
                                                                strInnerValue.Length -
                                                                Constants.StrPreferenceOpacity.Length);

                        Double dOpacity;
                        Double.TryParse(strInnerValue, out dOpacity);

                        pSettings.ResourceOpacity = dOpacity;
                    }

                    #endregion
                }

                    #endregion

                #region Income

            else if (strKeyword.Equals(Constants.StrPreferenceKeywordIncome))
            {
                #region Boolean values

                /* Remove Ai */
                if (strInnerValue.StartsWith(Constants.StrPreferenceRemoveAi))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceRemoveAi.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceRemoveAi.Length);

                    bool bRemoveAi;
                    Boolean.TryParse(strInnerValue, out bRemoveAi);

                    pSettings.IncomeRemoveAi = bRemoveAi;
                }

                /* Remove Localplayer */
                if (strInnerValue.StartsWith(Constants.StrPreferenceRemoveLocalplayer))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceRemoveLocalplayer.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceRemoveLocalplayer.Length);

                    bool bRemoveLocalplayer;
                    Boolean.TryParse(strInnerValue, out bRemoveLocalplayer);

                    pSettings.IncomeRemoveLocalplayer = bRemoveLocalplayer;
                }

                /* Remove Neutral */
                if (strInnerValue.StartsWith(Constants.StrPreferenceRemoveNeutral))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceRemoveNeutral.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceRemoveNeutral.Length);

                    bool bRemoveNeutral;
                    Boolean.TryParse(strInnerValue, out bRemoveNeutral);

                    pSettings.IncomeRemoveNeutral = bRemoveNeutral;
                }

                /* Remove Allie */
                if (strInnerValue.StartsWith(Constants.StrPreferenceRemoveAllie))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceRemoveAllie.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceRemoveAllie.Length);

                    bool bRemoveAllie;
                    Boolean.TryParse(strInnerValue, out bRemoveAllie);

                    pSettings.IncomeRemoveAllie = bRemoveAllie;
                }

                /* Disable Background */
                if (strInnerValue.StartsWith(Constants.StrPreferenceDisableBackground))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceDisableBackground.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceDisableBackground.Length);

                    bool bDisableBackground;
                    Boolean.TryParse(strInnerValue, out bDisableBackground);

                    pSettings.IncomeDrawBackground = bDisableBackground;
                }

                #endregion

                #region Int32 values

                /* Pos X */
                if (strInnerValue.StartsWith(Constants.StrPreferencePosX))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferencePosX.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferencePosX.Length);
                    if (strInnerValue.Length > 0)
                    {
                        Int32 iPosX;
                        Int32.TryParse(strInnerValue, out iPosX);

                        pSettings.IncomePositionX = iPosX;
                    }

                    else
                        pSettings.IncomePositionX = 50;
                }

                /* Pos Y */
                if (strInnerValue.StartsWith(Constants.StrPreferencePosY))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferencePosY.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferencePosY.Length);
                    if (strInnerValue.Length > 0)
                    {
                        Int32 iPosY;
                        Int32.TryParse(strInnerValue, out iPosY);

                        pSettings.IncomePositionY = iPosY;
                    }

                    else
                        pSettings.IncomePositionY = 50;
                }

                /* Width */
                if (strInnerValue.StartsWith(Constants.StrPreferenceWidth))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceWidth.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceWidth.Length);

                    Int32 iWidth;
                    Int32.TryParse(strInnerValue, out iWidth);

                    if (iWidth <= 10)
                        pSettings.IncomeWidth = 600;

                    else 
                        pSettings.IncomeWidth = iWidth;
                }

                /* Height */
                if (strInnerValue.StartsWith(Constants.StrPreferenceHeight))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceHeight.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceHeight.Length);

                    Int32 iHeight;
                    Int32.TryParse(strInnerValue, out iHeight);

                    if (iHeight <= 10)
                        pSettings.IncomeHeight = 50;

                    else
                        pSettings.IncomeHeight = iHeight;
                }


                #endregion

                #region String

                /* Font Name */
                if (strInnerValue.StartsWith(Constants.StrPreferenceFontText))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceFontText.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceFontText.Length);


                    pSettings.IncomeFontName = strInnerValue.Length <= 0 ? "Century Gothic" : strInnerValue;
                }

                /* On/ Off Panel */
                if (strInnerValue.StartsWith(Constants.StrPreferenceOnOffText))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceOnOffText.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceOnOffText.Length);


                    pSettings.IncomeTogglePanel = strInnerValue.Length <= 0 ? "/res" : strInnerValue;
                }

                /* Change Position */
                if (strInnerValue.StartsWith(Constants.StrPreferenceChangePositionText))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceChangePositionText.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceChangePositionText.Length);


                    pSettings.IncomeChangePositionPanel = strInnerValue.Length <= 0 ? "/rcp" : strInnerValue;
                }

                /* Change Size */
                if (strInnerValue.StartsWith(Constants.StrPreferenceChangeSizeText))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceChangeSizeText.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceChangeSizeText.Length);


                    pSettings.IncomeChangeSizePanel = strInnerValue.Length <= 0 ? "/rcs" : strInnerValue;
                }

                #endregion

                #region Keys

                /* Hotkeys 1 */
                if (strInnerValue.StartsWith(Constants.StrPreferenceHotkey1))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceHotkey1.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceHotkey1.Length);

                    Keys kHotkey1;
                    Enum.TryParse(strInnerValue, out kHotkey1);

                    pSettings.IncomeHotkey1 = kHotkey1;
                }

                /* Hotkeys 2 */
                if (strInnerValue.StartsWith(Constants.StrPreferenceHotkey2))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceHotkey2.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceHotkey2.Length);

                    Keys kHotkey2;
                    Enum.TryParse(strInnerValue, out kHotkey2);

                    pSettings.IncomeHotkey2 = kHotkey2;
                }

                /* Hotkeys 3 */
                if (strInnerValue.StartsWith(Constants.StrPreferenceHotkey3))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceHotkey3.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceHotkey3.Length);

                    Keys kHotkey3;
                    Enum.TryParse(strInnerValue, out kHotkey3);

                    pSettings.IncomeHotkey3 = kHotkey3;
                }

                #endregion

                #region Other

                /* Opacity */
                if (strInnerValue.StartsWith(Constants.StrPreferenceOpacity))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceOpacity.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceOpacity.Length);

                    Double dOpacity;
                    Double.TryParse(strInnerValue, out dOpacity);

                    pSettings.IncomeOpacity = dOpacity;
                }

                #endregion
            }

                #endregion

                #region Apm

            else if (strKeyword.Equals(Constants.StrPreferenceKeywordApm))
            {
                #region Boolean values

                /* Remove Ai */
                if (strInnerValue.StartsWith(Constants.StrPreferenceRemoveAi))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceRemoveAi.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceRemoveAi.Length);

                    bool bRemoveAi;
                    Boolean.TryParse(strInnerValue, out bRemoveAi);

                    pSettings.ApmRemoveAi = bRemoveAi;
                }

                /* Remove Localplayer */
                if (strInnerValue.StartsWith(Constants.StrPreferenceRemoveLocalplayer))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceRemoveLocalplayer.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceRemoveLocalplayer.Length);

                    bool bRemoveLocalplayer;
                    Boolean.TryParse(strInnerValue, out bRemoveLocalplayer);

                    pSettings.ApmRemoveLocalplayer = bRemoveLocalplayer;
                }

                /* Remove Neutral */
                if (strInnerValue.StartsWith(Constants.StrPreferenceRemoveNeutral))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceRemoveNeutral.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceRemoveNeutral.Length);

                    bool bRemoveNeutral;
                    Boolean.TryParse(strInnerValue, out bRemoveNeutral);

                    pSettings.ApmRemoveNeutral = bRemoveNeutral;
                }

                /* Remove Allie */
                if (strInnerValue.StartsWith(Constants.StrPreferenceRemoveAllie))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceRemoveAllie.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceRemoveAllie.Length);

                    bool bRemoveAllie;
                    Boolean.TryParse(strInnerValue, out bRemoveAllie);

                    pSettings.ApmRemoveAllie = bRemoveAllie;
                }

                /* Disable Background */
                if (strInnerValue.StartsWith(Constants.StrPreferenceDisableBackground))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceDisableBackground.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceDisableBackground.Length);

                    bool bDisableBackground;
                    Boolean.TryParse(strInnerValue, out bDisableBackground);

                    pSettings.ApmDrawBackground = bDisableBackground;
                }

                #endregion

                #region Int32 values

                /* Pos X */
                if (strInnerValue.StartsWith(Constants.StrPreferencePosX))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferencePosX.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferencePosX.Length);
                    if (strInnerValue.Length > 0)
                    {
                        Int32 iPosX;
                        Int32.TryParse(strInnerValue, out iPosX);

                        pSettings.ApmPositionX = iPosX;
                    }

                    else
                        pSettings.ApmPositionX = 50;
                }

                /* Pos Y */
                if (strInnerValue.StartsWith(Constants.StrPreferencePosY))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferencePosY.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferencePosY.Length);

                    if (strInnerValue.Length > 0)
                    {
                        Int32 iPosY;
                        Int32.TryParse(strInnerValue, out iPosY);

                        pSettings.ApmPositionY = iPosY;
                    }

                    pSettings.ApmPositionY = 50;
                }

                /* Width */
                if (strInnerValue.StartsWith(Constants.StrPreferenceWidth))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceWidth.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceWidth.Length);

                    Int32 iWidth;
                    Int32.TryParse(strInnerValue, out iWidth);

                    if (iWidth <= 10)
                        pSettings.ApmWidth = 600;

                    else 
                        pSettings.ApmWidth = iWidth;
                }

                /* Height */
                if (strInnerValue.StartsWith(Constants.StrPreferenceHeight))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceHeight.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceHeight.Length);

                    Int32 iHeight;
                    Int32.TryParse(strInnerValue, out iHeight);

                    if (iHeight <= 10)
                        pSettings.ApmHeight = 50;

                    else 
                        pSettings.ApmHeight = iHeight;
                }


                #endregion

                #region String

                /* Font Name */
                if (strInnerValue.StartsWith(Constants.StrPreferenceFontText))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceFontText.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceFontText.Length);


                    pSettings.ApmFontName = strInnerValue.Length <= 0 ? "Century Gothic" : strInnerValue;
                }

                /* On/ Off Panel */
                if (strInnerValue.StartsWith(Constants.StrPreferenceOnOffText))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceOnOffText.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceOnOffText.Length);


                    pSettings.ApmTogglePanel = strInnerValue.Length <= 0 ? "/res" : strInnerValue;
                }

                /* Change Position */
                if (strInnerValue.StartsWith(Constants.StrPreferenceChangePositionText))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceChangePositionText.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceChangePositionText.Length);


                    pSettings.ApmChangePositionPanel = strInnerValue.Length <= 0 ? "/rcp" : strInnerValue;
                }

                /* Change Size */
                if (strInnerValue.StartsWith(Constants.StrPreferenceChangeSizeText))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceChangeSizeText.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceChangeSizeText.Length);


                    pSettings.ApmChangeSizePanel = strInnerValue.Length <= 0 ? "/rcs" : strInnerValue;
                }

                #endregion

                #region Keys

                /* Hotkeys 1 */
                if (strInnerValue.StartsWith(Constants.StrPreferenceHotkey1))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceHotkey1.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceHotkey1.Length);

                    Keys kHotkey1;
                    Enum.TryParse(strInnerValue, out kHotkey1);

                    pSettings.ApmHotkey1 = kHotkey1;
                }

                /* Hotkeys 2 */
                if (strInnerValue.StartsWith(Constants.StrPreferenceHotkey2))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceHotkey2.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceHotkey2.Length);

                    Keys kHotkey2;
                    Enum.TryParse(strInnerValue, out kHotkey2);

                    pSettings.ApmHotkey2 = kHotkey2;
                }

                /* Hotkeys 3 */
                if (strInnerValue.StartsWith(Constants.StrPreferenceHotkey3))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceHotkey3.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceHotkey3.Length);

                    Keys kHotkey3;
                    Enum.TryParse(strInnerValue, out kHotkey3);

                    pSettings.ApmHotkey3 = kHotkey3;
                }

                #endregion

                #region Other

                /* Opacity */
                if (strInnerValue.StartsWith(Constants.StrPreferenceOpacity))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceOpacity.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceOpacity.Length);

                    Double dOpacity;
                    Double.TryParse(strInnerValue, out dOpacity);

                    pSettings.ApmOpacity = dOpacity;
                }

                #endregion
            }

                #endregion

                #region Army

            else if (strKeyword.Equals(Constants.StrPreferenceKeywordArmy))
            {
                #region Boolean values

                /* Remove Ai */
                if (strInnerValue.StartsWith(Constants.StrPreferenceRemoveAi))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceRemoveAi.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceRemoveAi.Length);

                    bool bRemoveAi;
                    Boolean.TryParse(strInnerValue, out bRemoveAi);

                    pSettings.ArmyRemoveAi = bRemoveAi;
                }

                /* Remove Localplayer */
                if (strInnerValue.StartsWith(Constants.StrPreferenceRemoveLocalplayer))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceRemoveLocalplayer.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceRemoveLocalplayer.Length);

                    bool bRemoveLocalplayer;
                    Boolean.TryParse(strInnerValue, out bRemoveLocalplayer);

                    pSettings.ArmyRemoveLocalplayer = bRemoveLocalplayer;
                }

                /* Remove Neutral */
                if (strInnerValue.StartsWith(Constants.StrPreferenceRemoveNeutral))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceRemoveNeutral.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceRemoveNeutral.Length);

                    bool bRemoveNeutral;
                    Boolean.TryParse(strInnerValue, out bRemoveNeutral);

                    pSettings.ArmyRemoveNeutral = bRemoveNeutral;
                }

                /* Remove Allie */
                if (strInnerValue.StartsWith(Constants.StrPreferenceRemoveAllie))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceRemoveAllie.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceRemoveAllie.Length);

                    bool bRemoveAllie;
                    Boolean.TryParse(strInnerValue, out bRemoveAllie);

                    pSettings.ArmyRemoveAllie = bRemoveAllie;
                }

                /* Disable Background */
                if (strInnerValue.StartsWith(Constants.StrPreferenceDisableBackground))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceDisableBackground.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceDisableBackground.Length);

                    bool bDisableBackground;
                    Boolean.TryParse(strInnerValue, out bDisableBackground);

                    pSettings.ArmyDrawBackground = bDisableBackground;
                }

                #endregion

                #region Int32 values

                /* Pos X */
                if (strInnerValue.StartsWith(Constants.StrPreferencePosX))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferencePosX.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferencePosX.Length);
                    if (strInnerValue.Length > 0)
                    {
                        Int32 iPosX;
                        Int32.TryParse(strInnerValue, out iPosX);

                        pSettings.ArmyPositionX = iPosX;
                    }

                    else
                        pSettings.ArmyPositionX = 50;

                }

                /* Pos Y */
                if (strInnerValue.StartsWith(Constants.StrPreferencePosY))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferencePosY.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferencePosY.Length);

                    if (strInnerValue.Length > 0)
                    {
                        Int32 iPosY;
                        Int32.TryParse(strInnerValue, out iPosY);

                        pSettings.ArmyPositionY = iPosY;
                    }

                    else
                        pSettings.ArmyPositionY = 50;
                }

                /* Width */
                if (strInnerValue.StartsWith(Constants.StrPreferenceWidth))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceWidth.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceWidth.Length);

                    Int32 iWidth;
                    Int32.TryParse(strInnerValue, out iWidth);

                    if (iWidth <= 10)
                        pSettings.ArmyWidth = 600;

                    else 
                        pSettings.ArmyWidth = iWidth;
                }

                /* Height */
                if (strInnerValue.StartsWith(Constants.StrPreferenceHeight))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceHeight.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceHeight.Length);

                    Int32 iHeight;
                    Int32.TryParse(strInnerValue, out iHeight);

                    if (iHeight <= 10)
                        pSettings.ArmyHeight = 50;

                    else 
                        pSettings.ArmyHeight = iHeight;
                }


                #endregion

                #region String

                /* Font Name */
                if (strInnerValue.StartsWith(Constants.StrPreferenceFontText))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceFontText.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceFontText.Length);


                    pSettings.ArmyFontName = strInnerValue.Length <= 0 ? "Century Gothic" : strInnerValue;
                }

                /* On/ Off Panel */
                if (strInnerValue.StartsWith(Constants.StrPreferenceOnOffText))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceOnOffText.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceOnOffText.Length);


                    pSettings.ArmyTogglePanel = strInnerValue.Length <= 0 ? "/res" : strInnerValue;
                }

                /* Change Position */
                if (strInnerValue.StartsWith(Constants.StrPreferenceChangePositionText))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceChangePositionText.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceChangePositionText.Length);


                    pSettings.ArmyChangePositionPanel = strInnerValue.Length <= 0 ? "/rcp" : strInnerValue;
                }

                /* Change Size */
                if (strInnerValue.StartsWith(Constants.StrPreferenceChangeSizeText))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceChangeSizeText.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceChangeSizeText.Length);


                    pSettings.ArmyChangeSizePanel = strInnerValue.Length <= 0 ? "/rcs" : strInnerValue;
                }

                #endregion

                #region Keys

                /* Hotkeys 1 */
                if (strInnerValue.StartsWith(Constants.StrPreferenceHotkey1))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceHotkey1.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceHotkey1.Length);

                    Keys kHotkey1;
                    Enum.TryParse(strInnerValue, out kHotkey1);

                    pSettings.ArmyHotkey1 = kHotkey1;
                }

                /* Hotkeys 2 */
                if (strInnerValue.StartsWith(Constants.StrPreferenceHotkey2))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceHotkey2.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceHotkey2.Length);

                    Keys kHotkey2;
                    Enum.TryParse(strInnerValue, out kHotkey2);

                    pSettings.ArmyHotkey2 = kHotkey2;
                }

                /* Hotkeys 3 */
                if (strInnerValue.StartsWith(Constants.StrPreferenceHotkey3))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceHotkey3.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceHotkey3.Length);

                    Keys kHotkey3;
                    Enum.TryParse(strInnerValue, out kHotkey3);

                    pSettings.ArmyHotkey3 = kHotkey3;
                }

                #endregion

                #region Other

                /* Opacity */
                if (strInnerValue.StartsWith(Constants.StrPreferenceOpacity))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceOpacity.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceOpacity.Length);

                    Double dOpacity;
                    Double.TryParse(strInnerValue, out dOpacity);

                    pSettings.ArmyOpacity = dOpacity;
                }

                #endregion
            }

                #endregion

                #region Worker

            else if (strKeyword.Equals(Constants.StrPreferenceKeywordWorker))
            {
                #region Boolean values

                /* Disable Background */
                if (strInnerValue.StartsWith(Constants.StrPreferenceDisableBackground))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceDisableBackground.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceDisableBackground.Length);

                    bool bDisableBackground;
                    Boolean.TryParse(strInnerValue, out bDisableBackground);

                    pSettings.WorkerDrawBackground = bDisableBackground;
                }

                #endregion

                #region Int32 values

                /* Pos X */
                if (strInnerValue.StartsWith(Constants.StrPreferencePosX))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferencePosX.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferencePosX.Length);
                    if (strInnerValue.Length > 0)
                    {
                        Int32 iPosX;
                        Int32.TryParse(strInnerValue, out iPosX);

                        pSettings.WorkerPositionX = iPosX;
                    }

                    else
                        pSettings.WorkerPositionX = 50;
                }

                /* Pos Y */
                if (strInnerValue.StartsWith(Constants.StrPreferencePosY))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferencePosY.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferencePosY.Length);
                    if (strInnerValue.Length > 0)
                    {
                        Int32 iPosY;
                        Int32.TryParse(strInnerValue, out iPosY);

                        pSettings.WorkerPositionY = iPosY;
                    }

                    else
                        pSettings.WorkerPositionY = 50;
                }

                /* Width */
                if (strInnerValue.StartsWith(Constants.StrPreferenceWidth))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceWidth.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceWidth.Length);

                    Int32 iWidth;
                    Int32.TryParse(strInnerValue, out iWidth);

                    if (iWidth <= 10)
                        pSettings.WorkerWidth = 200;

                    else
                        pSettings.WorkerWidth = iWidth;
                }

                /* Height */
                if (strInnerValue.StartsWith(Constants.StrPreferenceHeight))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceHeight.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceHeight.Length);

                    Int32 iHeight;
                    Int32.TryParse(strInnerValue, out iHeight);

                    if (iHeight <= 10)
                        pSettings.WorkerHeight = 50;

                    else
                        pSettings.WorkerHeight = iHeight;
                }


                #endregion

                #region String

                /* Font Name */
                if (strInnerValue.StartsWith(Constants.StrPreferenceFontText))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceFontText.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceFontText.Length);


                    pSettings.WorkerFontName = strInnerValue.Length <= 0 ? "Century Gothic" : strInnerValue;
                }

                /* On/ Off Panel */
                if (strInnerValue.StartsWith(Constants.StrPreferenceOnOffText))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceOnOffText.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceOnOffText.Length);


                    pSettings.WorkerTogglePanel = strInnerValue.Length <= 0 ? "/res" : strInnerValue;
                }

                /* Change Position */
                if (strInnerValue.StartsWith(Constants.StrPreferenceChangePositionText))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceChangePositionText.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceChangePositionText.Length);


                    pSettings.WorkerChangePositionPanel = strInnerValue.Length <= 0 ? "/rcp" : strInnerValue;
                }

                /* Change Size */
                if (strInnerValue.StartsWith(Constants.StrPreferenceChangeSizeText))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceChangeSizeText.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceChangeSizeText.Length);


                    pSettings.WorkerChangeSizePanel = strInnerValue.Length <= 0 ? "/rcs" : strInnerValue;
                }

                #endregion

                #region Keys

                /* Hotkeys 1 */
                if (strInnerValue.StartsWith(Constants.StrPreferenceHotkey1))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceHotkey1.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceHotkey1.Length);

                    Keys kHotkey1;
                    Enum.TryParse(strInnerValue, out kHotkey1);

                    pSettings.WorkerHotkey1 = kHotkey1;
                }

                /* Hotkeys 2 */
                if (strInnerValue.StartsWith(Constants.StrPreferenceHotkey2))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceHotkey2.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceHotkey2.Length);

                    Keys kHotkey2;
                    Enum.TryParse(strInnerValue, out kHotkey2);

                    pSettings.WorkerHotkey2 = kHotkey2;
                }

                /* Hotkeys 3 */
                if (strInnerValue.StartsWith(Constants.StrPreferenceHotkey3))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceHotkey3.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceHotkey3.Length);

                    Keys kHotkey3;
                    Enum.TryParse(strInnerValue, out kHotkey3);

                    pSettings.WorkerHotkey3 = kHotkey3;
                }

                #endregion

                #region Other

                /* Opacity */
                if (strInnerValue.StartsWith(Constants.StrPreferenceOpacity))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceOpacity.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceOpacity.Length);

                    Double dOpacity;
                    Double.TryParse(strInnerValue, out dOpacity);

                    pSettings.WorkerOpacity = dOpacity;
                }

                #endregion
            }

                #endregion

                #region UnitTab

            else if (strKeyword.Equals(Constants.StrPreferenceKeywordUnitTab))
            {
                #region Boolean values

                /* Remove Ai */
                if (strInnerValue.StartsWith(Constants.StrPreferenceRemoveAi))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceRemoveAi.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceRemoveAi.Length);

                    bool bRemoveAi;
                    Boolean.TryParse(strInnerValue, out bRemoveAi);

                    pSettings.UnitTabRemoveAi = bRemoveAi;
                }

                /* Remove Localplayer */
                if (strInnerValue.StartsWith(Constants.StrPreferenceRemoveLocalplayer))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceRemoveLocalplayer.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceRemoveLocalplayer.Length);

                    bool bRemoveLocalplayer;
                    Boolean.TryParse(strInnerValue, out bRemoveLocalplayer);

                    pSettings.UnitTabRemoveLocalplayer = bRemoveLocalplayer;
                }

                /* Remove Neutral */
                if (strInnerValue.StartsWith(Constants.StrPreferenceRemoveNeutral))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceRemoveNeutral.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceRemoveNeutral.Length);

                    bool bRemoveNeutral;
                    Boolean.TryParse(strInnerValue, out bRemoveNeutral);

                    pSettings.UnitTabRemoveNeutral = bRemoveNeutral;
                }

                /* Remove Allie */
                if (strInnerValue.StartsWith(Constants.StrPreferenceRemoveAllie))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceRemoveAllie.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceRemoveAllie.Length);

                    bool bRemoveAllie;
                    Boolean.TryParse(strInnerValue, out bRemoveAllie);

                    pSettings.UnitTabRemoveAllie = bRemoveAllie;
                }

                /* DSplit Buildings */
                var strSplitBuildings = "Split Buildings/ Units";
                if (strInnerValue.StartsWith(strSplitBuildings))
                {
                    strInnerValue = strInnerValue.Substring(strSplitBuildings.Length,
                                                            strInnerValue.Length - strSplitBuildings.Length);

                    bool bSplitBuildings;
                    Boolean.TryParse(strInnerValue, out bSplitBuildings);

                    pSettings.UnitTabSplitUnitsAndBuildings = bSplitBuildings;
                }

                #endregion

                #region Int32 values

                /* Pos X */
                if (strInnerValue.StartsWith(Constants.StrPreferencePosX))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferencePosX.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferencePosX.Length);

                    Int32 iPosX;
                    Int32.TryParse(strInnerValue, out iPosX);

                    pSettings.UnitTabPositionX = iPosX;
                }

                /* Pos Y */
                if (strInnerValue.StartsWith(Constants.StrPreferencePosY))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferencePosY.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferencePosY.Length);

                    Int32 iPosY;
                    Int32.TryParse(strInnerValue, out iPosY);

                    pSettings.UnitTabPositionY = iPosY;
                }

                /* Width */
                if (strInnerValue.StartsWith(Constants.StrPreferenceWidth))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceWidth.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceWidth.Length);

                    Int32 iWidth;
                    Int32.TryParse(strInnerValue, out iWidth);

                    pSettings.UnitTabWidth = iWidth;
                }

                /* Height */
                if (strInnerValue.StartsWith(Constants.StrPreferenceHeight))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceHeight.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceHeight.Length);

                    Int32 iHeight;
                    Int32.TryParse(strInnerValue, out iHeight);

                    pSettings.UnitTabHeigth = iHeight;
                }


                #endregion

                #region String

                /* On/ Off Panel */
                if (strInnerValue.StartsWith(Constants.StrPreferenceOnOffText))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceOnOffText.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceOnOffText.Length);


                    pSettings.UnitTogglePanel = strInnerValue.Length <= 0 ? "/res" : strInnerValue;
                }

                /* Change Position */
                if (strInnerValue.StartsWith(Constants.StrPreferenceChangePositionText))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceChangePositionText.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceChangePositionText.Length);


                    pSettings.UnitChangePositionPanel = strInnerValue.Length <= 0 ? "/rcp" : strInnerValue;
                }

                /* Change Size */
                if (strInnerValue.StartsWith(Constants.StrPreferenceChangeSizeText))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceChangeSizeText.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceChangeSizeText.Length);


                    pSettings.UnitChangeSizePanel = strInnerValue.Length <= 0 ? "/rcs" : strInnerValue;
                }

                #endregion

                #region Keys

                /* Hotkeys 1 */
                if (strInnerValue.StartsWith(Constants.StrPreferenceHotkey1))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceHotkey1.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceHotkey1.Length);

                    Keys kHotkey1;
                    Enum.TryParse(strInnerValue, out kHotkey1);

                    pSettings.UnitHotkey1 = kHotkey1;
                }

                /* Hotkeys 2 */
                if (strInnerValue.StartsWith(Constants.StrPreferenceHotkey2))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceHotkey2.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceHotkey2.Length);

                    Keys kHotkey2;
                    Enum.TryParse(strInnerValue, out kHotkey2);

                    pSettings.UnitHotkey2 = kHotkey2;
                }

                /* Hotkeys 3 */
                if (strInnerValue.StartsWith(Constants.StrPreferenceHotkey3))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceHotkey3.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceHotkey3.Length);

                    Keys kHotkey3;
                    Enum.TryParse(strInnerValue, out kHotkey3);

                    pSettings.UnitHotkey3 = kHotkey3;
                }

                #endregion

                #region Other

                /* Opacity */
                if (strInnerValue.StartsWith(Constants.StrPreferenceOpacity))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceOpacity.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceOpacity.Length);

                    Double dOpacity;
                    Double.TryParse(strInnerValue, out dOpacity);

                    pSettings.UnitTabOpacity = dOpacity;
                }

                #endregion
            }

                #endregion

                #region Maphack

            else if (strKeyword.Equals(Constants.StrPreferenceKeywordMaphack))
            {
                #region Boolean values

                /* Remove Ai */
                if (strInnerValue.StartsWith(Constants.StrPreferenceRemoveAi))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceRemoveAi.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceRemoveAi.Length);

                    bool bRemoveAi;
                    Boolean.TryParse(strInnerValue, out bRemoveAi);

                    pSettings.MaphackRemoveAi = bRemoveAi;
                }

                /* Remove Localplayer */
                if (strInnerValue.StartsWith(Constants.StrPreferenceRemoveLocalplayer))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceRemoveLocalplayer.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceRemoveLocalplayer.Length);

                    bool bRemoveLocalplayer;
                    Boolean.TryParse(strInnerValue, out bRemoveLocalplayer);

                    pSettings.MaphackRemoveLocalplayer = bRemoveLocalplayer;
                }

                /* Remove Neutral */
                if (strInnerValue.StartsWith(Constants.StrPreferenceRemoveNeutral))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceRemoveNeutral.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceRemoveNeutral.Length);

                    bool bRemoveNeutral;
                    Boolean.TryParse(strInnerValue, out bRemoveNeutral);

                    pSettings.MaphackRemoveNeutral = bRemoveNeutral;
                }

                /* Remove Allie */
                if (strInnerValue.StartsWith(Constants.StrPreferenceRemoveAllie))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceRemoveAllie.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceRemoveAllie.Length);

                    bool bRemoveAllie;
                    Boolean.TryParse(strInnerValue, out bRemoveAllie);

                    pSettings.MaphackRemoveAllie = bRemoveAllie;
                }

                /* Disable DestinationLine */
                if (strInnerValue.StartsWith("Disable Destination Line"))
                {
                    strInnerValue = strInnerValue.Substring("Disable Destination Line".Length,
                                                            strInnerValue.Length - "Disable Destination Line".Length);

                    bool bDisableDestinationLine;
                    Boolean.TryParse(strInnerValue, out bDisableDestinationLine);

                    pSettings.MaphackDisableDestinationLine = bDisableDestinationLine;
                }

                /* Color Defensive structures */
                if (strInnerValue.StartsWith("Color Def Structures"))
                {
                    strInnerValue = strInnerValue.Substring("Color Def Structures".Length,
                                                            strInnerValue.Length - "Color Def Structures".Length);

                    bool bColorDefensiveStructures;
                    Boolean.TryParse(strInnerValue, out bColorDefensiveStructures);

                    pSettings.MaphackColorDefensivestructuresYellow = bColorDefensiveStructures;
                }

                #endregion

                #region Int32 values

                /* Pos X */
                if (strInnerValue.StartsWith(Constants.StrPreferencePosX))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferencePosX.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferencePosX.Length);
                    if (strInnerValue.Length > 0)
                    {
                        Int32 iPosX;
                        Int32.TryParse(strInnerValue, out iPosX);

                        pSettings.MaphackPositionX = iPosX;
                    }

                    else
                        pSettings.MaphackPositionX = 50;
                }

                /* Pos Y */
                if (strInnerValue.StartsWith(Constants.StrPreferencePosY))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferencePosY.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferencePosY.Length);
                    if (strInnerValue.Length > 0)
                    {
                        Int32 iPosY;
                        Int32.TryParse(strInnerValue, out iPosY);

                        pSettings.MaphackPositionY = iPosY;
                    }

                    else
                        pSettings.MaphackHeigth = 50;
                }

                /* Width */
                if (strInnerValue.StartsWith(Constants.StrPreferenceWidth))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceWidth.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceWidth.Length);

                    Int32 iWidth;
                    Int32.TryParse(strInnerValue, out iWidth);

                    if (iWidth <= 10)
                        pSettings.WorkerWidth = 300;

                    else
                        pSettings.MaphackWidth = iWidth;
                }

                /* Height */
                if (strInnerValue.StartsWith(Constants.StrPreferenceHeight))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceHeight.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceHeight.Length);

                    Int32 iHeight;
                    Int32.TryParse(strInnerValue, out iHeight);

                    if (iHeight <= 10)
                        pSettings.MaphackHeigth = 300;

                    else 
                        pSettings.MaphackHeigth = iHeight;
                }


                #endregion

                #region String

                /* On/ Off Panel */
                if (strInnerValue.StartsWith(Constants.StrPreferenceOnOffText))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceOnOffText.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceOnOffText.Length);


                    pSettings.MaphackTogglePanel = strInnerValue.Length <= 0 ? "/res" : strInnerValue;
                }

                /* Change Position */
                if (strInnerValue.StartsWith(Constants.StrPreferenceChangePositionText))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceChangePositionText.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceChangePositionText.Length);


                    pSettings.MaphackChangePositionPanel = strInnerValue.Length <= 0 ? "/rcp" : strInnerValue;
                }

                /* Change Size */
                if (strInnerValue.StartsWith(Constants.StrPreferenceChangeSizeText))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceChangeSizeText.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceChangeSizeText.Length);


                    pSettings.MaphackChangeSizePanel = strInnerValue.Length <= 0 ? "/rcs" : strInnerValue;
                }

                #endregion

                #region Keys

                /* Hotkeys 1 */
                if (strInnerValue.StartsWith(Constants.StrPreferenceHotkey1))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceHotkey1.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceHotkey1.Length);

                    Keys kHotkey1;
                    Enum.TryParse(strInnerValue, out kHotkey1);

                    pSettings.MaphackHotkey1 = kHotkey1;
                }

                /* Hotkeys 2 */
                if (strInnerValue.StartsWith(Constants.StrPreferenceHotkey2))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceHotkey2.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceHotkey2.Length);

                    Keys kHotkey2;
                    Enum.TryParse(strInnerValue, out kHotkey2);

                    pSettings.MaphackHotkey2 = kHotkey2;
                }

                /* Hotkeys 3 */
                if (strInnerValue.StartsWith(Constants.StrPreferenceHotkey3))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceHotkey3.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceHotkey3.Length);

                    Keys kHotkey3;
                    Enum.TryParse(strInnerValue, out kHotkey3);

                    pSettings.MaphackHotkey3 = kHotkey3;
                }

                #endregion

                #region Other

                /* Opacity */
                if (strInnerValue.StartsWith(Constants.StrPreferenceOpacity))
                {
                    strInnerValue = strInnerValue.Substring(Constants.StrPreferenceOpacity.Length,
                                                            strInnerValue.Length -
                                                            Constants.StrPreferenceOpacity.Length);

                    Double dOpacity;
                    Double.TryParse(strInnerValue, out dOpacity);

                    pSettings.MaphackOpacity = dOpacity;
                }

                /* Color */
                if (strInnerValue.StartsWith("Destination Line Color"))
                {
                    strInnerValue = strInnerValue.Substring("Destination Line Color".Length,
                                                            strInnerValue.Length - "Destination Line Color".Length);

                    Color cDefColor = ColorTranslator.FromHtml(strInnerValue);

                    pSettings.MaphackDestinationColor = cDefColor;
                }

                #endregion
            }

                #endregion

                #region UnitId's

            else if (strKeyword.Equals(Constants.StrPreferenceKeywordMaphackUnits))
            {
                /* UnitId */
                if (strInnerValue.StartsWith("UnitId"))
                {
                    strInnerValue = strInnerValue.Substring("UnitId".Length,
                                                            strInnerValue.Length - "UnitId".Length);

                    if (strInnerValue.Length <= 0)
                    {
                        PredefinedTypes.UnitId uId;
                        uId = PredefinedTypes.UnitId.TuScv;
                        pSettings.MaphackUnitIds.Add(uId);
                    }

                    else
                    {



                        var id =
                            (PredefinedTypes.UnitId) Enum.Parse(typeof (PredefinedTypes.UnitId), strInnerValue);

                        pSettings.MaphackUnitIds.Add(id);
                    }
                }

                /* UnitColor Color */
                if (strInnerValue.StartsWith("Unit Color"))
                {
                    strInnerValue = strInnerValue.Substring("Unit Color".Length,
                                                            strInnerValue.Length - "Unit Color".Length);

                    if (strInnerValue.Length <= 0)
                        pSettings.MaphackUnitColors.Add(Color.Transparent);

                    else
                    {
                        var color = ColorTranslator.FromHtml(strInnerValue);

                        pSettings.MaphackUnitColors.Add(color);
                    }
                }
            }

            #endregion

                
            }

            #endregion

            return pSettings;
        }

        public static void WritePreferences(PredefinedTypes.Preferences pSettings)
        {
            if (File.Exists(Constants.StrDummyPref))
                File.Delete(Constants.StrDummyPref);

            var sw = new StreamWriter(Constants.StrDummyPref);

            #region Global

            sw.WriteLine("[Global]");
            sw.WriteLine(";Boolean");
            sw.WriteLine("DrawOnlyInForeground = " + pSettings.GlobalDrawOnlyInForeground);
            sw.WriteLine(";Keys");
            sw.WriteLine("Hotkey Resize = " + pSettings.GlobalChangeSizeAndPosition);
            sw.WriteLine(";Int32");
            sw.WriteLine("Data Refresh = " + pSettings.GlobalDataRefresh);
            sw.WriteLine(";Int32");
            sw.WriteLine("Drawing Refresh = " + pSettings.GlobalDrawingRefresh);

            #endregion


            sw.WriteLine("");
            sw.WriteLine("");

            #region Resource

            sw.WriteLine("[Resource]");
            sw.WriteLine(";Int32");
            sw.WriteLine("PosX = " + pSettings.ResourcePositionX.ToString(CultureInfo.InvariantCulture));
            sw.WriteLine(";Int32");
            sw.WriteLine("PosY = " + pSettings.ResourcePositionY.ToString(CultureInfo.InvariantCulture));
            sw.WriteLine(";Int32");
            sw.WriteLine("Width = " + pSettings.ResourceWidth.ToString(CultureInfo.InvariantCulture));
            sw.WriteLine(";Int32");
            sw.WriteLine("Height = " + pSettings.ResourceHeight.ToString(CultureInfo.InvariantCulture));
            sw.WriteLine(";Boolean");
            sw.WriteLine("Remove Ai = " + pSettings.ResourceRemoveAi);
            sw.WriteLine(";Boolean");
            sw.WriteLine("Remove Neutral = " + pSettings.ResourceRemoveNeutral);
            sw.WriteLine(";Boolean");
            sw.WriteLine("Remove Allie = " + pSettings.ResourceRemoveAllie);
            sw.WriteLine(";Boolean");
            sw.WriteLine("Remove Localplayer = " + pSettings.ResourceRemoveLocalplayer);
            sw.WriteLine(";String");
            sw.WriteLine("FontName = " + pSettings.ResourceFontName);
            sw.WriteLine(";float");
            sw.WriteLine("Opacity = " + pSettings.ResourceOpacity);
            sw.WriteLine(";String");
            sw.WriteLine("On/Off Text = " + pSettings.ResourceTogglePanel);
            sw.WriteLine(";String");
            sw.WriteLine("Change Position Text = " + pSettings.ResourceChangePositionPanel);
            sw.WriteLine(";String");
            sw.WriteLine("Change Size Text = " + pSettings.ResourceChangeSizePanel);
            sw.WriteLine(";Keys");
            sw.WriteLine("Hotkey 1 = " + pSettings.ResourceHotkey1);
            sw.WriteLine(";Keys");
            sw.WriteLine("Hotkey 2 = " + pSettings.ResourceHotkey2);
            sw.WriteLine(";Keys");
            sw.WriteLine("Hotkey 3 = " + pSettings.ResourceHotkey3);
            sw.WriteLine(";Boolean");
            sw.WriteLine("Draw Background = " + pSettings.ResourceDrawBackground);

            #endregion


            sw.WriteLine("");
            sw.WriteLine("");

            #region Income

            sw.WriteLine("[Income]");
            sw.WriteLine(";Int32");
            sw.WriteLine("PosX = " + pSettings.IncomePositionX.ToString(CultureInfo.InvariantCulture));
            sw.WriteLine(";Int32");
            sw.WriteLine("PosY = " + pSettings.IncomePositionY.ToString(CultureInfo.InvariantCulture));
            sw.WriteLine(";Int32");
            sw.WriteLine("Width = " + pSettings.IncomeWidth.ToString(CultureInfo.InvariantCulture));
            sw.WriteLine(";Int32");
            sw.WriteLine("Height = " + pSettings.IncomeHeight.ToString(CultureInfo.InvariantCulture));
            sw.WriteLine(";Boolean");
            sw.WriteLine("Remove Ai = " + pSettings.IncomeRemoveAi);
            sw.WriteLine(";Boolean");
            sw.WriteLine("Remove Neutral = " + pSettings.IncomeRemoveNeutral);
            sw.WriteLine(";Boolean");
            sw.WriteLine("Remove Allie = " + pSettings.IncomeRemoveAllie);
            sw.WriteLine(";Boolean");
            sw.WriteLine("Remove Localplayer = " + pSettings.IncomeRemoveLocalplayer);
            sw.WriteLine(";String");
            sw.WriteLine("FontName = " + pSettings.IncomeFontName);
            sw.WriteLine(";float");
            sw.WriteLine("Opacity = " + pSettings.IncomeOpacity);
            sw.WriteLine(";String");
            sw.WriteLine("On/Off Text = " + pSettings.IncomeTogglePanel);
            sw.WriteLine(";String");
            sw.WriteLine("Change Position Text = " + pSettings.IncomeChangePositionPanel);
            sw.WriteLine(";String");
            sw.WriteLine("Change Size Text = " + pSettings.IncomeChangeSizePanel);
            sw.WriteLine(";Keys");
            sw.WriteLine("Hotkey 1 = " + pSettings.IncomeHotkey1);
            sw.WriteLine(";Keys");
            sw.WriteLine("Hotkey 2 = " + pSettings.IncomeHotkey2);
            sw.WriteLine(";Keys");
            sw.WriteLine("Hotkey 3 = " + pSettings.IncomeHotkey3);
            sw.WriteLine(";Boolean");
            sw.WriteLine("Draw Background = " + pSettings.IncomeDrawBackground);

            #endregion


            sw.WriteLine("");
            sw.WriteLine("");

            #region Apm

            sw.WriteLine("[Apm]");
            sw.WriteLine(";Int32");
            sw.WriteLine("PosX = " + pSettings.ApmPositionX.ToString(CultureInfo.InvariantCulture));
            sw.WriteLine(";Int32");
            sw.WriteLine("PosY = " + pSettings.ApmPositionY.ToString(CultureInfo.InvariantCulture));
            sw.WriteLine(";Int32");
            sw.WriteLine("Width = " + pSettings.ApmWidth.ToString(CultureInfo.InvariantCulture));
            sw.WriteLine(";Int32");
            sw.WriteLine("Height = " + pSettings.ApmHeight.ToString(CultureInfo.InvariantCulture));
            sw.WriteLine(";Boolean");
            sw.WriteLine("Remove Ai = " + pSettings.ApmRemoveAi);
            sw.WriteLine(";Boolean");
            sw.WriteLine("Remove Neutral = " + pSettings.ApmRemoveNeutral);
            sw.WriteLine(";Boolean");
            sw.WriteLine("Remove Allie = " + pSettings.ApmRemoveAllie);
            sw.WriteLine(";Boolean");
            sw.WriteLine("Remove Localplayer = " + pSettings.ApmRemoveLocalplayer);
            sw.WriteLine(";String");
            sw.WriteLine("FontName = " + pSettings.ApmFontName);
            sw.WriteLine(";float");
            sw.WriteLine("Opacity = " + pSettings.ApmOpacity);
            sw.WriteLine(";String");
            sw.WriteLine("On/Off Text = " + pSettings.ApmTogglePanel);
            sw.WriteLine(";String");
            sw.WriteLine("Change Position Text = " + pSettings.ApmChangePositionPanel);
            sw.WriteLine(";String");
            sw.WriteLine("Change Size Text = " + pSettings.ApmChangeSizePanel);
            sw.WriteLine(";Keys");
            sw.WriteLine("Hotkey 1 = " + pSettings.ApmHotkey1);
            sw.WriteLine(";Keys");
            sw.WriteLine("Hotkey 2 = " + pSettings.ApmHotkey2);
            sw.WriteLine(";Keys");
            sw.WriteLine("Hotkey 3 = " + pSettings.ApmHotkey3);
            sw.WriteLine(";Boolean");
            sw.WriteLine("Draw Background = " + pSettings.ApmDrawBackground);

            #endregion


            sw.WriteLine("");
            sw.WriteLine("");

            #region Army

            sw.WriteLine("[Army]");
            sw.WriteLine(";Int32");
            sw.WriteLine("PosX = " + pSettings.ArmyPositionX.ToString(CultureInfo.InvariantCulture));
            sw.WriteLine(";Int32");
            sw.WriteLine("PosY = " + pSettings.ArmyPositionY.ToString(CultureInfo.InvariantCulture));
            sw.WriteLine(";Int32");
            sw.WriteLine("Width = " + pSettings.ArmyWidth.ToString(CultureInfo.InvariantCulture));
            sw.WriteLine(";Int32");
            sw.WriteLine("Height = " + pSettings.ArmyHeight.ToString(CultureInfo.InvariantCulture));
            sw.WriteLine(";Boolean");
            sw.WriteLine("Remove Ai = " + pSettings.ArmyRemoveAi);
            sw.WriteLine(";Boolean");
            sw.WriteLine("Remove Neutral = " + pSettings.ArmyRemoveNeutral);
            sw.WriteLine(";Boolean");
            sw.WriteLine("Remove Allie = " + pSettings.ArmyRemoveAllie);
            sw.WriteLine(";Boolean");
            sw.WriteLine("Remove Localplayer = " + pSettings.ArmyRemoveLocalplayer);
            sw.WriteLine(";String");
            sw.WriteLine("FontName = " + pSettings.ArmyFontName);
            sw.WriteLine(";float");
            sw.WriteLine("Opacity = " + pSettings.ArmyOpacity);
            sw.WriteLine(";String");
            sw.WriteLine("On/Off Text = " + pSettings.ArmyTogglePanel);
            sw.WriteLine(";String");
            sw.WriteLine("Change Position Text = " + pSettings.ArmyChangePositionPanel);
            sw.WriteLine(";String");
            sw.WriteLine("Change Size Text = " + pSettings.ArmyChangeSizePanel);
            sw.WriteLine(";Keys");
            sw.WriteLine("Hotkey 1 = " + pSettings.ArmyHotkey1);
            sw.WriteLine(";Keys");
            sw.WriteLine("Hotkey 2 = " + pSettings.ArmyHotkey2);
            sw.WriteLine(";Keys");
            sw.WriteLine("Hotkey 3 = " + pSettings.ArmyHotkey3);
            sw.WriteLine(";Boolean");
            sw.WriteLine("Draw Background = " + pSettings.ArmyDrawBackground);

            #endregion


            sw.WriteLine("");
            sw.WriteLine("");

            #region Worker

            sw.WriteLine("[Worker]");
            sw.WriteLine(";Int32");
            sw.WriteLine("PosX = " + pSettings.WorkerPositionX.ToString(CultureInfo.InvariantCulture));
            sw.WriteLine(";Int32");
            sw.WriteLine("PosY = " + pSettings.WorkerPositionY.ToString(CultureInfo.InvariantCulture));
            sw.WriteLine(";Int32");
            sw.WriteLine("Width = " + pSettings.WorkerWidth.ToString(CultureInfo.InvariantCulture));
            sw.WriteLine(";Int32");
            sw.WriteLine("Height = " + pSettings.WorkerHeight.ToString(CultureInfo.InvariantCulture));
            sw.WriteLine(";String");
            sw.WriteLine("FontName = " + pSettings.WorkerFontName);
            sw.WriteLine(";float");
            sw.WriteLine("Opacity = " + pSettings.WorkerOpacity);
            sw.WriteLine(";String");
            sw.WriteLine("On/Off Text = " + pSettings.WorkerTogglePanel);
            sw.WriteLine(";String");
            sw.WriteLine("Change Position Text = " + pSettings.WorkerChangePositionPanel);
            sw.WriteLine(";String");
            sw.WriteLine("Change Size Text = " + pSettings.WorkerChangeSizePanel);
            sw.WriteLine(";Keys");
            sw.WriteLine("Hotkey 1 = " + pSettings.WorkerHotkey1);
            sw.WriteLine(";Keys");
            sw.WriteLine("Hotkey 2 = " + pSettings.WorkerHotkey2);
            sw.WriteLine(";Keys");
            sw.WriteLine("Hotkey 3 = " + pSettings.WorkerHotkey3);
            sw.WriteLine(";Boolean");
            sw.WriteLine("Draw Background = " + pSettings.WorkerDrawBackground);

            #endregion


            sw.WriteLine("");
            sw.WriteLine("");

            #region UnitTab

            sw.WriteLine("[UnitTab]");
            sw.WriteLine(";Int32");
            sw.WriteLine("PosX = " + pSettings.UnitTabPositionX.ToString(CultureInfo.InvariantCulture));
            sw.WriteLine(";Int32");
            sw.WriteLine("PosY = " + pSettings.UnitTabPositionY.ToString(CultureInfo.InvariantCulture));
            sw.WriteLine(";Int32");
            sw.WriteLine("Width = " + pSettings.UnitTabWidth.ToString(CultureInfo.InvariantCulture));
            sw.WriteLine(";Int32");
            sw.WriteLine("Height = " + pSettings.UnitTabHeigth.ToString(CultureInfo.InvariantCulture));
            sw.WriteLine(";Boolean");
            sw.WriteLine("Remove Ai = " + pSettings.UnitTabRemoveAi);
            sw.WriteLine(";Boolean");
            sw.WriteLine("Remove Neutral = " + pSettings.UnitTabRemoveNeutral);
            sw.WriteLine(";Boolean");
            sw.WriteLine("Remove Allie = " + pSettings.UnitTabRemoveAllie);
            sw.WriteLine(";Boolean");
            sw.WriteLine("Remove Localplayer = " + pSettings.UnitTabRemoveLocalplayer);
            sw.WriteLine(";float");
            sw.WriteLine("Opacity = " + pSettings.UnitTabOpacity);
            sw.WriteLine(";String");
            sw.WriteLine("On/Off Text = " + pSettings.UnitTogglePanel);
            sw.WriteLine(";String");
            sw.WriteLine("Change Position Text = " + pSettings.UnitChangePositionPanel);
            sw.WriteLine(";String");
            sw.WriteLine("Change Size Text = " + pSettings.UnitChangeSizePanel);
            sw.WriteLine(";Keys");
            sw.WriteLine("Hotkey 1 = " + pSettings.UnitHotkey1);
            sw.WriteLine(";Keys");
            sw.WriteLine("Hotkey 2 = " + pSettings.UnitHotkey2);
            sw.WriteLine(";Keys");
            sw.WriteLine("Hotkey 3 = " + pSettings.UnitHotkey3);
            sw.WriteLine(";Boolean");
            sw.WriteLine("Split Buildings/ Units = " + pSettings.UnitTabSplitUnitsAndBuildings);

            #endregion


            sw.WriteLine("");
            sw.WriteLine("");

            #region Maphack

            sw.WriteLine("[Maphack]");
            sw.WriteLine(";Int32");
            sw.WriteLine("PosX = " + pSettings.MaphackPositionX.ToString(CultureInfo.InvariantCulture));
            sw.WriteLine(";Int32");
            sw.WriteLine("PosY = " + pSettings.MaphackPositionY.ToString(CultureInfo.InvariantCulture));
            sw.WriteLine(";Int32");
            sw.WriteLine("Width = " + pSettings.MaphackWidth.ToString(CultureInfo.InvariantCulture));
            sw.WriteLine(";Int32");
            sw.WriteLine("Height = " + pSettings.MaphackHeigth.ToString(CultureInfo.InvariantCulture));
            sw.WriteLine(";Boolean");
            sw.WriteLine("Remove Ai = " + pSettings.MaphackRemoveAi);
            sw.WriteLine(";Boolean");
            sw.WriteLine("Remove Neutral = " + pSettings.MaphackRemoveNeutral);
            sw.WriteLine(";Boolean");
            sw.WriteLine("Remove Allie = " + pSettings.MaphackRemoveAllie);
            sw.WriteLine(";Boolean");
            sw.WriteLine("Remove Localplayer = " + pSettings.MaphackRemoveLocalplayer);
            sw.WriteLine(";float");
            sw.WriteLine("Opacity = " + pSettings.MaphackOpacity);
            sw.WriteLine(";String");
            sw.WriteLine("On/Off Text = " + pSettings.MaphackTogglePanel);
            sw.WriteLine(";String");
            sw.WriteLine("Change Position Text = " + pSettings.MaphackChangePositionPanel);
            sw.WriteLine(";String");
            sw.WriteLine("Change Size Text = " + pSettings.MaphackChangeSizePanel);
            sw.WriteLine(";Keys");
            sw.WriteLine("Hotkey 1 = " + pSettings.MaphackHotkey1);
            sw.WriteLine(";Keys");
            sw.WriteLine("Hotkey 2 = " + pSettings.MaphackHotkey2);
            sw.WriteLine(";Keys");
            sw.WriteLine("Hotkey 3 = " + pSettings.MaphackHotkey3);
            sw.WriteLine(";Boolean");
            sw.WriteLine("Disable Destination Line = " + pSettings.MaphackDisableDestinationLine);
            sw.WriteLine(";Color");
            sw.WriteLine("Destination Line Color = " + ColorTranslator.ToHtml(pSettings.MaphackDestinationColor));
            sw.WriteLine(";Boolean");
            sw.WriteLine("Color Def Structures = " + pSettings.MaphackColorDefensivestructuresYellow);

            #endregion


            sw.WriteLine("");
            sw.WriteLine("");


            #region Maphack UnitIds

            sw.WriteLine("[Maphack Units]");

            if (pSettings.MaphackUnitIds == null)
            {
                sw.Close();
                return;
            }

            if (pSettings.MaphackUnitIds.Count <= 0)
            {
                sw.Close();
                return;
            }

            for (var i = 0; i < pSettings.MaphackUnitIds.Count; i++)
            {
                sw.WriteLine(";UnitId");
                sw.WriteLine("UnitId = " + pSettings.MaphackUnitIds[i]);
                sw.WriteLine(";Color");
                sw.WriteLine("Unit Color = " + ColorTranslator.ToHtml(pSettings.MaphackUnitColors[i]));
            }

            #endregion

                sw.Close();

        }

        public static String SetWindowTitle()
        {
            var rnd = new Random();

            var iNum = rnd.Next(0xFFF, 0xFFFFFFF);
            var strCompleteText = Crypting.CreateSha1(iNum.ToString(CultureInfo.InvariantCulture));

            return strCompleteText;
        }

        public static void EncryptEntireFile()
        {
            using (StreamReader sr = new StreamReader(Constants.StrPreferencesFile))
            using (StreamWriter sw = new StreamWriter(Constants.StrPreferencesFile + "-"))
            {
                while (!sr.EndOfStream)
                {
                    sw.WriteLine(Crypting.CreateXor(sr.ReadLine()));
                }
            }

            File.Replace(Constants.StrPreferencesFile +"-", Constants.StrPreferencesFile, Constants.StrPreferencesFile + "backup");
        }

        public class Help_Graphics
        {


            public static void DrawRoundedRectangle(Graphics g,
       Rectangle r, int d, Pen p)
            {

                System.Drawing.Drawing2D.GraphicsPath gp =
                        new System.Drawing.Drawing2D.GraphicsPath();

                gp.AddArc(r.X, r.Y, d, d, 180, 90);
                gp.AddArc(r.X + r.Width - d, r.Y, d, d, 270, 90);
                gp.AddArc(r.X + r.Width - d, r.Y + r.Height - d, d, d, 0, 90);
                gp.AddArc(r.X, r.Y + r.Height - d, d, d, 90, 90);
                gp.AddLine(r.X, r.Y + r.Height - d, r.X, r.Y + d / 2);

                g.DrawPath(p, gp);
            }
        }
    }
}
