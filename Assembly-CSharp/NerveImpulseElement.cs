using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020007AB RID: 1963
public class NerveImpulseElement : MonoBehaviour
{
	// Token: 0x06003E0F RID: 15887 RVA: 0x001B0A44 File Offset: 0x001AEC44
	public void energize(NeuronElement pPresynapticNeuron, NeuronElement pPostsynapticNeuron, int pWave)
	{
		base.transform.localPosition = pPresynapticNeuron.transform.localPosition;
		this.presynaptic_neuron = pPresynapticNeuron;
		this.postsynaptic_neuron = pPostsynapticNeuron;
		this._move_timer = 0f;
		this._speed_current = Randy.randomFloat(1f, 3f);
		this.wave = pWave;
	}

	// Token: 0x06003E10 RID: 15888 RVA: 0x001B0A9C File Offset: 0x001AEC9C
	public ImpulseReachResult moveTowardsNextNeuron()
	{
		if (this.postsynaptic_neuron == null)
		{
			return ImpulseReachResult.Done;
		}
		this._move_timer += this._speed_current * Time.deltaTime;
		this._move_timer = Mathf.Clamp01(this._move_timer);
		Vector3 tStartPos = this.presynaptic_neuron.transform.localPosition;
		Vector3 tTargetPos = this.postsynaptic_neuron.transform.localPosition;
		base.transform.localPosition = Vector3.Lerp(tStartPos, tTargetPos, this._move_timer);
		this.updateImpulseColor();
		if (this._move_timer < 1f)
		{
			return ImpulseReachResult.Move;
		}
		this.presynaptic_neuron = this.postsynaptic_neuron;
		this.postsynaptic_neuron = this.GetNextTargetNeuron();
		this._move_timer = 0f;
		this.wave--;
		if (this.wave > 0)
		{
			return ImpulseReachResult.Split;
		}
		return ImpulseReachResult.Done;
	}

	// Token: 0x06003E11 RID: 15889 RVA: 0x001B0B6F File Offset: 0x001AED6F
	private NeuronElement GetNextTargetNeuron()
	{
		if (this.presynaptic_neuron.connected_neurons.Count == 0)
		{
			return null;
		}
		return this.presynaptic_neuron.connected_neurons.GetRandom<NeuronElement>();
	}

	// Token: 0x06003E12 RID: 15890 RVA: 0x001B0B98 File Offset: 0x001AED98
	private void updateImpulseColor()
	{
		float tDepthFactor = Mathf.Lerp(this.presynaptic_neuron.render_depth, this.postsynaptic_neuron.render_depth, this._move_timer);
		Color tFinalColor = Color.Lerp(this._color_back, this._color_front, tDepthFactor);
		if (this.image.color != tFinalColor)
		{
			this.image.color = tFinalColor;
		}
		float tScale = Mathf.Lerp(0.6f, 1f, tDepthFactor);
		if (base.transform.localScale.x != tScale)
		{
			base.transform.localScale = new Vector3(tScale, tScale, tScale);
		}
	}

	// Token: 0x04002D1A RID: 11546
	public Image image;

	// Token: 0x04002D1B RID: 11547
	private const float SPEED_MIN = 1f;

	// Token: 0x04002D1C RID: 11548
	private const float SPEED_MAX = 3f;

	// Token: 0x04002D1D RID: 11549
	private const float SCALE_MIN = 0.6f;

	// Token: 0x04002D1E RID: 11550
	private const float SCALE_MAX = 1f;

	// Token: 0x04002D1F RID: 11551
	private float _speed_current;

	// Token: 0x04002D20 RID: 11552
	private float _move_timer;

	// Token: 0x04002D21 RID: 11553
	public NeuronElement presynaptic_neuron;

	// Token: 0x04002D22 RID: 11554
	public NeuronElement postsynaptic_neuron;

	// Token: 0x04002D23 RID: 11555
	private Color _color_back = Toolbox.makeColor("#26A8A8", 0.5f);

	// Token: 0x04002D24 RID: 11556
	private Color _color_front = Toolbox.makeColor("#3AFFFF", 0.7f);

	// Token: 0x04002D25 RID: 11557
	public int wave;
}
