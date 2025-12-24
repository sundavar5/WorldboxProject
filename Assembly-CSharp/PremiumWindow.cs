using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200084A RID: 2122
public class PremiumWindow : MonoBehaviour
{
	// Token: 0x0600426E RID: 17006 RVA: 0x001C1D95 File Offset: 0x001BFF95
	public void Awake()
	{
		this.clearButtons();
		this.addButtons();
	}

	// Token: 0x0600426F RID: 17007 RVA: 0x001C1DA4 File Offset: 0x001BFFA4
	private void addButtons()
	{
		Dictionary<string, PowerButton> tUniqueButtons = new Dictionary<string, PowerButton>();
		foreach (PowerButton tButton in GodPower.premium_buttons)
		{
			tUniqueButtons.TryAdd(tButton.godPower.id, tButton);
		}
		foreach (PowerButton tButton2 in tUniqueButtons.Values)
		{
			tButton2.gameObject.SetActive(false);
			PowerButton powerButton = Object.Instantiate<PowerButton>(tButton2, this.buttons_transform);
			powerButton.transform.name = tButton2.transform.name;
			powerButton.type = PowerButtonType.Shop;
			powerButton.destroyLockIcon();
			powerButton.GetComponent<RectTransform>().pivot = tButton2.GetComponent<RectTransform>().pivot;
			IconRotationAnimation iconRotationAnimation = powerButton.gameObject.AddComponent<IconRotationAnimation>();
			iconRotationAnimation.delay = Randy.randomFloat(1f, 10f);
			iconRotationAnimation.randomDelay = true;
			tButton2.gameObject.SetActive(true);
			powerButton.gameObject.SetActive(true);
		}
	}

	// Token: 0x06004270 RID: 17008 RVA: 0x001C1EE0 File Offset: 0x001C00E0
	private void clearButtons()
	{
		while (this.buttons_transform.childCount > 0)
		{
			GameObject gameObject = this.buttons_transform.GetChild(0).gameObject;
			gameObject.transform.SetParent(null);
			Object.Destroy(gameObject);
		}
	}

	// Token: 0x040030B0 RID: 12464
	public Transform buttons_transform;
}
