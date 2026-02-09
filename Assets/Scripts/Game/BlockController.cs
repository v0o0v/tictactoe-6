using UnityEngine;
using static Tictactoe.Block;
using static Tictactoe.Constants;

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

        public void PlaceMarker(int blockIndex, PlayerType playerType){
            blocks[blockIndex].SetMarker(playerType == PlayerType.Player1 ? MarkerType.O : MarkerType.X);
        }

    }

}