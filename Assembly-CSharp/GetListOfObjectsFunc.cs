using System;
using System.Collections.Generic;

// Token: 0x020007CF RID: 1999
// (Invoke) Token: 0x06003F0C RID: 16140
public delegate IEnumerable<TMetaObject> GetListOfObjectsFunc<TListElement, TMetaObject, TData, TComponent>(TComponent pComponent) where TListElement : WindowListElementBase<TMetaObject, TData> where TMetaObject : CoreSystemObject<TData> where TData : BaseSystemData where TComponent : ComponentListBase<TListElement, TMetaObject, TData, TComponent>;
