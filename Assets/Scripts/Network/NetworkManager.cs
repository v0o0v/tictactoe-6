using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

namespace Tictactoe {

    public class NetworkManager : Singleton<NetworkManager> {

        public IEnumerator Signup(SignupData signupData, Action onSuccess, Action onFailure){
            string jsonString = JsonUtility.ToJson(signupData);
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonString);

            using (UnityWebRequest www =
                   new UnityWebRequest(Constants.ServerURL + "/users/signup", UnityWebRequest.kHttpVerbPOST)){
                www.uploadHandler = new UploadHandlerRaw(bodyRaw);
                www.downloadHandler = new DownloadHandlerBuffer();
                www.SetRequestHeader("Content-Type", "application/json");

                yield return www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.ConnectionError ||
                    www.result == UnityWebRequest.Result.ProtocolError){
                    switch (www.responseCode){
                        case 400:
                            Debug.LogError("Bad Request: " + www.downloadHandler.text);
                            break;
                        case 409:
                            Debug.LogError("Conflict: " + www.downloadHandler.text);
                            break;
                        default:
                            Debug.LogError("Signup failed: " + www.error);
                            break;
                    }

                    onFailure?.Invoke();
                }
                else{
                    var result = www.downloadHandler.text;
                    Debug.Log("Signup successful: " + result);
                    onSuccess?.Invoke();
                }
            }
        }

        public IEnumerator Login(LoginData loginData, Action onSuccess, Action onFailure){
            string jsonString = JsonUtility.ToJson(loginData);
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonString);

            using (UnityWebRequest www =
                   new UnityWebRequest(Constants.ServerURL + "/users/signin", UnityWebRequest.kHttpVerbPOST)){
                www.uploadHandler = new UploadHandlerRaw(bodyRaw);
                www.downloadHandler = new DownloadHandlerBuffer();
                www.SetRequestHeader("Content-Type", "application/json");

                yield return www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.ConnectionError ||
                    www.result == UnityWebRequest.Result.ProtocolError){
                    onFailure?.Invoke();
                }
                else{
                    var cookie = www.GetResponseHeader("Set-Cookie");
                    if (!string.IsNullOrEmpty(cookie)){
                        string sid = cookie.Substring(0, cookie.IndexOf(';'));
                        PlayerPrefs.SetString("SID", sid);
                        PlayerPrefs.Save();
                    }

                    var result = www.downloadHandler.text;
                    var resultData = JsonUtility.FromJson<SigninData>(result);
                    Debug.Log("Login successful: " + result);
                    onSuccess?.Invoke();
                }
            }
        }

        public IEnumerator GetScore(Action<ScoreResult> onSuccess, Action onFailure){
            using (UnityWebRequest www = new UnityWebRequest(Constants.ServerURL + "/users/score", UnityWebRequest.kHttpVerbGET)){
                www.downloadHandler = new DownloadHandlerBuffer();
                string sid = PlayerPrefs.GetString("SID", null);
                if (!string.IsNullOrEmpty(sid)){
                    www.SetRequestHeader("Cookie", sid);
                }

                yield return www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.ConnectionError ||
                    www.result == UnityWebRequest.Result.ProtocolError){
                    onFailure?.Invoke();
                }
                else{
                    var resultData = JsonUtility.FromJson<ScoreResult>(www.downloadHandler.text);
                    onSuccess?.Invoke(resultData);
                }
            }
            yield return null;
        }
        
        public IEnumerator Signout(Action<SigninData> onSuccess, Action onFailure){
            using (UnityWebRequest www = new UnityWebRequest(Constants.ServerURL + "/users/signout", UnityWebRequest.kHttpVerbGET)){
                www.downloadHandler = new DownloadHandlerBuffer();
                string sid = PlayerPrefs.GetString("SID", null);
                if (!string.IsNullOrEmpty(sid)){
                    www.SetRequestHeader("Cookie", sid);
                }

                yield return www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.ConnectionError ||
                    www.result == UnityWebRequest.Result.ProtocolError){
                    onFailure?.Invoke();
                }
                else{
                    var resultData = JsonUtility.FromJson<SigninData>(www.downloadHandler.text);
                    onSuccess?.Invoke(resultData);
                }
            }
            yield return null;
        }

        protected override void OnSceneLoad(Scene scene, LoadSceneMode mode){
            // Do nothing on scene load
        }

    }

    public struct SigninData {

        public string message;

    }

    public struct ScoreResult {

        public int score;

    }

    public struct LoginData {

        public string username;
        public string password;

        public LoginData(string username, string password){
            this.username = username;
            this.password = password;
        }

    }

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

}