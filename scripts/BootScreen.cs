using Godot;
using System;

public class BootScreen : Control
{
	private AnimationPlayer animationPlayer;
	private DataManager DataManager;
	private string mainMenuPath = "MenuContainer/MainMenu";
	private string animationPlayerPath = "AnimationPlayer";

	//Ready function
	public override void _Ready()
	{
		DataManager = GetNode<DataManager>("/root/DataManager");
		animationPlayer = GetNode<AnimationPlayer>(animationPlayerPath);
		animationPlayer.Connect("animation_finished", this, "animationFinished");
	}



	void animationFinished(String anim_name){
		if (anim_name == "LogoPop"){
			GetParent<Control>().GetNode<Control>(mainMenuPath).Visible = true;
			DataManager.popAnimDone = true;
			this.QueueFree();
		}
	}
}
