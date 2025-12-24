using System;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

// Token: 0x02000472 RID: 1138
public class LocalizedTextPrice : MonoBehaviour
{
	// Token: 0x060026F4 RID: 9972 RVA: 0x0013C32C File Offset: 0x0013A52C
	internal void updateText(bool pCheckText = true)
	{
		if (!string.IsNullOrEmpty(LocalizedTextPrice.discount))
		{
			this.showDiscount(LocalizedTextPrice.discount);
		}
		string tString = "";
		if (InAppManager.instance != null)
		{
			InAppManager instance = InAppManager.instance;
			bool flag;
			if (instance == null)
			{
				flag = (null != null);
			}
			else
			{
				IStoreController controller = instance.controller;
				flag = (((controller != null) ? controller.products : null) != null);
			}
			if (flag)
			{
				Product product = InAppManager.instance.controller.products.WithID("premium");
				if (product != null)
				{
					tString = product.metadata.localizedPriceString;
					goto IL_7A;
				}
				goto IL_7A;
			}
		}
		tString = LocalizedTextPrice.price_current;
		IL_7A:
		this.text_current_price.text = tString;
		if (!string.IsNullOrEmpty(LocalizedTextPrice.price_old))
		{
			this.text_old_price.text = LocalizedTextPrice.price_old;
			this.text_old_price.gameObject.SetActive(true);
		}
	}

	// Token: 0x060026F5 RID: 9973 RVA: 0x0013C3EC File Offset: 0x0013A5EC
	private void showDiscount(string pString)
	{
		this.text_percent.text = pString;
		this.discount_bg.gameObject.SetActive(true);
	}

	// Token: 0x060026F6 RID: 9974 RVA: 0x0013C40C File Offset: 0x0013A60C
	private void setDefault()
	{
		this.discount_bg.gameObject.SetActive(false);
		this.text_current_price.gameObject.SetActive(true);
		this.text_current_price.text = "??";
		this.text_old_price.gameObject.SetActive(false);
	}

	// Token: 0x060026F7 RID: 9975 RVA: 0x0013C45C File Offset: 0x0013A65C
	private void OnEnable()
	{
		this.setDefault();
		this.updateText(true);
	}

	// Token: 0x04001D3E RID: 7486
	public static string price_current = "???";

	// Token: 0x04001D3F RID: 7487
	public static string price_old = string.Empty;

	// Token: 0x04001D40 RID: 7488
	public static string discount = string.Empty;

	// Token: 0x04001D41 RID: 7489
	public Text text_old_price;

	// Token: 0x04001D42 RID: 7490
	public Text text_current_price;

	// Token: 0x04001D43 RID: 7491
	public GameObject discount_bg;

	// Token: 0x04001D44 RID: 7492
	public Text text_percent;

	// Token: 0x04001D45 RID: 7493
	private const string IN_APP_ID = "premium";
}
