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
                    var result = www.downloadHandler.text;
                    Debug.Log("Login successful: " + result);
                    onSuccess?.Invoke();
                }
            }
        }

        protected override void OnSceneLoad(Scene scene, LoadSceneMode mode){
            // Do nothing on scene load
        }

    }

}