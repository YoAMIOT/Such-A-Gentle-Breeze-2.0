using Godot;
using System;

public class Game : Control
{
    //Nodes declarations
    private Control MainMenu;
    private Button StartNewBtn;
    private Button ContinueBtn;
    private Button OptionsBtn;
    private Button QuitBtn;
    private Control OptionsMenu;
    private HSlider RedSlider;
    private HSlider GreenSlider;
    private HSlider BlueSlider;
    private Label RedCount;
    private Label GreenCount;
    private Label BlueCount;
    private Button BackBtn;
    private DataManager DataManager;
    private SoundManager SoundManager;
    private Control PauseMenu;
    private Control MenuContainer;
    private Control TextScene;
    //Scene declaration
    private PackedScene TextGameplayScene;



    //Ready function
    public override void _Ready(){
        //Associating node declarations to their instances
        DataManager = GetNode<DataManager>("/root/DataManager");
        SoundManager = GetNode<SoundManager>("/root/SoundManager");
        MainMenu = GetNode<Control>("MenuContainer/MainMenu");
        StartNewBtn = GetNode<Button>("MenuContainer/MainMenu/StartNewBtn");
        ContinueBtn = GetNode<Button>("MenuContainer/MainMenu/ContinueBtn");
        OptionsBtn = GetNode<Button>("MenuContainer/MainMenu/OptionsBtn");
        QuitBtn = GetNode<Button>("MenuContainer/MainMenu/QuitBtn");
        OptionsMenu = GetNode<Control>("MenuContainer/OptionsMenu");
        BackBtn = GetNode<Button>("MenuContainer/OptionsMenu/BackBtn");
        MenuContainer = GetNode<Control>("MenuContainer");
        RedSlider = GetNode<HSlider>("MenuContainer/OptionsMenu/ColorPicker/RedSlider");
        RedCount = GetNode<Label>("MenuContainer/OptionsMenu/ColorPicker/RedCount");
        GreenSlider = GetNode<HSlider>("MenuContainer/OptionsMenu/ColorPicker/GreenSlider");
        GreenCount = GetNode<Label>("MenuContainer/OptionsMenu/ColorPicker/GreenCount");
        BlueSlider = GetNode<HSlider>("MenuContainer/OptionsMenu/ColorPicker/BlueSlider");
        BlueCount = GetNode<Label>("MenuContainer/OptionsMenu/ColorPicker/BlueCount");
        PauseMenu = GetNode<Control>("PauseMenu");

        //Kill the pop anim if needed
        if(DataManager.popAnimDone == true){
            GetNode<Control>("BootScreen").QueueFree();
            MainMenu.Visible = true;
        }

        //Loading the text gameplay scene
        TextGameplayScene = ResourceLoader.Load<PackedScene>("res://scenes/TextScene.tscn");

        //Connecting signals
        StartNewBtn.Connect("pressed", this, "startNewBtn_pressed");
        ContinueBtn.Connect("pressed", this, "continueBtn_pressed");
        OptionsBtn.Connect("pressed", this, "optionsBtn_pressed");
        QuitBtn.Connect("pressed", this, "quitBtn_pressed");
        BackBtn.Connect("pressed", this, "backBtn_pressed");
        RedSlider.Connect("value_changed", this, "redValueChanged");
        GreenSlider.Connect("value_changed", this, "greenValueChanged");
        BlueSlider.Connect("value_changed", this, "blueValueChanged");

        //Testing if the user already have a save file
        if(DataManager.userDataExists() == false){
            ContinueBtn.Visible = false;
            Vector2 optionsPos;
            Vector2 quitPos;
            optionsPos.x = 908;
            optionsPos.y = 716;
            OptionsBtn.SetPosition(optionsPos);
            quitPos.x = 835;
            quitPos.y = 793;
            QuitBtn.SetPosition(quitPos);
        }

        getOptionsData();
    }



    //Input function
    public override void _Input(InputEvent inputEvent){
        if(this.HasNode("MenuContainer") == true){
            if (inputEvent.IsActionPressed("ui_cancel") && OptionsMenu.Visible == true){
                SoundManager.playButtonSound();
                OptionsMenu.Visible = false;
                MainMenu.Visible = true;
            }
        }

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



    //Pressed button functions
    private void startNewBtn_pressed(){
        DataManager.currentScene = "";
        Control MainTextScene = (Control)TextGameplayScene.Instance();
        AddChild(MainTextScene);
        MenuContainer.QueueFree();
        SoundManager.playButtonSound();
        TextScene = GetNode<Control>("TextScene");
    }
    private void continueBtn_pressed(){
        DataManager.loadUserData();
        Control MainTextScene = (Control)TextGameplayScene.Instance();
        AddChild(MainTextScene);
        MenuContainer.QueueFree();
        SoundManager.playButtonSound();
        TextScene = GetNode<Control>("TextScene");
    }
    private void optionsBtn_pressed(){
        SoundManager.playButtonSound();
        getOptionsData();
        MainMenu.Visible = !MainMenu.Visible;
        OptionsMenu.Visible = !OptionsMenu.Visible;
    }
    private void quitBtn_pressed(){
        GetTree().Quit();
    }
    //Options menu funtions
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
    private void backBtn_pressed(){
        OptionsMenu.Visible= !OptionsMenu.Visible;
        MainMenu.Visible = !MainMenu.Visible;
        SoundManager.playButtonSound();
    }
}
