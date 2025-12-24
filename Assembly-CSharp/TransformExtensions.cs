using System;
using UnityEngine;

// Token: 0x02000496 RID: 1174
public static class TransformExtensions
{
	// Token: 0x06002885 RID: 10373 RVA: 0x00145464 File Offset: 0x00143664
	public static Transform FindRecursive(this Transform pTransform, string pName)
	{
		return pTransform.FindRecursive((Transform tChild) => tChild.name == pName);
	}

	// Token: 0x06002886 RID: 10374 RVA: 0x00145490 File Offset: 0x00143690
	public static Transform FindRecursive(this Transform pTransform, Func<Transform, bool> pSelector)
	{
		foreach (object obj in pTransform)
		{
			Transform tChild = (Transform)obj;
			if (pSelector(tChild))
			{
				return tChild;
			}
			Transform tFound = tChild.FindRecursive(pSelector);
			if (tFound != null)
			{
				return tFound;
			}
		}
		return null;
	}

	// Token: 0x06002887 RID: 10375 RVA: 0x00145508 File Offset: 0x00143708
	public static T[] FindAllRecursive<T>(this Transform pTransform)
	{
		return pTransform.FindAllRecursive((Transform p) => p.HasComponent<T>());
	}

	// Token: 0x06002888 RID: 10376 RVA: 0x00145530 File Offset: 0x00143730
	public static T[] FindAllRecursive<T>(this Transform pTransform, Func<Transform, bool> pSelector)
	{
		T[] result;
		using (ListPool<T> tResult = new ListPool<T>())
		{
			foreach (object obj in pTransform)
			{
				Transform tChild = (Transform)obj;
				if (pSelector(tChild) && tChild.HasComponent<T>())
				{
					tResult.Add(tChild.GetComponent<T>());
				}
				T[] tFound = tChild.FindAllRecursive(pSelector);
				if (tFound != null)
				{
					tResult.AddRange(tFound);
				}
			}
			result = tResult.ToArray<T>();
		}
		return result;
	}

	// Token: 0x06002889 RID: 10377 RVA: 0x001455D8 File Offset: 0x001437D8
	public static Transform FindParentWithName(this Transform pChildObject, params string[] pNames)
	{
		Transform tParent = null;
		foreach (string tName in pNames)
		{
			tParent = pChildObject.FindParentWithName(tName);
			if (tParent != null)
			{
				break;
			}
		}
		return tParent;
	}

	// Token: 0x0600288A RID: 10378 RVA: 0x00145610 File Offset: 0x00143810
	public static Transform FindParentWithName(this Transform pChildObject, string pName)
	{
		Transform t = pChildObject;
		while (t.parent != null)
		{
			if (t.parent.gameObject.name == pName)
			{
				return t.parent;
			}
			t = t.parent.transform;
		}
		return null;
	}

	// Token: 0x0600288B RID: 10379 RVA: 0x0014565C File Offset: 0x0014385C
	public static int GetActiveSiblingIndex(this Transform pTransform)
	{
		int tActiveIndex = 0;
		Transform tParent = pTransform.parent;
		int i = 0;
		int tLength = tParent.childCount;
		while (i < tLength)
		{
			Transform tChild = tParent.GetChild(i);
			if (tChild.gameObject.activeSelf)
			{
				if (tChild == pTransform)
				{
					return tActiveIndex;
				}
				tActiveIndex++;
			}
			i++;
		}
		return -1;
	}

	// Token: 0x0600288C RID: 10380 RVA: 0x001456B0 File Offset: 0x001438B0
	public static int CountActiveChildren(this Transform pTransform)
	{
		int tCount = 0;
		int i = 0;
		int tLength = pTransform.childCount;
		while (i < tLength)
		{
			if (pTransform.GetChild(i).gameObject.activeSelf)
			{
				tCount++;
			}
			i++;
		}
		return tCount;
	}

	// Token: 0x0600288D RID: 10381 RVA: 0x001456EC File Offset: 0x001438EC
	public static int CountChildren(this Transform pTransform, Func<Transform, bool> pSelector)
	{
		int tCount = 0;
		int i = 0;
		int tLength = pTransform.childCount;
		while (i < tLength)
		{
			if (pSelector(pTransform.GetChild(i)))
			{
				tCount++;
			}
			i++;
		}
		return tCount;
	}
}
