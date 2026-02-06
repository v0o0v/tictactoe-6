using UnityEngine;
using UnityEngine.SceneManagement;
using static Tictactoe.Constants;

namespace Tictactoe {

    public class GameManager : Singleton<GameManager> {

        [SerializeField] private GameObject settingsPanelPrefab;
        
        private Canvas _canvas;

        private GameType _gameType;

        protected override void OnSceneLoad(Scene scene, LoadSceneMode mode){
            _canvas = FindFirstObjectByType<Canvas>();
        }

        public void OpenSettingsPanel(){
            GameObject settingPanelObject = Instantiate(settingsPanelPrefab, _canvas.transform);
            settingPanelObject.GetComponent<SettingsPanelController>().Show();
        }

        public void ChangeToGameScene(GameType gameType){
            _gameType = gameType;
            SceneManager.LoadScene("Game");
        }
        
        public void ChangeToMainScene(){
            SceneManager.LoadScene("Main");
        }

    }

}