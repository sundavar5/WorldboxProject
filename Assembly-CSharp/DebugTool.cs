using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000533 RID: 1331
public class DebugTool : MonoBehaviour
{
	// Token: 0x06002B72 RID: 11122 RVA: 0x00158900 File Offset: 0x00156B00
	private void Awake()
	{
		this.populateOptions();
		this.benchmark_icons = base.transform.FindRecursive("Benchmark Icons");
		this.initButtons();
		this.initElements();
	}

	// Token: 0x06002B73 RID: 11123 RVA: 0x0015892A File Offset: 0x00156B2A
	private void initElements()
	{
		this.transform_texts = base.transform.FindRecursive("Texts");
		this.pool_texts = new ObjectPoolGenericMono<DebugToolTextElement>(this.element_prefab, this.transform_texts);
		this.element_prefab.gameObject.SetActive(false);
	}

	// Token: 0x06002B74 RID: 11124 RVA: 0x0015896C File Offset: 0x00156B6C
	private float calculateLineHeight(Text pText)
	{
		Vector2 extents = pText.cachedTextGenerator.rectExtents.size * 0.5f;
		return pText.cachedTextGeneratorForLayout.GetPreferredHeight("A", pText.GetGenerationSettings(extents));
	}

	// Token: 0x06002B75 RID: 11125 RVA: 0x001589B0 File Offset: 0x00156BB0
	internal void populateOptions()
	{
		this.dropdown.ClearOptions();
		List<string> tOptions = new List<string>();
		foreach (DebugToolAsset tAsset in AssetManager.debug_tool_library.list)
		{
			if (tAsset.type == this.type)
			{
				tOptions.Add(tAsset.name);
			}
		}
		this.dropdown.AddOptions(tOptions);
		this.dropdown.onValueChanged.RemoveListener(new UnityAction<int>(this.switchTool));
		this.dropdown.onValueChanged.AddListener(new UnityAction<int>(this.switchTool));
	}

	// Token: 0x06002B76 RID: 11126 RVA: 0x00158A70 File Offset: 0x00156C70
	public void filterOptions(string pInput)
	{
		foreach (DebugDropdownOption tOption in this.active_dropdown.transform.GetComponentsInChildren<DebugDropdownOption>(true))
		{
			string tName = tOption.title.text;
			if (tName == "Debug option")
			{
				tOption.gameObject.SetActive(false);
			}
			else if (!string.IsNullOrEmpty(pInput) && !tName.ToLower().Contains(pInput.ToLower()))
			{
				tOption.gameObject.SetActive(false);
			}
			else
			{
				tOption.gameObject.SetActive(true);
			}
		}
	}

	// Token: 0x06002B77 RID: 11127 RVA: 0x00158B04 File Offset: 0x00156D04
	private void initButtons()
	{
		this.newButton("SortByName", new UnityAction(this.clickSortByName), delegate(Image pIcon)
		{
			this.checkIcon(pIcon, this.sort_by_names);
		});
		this.newButton("SortByValues", new UnityAction(this.clickSortByValues), delegate(Image pIcon)
		{
			this.checkIcon(pIcon, this.sort_by_values);
		});
		this.newButton("SortReversed", delegate
		{
			this.sort_order_reversed = !this.sort_order_reversed;
		}, delegate(Image pIcon)
		{
			this.checkIcon(pIcon, this.sort_order_reversed);
		});
		this.newButton("ShowAverages", delegate
		{
			this.show_averages = !this.show_averages;
		}, delegate(Image pIcon)
		{
			this.checkIcon(pIcon, this.isValueAverage());
		});
		this.newButton("PercentBasedOnSlowest", delegate
		{
			this.percentage_slowest = !this.percentage_slowest;
		}, delegate(Image pIcon)
		{
			this.checkIcon(pIcon, this.percentage_slowest);
		});
		this.newButton("HideZeroes", delegate
		{
			this.hide_zeroes = !this.hide_zeroes;
		}, delegate(Image pIcon)
		{
			this.checkIcon(pIcon, this.hide_zeroes);
		});
		this.newButton("ShowCounter", delegate
		{
			this.show_counter = !this.show_counter;
		}, delegate(Image pIcon)
		{
			this.checkIcon(pIcon, this.show_counter);
		});
		this.newButton("ShowMax", delegate
		{
			this.show_max = !this.show_max;
		}, delegate(Image pIcon)
		{
			this.checkIcon(pIcon, this.show_max);
		});
		this.newButton("ShowSeconds", delegate
		{
			this.state = DebugToolState.Values;
		}, delegate(Image pIcon)
		{
			this.checkIcon(pIcon, this.state == DebugToolState.Values);
		});
		this.newButton("ShowPercentages", delegate
		{
			this.state = DebugToolState.Percent;
		}, delegate(Image pIcon)
		{
			this.checkIcon(pIcon, this.state == DebugToolState.Percent);
		});
		this.newButton("ShowTimeSpent", delegate
		{
			this.state = DebugToolState.TimeSpent;
		}, delegate(Image pIcon)
		{
			this.checkIcon(pIcon, this.state == DebugToolState.TimeSpent);
		});
		this.newButton("ShowFrameBudget", delegate
		{
			this.state = DebugToolState.FrameBudget;
		}, delegate(Image pIcon)
		{
			this.checkIcon(pIcon, this.state == DebugToolState.FrameBudget);
		});
		this.newButton("Paused", delegate
		{
			this.paused = !this.paused;
		}, delegate(Image pIcon)
		{
			this.checkIcon(pIcon, this.paused);
		});
		this.newButton("EnableBenchmarks", delegate
		{
			Bench.bench_enabled = !Bench.bench_enabled;
		}, delegate(Image pIcon)
		{
			this.checkIcon(pIcon, Bench.bench_enabled);
		});
	}

	// Token: 0x06002B78 RID: 11128 RVA: 0x00158D10 File Offset: 0x00156F10
	private void newButton(string pID, UnityAction pAction, DebugIconOptionAction pCheckIcon)
	{
		Transform tButton = base.transform.FindRecursive(pID);
		tButton.GetComponent<Button>().onClick.AddListener(pAction);
		this.list_actions.Add(pCheckIcon);
		this.list_icons.Add(tButton.GetComponent<Image>());
	}

	// Token: 0x06002B79 RID: 11129 RVA: 0x00158D58 File Offset: 0x00156F58
	public bool isValueAverage()
	{
		return this.show_averages;
	}

	// Token: 0x06002B7A RID: 11130 RVA: 0x00158D60 File Offset: 0x00156F60
	public bool isState(DebugToolState pState)
	{
		return this.state == pState;
	}

	// Token: 0x06002B7B RID: 11131 RVA: 0x00158D6C File Offset: 0x00156F6C
	private void updateIcons()
	{
		for (int i = 0; i < this.list_actions.Count; i++)
		{
			DebugIconOptionAction debugIconOptionAction = this.list_actions[i];
			Image tImage = this.list_icons[i];
			debugIconOptionAction(tImage);
		}
	}

	// Token: 0x06002B7C RID: 11132 RVA: 0x00158DAE File Offset: 0x00156FAE
	private void checkIcon(Image pImageIcon, bool pValue)
	{
		if (pValue)
		{
			pImageIcon.color = Color.white;
			return;
		}
		pImageIcon.color = Toolbox.color_transparent_grey;
	}

	// Token: 0x06002B7D RID: 11133 RVA: 0x00158DD0 File Offset: 0x00156FD0
	private void switchTool(int pIndex)
	{
		string tID = this.dropdown.options[pIndex].text;
		DebugToolAsset tAsset = AssetManager.debug_tool_library.get(tID);
		this.setAsset(tAsset);
	}

	// Token: 0x06002B7E RID: 11134 RVA: 0x00158E08 File Offset: 0x00157008
	public void setAsset(DebugToolAsset pAsset)
	{
		this.asset = pAsset;
		this.type = this.asset.type;
		this.benchmark_icons.gameObject.SetActive(this.asset.show_benchmark_buttons);
		if (this.asset.action_start != null)
		{
			this.asset.action_start(this);
		}
	}

	// Token: 0x06002B7F RID: 11135 RVA: 0x00158E68 File Offset: 0x00157068
	private void Update()
	{
		if (SmoothLoader.isLoading())
		{
			return;
		}
		this.updateIcons();
		double tCur = World.world.getCurSessionTime();
		if (tCur < this.last_update_timestamp + (double)this.asset.update_timeout)
		{
			return;
		}
		if (this.paused)
		{
			return;
		}
		if (this.asset.action_update != null)
		{
			this.asset.action_update(this);
		}
		this.clearTexts();
		string text = this.dropdown.captionText.text;
		this.last_update_timestamp = tCur;
		if (this.asset.action_1 != null)
		{
			this.asset.action_1(this);
		}
		if (this.asset.action_2 != null)
		{
			this.asset.action_2(this);
		}
		this.updateSize();
		this.pool_texts.disableInactive();
		base.StartCoroutine(this.updateSizeAfterFrame());
	}

	// Token: 0x06002B80 RID: 11136 RVA: 0x00158F44 File Offset: 0x00157144
	public IEnumerator updateSizeAfterFrame()
	{
		yield return CoroutineHelper.wait_for_end_of_frame;
		this.updateSize();
		yield break;
	}

	// Token: 0x06002B81 RID: 11137 RVA: 0x00158F54 File Offset: 0x00157154
	private void updateSize()
	{
		float tWidth = LayoutUtility.GetPreferredWidth(this.transform_texts.GetComponent<RectTransform>()) * 1.2f;
		float tHeight = LayoutUtility.GetPreferredHeight(this.transform_texts.GetComponent<RectTransform>()) + 40f;
		if (tWidth < 126f)
		{
			tWidth = 126f;
		}
		if (tHeight < 60f)
		{
			tHeight = 60f;
		}
		base.GetComponent<RectTransform>().sizeDelta = new Vector2(tWidth, tHeight);
	}

	// Token: 0x06002B82 RID: 11138 RVA: 0x00158FBD File Offset: 0x001571BD
	public void clickSortByName()
	{
		this.sort_by_names = !this.sort_by_names;
		this.sort_by_values = !this.sort_by_names;
	}

	// Token: 0x06002B83 RID: 11139 RVA: 0x00158FDD File Offset: 0x001571DD
	public void clickSortByValues()
	{
		this.sort_by_values = !this.sort_by_values;
		this.sort_by_names = !this.sort_by_values;
	}

	// Token: 0x06002B84 RID: 11140 RVA: 0x00159000 File Offset: 0x00157200
	public int kingdomSorter(Kingdom k1, Kingdom k2)
	{
		return k2.units.Count.CompareTo(k1.units.Count);
	}

	// Token: 0x06002B85 RID: 11141 RVA: 0x0015902C File Offset: 0x0015722C
	public int citySorter(City c1, City c2)
	{
		return c2.getPopulationPeople().CompareTo(c1.getPopulationPeople());
	}

	// Token: 0x06002B86 RID: 11142 RVA: 0x00159050 File Offset: 0x00157250
	internal void setText(string pT1, object pT2, float pBarValue = 0f, bool pShowBar = false, long pCounter = 0L, bool pShowCounter = false, bool pShowMax = false, string pMaxValue = "")
	{
		DebugToolTextElement tElement = this.pool_texts.getNext();
		string tStringRight = (pT2 == null) ? "-" : pT2.ToString();
		if (pT2 != null)
		{
			if (pShowCounter && this.show_counter && (this.asset.split_benchmark || this.asset.show_last_count))
			{
				tStringRight = pCounter.ToString() + " | " + tStringRight;
			}
			if (pShowMax)
			{
				tStringRight = pMaxValue + " | " + tStringRight;
			}
		}
		tElement.text_left.text = pT1;
		tElement.text_right.text = tStringRight;
		this.textCount++;
		if (!pShowBar)
		{
			tElement.text_bar.gameObject.SetActive(false);
			return;
		}
		tElement.text_bar.gameObject.SetActive(true);
		if (pBarValue > 100f)
		{
			pBarValue = 101f;
		}
		float tWidth = pBarValue * 0.5f;
		tElement.text_bar.GetComponent<RectTransform>().sizeDelta = new Vector2(tWidth, 4.2f);
		if (pBarValue > 70f && pBarValue != 100f)
		{
			tElement.text_bar.color = Toolbox.color_debug_bar_red;
			return;
		}
		tElement.text_bar.color = Toolbox.color_debug_bar_blue;
	}

	// Token: 0x06002B87 RID: 11143 RVA: 0x00159182 File Offset: 0x00157382
	internal void setSeparator()
	{
		DebugToolTextElement next = this.pool_texts.getNext();
		next.text_left.text = string.Empty;
		next.text_right.text = string.Empty;
		next.text_bar.gameObject.SetActive(false);
	}

	// Token: 0x06002B88 RID: 11144 RVA: 0x001591BF File Offset: 0x001573BF
	private void clearTexts()
	{
		this.textCount = 0;
		this.pool_texts.clear(false);
	}

	// Token: 0x06002B89 RID: 11145 RVA: 0x001591D4 File Offset: 0x001573D4
	public void clickClose()
	{
		Object.Destroy(base.gameObject, 0.01f);
	}

	// Token: 0x06002B8A RID: 11146 RVA: 0x001591E8 File Offset: 0x001573E8
	public void clickDuplicate()
	{
		int tX = (int)base.transform.localPosition.x + 126 + 2;
		int tY = (int)base.transform.localPosition.y;
		DebugConfig.createTool(this.asset.id, tX, tY, -1);
	}

	// Token: 0x04002151 RID: 8529
	public const int DT_WIDTH = 126;

	// Token: 0x04002152 RID: 8530
	public const int DT_HEIGHT = 60;

	// Token: 0x04002153 RID: 8531
	protected ObjectPoolGenericMono<DebugToolTextElement> pool_texts;

	// Token: 0x04002154 RID: 8532
	public DebugToolTextElement element_prefab;

	// Token: 0x04002155 RID: 8533
	internal int textCount;

	// Token: 0x04002156 RID: 8534
	public Dropdown dropdown;

	// Token: 0x04002157 RID: 8535
	internal bool sort_order_reversed;

	// Token: 0x04002158 RID: 8536
	internal bool sort_by_names;

	// Token: 0x04002159 RID: 8537
	internal bool sort_by_values = true;

	// Token: 0x0400215A RID: 8538
	internal bool show_averages = true;

	// Token: 0x0400215B RID: 8539
	internal bool percentage_slowest;

	// Token: 0x0400215C RID: 8540
	internal bool hide_zeroes = true;

	// Token: 0x0400215D RID: 8541
	internal bool show_counter = true;

	// Token: 0x0400215E RID: 8542
	internal bool show_max = true;

	// Token: 0x0400215F RID: 8543
	internal DebugToolState state = DebugToolState.FrameBudget;

	// Token: 0x04002160 RID: 8544
	public DebugToolType type;

	// Token: 0x04002161 RID: 8545
	internal bool paused;

	// Token: 0x04002162 RID: 8546
	internal DebugToolAsset asset;

	// Token: 0x04002163 RID: 8547
	[HideInInspector]
	public DebugDropdown active_dropdown;

	// Token: 0x04002164 RID: 8548
	private double last_update_timestamp;

	// Token: 0x04002165 RID: 8549
	private List<DebugIconOptionAction> list_actions = new List<DebugIconOptionAction>();

	// Token: 0x04002166 RID: 8550
	private List<Image> list_icons = new List<Image>();

	// Token: 0x04002167 RID: 8551
	private Transform transform_texts;

	// Token: 0x04002168 RID: 8552
	private Transform benchmark_icons;

	// Token: 0x04002169 RID: 8553
	private string _latest_text;
}
