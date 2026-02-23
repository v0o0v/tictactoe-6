using System;
using TMPro;
using UnityEngine;

namespace Tictactoe {

    public struct SignupData {

        public string username;
        public string password;
        public string nickname;

        public SignupData(string username, string password1, string nickname1){
            this.username = username;
            this.password = password1;
            this.nickname = nickname1;
        }

    }

    public class SignupPanelController : PanelController {

        [SerializeField] private TMP_InputField usernameInputField;
        [SerializeField] private TMP_InputField passwordInputField;
        [SerializeField] private TMP_InputField confirmPasswordInputField;
        [SerializeField] private TMP_InputField nicknameInputField;

        private Action _onSignupButtonClicked;

        public void Show(Action onSignupButtonClick = null){
            this._onSignupButtonClicked = onSignupButtonClick;
            base.Show();
        }

        public void OnClickConfirmButton(){
            string username = usernameInputField.text;
            string password = passwordInputField.text;
            string confirmPassword = confirmPasswordInputField.text;
            string nickname = nicknameInputField.text;

            if (string.IsNullOrEmpty(username)
                || string.IsNullOrEmpty(password)
                || string.IsNullOrEmpty(confirmPassword)
                || string.IsNullOrEmpty(nickname)){
                GameManager.Instance.OpenConfirmPanel("입력 값이 누락되었습니다.", () => { });
                return;
            }

            if (password.Equals(confirmPassword)){
                SignupData signupData = new(username, password, nickname);
                StartCoroutine(NetworkManager.Instance.Signup(signupData
                    , () => { GameManager.Instance.OpenConfirmPanel("회원 가입 완료", () => { Hide(); }); }
                    , () => {
                        GameManager.Instance.OpenConfirmPanel("회원 가입 실패", () => {
                            usernameInputField.text = "";
                            passwordInputField.text = "";
                            confirmPasswordInputField.text = "";
                            nicknameInputField.text = "";
                        });
                    }
                ));
            }
            else{ }
        }

        public void OnClickCancelButton(){
            Hide();
        }

    }

}