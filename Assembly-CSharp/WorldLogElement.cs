using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000718 RID: 1816
public class WorldLogElement : MonoBehaviour
{
	// Token: 0x060039E7 RID: 14823 RVA: 0x0019B3FC File Offset: 0x001995FC
	public void showMessage(WorldLogMessage pMessage)
	{
		this.message = pMessage;
		this.date.text = "y:" + Date.getYear((double)this.message.timestamp).ToString() + ", m:" + Date.getMonth((double)this.message.timestamp).ToString();
		string tText = this.message.getFormatedText(this.description);
		bool tHasLocation = this.message.hasLocation();
		if (this.message.hasFollowLocation())
		{
			this.follow.SetActive(true);
			this.locate.SetActive(false);
		}
		else
		{
			this.follow.SetActive(false);
			this.locate.SetActive(tHasLocation);
		}
		this.description.text = (tText ?? "");
		this.description.GetComponent<LocalizedText>().checkTextFont(null);
		string tIconPath = this.message.getAsset().path_icon;
		if (!string.IsNullOrEmpty(tIconPath))
		{
			Sprite tSprite = SpriteTextureLoader.getSprite(tIconPath);
			this.icon.sprite = tSprite;
		}
		else
		{
			this.icon.gameObject.SetActive(false);
		}
		this.description.GetComponent<LocalizedText>().checkSpecialLanguages(null);
	}

	// Token: 0x060039E8 RID: 14824 RVA: 0x0019B531 File Offset: 0x00199731
	public void clickLocate()
	{
		this.message.jumpToLocation();
	}

	// Token: 0x04002AD3 RID: 10963
	public Text date;

	// Token: 0x04002AD4 RID: 10964
	public Text description;

	// Token: 0x04002AD5 RID: 10965
	public Image icon;

	// Token: 0x04002AD6 RID: 10966
	public GameObject locate;

	// Token: 0x04002AD7 RID: 10967
	public GameObject follow;

	// Token: 0x04002AD8 RID: 10968
	public WorldLogMessage message;
}
