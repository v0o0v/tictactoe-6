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

        public void PlaceMarker(int blockIndex, MarkerType markerType){
            if (blockIndex < 0 || blockIndex >= blocks.Length){
                Debug.LogError("Invalid block index: " + blockIndex);
                return;
            }

            blocks[blockIndex].SetMarker(markerType);
        }

    }

}