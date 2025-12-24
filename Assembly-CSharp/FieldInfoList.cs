using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000537 RID: 1335
public class FieldInfoList : MonoBehaviour
{
	// Token: 0x06002BAC RID: 11180 RVA: 0x00159555 File Offset: 0x00157755
	public void init<T>() where T : class
	{
		this.init<T>(null);
	}

	// Token: 0x06002BAD RID: 11181 RVA: 0x00159560 File Offset: 0x00157760
	public void init<T>(ListPool<string> pFieldsToLoad) where T : class
	{
		this.checkInitPool();
		this.field_infos.Clear();
		this.fields_collection_data.Clear();
		FieldInfo[] fields = typeof(T).GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
		Array.Reverse<FieldInfo>(fields);
		bool tUseFilter = pFieldsToLoad != null && pFieldsToLoad.Count > 0;
		int i = 0;
		foreach (FieldInfo tField in fields)
		{
			if (!tUseFilter || pFieldsToLoad.Contains(tField.Name))
			{
				this.field_infos.Add(tField);
				i++;
			}
		}
		if (this.search_input_field != null)
		{
			this.search_input_field.onValueChanged.AddListener(new UnityAction<string>(this.setDataSearched));
		}
	}

	// Token: 0x06002BAE RID: 11182 RVA: 0x00159613 File Offset: 0x00157813
	public void checkInitPool()
	{
		if (this._pool_fields == null)
		{
			this._pool_fields = new ObjectPoolGenericMono<KeyValueField>(this.field_prefab, this.fields_transform);
			return;
		}
		this.clear();
	}

	// Token: 0x06002BAF RID: 11183 RVA: 0x0015963C File Offset: 0x0015783C
	public void setData(object pReference)
	{
		foreach (FieldInfo tField in this.field_infos)
		{
			FieldInfoListItem tItem = this.getFieldData(tField, pReference);
			this.fields_collection_data.Add(tItem.field_name, tItem);
			this.addRow(tItem.field_name, tItem.field_value);
		}
	}

	// Token: 0x06002BB0 RID: 11184 RVA: 0x001596B8 File Offset: 0x001578B8
	public FieldInfoListItem getFieldData(FieldInfo pField, object pReference)
	{
		object tValue = pField.GetValue(pReference);
		Type tType = pField.FieldType;
		Dictionary<string, string> tCollectionContent = null;
		string tValueString;
		if (tValue != null)
		{
			if (tValue is bool)
			{
				bool tBool = (bool)tValue;
				tValueString = Toolbox.coloredText(string.Format("{0}", tBool), tBool ? "#43FF43" : "#FB2C21", false);
			}
			else
			{
				string tStr = tValue as string;
				if (tStr == null)
				{
					if (tValue is int)
					{
						int tInt = (int)tValue;
						tValueString = Toolbox.coloredText(string.Format("{0}", tInt), FieldInfoList.color_white, false);
					}
					else if (tValue is float)
					{
						float tFloat = (float)tValue;
						tValueString = Toolbox.coloredText(tFloat.ToText() + "f", FieldInfoList.color_white, false);
					}
					else if (tValue is Vector2)
					{
						Vector2 tVector = (Vector2)tValue;
						string tVValueX = Toolbox.coloredText(tVector.x.ToText() + "f", FieldInfoList.color_white, false);
						string tVValueY = Toolbox.coloredText(tVector.y.ToText() + "f", FieldInfoList.color_white, false);
						tValueString = Toolbox.coloredText(string.Concat(new string[]
						{
							"Vector2(",
							tVValueX,
							", ",
							tVValueY,
							")"
						}), FieldInfoList.color_collection, false);
					}
					else if (tValue is Vector2Int)
					{
						Vector2Int tVectorInt = (Vector2Int)tValue;
						string tVIntValueX = Toolbox.coloredText(tVectorInt.x.ToText(), FieldInfoList.color_white, false);
						string tVIntValueY = Toolbox.coloredText(tVectorInt.y.ToText(), FieldInfoList.color_white, false);
						tValueString = Toolbox.coloredText(string.Concat(new string[]
						{
							"Vector2Int(",
							tVIntValueX,
							", ",
							tVIntValueY,
							")"
						}), FieldInfoList.color_collection, false);
					}
					else
					{
						Enum tEnum = tValue as Enum;
						if (tEnum == null)
						{
							Array tArray = tValue as Array;
							if (tArray == null)
							{
								IList tList = tValue as IList;
								if (tList == null)
								{
									IDictionary tDict = tValue as IDictionary;
									if (tDict == null)
									{
										if (tType.IsGenericType && typeof(HashSet<>) == tType.GetGenericTypeDefinition())
										{
											tCollectionContent = this.enumerableToRows(tValue as IEnumerable);
											string tSValue = Toolbox.coloredText(tType.GetGenericArguments()[0].Name, FieldInfoList.color_type, false);
											string tSCount = Toolbox.coloredText(tType.GetProperty("Count").GetValue(tValue).ToString(), FieldInfoList.color_white, false);
											tValueString = Toolbox.coloredText(string.Concat(new string[]
											{
												"HashSet<",
												tSValue,
												">[",
												tSCount,
												"]"
											}), FieldInfoList.color_collection, false);
										}
										else
										{
											tValueString = Toolbox.coloredText(tValue.GetType().Name, FieldInfoList.color_type, false);
										}
									}
									else
									{
										tCollectionContent = this.dictionaryToRows(tDict);
										Type[] genericArguments = tType.GetGenericArguments();
										string tDKey = Toolbox.coloredText(genericArguments[0].Name, FieldInfoList.color_type, false);
										string tDValue = Toolbox.coloredText(genericArguments[1].Name, FieldInfoList.color_type, false);
										string tDCount = Toolbox.coloredText(tDict.Count.ToString(), FieldInfoList.color_white, false);
										tValueString = Toolbox.coloredText(string.Concat(new string[]
										{
											"Dictionary<",
											tDKey,
											", ",
											tDValue,
											">[",
											tDCount,
											"]"
										}), FieldInfoList.color_collection, false);
									}
								}
								else
								{
									tCollectionContent = this.enumerableToRowsCompacted(tList);
									string tLValue = Toolbox.coloredText(tType.GetGenericArguments()[0].Name, FieldInfoList.color_type, false);
									string tLCount = Toolbox.coloredText(tList.Count.ToString(), FieldInfoList.color_white, false);
									tValueString = Toolbox.coloredText(string.Concat(new string[]
									{
										"List<",
										tLValue,
										">[",
										tLCount,
										"]"
									}), FieldInfoList.color_collection, false);
								}
							}
							else
							{
								tCollectionContent = this.enumerableToRowsCompacted(tArray);
								string tAValue = Toolbox.coloredText(tType.GetElementType().Name, FieldInfoList.color_type, false);
								string tACount = Toolbox.coloredText(tArray.Length.ToString(), FieldInfoList.color_white, false);
								tValueString = Toolbox.coloredText(string.Concat(new string[]
								{
									"Array<",
									tAValue,
									">[",
									tACount,
									"]"
								}), FieldInfoList.color_collection, false);
							}
						}
						else
						{
							tValueString = Toolbox.coloredText(string.Format("{0}.{1}", tType.Name, tEnum), FieldInfoList.color_enum, false);
						}
					}
				}
				else
				{
					string tQuoteSymbol = Toolbox.coloredText("\"", FieldInfoList.color_null, false);
					tValueString = Toolbox.coloredText(tQuoteSymbol + tStr + tQuoteSymbol, FieldInfoList.color_string, false);
				}
			}
		}
		else
		{
			tValueString = Toolbox.coloredText("—", FieldInfoList.color_null, false);
		}
		return new FieldInfoListItem(pField.Name, tValueString, tCollectionContent);
	}

	// Token: 0x06002BB1 RID: 11185 RVA: 0x00159BD0 File Offset: 0x00157DD0
	public KeyValueField addRow(string pName, string pValue)
	{
		KeyValueField tNewRow = this._pool_fields.getNext();
		tNewRow.name_text.text = pName;
		tNewRow.value.text = pValue;
		FieldInfoListItem tItem;
		if (this.fields_collection_data.TryGetValue(pName, out tItem))
		{
			Dictionary<string, string> tCollectionContent = tItem.collection_data;
			if (tCollectionContent == null || tCollectionContent.Count == 0)
			{
				tNewRow.value.GetComponent<TipButton>().enabled = false;
			}
			else
			{
				tNewRow.value.GetComponent<TipButton>().enabled = true;
				tNewRow.on_hover_value = delegate()
				{
					FieldInfoList.selected_field_data = tCollectionContent;
				};
				tNewRow.on_hover_value_out = new UnityAction(Tooltip.hideTooltip);
			}
		}
		return tNewRow;
	}

	// Token: 0x06002BB2 RID: 11186 RVA: 0x00159C84 File Offset: 0x00157E84
	internal void setDataSearched(string pValue)
	{
		this.clear();
		pValue = pValue.ToLower();
		if (string.IsNullOrEmpty(pValue))
		{
			int i = 0;
			foreach (FieldInfoListItem tItem in this.fields_collection_data.Values)
			{
				KeyValueField tElement = this.addRow(tItem.field_name, tItem.field_value);
				this.setOddEvenColor(tElement, i);
				i++;
			}
			return;
		}
		int j = 0;
		foreach (FieldInfoListItem tItem2 in this.fields_collection_data.Values)
		{
			if (tItem2.field_name.ToLower().Contains(pValue))
			{
				KeyValueField tElement2 = this.addRow(tItem2.field_name, tItem2.field_value);
				this.setOddEvenColor(tElement2, j);
				j++;
			}
		}
	}

	// Token: 0x06002BB3 RID: 11187 RVA: 0x00159D8C File Offset: 0x00157F8C
	private void setOddEvenColor(KeyValueField pComponent, int pIndex)
	{
		if (pIndex % 2 == 0)
		{
			pComponent.setEvenColor();
			return;
		}
		pComponent.setOddColor();
	}

	// Token: 0x06002BB4 RID: 11188 RVA: 0x00159DA0 File Offset: 0x00157FA0
	private Dictionary<string, string> enumerableToRowsCompacted(IEnumerable pEnumerable)
	{
		Dictionary<string, int> tCompacted = new Dictionary<string, int>();
		int i = 0;
		foreach (object obj in pEnumerable)
		{
			string tKey = obj.ToString();
			if (tCompacted.ContainsKey(tKey))
			{
				Dictionary<string, int> dictionary = tCompacted;
				string key = tKey;
				dictionary[key]++;
			}
			else
			{
				tCompacted.Add(tKey, 1);
				i++;
			}
		}
		string tColorYellow = Toolbox.colorToHex(Toolbox.color_yellow, true);
		Dictionary<string, string> tResult = new Dictionary<string, string>();
		int j = 0;
		foreach (KeyValuePair<string, int> tPair in tCompacted)
		{
			string tValue = tPair.Value.ToString();
			tResult.Add(tPair.Key + "    ", Toolbox.coloredText("x      " + tValue, tColorYellow, false));
			j++;
		}
		return tResult;
	}

	// Token: 0x06002BB5 RID: 11189 RVA: 0x00159EC8 File Offset: 0x001580C8
	private Dictionary<string, string> enumerableToRows(IEnumerable pEnumerable)
	{
		Dictionary<string, string> tResult = new Dictionary<string, string>();
		int i = 0;
		foreach (object tObject in pEnumerable)
		{
			tResult.Add(string.Format("[{0}]     ", i), tObject.ToString());
			i++;
		}
		return tResult;
	}

	// Token: 0x06002BB6 RID: 11190 RVA: 0x00159F3C File Offset: 0x0015813C
	private Dictionary<string, string> dictionaryToRows(IDictionary pDictionary)
	{
		Dictionary<string, string> tResult = new Dictionary<string, string>();
		foreach (object tKey in pDictionary.Keys)
		{
			tResult.Add(string.Format("[\"{0}\"]", tKey), pDictionary[tKey].ToString());
		}
		return tResult;
	}

	// Token: 0x06002BB7 RID: 11191 RVA: 0x00159FB0 File Offset: 0x001581B0
	public void clear()
	{
		this._pool_fields.clear(true);
	}

	// Token: 0x0400218C RID: 8588
	public static string color_null = "#9F9F9F";

	// Token: 0x0400218D RID: 8589
	public static string color_white = Toolbox.colorToHex(Toolbox.color_white, true);

	// Token: 0x0400218E RID: 8590
	public static string color_string = "#F3961F";

	// Token: 0x0400218F RID: 8591
	public static string color_enum = Toolbox.colorToHex(Toolbox.color_plague, true);

	// Token: 0x04002190 RID: 8592
	public static string color_type = Toolbox.colorToHex(Toolbox.color_yellow, true);

	// Token: 0x04002191 RID: 8593
	public static string color_collection = FieldInfoList.color_null;

	// Token: 0x04002192 RID: 8594
	public static Dictionary<string, string> selected_field_data;

	// Token: 0x04002193 RID: 8595
	public KeyValueField field_prefab;

	// Token: 0x04002194 RID: 8596
	public InputField search_input_field;

	// Token: 0x04002195 RID: 8597
	public Transform fields_transform;

	// Token: 0x04002196 RID: 8598
	private ObjectPoolGenericMono<KeyValueField> _pool_fields;

	// Token: 0x04002197 RID: 8599
	internal List<FieldInfo> field_infos = new List<FieldInfo>();

	// Token: 0x04002198 RID: 8600
	internal Dictionary<string, FieldInfoListItem> fields_collection_data = new Dictionary<string, FieldInfoListItem>();
}
