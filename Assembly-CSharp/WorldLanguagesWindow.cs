using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

// Token: 0x02000846 RID: 2118
public class WorldLanguagesWindow : MonoBehaviour
{
	// Token: 0x06004252 RID: 16978 RVA: 0x001C100C File Offset: 0x001BF20C
	private void Start()
	{
		TextAsset tPercentageAsset = Resources.Load<TextAsset>("locales/percentages");
		this._percentage_data = JsonConvert.DeserializeObject<Dictionary<string, int>>(tPercentageAsset.text);
		this._pool = new ObjectPoolGenericMono<LocalizationButton>(this._language_button, this._content);
		foreach (GameLanguageAsset tLang in AssetManager.game_language_library.list)
		{
			if (tLang != null)
			{
				LocalizationButton tButton = this._pool.getNext();
				WorldLanguagesWindow._all_buttons.Add(tButton);
				int tPercentageDone;
				this._percentage_data.TryGetValue(tLang.id, out tPercentageDone);
				tButton.SetAsset(tLang, tPercentageDone);
			}
		}
		this.checkDebug();
	}

	// Token: 0x06004253 RID: 16979 RVA: 0x001C10D0 File Offset: 0x001BF2D0
	private void OnEnable()
	{
		this.checkDebug();
	}

	// Token: 0x06004254 RID: 16980 RVA: 0x001C10D8 File Offset: 0x001BF2D8
	private void checkDebug()
	{
		bool tDebugOn = DebugConfig.isOn(DebugOption.DebugButton);
		foreach (LocalizationButton tButton in WorldLanguagesWindow._all_buttons)
		{
			if (tButton.getAsset().debug_only)
			{
				tButton.gameObject.SetActive(tDebugOn);
			}
		}
	}

	// Token: 0x06004255 RID: 16981 RVA: 0x001C1148 File Offset: 0x001BF348
	public static void updateButtons()
	{
		foreach (LocalizationButton localizationButton in WorldLanguagesWindow._all_buttons)
		{
			localizationButton.checkSprite();
		}
	}

	// Token: 0x04003065 RID: 12389
	[SerializeField]
	private LocalizationButton _language_button;

	// Token: 0x04003066 RID: 12390
	private ObjectPoolGenericMono<LocalizationButton> _pool;

	// Token: 0x04003067 RID: 12391
	[SerializeField]
	private Transform _content;

	// Token: 0x04003068 RID: 12392
	private static HashSet<LocalizationButton> _all_buttons = new HashSet<LocalizationButton>();

	// Token: 0x04003069 RID: 12393
	private Dictionary<string, int> _percentage_data;
}
