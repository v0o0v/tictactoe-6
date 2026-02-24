using System;
using TMPro;
using UnityEngine;

namespace Tictactoe {

    

    public class LoginPanelController : PanelController {

        [SerializeField] private TMP_InputField usernameInputField;
        [SerializeField] private TMP_InputField passwordInputField;

        private Action _onLoginButtonClicked;

        public void Show(Action onSignupButtonClick = null){
            this._onLoginButtonClicked = onSignupButtonClick;
            base.Show();
        }

        public void OnClickConfirmButton(){
            string username = usernameInputField.text;
            string password = passwordInputField.text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)){
                GameManager.Instance.OpenConfirmPanel("입력 값이 누락되었습니다.", () => { });
                return;
            }


            LoginData loginData = new(username, password);
            StartCoroutine(NetworkManager.Instance.Login(loginData
                , () => { GameManager.Instance.OpenConfirmPanel("로그인 완료", () => { Hide(); }); }
                , () => {
                    GameManager.Instance.OpenConfirmPanel("로그인 실패", () => {
                        usernameInputField.text = "";
                        passwordInputField.text = "";
                    });
                }
            ));
        }

        public void OnClickCancelButton(){
            Hide();
        }

    }

}