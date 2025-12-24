using System;
using UnityEngine;

// Token: 0x020000F2 RID: 242
public class PixelBag
{
	// Token: 0x06000728 RID: 1832 RVA: 0x00069E14 File Offset: 0x00068014
	public PixelBag(Sprite pSpriteSource, bool pCheckPhenotypes, bool pCheckLights)
	{
		Texture2D texture = pSpriteSource.texture;
		Rect tTextureRectBody = pSpriteSource.rect;
		int tWidthTexture = texture.width;
		this.texture_rect_width = (int)tTextureRectBody.width;
		this.texture_rect_height = (int)tTextureRectBody.height;
		int tTextureX = (int)tTextureRectBody.x;
		int tTextureY = (int)tTextureRectBody.y;
		Color32[] tTexturePixels32 = texture.GetPixels32();
		for (int tFrameX = 0; tFrameX < this.texture_rect_width; tFrameX++)
		{
			for (int tFrameY = 0; tFrameY < this.texture_rect_height; tFrameY++)
			{
				int num = tFrameX + tTextureX;
				int tPixelY = tFrameY + tTextureY;
				int tPixelID = num + tPixelY * tWidthTexture;
				Color32 tColorSource = tTexturePixels32[tPixelID];
				if (tColorSource.a != 0)
				{
					this.checkAndSavePixel(tColorSource, tFrameX, tFrameY, pCheckPhenotypes, pCheckLights);
				}
			}
		}
		ListPool<Pixel> pixels_normal = this._pixels_normal;
		this.arr_pixels_normal = ((pixels_normal != null) ? pixels_normal.ToArray<Pixel>() : null);
		ListPool<Pixel> pixels_light = this._pixels_light;
		this.arr_pixels_light = ((pixels_light != null) ? pixels_light.ToArray<Pixel>() : null);
		ListPool<Pixel> pixels_k1_ = this._pixels_k1_0;
		this.arr_pixels_k1_0 = ((pixels_k1_ != null) ? pixels_k1_.ToArray<Pixel>() : null);
		ListPool<Pixel> pixels_k1_2 = this._pixels_k1_1;
		this.arr_pixels_k1_1 = ((pixels_k1_2 != null) ? pixels_k1_2.ToArray<Pixel>() : null);
		ListPool<Pixel> pixels_k1_3 = this._pixels_k1_2;
		this.arr_pixels_k1_2 = ((pixels_k1_3 != null) ? pixels_k1_3.ToArray<Pixel>() : null);
		ListPool<Pixel> pixels_k1_4 = this._pixels_k1_3;
		this.arr_pixels_k1_3 = ((pixels_k1_4 != null) ? pixels_k1_4.ToArray<Pixel>() : null);
		ListPool<Pixel> pixels_k1_5 = this._pixels_k1_4;
		this.arr_pixels_k1_4 = ((pixels_k1_5 != null) ? pixels_k1_5.ToArray<Pixel>() : null);
		ListPool<Pixel> pixels_k2_ = this._pixels_k2_0;
		this.arr_pixels_k2_0 = ((pixels_k2_ != null) ? pixels_k2_.ToArray<Pixel>() : null);
		ListPool<Pixel> pixels_k2_2 = this._pixels_k2_1;
		this.arr_pixels_k2_1 = ((pixels_k2_2 != null) ? pixels_k2_2.ToArray<Pixel>() : null);
		ListPool<Pixel> pixels_k2_3 = this._pixels_k2_2;
		this.arr_pixels_k2_2 = ((pixels_k2_3 != null) ? pixels_k2_3.ToArray<Pixel>() : null);
		ListPool<Pixel> pixels_k2_4 = this._pixels_k2_3;
		this.arr_pixels_k2_3 = ((pixels_k2_4 != null) ? pixels_k2_4.ToArray<Pixel>() : null);
		ListPool<Pixel> pixels_k2_5 = this._pixels_k2_4;
		this.arr_pixels_k2_4 = ((pixels_k2_5 != null) ? pixels_k2_5.ToArray<Pixel>() : null);
		ListPool<Pixel> pixels_phenotype_shade_ = this._pixels_phenotype_shade_0;
		this.arr_pixels_phenotype_shade_0 = ((pixels_phenotype_shade_ != null) ? pixels_phenotype_shade_.ToArray<Pixel>() : null);
		ListPool<Pixel> pixels_phenotype_shade_2 = this._pixels_phenotype_shade_1;
		this.arr_pixels_phenotype_shade_1 = ((pixels_phenotype_shade_2 != null) ? pixels_phenotype_shade_2.ToArray<Pixel>() : null);
		ListPool<Pixel> pixels_phenotype_shade_3 = this._pixels_phenotype_shade_2;
		this.arr_pixels_phenotype_shade_2 = ((pixels_phenotype_shade_3 != null) ? pixels_phenotype_shade_3.ToArray<Pixel>() : null);
		ListPool<Pixel> pixels_phenotype_shade_4 = this._pixels_phenotype_shade_3;
		this.arr_pixels_phenotype_shade_3 = ((pixels_phenotype_shade_4 != null) ? pixels_phenotype_shade_4.ToArray<Pixel>() : null);
		this.clearLists();
	}

	// Token: 0x06000729 RID: 1833 RVA: 0x0006A054 File Offset: 0x00068254
	private void clearLists()
	{
		ListPool<Pixel> pixels_normal = this._pixels_normal;
		if (pixels_normal != null)
		{
			pixels_normal.Dispose();
		}
		ListPool<Pixel> pixels_light = this._pixels_light;
		if (pixels_light != null)
		{
			pixels_light.Dispose();
		}
		ListPool<Pixel> pixels_k1_ = this._pixels_k1_0;
		if (pixels_k1_ != null)
		{
			pixels_k1_.Dispose();
		}
		ListPool<Pixel> pixels_k1_2 = this._pixels_k1_1;
		if (pixels_k1_2 != null)
		{
			pixels_k1_2.Dispose();
		}
		ListPool<Pixel> pixels_k1_3 = this._pixels_k1_2;
		if (pixels_k1_3 != null)
		{
			pixels_k1_3.Dispose();
		}
		ListPool<Pixel> pixels_k1_4 = this._pixels_k1_3;
		if (pixels_k1_4 != null)
		{
			pixels_k1_4.Dispose();
		}
		ListPool<Pixel> pixels_k1_5 = this._pixels_k1_4;
		if (pixels_k1_5 != null)
		{
			pixels_k1_5.Dispose();
		}
		ListPool<Pixel> pixels_k2_ = this._pixels_k2_0;
		if (pixels_k2_ != null)
		{
			pixels_k2_.Dispose();
		}
		ListPool<Pixel> pixels_k2_2 = this._pixels_k2_1;
		if (pixels_k2_2 != null)
		{
			pixels_k2_2.Dispose();
		}
		ListPool<Pixel> pixels_k2_3 = this._pixels_k2_2;
		if (pixels_k2_3 != null)
		{
			pixels_k2_3.Dispose();
		}
		ListPool<Pixel> pixels_k2_4 = this._pixels_k2_3;
		if (pixels_k2_4 != null)
		{
			pixels_k2_4.Dispose();
		}
		ListPool<Pixel> pixels_k2_5 = this._pixels_k2_4;
		if (pixels_k2_5 != null)
		{
			pixels_k2_5.Dispose();
		}
		ListPool<Pixel> pixels_phenotype_shade_ = this._pixels_phenotype_shade_0;
		if (pixels_phenotype_shade_ != null)
		{
			pixels_phenotype_shade_.Dispose();
		}
		ListPool<Pixel> pixels_phenotype_shade_2 = this._pixels_phenotype_shade_1;
		if (pixels_phenotype_shade_2 != null)
		{
			pixels_phenotype_shade_2.Dispose();
		}
		ListPool<Pixel> pixels_phenotype_shade_3 = this._pixels_phenotype_shade_2;
		if (pixels_phenotype_shade_3 != null)
		{
			pixels_phenotype_shade_3.Dispose();
		}
		ListPool<Pixel> pixels_phenotype_shade_4 = this._pixels_phenotype_shade_3;
		if (pixels_phenotype_shade_4 != null)
		{
			pixels_phenotype_shade_4.Dispose();
		}
		this._pixels_normal = null;
		this._pixels_light = null;
		this._pixels_k1_0 = null;
		this._pixels_k1_1 = null;
		this._pixels_k1_2 = null;
		this._pixels_k1_3 = null;
		this._pixels_k1_4 = null;
		this._pixels_k2_0 = null;
		this._pixels_k2_1 = null;
		this._pixels_k2_2 = null;
		this._pixels_k2_3 = null;
		this._pixels_k2_4 = null;
		this._pixels_phenotype_shade_0 = null;
		this._pixels_phenotype_shade_1 = null;
		this._pixels_phenotype_shade_2 = null;
		this._pixels_phenotype_shade_3 = null;
	}

	// Token: 0x0600072A RID: 1834 RVA: 0x0006A1E4 File Offset: 0x000683E4
	private void checkAndSavePixel(Color32 pColor, int pX, int pY, bool pCheckPhenotypes, bool pCheckLights)
	{
		Pixel tPixel = new Pixel(pX, pY, pColor);
		if (Toolbox.areColorsEqual(pColor, Toolbox.color_magenta_0))
		{
			if (this._pixels_k1_0 == null)
			{
				this._pixels_k1_0 = new ListPool<Pixel>();
			}
			this._pixels_k1_0.Add(tPixel);
			return;
		}
		if (Toolbox.areColorsEqual(pColor, Toolbox.color_magenta_1))
		{
			if (this._pixels_k1_1 == null)
			{
				this._pixels_k1_1 = new ListPool<Pixel>();
			}
			this._pixels_k1_1.Add(tPixel);
			return;
		}
		if (Toolbox.areColorsEqual(pColor, Toolbox.color_magenta_2))
		{
			if (this._pixels_k1_2 == null)
			{
				this._pixels_k1_2 = new ListPool<Pixel>();
			}
			this._pixels_k1_2.Add(tPixel);
			return;
		}
		if (Toolbox.areColorsEqual(pColor, Toolbox.color_magenta_3))
		{
			if (this._pixels_k1_3 == null)
			{
				this._pixels_k1_3 = new ListPool<Pixel>();
			}
			this._pixels_k1_3.Add(tPixel);
			return;
		}
		if (Toolbox.areColorsEqual(pColor, Toolbox.color_magenta_4))
		{
			if (this._pixels_k1_4 == null)
			{
				this._pixels_k1_4 = new ListPool<Pixel>();
			}
			this._pixels_k1_4.Add(tPixel);
			return;
		}
		if (Toolbox.areColorsEqual(pColor, Toolbox.color_teal_0))
		{
			if (this._pixels_k2_0 == null)
			{
				this._pixels_k2_0 = new ListPool<Pixel>();
			}
			this._pixels_k2_0.Add(tPixel);
			return;
		}
		if (Toolbox.areColorsEqual(pColor, Toolbox.color_teal_1))
		{
			if (this._pixels_k2_1 == null)
			{
				this._pixels_k2_1 = new ListPool<Pixel>();
			}
			this._pixels_k2_1.Add(tPixel);
			return;
		}
		if (Toolbox.areColorsEqual(pColor, Toolbox.color_teal_2))
		{
			if (this._pixels_k2_2 == null)
			{
				this._pixels_k2_2 = new ListPool<Pixel>();
			}
			this._pixels_k2_2.Add(tPixel);
			return;
		}
		if (Toolbox.areColorsEqual(pColor, Toolbox.color_teal_3))
		{
			if (this._pixels_k2_3 == null)
			{
				this._pixels_k2_3 = new ListPool<Pixel>();
			}
			this._pixels_k2_3.Add(tPixel);
			return;
		}
		if (Toolbox.areColorsEqual(pColor, Toolbox.color_teal_4))
		{
			if (this._pixels_k2_4 == null)
			{
				this._pixels_k2_4 = new ListPool<Pixel>();
			}
			this._pixels_k2_4.Add(tPixel);
			return;
		}
		if (pCheckPhenotypes)
		{
			if (Toolbox.areColorsEqual(pColor, Toolbox.color_phenotype_green_0))
			{
				if (this._pixels_phenotype_shade_0 == null)
				{
					this._pixels_phenotype_shade_0 = new ListPool<Pixel>();
				}
				this._pixels_phenotype_shade_0.Add(tPixel);
				return;
			}
			if (Toolbox.areColorsEqual(pColor, Toolbox.color_phenotype_green_1))
			{
				if (this._pixels_phenotype_shade_1 == null)
				{
					this._pixels_phenotype_shade_1 = new ListPool<Pixel>();
				}
				this._pixels_phenotype_shade_1.Add(tPixel);
				return;
			}
			if (Toolbox.areColorsEqual(pColor, Toolbox.color_phenotype_green_2))
			{
				if (this._pixels_phenotype_shade_2 == null)
				{
					this._pixels_phenotype_shade_2 = new ListPool<Pixel>();
				}
				this._pixels_phenotype_shade_2.Add(tPixel);
				return;
			}
			if (Toolbox.areColorsEqual(pColor, Toolbox.color_phenotype_green_3))
			{
				if (this._pixels_phenotype_shade_3 == null)
				{
					this._pixels_phenotype_shade_3 = new ListPool<Pixel>();
				}
				this._pixels_phenotype_shade_3.Add(tPixel);
				return;
			}
		}
		if (pCheckLights && Toolbox.areColorsEqual(pColor, Toolbox.color_light))
		{
			if (this._pixels_light == null)
			{
				this._pixels_light = new ListPool<Pixel>();
			}
			this._pixels_light.Add(tPixel);
			return;
		}
		if (this._pixels_normal == null)
		{
			this._pixels_normal = new ListPool<Pixel>();
		}
		this._pixels_normal.Add(tPixel);
	}

	// Token: 0x040007AE RID: 1966
	public readonly int texture_rect_width;

	// Token: 0x040007AF RID: 1967
	public readonly int texture_rect_height;

	// Token: 0x040007B0 RID: 1968
	public readonly Pixel[] arr_pixels_normal;

	// Token: 0x040007B1 RID: 1969
	public readonly Pixel[] arr_pixels_light;

	// Token: 0x040007B2 RID: 1970
	public readonly Pixel[] arr_pixels_k1_0;

	// Token: 0x040007B3 RID: 1971
	public readonly Pixel[] arr_pixels_k1_1;

	// Token: 0x040007B4 RID: 1972
	public readonly Pixel[] arr_pixels_k1_2;

	// Token: 0x040007B5 RID: 1973
	public readonly Pixel[] arr_pixels_k1_3;

	// Token: 0x040007B6 RID: 1974
	public readonly Pixel[] arr_pixels_k1_4;

	// Token: 0x040007B7 RID: 1975
	public readonly Pixel[] arr_pixels_k2_0;

	// Token: 0x040007B8 RID: 1976
	public readonly Pixel[] arr_pixels_k2_1;

	// Token: 0x040007B9 RID: 1977
	public readonly Pixel[] arr_pixels_k2_2;

	// Token: 0x040007BA RID: 1978
	public readonly Pixel[] arr_pixels_k2_3;

	// Token: 0x040007BB RID: 1979
	public readonly Pixel[] arr_pixels_k2_4;

	// Token: 0x040007BC RID: 1980
	public readonly Pixel[] arr_pixels_phenotype_shade_0;

	// Token: 0x040007BD RID: 1981
	public readonly Pixel[] arr_pixels_phenotype_shade_1;

	// Token: 0x040007BE RID: 1982
	public readonly Pixel[] arr_pixels_phenotype_shade_2;

	// Token: 0x040007BF RID: 1983
	public readonly Pixel[] arr_pixels_phenotype_shade_3;

	// Token: 0x040007C0 RID: 1984
	private ListPool<Pixel> _pixels_normal;

	// Token: 0x040007C1 RID: 1985
	private ListPool<Pixel> _pixels_light;

	// Token: 0x040007C2 RID: 1986
	private ListPool<Pixel> _pixels_k1_0;

	// Token: 0x040007C3 RID: 1987
	private ListPool<Pixel> _pixels_k1_1;

	// Token: 0x040007C4 RID: 1988
	private ListPool<Pixel> _pixels_k1_2;

	// Token: 0x040007C5 RID: 1989
	private ListPool<Pixel> _pixels_k1_3;

	// Token: 0x040007C6 RID: 1990
	private ListPool<Pixel> _pixels_k1_4;

	// Token: 0x040007C7 RID: 1991
	private ListPool<Pixel> _pixels_k2_0;

	// Token: 0x040007C8 RID: 1992
	private ListPool<Pixel> _pixels_k2_1;

	// Token: 0x040007C9 RID: 1993
	private ListPool<Pixel> _pixels_k2_2;

	// Token: 0x040007CA RID: 1994
	private ListPool<Pixel> _pixels_k2_3;

	// Token: 0x040007CB RID: 1995
	private ListPool<Pixel> _pixels_k2_4;

	// Token: 0x040007CC RID: 1996
	private ListPool<Pixel> _pixels_phenotype_shade_0;

	// Token: 0x040007CD RID: 1997
	private ListPool<Pixel> _pixels_phenotype_shade_1;

	// Token: 0x040007CE RID: 1998
	private ListPool<Pixel> _pixels_phenotype_shade_2;

	// Token: 0x040007CF RID: 1999
	private ListPool<Pixel> _pixels_phenotype_shade_3;
}
