using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000819 RID: 2073
public class WorldLawsCursedStars : MonoBehaviour
{
	// Token: 0x060040EE RID: 16622 RVA: 0x001BB6F4 File Offset: 0x001B98F4
	private void Awake()
	{
		this._center = this._stars_parent.localPosition;
		for (int i = 0; i < 88; i++)
		{
			WorldLawsCursedStar tStar = Object.Instantiate<WorldLawsCursedStar>(this._star_prefab, this._stars_parent);
			this._stars.Add(tStar);
			this._offset_indexes.Add((float)i);
		}
		this.updateStarsPositions();
	}

	// Token: 0x060040EF RID: 16623 RVA: 0x001BB750 File Offset: 0x001B9950
	private void OnEnable()
	{
		float tCurseProgressRatio = CursedSacrifice.getCurseProgressRatio();
		this._rotation_speed = Mathf.Lerp(0.05f, 0.5f, tCurseProgressRatio);
		this._attraction_speed = Mathf.Lerp(0.05f, 0.3f, tCurseProgressRatio);
	}

	// Token: 0x060040F0 RID: 16624 RVA: 0x001BB78F File Offset: 0x001B998F
	private void Update()
	{
		this.updateStarsPositions();
	}

	// Token: 0x060040F1 RID: 16625 RVA: 0x001BB798 File Offset: 0x001B9998
	private void updateStarsPositions()
	{
		if (this._stars.Count == 0)
		{
			return;
		}
		float tCurseProgressRatio = CursedSacrifice.getCurseProgressRatio();
		this._angle += this._rotation_speed * Time.deltaTime;
		for (int i = 0; i < this._stars.Count; i++)
		{
			WorldLawsCursedStar tStar = this._stars[i];
			Transform tTransform = tStar.transform;
			if (this._offset_indexes[i] <= 0f)
			{
				List<float> offset_indexes = this._offset_indexes;
				int index = i;
				offset_indexes[index] += 87f;
				tStar.toggleEgg(CursedSacrifice.isLatestWasEgg());
				tStar.toggleFilled(Randy.randomChance(tCurseProgressRatio));
			}
			else
			{
				List<float> offset_indexes = this._offset_indexes;
				int index = i;
				offset_indexes[index] -= this._attraction_speed;
			}
			if (tStar.isFilled())
			{
				tStar.setStarsTransparency(1f);
			}
			else
			{
				tStar.setStarsTransparency(0f);
			}
			float tIndex = this._offset_indexes[i];
			float tChangeAngle = (float)i + this._angle;
			Vector3 tPosition = this._center;
			tPosition.x += Mathf.Cos(tChangeAngle) * tIndex * 1.25f;
			tPosition.y += Mathf.Sin(tChangeAngle) * tIndex * 1.25f;
			tTransform.localPosition = tPosition;
			this.mouseAvoidance(tTransform, tIndex);
			float tDistanceNormalized = this.normalizedDistanceFromCenter(tIndex);
			float tScale = Mathf.Lerp(0.5f, 1f, tDistanceNormalized);
			tTransform.localScale = new Vector3(tScale, tScale);
			this.colorize(tStar, tIndex);
		}
	}

	// Token: 0x060040F2 RID: 16626 RVA: 0x001BB934 File Offset: 0x001B9B34
	private void mouseAvoidance(Transform pTransform, float pIndex)
	{
		Vector2 tMousePosition;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(this._stars_parent, Input.mousePosition, null, out tMousePosition);
		float tDistanceToMouse = Mathf.Min(Vector2.Distance(pTransform.localPosition, tMousePosition), 40f);
		float tDistanceToMouseNormalized = 1f - tDistanceToMouse / 40f;
		float tPower = 15f * tDistanceToMouseNormalized;
		Vector3 tDirection = (pTransform.localPosition - tMousePosition).normalized;
		float tDistanceToCenterNormalized = Mathf.Max(this.normalizedDistanceFromCenter(pIndex), 0.2f);
		pTransform.localPosition += new Vector3(tPower * tDirection.x, tPower * tDirection.y) * tDistanceToCenterNormalized;
	}

	// Token: 0x060040F3 RID: 16627 RVA: 0x001BB9EC File Offset: 0x001B9BEC
	private void colorize(WorldLawsCursedStar pStar, float pIndex)
	{
		float tDistanceNormalized = this.normalizedDistanceFromCenter(pIndex);
		Color tNewColor = Toolbox.blendColor(WorldLawsCursedStars.OUTER_COLOR, WorldLawsCursedStars.CENTER_COLOR, tDistanceNormalized * 1.35f);
		float tAlphaByDistance = 8.4f - (pIndex + 1f) / 8.8f;
		pStar.setColorMultiplyAlphaBoth(tNewColor, tAlphaByDistance);
	}

	// Token: 0x060040F4 RID: 16628 RVA: 0x001BBA34 File Offset: 0x001B9C34
	private float normalizedDistanceFromCenter(float pIndex)
	{
		return (pIndex + 1f) / 88f;
	}

	// Token: 0x04002F16 RID: 12054
	private const int STARS_COUNT = 88;

	// Token: 0x04002F17 RID: 12055
	private const float ATTRACTION_SPEED_MIN = 0.05f;

	// Token: 0x04002F18 RID: 12056
	private const float ATTRACTION_SPEED_MAX = 0.3f;

	// Token: 0x04002F19 RID: 12057
	private const float ROTATION_SPEED_MIN = 0.05f;

	// Token: 0x04002F1A RID: 12058
	private const float ROTATION_SPEED_MAX = 0.5f;

	// Token: 0x04002F1B RID: 12059
	private const float RADIUS_MULTIPLIER = 1.25f;

	// Token: 0x04002F1C RID: 12060
	private const float MOUSE_AVOIDANCE_RADIUS = 40f;

	// Token: 0x04002F1D RID: 12061
	private const float MOUSE_AVOIDANCE_POWER = 15f;

	// Token: 0x04002F1E RID: 12062
	private const float ALPHA_FIX = 0.1f;

	// Token: 0x04002F1F RID: 12063
	private const float ALPHA_START = 8.4f;

	// Token: 0x04002F20 RID: 12064
	private static readonly Color OUTER_COLOR = Toolbox.makeColor("#FFAA00");

	// Token: 0x04002F21 RID: 12065
	private static readonly Color CENTER_COLOR = Toolbox.makeColor("#8B00FF");

	// Token: 0x04002F22 RID: 12066
	private float _attraction_speed;

	// Token: 0x04002F23 RID: 12067
	private float _rotation_speed;

	// Token: 0x04002F24 RID: 12068
	[SerializeField]
	private RectTransform _stars_parent;

	// Token: 0x04002F25 RID: 12069
	[SerializeField]
	private WorldLawsCursedStar _star_prefab;

	// Token: 0x04002F26 RID: 12070
	private float _angle;

	// Token: 0x04002F27 RID: 12071
	private Vector3 _center;

	// Token: 0x04002F28 RID: 12072
	private readonly List<WorldLawsCursedStar> _stars = new List<WorldLawsCursedStar>();

	// Token: 0x04002F29 RID: 12073
	private readonly List<float> _offset_indexes = new List<float>();
}
