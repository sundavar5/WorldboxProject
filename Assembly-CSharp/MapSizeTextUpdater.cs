using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020005EA RID: 1514
public class MapSizeTextUpdater : MonoBehaviour
{
	// Token: 0x0600319F RID: 12703 RVA: 0x0017B40D File Offset: 0x0017960D
	private void Update()
	{
		this.updateVars();
	}

	// Token: 0x060031A0 RID: 12704 RVA: 0x0017B418 File Offset: 0x00179618
	private void updateVars()
	{
		Text component = base.GetComponent<Text>();
		string tTextContent = LocalizedTextManager.getText(AssetManager.map_sizes.get(Config.customMapSize).getLocaleID(), null, false);
		component.text = tTextContent.ToUpper();
		component.GetComponent<LocalizedText>().checkSpecialLanguages(null);
		string[] tMapSizes = MapSizeLibrary.getSizes();
		int tCurIndex = tMapSizes.IndexOf(Config.customMapSize);
		this.text_counter.text = (tCurIndex + 1).ToString() + "/" + tMapSizes.Length.ToString();
	}

	// Token: 0x0400257A RID: 9594
	public Text text_counter;
}
