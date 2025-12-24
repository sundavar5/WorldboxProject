using System;
using System.Collections.Concurrent;
using System.IO;
using UnityEngine;
using WorldBoxConsole;

// Token: 0x0200053C RID: 1340
public class LogHandler : MonoBehaviour
{
	// Token: 0x06002BD6 RID: 11222 RVA: 0x0015A454 File Offset: 0x00158654
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
	public static void init()
	{
		if (LogHandler._init_handler)
		{
			return;
		}
		LogHandler._init_handler = true;
		if (!Application.isEditor)
		{
			Application.SetStackTraceLogType(LogType.Log, StackTraceLogType.None);
			Application.SetStackTraceLogType(LogType.Warning, StackTraceLogType.ScriptOnly);
			Application.SetStackTraceLogType(LogType.Error, StackTraceLogType.ScriptOnly);
		}
		Application.logMessageReceivedThreaded += LogHandler.HandleLog;
		Application.logMessageReceivedThreaded += Console.HandleLog;
		if (!Directory.Exists(LogHandler.getDirPath()))
		{
			Directory.CreateDirectory(LogHandler.getDirPath());
		}
	}

	// Token: 0x06002BD7 RID: 11223 RVA: 0x0015A4C4 File Offset: 0x001586C4
	[RuntimeInitializeOnLoadMethod]
	public static void initInstance()
	{
		if (LogHandler._init_instance)
		{
			return;
		}
		LogHandler._init_instance = true;
		if (LogHandler._instance == null)
		{
			GameObject gameObject = new GameObject("[LogHandler]");
			LogHandler._instance = gameObject.AddComponent<LogHandler>();
			Object.DontDestroyOnLoad(gameObject);
			gameObject.hideFlags = HideFlags.DontSave;
		}
	}

	// Token: 0x06002BD8 RID: 11224 RVA: 0x0015A504 File Offset: 0x00158704
	private void Update()
	{
		LogItem tLogItem;
		while (LogHandler.log_queue.TryDequeue(out tLogItem))
		{
			LogHandler.ProcessLog(tLogItem.log, tLogItem.stack_trace, tLogItem.type);
		}
	}

	// Token: 0x06002BD9 RID: 11225 RVA: 0x0015A538 File Offset: 0x00158738
	private static void HandleLog(string pLogString, string pStackTrace, LogType pLogType)
	{
		if (ThreadHelper.isMainThread())
		{
			LogHandler.ProcessLog(pLogString, pStackTrace, pLogType);
			return;
		}
		LogHandler.log_queue.Enqueue(new LogItem(pLogString, pStackTrace, pLogType));
	}

	// Token: 0x06002BDA RID: 11226 RVA: 0x0015A55C File Offset: 0x0015875C
	private static void ProcessLog(string pLogString, string pStackTrace, LogType pLogType)
	{
		pLogString = pLogString.Trim(new char[]
		{
			' ',
			'\n'
		});
		if (pLogType != LogType.Error && pLogType != LogType.Exception && pLogType != LogType.Assert)
		{
			LogHandler.clearRepeat();
			LogHandler.log = LogHandler.log + "- trace: " + pLogString + "\n";
			return;
		}
		if (LogHandler.errorNum > 100)
		{
			return;
		}
		LogHandler.log = "";
		if (LogHandler.errorNum == 0)
		{
			LogHandler.log = LogHandler.log + "Game Version: " + Application.version;
			if (!string.IsNullOrEmpty(Config.versionCodeText))
			{
				LogHandler.log = LogHandler.log + " (" + Config.versionCodeText;
				if (!string.IsNullOrEmpty(Config.gitCodeText))
				{
					LogHandler.log = LogHandler.log + "@" + Config.gitCodeText;
				}
				LogHandler.log += ")";
			}
			LogHandler.log = LogHandler.log + "\nModded: " + Config.MODDED.ToString();
			LogHandler.log = LogHandler.log + "\noperatingSystemFamily: " + SystemInfo.operatingSystemFamily.ToString();
			LogHandler.log = LogHandler.log + "\ndeviceModel: " + SystemInfo.deviceModel;
			LogHandler.log = LogHandler.log + "\ndeviceName: " + SystemInfo.deviceName;
			LogHandler.log = LogHandler.log + "\ndeviceType: " + SystemInfo.deviceType.ToString();
			LogHandler.log = LogHandler.log + "\nsystemMemorySize: " + SystemInfo.systemMemorySize.ToString();
			LogHandler.log = LogHandler.log + "\ngraphicsDeviceID: " + SystemInfo.graphicsDeviceID.ToString();
			LogHandler.log = LogHandler.log + "\ngraphicsActiveTier: " + Graphics.activeTier.ToString();
			LogHandler.log = LogHandler.log + "\nGC.GetTotalMemory: " + (GC.GetTotalMemory(false) / 1000000L).ToString() + " mb";
			LogHandler.log = LogHandler.log + "\ngraphicsMemorySize: " + SystemInfo.graphicsMemorySize.ToString();
			LogHandler.log = LogHandler.log + "\nmaxTextureSize: " + SystemInfo.maxTextureSize.ToString();
			LogHandler.log = LogHandler.log + "\noperatingSystem: " + SystemInfo.operatingSystem;
			LogHandler.log = LogHandler.log + "\nprocessorType: " + SystemInfo.processorType;
			LogHandler.log = LogHandler.log + "\ninstallMode: " + Application.installMode.ToString();
			LogHandler.log = LogHandler.log + "\nsandboxType: " + Application.sandboxType.ToString();
			try
			{
				if (Input.anyKey)
				{
					string tMods = "";
					if (HotkeyLibrary.isHoldingAlt())
					{
						tMods += "Alt ";
					}
					if (HotkeyLibrary.isHoldingControlForSelection())
					{
						tMods += "Ctrl ";
					}
					if (HotkeyLibrary.isHoldingAnyMod())
					{
						tMods += "Mod ";
					}
					LogHandler.log = string.Concat(new string[]
					{
						LogHandler.log,
						"\nkeyboard: ",
						Input.anyKey.ToString(),
						" ",
						Input.anyKeyDown.ToString(),
						" ",
						Input.inputString,
						" ",
						tMods
					});
					if (Input.mousePresent)
					{
						string tMouse0State = Input.GetMouseButton(0) ? "press0" : (Input.GetMouseButtonDown(0) ? "down0" : (Input.GetMouseButtonUp(0) ? "up0" : "none1"));
						string tMouse1State = Input.GetMouseButton(1) ? "press1" : (Input.GetMouseButtonDown(1) ? "down1" : (Input.GetMouseButtonUp(1) ? "up1" : "none1"));
						string tMouse2State = Input.GetMouseButton(2) ? "press2" : (Input.GetMouseButtonDown(2) ? "down2" : (Input.GetMouseButtonUp(2) ? "up2" : "none2"));
						string tMouseLocation = Input.mousePosition.ToString();
						LogHandler.log = string.Concat(new string[]
						{
							LogHandler.log,
							"\nmouse: ",
							tMouseLocation,
							" ",
							tMouse0State,
							" ",
							tMouse1State,
							" ",
							tMouse2State
						});
					}
				}
			}
			catch (Exception)
			{
			}
			LogHandler.log = LogHandler.log + "\nFPS: " + FPS.fps.ToString();
			LogHandler.log += "\n-----------\n\n";
		}
		if (!pStackTrace.AsSpan().Trim().IsEmpty && pStackTrace == LogHandler.lastError)
		{
			LogHandler.errorRepeated++;
			return;
		}
		if (pStackTrace.AsSpan().Trim().IsEmpty && pLogString == LogHandler.lastError)
		{
			LogHandler.errorRepeated++;
			return;
		}
		LogHandler.clearRepeat();
		LogHandler.log = string.Concat(new string[]
		{
			LogHandler.log,
			"- error[",
			LogHandler.errorNum.ToString(),
			"]: ",
			pLogString,
			"\n"
		});
		LogHandler.log = LogHandler.log + "- stack:\n" + pStackTrace + "\n";
		LogHandler.lastError = pStackTrace;
		File.AppendAllText(LogHandler.getPath(), LogHandler.log);
		LogHandler.errorNum++;
		LogHandler.openConsole();
	}

	// Token: 0x06002BDB RID: 11227 RVA: 0x0015AB3C File Offset: 0x00158D3C
	private static void openConsole()
	{
		if (!Config.show_console_on_error)
		{
			return;
		}
		if (World.world != null && World.world.console != null && !LogHandler.toggledConsole)
		{
			LogHandler.toggledConsole = true;
			World.world.console.Show();
		}
	}

	// Token: 0x06002BDC RID: 11228 RVA: 0x0015AB8C File Offset: 0x00158D8C
	private static void clearRepeat()
	{
		if (LogHandler.errorRepeated > 0)
		{
			LogHandler.log = LogHandler.log + "- last error repeated " + LogHandler.errorRepeated.ToString() + " times\n";
			LogHandler.lastError = "";
			LogHandler.errorRepeated = 0;
		}
	}

	// Token: 0x06002BDD RID: 11229 RVA: 0x0015ABC9 File Offset: 0x00158DC9
	public static string getDirPath()
	{
		return Application.persistentDataPath + LogHandler.folder_base;
	}

	// Token: 0x06002BDE RID: 11230 RVA: 0x0015ABDA File Offset: 0x00158DDA
	private static string getPath()
	{
		if (LogHandler._filename == null)
		{
			LogHandler._filename = LogHandler.getFileName();
		}
		return LogHandler._filename;
	}

	// Token: 0x06002BDF RID: 11231 RVA: 0x0015ABF4 File Offset: 0x00158DF4
	private static string getFileName()
	{
		string tDateTimeNow = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");
		return string.Concat(new string[]
		{
			LogHandler.getDirPath(),
			LogHandler.dataName,
			"_",
			tDateTimeNow,
			".log"
		});
	}

	// Token: 0x040021AD RID: 8621
	private static string folder_base = "/logs";

	// Token: 0x040021AE RID: 8622
	private static string dataName = "/error";

	// Token: 0x040021AF RID: 8623
	public static string log = "";

	// Token: 0x040021B0 RID: 8624
	internal static int errorNum = 0;

	// Token: 0x040021B1 RID: 8625
	private static string lastError = "";

	// Token: 0x040021B2 RID: 8626
	private static int errorRepeated = 0;

	// Token: 0x040021B3 RID: 8627
	private static bool _init_handler = false;

	// Token: 0x040021B4 RID: 8628
	private static bool _init_instance = false;

	// Token: 0x040021B5 RID: 8629
	private static bool toggledConsole = false;

	// Token: 0x040021B6 RID: 8630
	private static string _filename = null;

	// Token: 0x040021B7 RID: 8631
	private static ConcurrentQueue<LogItem> log_queue = new ConcurrentQueue<LogItem>();

	// Token: 0x040021B8 RID: 8632
	private static LogHandler _instance;
}
