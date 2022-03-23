using Godot;
using System;

public class DTR : Control
{
    private AnimationPlayer animPlayer;



    //Ready function
    public override void _Ready(){
        animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        animPlayer.Play("BrokenScreen");
        animPlayer.Connect("animation_finished", this, "animDone");
    }



    //Function when animation is done
    private void animDone(string anim){
        GetTree().ChangeScene("res://scenes/GameScene.tscn");
    }
}
