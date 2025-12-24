using System;
using UnityEngine;

// Token: 0x02000239 RID: 569
public class BuildingMapIcon
{
	// Token: 0x060015DA RID: 5594 RVA: 0x000E07DC File Offset: 0x000DE9DC
	public BuildingMapIcon(Sprite sprite)
	{
		this._width = sprite.texture.width;
		this._height = sprite.texture.height;
		this._tex = new BuildingColorPixel[this._height][];
		for (int yy = 0; yy < this._height; yy++)
		{
			BuildingColorPixel[] row = new BuildingColorPixel[this._width];
			for (int xx = 0; xx < this._width; xx++)
			{
				Color32 tColor = sprite.texture.GetPixel(xx, yy);
				if (tColor.a == 0)
				{
					row[xx] = this._clear_color_pixel;
				}
				else
				{
					Color tColorAbandoned = Toolbox.makeDarkerColor(tColor, 0.9f);
					Color tColorRuin = Toolbox.makeDarkerColor(tColor, 0.6f);
					row[xx] = new BuildingColorPixel(tColor, tColorAbandoned, tColorRuin);
				}
			}
			this._tex[yy] = row;
		}
	}

	// Token: 0x060015DB RID: 5595 RVA: 0x000E08E4 File Offset: 0x000DEAE4
	internal Color32 getColor(int pX, int pY, Building pBuilding)
	{
		if (pX >= this._width || pY >= this._height)
		{
			return Toolbox.clear;
		}
		BuildingColorPixel tItem = this._tex[pY][pX];
		Color32 tColor = tItem.color;
		bool tPixelChanged = false;
		ColorAsset tAsset = pBuilding.kingdom.getColor();
		if (tAsset != null)
		{
			if (Toolbox.areColorsEqual(tColor, Toolbox.color_magenta_0))
			{
				tColor = tAsset.k_color_0;
				tPixelChanged = true;
			}
			else if (Toolbox.areColorsEqual(tColor, Toolbox.color_magenta_1))
			{
				tColor = tAsset.k_color_1;
				tPixelChanged = true;
			}
			else if (Toolbox.areColorsEqual(tColor, Toolbox.color_magenta_2))
			{
				tColor = tAsset.k_color_2;
				tPixelChanged = true;
			}
			else if (Toolbox.areColorsEqual(tColor, Toolbox.color_magenta_3))
			{
				tColor = tAsset.k_color_3;
				tPixelChanged = true;
			}
			else if (Toolbox.areColorsEqual(tColor, Toolbox.color_magenta_4))
			{
				tColor = tAsset.k_color_4;
				tPixelChanged = true;
			}
		}
		if (pBuilding.asset.has_get_map_icon_color && Toolbox.areColorsEqual(tColor, Toolbox.color_map_icon_green))
		{
			tColor = pBuilding.asset.get_map_icon_color(pBuilding);
			tPixelChanged = true;
		}
		if (!tPixelChanged)
		{
			if (pBuilding.isAbandoned())
			{
				tColor = tItem.color_abandoned;
			}
			else if (pBuilding.isRuin())
			{
				tColor = tItem.color_ruin;
			}
		}
		return tColor;
	}

	// Token: 0x0400123D RID: 4669
	private BuildingColorPixel[][] _tex;

	// Token: 0x0400123E RID: 4670
	private BuildingColorPixel _clear_color_pixel = new BuildingColorPixel(Toolbox.clear, Toolbox.clear, Toolbox.clear);

	// Token: 0x0400123F RID: 4671
	private int _width;

	// Token: 0x04001240 RID: 4672
	private int _height;
}
