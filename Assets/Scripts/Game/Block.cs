using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Tictactoe {

    public class Block : MonoBehaviour {

        [SerializeField] private Sprite oSprite;
        [SerializeField] private Sprite xSprite;
        [SerializeField] private SpriteRenderer markerSpriteRenderer;

        public enum MarkerType { None, O, X }

        private int _blockIndex;

        public delegate void OnBlockClicked(int index);

        private OnBlockClicked _onBlockClicked;

        public void InitMarker(int blockIndex, OnBlockClicked onBlockClicked){
            _blockIndex = blockIndex;
            SetMarker(MarkerType.None);
            _onBlockClicked = onBlockClicked;
        }

        public void SetMarker(MarkerType markerType){
            switch (markerType){
                case MarkerType.O:
                    markerSpriteRenderer.sprite = oSprite;
                    break;
                case MarkerType.X:
                    markerSpriteRenderer.sprite = xSprite;
                    break;
                case MarkerType.None:
                    markerSpriteRenderer.sprite = null;
                    break;
            }
        }

        private void OnMouseUpAsButton(){
            if (EventSystem.current.IsPointerOverGameObject()){
                return;
            }
            _onBlockClicked?.Invoke(_blockIndex);
        }

    }

}