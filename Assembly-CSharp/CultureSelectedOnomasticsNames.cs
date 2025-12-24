using System;
using UnityEngine;

// Token: 0x02000677 RID: 1655
public class CultureSelectedOnomasticsNames : OnomasticsNameGenerator
{
	// Token: 0x170002F0 RID: 752
	// (get) Token: 0x06003556 RID: 13654 RVA: 0x0018871D File Offset: 0x0018691D
	private MetaType _meta_type
	{
		get
		{
			return MetaType.Unit;
		}
	}

	// Token: 0x06003557 RID: 13655 RVA: 0x00188724 File Offset: 0x00186924
	public void load(Culture pCulture)
	{
		string tTemplate = this.getTemplateString(pCulture);
		if (this._culture == pCulture && tTemplate == this._last_template)
		{
			return;
		}
		this._culture = pCulture;
		this._last_template = tTemplate;
		base.clickRegenerate();
	}

	// Token: 0x06003558 RID: 13656 RVA: 0x00188768 File Offset: 0x00186968
	public void update()
	{
		bool tIsRekt = this._culture.isRekt();
		this._main_container.SetActive(!tIsRekt);
		this._separator.SetActive(!tIsRekt);
		if (tIsRekt)
		{
			return;
		}
		OnomasticsData tData = this._culture.getOnomasticData(this._meta_type, false);
		base.updateNameGeneration(tData);
	}

	// Token: 0x06003559 RID: 13657 RVA: 0x001887BD File Offset: 0x001869BD
	public void click()
	{
		base.clickRegenerate();
	}

	// Token: 0x0600355A RID: 13658 RVA: 0x001887C5 File Offset: 0x001869C5
	private string getTemplateString(Culture pCulture)
	{
		return pCulture.getOnomasticData(this._meta_type, false).getShortTemplate();
	}

	// Token: 0x040027DE RID: 10206
	[SerializeField]
	private GameObject _main_container;

	// Token: 0x040027DF RID: 10207
	[SerializeField]
	private GameObject _separator;

	// Token: 0x040027E0 RID: 10208
	private Culture _culture;

	// Token: 0x040027E1 RID: 10209
	private string _last_template;
}
