using Godot;
using System;

public class DTR : Control
{
    private AnimationPlayer AnimPlayer;



    //Ready function
    public override void _Ready(){
        AnimPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        AnimPlayer.Play("BrokenScreen");
        AnimPlayer.Connect("animation_finished", this, "animDone");
    }



    //Function when animation is done
    private void animDone(string anim){
        GetTree().ChangeScene("res://scenes/GameScene.tscn");
    }
}
