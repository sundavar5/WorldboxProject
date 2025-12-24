using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000588 RID: 1416
public class FaveWorldButton : MonoBehaviour
{
	// Token: 0x06002F04 RID: 12036 RVA: 0x0016C218 File Offset: 0x0016A418
	private void Start()
	{
		Button tButton;
		if (base.TryGetComponent<Button>(out tButton))
		{
			tButton.onClick.AddListener(new UnityAction(this.faveWorld));
		}
	}

	// Token: 0x06002F05 RID: 12037 RVA: 0x0016C246 File Offset: 0x0016A446
	private void OnEnable()
	{
		this.updateFavoriteIconFor(SaveManager.currentSlot);
	}

	// Token: 0x06002F06 RID: 12038 RVA: 0x0016C254 File Offset: 0x0016A454
	private void faveWorld()
	{
		int tCurrentSlot = SaveManager.currentSlot;
		if (PlayerConfig.instance.data.favorite_world == tCurrentSlot)
		{
			PlayerConfig.instance.data.favorite_world = -1;
		}
		else
		{
			PlayerConfig.instance.data.favorite_world = tCurrentSlot;
		}
		PlayerConfig.saveData();
		this.updateFavoriteIconFor(tCurrentSlot);
	}

	// Token: 0x06002F07 RID: 12039 RVA: 0x0016C2A7 File Offset: 0x0016A4A7
	private void updateFavoriteIconFor(int pId)
	{
		if (PlayerConfig.instance.data.favorite_world == pId)
		{
			this.icon.color = ColorStyleLibrary.m.favorite_selected;
			return;
		}
		this.icon.color = ColorStyleLibrary.m.favorite_not_selected;
	}

	// Token: 0x040022FF RID: 8959
	public Image icon;
}
