using Godot;
using System;

public class MainMenu : Control
{
    private Control mainMenu;
    private Button startNewBtn;
    private Button continueBtn;
    private Button optionsBtn;
    private Button quitBtn;
    private Control optionsMenu;
    private Button backBtn;



    //Ready Function
    public override void _Ready(){
        mainMenu = GetNode<Control>("Menu");

        startNewBtn = GetNode<Button>("Menu/StartNewBtn");
        startNewBtn.Connect("pressed", this, "startNewBtn_pressed");

        continueBtn = GetNode<Button>("Menu/ContinueBtn");
        continueBtn.Connect("pressed", this, "continueBtn_pressed");

        optionsBtn = GetNode<Button>("Menu/OptionsBtn");
        optionsBtn.Connect("pressed", this, "optionsBtn_pressed");

        quitBtn = GetNode<Button>("Menu/QuitBtn");
        quitBtn.Connect("pressed", this, "quitBtn_pressed");

        optionsMenu = GetNode<Control>("OptionsMenu");

        backBtn = GetNode<Button>("OptionsMenu/BackBtn");
        backBtn.Connect("pressed", this, "backBtn_pressed");
    }

    //Pressed Button Functions
    void startNewBtn_pressed(){
        GD.Print("StartNewPressed");
    }
     void continueBtn_pressed(){
        GD.Print("ContinuePressed");
    }
     void optionsBtn_pressed(){
        mainMenu.SetVisible(false);
        optionsMenu.SetVisible(true);
    }
     void quitBtn_pressed(){
        GetTree().Quit();
    }
    void backBtn_pressed(){
        optionsMenu.SetVisible(false);
        mainMenu.SetVisible(true);
    }
}
