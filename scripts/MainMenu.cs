using Godot;
using System;

public class MainMenu : Control
{
    private Button startNewBtn;
    private Button continueBtn;
    private Button optionsBtn;
    private Button quitBtn;



    //Ready Function
    public override void _Ready(){
        startNewBtn = GetNode<Button>("StartNewBtn");
        startNewBtn.Connect("mouse_entered", this, "startNewBtn_mouseEntered");
        startNewBtn.Connect("mouse_exited", this, "startNewBtn_mouseExited");

        continueBtn = GetNode<Button>("ContinueBtn");
        continueBtn.Connect("mouse_entered", this, "continueBtn_mouseEntered");
        continueBtn.Connect("mouse_exited", this, "continueBtn_mouseExited");

        optionsBtn = GetNode<Button>("OptionsBtn");
        optionsBtn.Connect("mouse_entered", this, "optionsBtn_mouseEntered");
        optionsBtn.Connect("mouse_exited", this, "optionsBtn_mouseExited");

        quitBtn = GetNode<Button>("QuitBtn");
        quitBtn.Connect("mouse_entered", this, "quitBtn_mouseEntered");
        quitBtn.Connect("mouse_exited", this, "quitBtn_mouseExited");
    }



    //Signals Functions
    void startNewBtn_mouseEntered(){
        GD.Print("entered");
    }
    void startNewBtn_mouseExited(){
        GD.Print("exited");
    }

    void continueBtn_mouseEntered(){
        GD.Print("entered");
    }

     void continueBtn_mouseExited(){
        GD.Print("exited");
    }

    void optionsBtn_mouseEntered(){
        GD.Print("entered");
    }

    void optionsBtn_mouseExited(){
        GD.Print("exited");
    }

    void quitBtn_mouseEntered(){
        GD.Print("entered");
    }
    void quitBtn_mouseExited(){
        GD.Print("exited");
    }
}
