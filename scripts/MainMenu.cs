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
    private HSlider redSlider;
    private HSlider greenSlider;
    private HSlider blueSlider;
    private Label redCount;
    private Label greenCount;
    private Label blueCount;
    private Button backBtn;
    private DataManager DataManager;
    private string dataManagerPath = "/root/DataManager";
    private string mainMenuPath = "MenuContainer/MainMenu";
    private string optionsMenuPath = "MenuContainer/OptionsMenu";
    private string startNewBtnPath = "/StartNewBtn";
    private string continueBtnPath = "/ContinueBtn";
    private string optionsBtnPath = "/OptionsBtn";
    private string quitBtnPath = "/QuitBtn";
    private string backBtnPath = "/BackBtn";



    //Ready Function
    public override void _Ready(){
        DataManager = GetNode<DataManager>(dataManagerPath);
        mainMenu = GetNode<Control>(mainMenuPath);
        startNewBtn = GetNode<Button>(mainMenuPath + startNewBtnPath);
        continueBtn = GetNode<Button>(mainMenuPath + continueBtnPath);
        optionsBtn = GetNode<Button>(mainMenuPath + optionsBtnPath);
        quitBtn = GetNode<Button>(mainMenuPath + quitBtnPath);
        optionsMenu = GetNode<Control>(optionsMenuPath);
        backBtn = GetNode<Button>(optionsMenuPath + backBtnPath);

        startNewBtn.Connect("pressed", this, "startNewBtn_pressed");
        continueBtn.Connect("pressed", this, "continueBtn_pressed");
        optionsBtn.Connect("pressed", this, "optionsBtn_pressed");
        quitBtn.Connect("pressed", this, "quitBtn_pressed");
        backBtn.Connect("pressed", this, "backBtn_pressed");
       
        redSlider = GetNode<HSlider>(optionsMenuPath +"/ColorPicker/RedSlider");
        redSlider.Connect("value_changed", this, "redValueChanged");
        redSlider.Value = DataManager.uiColor.r;
        redCount = GetNode<Label>(optionsMenuPath + "/ColorPicker/RedCount");
        float redMultiplied = DataManager.uiColor.r * 255.0F;
        int redCasted = (int)redMultiplied;
        redCount.Text = redCasted.ToString();

        greenSlider = GetNode<HSlider>(optionsMenuPath + "/ColorPicker/GreenSlider");
        greenSlider.Connect("value_changed", this, "greenValueChanged");
        greenSlider.Value = DataManager.uiColor.g;
        greenCount = GetNode<Label>(optionsMenuPath + "/ColorPicker/GreenCount");
        float greenMultiplied = DataManager.uiColor.g * 255.0F;
        int greenCasted = (int)greenMultiplied;
        greenCount.Text = greenCasted.ToString();

        blueSlider = GetNode<HSlider>(optionsMenuPath + "/ColorPicker/BlueSlider");
        blueSlider.Connect("value_changed", this, "blueValueChanged");
        blueSlider.Value = DataManager.uiColor.b;
        blueCount = GetNode<Label>(optionsMenuPath + "/ColorPicker/BlueCount");
        float blueMultiplied = DataManager.uiColor.b * 255.0F;
        int blueCasted = (int)blueMultiplied;
        blueCount.Text = blueCasted.ToString();
    }

    //Pressed Button Functions
    void startNewBtn_pressed(){
        GD.Print("StartNewPressed");
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
