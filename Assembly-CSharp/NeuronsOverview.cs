using System;
using System.Collections.Generic;
using System.Globalization;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x020007AD RID: 1965
public class NeuronsOverview : UnitElement, IInitializePotentialDragHandler, IEventSystemHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	// Token: 0x06003E2E RID: 15918 RVA: 0x001B117E File Offset: 0x001AF37E
	private void Start()
	{
		NeuronsOverview.instance = this;
	}

	// Token: 0x06003E2F RID: 15919 RVA: 0x001B1188 File Offset: 0x001AF388
	protected override void Awake()
	{
		base.Awake();
		this._pool_neurons = new ObjectPoolGenericMono<NeuronElement>(this._prefab_neuron, this._parent_neurons);
		this._pool_impulses = new ObjectPoolGenericMono<NerveImpulseElement>(this._prefab_nerve_impulse, this._parent_nerve_impulses);
		this._pool_axons = new ObjectPoolGenericMono<AxonElement>(this._prefab_axon, this._parent_axons);
	}

	// Token: 0x06003E30 RID: 15920 RVA: 0x001B11E0 File Offset: 0x001AF3E0
	public void OnInitializePotentialDrag(PointerEventData eventData)
	{
		eventData.useDragThreshold = false;
		this._last_mouse_delta = Vector2.zero;
	}

	// Token: 0x06003E31 RID: 15921 RVA: 0x001B11F4 File Offset: 0x001AF3F4
	public void OnBeginDrag(PointerEventData eventData)
	{
		this._offset_x = (this._offset_target_x = 0f);
		this._offset_y = (this._offset_target_y = 0f);
		this.clearHighlight();
		Tooltip.hideTooltipNow();
	}

	// Token: 0x06003E32 RID: 15922 RVA: 0x001B1234 File Offset: 0x001AF434
	private void highlightAllAxons(float pLight)
	{
		foreach (AxonElement tAxon in this._pool_axons.getListTotal())
		{
			if (tAxon.mod_light <= pLight)
			{
				tAxon.mod_light = pLight;
			}
		}
	}

	// Token: 0x06003E33 RID: 15923 RVA: 0x001B1290 File Offset: 0x001AF490
	public void OnDrag(PointerEventData eventData)
	{
		this._is_dragging = true;
		Vector2 tMouseDelta = eventData.delta;
		if (tMouseDelta.magnitude > this._last_mouse_delta.magnitude)
		{
			this.highlightAllAxons(0.35f);
		}
		this._last_mouse_delta = tMouseDelta;
		this._offset_x = -tMouseDelta.y * 0.46f;
		this._offset_y = tMouseDelta.x * 0.46f;
		this.updateNeuronsVisual();
	}

	// Token: 0x06003E34 RID: 15924 RVA: 0x001B12FC File Offset: 0x001AF4FC
	public void OnEndDrag(PointerEventData eventData)
	{
		this._is_dragging = false;
		Vector2 tMouseDelta = eventData.delta;
		this._offset_target_x += -tMouseDelta.y * 0.005f;
		this._offset_target_y += tMouseDelta.x * 0.005f;
		if (Mathf.Abs(this._offset_target_x) > 0.7f || Mathf.Abs(this._offset_target_y) > 0.7f)
		{
			if (Mathf.Abs(this._offset_target_x) > Mathf.Abs(this._offset_target_y))
			{
				this._offset_target_y = this._offset_target_y / Mathf.Abs(this._offset_target_x) * 0.7f;
			}
			else
			{
				this._offset_target_x = this._offset_target_x / Mathf.Abs(this._offset_target_y) * 0.7f;
			}
		}
		this._offset_target_x = Mathf.Clamp(this._offset_target_x, -0.7f, 0.7f);
		this._offset_target_y = Mathf.Clamp(this._offset_target_y, -0.7f, 0.7f);
		this.highlightAllAxons(1f);
		this.fireImpulsesEverywhere();
	}

	// Token: 0x06003E35 RID: 15925 RVA: 0x001B140C File Offset: 0x001AF60C
	private void fireImpulsesEverywhere()
	{
		for (int i = 0; i < this._neurons.Count; i++)
		{
			NeuronElement tNeuron = this._neurons[i];
			this.fireImpulseWaveFromHere(tNeuron, 2);
		}
	}

	// Token: 0x06003E36 RID: 15926 RVA: 0x001B1444 File Offset: 0x001AF644
	private void highlightNeuron(NeuronElement pHighlighted = null)
	{
		foreach (NeuronElement tNeuronElement in this._neurons)
		{
			if (!(tNeuronElement == pHighlighted) && tNeuronElement.highlighted)
			{
				tNeuronElement.highlighted = false;
				Tooltip.hideTooltipNow();
			}
		}
		if (pHighlighted != null)
		{
			pHighlighted.setHighlighted();
		}
	}

	// Token: 0x06003E37 RID: 15927 RVA: 0x001B14B8 File Offset: 0x001AF6B8
	private NeuronElement getClosestNeuronToCursor()
	{
		NeuronElement tResult = null;
		float tBestDist = float.MaxValue;
		Vector2 tCursorPosition = Input.mousePosition;
		foreach (NeuronElement tNeuron in this._neurons)
		{
			Vector2 tPos = tNeuron.transform.position;
			float tDist = Vector2.Distance(tCursorPosition, tPos);
			if (tDist <= 40f)
			{
				if (tNeuron == this._active_neuron)
				{
					return tNeuron;
				}
				if (tDist < tBestDist)
				{
					tBestDist = tDist;
					tResult = tNeuron;
				}
			}
		}
		return tResult;
	}

	// Token: 0x06003E38 RID: 15928 RVA: 0x001B1564 File Offset: 0x001AF764
	private void prepareAxons()
	{
		int tTotalNeurons = this._neurons.Count;
		float tMaximumAxonDistance = 250f / Mathf.Sqrt((float)tTotalNeurons) * 1.5f;
		for (int i = 0; i < this._neurons.Count - 1; i++)
		{
			NeuronElement tNeuron = this._neurons[i];
			if (!tNeuron.isCenter())
			{
				for (int j = i + 1; j < this._neurons.Count; j++)
				{
					NeuronElement tNeuron2 = this._neurons[j];
					if (!tNeuron2.isCenter() && Vector3.Distance(tNeuron.transform.localPosition, tNeuron2.transform.localPosition) <= tMaximumAxonDistance)
					{
						this.makeAxon(tNeuron, tNeuron2);
					}
				}
			}
		}
	}

	// Token: 0x06003E39 RID: 15929 RVA: 0x001B1620 File Offset: 0x001AF820
	private AxonElement makeAxon(NeuronElement pNeuron1, NeuronElement pNeuron2)
	{
		AxonElement tAxon = this._pool_axons.getNext();
		tAxon.neuron_1 = pNeuron1;
		tAxon.neuron_2 = pNeuron2;
		pNeuron1.addConnection(pNeuron2, tAxon);
		pNeuron2.addConnection(pNeuron1, tAxon);
		return tAxon;
	}

	// Token: 0x06003E3A RID: 15930 RVA: 0x001B1658 File Offset: 0x001AF858
	private void checkActorDecisions()
	{
		DecisionHelper.runSimulationForMindTab(this.actor);
		this._decision_counter = DecisionHelper.decision_system.getCounter();
		this._decision_assets = DecisionHelper.decision_system.getActions();
	}

	// Token: 0x06003E3B RID: 15931 RVA: 0x001B1688 File Offset: 0x001AF888
	private void updateNeuronsVisual()
	{
		Quaternion tCombinedRotation = Quaternion.Euler(this._offset_x, this._offset_y, 0f);
		foreach (NeuronElement tNeuronElement in this._neurons)
		{
			tNeuronElement.updateColorsAndTooltip();
			Vector3 tRotatedPosition = tCombinedRotation * tNeuronElement.transform.localPosition;
			tNeuronElement.transform.localPosition = tRotatedPosition;
			this.calculateNeuronDepth(tNeuronElement, 70f);
			this.updateNeuronColorAndScale(tNeuronElement);
		}
		this.sortNeuronsByDepth();
	}

	// Token: 0x06003E3C RID: 15932 RVA: 0x001B1728 File Offset: 0x001AF928
	private void updateNeuronImpulseAutoSpawn()
	{
		foreach (NeuronElement neuronElement in this._neurons)
		{
			neuronElement.updateSpawnTimer();
		}
	}

	// Token: 0x06003E3D RID: 15933 RVA: 0x001B1778 File Offset: 0x001AF978
	private void sortNeuronsByDepth()
	{
		foreach (NeuronElement neuronElement in this._neurons)
		{
			neuronElement.transform.SetAsLastSibling();
		}
		this._neurons.Sort((NeuronElement a, NeuronElement b) => a.render_depth.CompareTo(b.render_depth));
	}

	// Token: 0x06003E3E RID: 15934 RVA: 0x001B17F8 File Offset: 0x001AF9F8
	private void calculateNeuronDepth(NeuronElement pNeuronElement, float pRadius)
	{
		float zPosition = pNeuronElement.transform.localPosition.z;
		float tDepthFactor = Mathf.InverseLerp(-pRadius, pRadius, zPosition);
		pNeuronElement.render_depth = tDepthFactor;
	}

	// Token: 0x06003E3F RID: 15935 RVA: 0x001B1828 File Offset: 0x001AFA28
	private void updateNeuronColorAndScale(NeuronElement pElement)
	{
		if (!pElement.isDecisionEnabled())
		{
			Color tNeuronColor = Color.Lerp(this._color_neuron_disabled_back, this._color_neuron_disabled_front, pElement.render_depth);
			pElement.setColor(tNeuronColor);
		}
		else if (pElement.highlighted)
		{
			Color tNeuronColor2 = Color.Lerp(this._color_neuron_back, this._neuron_highlighted, pElement.render_depth);
			pElement.setColor(tNeuronColor2);
		}
		else
		{
			Color tNeuronColor3 = Color.Lerp(this._color_neuron_back, this._color_neuron_front, pElement.render_depth);
			pElement.setColor(tNeuronColor3);
		}
		float tScale = Mathf.Lerp(0.8f, 1.5f, pElement.render_depth);
		tScale *= pElement.scale_mod_spawn * pElement.bonus_scale;
		pElement.transform.localScale = new Vector3(tScale, tScale, tScale);
	}

	// Token: 0x06003E40 RID: 15936 RVA: 0x001B18E0 File Offset: 0x001AFAE0
	private void updateAxonPositions()
	{
		foreach (AxonElement tAxon in this._pool_axons.getListTotal())
		{
			tAxon.update();
			float tScaleMod = 1f;
			NeuronElement neuron_ = tAxon.neuron_1;
			NeuronElement tNeuron2 = tAxon.neuron_2;
			if (neuron_.highlighted || tNeuron2.highlighted)
			{
				tScaleMod = 6f;
			}
			Color tColorAxon = this._color_axon_default;
			if (tAxon.axon_center)
			{
				tColorAxon = this._color_axon_default_center;
				tScaleMod = 7f;
			}
			if (tAxon.mod_light > 0f)
			{
				Color tAxonColor = Color.Lerp(tColorAxon, this._color_light_axon, tAxon.mod_light);
				tAxon.image.color = tAxonColor;
			}
			else
			{
				tAxon.image.color = tColorAxon;
			}
			Vector2 tPos = neuron_.transform.localPosition;
			Vector2 tPos2 = tNeuron2.transform.localPosition;
			Vector2 tAxonPos = (tPos + tPos2) / 2f;
			tAxon.transform.localPosition = tAxonPos;
			float tDistance = Vector3.Distance(tPos, tPos2);
			tAxon.transform.localScale = new Vector3(tDistance, tScaleMod, 1f);
			Vector3 tDirection = tPos2 - tPos;
			float tAngle = Mathf.Atan2(tDirection.y, tDirection.x) * 57.29578f;
			tAxon.transform.rotation = Quaternion.Euler(0f, 0f, tAngle);
		}
	}

	// Token: 0x06003E41 RID: 15937 RVA: 0x001B1A88 File Offset: 0x001AFC88
	private void smoothOffsets()
	{
		this._offset_x = Mathf.Lerp(this._offset_x, this._offset_target_x, 0.1f);
		this._offset_y = Mathf.Lerp(this._offset_y, this._offset_target_y, 0.1f);
	}

	// Token: 0x06003E42 RID: 15938 RVA: 0x001B1AC4 File Offset: 0x001AFCC4
	internal void fireImpulseWaveFromHere(NeuronElement pNeuron, int pWaves = 4)
	{
		if (!pNeuron.isDecisionEnabled())
		{
			return;
		}
		foreach (NeuronElement tConnected in pNeuron.connected_neurons)
		{
			if (tConnected.isDecisionEnabled())
			{
				this.fireImpulse(pNeuron, tConnected, pWaves);
			}
		}
	}

	// Token: 0x06003E43 RID: 15939 RVA: 0x001B1B2C File Offset: 0x001AFD2C
	private void fireImpulseFrom(NeuronElement pPresynapticNeuron, int pWave, NeuronElement pIgnoreNeuron = null)
	{
		if (pPresynapticNeuron.connected_neurons.Count == 0)
		{
			return;
		}
		NeuronElement tPostsynapticNeuron;
		if (pIgnoreNeuron == null)
		{
			tPostsynapticNeuron = pPresynapticNeuron.connected_neurons.GetRandom<NeuronElement>();
		}
		else
		{
			using (ListPool<NeuronElement> tTempPool = new ListPool<NeuronElement>())
			{
				foreach (NeuronElement tNeuron in pPresynapticNeuron.connected_neurons)
				{
					if (!(pIgnoreNeuron == tNeuron))
					{
						tTempPool.Add(tNeuron);
					}
				}
				if (tTempPool.Count == 0)
				{
					return;
				}
				tPostsynapticNeuron = tTempPool.GetRandom<NeuronElement>();
			}
		}
		if (tPostsynapticNeuron == null)
		{
			return;
		}
		this.fireImpulse(pPresynapticNeuron, tPostsynapticNeuron, pWave);
	}

	// Token: 0x06003E44 RID: 15940 RVA: 0x001B1BF0 File Offset: 0x001AFDF0
	internal void fireImpulse(NeuronElement pPresynapticNeuron, NeuronElement pPostsynapticNeuron, int pWave)
	{
		NerveImpulseElement tNewImpulse = this._pool_impulses.getNext();
		tNewImpulse.energize(pPresynapticNeuron, pPostsynapticNeuron, pWave);
		pPresynapticNeuron.spawnImpulseFromHere();
		this._active_impulses.Add(tNewImpulse);
	}

	// Token: 0x06003E45 RID: 15941 RVA: 0x001B1C24 File Offset: 0x001AFE24
	private void updateImpulses()
	{
		for (int i = this._active_impulses.Count - 1; i >= 0; i--)
		{
			NerveImpulseElement tImpulse = this._active_impulses[i];
			ImpulseReachResult tIsMoving = tImpulse.moveTowardsNextNeuron();
			NeuronElement tPostsynaptic = tImpulse.postsynaptic_neuron;
			NeuronElement tPresynaptic = tImpulse.presynaptic_neuron;
			switch (tIsMoving)
			{
			case ImpulseReachResult.Done:
				if (tPostsynaptic != null)
				{
					tPostsynaptic.receiveImpulse();
				}
				this._active_impulses.RemoveAt(i);
				this._pool_impulses.release(tImpulse, true);
				break;
			case ImpulseReachResult.Split:
				if (tPostsynaptic != null)
				{
					tPostsynaptic.receiveImpulse();
				}
				this.fireImpulseFrom(tPostsynaptic, tImpulse.wave, tPresynaptic);
				break;
			}
		}
	}

	// Token: 0x06003E46 RID: 15942 RVA: 0x001B1CBC File Offset: 0x001AFEBC
	private void Update()
	{
		if (!this._is_dragging)
		{
			this.smoothOffsets();
			if (InputHelpers.mouseSupported)
			{
				this._active_neuron = this.getHighlightedNeuron();
				this.highlightNeuron(this._active_neuron);
			}
			if (InputHelpers.mouseSupported || this._latest_touched_neuron == null || !Tooltip.isShowingFor(this._latest_touched_neuron))
			{
				this.updateNeuronsVisual();
			}
		}
		this.updateNeuronImpulseAutoSpawn();
		this.updateAxonPositions();
		this.updateImpulseSpawn();
		this.updateImpulses();
	}

	// Token: 0x06003E47 RID: 15943 RVA: 0x001B1D3C File Offset: 0x001AFF3C
	private NeuronElement getHighlightedNeuron()
	{
		if (this._is_dragging)
		{
			return null;
		}
		if (this._offset_x > 1.05f || this._offset_x < -1.05f)
		{
			return null;
		}
		if (this._offset_y > 1.05f || this._offset_y < -1.05f)
		{
			return null;
		}
		return this.getClosestNeuronToCursor();
	}

	// Token: 0x06003E48 RID: 15944 RVA: 0x001B1D91 File Offset: 0x001AFF91
	internal void startNewWhat()
	{
		this._text_phrases.startNewWhat();
	}

	// Token: 0x06003E49 RID: 15945 RVA: 0x001B1DA0 File Offset: 0x001AFFA0
	private void updateImpulseSpawn()
	{
		foreach (NeuronElement tNeuronElement in this._neurons)
		{
			if (tNeuronElement.hasDecisionSet() && tNeuronElement.readyToSpawnImpulse())
			{
				this.fireImpulseFrom(tNeuronElement, 1, null);
			}
		}
	}

	// Token: 0x06003E4A RID: 15946 RVA: 0x001B1E08 File Offset: 0x001B0008
	private void initStartPositions()
	{
		for (int i = 0; i < this._decision_counter; i++)
		{
			DecisionAsset tDecision = this._decision_assets[i];
			NeuronElement tNeuron = this._pool_neurons.getNext();
			tNeuron.setupDecisionAndActor(tDecision, this.actor);
			Vector3 tPos = this.getPositionOnSphere(i, this._decision_counter);
			tNeuron.transform.localPosition = tPos;
			this._neurons.Add(tNeuron);
		}
		this.updateNeuronsVisual();
	}

	// Token: 0x06003E4B RID: 15947 RVA: 0x001B1E74 File Offset: 0x001B0074
	private void loadLastDecisionForCenter()
	{
		NeuronElement tSphereNeuron = null;
		string tLastDecisionID = this.actor.getLastDecisionForMindOverview();
		if (!string.IsNullOrEmpty(tLastDecisionID))
		{
			foreach (NeuronElement tElement in this._neurons)
			{
				if (tElement.decision.id == tLastDecisionID)
				{
					tSphereNeuron = tElement;
					break;
				}
			}
		}
		this._last_activated_neuron = this._pool_neurons.getNext();
		this._last_activated_neuron.transform.localPosition = Vector3.zero;
		this._last_activated_neuron.image.sprite = SpriteTextureLoader.getSprite("ui/icons/iconBrain");
		this._last_activated_neuron.bonus_scale = 1.5f;
		this._last_activated_neuron.setCenter(true);
		this._last_activated_neuron.actor = this.actor;
		this._neurons.Add(this._last_activated_neuron);
		if (tSphereNeuron != null)
		{
			this.makeAxon(tSphereNeuron, this._last_activated_neuron).axon_center = true;
		}
	}

	// Token: 0x06003E4C RID: 15948 RVA: 0x001B1F88 File Offset: 0x001B0188
	private Vector3 getPositionOnSphere(int pNeuronIndex, int pTotalNeurons)
	{
		float tPhi = Mathf.Acos(1f - (float)(2 * (pNeuronIndex + 1)) / (float)pTotalNeurons);
		float tTheta = 3.1415927f * (1f + Mathf.Sqrt(5f)) * (float)pNeuronIndex;
		float x = 70f * Mathf.Cos(tTheta) * Mathf.Sin(tPhi);
		float tY = 70f * Mathf.Sin(tTheta) * Mathf.Sin(tPhi);
		float tZ = 70f * Mathf.Cos(tPhi);
		return new Vector3(x, tY, tZ);
	}

	// Token: 0x06003E4D RID: 15949 RVA: 0x001B2000 File Offset: 0x001B0200
	private void clearMind()
	{
		foreach (NeuronElement neuronElement in this._neurons)
		{
			neuronElement.clear();
		}
		this._active_impulses.Clear();
		this._pool_axons.clear(true);
		this._pool_neurons.clear(true);
		this._pool_impulses.clear(true);
		this._neurons.Clear();
		foreach (AxonElement axonElement in this._pool_axons.getListTotal())
		{
			axonElement.clear();
		}
	}

	// Token: 0x06003E4E RID: 15950 RVA: 0x001B20C8 File Offset: 0x001B02C8
	protected override void OnEnable()
	{
		base.OnEnable();
		this._mind_main.transform.DOKill(false);
		this._mind_main.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
		this._mind_main.transform.DOScale(1f, 0.6f).SetEase(Ease.OutBack);
		this.checkActorDecisions();
		this.clearMind();
		this.initStartPositions();
		this.loadLastDecisionForCenter();
		this.prepareAxons();
		this._is_dragging = false;
	}

	// Token: 0x06003E4F RID: 15951 RVA: 0x001B2158 File Offset: 0x001B0358
	internal bool isDragging()
	{
		return this._is_dragging;
	}

	// Token: 0x06003E50 RID: 15952 RVA: 0x001B2160 File Offset: 0x001B0360
	internal void clearHighlight()
	{
		if (this._active_neuron == null)
		{
			return;
		}
		this._active_neuron.highlighted = false;
		this._active_neuron = null;
	}

	// Token: 0x06003E51 RID: 15953 RVA: 0x001B2184 File Offset: 0x001B0384
	public static void debugTool(DebugTool pTool)
	{
		NeuronsOverview neuronsOverview = NeuronsOverview.instance;
		if (neuronsOverview == null)
		{
			return;
		}
		neuronsOverview.debug(pTool);
	}

	// Token: 0x06003E52 RID: 15954 RVA: 0x001B2198 File Offset: 0x001B0398
	public void debug(DebugTool pTool)
	{
		pTool.setText("offset_target_x:", NeuronsOverview.getFloat(this._offset_target_x), 0f, false, 0L, false, false, "");
		pTool.setText("offset_target_y:", NeuronsOverview.getFloat(this._offset_target_y), 0f, false, 0L, false, false, "");
		pTool.setSeparator();
		pTool.setText("offset_x:", NeuronsOverview.getFloat(this._offset_x), 0f, false, 0L, false, false, "");
		pTool.setText("offset_y:", NeuronsOverview.getFloat(this._offset_y), 0f, false, 0L, false, false, "");
		pTool.setSeparator();
		Quaternion tCombinedRotation = Quaternion.Euler(this._offset_x, this._offset_y, 0f);
		pTool.setText("combined_rotation.x:", NeuronsOverview.getFloat(tCombinedRotation.x), 0f, false, 0L, false, false, "");
		pTool.setText("combined_rotation.y:", NeuronsOverview.getFloat(tCombinedRotation.y), 0f, false, 0L, false, false, "");
		pTool.setText("combined_rotation.z:", NeuronsOverview.getFloat(tCombinedRotation.z), 0f, false, 0L, false, false, "");
		pTool.setText("combined_rotation.w:", NeuronsOverview.getFloat(tCombinedRotation.w), 0f, false, 0L, false, false, "");
		pTool.setSeparator();
		pTool.setText("is_dragging:", this._is_dragging, 0f, false, 0L, false, false, "");
		pTool.setText("last_mouse_delta:", this._last_mouse_delta, 0f, false, 0L, false, false, "");
		pTool.setSeparator();
		pTool.setText("decisions:", this._decision_counter, 0f, false, 0L, false, false, "");
		pTool.setSeparator();
		pTool.setText("neuron selected:", this._active_neuron, 0f, false, 0L, false, false, "");
	}

	// Token: 0x06003E53 RID: 15955 RVA: 0x001B2394 File Offset: 0x001B0594
	public static string getFloat(float pFloat)
	{
		if (pFloat < 0.001f && pFloat > -0.001f)
		{
			return pFloat.ToString("F6", CultureInfo.InvariantCulture);
		}
		if (pFloat > 0f)
		{
			return "<color=#75D53A>" + pFloat.ToString("F6", CultureInfo.InvariantCulture) + "</color>";
		}
		return "<color=#DB2920>" + pFloat.ToString("F6", CultureInfo.InvariantCulture) + "</color>";
	}

	// Token: 0x06003E54 RID: 15956 RVA: 0x001B240C File Offset: 0x001B060C
	public void setLatestTouched(NeuronElement pNeuron)
	{
		this._latest_touched_neuron = pNeuron;
	}

	// Token: 0x06003E55 RID: 15957 RVA: 0x001B2418 File Offset: 0x001B0618
	public void switchAllNeurons()
	{
		if (!this.isAnyEnabled())
		{
			this._all_state = true;
		}
		else if (this.isAllEnabled())
		{
			this._all_state = false;
		}
		else
		{
			this._all_state = !this._all_state;
		}
		foreach (NeuronElement tNeuron in this._neurons)
		{
			if (tNeuron.hasDecisionSet())
			{
				this.actor.setDecisionState(tNeuron.decision.decision_index, this._all_state);
			}
		}
		this.fireImpulsesEverywhere();
	}

	// Token: 0x06003E56 RID: 15958 RVA: 0x001B24C0 File Offset: 0x001B06C0
	public bool getAllState()
	{
		return this._all_state;
	}

	// Token: 0x06003E57 RID: 15959 RVA: 0x001B24C8 File Offset: 0x001B06C8
	private bool isAnyEnabled()
	{
		foreach (NeuronElement tNeuron in this._neurons)
		{
			if (tNeuron.hasDecisionSet() && this.actor.isDecisionEnabled(tNeuron.decision.decision_index))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06003E58 RID: 15960 RVA: 0x001B253C File Offset: 0x001B073C
	private bool isAllEnabled()
	{
		foreach (NeuronElement tNeuron in this._neurons)
		{
			if (tNeuron.hasDecisionSet() && !this.actor.isDecisionEnabled(tNeuron.decision.decision_index))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x04002D38 RID: 11576
	private const float DRAGGING_SMOOTHING_TIME = 0.1f;

	// Token: 0x04002D39 RID: 11577
	private const float ROTATION_BOUNDS = 0.7f;

	// Token: 0x04002D3A RID: 11578
	private const float ROTATION_BOUNDS_MARGIN = 1.05f;

	// Token: 0x04002D3B RID: 11579
	private const float DRAG_SPEED = 0.46f;

	// Token: 0x04002D3C RID: 11580
	private const float DRAG_ROTATE_SPEED = 0.005f;

	// Token: 0x04002D3D RID: 11581
	private const float MIN_NEURON_CURSOR_DISTANCE = 40f;

	// Token: 0x04002D3E RID: 11582
	private const float RADIUS_NEURONS = 70f;

	// Token: 0x04002D3F RID: 11583
	private const float NEURON_SCALE_MIN = 0.8f;

	// Token: 0x04002D40 RID: 11584
	private const float NEURON_SCALE_MAX = 1.5f;

	// Token: 0x04002D41 RID: 11585
	private const float BASE_AXON_DISTANCE = 250f;

	// Token: 0x04002D42 RID: 11586
	private const float DISTANCE_SCALING_FACTOR = 1.5f;

	// Token: 0x04002D43 RID: 11587
	[SerializeField]
	private NeuronElement _prefab_neuron;

	// Token: 0x04002D44 RID: 11588
	[SerializeField]
	private NerveImpulseElement _prefab_nerve_impulse;

	// Token: 0x04002D45 RID: 11589
	[SerializeField]
	private AxonElement _prefab_axon;

	// Token: 0x04002D46 RID: 11590
	[SerializeField]
	private RectTransform _parent_axons;

	// Token: 0x04002D47 RID: 11591
	[SerializeField]
	private RectTransform _parent_nerve_impulses;

	// Token: 0x04002D48 RID: 11592
	[SerializeField]
	private RectTransform _parent_neurons;

	// Token: 0x04002D49 RID: 11593
	[SerializeField]
	private GameObject _mind_main;

	// Token: 0x04002D4A RID: 11594
	[SerializeField]
	private UnitTextManager _text_phrases;

	// Token: 0x04002D4B RID: 11595
	private ObjectPoolGenericMono<NeuronElement> _pool_neurons;

	// Token: 0x04002D4C RID: 11596
	private ObjectPoolGenericMono<NerveImpulseElement> _pool_impulses;

	// Token: 0x04002D4D RID: 11597
	private ObjectPoolGenericMono<AxonElement> _pool_axons;

	// Token: 0x04002D4E RID: 11598
	private List<NeuronElement> _neurons = new List<NeuronElement>();

	// Token: 0x04002D4F RID: 11599
	private NeuronElement _last_activated_neuron;

	// Token: 0x04002D50 RID: 11600
	private List<NerveImpulseElement> _active_impulses = new List<NerveImpulseElement>();

	// Token: 0x04002D51 RID: 11601
	private Color _color_neuron_disabled_front = Toolbox.makeColor("#111111");

	// Token: 0x04002D52 RID: 11602
	private Color _color_neuron_disabled_back = Toolbox.makeColor("#111111", 0.3f);

	// Token: 0x04002D53 RID: 11603
	private Color _color_neuron_back = Toolbox.makeColor("#A9A9A9", 0.3f);

	// Token: 0x04002D54 RID: 11604
	private Color _color_neuron_front = Toolbox.makeColor("#DDDDDD");

	// Token: 0x04002D55 RID: 11605
	private Color _color_axon_default = Toolbox.makeColor("#FFFFFF", 0.1f);

	// Token: 0x04002D56 RID: 11606
	private Color _color_axon_default_center = Toolbox.makeColor("#FF6666", 0.1f);

	// Token: 0x04002D57 RID: 11607
	private Color _color_light_axon = Toolbox.makeColor("#3AFFFF", 0.54f);

	// Token: 0x04002D58 RID: 11608
	private Color _neuron_highlighted = Toolbox.makeColor("#FFFFFF");

	// Token: 0x04002D59 RID: 11609
	private float _offset_target_x = -0.015f;

	// Token: 0x04002D5A RID: 11610
	private float _offset_target_y = 0.07f;

	// Token: 0x04002D5B RID: 11611
	private bool _is_dragging;

	// Token: 0x04002D5C RID: 11612
	private Vector2 _last_mouse_delta;

	// Token: 0x04002D5D RID: 11613
	private float _offset_x;

	// Token: 0x04002D5E RID: 11614
	private float _offset_y;

	// Token: 0x04002D5F RID: 11615
	private int _decision_counter;

	// Token: 0x04002D60 RID: 11616
	private DecisionAsset[] _decision_assets;

	// Token: 0x04002D61 RID: 11617
	public static NeuronsOverview instance;

	// Token: 0x04002D62 RID: 11618
	private NeuronElement _active_neuron;

	// Token: 0x04002D63 RID: 11619
	private NeuronElement _latest_touched_neuron;

	// Token: 0x04002D64 RID: 11620
	private bool _all_state = true;
}
