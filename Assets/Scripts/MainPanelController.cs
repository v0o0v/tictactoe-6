using UnityEngine;
using static Tictactoe.Constants;

namespace Tictactoe {

    public class MainPanelController : MonoBehaviour {

        public void onClickSinglePlayButton(){
            GameManager.Instance.ChangeToGameScece(GameType.SinglePlay);
        }

        public void onClickDualPlayButton(){
            GameManager.Instance.ChangeToGameScece(GameType.DualPlay);
        }

        public void onClickSettingsButton(){
            GameManager.Instance.OpenSettingsPanel();
        }

    }

}