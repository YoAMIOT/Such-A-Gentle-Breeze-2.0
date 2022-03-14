using Godot;
using System;

public class BootScreen : Control
{
    private AnimationPlayer animationPlayer;

    //Ready function
    public override void _Ready()
    {
        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        animationPlayer.Connect("animation_finished", this, "animationFinished");
    }

    void animationFinished(String anim_name){
        if (anim_name == "LogoPop"){
            GetParent<Control>().GetNode<Control>("Menu").SetVisible(true);
            this.QueueFree();
        }
    }
}
