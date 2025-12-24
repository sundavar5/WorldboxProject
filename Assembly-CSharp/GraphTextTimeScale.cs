using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020006BE RID: 1726
public class GraphTextTimeScale : MonoBehaviour
{
	// Token: 0x06003760 RID: 14176 RVA: 0x00190369 File Offset: 0x0018E569
	public void Awake()
	{
		this._text = base.GetComponent<Text>();
	}

	// Token: 0x06003761 RID: 14177 RVA: 0x00190378 File Offset: 0x0018E578
	public void Update()
	{
		string tText = Toolbox.formatNumber((long)AssetManager.graph_time_library.get(this.graph_time_scale_container.current_scale.ToString()).max_time_frame);
		tText += this.graph_time_scale_container.getIndexString();
		this._text.text = tText;
	}

	// Token: 0x04002901 RID: 10497
	public GraphTimeScaleContainer graph_time_scale_container;

	// Token: 0x04002902 RID: 10498
	private Text _text;
}
