using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000605 RID: 1541
public class ToolbarButtons : MonoBehaviour, IShakable
{
	// Token: 0x170002B9 RID: 697
	// (get) Token: 0x06003292 RID: 12946 RVA: 0x0017FC60 File Offset: 0x0017DE60
	public float shake_duration { get; } = 0.5f;

	// Token: 0x170002BA RID: 698
	// (get) Token: 0x06003293 RID: 12947 RVA: 0x0017FC68 File Offset: 0x0017DE68
	public float shake_strength { get; } = 4f;

	// Token: 0x170002BB RID: 699
	// (get) Token: 0x06003294 RID: 12948 RVA: 0x0017FC70 File Offset: 0x0017DE70
	// (set) Token: 0x06003295 RID: 12949 RVA: 0x0017FC78 File Offset: 0x0017DE78
	public Tweener shake_tween { get; set; }

	// Token: 0x06003296 RID: 12950 RVA: 0x0017FC81 File Offset: 0x0017DE81
	public static Sprite getSpriteButtonNormal()
	{
		if (ToolbarButtons.instance == null)
		{
			return null;
		}
		return ToolbarButtons.instance.button_sprite_normal;
	}

	// Token: 0x06003297 RID: 12951 RVA: 0x0017FC9C File Offset: 0x0017DE9C
	public static Sprite getSpriteButtonUnitExists()
	{
		if (ToolbarButtons.instance == null)
		{
			return null;
		}
		return ToolbarButtons.instance.button_sprite_unit_exists;
	}

	// Token: 0x06003298 RID: 12952 RVA: 0x0017FCB7 File Offset: 0x0017DEB7
	private void Awake()
	{
		ToolbarButtons.instance = this;
	}

	// Token: 0x06003299 RID: 12953 RVA: 0x0017FCBF File Offset: 0x0017DEBF
	public void resetBar()
	{
		base.gameObject.SetActive(false);
		base.gameObject.SetActive(true);
	}

	// Token: 0x0600329A RID: 12954 RVA: 0x0017FCD9 File Offset: 0x0017DED9
	private void Update()
	{
		if (Time.frameCount % 30 == 0)
		{
			PowerButton.checkActorSpawnButtons();
		}
	}

	// Token: 0x0600329B RID: 12955 RVA: 0x0017FCEC File Offset: 0x0017DEEC
	public Vector3 getPowerBarLeftCornerViewportPos()
	{
		RectTransform rectTransform = this.main_background.rectTransform;
		Vector3[] tCorners = new Vector3[4];
		rectTransform.GetWorldCorners(tCorners);
		return tCorners[1];
	}

	// Token: 0x0600329D RID: 12957 RVA: 0x0017FD36 File Offset: 0x0017DF36
	Transform IShakable.get_transform()
	{
		return base.transform;
	}

	// Token: 0x04002637 RID: 9783
	public static ToolbarButtons instance;

	// Token: 0x0400263B RID: 9787
	public Image main_background;

	// Token: 0x0400263C RID: 9788
	public Sprite button_sprite_normal;

	// Token: 0x0400263D RID: 9789
	public Sprite button_sprite_unit_exists;
}
