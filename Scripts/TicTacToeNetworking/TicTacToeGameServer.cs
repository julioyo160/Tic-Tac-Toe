using System;
public class TicTacToeGameServer
{
    private readonly TicTacToeServer _server;
    private readonly TicTacToeGameEngine _engine;
    public TicTacToeGameServer(TicTacToeServer server)
    {
        _server = server;
        _server.PayloadReceived += Server_PayloadReceived;
        _server.ClientListFull += StartTheRound;
        _engine = new TicTacToeGameEngine();
    }
    private void StartTheRound(object sender, EventArgs e)
    {
        _engine.Reset();
        _server.StartGame();
    }
    private void MakeMove(int[] boardState)
    {
        var cellIndex = boardState[0];
        var state = _engine.MakeMove(cellIndex);
        if (state == TicTacToeGameState.Podium)
        {
            _server.ShowPodium(_engine.Winner, _engine.Cells);
        }
        else
        {
            _server.SetCurrentPlayer(_engine.CurrentPlayer, _engine.Cells);
        }
    }
    private void Server_PayloadReceived(object sender, PayloadEventArgs<GameMessage> e)
    {
        var client = (NetworkClient)sender;
        switch (_engine.State)
        {
            case TicTacToeGameState.Playing:
                if (_server.IsCurrentPlayer(client, _engine.CurrentPlayer))
                {
                    if (e.Payload.MessageType == MessageType.ClientMakeMove)
                    {
                        MakeMove(e.Payload.boardState);
                    }
                }
                break;
            case TicTacToeGameState.Podium:
                if (e.Payload.MessageType == MessageType.ClientPlayAgain)
                {
                    StartTheRound(this, EventArgs.Empty);
                }
                else if (e.Payload.MessageType == MessageType.ClientQuit)
                {
                    _server.QuitToTitle();
                }
                break;
        }
    }
}