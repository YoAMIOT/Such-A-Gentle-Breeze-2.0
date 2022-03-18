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
    
    //Node Paths declaration
    private string dataManagerPath = "/root/DataManager";
    private string menuContainerPath = "MenuContainer";
    private string mainMenuPath = "MenuContainer/MainMenu";
    private string optionsMenuPath = "MenuContainer/OptionsMenu";
    private string startNewBtnPath = "/StartNewBtn";
    private string continueBtnPath = "/ContinueBtn";
    private string optionsBtnPath = "/OptionsBtn";
    private string quitBtnPath = "/QuitBtn";
    private string backBtnPath = "/BackBtn";
    private string colorPickerPath = "/ColorPicker";

    //Scene declaration
    private PackedScene TextGameplayScene;



    //Ready Function
    public override void _Ready(){
        //Associating node declarations to their instances
        DataManager = GetNode<DataManager>(dataManagerPath);
        mainMenu = GetNode<Control>(mainMenuPath);
        startNewBtn = GetNode<Button>(mainMenuPath + startNewBtnPath);
        continueBtn = GetNode<Button>(mainMenuPath + continueBtnPath);
        optionsBtn = GetNode<Button>(mainMenuPath + optionsBtnPath);
        quitBtn = GetNode<Button>(mainMenuPath + quitBtnPath);
        optionsMenu = GetNode<Control>(optionsMenuPath);
        backBtn = GetNode<Button>(optionsMenuPath + backBtnPath);
        menuContainer = GetNode<Control>(menuContainerPath);
        redSlider = GetNode<HSlider>(optionsMenuPath + colorPickerPath + "/RedSlider");
        redCount = GetNode<Label>(optionsMenuPath + colorPickerPath + "/RedCount");
        greenSlider = GetNode<HSlider>(optionsMenuPath + colorPickerPath + "/GreenSlider");
        greenCount = GetNode<Label>(optionsMenuPath + colorPickerPath + "/GreenCount");
        blueSlider = GetNode<HSlider>(optionsMenuPath + colorPickerPath + "/BlueSlider");
        blueCount = GetNode<Label>(optionsMenuPath + colorPickerPath + "/BlueCount");

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

    //Pressed Button Functions
    void startNewBtn_pressed(){
        Control MainTextScene = (Control)TextGameplayScene.Instance();
        AddChild(MainTextScene);
        menuContainer.QueueFree();
    }
     void continueBtn_pressed(){
        GD.Print("ContinuePressed");
    }
     void optionsBtn_pressed(){
        mainMenu.Visible = !mainMenu.Visible;
        optionsMenu.Visible= !optionsMenu.Visible;
    }
     void quitBtn_pressed(){
        GetTree().Quit();
    }
    void redValueChanged(float value){
        DataManager.setRed(value);
        float multiplied = value * 255;
        int casted = (int)multiplied;
        GetNode<Label>(optionsMenuPath + "/ColorPicker/RedCount").Text = casted.ToString();
    }
    void greenValueChanged(float value){
        DataManager.setGreen(value);
        float multiplied = value * 255;
        int casted = (int)multiplied;
        GetNode<Label>(optionsMenuPath + "/ColorPicker/GreenCount").Text = casted.ToString();
    }
    void blueValueChanged(float value){
        DataManager.setBlue(value);
        float multiplied = value * 255;
        int casted = (int)multiplied;
        GetNode<Label>(optionsMenuPath + "/ColorPicker/BlueCount").Text = casted.ToString();
    }
    void backBtn_pressed(){
        optionsMenu.Visible= !optionsMenu.Visible;
        mainMenu.Visible = !mainMenu.Visible;
    }
}
