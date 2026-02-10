using static Tictactoe.Constants;

namespace Tictactoe.States {

    public class AIState : BaseState {

        private PlayerType _playerType;

        public AIState(bool isFirstPlayer){
            _playerType = isFirstPlayer ? PlayerType.Player1 : PlayerType.Player2;
        }

        public override void OnEnter(GameLogic gameLogic){
            GameManager.Instance.SetGameTurn(_playerType);
        }

        public override void HandleMove(GameLogic gameLogic, int index){
            ProcessMove(gameLogic, index, _playerType);
        }
        public override void HandleNextTurn(GameLogic gameLogic){ }
        public override void OnExit(GameLogic gameLogic){ }

    }

}