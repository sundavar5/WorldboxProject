using System;
using System.Text.RegularExpressions;
using db;
using UnityEngine;

namespace WorldBoxConsole
{
	// Token: 0x02000854 RID: 2132
	public class ConsoleFormatter
	{
		// Token: 0x060042B0 RID: 17072 RVA: 0x001C3CD4 File Offset: 0x001C1ED4
		public static string logWarning(int pWarningNum, string pLogString)
		{
			string result;
			using (StringBuilderPool tLog = new StringBuilderPool())
			{
				tLog.AppendLine().Append("<color=yellow>--- warning[").Append(pWarningNum).Append("]: ---" + ConsoleFormatter.build + "</color>").AppendLine();
				foreach (string logStringItem in pLogString.Trim().Split('\n', StringSplitOptions.None))
				{
					tLog.Append("<b><color=cyan>").Append(logStringItem).Append("</color></b>").AppendLine();
				}
				result = tLog.ToString();
			}
			return result;
		}

		// Token: 0x060042B1 RID: 17073 RVA: 0x001C3D84 File Offset: 0x001C1F84
		public static string logError(int pErrorNum, string pLogString, string pStackTrace)
		{
			string result;
			using (StringBuilderPool tLog = new StringBuilderPool())
			{
				string tGameplayStateInfo = "";
				try
				{
					tGameplayStateInfo = ConsoleFormatter.getShortGameplayStateInfo();
				}
				catch (Exception)
				{
					tGameplayStateInfo = "(gameplay state crashed)";
				}
				foreach (string tGameplayStateInfoItem in tGameplayStateInfo.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries))
				{
					tLog.Append("<size=7><b><color=#df4ec8>").Append(tGameplayStateInfoItem).Append("</color></b></size>").AppendLine();
				}
				tLog.Append("<color=red>--- error[").Append(pErrorNum).Append("]: ---").Append(ConsoleFormatter.build).Append("</color>").AppendLine();
				foreach (string logStringItem in pLogString.Trim().Split('\n', StringSplitOptions.None))
				{
					tLog.Append("<b><color=cyan>").Append(logStringItem).Append("</color></b>").AppendLine();
				}
				if (!string.IsNullOrEmpty(pStackTrace.Trim(new char[]
				{
					'\n',
					' '
				})))
				{
					try
					{
						pStackTrace = ConsoleFormatter.formatStacktrace(pStackTrace);
					}
					catch (Exception)
					{
					}
					tLog.Append("<color=red>--- stack: ---").Append(ConsoleFormatter.build).Append("</color>").AppendLine().Append(pStackTrace).AppendLine();
				}
				result = tLog.ToString();
			}
			return result;
		}

		// Token: 0x060042B2 RID: 17074 RVA: 0x001C3F28 File Offset: 0x001C2128
		public static string addSystemInfo()
		{
			string result;
			using (StringBuilderPool tLog = new StringBuilderPool())
			{
				tLog.Append("-----------").AppendLine().Append("Game Version: <color=white>").Append(Application.version).Append("</color>");
				ConsoleFormatter.build = " " + Application.version;
				if (!string.IsNullOrEmpty(Config.versionCodeText))
				{
					tLog.Append(" (<color=white>").Append(Config.versionCodeText);
					if (!string.IsNullOrEmpty(Config.gitCodeText))
					{
						tLog.Append("@").Append(Config.gitCodeText);
					}
					tLog.Append("</color>)");
					ConsoleFormatter.build = ConsoleFormatter.build + " (" + Config.versionCodeText;
					if (!string.IsNullOrEmpty(Config.gitCodeText))
					{
						ConsoleFormatter.build = ConsoleFormatter.build + "@" + Config.gitCodeText;
					}
					ConsoleFormatter.build += ")";
				}
				ConsoleFormatter.build += " ---";
				tLog.AppendLine().Append("Modded: <color=white>").Append(Config.MODDED).Append("</color>").AppendLine().Append("operatingSystemFamily: <color=white>").Append(SystemInfo.operatingSystemFamily).Append("</color>").AppendLine().Append("deviceModel: <color=white>").Append(SystemInfo.deviceModel).Append("</color>").AppendLine().Append("deviceName: <color=white>").Append(SystemInfo.deviceName).Append("</color>").AppendLine().Append("deviceType: <color=white>").Append(SystemInfo.deviceType).Append("</color>").AppendLine().Append("systemMemorySize: <color=white>").Append(SystemInfo.systemMemorySize).Append("</color>").AppendLine().Append("graphicsDeviceID: <color=white>").Append(SystemInfo.graphicsDeviceID).Append("</color>").AppendLine().Append("Graphics.activeTier: <color=white>").Append(Graphics.activeTier.ToString()).Append("</color>").AppendLine().Append("GC.GetTotalMemory: <color=white>").Append((GC.GetTotalMemory(false) / 1000000L).ToString() + " mb").Append("</color>").AppendLine().Append("graphicsMemorySize: <color=white>").Append(SystemInfo.graphicsMemorySize).Append("</color>").AppendLine().Append("maxTextureSize: <color=white>").Append(SystemInfo.maxTextureSize).Append("</color>").AppendLine().Append("operatingSystem: <color=white>").Append(SystemInfo.operatingSystem).Append("</color>").AppendLine().Append("processorType: <color=white>").Append(SystemInfo.processorType).Append("</color>").AppendLine().Append("installMode: <color=white>").Append(Application.installMode).Append("</color>").AppendLine().Append("sandboxType: <color=white>").Append(Application.sandboxType).Append("</color>").AppendLine().Append("FPS: <color=white>").Append(FPS.fps).Append("</color>").AppendLine().Append("-----------");
				result = tLog.ToString();
			}
			return result;
		}

		// Token: 0x060042B3 RID: 17075 RVA: 0x001C42E4 File Offset: 0x001C24E4
		public static string logFormatter(string pLogString, string pColor = "white")
		{
			pLogString = pLogString.Trim(new char[]
			{
				' ',
				'\n'
			});
			if (pLogString != "" && ConsoleFormatter.HasDigit(pLogString) && !pLogString.Contains("<color"))
			{
				return ConsoleFormatter._regex.Replace(pLogString, "<color=" + pColor + ">$0</color>");
			}
			return pLogString;
		}

		// Token: 0x060042B4 RID: 17076 RVA: 0x001C4348 File Offset: 0x001C2548
		private static bool HasDigit(string pString)
		{
			for (int i = 0; i < pString.Length; i++)
			{
				if (char.IsDigit(pString[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060042B5 RID: 17077 RVA: 0x001C437C File Offset: 0x001C257C
		public static string formatStacktrace(string pStackTrace)
		{
			string[] tLines = pStackTrace.Split('\n', StringSplitOptions.None);
			for (int i = 0; i < tLines.Length; i++)
			{
				if (tLines[i].Contains("(at "))
				{
					string[] tSplitLine = tLines[i].Split(new string[]
					{
						" (at "
					}, StringSplitOptions.None);
					ConsoleFormatter.start = tSplitLine[0];
					ConsoleFormatter.end = tSplitLine[1].Substring(0, tSplitLine[1].Length - 1);
				}
				else
				{
					ConsoleFormatter.start = tLines[i];
					ConsoleFormatter.end = "";
				}
				if (ConsoleFormatter.start.Contains("("))
				{
					string[] startSplit = ConsoleFormatter.start.Split('(', StringSplitOptions.None);
					string tMethodName = startSplit[0];
					string tMethodParameters = startSplit[1].Substring(0, startSplit[1].Length - 1);
					char? tSplitChar = null;
					if (tMethodName.Contains(":"))
					{
						tSplitChar = new char?(':');
					}
					else if (tMethodName.Contains("."))
					{
						tSplitChar = new char?('.');
					}
					if (tSplitChar != null)
					{
						string[] tDotSplit = tMethodName.Split(tSplitChar.Value, StringSplitOptions.None);
						tDotSplit[tDotSplit.Length - 1] = "<b><color=cyan>" + tDotSplit[tDotSplit.Length - 1] + "</color></b>";
						tMethodName = string.Join(tSplitChar.Value.ToString(), tDotSplit);
					}
					if (tMethodParameters.Trim() != string.Empty)
					{
						string[] tCommaSplit;
						if (tMethodParameters.Contains(","))
						{
							tCommaSplit = tMethodParameters.Split(',', StringSplitOptions.None);
						}
						else
						{
							tCommaSplit = new string[]
							{
								tMethodParameters
							};
						}
						for (int j = 0; j < tCommaSplit.Length; j++)
						{
							string tSplitItem = tCommaSplit[j].Trim();
							if (tSplitItem.Contains(' '))
							{
								string[] array = tSplitItem.Split(' ', StringSplitOptions.None);
								string tTypeItem = array[0];
								if (tTypeItem.Contains("."))
								{
									string[] array2 = tTypeItem.Split('.', StringSplitOptions.None);
									tTypeItem = array2[array2.Length - 1];
								}
								string tVarItem = array[1];
								tCommaSplit[j] = string.Concat(new string[]
								{
									"<color=#FFCC1C>",
									tTypeItem,
									"</color> <b><color=cyan>",
									tVarItem,
									"</color></b>"
								});
							}
							else
							{
								tCommaSplit[j] = "<color=#FFCC1C>" + tSplitItem + "</color>";
							}
							tMethodParameters = string.Join(", ", tCommaSplit);
						}
					}
					ConsoleFormatter.start = tMethodName + "(" + tMethodParameters + ")";
					while (ConsoleFormatter.start.Contains("System."))
					{
						ConsoleFormatter.start = ConsoleFormatter.start.Replace("System.", string.Empty);
					}
				}
				if (ConsoleFormatter.end != string.Empty)
				{
					if (ConsoleFormatter.end.Contains("BuiltInPackages/"))
					{
						ConsoleFormatter.end = ConsoleFormatter.end.Split(new string[]
						{
							"BuiltInPackages/"
						}, StringSplitOptions.None)[1];
					}
					if (ConsoleFormatter.end.Contains("unity/build/"))
					{
						ConsoleFormatter.end = ConsoleFormatter.end.Split(new string[]
						{
							"unity/build/"
						}, StringSplitOptions.None)[1];
					}
					if (ConsoleFormatter.end.Contains("Unity.app/"))
					{
						ConsoleFormatter.end = ConsoleFormatter.end.Split(new string[]
						{
							"Unity.app/"
						}, StringSplitOptions.None)[1];
					}
					if (ConsoleFormatter.end.Contains("Export/"))
					{
						ConsoleFormatter.end = ConsoleFormatter.end.Split(new string[]
						{
							"Export/"
						}, StringSplitOptions.None)[1];
					}
					if (ConsoleFormatter.end.Contains("github/workspace/"))
					{
						ConsoleFormatter.end = ConsoleFormatter.end.Split(new string[]
						{
							"github/workspace/"
						}, StringSplitOptions.None)[1];
					}
					if (ConsoleFormatter.end.Contains(":"))
					{
						string[] tEndSplit = ConsoleFormatter.end.Split(':', StringSplitOptions.None);
						string[] tFiles = tEndSplit[tEndSplit.Length - 2].Split('/', StringSplitOptions.None);
						tFiles[tFiles.Length - 1] = "<size=7><b><color=cyan>" + tFiles[tFiles.Length - 1] + "</color></b></size>";
						tEndSplit[tEndSplit.Length - 2] = string.Join("/", tFiles);
						tEndSplit[tEndSplit.Length - 1] = "<size=7><b><color=cyan>" + tEndSplit[tEndSplit.Length - 1] + "</color></b></size>";
						ConsoleFormatter.end = string.Join(":", tEndSplit);
					}
					ConsoleFormatter.end = "<size=5> (at " + ConsoleFormatter.end + ")</size>";
				}
				tLines[i] = "<size=7>" + ConsoleFormatter.start + "</size>" + ConsoleFormatter.end;
			}
			pStackTrace = string.Join("\n", tLines);
			return pStackTrace;
		}

		// Token: 0x060042B6 RID: 17078 RVA: 0x001C47F4 File Offset: 0x001C29F4
		private static string getShortGameplayStateInfo()
		{
			MapBox tWorld = MapBox.instance;
			if (tWorld == null)
			{
				return "(world not loaded)";
			}
			WindowStats tWindowInfo = Config.debug_window_stats;
			QualityChanger quality_changer = tWorld.quality_changer;
			bool? tLowRes = (quality_changer != null) ? new bool?(quality_changer.isLowRes()) : null;
			PowerButtonSelector instance = PowerButtonSelector.instance;
			string text;
			if (instance == null)
			{
				text = null;
			}
			else
			{
				PowerButton selectedButton = instance.selectedButton;
				if (selectedButton == null)
				{
					text = null;
				}
				else
				{
					GodPower godPower = selectedButton.godPower;
					text = ((godPower != null) ? godPower.id : null);
				}
			}
			string tSelectedPower = text;
			string tLastSelectedPower = Config.debug_last_selected_power_button;
			bool tSelectedUnit = SelectedUnit.isSet();
			bool tControlling = ControllableUnit.isControllingUnit();
			WorldTimeScaleAsset time_scale_asset = Config.time_scale_asset;
			string tGameSpeed = ((time_scale_asset != null) ? time_scale_asset.id : null) ?? "null";
			int tWorldsLoaded = Config.debug_worlds_loaded;
			string result;
			using (StringBuilderPool tLog = new StringBuilderPool())
			{
				tLog.Append("spd: <H>" + tGameSpeed + "</H>");
				if (!string.IsNullOrEmpty(tSelectedPower) || !string.IsNullOrEmpty(tLastSelectedPower))
				{
					tLog.Append(", ");
					tLog.Append("pow: <H>" + (tSelectedPower ?? "none") + "</H>");
					if (tLastSelectedPower != tSelectedPower)
					{
						tLog.Append(" last: <H>" + (tLastSelectedPower ?? "none") + "</H>");
					}
				}
				tLog.Append(", ");
				tLog.Append("zoom: <H>");
				tLog.Append((tLowRes != null) ? (tLowRes.Value ? "map" : "full") : "null");
				tLog.Append("</H>");
				tLog.Append(", ");
				tLog.Append(string.Concat(new string[]
				{
					"win: <H>",
					tWindowInfo.current ?? "none",
					"</H> (<H>",
					tWindowInfo.previous ?? "none",
					"</H>)"
				}));
				tLog.Append(string.Format(" (o:{0},c:{1},s:{2},h:{3})", new object[]
				{
					tWindowInfo.opens,
					tWindowInfo.closes,
					tWindowInfo.shows,
					tWindowInfo.hides
				}));
				tLog.Append(", ");
				tLog.Append(string.Format("worlds: {0}", tWorldsLoaded));
				tLog.Append(", ");
				tLog.Append(string.Format("modded: <H>{0}</H>", Config.MODDED));
				tLog.Append(", ");
				tLog.Append(string.Format("db pend: <H>{0}</H>", DBInserter.hasCommands()));
				tLog.AppendLine();
				using (StringBuilderPool tManagerLine = new StringBuilderPool())
				{
					foreach (BaseSystemManager baseSystemManager in MapBox.instance.list_all_sim_managers)
					{
						string tDebug = baseSystemManager.debugShort();
						if (!string.IsNullOrEmpty(tDebug))
						{
							if (tManagerLine.Length > 0)
							{
								tManagerLine.Append(", ");
							}
							tManagerLine.Append(tDebug);
							if (tManagerLine.Length > 78)
							{
								tLog.Append(tManagerLine.ToString());
								tLog.AppendLine();
								tManagerLine.Clear();
							}
						}
					}
					if (tManagerLine.Length > 0)
					{
						tLog.Append(tManagerLine.ToString());
						tLog.AppendLine();
					}
					using (StringBuilderPool tSelLine = new StringBuilderPool())
					{
						if (tSelectedUnit)
						{
							Actor unit = SelectedUnit.unit;
							string text2;
							if (unit == null)
							{
								text2 = null;
							}
							else
							{
								ActorAsset asset = unit.asset;
								text2 = ((asset != null) ? asset.id : null);
							}
							string tSelectedUnitID = text2;
							tSelLine.Append("selected: <H>" + tSelectedUnitID + "</H>");
							if (SelectedUnit.multipleSelected())
							{
								int tCountSelected = SelectedUnit.countSelected();
								tSelLine.Append(string.Format(" ({0})", tCountSelected));
							}
						}
						if (tControlling)
						{
							if (tSelLine.Length > 0)
							{
								tSelLine.Append(", ");
							}
							Actor controllableUnit = ControllableUnit.getControllableUnit();
							string text3;
							if (controllableUnit == null)
							{
								text3 = null;
							}
							else
							{
								ActorAsset asset2 = controllableUnit.asset;
								text3 = ((asset2 != null) ? asset2.id : null);
							}
							string tControlledID = text3;
							int tCountControlling = ControllableUnit.count();
							tSelLine.Append("controlling: <H>" + tControlledID + "</H>");
							if (tCountControlling > 1)
							{
								tSelLine.Append(string.Format(" ({0})", tCountControlling));
							}
						}
						if (tSelLine.Length > 0)
						{
							tLog.Append(tSelLine.ToString());
							tLog.AppendLine();
						}
						result = ConsoleFormatter.logFormatter(tLog.ToString(), "yellow").Replace("<H>", "<color=yellow>").Replace("</H>", "</color>");
					}
				}
			}
			return result;
		}

		// Token: 0x060042B7 RID: 17079 RVA: 0x001C4D20 File Offset: 0x001C2F20
		private static string getWindowInfo()
		{
			if (!ScrollWindow.isWindowActive())
			{
				return Config.debug_last_window;
			}
			ScrollWindow currentWindow = ScrollWindow.getCurrentWindow();
			if (currentWindow == null)
			{
				return null;
			}
			return currentWindow.screen_id;
		}

		// Token: 0x040030DA RID: 12506
		private static string log;

		// Token: 0x040030DB RID: 12507
		private static string start;

		// Token: 0x040030DC RID: 12508
		private static string end;

		// Token: 0x040030DD RID: 12509
		private static string build = "";

		// Token: 0x040030DE RID: 12510
		private static Regex _regex = new Regex("[\\d\\.]+");
	}
}
