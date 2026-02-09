using System;
using UnityEngine;
using static Tictactoe.Block;

namespace Tictactoe {

    public class BlockController : MonoBehaviour {

        [SerializeField] public Block[] blocks;

        public delegate void OnBlockClicked(int index);

        public OnBlockClicked onBlockClicked;

        public void InitBlocks(){
            for (int i = 0; i < blocks.Length; i++){
                blocks[i].InitMarker(i, blockIndex => { onBlockClicked?.Invoke(blockIndex); });
            }
        }

        public void PlaceMarker(int blockIndex, Constants.PlayerType playerType){
            switch (playerType){
                case Constants.PlayerType.Player1:
                    blocks[blockIndex].SetMarker(MarkerType.O);
                    break;
                case Constants.PlayerType.Player2:
                    blocks[blockIndex].SetMarker(MarkerType.X);
                    break;
            }
            
        }

    }

}