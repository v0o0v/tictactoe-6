using UnityEngine;

namespace Tictactoe {

    public class GamePanelController : MonoBehaviour {

        public void OnClickBackButton(){
            GameManager.Instance.ChangeToMainScece();
        }
        
        public void OnClickSettingsButton(){
            GameManager.Instance.OpenSettingsPanel();
        }

    }

}