using static Tictactoe.Constants;

namespace Tictactoe.States {

    public class PlayerState : BaseState {

        private PlayerType _playerType;
        private bool _isMultiplayer;
        private MultiplayManager _multiplayManager;
        private string _multiplayRoomId;

        public PlayerState(bool isFirstPlayer){
            _playerType = isFirstPlayer ? PlayerType.Player1 : PlayerType.Player2;
            _isMultiplayer = false;
        }

        public PlayerState(bool isFirstPlayer, MultiplayManager multiplayManager, string roomId){
            _playerType = isFirstPlayer ? PlayerType.Player1 : PlayerType.Player2;
            _isMultiplayer = true;
            _multiplayManager = multiplayManager;
            _multiplayRoomId = roomId;
        }

        public override void OnEnter(GameLogic gameLogic){
            gameLogic.blockController.onBlockClicked = blockIndex => { HandleMove(gameLogic, blockIndex); };
            GameManager.Instance.SetGameTurn(_playerType);
        }

        public override void HandleMove(GameLogic gameLogic, int index){
            ProcessMove(gameLogic, index, _playerType);

            if (_isMultiplayer){
                _multiplayManager.SendPlayerMove(_multiplayRoomId, index);
            }
        }

        public override void HandleNextTurn(GameLogic gameLogic){
            gameLogic.ChangeGameState();
        }

        public override void OnExit(GameLogic gameLogic){
            gameLogic.blockController.onBlockClicked = null;
        }

    }

}