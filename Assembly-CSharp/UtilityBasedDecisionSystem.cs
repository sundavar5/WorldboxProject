using System;
using System.Collections.Generic;

// Token: 0x020003EE RID: 1006
public class UtilityBasedDecisionSystem
{
	// Token: 0x060022E8 RID: 8936 RVA: 0x001234A8 File Offset: 0x001216A8
	public UtilityBasedDecisionSystem()
	{
		int i = 0;
		int tLength = this._priority_array.Length;
		while (i < tLength)
		{
			this._priority_array[i] = new DecisionAsset[1024];
			this._priority_array_counters[i] = 0;
			i++;
		}
	}

	// Token: 0x060022E9 RID: 8937 RVA: 0x0012356C File Offset: 0x0012176C
	public DecisionAsset useOn(Actor pActor, bool pGameplay = true)
	{
		this.clear();
		ActorAsset tActorAsset = pActor.asset;
		if (pActor.isAbleToSkipPriorityLevels())
		{
			this._do_priority_levels = Randy.randomChance(0.8f);
		}
		else
		{
			this._do_priority_levels = true;
		}
		this.registerBasicDecisionLists(pActor, pGameplay);
		if (tActorAsset.hasDecisions())
		{
			this.registerDecisionArray(pActor, tActorAsset.getDecisions(), tActorAsset.decisions_counter, pGameplay);
		}
		if (pActor.decisions_counter > 0)
		{
			this.registerDecisionArray(pActor, pActor.decisions, pActor.decisions_counter, pGameplay);
		}
		this.calculateFactors(pActor);
		if (this._counter_possible == 0)
		{
			return null;
		}
		if (!pGameplay)
		{
			this.calculateChances(1f);
		}
		DecisionAsset tBestAction = this.chooseBestAction(1f);
		if (pGameplay)
		{
			pActor.setDecisionCooldown(tBestAction);
		}
		return tBestAction;
	}

	// Token: 0x060022EA RID: 8938 RVA: 0x00123620 File Offset: 0x00121820
	private void registerBasicDecisionLists(Actor pActor, bool pGameplay)
	{
		if (pActor.asset.is_boat)
		{
			return;
		}
		DecisionsLibrary tLib = AssetManager.decisions_library;
		if (pActor.isAnimal())
		{
			this.registerDecisionArray(pActor, tLib.list_only_animal, -1, pGameplay);
		}
		else if (pActor.isKingdomCiv())
		{
			this.registerDecisionArray(pActor, tLib.list_only_civ, -1, pGameplay);
			if (pActor.hasCity())
			{
				this.registerDecisionArray(pActor, tLib.list_only_city, -1, pGameplay);
			}
		}
		if (pActor.isBaby())
		{
			this.registerDecisionArray(pActor, tLib.list_only_children, -1, pGameplay);
		}
		this.registerDecisionArray(pActor, tLib.list_others, -1, pGameplay);
	}

	// Token: 0x060022EB RID: 8939 RVA: 0x001236AE File Offset: 0x001218AE
	private void registerDecisionArray(Actor pActor, DecisionAsset[] pList, int pLength = -1, bool pGameplay = true)
	{
		if (pLength == -1)
		{
			pLength = pList.Length;
		}
		if (pGameplay)
		{
			this.registerDecisionArrayGameplay(pActor, pList, pLength);
			return;
		}
		this.registerDecisionArraySimulation(pActor, pList, pLength);
	}

	// Token: 0x060022EC RID: 8940 RVA: 0x001236D0 File Offset: 0x001218D0
	private void registerDecisionArrayGameplay(Actor pActor, DecisionAsset[] pArray, int pLength)
	{
		NeuralLayerAsset[] tNeuralLayers = AssetManager.neural_layers.layers_array;
		DecisionChecks tActorChecks = new DecisionChecks(pActor);
		for (int i = 0; i < pLength; i++)
		{
			DecisionAsset tAsset = pArray[i];
			if ((!this._do_priority_levels || tAsset.priority_int_cached >= this._highest_priority) && !pActor.isDecisionOnCooldown(tAsset.decision_index, (double)tAsset.cooldown) && pActor.isDecisionEnabled(tAsset.decision_index) && tAsset.isPossible(ref tActorChecks))
			{
				if (tAsset.action_check_launch != null && !tAsset.action_check_launch(pActor))
				{
					if (tAsset.cooldown_on_launch_failure)
					{
						pActor.setDecisionCooldown(tAsset);
					}
				}
				else
				{
					DecisionAsset[] all_assets = this._all_assets;
					int counter_all_assets = this._counter_all_assets;
					this._counter_all_assets = counter_all_assets + 1;
					all_assets[counter_all_assets] = tAsset;
					if (tNeuralLayers[tAsset.priority_int_cached].critical)
					{
						this._do_priority_levels = true;
					}
					if (this._do_priority_levels && tAsset.priority_int_cached > this._highest_priority)
					{
						this._highest_priority = tAsset.priority_int_cached;
					}
					int tPriorityIndex = this._priority_array_counters[tAsset.priority_int_cached];
					this._priority_array[tAsset.priority_int_cached][tPriorityIndex] = tAsset;
					this._priority_array_counters[tAsset.priority_int_cached]++;
				}
			}
		}
	}

	// Token: 0x060022ED RID: 8941 RVA: 0x0012380C File Offset: 0x00121A0C
	private void calculateFactors(Actor pActor)
	{
		DecisionAsset[] tTargetArray;
		int tTargetLength;
		if (this._do_priority_levels)
		{
			tTargetArray = this._priority_array[this._highest_priority];
			tTargetLength = this._priority_array_counters[this._highest_priority];
		}
		else
		{
			tTargetArray = this._all_assets;
			tTargetLength = this._counter_all_assets;
		}
		this.calculateFactorsFrom(tTargetArray, tTargetLength, pActor);
	}

	// Token: 0x060022EE RID: 8942 RVA: 0x00123858 File Offset: 0x00121A58
	private void calculateFactorsFrom(DecisionAsset[] pPriorityArray, int pLength, Actor pActor)
	{
		DecisionAsset[] tActions = this._actions;
		float[] tFactors = this._factors;
		for (int i = 0; i < pLength; i++)
		{
			DecisionAsset tAsset = pPriorityArray[i];
			float tWeight = tAsset.weight;
			if (tAsset.has_weight_custom)
			{
				tWeight = tAsset.weight_calculate_custom(pActor);
			}
			tActions[this._counter_possible] = tAsset;
			tFactors[this._counter_possible] = tWeight;
			this._counter_possible++;
		}
	}

	// Token: 0x060022EF RID: 8943 RVA: 0x001238C4 File Offset: 0x00121AC4
	private void registerDecisionArraySimulation(Actor pActor, DecisionAsset[] pArray, int pLength)
	{
		DecisionAsset[] tActions = this._actions;
		float[] tFactors = this._factors;
		DecisionChecks tActorChecks = new DecisionChecks(pActor);
		for (int i = 0; i < pLength; i++)
		{
			DecisionAsset tAsset = pArray[i];
			if (tAsset.isPossible(ref tActorChecks))
			{
				float tWeight = tAsset.weight;
				if (tAsset.has_weight_custom)
				{
					tWeight = tAsset.weight_calculate_custom(pActor);
				}
				tActions[this._counter_possible] = tAsset;
				tFactors[this._counter_possible] = tWeight;
				this._counter_possible++;
			}
		}
	}

	// Token: 0x060022F0 RID: 8944 RVA: 0x00123946 File Offset: 0x00121B46
	public void clear()
	{
		this.clearPriorityArray();
		this._counter_possible = 0;
		this._highest_priority = 0;
		this._counter_all_assets = 0;
	}

	// Token: 0x060022F1 RID: 8945 RVA: 0x00123964 File Offset: 0x00121B64
	private void clearPriorityArray()
	{
		int i = 0;
		int tLength = this._priority_array.Length;
		while (i < tLength)
		{
			this._priority_array[i].Clear<DecisionAsset>();
			this._priority_array_counters[i] = 0;
			i++;
		}
	}

	// Token: 0x060022F2 RID: 8946 RVA: 0x0012399C File Offset: 0x00121B9C
	private void calculateChances(float pRandomnessFactor = 1f)
	{
		float[] tChances = this._chances;
		float[] tFactors = this._factors;
		int i = 0;
		int tLength = this._counter_possible;
		while (i < tLength)
		{
			float tFactor = tFactors[i];
			float tChance = (float)Math.Pow(2.718281828459045, (double)(tFactor * pRandomnessFactor));
			tChances[i] = tChance;
			i++;
		}
	}

	// Token: 0x060022F3 RID: 8947 RVA: 0x001239EC File Offset: 0x00121BEC
	public DecisionAsset chooseBestAction(float pRandomnessFactor = 1f)
	{
		float[] tChances = this._chances;
		DecisionAsset[] tActions = this._actions;
		this.calculateChances(pRandomnessFactor);
		float tRandomValue = Randy.random() * this.sum();
		float tAccumulatedProbability = 0f;
		int i = 0;
		int tLength = this._counter_possible;
		while (i < tLength)
		{
			tAccumulatedProbability += tChances[i];
			if (tRandomValue < tAccumulatedProbability)
			{
				return tActions[i];
			}
			i++;
		}
		if (this._counter_possible <= 0)
		{
			return null;
		}
		return tActions[this._counter_possible - 1];
	}

	// Token: 0x060022F4 RID: 8948 RVA: 0x00123A60 File Offset: 0x00121C60
	private float sum()
	{
		float[] tChances = this._chances;
		float tResult = 0f;
		int i = 0;
		int tLength = this._counter_possible;
		while (i < tLength)
		{
			tResult += tChances[i];
			i++;
		}
		return tResult;
	}

	// Token: 0x060022F5 RID: 8949 RVA: 0x00123A94 File Offset: 0x00121C94
	public string getFactorString(DecisionAsset pAsset)
	{
		float tValue = this._factors[pAsset.decision_index];
		return tValue.ToString("F3");
	}

	// Token: 0x060022F6 RID: 8950 RVA: 0x00123ABC File Offset: 0x00121CBC
	public string getChanceString(DecisionAsset pAsset)
	{
		float tValue = this._chances[pAsset.decision_index];
		return tValue.ToString("F3");
	}

	// Token: 0x060022F7 RID: 8951 RVA: 0x00123AE4 File Offset: 0x00121CE4
	public string getOrderString(DecisionAsset pAsset)
	{
		int i = 0;
		int tLength = this._counter_possible;
		while (i < tLength)
		{
			if (this._actions[i] == pAsset)
			{
				return i.ToString() + "/" + this._counter_possible.ToString();
			}
			i++;
		}
		return "??";
	}

	// Token: 0x060022F8 RID: 8952 RVA: 0x00123B34 File Offset: 0x00121D34
	public void debug(Actor pActor, DebugTool pTool)
	{
		this.useOnDebug(pActor);
		int i = 0;
		int tLength = this._counter_possible;
		while (i < tLength)
		{
			DecisionAsset tDecision = this._actions[i];
			float tFactor = this._factors[i];
			pTool.setText(tDecision.id, tFactor.ToString("F3"), 0f, false, 0L, false, false, "");
			i++;
		}
	}

	// Token: 0x060022F9 RID: 8953 RVA: 0x00123B94 File Offset: 0x00121D94
	private void useOnDebug(Actor pActor)
	{
		ActorAsset tAsset = pActor.asset;
		this.clear();
		this.registerBasicDecisionLists(pActor, false);
		if (tAsset.hasDecisions())
		{
			this.registerDecisionArraySimulation(pActor, tAsset.getDecisions(), tAsset.decisions_counter);
		}
	}

	// Token: 0x060022FA RID: 8954 RVA: 0x00123BD1 File Offset: 0x00121DD1
	public int getCounter()
	{
		return this._counter_possible;
	}

	// Token: 0x060022FB RID: 8955 RVA: 0x00123BD9 File Offset: 0x00121DD9
	public DecisionAsset[] getActions()
	{
		return this._actions;
	}

	// Token: 0x04001901 RID: 6401
	private const int MAX_POSSIBLE_DECISIONS = 1024;

	// Token: 0x04001902 RID: 6402
	private readonly DecisionAsset[] _actions = new DecisionAsset[1024];

	// Token: 0x04001903 RID: 6403
	private readonly float[] _factors = new float[1024];

	// Token: 0x04001904 RID: 6404
	private readonly float[] _chances = new float[1024];

	// Token: 0x04001905 RID: 6405
	public static Dictionary<string, int> debug_counter = new Dictionary<string, int>();

	// Token: 0x04001906 RID: 6406
	private DecisionAsset[] _all_assets = new DecisionAsset[1024];

	// Token: 0x04001907 RID: 6407
	private int _counter_all_assets;

	// Token: 0x04001908 RID: 6408
	private int _counter_possible;

	// Token: 0x04001909 RID: 6409
	private int _highest_priority;

	// Token: 0x0400190A RID: 6410
	private readonly DecisionAsset[][] _priority_array = new DecisionAsset[Enum.GetValues(typeof(NeuroLayer)).Length][];

	// Token: 0x0400190B RID: 6411
	private readonly int[] _priority_array_counters = new int[Enum.GetValues(typeof(NeuroLayer)).Length];

	// Token: 0x0400190C RID: 6412
	private bool _do_priority_levels;
}
