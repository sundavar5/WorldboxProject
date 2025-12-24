using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020007AC RID: 1964
public class NeuronElement : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler, IInitializePotentialDragHandler
{
	// Token: 0x06003E14 RID: 15892 RVA: 0x001B0C62 File Offset: 0x001AEE62
	private void Start()
	{
		this._neurons_overview = base.gameObject.GetComponentInParent<NeuronsOverview>();
		this.initClick();
		this.initTooltip();
	}

	// Token: 0x06003E15 RID: 15893 RVA: 0x001B0C84 File Offset: 0x001AEE84
	protected void initClick()
	{
		Button tButton;
		if (!base.TryGetComponent<Button>(out tButton))
		{
			return;
		}
		tButton.onClick.AddListener(new UnityAction(this.setPressed));
	}

	// Token: 0x06003E16 RID: 15894 RVA: 0x001B0CB4 File Offset: 0x001AEEB4
	protected void initTooltip()
	{
		TipButton tTipButton;
		if (!base.TryGetComponent<TipButton>(out tTipButton))
		{
			return;
		}
		Object.Destroy(tTipButton);
	}

	// Token: 0x06003E17 RID: 15895 RVA: 0x001B0CD4 File Offset: 0x001AEED4
	private void showTooltip()
	{
		if (this.isCenter())
		{
			Tooltip.show(this, "tip", new TooltipData
			{
				tip_name = "toggle_all_neurons",
				tip_description = "toggle_all_neurons_description"
			});
			return;
		}
		if (!this.hasDecisionSet())
		{
			return;
		}
		this._tooltip_data = new TooltipData
		{
			neuron = this,
			tooltip_scale = Mathf.Lerp(0.4f, 1f, this.render_depth)
		};
		Tooltip.show(this, "neuron", this._tooltip_data);
	}

	// Token: 0x06003E18 RID: 15896 RVA: 0x001B0D58 File Offset: 0x001AEF58
	public void setupDecisionAndActor(DecisionAsset pAsset, Actor pActor)
	{
		this.decision = pAsset;
		this.actor = pActor;
		this.image.sprite = this.decision.getSprite();
		this._spawn_interval = (float)this.decision.cooldown;
		this._spawn_timer = Randy.randomFloat(0f, this._spawn_interval);
	}

	// Token: 0x06003E19 RID: 15897 RVA: 0x001B0DB4 File Offset: 0x001AEFB4
	public void updateColorsAndTooltip()
	{
		if (!this.highlighted)
		{
			this.scale_mod_spawn -= Time.deltaTime * 2f;
			this.scale_mod_spawn = Mathf.Max(1f, this.scale_mod_spawn);
			return;
		}
		if (this.hasDecisionSet() && Tooltip.isShowingFor(this))
		{
			this._tooltip_data.tooltip_scale = Mathf.Lerp(0.4f, 1f, this.render_depth);
		}
	}

	// Token: 0x06003E1A RID: 15898 RVA: 0x001B0E28 File Offset: 0x001AF028
	public void updateSpawnTimer()
	{
		if (this._spawn_timer > 0f && this.isDecisionEnabled())
		{
			this._spawn_timer -= Time.deltaTime;
		}
	}

	// Token: 0x06003E1B RID: 15899 RVA: 0x001B0E54 File Offset: 0x001AF054
	public void spawnImpulseFromHere()
	{
		this.scale_mod_spawn = Math.Max(1.5f, this.scale_mod_spawn);
		this._spawn_timer = this._spawn_interval;
		foreach (AxonElement axonElement in this._axons)
		{
			axonElement.mod_light = 1f;
		}
	}

	// Token: 0x06003E1C RID: 15900 RVA: 0x001B0ECC File Offset: 0x001AF0CC
	public bool isDecisionEnabled()
	{
		return !this.hasDecisionSet() || this.actor.isDecisionEnabled(this.decision.decision_index);
	}

	// Token: 0x06003E1D RID: 15901 RVA: 0x001B0EEE File Offset: 0x001AF0EE
	public void setHighlighted()
	{
		if (this.highlighted)
		{
			return;
		}
		this.highlighted = true;
		this.scale_mod_spawn = 1.6f;
		this.showTooltip();
	}

	// Token: 0x06003E1E RID: 15902 RVA: 0x001B0F14 File Offset: 0x001AF114
	public void setPressed()
	{
		if (this._neurons_overview.isDragging())
		{
			return;
		}
		if (!this.hasDecisionSet() && !this.isCenter())
		{
			return;
		}
		this._neurons_overview.setLatestTouched(this);
		if (!InputHelpers.mouseSupported)
		{
			if (!Tooltip.isShowingFor(this))
			{
				this.showTooltip();
				return;
			}
		}
		else
		{
			this.showTooltip();
		}
		this.scale_mod_spawn = 1.6f;
		if (this.isCenter())
		{
			this.centerBrainClick();
		}
		else
		{
			this.actor.switchDecisionState(this.decision.decision_index);
		}
		this.actor.makeConfused(-1f, true);
		this._neurons_overview.fireImpulseWaveFromHere(this, 4);
		this._neurons_overview.startNewWhat();
		this.actor.updateStats();
		base.GetComponentInParent<StatsWindow>().updateStats();
		AchievementLibrary.mindless_husk.check(this.actor);
	}

	// Token: 0x06003E1F RID: 15903 RVA: 0x001B0FEA File Offset: 0x001AF1EA
	private void centerBrainClick()
	{
		this._neurons_overview.switchAllNeurons();
		if (this._neurons_overview.getAllState())
		{
			this.image.color = Toolbox.color_clear;
			return;
		}
		this.image.color = Toolbox.color_white;
	}

	// Token: 0x06003E20 RID: 15904 RVA: 0x001B102A File Offset: 0x001AF22A
	public void receiveImpulse()
	{
		this.scale_mod_spawn += 0.1f;
		this.scale_mod_spawn = Mathf.Min(1.5f, this.scale_mod_spawn);
	}

	// Token: 0x06003E21 RID: 15905 RVA: 0x001B1054 File Offset: 0x001AF254
	public bool readyToSpawnImpulse()
	{
		return this.isDecisionEnabled() && this._spawn_timer <= 0f;
	}

	// Token: 0x06003E22 RID: 15906 RVA: 0x001B1070 File Offset: 0x001AF270
	public int getSimulatedTimer()
	{
		return (int)this._spawn_timer;
	}

	// Token: 0x06003E23 RID: 15907 RVA: 0x001B1079 File Offset: 0x001AF279
	public void clear()
	{
		this.decision = null;
		this.actor = null;
		this.connected_neurons.Clear();
		this._axons.Clear();
		this._center = false;
	}

	// Token: 0x06003E24 RID: 15908 RVA: 0x001B10A6 File Offset: 0x001AF2A6
	public void setColor(Color pColor)
	{
		this.image.color = pColor;
	}

	// Token: 0x06003E25 RID: 15909 RVA: 0x001B10B4 File Offset: 0x001AF2B4
	public bool hasDecisionSet()
	{
		return this.decision != null;
	}

	// Token: 0x06003E26 RID: 15910 RVA: 0x001B10BF File Offset: 0x001AF2BF
	public void addConnection(NeuronElement pConnection, AxonElement pAxon)
	{
		this.connected_neurons.Add(pConnection);
		this._axons.Add(pAxon);
	}

	// Token: 0x06003E27 RID: 15911 RVA: 0x001B10D9 File Offset: 0x001AF2D9
	public void setCenter(bool pState)
	{
		this._center = pState;
	}

	// Token: 0x06003E28 RID: 15912 RVA: 0x001B10E2 File Offset: 0x001AF2E2
	public bool isCenter()
	{
		return this._center;
	}

	// Token: 0x06003E29 RID: 15913 RVA: 0x001B10EA File Offset: 0x001AF2EA
	public void OnInitializePotentialDrag(PointerEventData pEventData)
	{
		NeuronsOverview neurons_overview = this._neurons_overview;
		if (neurons_overview == null)
		{
			return;
		}
		neurons_overview.SendMessage("OnInitializePotentialDrag", pEventData);
	}

	// Token: 0x06003E2A RID: 15914 RVA: 0x001B1102 File Offset: 0x001AF302
	public void OnBeginDrag(PointerEventData pEventData)
	{
		NeuronsOverview neurons_overview = this._neurons_overview;
		if (neurons_overview == null)
		{
			return;
		}
		neurons_overview.SendMessage("OnBeginDrag", pEventData);
	}

	// Token: 0x06003E2B RID: 15915 RVA: 0x001B111A File Offset: 0x001AF31A
	public void OnDrag(PointerEventData pEventData)
	{
		NeuronsOverview neurons_overview = this._neurons_overview;
		if (neurons_overview == null)
		{
			return;
		}
		neurons_overview.SendMessage("OnDrag", pEventData);
	}

	// Token: 0x06003E2C RID: 15916 RVA: 0x001B1132 File Offset: 0x001AF332
	public void OnEndDrag(PointerEventData pEventData)
	{
		NeuronsOverview neurons_overview = this._neurons_overview;
		if (neurons_overview == null)
		{
			return;
		}
		neurons_overview.SendMessage("OnEndDrag", pEventData);
	}

	// Token: 0x04002D26 RID: 11558
	[SerializeField]
	public Image image;

	// Token: 0x04002D27 RID: 11559
	private const float SCALE_HIGHLIGHTED = 1.6f;

	// Token: 0x04002D28 RID: 11560
	private const float SCALE_SPAWN_IMPULSE = 1.5f;

	// Token: 0x04002D29 RID: 11561
	private const float SCALE_RECEIVE_IMPULSE_INCREASE = 0.1f;

	// Token: 0x04002D2A RID: 11562
	private const float SCALE_NORMAL = 1f;

	// Token: 0x04002D2B RID: 11563
	internal float render_depth;

	// Token: 0x04002D2C RID: 11564
	internal bool highlighted;

	// Token: 0x04002D2D RID: 11565
	internal List<NeuronElement> connected_neurons = new List<NeuronElement>();

	// Token: 0x04002D2E RID: 11566
	internal float scale_mod_spawn = 1f;

	// Token: 0x04002D2F RID: 11567
	internal float bonus_scale = 1f;

	// Token: 0x04002D30 RID: 11568
	internal DecisionAsset decision;

	// Token: 0x04002D31 RID: 11569
	internal Actor actor;

	// Token: 0x04002D32 RID: 11570
	private float _spawn_interval;

	// Token: 0x04002D33 RID: 11571
	private float _spawn_timer;

	// Token: 0x04002D34 RID: 11572
	private List<AxonElement> _axons = new List<AxonElement>();

	// Token: 0x04002D35 RID: 11573
	private NeuronsOverview _neurons_overview;

	// Token: 0x04002D36 RID: 11574
	private TooltipData _tooltip_data;

	// Token: 0x04002D37 RID: 11575
	private bool _center;
}
