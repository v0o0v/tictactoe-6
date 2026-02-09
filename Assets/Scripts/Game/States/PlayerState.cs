namespace Tictactoe.States {

    public class PlayerState : BaseState {
        
        private Constants.PlayerType _playerType;
        
        public PlayerState(bool isFirstPlayer){
            _playerType = isFirstPlayer ? Constants.PlayerType.Player1 : Constants.PlayerType.Player2;
        }

        public override void OnEnter(GameLogic gameLogic){
            gameLogic.blockController.onBlockClicked = blockIndex => {
                HandleMove(gameLogic, blockIndex);
            };
        }

        public override void HandleMove(GameLogic gameLogic, int index) {
            ProcessMove(gameLogic, index, _playerType);
        }

        public override void HandleNextTurn(GameLogic gameLogic){
            gameLogic.ChangeGameState();
        }

        public override void OnExit(GameLogic gameLogic){ }

    }

}