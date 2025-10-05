using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class CollectionView : UIViewBase
{
    [SerializeField] Transform _collectionHolder;

    public Transform GetCollectionParent()
    {
        return _collectionHolder;
    }
}