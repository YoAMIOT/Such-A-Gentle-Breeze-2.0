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
    private Control OptionsMenu;
    private HSlider RedSlider;
    private HSlider GreenSlider;
    private HSlider BlueSlider;
    private Label RedCount;
    private Label GreenCount;
    private Label BlueCount;
    private Button BackBtn;
    private SoundManager SoundManager;
    private DataManager DataManager;
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
        OptionsMenu = GetNode<Control>("OptionsMenu");
        RedSlider = GetNode<HSlider>("OptionsMenu/ColorPicker/RedSlider");
        RedCount = GetNode<Label>("OptionsMenu/ColorPicker/RedCount");
        GreenSlider = GetNode<HSlider>("OptionsMenu/ColorPicker/GreenSlider");
        GreenCount = GetNode<Label>("OptionsMenu/ColorPicker/GreenCount");
        BlueSlider = GetNode<HSlider>("OptionsMenu/ColorPicker/BlueSlider");
        BlueCount = GetNode<Label>("OptionsMenu/ColorPicker/BlueCount");
        BackBtn = GetNode<Button>("OptionsMenu/BackBtn");
        SoundManager = GetNode<SoundManager>("/root/SoundManager");
        DataManager = GetNode<DataManager>("/root/DataManager");
        

        //Connect signals
        ContinueBtn.Connect("pressed", this, "continuePressed");
        OptionsBtn.Connect("pressed", this, "optionsPressed");
        MainMenuBtn.Connect("pressed", this, "mainMenuPressed");
        YesBtn.Connect("pressed", this, "yesPressed");
        NoBtn.Connect("pressed", this, "noPressed");
        BackBtn.Connect("pressed", this, "backPressed");
        RedSlider.Connect("value_changed", this, "redValueChanged");
        GreenSlider.Connect("value_changed", this, "greenValueChanged");
        BlueSlider.Connect("value_changed", this, "blueValueChanged");

        getOptionsData();
    }



    private void getOptionsData(){
        //Get the ui color from the data manager 
        RedSlider.Value = DataManager.uiColor.r;
        GreenSlider.Value = DataManager.uiColor.g;
        BlueSlider.Value = DataManager.uiColor.b;
        //Multiply it by 255
        float redMultiplied = DataManager.uiColor.r * 255;
        float greenMultiplied = DataManager.uiColor.g * 255;
        float blueMultiplied = DataManager.uiColor.b * 255;
        //Cast it to int
        int redCasted = (int)redMultiplied;
        int greenCasted = (int)greenMultiplied;
        int blueCasted = (int)blueMultiplied;
        //Set the text of the options menu
        RedCount.Text = redCasted.ToString();
        GreenCount.Text = greenCasted.ToString();
        BlueCount.Text = blueCasted.ToString();
    }



    //Button pressed
    private void continuePressed(){
        SoundManager.playButtonSound();
        EmitSignal(nameof(pauseMenuSwitch));
    }
    private void optionsPressed(){
        SoundManager.playButtonSound();
        getOptionsData();
        MainPauseMenu.Visible = false;
        OptionsMenu.Visible = true;
    }
    private void mainMenuPressed(){
        SoundManager.playButtonSound();
        MainPauseMenu.Visible = false;
        WarnPopup.Visible = true;
    }
    private void yesPressed(){
        SoundManager.playButtonSound();
        GetTree().ChangeScene("res://scenes/GameScene.tscn");
    }
    private void noPressed(){
        SoundManager.playButtonSound();
        WarnPopup.Visible = false;
        MainPauseMenu.Visible = true;
    }
    private void backPressed(){
        SoundManager.playButtonSound();
        OptionsMenu.Visible = false;
        MainPauseMenu.Visible = true;
    }
    //Options functions
    private void redValueChanged(float value){
        DataManager.setRed(value);
        float multiplied = value * 255;
        int casted = (int)multiplied;
        RedCount.Text = casted.ToString();
    }
    private void greenValueChanged(float value){
        DataManager.setGreen(value);
        float multiplied = value * 255;
        int casted = (int)multiplied;
        GreenCount.Text = casted.ToString();
    }
    private void blueValueChanged(float value){
        DataManager.setBlue(value);
        float multiplied = value * 255;
        int casted = (int)multiplied;
        BlueCount.Text = casted.ToString();
    }
}
