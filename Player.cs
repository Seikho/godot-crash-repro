using Godot;
using System;

public partial class Player : CharacterBody2D {
  Vector2 Bounds = new Vector2(1920, 1080);
  public const float Speed = 300.0f;

  public override void _Ready() {
    var camera = GetNode<Camera2D>("Camera2D");
    camera.LimitLeft = 0;
    camera.LimitTop = 0;
    camera.LimitRight = (int)Bounds.x;
    camera.LimitBottom = (int)Bounds.y;
  }

  public override void _PhysicsProcess(double delta) {
    Vector2 velocity = Velocity;

    // Get the input direction and handle the movement/deceleration.
    // As good practice, you should replace UI actions with custom gameplay actions.
    var direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
    Velocity = direction.Normalized() * Speed;
    MoveAndSlide();

    Position = new Vector2(
      x: Mathf.Clamp(Position.x, 0, Bounds.x),
      y: Mathf.Clamp(Position.y, 0, Bounds.y)
    );
  }

  public override void _Process(double delta) {

  }
}
