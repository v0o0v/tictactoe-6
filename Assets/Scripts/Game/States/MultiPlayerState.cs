using static Tictactoe.Constants;

namespace Tictactoe.States {

    public class MultiPlayerState : BaseState {

        private PlayerType _playerType;
        private MultiplayManager _multiplayManager;

        public MultiPlayerState(bool isFirstPlayer, MultiplayManager multiplayManager){
            _playerType = isFirstPlayer ? PlayerType.Player1 : PlayerType.Player2;
            _multiplayManager = multiplayManager;
        }

        public override void OnEnter(GameLogic gameLogic){
            _multiplayManager.OnOpponentMove = (moveData) => {
                if (moveData.position >= 0 && moveData.position < BOARD_SIZE * BOARD_SIZE){
                    UnityThread.executeInUpdate(() => {
                        HandleMove(gameLogic, moveData.position);
                        GameManager.Instance.SetGameTurn(_playerType);
                    });
                }
                else{
                    // Invalid move data received, handle accordingly (e.g., log an error)
                }
            };
        }

        public override void HandleMove(GameLogic gameLogic, int index){
            ProcessMove(gameLogic, index, _playerType);
        }

        public override void HandleNextTurn(GameLogic gameLogic){
            gameLogic.ChangeGameState();
        }

        public override void OnExit(GameLogic gameLogic){
            _multiplayManager.OnOpponentMove = null;
        }

    }

}