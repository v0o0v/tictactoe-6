using UnityEngine;
using static Tictactoe.Constants;

namespace Tictactoe {

    public class MainPanelController : MonoBehaviour {

        [SerializeField] private GameObject signupPanelPrefab;

        private void Start(){
            var instantiate = Instantiate(signupPanelPrefab, transform);
                instantiate.GetComponent<SignupPanelController>().Show();
        }

        public void onClickSinglePlayButton(){
            GameManager.Instance.ChangeToGameScene(GameType.SinglePlay);
        }

        public void onClickDualPlayButton(){
            GameManager.Instance.ChangeToGameScene(GameType.DualPlay);
        }

        public void onClickSettingsButton(){
            GameManager.Instance.OpenSettingsPanel();
        }

    }

}