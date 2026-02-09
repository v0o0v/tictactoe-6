using Tictactoe.States;
using static Tictactoe.Constants;

namespace Tictactoe {

    public class GameLogic {

        public BlockController blockController;

        private PlayerType[,] _board;

        public BaseState playerAState;
        public BaseState playerBState;

        private BaseState _currentState;

        public GameLogic(GameType gameType, BlockController blockController){
            this.blockController = blockController;
            _board = new PlayerType[BOARD_SIZE, BOARD_SIZE];
            switch (gameType){
                case GameType.SinglePlay:
                    playerAState = new PlayerState(true);
                    playerBState = new AIState();
                    SetState(playerAState);
                    break;
                case GameType.DualPlay:
                    playerAState = new PlayerState(true);
                    playerBState = new PlayerState(false);
                    SetState(playerAState);
                    break;
            }
        }

        public void SetState(BaseState newState){
            _currentState?.OnExit(this);
            _currentState = newState;
            _currentState?.OnEnter(this);
        }

        public bool PlaceMarker(int index, PlayerType playerType){
            int row = index / BOARD_SIZE;
            int col = index % BOARD_SIZE;

            if (_board[row, col] != PlayerType.None)
                return false;

            blockController.PlaceMarker(index, playerType);
            _board[row, col] = playerType;
            return true;
        }

        public void ChangeGameState(){
            SetState(_currentState == playerAState ? playerBState : playerAState);
        }

        public void CheckGameResult(){
            // Game over logic to be implemented
        }

        public bool CheckGameWin(PlayerType playerType, PlayerType[,] board){
            return false;
        }

    }

}