using System;
using Newtonsoft.Json;
using SocketIOClient;
using SocketIOClient.Transport;
using Tictactoe;

public class RoomData {

    [JsonProperty("roomId")] public string roomId{ get; set; }

}

public class UserData {

    [JsonProperty("usedId")] public string userId{ get; set; }

}

public class MoveData {

    [JsonProperty("position")] public int position{ get; set; }

}

public enum MultiplayManageState { CreateRoom, JoinRoom, StartGame, ExitRoom, EndGame }

public class MultiplayManager : IDisposable {

    private SocketIOUnity _socket;
    private event Action<MultiplayManageState, string> _onMultiplayStateChanged;
    public Action<MoveData> OnOpponentMove;

    public MultiplayManager(Action<MultiplayManageState, string> onMultiplayStateChanged){
        _onMultiplayStateChanged = onMultiplayStateChanged;

        var uri = new Uri(Constants.SocketURL);
        _socket = new SocketIOUnity(uri, new SocketIOOptions{
            Transport = TransportProtocol.WebSocket
        });

        _socket.On("createRoom", CreateRoom);
        _socket.On("joinRoom", JoinRoom);
        _socket.On("startGame", StartGame);
        _socket.On("exitRoom", ExitRoom);
        _socket.On("endGame", EndGame);
        _socket.On("doOpponent", DoOpponent);

        _socket.Connect();
    }

    private void CreateRoom(SocketIOResponse response){
        var data = response.GetValue<RoomData>();
        _onMultiplayStateChanged?.Invoke(MultiplayManageState.CreateRoom, data.roomId);
    }

    private void JoinRoom(SocketIOResponse response){
        var data = response.GetValue<RoomData>();
        _onMultiplayStateChanged?.Invoke(MultiplayManageState.JoinRoom, data.roomId);
    }

    private void StartGame(SocketIOResponse response){
        var data = response.GetValue<RoomData>();
        _onMultiplayStateChanged?.Invoke(MultiplayManageState.StartGame, data.roomId);
    }

    private void ExitRoom(SocketIOResponse response){
        _onMultiplayStateChanged?.Invoke(MultiplayManageState.ExitRoom, null);
    }

    private void EndGame(SocketIOResponse response){
        _onMultiplayStateChanged?.Invoke(MultiplayManageState.EndGame, null);
    }

    private void DoOpponent(SocketIOResponse response){
        var data = response.GetValue<MoveData>();
        OnOpponentMove?.Invoke(data);
    }

    public void SendPlayerMove(string roomId, int position){
        _socket.Emit("doPlayer", new{ roomId, position });
    }

    public void LeaveRoom(string roomId){
        _socket.Emit("leaveRoom", new{ roomId });
    }

    public void Dispose(){
        if (_socket != null){
            _socket.Disconnect();
            _socket.Dispose();
        }
    }

}