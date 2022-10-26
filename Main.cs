using Godot;
using static Godot.MultiplayerAPI;

public partial class Main : Node2D {
  int Port = 19591;
  int Repeat = 10;
  int Times = 0;

  bool CauseCrash = true;

  public override void _Ready() {
    var peer = new ENetMultiplayerPeer();
    peer.CreateServer(Port);
    Multiplayer.MultiplayerPeer = peer;

    var btn = GetNode<Button>("ColorRect/Control/Button");

    btn.Pressed += CauseCrash ? NotifyFourParams : NotifyNoParams;

    btn.Text = $"RPC call x{Repeat}";
  }

  public override void _Process(double delta) {
  }

  [RPC(RPCMode.AnyPeer)]
  void NotifyFourParams() {
    GD.Print(++Times);
    for (var i = 0; i < Repeat; i++) {
      Rpc(nameof(FourParams), "one", "two", "three", GetGlobalMousePosition());
    }
  }

  [RPC(RPCMode.AnyPeer)]
  void NotifyNoParams() {
    GD.Print(++Times);
    for (var i = 0; i < Repeat; i++) {
      Rpc(nameof(NoParams));
    }
  }


  [RPC(RPCMode.Authority)]
  void FourParams(string one, string two, string three, Vector2 loc) {
    // This is never called, just exists for completeness
  }

  [RPC(RPCMode.Authority)]
  void NoParams() {
    // This is never called, just exists for completeness
  }
}
