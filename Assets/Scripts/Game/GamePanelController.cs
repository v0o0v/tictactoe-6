using UnityEngine;

namespace Tictactoe {

    public class GamePanelController : MonoBehaviour {

        public void OnClickBackButton(){
            GameManager.Instance.ChangeToMainScene();
        }
        
        public void OnClickSettingsButton(){
            GameManager.Instance.OpenSettingsPanel();
        }

    }

}