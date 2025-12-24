using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200010D RID: 269
public static class HotkeysLocalized
{
	// Token: 0x06000856 RID: 2134 RVA: 0x00074E38 File Offset: 0x00073038
	private static void init()
	{
		HotkeysLocalized._dictionary = new Dictionary<KeyCode, string>();
		HotkeysLocalized._dictionary.Add(KeyCode.Alpha0, "0");
		HotkeysLocalized._dictionary.Add(KeyCode.Alpha1, "1");
		HotkeysLocalized._dictionary.Add(KeyCode.Alpha2, "2");
		HotkeysLocalized._dictionary.Add(KeyCode.Alpha3, "3");
		HotkeysLocalized._dictionary.Add(KeyCode.Alpha4, "4");
		HotkeysLocalized._dictionary.Add(KeyCode.Alpha5, "5");
		HotkeysLocalized._dictionary.Add(KeyCode.Alpha6, "6");
		HotkeysLocalized._dictionary.Add(KeyCode.Alpha7, "7");
		HotkeysLocalized._dictionary.Add(KeyCode.Alpha8, "8");
		HotkeysLocalized._dictionary.Add(KeyCode.Alpha9, "9");
		HotkeysLocalized._dictionary.Add(KeyCode.Keypad0, "0");
		HotkeysLocalized._dictionary.Add(KeyCode.Keypad1, "1");
		HotkeysLocalized._dictionary.Add(KeyCode.Keypad2, "2");
		HotkeysLocalized._dictionary.Add(KeyCode.Keypad3, "3");
		HotkeysLocalized._dictionary.Add(KeyCode.Keypad4, "4");
		HotkeysLocalized._dictionary.Add(KeyCode.Keypad5, "5");
		HotkeysLocalized._dictionary.Add(KeyCode.Keypad6, "6");
		HotkeysLocalized._dictionary.Add(KeyCode.Keypad7, "7");
		HotkeysLocalized._dictionary.Add(KeyCode.Keypad8, "8");
		HotkeysLocalized._dictionary.Add(KeyCode.Keypad9, "9");
		HotkeysLocalized._dictionary.Add(KeyCode.Space, "SPACE");
		HotkeysLocalized._dictionary.Add(KeyCode.LeftShift, "SHIFT");
		HotkeysLocalized._dictionary.Add(KeyCode.RightShift, "SHIFT");
		HotkeysLocalized._dictionary.Add(KeyCode.LeftAlt, "ALT");
		HotkeysLocalized._dictionary.Add(KeyCode.RightAlt, "ALT");
		HotkeysLocalized._dictionary.Add(KeyCode.LeftControl, "CONTROL");
		HotkeysLocalized._dictionary.Add(KeyCode.RightControl, "CONTROL");
		HotkeysLocalized._dictionary.Add(KeyCode.LeftMeta, "");
		HotkeysLocalized._dictionary.Add(KeyCode.RightMeta, "");
	}

	// Token: 0x06000857 RID: 2135 RVA: 0x00075074 File Offset: 0x00073274
	public static string getLocalizedKey(KeyCode pCode)
	{
		if (HotkeysLocalized._dictionary == null)
		{
			HotkeysLocalized.init();
		}
		if (pCode == KeyCode.None)
		{
			return string.Empty;
		}
		string tResult = string.Empty;
		if (HotkeysLocalized._dictionary.ContainsKey(pCode))
		{
			tResult = HotkeysLocalized._dictionary[pCode];
		}
		else
		{
			tResult = pCode.ToString();
		}
		if (string.IsNullOrEmpty(tResult))
		{
			return string.Empty;
		}
		return Toolbox.coloredText(tResult, "#95DD5D", false);
	}

	// Token: 0x040008AE RID: 2222
	private static Dictionary<KeyCode, string> _dictionary;
}
