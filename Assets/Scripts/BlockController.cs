using UnityEngine;

namespace Tictactoe {

    public class BlockController : MonoBehaviour {

        [SerializeField] private GameObject blockPrefab;

        public void InitBlocks(){
            for (int i = 0; i < 0; i++){
                GameObject blockObject = Instantiate(blockPrefab, transform);
                Block block = blockObject.GetComponent<Block>();
                block.InitMarker(i);
            }
        }

    }

}