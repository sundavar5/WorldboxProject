using System;

// Token: 0x020002AE RID: 686
public class MetaTextReportHelper
{
	// Token: 0x1700018A RID: 394
	// (get) Token: 0x06001998 RID: 6552 RVA: 0x000F16C2 File Offset: 0x000EF8C2
	public static string color_text_main
	{
		get
		{
			return ColorStyleLibrary.m.color_text_grey;
		}
	}

	// Token: 0x1700018B RID: 395
	// (get) Token: 0x06001999 RID: 6553 RVA: 0x000F16CE File Offset: 0x000EF8CE
	public static string color_text_quote
	{
		get
		{
			return ColorStyleLibrary.m.color_text_grey_dark;
		}
	}

	// Token: 0x0600199A RID: 6554 RVA: 0x000F16DC File Offset: 0x000EF8DC
	public static string addSingleUnitText(Actor pActor, bool pAddGap = true, bool pAddNameQuote = true)
	{
		if (!pActor.hasHappinessHistory())
		{
			return string.Empty;
		}
		string result;
		using (ListPool<HappinessHistory> tTempListPool = new ListPool<HappinessHistory>(pActor.happiness_change_history))
		{
			HappinessHistory tHappinessElement = tTempListPool.GetRandom<HappinessHistory>();
			string tTranslatedResult = tHappinessElement.asset.getRandomTextSingleReportLocalized();
			string tQuote = "<i>\"" + tTranslatedResult + "\"</i>";
			string tName = "\n— " + pActor.name;
			string tInfo = pActor.getAge().ToString();
			if (pActor.isSexFemale())
			{
				tInfo += " F";
			}
			else
			{
				tInfo += " M";
			}
			string tAgo = tHappinessElement.getAgoString().ColorHex(ColorStyleLibrary.m.color_text_grey_dark, false);
			string tFinalResult = "";
			if (pAddGap)
			{
				tFinalResult = "\n\n";
			}
			tFinalResult = tFinalResult + tQuote.ColorHex(MetaTextReportHelper.color_text_quote, false) + "  " + tAgo;
			if (pAddNameQuote)
			{
				tFinalResult = tFinalResult + tName.ColorHex(pActor.kingdom.getColor().color_text, false) + "  " + tInfo.ColorHex(ColorStyleLibrary.m.color_text_grey_dark, false);
			}
			result = tFinalResult;
		}
		return result;
	}

	// Token: 0x0600199B RID: 6555 RVA: 0x000F1814 File Offset: 0x000EFA14
	public static string addSingleUnitTextRandomUnit(IMetaObject pMetaObject, out Actor pActorResult)
	{
		pActorResult = null;
		int tTries = 10;
		while (tTries-- > 0)
		{
			Actor tActor = pMetaObject.getRandomUnit();
			if (tActor != null && tActor.isAlive() && tActor.hasHappinessHistory())
			{
				string tFinalResult = MetaTextReportHelper.addSingleUnitText(tActor, true, true);
				if (!string.IsNullOrEmpty(tFinalResult))
				{
					pActorResult = tActor;
					return tFinalResult;
				}
			}
		}
		return string.Empty;
	}

	// Token: 0x0600199C RID: 6556 RVA: 0x000F1868 File Offset: 0x000EFA68
	public static string getText(IMetaObject pMetaObject, MetaTypeAsset pAsset, out Actor pActorResult)
	{
		pActorResult = null;
		string tFinalText = string.Empty;
		bool tAnyTextAdded = false;
		foreach (string tReportID in pAsset.reports)
		{
			MetaTextReportAsset tReportAsset = AssetManager.meta_text_report_library.get(tReportID);
			if (tReportAsset.report_action(pMetaObject))
			{
				if (tAnyTextAdded)
				{
					tFinalText += " ";
				}
				tAnyTextAdded = true;
				string tTextToAdd = tReportAsset.get_random_text;
				if (tReportAsset.color != null)
				{
					tTextToAdd = tTextToAdd.ColorHex(tReportAsset.color, false);
				}
				tFinalText += tTextToAdd;
			}
		}
		if (tAnyTextAdded)
		{
			Actor tActorResult;
			tFinalText += MetaTextReportHelper.addSingleUnitTextRandomUnit(pMetaObject, out tActorResult);
			pActorResult = tActorResult;
			tFinalText = tFinalText.ColorHex(MetaTextReportHelper.color_text_main, false);
		}
		return tFinalText;
	}
}
