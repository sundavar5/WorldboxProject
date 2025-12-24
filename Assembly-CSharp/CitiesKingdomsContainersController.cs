using System;
using UnityEngine;

// Token: 0x02000747 RID: 1863
public class CitiesKingdomsContainersController : MonoBehaviour
{
	// Token: 0x06003B08 RID: 15112 RVA: 0x0019FCA0 File Offset: 0x0019DEA0
	public void update(NanoObject pNano)
	{
		this._banners_cities.update(pNano);
		this._banners_kingdoms.update(pNano);
		IMetaObject metaObject = (IMetaObject)pNano;
		bool tHasCities = metaObject.hasCities();
		this._banners_cities.gameObject.SetActive(tHasCities);
		this._line_cities.SetActive(tHasCities);
		bool tHasKingdoms = metaObject.hasKingdoms();
		this._banners_kingdoms.gameObject.SetActive(tHasKingdoms);
		this._line_kingdoms.SetActive(tHasKingdoms);
	}

	// Token: 0x04002B8B RID: 11147
	[SerializeField]
	private CitiesBannersContainer _banners_cities;

	// Token: 0x04002B8C RID: 11148
	[SerializeField]
	private GameObject _line_cities;

	// Token: 0x04002B8D RID: 11149
	[SerializeField]
	private KingdomsBannersContainer _banners_kingdoms;

	// Token: 0x04002B8E RID: 11150
	[SerializeField]
	private GameObject _line_kingdoms;
}
