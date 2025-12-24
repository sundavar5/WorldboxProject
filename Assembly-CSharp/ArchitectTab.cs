using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200062E RID: 1582
public class ArchitectTab : MonoBehaviour
{
	// Token: 0x060033A0 RID: 13216 RVA: 0x00183954 File Offset: 0x00181B54
	private void Awake()
	{
		this.initButtons();
	}

	// Token: 0x060033A1 RID: 13217 RVA: 0x0018395C File Offset: 0x00181B5C
	private void initButtons()
	{
		for (int i = 0; i < AssetManager.architect_mood_library.list.Count; i++)
		{
			ArchitectMood tAsset = AssetManager.architect_mood_library.list[i];
			ArchitectMoodButton tButton = this.initButton(tAsset);
			this._buttons.Add(tAsset, tButton);
		}
	}

	// Token: 0x060033A2 RID: 13218 RVA: 0x001839A9 File Offset: 0x00181BA9
	private ArchitectMoodButton initButton(ArchitectMood pAsset)
	{
		ArchitectMoodButton architectMoodButton = Object.Instantiate<ArchitectMoodButton>(this._mood_prefab, this._grid_placement);
		architectMoodButton.setAsset(pAsset);
		architectMoodButton.addClickCallback(new ArchitectMoodAction(this.buttonAction));
		return architectMoodButton;
	}

	// Token: 0x060033A3 RID: 13219 RVA: 0x001839D8 File Offset: 0x00181BD8
	private void buttonAction(ArchitectMoodButton pElement)
	{
		ArchitectMood tAsset = pElement.getAsset();
		World.world.map_stats.player_mood = tAsset.id;
		World.world.clearArchitectMood();
		this.updateElements();
	}

	// Token: 0x060033A4 RID: 13220 RVA: 0x00183A14 File Offset: 0x00181C14
	private void updateElements()
	{
		ArchitectMood tCurrentMood = World.world.getArchitectMood();
		foreach (ArchitectMoodButton architectMoodButton in this._buttons.Values)
		{
			bool tEnabled = architectMoodButton.getAsset() == tCurrentMood;
			architectMoodButton.toggleSelectedButton(tEnabled);
			architectMoodButton.setIconActiveColor(tEnabled);
		}
	}

	// Token: 0x060033A5 RID: 13221 RVA: 0x00183A88 File Offset: 0x00181C88
	private void OnEnable()
	{
		this.updateElements();
	}

	// Token: 0x04002719 RID: 10009
	private Dictionary<ArchitectMood, ArchitectMoodButton> _buttons = new Dictionary<ArchitectMood, ArchitectMoodButton>();

	// Token: 0x0400271A RID: 10010
	[SerializeField]
	private ArchitectMoodButton _mood_prefab;

	// Token: 0x0400271B RID: 10011
	[SerializeField]
	private Transform _grid_placement;
}
