using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200079B RID: 1947
public class Tutorial : MonoBehaviour
{
	// Token: 0x06003DB4 RID: 15796 RVA: 0x001AEDA0 File Offset: 0x001ACFA0
	private void create()
	{
		this.pages = new List<TutorialPage>();
		this.color_red = Toolbox.makeColor("#FF3700");
		this.color_red.a = 0.4f;
		this.color_white = Toolbox.makeColor("#4AA5FF");
		this.color_white.a = 0.4f;
		this.color_yellow = Toolbox.makeColor("#FEFE00");
		this.color_yellow_transparent = Toolbox.makeColor("#FEFE00");
		this.color_yellow_transparent.a = 0f;
		this.add(new TutorialPage
		{
			text = "tut_page1",
			wait = 1f
		});
		this.add(new TutorialPage
		{
			text = "tut_page2",
			wait = 0.3f
		});
		this.add(new TutorialPage
		{
			text = "tut_page3_mobile",
			mobileOnly = true,
			centerImage = this.icon_finger,
			wait = 1f
		});
		this.add(new TutorialPage
		{
			text = "tut_page3_pc",
			pcOnly = true,
			wait = 1f
		});
		this.add(new TutorialPage
		{
			text = "tut_page4"
		});
		this.add(new TutorialPage
		{
			text = "tut_page5",
			object1 = this.saveButton,
			centerImage = this.icon_saveBox,
			wait = 1.5f
		});
		this.add(new TutorialPage
		{
			text = "tut_page6",
			object1 = this.customMapButton,
			centerImage = this.icon_customWorld,
			wait = 1.5f
		});
		this.add(new TutorialPage
		{
			text = "tut_page7",
			object1 = this.worldRules,
			centerImage = this.icon_worldLaws
		});
		this.add(new TutorialPage
		{
			text = "tut_page8",
			object1 = this.tabDrawing
		});
		this.add(new TutorialPage
		{
			text = "tut_page9",
			icon = "brush"
		});
		this.add(new TutorialPage
		{
			text = "tut_page10",
			object1 = this.tabCivs,
			centerImage = this.icon_ivilizations
		});
		this.add(new TutorialPage
		{
			text = "tut_page11",
			object1 = this.tabCreatures,
			centerImage = this.icon_dragon
		});
		this.add(new TutorialPage
		{
			text = "tut_page12",
			object1 = this.tabNature,
			centerImage = this.icon_tornado
		});
		this.add(new TutorialPage
		{
			text = "tut_page13",
			object1 = this.tabBombs,
			centerImage = this.icon_nuke
		});
		this.add(new TutorialPage
		{
			text = "tut_page14",
			object1 = this.tabOther,
			centerImage = this.icon_greyGoo
		});
		this.add(new TutorialPage
		{
			text = "tut_page15",
			centerImage = this.icon_ufo
		});
		this.add(new TutorialPage
		{
			text = "tut_page16",
			mobileOnly = true,
			icon = "reward",
			wait = 1.5f
		});
		this.add(new TutorialPage
		{
			text = "tut_page17",
			centerImage = this.icon_heart,
			wait = 0.5f
		});
	}

	// Token: 0x06003DB5 RID: 15797 RVA: 0x001AF128 File Offset: 0x001AD328
	public void startTutorial()
	{
		if (this.pages == null)
		{
			this.create();
		}
		base.gameObject.SetActive(true);
		this.curPage = -1;
		PowerButtonSelector.instance.unselectAll();
		PowerButtonSelector.instance.unselectTabs();
		this.attentionBox.gameObject.SetActive(false);
		this.nextPage();
	}

	// Token: 0x06003DB6 RID: 15798 RVA: 0x001AF184 File Offset: 0x001AD384
	private void nextPage()
	{
		this.curPage++;
		if (this.curPage >= this.pages.Count)
		{
			this.endTutorial();
			return;
		}
		this.pressAnywhere.GetComponent<LocalizedText>().updateText(true);
		TutorialPage tPage = this.pages[this.curPage];
		if (!Config.isMobile && tPage.mobileOnly)
		{
			this.nextPage();
			return;
		}
		if (Config.isMobile && tPage.pcOnly)
		{
			this.nextPage();
			return;
		}
		if (tPage.object1 == null)
		{
			this.attentionBox.gameObject.SetActive(false);
		}
		else
		{
			this.attentionBox.gameObject.SetActive(true);
			Vector3 tNewPos = tPage.object1.transform.position;
			Vector2 tNewSize = tPage.object1.GetComponent<RectTransform>().sizeDelta;
			tNewSize.x += 10f;
			tNewSize.y += 10f;
			this.attentionBox.transform.position = tNewPos;
			this.attentionBox.rectTransform.sizeDelta = tNewSize;
			this.attentionBox.DOKill(false);
			this.attentionBox.transform.localScale = new Vector3(0.5f, 0.5f);
			this.attentionBox.transform.DOScale(new Vector3(1f, 1f, 1f), 0.3f).SetEase(Ease.OutBack);
			this.attentionBox.color = this.color_white;
		}
		if (tPage.centerImage == null)
		{
			tPage.centerImage = this.icon_default;
		}
		this.centerObject.GetComponent<Image>().sprite = tPage.centerImage;
		this.centerObject.gameObject.SetActive(false);
		this.adButton.gameObject.SetActive(false);
		this.brushSize.gameObject.SetActive(false);
		this.text.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
		this.text.DOKill(false);
		this.text.text = "";
		string tLocalText = LocalizedTextManager.getText(tPage.text, null, false);
		float tDur = (float)(tLocalText.Length / 25);
		if (tDur <= 1f)
		{
			tDur = 1f;
		}
		this.text.text = tLocalText;
		this.text.GetComponent<LocalizedText>().checkTextFont(null);
		this.text.GetComponent<LocalizedText>().checkSpecialLanguages(null);
		tLocalText = this.text.text;
		this.text.text = "";
		this.textTypeTween = this.text.DOText(tLocalText, tDur, false, ScrambleMode.None, null);
		this.text.transform.DOScale(new Vector3(1f, 1f, 1f), 0.3f).SetEase(Ease.InBack);
		this.waitTimer = tPage.wait;
		if (this.canSkipTutorial())
		{
			this.waitTimer = 0f;
		}
		if (this.waitTimer > 0f)
		{
			this.pressAnywhere.gameObject.SetActive(false);
		}
		this.bear.transform.DOKill(false);
		this.bear.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
		this.bear.transform.DOShakeRotation(tDur, 90f, 10, 90f, true, ShakeRandomnessMode.Full);
		if (tPage.icon == "default")
		{
			this.centerObject.SetActive(true);
			this.centerObject.transform.DOKill(false);
			this.centerObject.transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
			this.centerObject.transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f).SetEase(Ease.InBack);
			return;
		}
		if (tPage.icon == "reward")
		{
			this.adButton.SetActive(true);
			this.adButton.transform.DOKill(false);
			this.adButton.transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
			this.adButton.transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f).SetEase(Ease.InBack);
			return;
		}
		if (tPage.icon == "brush")
		{
			this.brushSize.SetActive(true);
			this.brushSize.transform.DOKill(false);
			this.brushSize.transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
			this.brushSize.transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f).SetEase(Ease.InBack);
		}
	}

	// Token: 0x06003DB7 RID: 15799 RVA: 0x001AF696 File Offset: 0x001AD896
	internal void completeText()
	{
	}

	// Token: 0x06003DB8 RID: 15800 RVA: 0x001AF698 File Offset: 0x001AD898
	private bool canSkipTutorial()
	{
		return true;
	}

	// Token: 0x06003DB9 RID: 15801 RVA: 0x001AF69B File Offset: 0x001AD89B
	public static void restartTutorial()
	{
		PlayerConfig.instance.data.tutorialFinished = false;
		PlayerConfig.saveData();
	}

	// Token: 0x06003DBA RID: 15802 RVA: 0x001AF6B2 File Offset: 0x001AD8B2
	internal bool isActive()
	{
		return base.gameObject.activeSelf;
	}

	// Token: 0x06003DBB RID: 15803 RVA: 0x001AF6BF File Offset: 0x001AD8BF
	internal void endTutorial()
	{
		base.gameObject.SetActive(false);
		PlayerConfig.instance.data.tutorialFinished = true;
		PlayerConfig.saveData();
		ScrollWindow.clearQueue();
	}

	// Token: 0x06003DBC RID: 15804 RVA: 0x001AF6E8 File Offset: 0x001AD8E8
	private void LateUpdate()
	{
		if (this.attentionBox.gameObject.activeSelf)
		{
			if (this.attentionBox.color == this.color_red)
			{
				this.attentionBox.DOColor(this.color_white, 1f);
			}
			else if (this.attentionBox.color == this.color_white)
			{
				this.attentionBox.DOColor(this.color_red, 1f);
			}
		}
		if (this.canSkipTutorial())
		{
			if (this.textTypeTween.IsActive() && Input.GetMouseButtonUp(0))
			{
				this.textTypeTween.Kill(true);
				return;
			}
		}
		else if (this.textTypeTween.IsActive())
		{
			return;
		}
		if (this.waitTimer > 0f)
		{
			this.waitTimer -= Time.deltaTime;
			return;
		}
		if (!this.pressAnywhere.gameObject.activeSelf)
		{
			this.pressAnywhere.gameObject.SetActive(true);
			this.pressAnywhere.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
			this.pressAnywhere.transform.DOScale(new Vector3(1f, 1f, 1f), 1f).SetEase(Ease.OutBack);
			this.pressAnywhere.color = this.color_yellow_transparent;
			this.pressAnywhere.DOColor(this.color_yellow, 1f);
		}
		if (Input.GetMouseButtonUp(0))
		{
			this.nextPage();
		}
	}

	// Token: 0x06003DBD RID: 15805 RVA: 0x001AF870 File Offset: 0x001ADA70
	private void add(TutorialPage pPage)
	{
		this.pages.Add(pPage);
	}

	// Token: 0x04002C94 RID: 11412
	public Sprite icon_default;

	// Token: 0x04002C95 RID: 11413
	public Sprite icon_bear;

	// Token: 0x04002C96 RID: 11414
	public Sprite icon_ivilizations;

	// Token: 0x04002C97 RID: 11415
	public Sprite icon_nuke;

	// Token: 0x04002C98 RID: 11416
	public Sprite icon_dragon;

	// Token: 0x04002C99 RID: 11417
	public Sprite icon_tornado;

	// Token: 0x04002C9A RID: 11418
	public Sprite icon_saveBox;

	// Token: 0x04002C9B RID: 11419
	public Sprite icon_customWorld;

	// Token: 0x04002C9C RID: 11420
	public Sprite icon_worldLaws;

	// Token: 0x04002C9D RID: 11421
	public Sprite icon_greyGoo;

	// Token: 0x04002C9E RID: 11422
	public Sprite icon_ufo;

	// Token: 0x04002C9F RID: 11423
	public Sprite icon_heart;

	// Token: 0x04002CA0 RID: 11424
	public Sprite icon_finger;

	// Token: 0x04002CA1 RID: 11425
	public GameObject bear;

	// Token: 0x04002CA2 RID: 11426
	public GameObject centerObject;

	// Token: 0x04002CA3 RID: 11427
	public GameObject brushSize;

	// Token: 0x04002CA4 RID: 11428
	public GameObject adButton;

	// Token: 0x04002CA5 RID: 11429
	public GameObject saveButton;

	// Token: 0x04002CA6 RID: 11430
	public GameObject customMapButton;

	// Token: 0x04002CA7 RID: 11431
	public GameObject worldRules;

	// Token: 0x04002CA8 RID: 11432
	public GameObject tabDrawing;

	// Token: 0x04002CA9 RID: 11433
	public GameObject tabCivs;

	// Token: 0x04002CAA RID: 11434
	public GameObject tabCreatures;

	// Token: 0x04002CAB RID: 11435
	public GameObject tabNature;

	// Token: 0x04002CAC RID: 11436
	public GameObject tabBombs;

	// Token: 0x04002CAD RID: 11437
	public GameObject tabOther;

	// Token: 0x04002CAE RID: 11438
	public GameObject settingsButton;

	// Token: 0x04002CAF RID: 11439
	public Text text;

	// Token: 0x04002CB0 RID: 11440
	public Image attentionBox;

	// Token: 0x04002CB1 RID: 11441
	public Text pressAnywhere;

	// Token: 0x04002CB2 RID: 11442
	private int curPage;

	// Token: 0x04002CB3 RID: 11443
	private List<TutorialPage> pages;

	// Token: 0x04002CB4 RID: 11444
	private float waitTimer;

	// Token: 0x04002CB5 RID: 11445
	private Color color_red;

	// Token: 0x04002CB6 RID: 11446
	private Color color_white;

	// Token: 0x04002CB7 RID: 11447
	private Color color_yellow;

	// Token: 0x04002CB8 RID: 11448
	private Color color_yellow_transparent;

	// Token: 0x04002CB9 RID: 11449
	private Tweener textTypeTween;
}
