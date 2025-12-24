using System;
using System.Runtime.InteropServices;

// Token: 0x02000009 RID: 9
public class PreciseLocale
{
	// Token: 0x17000001 RID: 1
	// (get) Token: 0x0600000D RID: 13 RVA: 0x00003330 File Offset: 0x00001530
	private static PreciseLocale.PlatformBridge platform
	{
		get
		{
			if (PreciseLocale._platform == null)
			{
				PreciseLocale._platform = new PreciseLocale.PreciseLocaleWindows();
			}
			return PreciseLocale._platform;
		}
	}

	// Token: 0x0600000E RID: 14 RVA: 0x00003348 File Offset: 0x00001548
	public static string GetRegion()
	{
		return PreciseLocale.platform.GetRegion();
	}

	// Token: 0x0600000F RID: 15 RVA: 0x00003354 File Offset: 0x00001554
	public static string GetLanguageID()
	{
		return PreciseLocale.platform.GetLanguageID();
	}

	// Token: 0x06000010 RID: 16 RVA: 0x00003360 File Offset: 0x00001560
	public static string GetLanguage()
	{
		return PreciseLocale.platform.GetLanguage();
	}

	// Token: 0x06000011 RID: 17 RVA: 0x0000336C File Offset: 0x0000156C
	public static string GetCurrencyCode()
	{
		return PreciseLocale.platform.GetCurrencyCode();
	}

	// Token: 0x06000012 RID: 18 RVA: 0x00003378 File Offset: 0x00001578
	public static string GetCurrencySymbol()
	{
		return PreciseLocale.platform.GetCurrencySymbol();
	}

	// Token: 0x0400000D RID: 13
	private static PreciseLocale.PlatformBridge _platform;

	// Token: 0x0200098D RID: 2445
	private interface PlatformBridge
	{
		// Token: 0x0600474A RID: 18250
		string GetRegion();

		// Token: 0x0600474B RID: 18251
		string GetLanguage();

		// Token: 0x0600474C RID: 18252
		string GetLanguageID();

		// Token: 0x0600474D RID: 18253
		string GetCurrencyCode();

		// Token: 0x0600474E RID: 18254
		string GetCurrencySymbol();
	}

	// Token: 0x0200098E RID: 2446
	private class EditorBridge : PreciseLocale.PlatformBridge
	{
		// Token: 0x0600474F RID: 18255 RVA: 0x001E3E54 File Offset: 0x001E2054
		public string GetRegion()
		{
			return "US";
		}

		// Token: 0x06004750 RID: 18256 RVA: 0x001E3E5B File Offset: 0x001E205B
		public string GetLanguage()
		{
			return "en";
		}

		// Token: 0x06004751 RID: 18257 RVA: 0x001E3E62 File Offset: 0x001E2062
		public string GetLanguageID()
		{
			return "en_US";
		}

		// Token: 0x06004752 RID: 18258 RVA: 0x001E3E69 File Offset: 0x001E2069
		public string GetCurrencyCode()
		{
			return "USD";
		}

		// Token: 0x06004753 RID: 18259 RVA: 0x001E3E70 File Offset: 0x001E2070
		public string GetCurrencySymbol()
		{
			return "$";
		}
	}

	// Token: 0x0200098F RID: 2447
	private class PreciseLocaleWindows : PreciseLocale.PlatformBridge
	{
		// Token: 0x06004755 RID: 18261
		[DllImport("PreciseLocale")]
		private static extern IntPtr _getLanguage();

		// Token: 0x06004756 RID: 18262
		[DllImport("PreciseLocale")]
		private static extern IntPtr _getRegion();

		// Token: 0x06004757 RID: 18263
		[DllImport("PreciseLocale")]
		private static extern IntPtr _getCurrencyCode();

		// Token: 0x06004758 RID: 18264
		[DllImport("PreciseLocale")]
		private static extern IntPtr _getCurrencySymbol();

		// Token: 0x06004759 RID: 18265 RVA: 0x001E3E7F File Offset: 0x001E207F
		public string GetLanguage()
		{
			return Marshal.PtrToStringUni(PreciseLocale.PreciseLocaleWindows._getLanguage());
		}

		// Token: 0x0600475A RID: 18266 RVA: 0x001E3E8B File Offset: 0x001E208B
		public string GetLanguageID()
		{
			return this.GetLanguage() + "_" + this.GetRegion();
		}

		// Token: 0x0600475B RID: 18267 RVA: 0x001E3EA3 File Offset: 0x001E20A3
		public string GetRegion()
		{
			return Marshal.PtrToStringUni(PreciseLocale.PreciseLocaleWindows._getRegion());
		}

		// Token: 0x0600475C RID: 18268 RVA: 0x001E3EAF File Offset: 0x001E20AF
		public string GetCurrencyCode()
		{
			return Marshal.PtrToStringUni(PreciseLocale.PreciseLocaleWindows._getCurrencyCode());
		}

		// Token: 0x0600475D RID: 18269 RVA: 0x001E3EBB File Offset: 0x001E20BB
		public string GetCurrencySymbol()
		{
			return Marshal.PtrToStringUni(PreciseLocale.PreciseLocaleWindows._getCurrencySymbol());
		}
	}
}
