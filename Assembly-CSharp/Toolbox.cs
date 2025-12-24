using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityPools;

// Token: 0x02000495 RID: 1173
public static class Toolbox
{
	// Token: 0x06002808 RID: 10248 RVA: 0x00142478 File Offset: 0x00140678
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void fromStringListToHashset(List<string> pList, HashSet<string> pHashset)
	{
		pHashset.UnionWith(pList);
	}

	// Token: 0x06002809 RID: 10249 RVA: 0x00142481 File Offset: 0x00140681
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string coloredText(string pText, string pColor, bool pLocalize = false)
	{
		if (pLocalize)
		{
			pText = LocalizedTextManager.getText(pText, null, false);
		}
		return string.Concat(new string[]
		{
			"<color=",
			pColor,
			">",
			pText,
			"</color>"
		});
	}

	// Token: 0x0600280A RID: 10250 RVA: 0x001424BC File Offset: 0x001406BC
	public static string coloredGreyPart(object pPart, string pMainColor, bool pUnit = false)
	{
		string tResult = string.Empty;
		if (pUnit)
		{
			tResult += Toolbox.coloredString(" (", ColorStyleLibrary.m.color_dead_text);
			tResult += Toolbox.coloredString(pPart.ToString(), pMainColor);
			tResult += Toolbox.coloredString(")", ColorStyleLibrary.m.color_dead_text);
		}
		else
		{
			tResult += Toolbox.coloredString(" [", ColorStyleLibrary.m.color_dead_text);
			tResult += Toolbox.coloredString(pPart.ToString(), pMainColor);
			tResult += Toolbox.coloredString("]", ColorStyleLibrary.m.color_dead_text);
		}
		return tResult;
	}

	// Token: 0x0600280B RID: 10251 RVA: 0x00142567 File Offset: 0x00140767
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool areColorsEqual(Color32 pC1, Color32 pC2)
	{
		return pC1.r == pC2.r && pC1.g == pC2.g && pC1.b == pC2.b;
	}

	// Token: 0x0600280C RID: 10252 RVA: 0x00142595 File Offset: 0x00140795
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool inBounds(float pVal, float pMin, float pMax)
	{
		return pVal > pMin && pVal < pMax;
	}

	// Token: 0x0600280D RID: 10253 RVA: 0x001425A4 File Offset: 0x001407A4
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe static string firstLetterToUpper(string str)
	{
		if (str == null)
		{
			return null;
		}
		if (str.Length == 0)
		{
			return str;
		}
		if (str.Length > 1)
		{
			int length = str.Length;
			Span<char> result = new Span<char>(stackalloc byte[checked(unchecked((UIntPtr)length) * 2)], length);
			*result[0] = char.ToUpper(str[0]);
			str.AsSpan(1).CopyTo(result.Slice(1));
			return new string(result);
		}
		return str.ToUpper();
	}

	// Token: 0x0600280E RID: 10254 RVA: 0x0014261B File Offset: 0x0014081B
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static int loopIndex(int pIndex, int pLength)
	{
		if (pLength < 1)
		{
			return 0;
		}
		return (pIndex % pLength + pLength) % pLength;
	}

	// Token: 0x0600280F RID: 10255 RVA: 0x0014262A File Offset: 0x0014082A
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector3 RotatePointAroundPivot(ref Vector3 point, ref Vector3 pivot, ref Vector3 angles)
	{
		return Quaternion.Euler(angles) * (point - pivot) + pivot;
	}

	// Token: 0x06002810 RID: 10256 RVA: 0x00142658 File Offset: 0x00140858
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector3 RotatePointAroundPivot2(ref Vector3 point, ref Vector3 pivot, ref Vector3 angles)
	{
		Vector3 dir = point - pivot;
		dir = Quaternion.Euler(angles) * dir;
		point = dir + pivot;
		return point;
	}

	// Token: 0x06002811 RID: 10257 RVA: 0x001426A4 File Offset: 0x001408A4
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2 rotateVector(Vector2 pVector, float degrees)
	{
		float f = degrees * 0.017453292f;
		float sin = Mathf.Sin(f);
		float cos = Mathf.Cos(f);
		float tx = pVector.x;
		float ty = pVector.y;
		return new Vector2(cos * tx - sin * ty, sin * tx + cos * ty);
	}

	// Token: 0x06002812 RID: 10258 RVA: 0x001426E8 File Offset: 0x001408E8
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector3 cubeBezier3(ref Vector3 p0, ref Vector3 p1, ref Vector3 p2, ref Vector3 p3, float t)
	{
		float r = 1f - t;
		float f0 = r * r * r;
		float f = r * r * t * 3f;
		float f2 = r * t * t * 3f;
		float f3 = t * t * t;
		return new Vector3(f0 * p0.x + f * p1.x + f2 * p2.x + f3 * p3.x, f0 * p0.y + f * p1.y + f2 * p2.y + f3 * p3.y, f0 * p0.z + f * p1.z + f2 * p2.z + f3 * p3.z);
	}

	// Token: 0x06002813 RID: 10259 RVA: 0x0014279C File Offset: 0x0014099C
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2 cubeBezier2(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3, float t)
	{
		float r = 1f - t;
		float f0 = r * r * r;
		float f = r * r * t * 3f;
		float f2 = r * t * t * 3f;
		float f3 = t * t * t;
		return new Vector2(f0 * p0.x + f * p1.x + f2 * p2.x + f3 * p3.x, f0 * p0.y + f * p1.y + f2 * p2.y + f3 * p3.y);
	}

	// Token: 0x06002814 RID: 10260 RVA: 0x0014282C File Offset: 0x00140A2C
	public unsafe static Vector2 cubeBezierN(float pTick, Span<Vector3> pPoints)
	{
		if (pPoints.Length > 2)
		{
			int lineCount = pPoints.Length - 1;
			Span<Vector3> span;
			if (lineCount < 128)
			{
				int num = lineCount;
				span = new Span<Vector3>(stackalloc byte[checked(unchecked((UIntPtr)num) * (UIntPtr)sizeof(Vector3))], num);
			}
			else
			{
				span = new Vector3[lineCount];
			}
			Span<Vector3> lines = span;
			for (int i = 0; i < lineCount; i++)
			{
				*lines[i] = Vector2.Lerp(*pPoints[i], *pPoints[i + 1], pTick);
			}
			return Toolbox.cubeBezierN(pTick, lines);
		}
		if (pPoints.Length == 2)
		{
			return Vector2.Lerp(*pPoints[0], *pPoints[1], pTick);
		}
		return *pPoints[0];
	}

	// Token: 0x06002815 RID: 10261 RVA: 0x0014291C File Offset: 0x00140B1C
	public static string encode(string pString)
	{
		string tK = "WorldboxIsAwesome";
		pString = Encryption.EncryptString(pString, tK + "555");
		return pString;
	}

	// Token: 0x06002816 RID: 10262 RVA: 0x00142944 File Offset: 0x00140B44
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float easeInOutQuart(float x)
	{
		if (x < 0.5f)
		{
			return 8f * x * x * x * x;
		}
		return 1f - (float)Math.Pow((double)(-2f * x + 2f), 4.0) / 2f;
	}

	// Token: 0x06002817 RID: 10263 RVA: 0x00142990 File Offset: 0x00140B90
	public static string decode(string pString)
	{
		string tK = "WorldboxIsAwesome";
		pString = Encryption.DecryptString(pString, tK + "555");
		return pString;
	}

	// Token: 0x06002818 RID: 10264 RVA: 0x001429B8 File Offset: 0x00140BB8
	public static string decodeMobile(string pString)
	{
		string tDeviceID = SystemInfo.deviceUniqueIdentifier;
		string tK = "WorldboxIsAwesome";
		pString = Encryption.DecryptString(pString, tK + "555" + tDeviceID);
		return pString;
	}

	// Token: 0x06002819 RID: 10265 RVA: 0x001429E6 File Offset: 0x00140BE6
	public static string generateID_old()
	{
		return Toolbox.shortGUID(Guid.NewGuid());
	}

	// Token: 0x0600281A RID: 10266 RVA: 0x001429F2 File Offset: 0x00140BF2
	public static string shortGUID(Guid guid)
	{
		return Convert.ToBase64String(guid.ToByteArray()).Replace('+', '-').Replace('/', '_').Substring(0, 8);
	}

	// Token: 0x0600281B RID: 10267 RVA: 0x00142A1C File Offset: 0x00140C1C
	public static Vector3 getNewPoint(float pX1, float pY1, float pX2, float pY2, float pDist, bool pConvertNegative = true)
	{
		Vector3 tPoint = default(Vector3);
		float tDistCalc = Toolbox.Dist(pX1, pY1, pX2, pY2) - pDist;
		if (tDistCalc == 0f)
		{
			tPoint.Set(pX2, pY2, 0f);
			return tPoint;
		}
		float tLambda = pDist / tDistCalc;
		if (pConvertNegative && tLambda < 0f)
		{
			tLambda = -tLambda;
		}
		float tX = (pX1 + tLambda * pX2) / (1f + tLambda);
		float tY = (pY1 + tLambda * pY2) / (1f + tLambda);
		tPoint.x = tX;
		tPoint.y = tY;
		return tPoint;
	}

	// Token: 0x0600281C RID: 10268 RVA: 0x00142A9F File Offset: 0x00140C9F
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2 getNewPointVec2(Vector2 pVec1, Vector2 pVec2, float pDist, bool pConvertNegative = true)
	{
		return Toolbox.getNewPointVec2(pVec1.x, pVec1.y, pVec2.x, pVec2.y, pDist, pConvertNegative);
	}

	// Token: 0x0600281D RID: 10269 RVA: 0x00142AC0 File Offset: 0x00140CC0
	public static Vector2 getNewPointVec2(float pX1, float pY1, float pX2, float pY2, float pDist, bool pConvertNegative = true)
	{
		float tDistCalc = Toolbox.Dist(pX1, pY1, pX2, pY2) - pDist;
		if (tDistCalc == 0f)
		{
			return new Vector2(pX2, pY2);
		}
		float tLambda = pDist / tDistCalc;
		if (pConvertNegative && tLambda < 0f)
		{
			tLambda = -tLambda;
		}
		float tInvDenom = 1f / (1f + tLambda);
		float x = (pX1 + tLambda * pX2) * tInvDenom;
		float tY = (pY1 + tLambda * pY2) * tInvDenom;
		return new Vector2(x, tY);
	}

	// Token: 0x0600281E RID: 10270 RVA: 0x00142B21 File Offset: 0x00140D21
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float DistVec3(Vector3 pT1, Vector3 pT2)
	{
		return Mathf.Sqrt((pT1.x - pT2.x) * (pT1.x - pT2.x) + (pT1.y - pT2.y) * (pT1.y - pT2.y));
	}

	// Token: 0x0600281F RID: 10271 RVA: 0x00142B60 File Offset: 0x00140D60
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float DistVec2(Vector2Int pT1, Vector2Int pT2)
	{
		return Mathf.Sqrt((float)((pT1.x - pT2.x) * (pT1.x - pT2.x) + (pT1.y - pT2.y) * (pT1.y - pT2.y)));
	}

	// Token: 0x06002820 RID: 10272 RVA: 0x00142BB2 File Offset: 0x00140DB2
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float DistVec2Float(Vector2 pT1, Vector2 pT2)
	{
		return Mathf.Sqrt((pT1.x - pT2.x) * (pT1.x - pT2.x) + (pT1.y - pT2.y) * (pT1.y - pT2.y));
	}

	// Token: 0x06002821 RID: 10273 RVA: 0x00142BF0 File Offset: 0x00140DF0
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float DistTile(WorldTile pT1, WorldTile pT2)
	{
		return Mathf.Sqrt((float)((pT1.x - pT2.x) * (pT1.x - pT2.x) + (pT1.y - pT2.y) * (pT1.y - pT2.y)));
	}

	// Token: 0x06002822 RID: 10274 RVA: 0x00142C2F File Offset: 0x00140E2F
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float Dist(float x1, float y1, float x2, float y2)
	{
		return Mathf.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
	}

	// Token: 0x06002823 RID: 10275 RVA: 0x00142C45 File Offset: 0x00140E45
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float Dist(int x1, int y1, int x2, int y2)
	{
		return Mathf.Sqrt((float)((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2)));
	}

	// Token: 0x06002824 RID: 10276 RVA: 0x00142C5C File Offset: 0x00140E5C
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float SquaredDist(float x1, float y1, float x2, float y2)
	{
		return (x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2);
	}

	// Token: 0x06002825 RID: 10277 RVA: 0x00142C6D File Offset: 0x00140E6D
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static int SquaredDist(int x1, int y1, int x2, int y2)
	{
		return (x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2);
	}

	// Token: 0x06002826 RID: 10278 RVA: 0x00142C7E File Offset: 0x00140E7E
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static int SquaredDistTile(WorldTile pT1, WorldTile pT2)
	{
		return (pT1.x - pT2.x) * (pT1.x - pT2.x) + (pT1.y - pT2.y) * (pT1.y - pT2.y);
	}

	// Token: 0x06002827 RID: 10279 RVA: 0x00142CB7 File Offset: 0x00140EB7
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float SquaredDistVec2Float(Vector2 pT1, Vector2 pT2)
	{
		return (pT1.x - pT2.x) * (pT1.x - pT2.x) + (pT1.y - pT2.y) * (pT1.y - pT2.y);
	}

	// Token: 0x06002828 RID: 10280 RVA: 0x00142CF0 File Offset: 0x00140EF0
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static int SquaredDistVec2(Vector2Int pT1, Vector2Int pT2)
	{
		return (pT1.x - pT2.x) * (pT1.x - pT2.x) + (pT1.y - pT2.y) * (pT1.y - pT2.y);
	}

	// Token: 0x06002829 RID: 10281 RVA: 0x00142D3C File Offset: 0x00140F3C
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float SquaredDistVec3(Vector3 pT1, Vector3 pT2)
	{
		return (pT1.x - pT2.x) * (pT1.x - pT2.x) + (pT1.y - pT2.y) * (pT1.y - pT2.y);
	}

	// Token: 0x0600282A RID: 10282 RVA: 0x00142D78 File Offset: 0x00140F78
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Color makeColor(string pHex)
	{
		Color tColor;
		ColorUtility.TryParseHtmlString(pHex, out tColor);
		return tColor;
	}

	// Token: 0x0600282B RID: 10283 RVA: 0x00142D90 File Offset: 0x00140F90
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Color makeColor(string pHex, float pAlpha)
	{
		Color tColor;
		ColorUtility.TryParseHtmlString(pHex, out tColor);
		tColor.a = pAlpha;
		return tColor;
	}

	// Token: 0x0600282C RID: 10284 RVA: 0x00142DB0 File Offset: 0x00140FB0
	public unsafe static string colorToHex(Color32 pColor, bool pAlpha = true)
	{
		if (pAlpha)
		{
			Span<char> buffer = new Span<char>(stackalloc byte[(UIntPtr)18], 9);
			*buffer[0] = '#';
			ColorUtility.ToHtmlStringRGBA(pColor).AsSpan().CopyTo(buffer.Slice(1));
			return new string(buffer);
		}
		Span<char> buffer2 = new Span<char>(stackalloc byte[(UIntPtr)14], 7);
		*buffer2[0] = '#';
		ColorUtility.ToHtmlStringRGB(pColor).AsSpan().CopyTo(buffer2.Slice(1));
		return new string(buffer2);
	}

	// Token: 0x0600282D RID: 10285 RVA: 0x00142E48 File Offset: 0x00141048
	public static string coloredString(string pText, string pColor)
	{
		if (string.IsNullOrEmpty(pColor))
		{
			return pText;
		}
		string result;
		using (StringBuilderPool tSB = new StringBuilderPool())
		{
			tSB.Append("<color=").Append(pColor).Append(">").Append(pText).Append("</color>");
			result = tSB.ToString();
		}
		return result;
	}

	// Token: 0x0600282E RID: 10286 RVA: 0x00142EB8 File Offset: 0x001410B8
	public static string colorBetween(double pValue, double pMin, double pMax, string pMinColor = "#FB2C21", string pMaxColor = "#43FF43")
	{
		float tPercent = 100f;
		if (pMax - pMin != 0.0)
		{
			tPercent = (float)(pValue - pMin) / (float)(pMax - pMin);
		}
		return Toolbox.colorToHex(Color.Lerp(Toolbox.makeColor(pMinColor), Toolbox.makeColor(pMaxColor), tPercent), true);
	}

	// Token: 0x0600282F RID: 10287 RVA: 0x00142F04 File Offset: 0x00141104
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float getAngle(float pX1, float pY1, float pX2, float pY2)
	{
		float deltaX = pX2 - pX1;
		return (float)Math.Atan2((double)(pY2 - pY1), (double)deltaX);
	}

	// Token: 0x06002830 RID: 10288 RVA: 0x00142F24 File Offset: 0x00141124
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Quaternion getEulerAngle(float pX1, float pY1, float pX2, float pY2)
	{
		float tAngle = Toolbox.getAngleDegrees(pX1, pY1, pX2, pY2);
		return Quaternion.Euler(new Vector3(0f, 0f, tAngle));
	}

	// Token: 0x06002831 RID: 10289 RVA: 0x00142F50 File Offset: 0x00141150
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Quaternion getEulerAngle(Vector2 pVec1, Vector2 pVec2)
	{
		float tAngle = Toolbox.getAngleDegrees(pVec1, pVec2);
		return Quaternion.Euler(new Vector3(0f, 0f, tAngle));
	}

	// Token: 0x06002832 RID: 10290 RVA: 0x00142F7A File Offset: 0x0014117A
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float getAngleDegrees(Vector2 pVec1, Vector2 pVec2)
	{
		return Toolbox.getAngle(pVec1.x, pVec1.y, pVec2.x, pVec2.y) * 57.29578f;
	}

	// Token: 0x06002833 RID: 10291 RVA: 0x00142F9F File Offset: 0x0014119F
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float getAngleDegrees(float pX1, float pY1, float pX2, float pY2)
	{
		return Toolbox.getAngle(pX1, pY1, pX2, pY2) * 57.29578f;
	}

	// Token: 0x06002834 RID: 10292 RVA: 0x00142FB0 File Offset: 0x001411B0
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Color makeDarkerColor(Color pColor, float pMod = 0.4f)
	{
		return new Color(pColor.r * pMod, pColor.g * pMod, pColor.b * pMod, pColor.a);
	}

	// Token: 0x06002835 RID: 10293 RVA: 0x00142FD5 File Offset: 0x001411D5
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string makeDarkerColor(string pHexColor, float pMod = 0.4f)
	{
		return Toolbox.colorToHex(Toolbox.makeDarkerColor(Toolbox.makeColor(pHexColor), pMod), true);
	}

	// Token: 0x06002836 RID: 10294 RVA: 0x00142FF0 File Offset: 0x001411F0
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Color blendColor(Color pFrom, Color pTo, float amount)
	{
		float r = pFrom.r * amount + pTo.r * (1f - amount);
		float g = pFrom.g * amount + pTo.g * (1f - amount);
		float b = pFrom.b * amount + pTo.b * (1f - amount);
		return new Color(r, g, b);
	}

	// Token: 0x06002837 RID: 10295 RVA: 0x0014304C File Offset: 0x0014124C
	public unsafe static Vector2Int getClosestTile(Span<Vector2Int> pArray, WorldTile pTarget)
	{
		Vector2Int tBest = default(Vector2Int);
		Span<Vector2Int> tArray = pArray;
		int tCount = tArray.Length;
		int tBestDist = int.MaxValue;
		for (int i = 0; i < tCount; i++)
		{
			Vector2Int tTile = *tArray[i];
			int tDist = Toolbox.SquaredDist(pTarget.x, pTarget.y, tTile.x, tTile.y);
			if (tDist < tBestDist)
			{
				tBestDist = tDist;
				tBest = tTile;
			}
		}
		return tBest;
	}

	// Token: 0x06002838 RID: 10296 RVA: 0x001430C0 File Offset: 0x001412C0
	public static WorldTile getClosestTile(WorldTile[] pArray, WorldTile pTarget)
	{
		WorldTile tBest = null;
		int tCount = pArray.Length;
		int tBestDist = int.MaxValue;
		for (int i = 0; i < tCount; i++)
		{
			WorldTile tTile = pArray[i];
			int tDist = Toolbox.SquaredDist(pTarget.x, pTarget.y, tTile.x, tTile.y);
			if (tDist < tBestDist)
			{
				tBestDist = tDist;
				tBest = tTile;
			}
		}
		return tBest;
	}

	// Token: 0x06002839 RID: 10297 RVA: 0x00143120 File Offset: 0x00141320
	public static WorldTile getClosestTile(List<WorldTile> pArray, WorldTile pTarget)
	{
		WorldTile tBest = null;
		int tCount = pArray.Count;
		int tBestDist = int.MaxValue;
		for (int i = 0; i < tCount; i++)
		{
			WorldTile tTile = pArray[i];
			int tDist = Toolbox.SquaredDist(pTarget.x, pTarget.y, tTile.x, tTile.y);
			if (tDist < tBestDist)
			{
				tBestDist = tDist;
				tBest = tTile;
			}
		}
		return tBest;
	}

	// Token: 0x0600283A RID: 10298 RVA: 0x00143188 File Offset: 0x00141388
	public static WorldTile getClosestTile(ListPool<WorldTile> pArray, WorldTile pTarget)
	{
		WorldTile tBest = null;
		int tCount = pArray.Count;
		int tBestDist = int.MaxValue;
		for (int i = 0; i < tCount; i++)
		{
			WorldTile tTile = pArray[i];
			int tDist = Toolbox.SquaredDist(pTarget.x, pTarget.y, tTile.x, tTile.y);
			if (tDist < tBestDist)
			{
				tBestDist = tDist;
				tBest = tTile;
			}
		}
		return tBest;
	}

	// Token: 0x0600283B RID: 10299 RVA: 0x001431F0 File Offset: 0x001413F0
	public static void sortRegionsByDistance(WorldTile pTile, List<MapRegion> pRegions)
	{
		pRegions.Sort((MapRegion x, MapRegion y) => Toolbox.SquaredDistTile(pTile, x.tiles[0]).CompareTo(Toolbox.SquaredDistTile(pTile, y.tiles[0])));
	}

	// Token: 0x0600283C RID: 10300 RVA: 0x0014321C File Offset: 0x0014141C
	public static void sortTilesByDistance(WorldTile pTile, ListPool<WorldTile> pTiles)
	{
		pTiles.Sort((WorldTile x, WorldTile y) => Toolbox.SquaredDistTile(pTile, x).CompareTo(Toolbox.SquaredDistTile(pTile, y)));
	}

	// Token: 0x0600283D RID: 10301 RVA: 0x00143248 File Offset: 0x00141448
	public static float maxTileDistance(WorldTile pTile, ListPool<WorldTile> pTiles)
	{
		float tMaxDistance = 0f;
		for (int i = 0; i < pTiles.Count; i++)
		{
			WorldTile tTile = pTiles[i];
			float tDist = Toolbox.DistTile(pTile, tTile);
			if (tDist > tMaxDistance)
			{
				tMaxDistance = tDist;
			}
		}
		return tMaxDistance;
	}

	// Token: 0x0600283E RID: 10302 RVA: 0x00143284 File Offset: 0x00141484
	public static MapRegion getClosestRegion(List<MapRegion> pArray, WorldTile pTarget)
	{
		MapRegion tBest = null;
		int tBestDist = int.MaxValue;
		for (int i = 0; i < pArray.Count; i++)
		{
			MapRegion tRegion = pArray[i];
			int tDist = Toolbox.SquaredDist(pTarget.pos.x, pTarget.pos.y, tRegion.tiles[0].pos.x, tRegion.tiles[0].pos.y);
			if (tDist < tBestDist)
			{
				tBestDist = tDist;
				tBest = tRegion;
			}
		}
		return tBest;
	}

	// Token: 0x0600283F RID: 10303 RVA: 0x00143318 File Offset: 0x00141518
	public static Vector2Int getRandomVectorWithinDistance(int pX, int pY, int pRange)
	{
		Vector2 tBottomLeft = new Vector2((float)(pX - pRange), (float)(pY - pRange));
		Vector2 tTopRight = new Vector2((float)(pX + pRange), (float)(pY + pRange));
		Toolbox.clampToMap(ref tBottomLeft);
		Toolbox.clampToMap(ref tTopRight);
		Vector2 tRandomPosition = default(Vector2);
		tRandomPosition.x = Randy.randomFloat(tBottomLeft.x, tTopRight.x);
		tRandomPosition.y = Randy.randomFloat(tBottomLeft.y, tTopRight.y);
		return new Vector2Int((int)tRandomPosition.x, (int)tRandomPosition.y);
	}

	// Token: 0x06002840 RID: 10304 RVA: 0x0014339C File Offset: 0x0014159C
	public static WorldTile getRandomTileWithinDistance(WorldTile pWorldTile, int pRange)
	{
		Vector2Int tRandomPosition = Toolbox.getRandomVectorWithinDistance(pWorldTile.pos.x, pWorldTile.pos.y, pRange);
		return World.world.GetTileSimple(tRandomPosition.x, tRandomPosition.y);
	}

	// Token: 0x06002841 RID: 10305 RVA: 0x001433E4 File Offset: 0x001415E4
	public static WorldTile getRandomTileWithinDistance(WorldTile pWorldTile, int pRange, ListPool<WorldTile> pTiles)
	{
		foreach (WorldTile tTile in pTiles.LoopRandom<WorldTile>())
		{
			if (Toolbox.DistTile(pWorldTile, tTile) <= (float)pRange)
			{
				return tTile;
			}
		}
		return Toolbox.getRandomTileWithinDistance(pWorldTile, pRange);
	}

	// Token: 0x06002842 RID: 10306 RVA: 0x00143444 File Offset: 0x00141644
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Actor getClosestActor(HashSet<Actor> pCollection, WorldTile pTile)
	{
		Actor tBest = null;
		int tBestDist = int.MaxValue;
		Vector2Int tTargetPos = pTile.pos;
		foreach (Actor tActor in pCollection)
		{
			if (!tActor.isRekt())
			{
				Vector2Int tTilePos = tActor.current_tile.pos;
				int tDist = Toolbox.SquaredDist(tTargetPos.x, tTargetPos.y, tTilePos.x, tTilePos.y);
				if (tDist < tBestDist)
				{
					tBestDist = tDist;
					tBest = tActor;
				}
			}
		}
		return tBest;
	}

	// Token: 0x06002843 RID: 10307 RVA: 0x001434E4 File Offset: 0x001416E4
	public static Actor getClosestActor(List<Actor> pCollection, WorldTile pTile)
	{
		Actor tBest = null;
		int tBestDist = int.MaxValue;
		Vector2Int tTargetPos = pTile.pos;
		int tCount = pCollection.Count;
		for (int i = 0; i < tCount; i++)
		{
			Actor tActor = pCollection[i];
			Vector2Int tTilePos = tActor.current_tile.pos;
			int tDist = Toolbox.SquaredDist(tTargetPos.x, tTargetPos.y, tTilePos.x, tTilePos.y);
			if (tDist < tBestDist)
			{
				tBestDist = tDist;
				tBest = tActor;
			}
		}
		return tBest;
	}

	// Token: 0x06002844 RID: 10308 RVA: 0x00143560 File Offset: 0x00141760
	public static Actor getClosestActor(ListPool<Actor> pCollection, WorldTile pTile)
	{
		Actor tBest = null;
		int tBestDist = int.MaxValue;
		Vector2Int tTargetPos = pTile.pos;
		int tCount = pCollection.Count;
		for (int i = 0; i < tCount; i++)
		{
			Actor tActor = pCollection[i];
			Vector2Int tTilePos = tActor.current_tile.pos;
			int tDist = Toolbox.SquaredDist(tTargetPos.x, tTargetPos.y, tTilePos.x, tTilePos.y);
			if (tDist < tBestDist)
			{
				tBestDist = tDist;
				tBest = tActor;
			}
		}
		return tBest;
	}

	// Token: 0x06002845 RID: 10309 RVA: 0x001435DC File Offset: 0x001417DC
	public static Building getClosestBuilding(List<Building> pCollection, WorldTile pTile)
	{
		Building tBest = null;
		int tBestDist = int.MaxValue;
		Vector2Int tTargetPos = pTile.pos;
		int tCount = pCollection.Count;
		for (int i = 0; i < tCount; i++)
		{
			Building tBuilding = pCollection[i];
			Vector2Int tTilePos = tBuilding.current_tile.pos;
			int tDist = Toolbox.SquaredDist(tTargetPos.x, tTargetPos.y, tTilePos.x, tTilePos.y);
			if (tDist < tBestDist)
			{
				tBestDist = tDist;
				tBest = tBuilding;
			}
		}
		return tBest;
	}

	// Token: 0x06002846 RID: 10310 RVA: 0x00143658 File Offset: 0x00141858
	public static Task<byte[]> ReadAllBytes(string filePath)
	{
		Toolbox.<ReadAllBytes>d__149 <ReadAllBytes>d__;
		<ReadAllBytes>d__.<>t__builder = AsyncTaskMethodBuilder<byte[]>.Create();
		<ReadAllBytes>d__.filePath = filePath;
		<ReadAllBytes>d__.<>1__state = -1;
		<ReadAllBytes>d__.<>t__builder.Start<Toolbox.<ReadAllBytes>d__149>(ref <ReadAllBytes>d__);
		return <ReadAllBytes>d__.<>t__builder.Task;
	}

	// Token: 0x06002847 RID: 10311 RVA: 0x0014369C File Offset: 0x0014189C
	public static Sprite LoadSprite(string path)
	{
		if (string.IsNullOrEmpty(path))
		{
			return null;
		}
		if (File.Exists(path))
		{
			byte[] bytes = File.ReadAllBytes(path);
			Texture2D texture = new Texture2D(1, 1);
			texture.anisoLevel = 0;
			texture.LoadImage(bytes);
			return Sprite.Create(texture, new Rect(0f, 0f, (float)texture.width, (float)texture.height), new Vector2(0.5f, 0.5f));
		}
		return null;
	}

	// Token: 0x06002848 RID: 10312 RVA: 0x00143710 File Offset: 0x00141910
	public static Sprite LoadResizedSprite(string path, int width, int height)
	{
		Sprite sprite = Toolbox.LoadSprite(path);
		if (sprite == null)
		{
			return null;
		}
		Sprite result = Sprite.Create(Toolbox.ScaleTexture(sprite.texture, width, height), new Rect(0f, 0f, (float)width, (float)height), new Vector2(0f, 0f));
		Object.DestroyImmediate(sprite.texture);
		Object.DestroyImmediate(sprite);
		return result;
	}

	// Token: 0x06002849 RID: 10313 RVA: 0x00143774 File Offset: 0x00141974
	public static Texture2D ScaleTexture(Texture2D source, int targetWidth, int targetHeight)
	{
		Texture2D result = new Texture2D(targetWidth, targetHeight, source.format, true);
		Color[] rpixels = result.GetPixels(0);
		float incX = 1f / (float)source.width * ((float)source.width / (float)targetWidth);
		float incY = 1f / (float)source.height * ((float)source.height / (float)targetHeight);
		for (int px = 0; px < rpixels.Length; px++)
		{
			rpixels[px] = source.GetPixelBilinear(incX * ((float)px % (float)targetWidth), incY * Mathf.Floor((float)(px / targetWidth)));
		}
		result.SetPixels(rpixels, 0);
		result.Apply();
		return result;
	}

	// Token: 0x0600284A RID: 10314 RVA: 0x00143810 File Offset: 0x00141A10
	public static string formatTimer(float pTime)
	{
		int tMinutes = (int)(pTime / 60f);
		int tSeconds = (int)(pTime - (float)(tMinutes * 60));
		string tRes;
		if (tMinutes < 10)
		{
			tRes = "0" + tMinutes.ToString() + ":";
		}
		else
		{
			tRes = tMinutes.ToString() + ":";
		}
		if (tSeconds < 10)
		{
			tRes = tRes + "0" + tSeconds.ToString();
		}
		else
		{
			tRes += tSeconds.ToString();
		}
		return tRes;
	}

	// Token: 0x0600284B RID: 10315 RVA: 0x00143890 File Offset: 0x00141A90
	public static string formatTime(float pTime)
	{
		string result;
		using (StringBuilderPool tSB = new StringBuilderPool())
		{
			TimeSpan timeSpan = TimeSpan.FromSeconds((double)pTime);
			int tWeeks = timeSpan.Days / 7;
			int tDays = timeSpan.Days;
			int tHours = (int)timeSpan.TotalHours;
			if (tWeeks > 0)
			{
				tSB.Append(tWeeks).Append("w ");
				tDays -= tWeeks * 7;
				tHours -= tWeeks * 7 * 24;
			}
			if (tDays > 1)
			{
				tSB.Append(tDays).Append("d ");
				tHours -= tDays * 24;
			}
			tSB.Append(tHours);
			if (timeSpan.Minutes < 10)
			{
				tSB.Append(":0").Append(timeSpan.Minutes);
			}
			else
			{
				tSB.Append(':').Append(timeSpan.Minutes);
			}
			if (timeSpan.Seconds < 10)
			{
				tSB.Append(":0").Append(timeSpan.Seconds);
			}
			else
			{
				tSB.Append(':').Append(timeSpan.Seconds);
			}
			result = tSB.ToString();
		}
		return result;
	}

	// Token: 0x0600284C RID: 10316 RVA: 0x001439B0 File Offset: 0x00141BB0
	public static string formatNumber(long pNumber)
	{
		long tNumber = Math.Abs(pNumber);
		if (tNumber >= 10000000000L)
		{
			return ((double)pNumber / 1000000000.0).ToString("N0") + "b";
		}
		if (tNumber >= 1000000000L)
		{
			return ((double)pNumber / 1000000000.0).ToText() + "b";
		}
		if (tNumber >= 10000000L)
		{
			return ((double)pNumber / 1000000.0).ToString("N0") + "m";
		}
		if (tNumber >= 1000000L)
		{
			return ((double)pNumber / 1000000.0).ToText() + "m";
		}
		if (tNumber >= 10000L)
		{
			return ((float)pNumber / 1000f).ToString("N0") + "k";
		}
		if (tNumber >= 1000L)
		{
			return ((float)pNumber / 1000f).ToText() + "k";
		}
		return pNumber.ToText();
	}

	// Token: 0x0600284D RID: 10317 RVA: 0x00143ABB File Offset: 0x00141CBB
	public static string formatNumber(long pNumber, int pMaxSize)
	{
		if (pNumber.ToText().Length <= pMaxSize)
		{
			return pNumber.ToText();
		}
		return Toolbox.formatNumber(pNumber);
	}

	// Token: 0x0600284E RID: 10318 RVA: 0x00143AD8 File Offset: 0x00141CD8
	internal static void clearAll()
	{
		Toolbox.temp_list_tiles.Clear();
		Toolbox._temp_array_chunks.Clear<MapChunk>();
		Toolbox._temp_array_zones.Clear<TileZone>();
	}

	// Token: 0x0600284F RID: 10319 RVA: 0x00143AF8 File Offset: 0x00141CF8
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal static MapChunk getRandomChunkFromTile(WorldTile pTile)
	{
		ValueTuple<MapChunk[], int> allChunksFromTile = Toolbox.getAllChunksFromTile(pTile);
		MapChunk[] tChunks = allChunksFromTile.Item1;
		int tCount = allChunksFromTile.Item2;
		return tChunks.GetRandom(tCount);
	}

	// Token: 0x06002850 RID: 10320 RVA: 0x00143B1F File Offset: 0x00141D1F
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal static WorldTile getRandomTileAround(WorldTile pTile)
	{
		return Toolbox.getRandomChunkFromTile(pTile).tiles.GetRandom<WorldTile>();
	}

	// Token: 0x06002851 RID: 10321 RVA: 0x00143B31 File Offset: 0x00141D31
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal static ValueTuple<MapChunk[], int> getAllChunksFromTile(WorldTile pTile)
	{
		return Toolbox.getAllChunksFromChunk(pTile.chunk);
	}

	// Token: 0x06002852 RID: 10322 RVA: 0x00143B40 File Offset: 0x00141D40
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal static ValueTuple<MapChunk[], int> getAllChunksFromChunk(MapChunk pChunk)
	{
		MapChunk[] temp_array_chunks = Toolbox._temp_array_chunks;
		temp_array_chunks[0] = pChunk;
		int tCount = pChunk.neighbours_all.Length;
		pChunk.neighbours_all.AsSpan<MapChunk>().CopyTo(Toolbox._temp_array_chunks.AsSpan(1));
		return new ValueTuple<MapChunk[], int>(temp_array_chunks, tCount + 1);
	}

	// Token: 0x06002853 RID: 10323 RVA: 0x00143B85 File Offset: 0x00141D85
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal static ValueTuple<TileZone[], int> getAllZonesFromTile(WorldTile pTile)
	{
		return Toolbox.getAllZonesFromZone(pTile.zone);
	}

	// Token: 0x06002854 RID: 10324 RVA: 0x00143B94 File Offset: 0x00141D94
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal static ValueTuple<TileZone[], int> getAllZonesFromZone(TileZone pZone)
	{
		TileZone[] temp_array_zones = Toolbox._temp_array_zones;
		temp_array_zones[0] = pZone;
		int tCount = pZone.neighbours_all.Length;
		pZone.neighbours_all.AsSpan<TileZone>().CopyTo(Toolbox._temp_array_zones.AsSpan(1));
		return new ValueTuple<TileZone[], int>(temp_array_zones, tCount + 1);
	}

	// Token: 0x06002855 RID: 10325 RVA: 0x00143BDC File Offset: 0x00141DDC
	internal static bool hasDifferentSpeciesInChunkAround(WorldTile pTile, string pSpecies)
	{
		using (IEnumerator<Actor> enumerator = Finder.getUnitsFromChunk(pTile, 1, 0f, false).GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.a.isSameSpecies(pSpecies))
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06002856 RID: 10326 RVA: 0x00143C3C File Offset: 0x00141E3C
	internal static int countUnitsInChunk(WorldTile pTile)
	{
		int tCount = 0;
		foreach (Actor actor in Finder.getUnitsFromChunk(pTile, 0, 0f, false))
		{
			tCount++;
		}
		return tCount;
	}

	// Token: 0x06002857 RID: 10327 RVA: 0x00143C90 File Offset: 0x00141E90
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal static bool inMapBorder(ref Vector2 pPoint)
	{
		return pPoint.x < (float)MapBox.width && pPoint.y < (float)MapBox.height && pPoint.x >= 0f && pPoint.y >= 0f;
	}

	// Token: 0x06002858 RID: 10328 RVA: 0x00143CCD File Offset: 0x00141ECD
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal static bool inMapBorder(ref Vector3 pPoint)
	{
		return pPoint.x < (float)MapBox.width && pPoint.y < (float)MapBox.height && pPoint.x >= 0f && pPoint.y >= 0f;
	}

	// Token: 0x06002859 RID: 10329 RVA: 0x00143D0A File Offset: 0x00141F0A
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal static void clampToMap(ref Vector2 pPos)
	{
		pPos.x = Mathf.Clamp(pPos.x, 0f, (float)(MapBox.width - 1));
		pPos.y = Mathf.Clamp(pPos.y, 0f, (float)(MapBox.height - 1));
	}

	// Token: 0x0600285A RID: 10330 RVA: 0x00143D48 File Offset: 0x00141F48
	internal static IEnumerable<Building> getBuildingsTypeFromChunk(MapChunk pChunk, string pType, bool pOnlyNonTargeted, bool pOnlyWithResources)
	{
		foreach (Building tBuilding in Finder.getBuildingsFromChunk(pChunk.tiles[0], 0, 0, true))
		{
			if ((!pOnlyWithResources || tBuilding.hasResourcesToCollect()) && tBuilding.isUsable() && (!pOnlyNonTargeted || !tBuilding.current_tile.isTargeted()) && tBuilding.asset.type == pType)
			{
				yield return tBuilding;
			}
		}
		IEnumerator<Building> enumerator = null;
		yield break;
		yield break;
	}

	// Token: 0x0600285B RID: 10331 RVA: 0x00143D6D File Offset: 0x00141F6D
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Quaternion LookAt2D(Vector2 forward)
	{
		return Quaternion.Euler(0f, 0f, Mathf.Atan2(forward.y, forward.x) * 57.29578f);
	}

	// Token: 0x0600285C RID: 10332 RVA: 0x00143D98 File Offset: 0x00141F98
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string LowerCaseFirst(string pString)
	{
		if (pString.Length == 0)
		{
			return "";
		}
		return char.ToLower(pString[0]).ToString() + ((pString.Length > 1) ? pString.Substring(1) : string.Empty);
	}

	// Token: 0x0600285D RID: 10333 RVA: 0x00143DE3 File Offset: 0x00141FE3
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T[] resizeArray<T>(T[] pArray, int aPos)
	{
		Array.Resize<T>(ref pArray, aPos);
		return pArray;
	}

	// Token: 0x0600285E RID: 10334 RVA: 0x00143DF0 File Offset: 0x00141FF0
	public static string getRoundedTimestamp()
	{
		DateTime dateTime = DateTime.UtcNow;
		new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		string i = (dateTime.Month < 10) ? ("0" + dateTime.Month.ToString()) : dateTime.Month.ToString();
		string d = (dateTime.Day < 10) ? ("0" + dateTime.Day.ToString()) : dateTime.Day.ToString();
		return dateTime.Year.ToString() + i + d;
	}

	// Token: 0x0600285F RID: 10335 RVA: 0x00143E98 File Offset: 0x00142098
	public static ListPool<string> getDirectories(string pPath)
	{
		ListPool<string> tList = new ListPool<string>();
		foreach (string tStr in Directory.GetDirectories(pPath))
		{
			if (!tStr.Contains(".meta"))
			{
				tList.Add(tStr);
			}
		}
		return tList;
	}

	// Token: 0x06002860 RID: 10336 RVA: 0x00143EDC File Offset: 0x001420DC
	public static ListPool<string> getFiles(string pPath)
	{
		ListPool<string> tList = new ListPool<string>();
		foreach (string tStr in Directory.GetFiles(pPath))
		{
			if (!tStr.Contains(".meta"))
			{
				tList.Add(tStr);
			}
		}
		return tList;
	}

	// Token: 0x06002861 RID: 10337 RVA: 0x00143F20 File Offset: 0x00142120
	public static string cacheBuster()
	{
		return DateTime.UtcNow.RoundMinutes().ToFileTime().ToString() + "_" + Config.versionCodeText;
	}

	// Token: 0x06002862 RID: 10338 RVA: 0x00143F56 File Offset: 0x00142156
	public static DateTime RoundMinutes(this DateTime value)
	{
		return value.RoundMinutes(30);
	}

	// Token: 0x06002863 RID: 10339 RVA: 0x00143F60 File Offset: 0x00142160
	public static DateTime RoundMinutes(this DateTime value, int roundMinutes)
	{
		DateTime result = new DateTime(value.Ticks);
		int minute = value.Minute;
		int hour = value.Hour;
		int minuteMod = minute % roundMinutes;
		if (minuteMod <= roundMinutes / 2)
		{
			result = result.AddMinutes((double)(-(double)minuteMod));
		}
		else
		{
			result = result.AddMinutes((double)(roundMinutes - minuteMod));
		}
		return result;
	}

	// Token: 0x06002864 RID: 10340 RVA: 0x00143FB0 File Offset: 0x001421B0
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static WorldTile getTileAt(float pX, float pY)
	{
		int tX = Mathf.Clamp(Mathf.FloorToInt(pX), 0, MapBox.width - 1);
		int tY = Mathf.Clamp(Mathf.FloorToInt(pY), 0, MapBox.height - 1);
		return World.world.GetTileSimple(tX, tY);
	}

	// Token: 0x06002865 RID: 10341 RVA: 0x00143FF4 File Offset: 0x001421F4
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static WorldTile getNearestTileToCursor()
	{
		Vector2 tMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		return Toolbox.getTileAt(tMousePosition.x, tMousePosition.y);
	}

	// Token: 0x06002866 RID: 10342 RVA: 0x00144028 File Offset: 0x00142228
	public static bool isBlockAt(float pX, float pY)
	{
		WorldTile tTile = Toolbox.getTileAt(pX, pY);
		return tTile != null && tTile.Type.block;
	}

	// Token: 0x06002867 RID: 10343 RVA: 0x00144050 File Offset: 0x00142250
	public static List<string> splitStringIntoList(params string[] pTypes)
	{
		List<string> tList = new List<string>();
		foreach (string tParam in pTypes)
		{
			if (tParam.Contains("#"))
			{
				string[] tSplit = tParam.Split('#', StringSplitOptions.None);
				string tBuildingID = tSplit[0];
				if (tSplit.Length > 2)
				{
					Debug.LogError("WRONG FORMAT - splitStringIntoList" + tParam);
					Debug.LogError("RETURN EMPTY STRING");
					return new List<string>();
				}
				int tAmount = int.Parse(tSplit[1]);
				for (int j = 0; j < tAmount; j++)
				{
					tList.Add(tBuildingID);
				}
			}
			else
			{
				tList.Add(tParam);
			}
		}
		return tList;
	}

	// Token: 0x06002868 RID: 10344 RVA: 0x001440E4 File Offset: 0x001422E4
	public static string[] splitStringIntoArray(params string[] pTypes)
	{
		string[] result;
		using (ListPool<string> tList = new ListPool<string>(pTypes.Length * 2))
		{
			foreach (string tParam in pTypes)
			{
				if (tParam.Contains('#'))
				{
					string[] tSplit = tParam.Split('#', StringSplitOptions.None);
					string tBuildingID = tSplit[0];
					if (tSplit.Length > 2)
					{
						Debug.LogError("WRONG FORMAT - splitStringIntoList" + tParam);
						Debug.LogError("RETURN EMPTY STRING");
						return new string[0];
					}
					int tAmount = int.Parse(tSplit[1]);
					for (int j = 0; j < tAmount; j++)
					{
						tList.Add(tBuildingID);
					}
				}
				else
				{
					tList.Add(tParam);
				}
			}
			result = tList.ToArray<string>();
		}
		return result;
	}

	// Token: 0x06002869 RID: 10345 RVA: 0x001441A4 File Offset: 0x001423A4
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool isFirstLatin(string pString)
	{
		char _char = pString[0];
		return _char >= 'A' && _char <= 'Z';
	}

	// Token: 0x0600286A RID: 10346 RVA: 0x001441C8 File Offset: 0x001423C8
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2 Parabola(Vector2 pStart, Vector2 pEnd, float pHeight, float pTime)
	{
		pTime = Mathf.Clamp(pTime, 0f, 1f);
		Vector2 tMid = Vector2.Lerp(pStart, pEnd, pTime);
		return new Vector2(tMid.x, Toolbox.parabolaHelper(pTime, pHeight) + tMid.y);
	}

	// Token: 0x0600286B RID: 10347 RVA: 0x0014420C File Offset: 0x0014240C
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2 ParabolaDrag(Vector2 pStart, Vector2 pEnd, float pHeight, float pTime)
	{
		pTime = Mathf.Clamp(pTime, 0f, 1f);
		float x = Mathf.Lerp(pStart.x, pEnd.x, iTween.easeOutQuad(0f, 1f, pTime));
		float tMidY = Mathf.Lerp(pStart.y, pEnd.y, iTween.easeInQuad(0f, 1f, pTime));
		return new Vector2(x, Toolbox.parabolaHelper(pTime, pHeight) + tMidY);
	}

	// Token: 0x0600286C RID: 10348 RVA: 0x0014427C File Offset: 0x0014247C
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static float parabolaHelper(float pTime, float pHeight)
	{
		return -4f * pHeight * pTime * pTime + 4f * pHeight * pTime;
	}

	// Token: 0x0600286D RID: 10349 RVA: 0x00144293 File Offset: 0x00142493
	public static bool WriteSafely(string pWhat, string pDataPath, ref string pStringData)
	{
		return Toolbox.WriteSafely(pWhat, pDataPath, ref pStringData, null);
	}

	// Token: 0x0600286E RID: 10350 RVA: 0x001442A0 File Offset: 0x001424A0
	public static bool WriteSafely(string pWhat, string pDataPath, byte[] pByteData)
	{
		string i = null;
		return Toolbox.WriteSafely(pWhat, pDataPath, ref i, pByteData);
	}

	// Token: 0x0600286F RID: 10351 RVA: 0x001442BC File Offset: 0x001424BC
	private static bool WriteSafely(string pWhat, string pDataPath, ref string pStringData, byte[] pByteData)
	{
		bool hasError = false;
		try
		{
			if (!string.IsNullOrEmpty(pStringData))
			{
				File.WriteAllText(pDataPath + ".tmp", pStringData);
			}
			if (pByteData != null)
			{
				File.WriteAllBytes(pDataPath + ".tmp", pByteData);
			}
		}
		catch (IOException e)
		{
			if (Toolbox.IsDiskFull(e))
			{
				WorldTip.showNow("Error saving " + pWhat + " : Disk full!", false, "top", 3f, "#F3961F");
			}
			else
			{
				Debug.Log("Could not save " + pWhat + " due to hard drive / IO Error : ");
				Debug.Log(e);
				WorldTip.showNow("Error saving " + pWhat + " due to IOError! Check console for details", false, "top", 3f, "#F3961F");
			}
			hasError = true;
		}
		catch (Exception message)
		{
			Debug.Log("Could not save " + pWhat + " due to error : ");
			Debug.Log(message);
			WorldTip.showNow("Error saving " + pWhat + "! Check console for errors", false, "top", 3f, "#F3961F");
			hasError = true;
		}
		if (hasError)
		{
			if (File.Exists(pDataPath + ".tmp"))
			{
				File.Delete(pDataPath + ".tmp");
			}
			return false;
		}
		if (File.Exists(pDataPath))
		{
			File.Delete(pDataPath);
		}
		File.Move(pDataPath + ".tmp", pDataPath);
		return true;
	}

	// Token: 0x06002870 RID: 10352 RVA: 0x0014441C File Offset: 0x0014261C
	public static bool MoveSafely(string pOldPath, string pNewPath)
	{
		if (string.IsNullOrEmpty(pOldPath))
		{
			return false;
		}
		if (string.IsNullOrEmpty(pNewPath))
		{
			return false;
		}
		if (File.Exists(pNewPath))
		{
			File.Delete(pNewPath);
		}
		File.Move(pOldPath, pNewPath);
		return true;
	}

	// Token: 0x06002871 RID: 10353 RVA: 0x00144448 File Offset: 0x00142648
	public static bool IsDiskFull(IOException ex)
	{
		return (ex.HResult & 65535) == 39 || (ex.HResult & 65535) == 112;
	}

	// Token: 0x06002872 RID: 10354 RVA: 0x0014446C File Offset: 0x0014266C
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string textureID(string pStringData, string pID)
	{
		return Encryption.EncryptString(pStringData, pID);
	}

	// Token: 0x06002873 RID: 10355 RVA: 0x00144478 File Offset: 0x00142678
	public static int getClosestAngle(int pAngle, AnimationDataBoat pData)
	{
		int tBestKey = int.MinValue;
		float tLastClosest = 0f;
		foreach (int tKey in pData.dict.Keys)
		{
			float tClosest = (float)Mathf.Abs(tKey - pAngle);
			if (tClosest < tLastClosest || tBestKey == -2147483648)
			{
				tLastClosest = tClosest;
				tBestKey = tKey;
			}
		}
		return tBestKey;
	}

	// Token: 0x06002874 RID: 10356 RVA: 0x001444F4 File Offset: 0x001426F4
	public static bool isInTriangle(Vector2 pPoint, Vector2 p0, Vector2 p1, Vector2 p2)
	{
		float tAngle = 0.5f * (-p1.y * p2.x + p0.y * (-p1.x + p2.x) + p0.x * (p1.y - p2.y) + p1.x * p2.y);
		int tSign = (tAngle < 0f) ? -1 : 1;
		float s = (p0.y * p2.x - p0.x * p2.y + (p2.y - p0.y) * pPoint.x + (p0.x - p2.x) * pPoint.y) * (float)tSign;
		float t = (p0.x * p1.y - p0.y * p1.x + (p0.y - p1.y) * pPoint.x + (p1.x - p0.x) * pPoint.y) * (float)tSign;
		return s > 0f && t > 0f && s + t < 2f * tAngle * (float)tSign;
	}

	// Token: 0x06002875 RID: 10357 RVA: 0x00144610 File Offset: 0x00142810
	public static List<string> getListForSave<T>(IReadOnlyCollection<T> pList) where T : Asset
	{
		List<string> tListResult = new List<string>(pList.Count);
		foreach (T tAsset in pList)
		{
			tListResult.Add(tAsset.id);
		}
		return tListResult;
	}

	// Token: 0x06002876 RID: 10358 RVA: 0x00144670 File Offset: 0x00142870
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T[] checkArraySize<T>(T[] pArray, int pTargetSize)
	{
		if (pArray == null || pTargetSize > pArray.Length)
		{
			pArray = new T[Toolbox.nextPowerOfTwo(pTargetSize)];
		}
		return pArray;
	}

	// Token: 0x06002877 RID: 10359 RVA: 0x00144689 File Offset: 0x00142889
	private static int nextPowerOfTwo(int pN)
	{
		pN--;
		pN |= pN >> 1;
		pN |= pN >> 2;
		pN |= pN >> 4;
		pN |= pN >> 8;
		pN |= pN >> 16;
		pN++;
		return pN;
	}

	// Token: 0x06002878 RID: 10360 RVA: 0x001446BC File Offset: 0x001428BC
	public unsafe static string fillLeft(string pString, int pSize = 1, char pFill = ' ')
	{
		string tStripped = Toolbox.removeRichTextTags(pString);
		if (pString != tStripped)
		{
			pSize += pString.Length - tStripped.Length;
		}
		if (pString.Length >= pSize)
		{
			return pString;
		}
		int num = pSize;
		Span<char> tSpan = new Span<char>(stackalloc byte[checked(unchecked((UIntPtr)num) * 2)], num);
		pString.AsSpan().CopyTo(tSpan.Slice(pSize - pString.Length));
		for (int i = 0; i < pSize - pString.Length; i++)
		{
			*tSpan[i] = pFill;
		}
		return new string(tSpan);
	}

	// Token: 0x06002879 RID: 10361 RVA: 0x00144750 File Offset: 0x00142950
	public unsafe static void fillRight(ref string pString, int pSize = 1, char pFill = ' ')
	{
		int tSize = pString.Length;
		if (Toolbox.removeRichTextTags(ref pString))
		{
			pSize += tSize - pString.Length;
		}
		if (pString.Length >= pSize)
		{
			return;
		}
		int num = pSize;
		Span<char> tSpan = new Span<char>(stackalloc byte[checked(unchecked((UIntPtr)num) * 2)], num);
		pString.AsSpan().CopyTo(tSpan.Slice(0, pString.Length));
		for (int i = pString.Length; i < pSize; i++)
		{
			*tSpan[i] = pFill;
		}
		pString = new string(tSpan);
	}

	// Token: 0x0600287A RID: 10362 RVA: 0x001447E0 File Offset: 0x001429E0
	public unsafe static string fillRight(string pString, int pSize = 1, char pFill = ' ')
	{
		string tStripped = Toolbox.removeRichTextTags(pString);
		if (pString != tStripped)
		{
			pSize += pString.Length - tStripped.Length;
		}
		if (pString.Length >= pSize)
		{
			return pString;
		}
		int num = pSize;
		Span<char> tSpan = new Span<char>(stackalloc byte[checked(unchecked((UIntPtr)num) * 2)], num);
		pString.AsSpan().CopyTo(tSpan.Slice(0, pString.Length));
		for (int i = pString.Length; i < pSize; i++)
		{
			*tSpan[i] = pFill;
		}
		return new string(tSpan);
	}

	// Token: 0x0600287B RID: 10363 RVA: 0x00144870 File Offset: 0x00142A70
	public static string printRows(ListPool<string[]> pRows, string pAlign = "right", bool pSkipFormatting = false)
	{
		int tColumns = 0;
		int tRowCount = pRows.Count;
		for (int i = 0; i < tRowCount; i++)
		{
			string[] tRow = pRows[i];
			if (tRow.Length > tColumns)
			{
				tColumns = tRow.Length;
			}
		}
		int[] tSizes = new int[tColumns];
		for (int j = 0; j < tRowCount; j++)
		{
			string[] tRow2 = pRows[j];
			for (int k = 0; k < tRow2.Length; k++)
			{
				int tLength = Toolbox.removeRichTextTags(tRow2[k]).Length;
				if (tLength > tSizes[k])
				{
					tSizes[k] = tLength;
				}
			}
		}
		string result;
		using (StringBuilderPool tBuilder = new StringBuilderPool())
		{
			for (int l = 0; l <= tRowCount; l++)
			{
				if (l == 0 || l == tRowCount || pRows[l].Length == 0)
				{
					tBuilder.Append("|");
					for (int m = 0; m < tColumns; m++)
					{
						if (tSizes[m] != 0)
						{
							tBuilder.Append(Toolbox.fillRight("", tSizes[m] + 2, '='));
							tBuilder.Append("|");
						}
					}
					tBuilder.Append("\n");
					if (l == tRowCount)
					{
						break;
					}
				}
				string[] tRow3 = pRows[l];
				if (tRow3.Length != 0)
				{
					tBuilder.Append("|");
					for (int n = 0; n < tColumns; n++)
					{
						if (tSizes[n] != 0)
						{
							string tString = "";
							if (n < tRow3.Length)
							{
								tString = tRow3[n];
							}
							tBuilder.Append(" ");
							if (n == 0)
							{
								if (!pSkipFormatting)
								{
									tBuilder.Append("<b>");
								}
								tBuilder.Append(Toolbox.fillRight(tString, tSizes[n], ' '));
								if (!pSkipFormatting)
								{
									tBuilder.Append("</b>");
								}
							}
							else if (pAlign == "right")
							{
								tBuilder.Append(Toolbox.fillLeft(tString, tSizes[n], ' '));
							}
							else
							{
								tBuilder.Append(Toolbox.fillRight(tString, tSizes[n], ' '));
							}
							tBuilder.Append(" ");
							tBuilder.Append("|");
						}
					}
					tBuilder.Append("\n");
				}
			}
			result = tBuilder.ToString();
		}
		return result;
	}

	// Token: 0x0600287C RID: 10364 RVA: 0x00144AB0 File Offset: 0x00142CB0
	public static string printColumns(params ListPool<string>[] pLists)
	{
		int tMaxLines = 0;
		int tCount = pLists.Length;
		int[] tSizes = new int[tCount];
		for (int i = 0; i < tCount; i++)
		{
			ListPool<string> tList = pLists[i];
			if (tList.Count > tMaxLines)
			{
				tMaxLines = tList.Count;
			}
			for (int j = 0; j < tList.Count; j++)
			{
				int tLength = Toolbox.removeRichTextTags(tList[j]).Length;
				if (tLength > tSizes[i])
				{
					tSizes[i] = tLength;
				}
			}
		}
		string result;
		using (StringBuilderPool tBuilder = new StringBuilderPool())
		{
			for (int k = 0; k < tMaxLines; k++)
			{
				if (k == 0 || k == 1)
				{
					tBuilder.Append("|");
					for (int l = 0; l < tCount; l++)
					{
						if (tSizes[l] != 0)
						{
							tBuilder.Append(Toolbox.fillRight("", tSizes[l] + 2, '='));
							tBuilder.Append("|");
						}
					}
					tBuilder.Append("\n");
				}
				tBuilder.Append("|");
				for (int m = 0; m < tCount; m++)
				{
					if (tSizes[m] != 0)
					{
						string tString = "";
						if (k < pLists[m].Count)
						{
							tString = pLists[m][k];
						}
						tBuilder.Append(" ");
						if (k == 0)
						{
							tBuilder.Append("<b>");
						}
						tBuilder.Append(Toolbox.fillRight(tString, tSizes[m] + 1, ' '));
						if (k == 0)
						{
							tBuilder.Append("</b>");
						}
						tBuilder.Append("|");
					}
				}
				tBuilder.Append("\n");
				if (k == tMaxLines - 1)
				{
					tBuilder.Append("|");
					for (int n = 0; n < tCount; n++)
					{
						if (tSizes[n] != 0)
						{
							tBuilder.Append(Toolbox.fillRight("", tSizes[n] + 2, '='));
							tBuilder.Append("|");
						}
					}
					tBuilder.Append("\n");
				}
			}
			result = tBuilder.ToString();
		}
		return result;
	}

	// Token: 0x0600287D RID: 10365 RVA: 0x00144CCC File Offset: 0x00142ECC
	public unsafe static string getRepeatedString(char pChar, int pCount)
	{
		Span<char> tSpan = new Span<char>(stackalloc byte[checked(unchecked((UIntPtr)pCount) * 2)], pCount);
		tSpan.Fill(pChar);
		return new string(tSpan);
	}

	// Token: 0x0600287E RID: 10366 RVA: 0x00144CFC File Offset: 0x00142EFC
	public static bool removeRichTextTags(ref string pInput)
	{
		bool tChanged = false;
		for (;;)
		{
			int tStart = pInput.IndexOf('<');
			if (tStart == -1)
			{
				break;
			}
			int tEnd = pInput.IndexOf('>', tStart);
			if (tEnd == -1)
			{
				return tChanged;
			}
			pInput = pInput.Remove(tStart, tEnd - tStart + 1);
			tChanged = true;
		}
		return tChanged;
	}

	// Token: 0x0600287F RID: 10367 RVA: 0x00144D40 File Offset: 0x00142F40
	public static string removeRichTextTags(string pInput)
	{
		for (;;)
		{
			int tStart = pInput.IndexOf('<');
			if (tStart == -1)
			{
				break;
			}
			int tEnd = pInput.IndexOf('>', tStart);
			if (tEnd == -1)
			{
				return pInput;
			}
			pInput = pInput.Remove(tStart, tEnd - tStart + 1);
		}
		return pInput;
	}

	// Token: 0x06002880 RID: 10368 RVA: 0x00144D7C File Offset: 0x00142F7C
	public static bool areListsEqual<T>(IList<T> pList1, IList<T> pList2)
	{
		HashSet<T> hashSet = UnsafeCollectionPool<HashSet<T>, T>.Get();
		hashSet.UnionWith(pList1);
		bool tEquals = hashSet.SetEquals(pList2);
		UnsafeCollectionPool<HashSet<T>, T>.Release(hashSet);
		return tEquals;
	}

	// Token: 0x06002881 RID: 10369 RVA: 0x00144DA3 File Offset: 0x00142FA3
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static TA[] a<TA>(params TA[] pArgs)
	{
		return pArgs;
	}

	// Token: 0x06002882 RID: 10370 RVA: 0x00144DA6 File Offset: 0x00142FA6
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static List<TL> l<TL>(params TL[] pArgs)
	{
		return List.Of<TL>(pArgs);
	}

	// Token: 0x06002883 RID: 10371 RVA: 0x00144DAE File Offset: 0x00142FAE
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static HashSet<TH> h<TH>(params TH[] pArgs)
	{
		return new HashSet<TH>(pArgs);
	}

	// Token: 0x06002884 RID: 10372 RVA: 0x00144DB8 File Offset: 0x00142FB8
	// Note: this type is marked as 'beforefieldinit'.
	static Toolbox()
	{
		Dictionary<ActorDirection, ActorDirection[]> dictionary = new Dictionary<ActorDirection, ActorDirection[]>();
		dictionary.Add(ActorDirection.Up, new ActorDirection[]
		{
			ActorDirection.Right,
			ActorDirection.Left
		});
		Dictionary<ActorDirection, ActorDirection[]> dictionary2 = dictionary;
		ActorDirection key = ActorDirection.Right;
		ActorDirection[] array = new ActorDirection[2];
		array[0] = ActorDirection.Down;
		dictionary2.Add(key, array);
		dictionary.Add(ActorDirection.Down, new ActorDirection[]
		{
			ActorDirection.Left,
			ActorDirection.Right
		});
		dictionary.Add(ActorDirection.Left, new ActorDirection[]
		{
			ActorDirection.Up,
			ActorDirection.Down
		});
		Toolbox.directions_turns = dictionary;
		Toolbox.directions_all_turns = new Dictionary<ActorDirection, ActorDirection[]>
		{
			{
				ActorDirection.Up,
				new ActorDirection[]
				{
					ActorDirection.Right,
					ActorDirection.UpRight,
					ActorDirection.UpLeft,
					ActorDirection.Left
				}
			},
			{
				ActorDirection.UpRight,
				new ActorDirection[]
				{
					ActorDirection.DownRight,
					ActorDirection.Right,
					ActorDirection.Up,
					ActorDirection.UpLeft
				}
			},
			{
				ActorDirection.Right,
				new ActorDirection[]
				{
					ActorDirection.Down,
					ActorDirection.DownRight,
					ActorDirection.UpRight,
					ActorDirection.Up
				}
			},
			{
				ActorDirection.DownRight,
				new ActorDirection[]
				{
					ActorDirection.DownLeft,
					ActorDirection.Down,
					ActorDirection.Right,
					ActorDirection.UpRight
				}
			},
			{
				ActorDirection.Down,
				new ActorDirection[]
				{
					ActorDirection.Left,
					ActorDirection.DownLeft,
					ActorDirection.DownRight,
					ActorDirection.Right
				}
			},
			{
				ActorDirection.DownLeft,
				new ActorDirection[]
				{
					ActorDirection.UpLeft,
					ActorDirection.Left,
					ActorDirection.Down,
					ActorDirection.DownRight
				}
			},
			{
				ActorDirection.Left,
				new ActorDirection[]
				{
					ActorDirection.Up,
					ActorDirection.UpLeft,
					ActorDirection.DownLeft,
					ActorDirection.Down
				}
			},
			{
				ActorDirection.UpLeft,
				new ActorDirection[]
				{
					ActorDirection.UpRight,
					ActorDirection.Up,
					ActorDirection.Left,
					ActorDirection.DownLeft
				}
			}
		};
		Toolbox.EVERYTHING_MAGIC_COLOR32 = Toolbox.makeColor("#DF7FFF");
		Toolbox.color_grey_dark = Toolbox.makeColor("#5D5D5D");
		Toolbox.color_grey = Toolbox.makeColor("#AAAAAA");
		Toolbox.color_transparent_grey = Toolbox.makeColor("#666666", 0.5f);
		Toolbox.color_debug_bar_blue = Toolbox.makeColor("#0092FF", 0.5f);
		Toolbox.color_debug_bar_red = Toolbox.makeColor("#FF6262", 0.5f);
		Toolbox.color_phenotype_green_0 = Toolbox.makeColor("#B8FF96");
		Toolbox.color_phenotype_green_1 = Toolbox.makeColor("#00FF00");
		Toolbox.color_phenotype_green_2 = Toolbox.makeColor("#00AF00");
		Toolbox.color_phenotype_green_3 = Toolbox.makeColor("#4A831F");
		Toolbox.color_map_icon_green = Toolbox.makeColor("#00FF00");
		Toolbox.color_magenta_0 = Toolbox.makeColor("#FF00FF");
		Toolbox.color_magenta_1 = Toolbox.makeColor("#DE00DE");
		Toolbox.color_magenta_2 = Toolbox.makeColor("#A700A7");
		Toolbox.color_magenta_3 = Toolbox.makeColor("#7F007F");
		Toolbox.color_magenta_4 = Toolbox.makeColor("#580058");
		Toolbox.color_teal_0 = Toolbox.makeColor("#00EFEF");
		Toolbox.color_teal_1 = Toolbox.makeColor("#00DBDB");
		Toolbox.color_teal_2 = Toolbox.makeColor("#00BCBC");
		Toolbox.color_teal_3 = Toolbox.makeColor("#009E9E");
		Toolbox.color_teal_4 = Toolbox.makeColor("#007777");
		Toolbox.color_ocean = Toolbox.makeColor("#3370CC");
		Toolbox.color_night = Toolbox.makeColor("#05003F");
		Toolbox.color_light = Toolbox.makeColor("#FFD800");
		Toolbox.color_light_100 = Toolbox.makeColor("#FFFFFF");
		Toolbox.color_light_10 = Toolbox.makeColor("#FFFFFF", 0.3f);
		Toolbox.color_light_replace = Toolbox.makeColor("#000000");
		Toolbox.color_augmentation_selected = Color.white;
		Toolbox.color_augmentation_unselected = new Color(0.7f, 0.7f, 0.7f, 1f);
		Toolbox.color_clear = Color.clear;
		Toolbox.color_white = Color.white;
		Toolbox.color_gray = Color.gray;
		Toolbox.color_black = Color.black;
		Toolbox.color_black_32 = Color.black;
		Toolbox.color_white_32 = Color.white;
		Toolbox.color_red = Color.red;
		Toolbox.color_yellow = Color.yellow;
		Toolbox.color_blue = Color.blue;
		Toolbox.color_green = Color.green;
		Toolbox.color_purple = new Color(0.5f, 0f, 0.5f);
		Toolbox.color_cyan = Color.cyan;
		Toolbox.color_cursed = new Color(1f, 0f, 0.8352941f);
		Toolbox.color_abandoned_building = new Color(0.8f, 0.8f, 0.8f);
		Toolbox.color_positive_RGBA = Toolbox.makeColor("#43FF43");
		Toolbox.color_negative_RGBA = Toolbox.makeColor("#FB2C21");
		Toolbox.clear = Color.clear;
		Toolbox.edge_alpha = Toolbox.makeColor("#000000", 0.1f);
		Toolbox.color_white_transparent = Toolbox.makeColor("#FFFFFF", 0f);
		Toolbox.color_text_default = Toolbox.makeColor("#FF9B1C");
		Toolbox.color_text_default_bright = Toolbox.makeColor("#FFBC66");
		Toolbox.color_log_good = Toolbox.makeColor("#95DD5D");
		Toolbox.color_log_warning = Toolbox.makeColor("#FF8686");
		Toolbox.color_log_neutral = Toolbox.makeColor("#F3961F");
		Toolbox.color_fire = Toolbox.makeColor("#FF6930");
		Toolbox.color_heal = Toolbox.makeColor("#23F3FF");
		Toolbox.color_plague = Toolbox.makeColor("#CE4A9B");
		Toolbox.color_mushSpores = Toolbox.makeColor("#8CFF99");
		Toolbox.color_infected = Toolbox.makeColor("#35CC6E");
		Toolbox.color_poisoned = Toolbox.makeColor("#D85BC5");
		Toolbox.colors_fire = new Color[]
		{
			Toolbox.makeColor("#D95032"),
			Toolbox.makeColor("#F27F3D"),
			Toolbox.makeColor("#F2A444"),
			Toolbox.makeColor("#F2C36B"),
			Toolbox.makeColor("#F2CA50"),
			Toolbox.makeColor("#E35632"),
			Toolbox.makeColor("#EEB543"),
			Color.red,
			Color.yellow,
			Color.white
		};
		Toolbox.colors_wheat = new Color[]
		{
			Toolbox.makeColor("#20B22B"),
			Toolbox.makeColor("#2A8E31"),
			Toolbox.makeColor("#20B22B"),
			Toolbox.makeColor("#74A926"),
			Toolbox.makeColor("#FFEB93")
		};
		Toolbox.temp_list_tiles = new List<WorldTile>();
		Toolbox._temp_array_chunks = new MapChunk[9];
		Toolbox._temp_array_zones = new TileZone[9];
	}

	// Token: 0x04001E24 RID: 7716
	public static readonly ActorDirection[] directions = new ActorDirection[]
	{
		ActorDirection.Up,
		ActorDirection.Right,
		ActorDirection.Down,
		ActorDirection.Left
	};

	// Token: 0x04001E25 RID: 7717
	public static readonly ActorDirection[] directions_all = new ActorDirection[]
	{
		ActorDirection.Up,
		ActorDirection.UpRight,
		ActorDirection.UpLeft,
		ActorDirection.Right,
		ActorDirection.DownRight,
		ActorDirection.DownLeft,
		ActorDirection.Down,
		ActorDirection.Left
	};

	// Token: 0x04001E26 RID: 7718
	public static readonly Dictionary<ActorDirection, ActorDirection[]> directions_turns;

	// Token: 0x04001E27 RID: 7719
	public static readonly Dictionary<ActorDirection, ActorDirection[]> directions_all_turns;

	// Token: 0x04001E28 RID: 7720
	public static readonly Color32 EVERYTHING_MAGIC_COLOR32;

	// Token: 0x04001E29 RID: 7721
	public static readonly Color32 color_grey_dark;

	// Token: 0x04001E2A RID: 7722
	public static readonly Color32 color_grey;

	// Token: 0x04001E2B RID: 7723
	public static readonly Color32 color_transparent_grey;

	// Token: 0x04001E2C RID: 7724
	public static readonly Color32 color_debug_bar_blue;

	// Token: 0x04001E2D RID: 7725
	public static readonly Color32 color_debug_bar_red;

	// Token: 0x04001E2E RID: 7726
	public static readonly Color32 color_phenotype_green_0;

	// Token: 0x04001E2F RID: 7727
	public static readonly Color32 color_phenotype_green_1;

	// Token: 0x04001E30 RID: 7728
	public static readonly Color32 color_phenotype_green_2;

	// Token: 0x04001E31 RID: 7729
	public static readonly Color32 color_phenotype_green_3;

	// Token: 0x04001E32 RID: 7730
	public static readonly Color32 color_map_icon_green;

	// Token: 0x04001E33 RID: 7731
	public static readonly Color32 color_magenta_0;

	// Token: 0x04001E34 RID: 7732
	public static readonly Color32 color_magenta_1;

	// Token: 0x04001E35 RID: 7733
	public static readonly Color32 color_magenta_2;

	// Token: 0x04001E36 RID: 7734
	public static readonly Color32 color_magenta_3;

	// Token: 0x04001E37 RID: 7735
	public static readonly Color32 color_magenta_4;

	// Token: 0x04001E38 RID: 7736
	public static readonly Color32 color_teal_0;

	// Token: 0x04001E39 RID: 7737
	public static readonly Color32 color_teal_1;

	// Token: 0x04001E3A RID: 7738
	public static readonly Color32 color_teal_2;

	// Token: 0x04001E3B RID: 7739
	public static readonly Color32 color_teal_3;

	// Token: 0x04001E3C RID: 7740
	public static readonly Color32 color_teal_4;

	// Token: 0x04001E3D RID: 7741
	public static readonly Color32 color_ocean;

	// Token: 0x04001E3E RID: 7742
	public static readonly Color32 color_night;

	// Token: 0x04001E3F RID: 7743
	public static readonly Color32 color_light;

	// Token: 0x04001E40 RID: 7744
	public static readonly Color32 color_light_100;

	// Token: 0x04001E41 RID: 7745
	public static readonly Color32 color_light_10;

	// Token: 0x04001E42 RID: 7746
	public static readonly Color32 color_light_replace;

	// Token: 0x04001E43 RID: 7747
	public static Color color_augmentation_selected;

	// Token: 0x04001E44 RID: 7748
	public static Color color_augmentation_unselected;

	// Token: 0x04001E45 RID: 7749
	public static readonly Color32 color_clear;

	// Token: 0x04001E46 RID: 7750
	public static Color color_white;

	// Token: 0x04001E47 RID: 7751
	public static Color color_gray;

	// Token: 0x04001E48 RID: 7752
	public static Color color_black;

	// Token: 0x04001E49 RID: 7753
	public static Color32 color_black_32;

	// Token: 0x04001E4A RID: 7754
	public static readonly Color32 color_white_32;

	// Token: 0x04001E4B RID: 7755
	public static Color color_red;

	// Token: 0x04001E4C RID: 7756
	public static Color color_yellow;

	// Token: 0x04001E4D RID: 7757
	public static Color color_blue;

	// Token: 0x04001E4E RID: 7758
	public static Color color_green;

	// Token: 0x04001E4F RID: 7759
	public static Color color_purple;

	// Token: 0x04001E50 RID: 7760
	public static Color color_cyan;

	// Token: 0x04001E51 RID: 7761
	public static Color color_cursed;

	// Token: 0x04001E52 RID: 7762
	public static Color color_abandoned_building;

	// Token: 0x04001E53 RID: 7763
	public const string color_positive = "#43FF43";

	// Token: 0x04001E54 RID: 7764
	public const string color_negative = "#FB2C21";

	// Token: 0x04001E55 RID: 7765
	public const string color_positive_light = "#95DD5D";

	// Token: 0x04001E56 RID: 7766
	public const string color_negative_light = "#FF8686";

	// Token: 0x04001E57 RID: 7767
	public static readonly Color color_positive_RGBA;

	// Token: 0x04001E58 RID: 7768
	public static readonly Color color_negative_RGBA;

	// Token: 0x04001E59 RID: 7769
	public const string color_report_positive = "#ADADAD";

	// Token: 0x04001E5A RID: 7770
	public const string color_report_negative = "#919191";

	// Token: 0x04001E5B RID: 7771
	public const string color_hex_white = "#FFFFFF";

	// Token: 0x04001E5C RID: 7772
	public const string color_hex_black = "#000000";

	// Token: 0x04001E5D RID: 7773
	public const string color_hex_neutral = "#F3961F";

	// Token: 0x04001E5E RID: 7774
	public const string color_hex_brighter = "#FFBC66";

	// Token: 0x04001E5F RID: 7775
	public const string color_tooltip_hotkey = "#95DD5D";

	// Token: 0x04001E60 RID: 7776
	public static readonly Color32 clear;

	// Token: 0x04001E61 RID: 7777
	public static readonly Color32 edge_alpha;

	// Token: 0x04001E62 RID: 7778
	public static readonly Color color_white_transparent;

	// Token: 0x04001E63 RID: 7779
	public static readonly Color color_text_default;

	// Token: 0x04001E64 RID: 7780
	public static readonly Color color_text_default_bright;

	// Token: 0x04001E65 RID: 7781
	public static readonly Color color_log_good;

	// Token: 0x04001E66 RID: 7782
	public static readonly Color color_log_warning;

	// Token: 0x04001E67 RID: 7783
	public static readonly Color color_log_neutral;

	// Token: 0x04001E68 RID: 7784
	public static readonly Color32 color_fire;

	// Token: 0x04001E69 RID: 7785
	public const string color_hex_ocean = "#3370CC";

	// Token: 0x04001E6A RID: 7786
	public const string color_hex_blue = "#4CCFFF";

	// Token: 0x04001E6B RID: 7787
	public const string color_hex_red = "#FF637D";

	// Token: 0x04001E6C RID: 7788
	public const string color_hex_green = "#43FF43";

	// Token: 0x04001E6D RID: 7789
	public const string color_hex_purple = "#E060CD";

	// Token: 0x04001E6E RID: 7790
	public const string color_hex_yellow = "#FFFF51";

	// Token: 0x04001E6F RID: 7791
	public const string color_hex_heal = "#23F3FF";

	// Token: 0x04001E70 RID: 7792
	public const string color_hex_plague = "#CE4A9B";

	// Token: 0x04001E71 RID: 7793
	public const string color_hex_mush_spores = "#8CFF99";

	// Token: 0x04001E72 RID: 7794
	public const string color_hex_infected = "#35CC6E";

	// Token: 0x04001E73 RID: 7795
	public const string color_hex_poisoned = "#D85BC5";

	// Token: 0x04001E74 RID: 7796
	public static Color color_heal;

	// Token: 0x04001E75 RID: 7797
	public static Color color_plague;

	// Token: 0x04001E76 RID: 7798
	public static Color color_mushSpores;

	// Token: 0x04001E77 RID: 7799
	public static Color color_infected;

	// Token: 0x04001E78 RID: 7800
	public static Color color_poisoned;

	// Token: 0x04001E79 RID: 7801
	public static readonly Color[] colors_fire;

	// Token: 0x04001E7A RID: 7802
	public static readonly Color[] colors_wheat;

	// Token: 0x04001E7B RID: 7803
	internal static readonly List<WorldTile> temp_list_tiles;

	// Token: 0x04001E7C RID: 7804
	private static readonly MapChunk[] _temp_array_chunks;

	// Token: 0x04001E7D RID: 7805
	private static readonly TileZone[] _temp_array_zones;
}
