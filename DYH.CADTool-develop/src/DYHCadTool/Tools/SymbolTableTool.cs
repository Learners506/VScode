namespace DYH.Tools;
public static class SymbolTableTool
{
    public static HashSet<string> GetLockedLayerNames(this SymbolTable<LayerTable, LayerTableRecord> lt)
    {
        var tr = DBTrans.Top;
        var nameSet= new HashSet<string>();
        foreach (var oid in lt)
        {
            if(tr.GetObject(oid) is LayerTableRecord { IsLocked: true } ltr)
                nameSet.Add(ltr.Name);
        }
        return nameSet;
    }
}
public static class BlockTableTool
{
    /// <summary>
    /// 同步块表属性
    /// </summary>
    /// <param name="target"></param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="Exception"></exception>
    public static void SynchronizeAttributes(this BlockTableRecord target)
    {
        if (target == null)
            throw new ArgumentNullException(nameof(target));
        Transaction topTransaction = target.Database.TransactionManager.TopTransaction;
        List<AttributeDefinition> attDefs = topTransaction != null ? target.GetAttributes(topTransaction) : throw new Exception();
        foreach (object blockReferenceId in target.GetBlockReferenceIds(true, false))
        {
            ObjectId objectId = blockReferenceId != null ? (ObjectId)blockReferenceId : new ObjectId();
            BlockReference br = (BlockReference)topTransaction.GetObject(objectId, (OpenMode)1, false, true);
            br.ResetAttributes(attDefs, topTransaction);
            BlockReference blockReference = br;
            Point3d position = br.Position;
            Matrix3d matrix3d = Matrix3d.Displacement(position.GetVectorTo(br.Position));
            blockReference.TransformBy(matrix3d);
        }
        if (!target.IsDynamicBlock)
            return;
        target.UpdateAnonymousBlocks();
        foreach (object anonymousBlockId in target.GetAnonymousBlockIds())
        {
            ObjectId objectId1 = anonymousBlockId != null ? (ObjectId)anonymousBlockId : new ObjectId();
            BlockTableRecord target1 = (BlockTableRecord)topTransaction.GetObject(objectId1, OpenMode.ForRead);
            List<AttributeDefinition> attributes = target1.GetAttributes(topTransaction);
            foreach (object blockReferenceId in target1.GetBlockReferenceIds(true, false))
            {
                ObjectId objectId2 = blockReferenceId != null ? (ObjectId)blockReferenceId : new ObjectId();
                BlockReference br = (BlockReference)topTransaction.GetObject(objectId2, OpenMode.ForWrite, false, true);
                br.ResetAttributes(attributes, topTransaction);
                BlockReference blockReference = br;
                Point3d position = br.Position;
                Matrix3d matrix3d = Matrix3d.Displacement(position.GetVectorTo(br.Position));
                blockReference.TransformBy(matrix3d);
            }
        }
    }
    // 属性定义RxClass
    private static readonly RXClass attDefClass = RXObject.GetClass(typeof(AttributeDefinition));
    private static List<AttributeDefinition> GetAttributes(this BlockTableRecord target,Transaction tr)
    {
        List<AttributeDefinition> attributes = new ();
        foreach (ObjectId objectId in target)
        {
            if (objectId.ObjectClass == attDefClass)
            {
                AttributeDefinition attributeDefinition = (AttributeDefinition)tr.GetObject(objectId, OpenMode.ForRead);
                attributes.Add(attributeDefinition);
            }
        }
        return attributes;
    }
    private static void ResetAttributes(this BlockReference br, List<AttributeDefinition> attDefs,Transaction tr)
    {
        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        foreach (object attribute in br.AttributeCollection)
        {
            ObjectId objectId = attribute != null ? (ObjectId)attribute : new ObjectId();
            if (!objectId.IsErased)
            {
                AttributeReference attributeReference = (AttributeReference)tr.GetObject(objectId, (OpenMode)1, false, true);
                dictionary.Add(attributeReference.Tag, attributeReference.IsMTextAttribute ? attributeReference.MTextAttribute.Contents : attributeReference.TextString);
                attributeReference.Erase();
            }
        }
        foreach (AttributeDefinition attDef in attDefs)
        {
            AttributeReference attributeReference = new AttributeReference();
            attributeReference.SetAttributeFromBlock(attDef, br.BlockTransform);
            if (attDef.Constant)
                attributeReference.TextString = attDef.IsMTextAttributeDefinition ? attDef.MTextAttributeDefinition.Contents : attDef.TextString;
            else if (dictionary != null && dictionary.ContainsKey(attDef.Tag))
                attributeReference.TextString = dictionary[attDef.Tag.ToUpper()];
            br.AttributeCollection.AppendAttribute(attributeReference);
            tr.AddNewlyCreatedDBObject(attributeReference, true);
        }
    }
}
public static class ViewTableTool
{
    public static Point2d[] GetCornerPoints(this ViewTableRecord viewTableRecord)
    {
        var minPoint = viewTableRecord.CenterPoint - new Vector2d(viewTableRecord.Width * 0.5, viewTableRecord.Height * 0.5);
        return new[] 
        {
            minPoint,
            minPoint+new Vector2d(viewTableRecord.Width,0),
            minPoint+ new Vector2d(viewTableRecord.Width,viewTableRecord.Height),
            minPoint+new Vector2d(0,viewTableRecord.Height)
        };
    }
}
