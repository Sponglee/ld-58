using UnityEngine;
using Zenject;

public class CollectionController
{
    private CollectionView _view;
    private CollectionModel _model;

    public CollectionController()
    {
        
    }
    
    public void Initialize(CollectionView view, CollectionModel model)
    {
        _view = view;
        _model = model;
    }

    public Transform GetCollectionParent()
    {
        return _view.GetCollectionParent();
    }
}