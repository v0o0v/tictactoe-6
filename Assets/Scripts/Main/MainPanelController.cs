using UnityEngine;
using static Tictactoe.Constants;

namespace Tictactoe {

    public class MainPanelController : MonoBehaviour {

        [SerializeField] private GameObject signupPanelPrefab;
        [SerializeField] private GameObject loginPanelPrefab;

        private void Start(){
            string sid = PlayerPrefs.GetString("SID", null);
            if (string.IsNullOrEmpty(sid)){
                var instantiate = Instantiate(loginPanelPrefab, transform);
                instantiate.GetComponent<LoginPanelController>().Show();
            }
        }

        public void OnClickGetScore(){
            StartCoroutine(NetworkManager.Instance.GetScore(
                onSuccess: (ScoreResult) => {
                    GameManager.Instance.OpenConfirmPanel("현재 점수는 " + ScoreResult.score + "점입니다.", () => { });
                },
                onFailure: () => { Debug.Log("점수 가져오기 실패"); }
            ));
        }

        public void OnClickSignout(){
            StartCoroutine(NetworkManager.Instance.Signout(
                onSuccess: (signoutData) => { GameManager.Instance.OpenConfirmPanel("" + signoutData.message, () => { }); },
                onFailure: () => { Debug.Log("로그아웃 실패"); }
            ));
        }

        public void OnClickSignupButton(){
            var instantiate = Instantiate(signupPanelPrefab, transform);
            instantiate.GetComponent<SignupPanelController>().Show();
        }

        public void OnClickLoginButton(){
            var instantiate = Instantiate(loginPanelPrefab, transform);
            instantiate.GetComponent<LoginPanelController>().Show();
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