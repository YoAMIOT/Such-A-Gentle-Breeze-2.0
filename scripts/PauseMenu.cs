using Godot;
using System;

public class PauseMenu : Control
{
    private Button ContinueBtn;
    private Button OptionsBtn;
    private Button MainMenuBtn;
    private Control TextScene;
    private Control WarnPopup;
    private Control MainPauseMenu;
    private Button YesBtn;
    private Button NoBtn;
    //Signal
    [Signal]
    public delegate void pauseMenuSwitch();



    //Ready function
    public override void _Ready()
    {
        //Linking nodes
        ContinueBtn = GetNode<Button>("Main/ContinueBtn");
        OptionsBtn = GetNode<Button>("Main/OptionsBtn");
        MainMenuBtn = GetNode<Button>("Main/MainMenuBtn");
        WarnPopup = GetNode<Control>("WarnPopup");
        MainPauseMenu = GetNode<Control>("Main");
        YesBtn = GetNode<Button>("WarnPopup/YesBtn");
        NoBtn = GetNode<Button>("WarnPopup/NoBtn");

        //Connect signals
        ContinueBtn.Connect("pressed", this, "continuePressed");
        OptionsBtn.Connect("pressed", this, "optionsPressed");
        MainMenuBtn.Connect("pressed", this, "mainMenuPressed");
        YesBtn.Connect("pressed", this, "yesPressed");
        NoBtn.Connect("pressed", this, "noPressed");
    }



    //Button pressed
    private void continuePressed(){
        EmitSignal(nameof(pauseMenuSwitch));
    }
    private void optionsPressed(){
        
    }
    private void mainMenuPressed(){
        MainPauseMenu.Visible = false;
        WarnPopup.Visible = true;
    }
    private void yesPressed(){
        GetTree().ChangeScene("res://scenes/GameScene.tscn");
    }
    private void noPressed(){
        WarnPopup.Visible = false;
        MainPauseMenu.Visible = true;
    }
}
