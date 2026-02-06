using UnityEngine;
using UnityEngine.SceneManagement;
using static Tictactoe.Constants;

namespace Tictactoe {

    public class GameManager : Singleton<GameManager> {

        [SerializeField] private GameObject settingsPanelPrefab;
        [SerializeField] private Canvas canvas;

        private GameType _gameType;

        protected override void OnSceneLoad(Scene scene, LoadSceneMode mode){ }

        public void OpenSettingsPanel(){
            GameObject settingPanelObject = Instantiate(settingsPanelPrefab, canvas.transform);
            settingPanelObject.GetComponent<SettingsPanelController>().Show();
        }

        public void ChangeToGameScece(GameType gameType){
            _gameType = gameType;
            SceneManager.LoadScene("Game");
        }
        
        public void ChangeToMainScece(){
            SceneManager.LoadScene("Main");
        }

    }

}