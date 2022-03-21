using Godot;
using System;

public class Game : Control
{
    //Nodes declarations
    private Control mainMenu;
    private Button startNewBtn;
    private Button continueBtn;
    private Button optionsBtn;
    private Button quitBtn;
    private Control optionsMenu;
    private HSlider redSlider;
    private HSlider greenSlider;
    private HSlider blueSlider;
    private Label redCount;
    private Label greenCount;
    private Label blueCount;
    private Button backBtn;
    private DataManager DataManager;
    private Control menuContainer;
    //Scene declaration
    private PackedScene TextGameplayScene;



    //Ready function
    public override void _Ready(){
        //Associating node declarations to their instances
        DataManager = GetNode<DataManager>("/root/DataManager");
        mainMenu = GetNode<Control>("MenuContainer/MainMenu");
        startNewBtn = GetNode<Button>("MenuContainer/MainMenu/StartNewBtn");
        continueBtn = GetNode<Button>("MenuContainer/MainMenu/ContinueBtn");
        optionsBtn = GetNode<Button>("MenuContainer/MainMenu/OptionsBtn");
        quitBtn = GetNode<Button>("MenuContainer/MainMenu/QuitBtn");
        optionsMenu = GetNode<Control>("MenuContainer/OptionsMenu");
        backBtn = GetNode<Button>("MenuContainer/OptionsMenu/BackBtn");
        menuContainer = GetNode<Control>("MenuContainer");
        redSlider = GetNode<HSlider>("MenuContainer/OptionsMenu/ColorPicker/RedSlider");
        redCount = GetNode<Label>("MenuContainer/OptionsMenu/ColorPicker/RedCount");
        greenSlider = GetNode<HSlider>("MenuContainer/OptionsMenu/ColorPicker/GreenSlider");
        greenCount = GetNode<Label>("MenuContainer/OptionsMenu/ColorPicker/GreenCount");
        blueSlider = GetNode<HSlider>("MenuContainer/OptionsMenu/ColorPicker/BlueSlider");
        blueCount = GetNode<Label>("MenuContainer/OptionsMenu/ColorPicker/BlueCount");

        //Loading the text gameplay scene
        TextGameplayScene = ResourceLoader.Load<PackedScene>("res://scenes/TextScene.tscn");

        //Connecting signals
        startNewBtn.Connect("pressed", this, "startNewBtn_pressed");
        continueBtn.Connect("pressed", this, "continueBtn_pressed");
        optionsBtn.Connect("pressed", this, "optionsBtn_pressed");
        quitBtn.Connect("pressed", this, "quitBtn_pressed");
        backBtn.Connect("pressed", this, "backBtn_pressed");
        redSlider.Connect("value_changed", this, "redValueChanged");
        greenSlider.Connect("value_changed", this, "greenValueChanged");
        blueSlider.Connect("value_changed", this, "blueValueChanged");

        //Testing if the user already have a save file
        if(DataManager.userDataExists() == false){
            continueBtn.Visible = false;
            Vector2 optionsPos;
            Vector2 quitPos;
            optionsPos.x = 850.16F;
            optionsPos.y = 714.72F;
            optionsBtn.SetPosition(optionsPos);
            quitPos.x = 825.44F;
            quitPos.y = 792.72F;
            quitBtn.SetPosition(quitPos);
        }

        //Get the ui color from the data manager 
        redSlider.Value = DataManager.uiColor.r;
        greenSlider.Value = DataManager.uiColor.g;
        blueSlider.Value = DataManager.uiColor.b;
        //Multiply it by 255
        float redMultiplied = DataManager.uiColor.r * 255;
        float greenMultiplied = DataManager.uiColor.g * 255;
        float blueMultiplied = DataManager.uiColor.b * 255;
        //Cast it to int
        int redCasted = (int)redMultiplied;
        int greenCasted = (int)greenMultiplied;
        int blueCasted = (int)blueMultiplied;
        //Set the text of the options menu
        redCount.Text = redCasted.ToString();
        greenCount.Text = greenCasted.ToString();
        blueCount.Text = blueCasted.ToString();
    }



    //Pressed button functions
    void startNewBtn_pressed(){
        DataManager.currentScene = "";
        Control MainTextScene = (Control)TextGameplayScene.Instance();
        AddChild(MainTextScene);
        menuContainer.QueueFree();
    }
    void continueBtn_pressed(){
        DataManager.loadUserData();
        Control MainTextScene = (Control)TextGameplayScene.Instance();
        AddChild(MainTextScene);
        menuContainer.QueueFree();
    }
    void optionsBtn_pressed(){
        mainMenu.Visible = !mainMenu.Visible;
        optionsMenu.Visible= !optionsMenu.Visible;
    }
    void quitBtn_pressed(){
        GetTree().Quit();
    }
    //Options menu funtions
    void redValueChanged(float value){
        DataManager.setRed(value);
        float multiplied = value * 255;
        int casted = (int)multiplied;
        GetNode<Label>("MenuContainer/OptionsMenu/ColorPicker/RedCount").Text = casted.ToString();
    }
    void greenValueChanged(float value){
        DataManager.setGreen(value);
        float multiplied = value * 255;
        int casted = (int)multiplied;
        GetNode<Label>("MenuContainer/OptionsMenu/ColorPicker/GreenCount").Text = casted.ToString();
    }
    void blueValueChanged(float value){
        DataManager.setBlue(value);
        float multiplied = value * 255;
        int casted = (int)multiplied;
        GetNode<Label>("MenuContainer/OptionsMenu/ColorPicker/BlueCount").Text = casted.ToString();
    }
    void backBtn_pressed(){
        optionsMenu.Visible= !optionsMenu.Visible;
        mainMenu.Visible = !mainMenu.Visible;
    }
}
