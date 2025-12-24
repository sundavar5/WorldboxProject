using System;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x020007E2 RID: 2018
// (Invoke) Token: 0x06003F8F RID: 16271
[return: TupleElementNames(new string[]
{
	"icon",
	"color"
})]
public delegate ValueTuple<Sprite, Color> IconGetter();
