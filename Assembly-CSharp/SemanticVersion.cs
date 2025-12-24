using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

// Token: 0x02000484 RID: 1156
public sealed class SemanticVersion : IComparable, IComparable<SemanticVersion>, IEquatable<SemanticVersion>
{
	// Token: 0x0600279B RID: 10139 RVA: 0x00140955 File Offset: 0x0013EB55
	public SemanticVersion(string version) : this(SemanticVersion.Parse(version))
	{
		this._originalString = version;
	}

	// Token: 0x0600279C RID: 10140 RVA: 0x0014096A File Offset: 0x0013EB6A
	public SemanticVersion(int major, int minor, int build, int revision) : this(new Version(major, minor, build, revision))
	{
	}

	// Token: 0x0600279D RID: 10141 RVA: 0x0014097C File Offset: 0x0013EB7C
	public SemanticVersion(int major, int minor, int build, string specialVersion) : this(new Version(major, minor, build), specialVersion)
	{
	}

	// Token: 0x0600279E RID: 10142 RVA: 0x0014098E File Offset: 0x0013EB8E
	public SemanticVersion(int major, int minor, int build, string specialVersion, string metadata) : this(new Version(major, minor, build), specialVersion, metadata)
	{
	}

	// Token: 0x0600279F RID: 10143 RVA: 0x001409A2 File Offset: 0x0013EBA2
	public SemanticVersion(Version version) : this(version, string.Empty)
	{
	}

	// Token: 0x060027A0 RID: 10144 RVA: 0x001409B0 File Offset: 0x0013EBB0
	public SemanticVersion(Version version, string specialVersion) : this(version, specialVersion, null, null)
	{
	}

	// Token: 0x060027A1 RID: 10145 RVA: 0x001409BC File Offset: 0x0013EBBC
	public SemanticVersion(Version version, string specialVersion, string metadata) : this(version, specialVersion, metadata, null)
	{
	}

	// Token: 0x060027A2 RID: 10146 RVA: 0x001409C8 File Offset: 0x0013EBC8
	private SemanticVersion(Version version, string specialVersion, string metadata, string originalString)
	{
		if (version == null)
		{
			throw new ArgumentNullException("version");
		}
		this.Version = SemanticVersion.NormalizeVersionValue(version);
		this.SpecialVersion = (specialVersion ?? string.Empty);
		this.Metadata = metadata;
		this._originalString = (string.IsNullOrEmpty(originalString) ? (version.ToString() + ((!string.IsNullOrEmpty(specialVersion)) ? ("-" + specialVersion) : null) + ((!string.IsNullOrEmpty(metadata)) ? ("+" + metadata) : null)) : originalString);
	}

	// Token: 0x060027A3 RID: 10147 RVA: 0x00140A5C File Offset: 0x0013EC5C
	internal SemanticVersion(SemanticVersion semVer)
	{
		this._originalString = semVer.ToOriginalString();
		this.Version = semVer.Version;
		this.SpecialVersion = semVer.SpecialVersion;
		this.Metadata = semVer.Metadata;
	}

	// Token: 0x1700021D RID: 541
	// (get) Token: 0x060027A4 RID: 10148 RVA: 0x00140A94 File Offset: 0x0013EC94
	// (set) Token: 0x060027A5 RID: 10149 RVA: 0x00140A9C File Offset: 0x0013EC9C
	public Version Version { get; private set; }

	// Token: 0x1700021E RID: 542
	// (get) Token: 0x060027A6 RID: 10150 RVA: 0x00140AA5 File Offset: 0x0013ECA5
	// (set) Token: 0x060027A7 RID: 10151 RVA: 0x00140AAD File Offset: 0x0013ECAD
	public string SpecialVersion { get; private set; }

	// Token: 0x1700021F RID: 543
	// (get) Token: 0x060027A8 RID: 10152 RVA: 0x00140AB6 File Offset: 0x0013ECB6
	// (set) Token: 0x060027A9 RID: 10153 RVA: 0x00140ABE File Offset: 0x0013ECBE
	public string Metadata { get; private set; }

	// Token: 0x060027AA RID: 10154 RVA: 0x00140AC8 File Offset: 0x0013ECC8
	public string[] GetOriginalVersionComponents()
	{
		if (!string.IsNullOrEmpty(this._originalString))
		{
			int labelIndex = this._originalString.IndexOfAny(new char[]
			{
				'-',
				'+'
			});
			string original;
			if (labelIndex != -1)
			{
				original = this._originalString.Substring(0, labelIndex);
			}
			else
			{
				original = this._originalString;
			}
			return SemanticVersion.SplitAndPadVersionString(original);
		}
		return SemanticVersion.SplitAndPadVersionString(this.Version.ToString());
	}

	// Token: 0x060027AB RID: 10155 RVA: 0x00140B30 File Offset: 0x0013ED30
	private static string[] SplitAndPadVersionString(string version)
	{
		string[] a = version.Split('.', StringSplitOptions.None);
		if (a.Length == 4)
		{
			return a;
		}
		string[] b = new string[]
		{
			"0",
			"0",
			"0",
			"0"
		};
		Array.Copy(a, 0, b, 0, a.Length);
		return b;
	}

	// Token: 0x060027AC RID: 10156 RVA: 0x00140B84 File Offset: 0x0013ED84
	public static SemanticVersion Parse(string version)
	{
		if (string.IsNullOrEmpty(version))
		{
			throw new ArgumentException("Value cannot be null or an empty string", "version");
		}
		SemanticVersion semVer;
		if (!SemanticVersion.TryParse(version, out semVer))
		{
			throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "'{0}' is not a valid version string.", version), "version");
		}
		return semVer;
	}

	// Token: 0x060027AD RID: 10157 RVA: 0x00140BCF File Offset: 0x0013EDCF
	public static bool TryParse(string version, out SemanticVersion value)
	{
		return SemanticVersion.TryParseInternal(version, SemanticVersion._semanticVersionRegex, out value);
	}

	// Token: 0x060027AE RID: 10158 RVA: 0x00140BDD File Offset: 0x0013EDDD
	public static bool TryParseStrict(string version, out SemanticVersion value)
	{
		return SemanticVersion.TryParseInternal(version, SemanticVersion._strictSemanticVersionRegex, out value);
	}

	// Token: 0x060027AF RID: 10159 RVA: 0x00140BEC File Offset: 0x0013EDEC
	private static bool TryParseInternal(string version, Regex regex, out SemanticVersion semVer)
	{
		semVer = null;
		if (string.IsNullOrEmpty(version))
		{
			return false;
		}
		Match match = regex.Match(version.Trim());
		Version versionValue;
		if (!match.Success || !Version.TryParse(match.Groups["Version"].Value, out versionValue))
		{
			return false;
		}
		semVer = new SemanticVersion(SemanticVersion.NormalizeVersionValue(versionValue), SemanticVersion.RemoveLeadingChar(match.Groups["Release"].Value), SemanticVersion.RemoveLeadingChar(match.Groups["Metadata"].Value), version.Replace(" ", ""));
		return true;
	}

	// Token: 0x060027B0 RID: 10160 RVA: 0x00140C8D File Offset: 0x0013EE8D
	private static string RemoveLeadingChar(string s)
	{
		if (s != null && s.Length > 0)
		{
			return s.Substring(1, s.Length - 1);
		}
		return s;
	}

	// Token: 0x060027B1 RID: 10161 RVA: 0x00140CAC File Offset: 0x0013EEAC
	public static SemanticVersion ParseOptionalVersion(string version)
	{
		SemanticVersion semVer;
		SemanticVersion.TryParse(version, out semVer);
		return semVer;
	}

	// Token: 0x060027B2 RID: 10162 RVA: 0x00140CC3 File Offset: 0x0013EEC3
	private static Version NormalizeVersionValue(Version version)
	{
		return new Version(version.Major, version.Minor, Math.Max(version.Build, 0), Math.Max(version.Revision, 0));
	}

	// Token: 0x060027B3 RID: 10163 RVA: 0x00140CF0 File Offset: 0x0013EEF0
	public int CompareTo(object obj)
	{
		if (obj == null)
		{
			return 1;
		}
		SemanticVersion other = obj as SemanticVersion;
		if (other == null)
		{
			throw new ArgumentException("Type to compare must be an instance of SemanticVersion.", "obj");
		}
		return this.CompareTo(other);
	}

	// Token: 0x060027B4 RID: 10164 RVA: 0x00140D2C File Offset: 0x0013EF2C
	public int CompareTo(SemanticVersion other)
	{
		if (other == null)
		{
			return 1;
		}
		int result = this.Version.CompareTo(other.Version);
		if (result != 0)
		{
			return result;
		}
		bool empty = string.IsNullOrEmpty(this.SpecialVersion);
		bool otherEmpty = string.IsNullOrEmpty(other.SpecialVersion);
		if (empty && otherEmpty)
		{
			return 0;
		}
		if (empty)
		{
			return 1;
		}
		if (otherEmpty)
		{
			return -1;
		}
		IEnumerable<string> version = this.SpecialVersion.Split('.', StringSplitOptions.None);
		string[] otherReleaseLabels = other.SpecialVersion.Split('.', StringSplitOptions.None);
		return SemanticVersion.CompareReleaseLabels(version, otherReleaseLabels);
	}

	// Token: 0x060027B5 RID: 10165 RVA: 0x00140DA1 File Offset: 0x0013EFA1
	public static bool operator ==(SemanticVersion version1, SemanticVersion version2)
	{
		if (version1 == null)
		{
			return version2 == null;
		}
		return version1.Equals(version2);
	}

	// Token: 0x060027B6 RID: 10166 RVA: 0x00140DB2 File Offset: 0x0013EFB2
	public static bool operator !=(SemanticVersion version1, SemanticVersion version2)
	{
		return !(version1 == version2);
	}

	// Token: 0x060027B7 RID: 10167 RVA: 0x00140DBE File Offset: 0x0013EFBE
	public static bool operator <(SemanticVersion version1, SemanticVersion version2)
	{
		if (version1 == null)
		{
			throw new ArgumentNullException("version1");
		}
		return version1.CompareTo(version2) < 0;
	}

	// Token: 0x060027B8 RID: 10168 RVA: 0x00140DDE File Offset: 0x0013EFDE
	public static bool operator <=(SemanticVersion version1, SemanticVersion version2)
	{
		return version1 == version2 || version1 < version2;
	}

	// Token: 0x060027B9 RID: 10169 RVA: 0x00140DF2 File Offset: 0x0013EFF2
	public static bool operator >(SemanticVersion version1, SemanticVersion version2)
	{
		if (version1 == null)
		{
			throw new ArgumentNullException("version1");
		}
		return version2 < version1;
	}

	// Token: 0x060027BA RID: 10170 RVA: 0x00140E0F File Offset: 0x0013F00F
	public static bool operator >=(SemanticVersion version1, SemanticVersion version2)
	{
		return version1 == version2 || version1 > version2;
	}

	// Token: 0x060027BB RID: 10171 RVA: 0x00140E24 File Offset: 0x0013F024
	public override string ToString()
	{
		if (this.IsSemVer2())
		{
			return this.ToNormalizedString();
		}
		int plusIndex = this._originalString.IndexOf('+');
		if (plusIndex > -1)
		{
			return this._originalString.Substring(0, plusIndex);
		}
		return this._originalString;
	}

	// Token: 0x060027BC RID: 10172 RVA: 0x00140E68 File Offset: 0x0013F068
	public string ToNormalizedString()
	{
		if (this._normalizedVersionString == null)
		{
			StringBuilder builder = new StringBuilder();
			builder.Append(this.Version.Major).Append('.').Append(this.Version.Minor).Append('.').Append(Math.Max(0, this.Version.Build));
			if (this.Version.Revision > 0)
			{
				builder.Append('.').Append(this.Version.Revision);
			}
			if (!string.IsNullOrEmpty(this.SpecialVersion))
			{
				builder.Append('-').Append(this.SpecialVersion);
			}
			this._normalizedVersionString = builder.ToString();
		}
		return this._normalizedVersionString;
	}

	// Token: 0x060027BD RID: 10173 RVA: 0x00140F28 File Offset: 0x0013F128
	public string ToFullString()
	{
		string s = this.ToNormalizedString();
		if (!string.IsNullOrEmpty(this.Metadata))
		{
			s = string.Format(CultureInfo.InvariantCulture, "{0}+{1}", s, this.Metadata);
		}
		return s;
	}

	// Token: 0x060027BE RID: 10174 RVA: 0x00140F61 File Offset: 0x0013F161
	public string ToOriginalString()
	{
		return this._originalString;
	}

	// Token: 0x060027BF RID: 10175 RVA: 0x00140F69 File Offset: 0x0013F169
	public bool IsSemVer2()
	{
		return !string.IsNullOrEmpty(this.Metadata) || (!string.IsNullOrEmpty(this.SpecialVersion) && this.SpecialVersion.Contains("."));
	}

	// Token: 0x060027C0 RID: 10176 RVA: 0x00140F99 File Offset: 0x0013F199
	public bool Equals(SemanticVersion other)
	{
		return other != null && this.Version.Equals(other.Version) && this.SpecialVersion.Equals(other.SpecialVersion, StringComparison.OrdinalIgnoreCase);
	}

	// Token: 0x060027C1 RID: 10177 RVA: 0x00140FC8 File Offset: 0x0013F1C8
	public override bool Equals(object obj)
	{
		SemanticVersion semVer = obj as SemanticVersion;
		return semVer != null && this.Equals(semVer);
	}

	// Token: 0x060027C2 RID: 10178 RVA: 0x00140FE8 File Offset: 0x0013F1E8
	public override int GetHashCode()
	{
		int hashCode = this.Version.GetHashCode();
		if (this.SpecialVersion != null)
		{
			hashCode = hashCode * 4567 + this.SpecialVersion.GetHashCode();
		}
		return hashCode;
	}

	// Token: 0x060027C3 RID: 10179 RVA: 0x00141020 File Offset: 0x0013F220
	private static int CompareReleaseLabels(IEnumerable<string> version1, IEnumerable<string> version2)
	{
		int result = 0;
		int result2;
		using (IEnumerator<string> a = version1.GetEnumerator())
		{
			using (IEnumerator<string> b = version2.GetEnumerator())
			{
				bool aExists = a.MoveNext();
				bool bExists = b.MoveNext();
				while (aExists || bExists)
				{
					if (!aExists && bExists)
					{
						return -1;
					}
					if (aExists && !bExists)
					{
						return 1;
					}
					result = SemanticVersion.CompareRelease(a.Current, b.Current);
					if (result != 0)
					{
						return result;
					}
					aExists = a.MoveNext();
					bExists = b.MoveNext();
				}
				result2 = result;
			}
		}
		return result2;
	}

	// Token: 0x060027C4 RID: 10180 RVA: 0x001410D0 File Offset: 0x0013F2D0
	private static int CompareRelease(string version1, string version2)
	{
		int version1Num = 0;
		int version2Num = 0;
		bool v1IsNumeric = int.TryParse(version1, out version1Num);
		bool v2IsNumeric = int.TryParse(version2, out version2Num);
		int result;
		if (v1IsNumeric && v2IsNumeric)
		{
			result = version1Num.CompareTo(version2Num);
		}
		else if (v1IsNumeric || v2IsNumeric)
		{
			if (v1IsNumeric)
			{
				result = -1;
			}
			else
			{
				result = 1;
			}
		}
		else
		{
			result = StringComparer.OrdinalIgnoreCase.Compare(version1, version2);
		}
		return result;
	}

	// Token: 0x04001DE3 RID: 7651
	private const RegexOptions _flags = RegexOptions.ExplicitCapture | RegexOptions.Compiled;

	// Token: 0x04001DE4 RID: 7652
	private static readonly Regex _semanticVersionRegex = new Regex("^(?<Version>\\d+(\\s*\\.\\s*\\d+){0,3})(?<Release>-([0]\\b|[0]$|[0][0-9]*[A-Za-z-]+|[1-9A-Za-z-][0-9A-Za-z-]*)+(\\.([0]\\b|[0]$|[0][0-9]*[A-Za-z-]+|[1-9A-Za-z-][0-9A-Za-z-]*)+)*)?(?<Metadata>\\+[0-9A-Za-z-]+(\\.[0-9A-Za-z-]+)*)?$", RegexOptions.ExplicitCapture | RegexOptions.Compiled);

	// Token: 0x04001DE5 RID: 7653
	private static readonly Regex _strictSemanticVersionRegex = new Regex("^(?<Version>([0-9]|[1-9][0-9]*)(\\.([0-9]|[1-9][0-9]*)){2})(?<Release>-([0]\\b|[0]$|[0][0-9]*[A-Za-z-]+|[1-9A-Za-z-][0-9A-Za-z-]*)+(\\.([0]\\b|[0]$|[0][0-9]*[A-Za-z-]+|[1-9A-Za-z-][0-9A-Za-z-]*)+)*)?(?<Metadata>\\+[0-9A-Za-z-]+(\\.[0-9A-Za-z-]+)*)?$", RegexOptions.ExplicitCapture | RegexOptions.Compiled);

	// Token: 0x04001DE6 RID: 7654
	private readonly string _originalString;

	// Token: 0x04001DE7 RID: 7655
	private string _normalizedVersionString;
}
