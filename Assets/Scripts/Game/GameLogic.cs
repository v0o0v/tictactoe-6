using Tictactoe.States;
using static Tictactoe.Constants;

namespace Tictactoe {

    public class GameLogic {

        public BlockController blockController;

        private PlayerType[,] _board;

        public BaseState playerAState;
        public BaseState playerBState;

        private BaseState _currentState;

        public enum GameResult { None, Win, Lose, Draw }

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

        public GameResult CheckGameResult(){
            if (CheckGameWin(PlayerType.Player1, _board)){
                return GameResult.Win;
            }

            if (CheckGameWin(PlayerType.Player2, _board)){
                return GameResult.Lose;
            }

            if (CheckGameDraw(_board)){
                return GameResult.Draw;
            }

            return GameResult.None;
        }

        public bool CheckGameWin(PlayerType playerType, PlayerType[,] board){
            for (int row = 0; row < BOARD_SIZE; row++){
                if (board[row, 0] == playerType &&
                    board[row, 1] == playerType &&
                    board[row, 2] == playerType)
                    return true;
            }

            for (int col = 0; col < BOARD_SIZE; col++){
                if (board[0, col] == playerType &&
                    board[1, col] == playerType &&
                    board[2, col] == playerType)
                    return true;
            }

            if (board[0, 0] == playerType &&
                board[1, 1] == playerType &&
                board[2, 2] == playerType)
                return true;
            if (board[0, 2] == playerType &&
                board[1, 1] == playerType &&
                board[2, 0] == playerType)
                return true;

            return false;
        }

        public bool CheckGameDraw(PlayerType[,] board){
            for (int row = 0; row < BOARD_SIZE; row++){
                for (int col = 0; col < BOARD_SIZE; col++){
                    if (board[row, col] == PlayerType.None)
                        return false;
                }
            }

            return true;
        }

        public void EndGame(GameResult gameResult){
            string resultStr = gameResult switch{
                GameResult.Win => "Player1 승리!",
                GameResult.Lose => "Player2 승리!",
                GameResult.Draw => "무승부!",
                _ => ""
            };
            GameManager.Instance.OpenConfirmPanel(resultStr
                , () => { GameManager.Instance.ChangeToMainScene(); }
            );
        }

    }

}