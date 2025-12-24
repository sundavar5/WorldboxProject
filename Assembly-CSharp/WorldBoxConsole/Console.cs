using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace WorldBoxConsole
{
	// Token: 0x02000853 RID: 2131
	public class Console : MonoBehaviour
	{
		// Token: 0x0600429D RID: 17053 RVA: 0x001C337C File Offset: 0x001C157C
		private void Awake()
		{
			this._text_group = (base.transform.Find("Scroll View/Viewport/Content") as RectTransform);
			this._prefab = this._text_group.Find("CText").GetComponent<Text>();
			this._prefab.text = "";
			this._pool_texts = new ObjectPoolGenericMono<Text>(this._prefab, this._text_group);
			this._prefab.gameObject.SetActive(false);
		}

		// Token: 0x0600429E RID: 17054 RVA: 0x001C33F8 File Offset: 0x001C15F8
		private void addText()
		{
			Text tText = this._pool_texts.getNext();
			tText.name = "CText " + (this._text_obj.Count + 1).ToString();
			RectTransform component = tText.GetComponent<RectTransform>();
			component.localScale = Vector3.one;
			component.localPosition = Vector3.zero;
			this._text_obj.Add(tText);
			this.truncateGameObjects();
		}

		// Token: 0x0600429F RID: 17055 RVA: 0x001C3464 File Offset: 0x001C1664
		private void truncateGameObjects()
		{
			while (this._text_obj.Count > 2500)
			{
				this._text_obj[0].text = "";
				this._pool_texts.release(this._text_obj[0], true);
				this._text_obj.RemoveAt(0);
			}
		}

		// Token: 0x060042A0 RID: 17056 RVA: 0x001C34BF File Offset: 0x001C16BF
		internal static bool hasErrors()
		{
			return Console._error_num > 0;
		}

		// Token: 0x060042A1 RID: 17057 RVA: 0x001C34C9 File Offset: 0x001C16C9
		private static void truncateTexts()
		{
			if (Console._texts.Count <= 2500)
			{
				return;
			}
			while (Console._texts.Count > 2500)
			{
				Console._texts.Dequeue();
			}
			Console._line_num = 0;
		}

		// Token: 0x060042A2 RID: 17058 RVA: 0x001C34FF File Offset: 0x001C16FF
		private void OnEnable()
		{
			if (!Config.game_loaded)
			{
				return;
			}
			Console._line_num = 0;
			this._text_group.SetBottom(0f);
		}

		// Token: 0x060042A3 RID: 17059 RVA: 0x001C3520 File Offset: 0x001C1720
		private void OnDisable()
		{
			Console._line_num = 0;
			foreach (Text text in this._text_obj)
			{
				text.text = "";
			}
			this._pool_texts.clear(true);
			this._text_obj.Clear();
		}

		// Token: 0x060042A4 RID: 17060 RVA: 0x001C3594 File Offset: 0x001C1794
		public void Toggle()
		{
			if (base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(false);
				return;
			}
			base.gameObject.SetActive(true);
		}

		// Token: 0x060042A5 RID: 17061 RVA: 0x001C35BC File Offset: 0x001C17BC
		public void Hide()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x060042A6 RID: 17062 RVA: 0x001C35CA File Offset: 0x001C17CA
		public void Show()
		{
			base.gameObject.SetActive(true);
		}

		// Token: 0x060042A7 RID: 17063 RVA: 0x001C35D8 File Offset: 0x001C17D8
		public bool isActive()
		{
			return base.gameObject.activeSelf;
		}

		// Token: 0x060042A8 RID: 17064 RVA: 0x001C35E5 File Offset: 0x001C17E5
		public static void HandleLog(string pLogString, string pStackTrace, LogType pLogType)
		{
			if (ThreadHelper.isMainThread())
			{
				Console.ProcessLog(pLogString, pStackTrace, pLogType, DateTime.Now);
				return;
			}
			Console.log_queue.Enqueue(new LogItem(pLogString, pStackTrace, pLogType));
		}

		// Token: 0x060042A9 RID: 17065 RVA: 0x001C3610 File Offset: 0x001C1810
		public static void ProcessLog(string pLogString, string pStackTrace, LogType pLogType, DateTime pTime)
		{
			if (pLogString.Contains("FIRAPP_DEFAULT"))
			{
				return;
			}
			if (pLogString.Length > 256 && !pLogString.Contains("</"))
			{
				string[] tSplit = pLogString.Split('\n', StringSplitOptions.None);
				using (StringBuilderPool tBuilder = new StringBuilderPool())
				{
					for (int i = 0; i < tSplit.Length; i++)
					{
						while (tSplit[i].Length > 256)
						{
							tBuilder.Append(tSplit[i].Substring(0, 256));
							tBuilder.Append('\n');
							tSplit[i] = tSplit[i].Substring(256);
						}
						tBuilder.Append(tSplit[i]);
						tBuilder.Append('\n');
					}
					pLogString = tBuilder.ToString();
				}
			}
			pLogString = pLogString.Trim(new char[]
			{
				' ',
				'\n'
			});
			pLogString = ConsoleFormatter.logFormatter(pLogString, "white");
			switch (pLogType)
			{
			case LogType.Error:
			case LogType.Assert:
			case LogType.Exception:
				if (Console._error_num == 0)
				{
					Console._texts.Enqueue(ConsoleFormatter.addSystemInfo().Trim(new char[]
					{
						'\n',
						' '
					}));
				}
				if (Console._previous_errors.ContainsKey(pLogString))
				{
					Dictionary<string, int> previous_errors = Console._previous_errors;
					string key = pLogString;
					int num = previous_errors[key];
					previous_errors[key] = num + 1;
					Console._error_repeated++;
					Console._log.Clear();
					return;
				}
				Console.clearRepeat();
				if (!Console._stacks.Add(pStackTrace))
				{
					pStackTrace = "";
				}
				Console._log.Append(ConsoleFormatter.logError(Console._error_num, pLogString, pStackTrace));
				Console._previous_errors.Add(pLogString, 1);
				Console._error_num++;
				goto IL_230;
			case LogType.Warning:
				if (Console._previous_warnings.ContainsKey(pLogString))
				{
					Dictionary<string, int> previous_warnings = Console._previous_warnings;
					string key = pLogString;
					int num = previous_warnings[key];
					previous_warnings[key] = num + 1;
					Console._warning_repeated++;
					Console._log.Clear();
					return;
				}
				Console.clearRepeat();
				Console._log.Append(ConsoleFormatter.logWarning(Console._warning_num, pLogString));
				Console._previous_warnings.Add(pLogString, 1);
				Console._warning_num++;
				goto IL_230;
			}
			Console.clearRepeat();
			Console._log.Append(pLogString);
			IL_230:
			Console.PrependTime(Console._log, pTime);
			Console._texts.Enqueue(Console._log.ToString().Trim(new char[]
			{
				'\n',
				' '
			}));
			Console._log.Clear();
			Console.truncateTexts();
		}

		// Token: 0x060042AA RID: 17066 RVA: 0x001C38A4 File Offset: 0x001C1AA4
		private static void clearRepeat()
		{
			if (Console._error_repeated > 0)
			{
				Console._texts.Enqueue("<color=red>( previous errors repeated " + Console._error_repeated.ToString() + " times )</color>");
				if (Console._error_repeated > 10)
				{
					Console._texts.Enqueue("<color=red>YOU SHOULD RESTART THE GAME</color>");
				}
				Console._error_repeated = 0;
			}
			if (Console._warning_repeated > 0)
			{
				Console._texts.Enqueue("<color=yellow>( previous warning repeated " + Console._warning_repeated.ToString() + " times )</color>");
				Console._warning_repeated = 0;
			}
		}

		// Token: 0x060042AB RID: 17067 RVA: 0x001C392C File Offset: 0x001C1B2C
		private void Update()
		{
			LogItem tLogItem;
			while (Console.log_queue.TryDequeue(out tLogItem))
			{
				Console.ProcessLog(tLogItem.log, tLogItem.stack_trace, tLogItem.type, tLogItem.time);
			}
			if (Console._line_num != Console._texts.Count + Console._error_repeated + Console._warning_repeated)
			{
				string tFullText = string.Join<string>('\n', Console._texts).Trim(new char[]
				{
					'\n',
					' '
				});
				if (Console._error_repeated > 0)
				{
					tFullText = tFullText + "\n<color=red>( previous errors repeated " + Console._error_repeated.ToString() + " times )</color>";
					if (Console._error_repeated > 10)
					{
						tFullText += "\n<color=red>YOU SHOULD RESTART THE GAME</color>";
					}
				}
				else if (Console._warning_repeated > 0)
				{
					tFullText = tFullText + "\n<color=yellow>( previous warning repeated " + Console._warning_repeated.ToString() + " times )</color>";
				}
				string[] tLines = tFullText.Split('\n', StringSplitOptions.None);
				if (tLines.Length > 25000)
				{
					string[] tTruncatedLines = new string[25000];
					Array.Copy(tLines, tLines.Length - 25000, tTruncatedLines, 0, 25000);
					tLines = tTruncatedLines;
				}
				int tCurNumber = -1;
				for (int i = 0; i < tLines.Length; i++)
				{
					int tThisText = Mathf.CeilToInt((float)(i + 1) / 10f) - 1;
					for (int j = this._text_obj.Count; j < tThisText + 1; j++)
					{
						this.addText();
					}
					Text tTextObj = this._text_obj[tThisText];
					if (tThisText != tCurNumber)
					{
						tTextObj.text = "";
						tCurNumber = tThisText;
					}
					Text text = tTextObj;
					text.text = text.text + "\n" + tLines[i].Trim(new char[]
					{
						'\n',
						' '
					});
					tTextObj.text = tTextObj.text.Trim(new char[]
					{
						'\n',
						' '
					});
				}
				Console._line_num = Console._texts.Count + Console._error_repeated + Console._warning_repeated;
			}
		}

		// Token: 0x060042AC RID: 17068 RVA: 0x001C3B24 File Offset: 0x001C1D24
		public static void PrependTime(StringBuilder pStringBuilder, DateTime pDateTime)
		{
			using (StringBuilderPool tStringBuilder = new StringBuilderPool())
			{
				tStringBuilder.Append("[").Append("<color=white>").Append((pDateTime.Hour < 10) ? "0" : "").Append(pDateTime.Hour).Append("</color>").Append(':').Append("<color=white>").Append((pDateTime.Minute < 10) ? "0" : "").Append(pDateTime.Minute).Append("</color>").Append(':').Append("<color=white>").Append((pDateTime.Second < 10) ? "0" : "").Append(pDateTime.Second).Append("</color>").Append("] ");
				pStringBuilder.Insert(0, tStringBuilder.string_builder);
			}
		}

		// Token: 0x060042AD RID: 17069 RVA: 0x001C3C3C File Offset: 0x001C1E3C
		public void openLogsFolder()
		{
			Application.OpenURL("file://" + Application.persistentDataPath);
		}

		// Token: 0x040030C8 RID: 12488
		private const int MAX_ELEMENTS = 2500;

		// Token: 0x040030C9 RID: 12489
		private const int MAX_LINES = 25000;

		// Token: 0x040030CA RID: 12490
		private const int MAX_CHARS_PER_LINE = 256;

		// Token: 0x040030CB RID: 12491
		private static int _line_num = 0;

		// Token: 0x040030CC RID: 12492
		private static int _warning_num = 0;

		// Token: 0x040030CD RID: 12493
		private static int _error_num = 0;

		// Token: 0x040030CE RID: 12494
		private Text _prefab;

		// Token: 0x040030CF RID: 12495
		private static Queue<string> _texts = new Queue<string>(2500);

		// Token: 0x040030D0 RID: 12496
		private static StringBuilder _log = new StringBuilder();

		// Token: 0x040030D1 RID: 12497
		private static int _error_repeated = 0;

		// Token: 0x040030D2 RID: 12498
		private static int _warning_repeated = 0;

		// Token: 0x040030D3 RID: 12499
		private static Dictionary<string, int> _previous_errors = new Dictionary<string, int>();

		// Token: 0x040030D4 RID: 12500
		private static Dictionary<string, int> _previous_warnings = new Dictionary<string, int>();

		// Token: 0x040030D5 RID: 12501
		private static HashSet<string> _stacks = new HashSet<string>();

		// Token: 0x040030D6 RID: 12502
		private RectTransform _text_group;

		// Token: 0x040030D7 RID: 12503
		private List<Text> _text_obj = new List<Text>();

		// Token: 0x040030D8 RID: 12504
		private ObjectPoolGenericMono<Text> _pool_texts;

		// Token: 0x040030D9 RID: 12505
		private static ConcurrentQueue<LogItem> log_queue = new ConcurrentQueue<LogItem>();
	}
}
