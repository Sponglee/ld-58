using System;
using DefaultNamespace;
using DG.Tweening;
using UnityEngine;

public class HandController : IDisposable
    {
        private HandView _handView;
        private HandModel _handModel;

        private Transform _t;
        
        public HandController(HandModel model, HandView view)
        {
            _handView = view;
            _handModel = model;
        }

        public Transform Transform => _t ??=_handView.gameObject.transform;

        public void InitializeHand(InventoryItemData data, float rotationDuration, Ease ease)
        {
            _handView.SetData(data);
            _handView.SetSettings(rotationDuration, ease);
            _handView.ToggleHand(true);
        }
        
        public void DeinitializeHand()
        {
            _handView.SetData(null);
            _handView.ToggleHand(false);
        }
        
        public void SetHandPosition(Vector3 position)
        {
            _handView?.SetPosition(position);
        }

        public void RotateHand(bool isClockwise)
        {
            _handView.RotateAround(isClockwise);
        }
        
        private void ActivateHand()
        {
            // _view.InitializeHand(data, duration, ease);
        }
        
        public void DeactivateHand()
        {
            DeinitializeHand();
        }


        public void Dispose()
        {
            GameObject.Destroy(_handView.gameObject);
            _handModel.Dispose();
        }

        public float GetRotationAngle()
        {
          return _handView.gameObject.transform.eulerAngles.z;
        }
    }