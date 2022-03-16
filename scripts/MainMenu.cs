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



    //Ready Function
    public override void _Ready(){
        DataManager = GetNode<DataManager>("/root/DataManager");
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
       
        redSlider = GetNode<HSlider>("OptionsMenu/ColorPicker/RedSlider");
        redSlider.Connect("value_changed", this, "redValueChanged");
        redSlider.Value = DataManager.uiColor.r;
        redCount = GetNode<Label>("OptionsMenu/ColorPicker/RedCount");
        float redMultiplied = DataManager.uiColor.r * 255.0F;
        int redCasted = (int)redMultiplied;
        redCount.Text = redCasted.ToString();

        greenSlider = GetNode<HSlider>("OptionsMenu/ColorPicker/GreenSlider");
        greenSlider.Connect("value_changed", this, "greenValueChanged");
        greenSlider.Value = DataManager.uiColor.g;
        greenCount = GetNode<Label>("OptionsMenu/ColorPicker/GreenCount");
        float greenMultiplied = DataManager.uiColor.g * 255.0F;
        int greenCasted = (int)greenMultiplied;
        greenCount.Text = greenCasted.ToString();

        blueSlider = GetNode<HSlider>("OptionsMenu/ColorPicker/BlueSlider");
        blueSlider.Connect("value_changed", this, "blueValueChanged");
        blueSlider.Value = DataManager.uiColor.b;
        blueCount = GetNode<Label>("OptionsMenu/ColorPicker/BlueCount");
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
        mainMenu.SetVisible(!mainMenu.IsVisible());
        optionsMenu.SetVisible(!optionsMenu.IsVisible());
    }
     void quitBtn_pressed(){
        GetTree().Quit();
    }
    void redValueChanged(float value){
        DataManager.setRed(value);
        float multiplied = value * 255;
        int casted = (int)multiplied;
        redCount.Text = casted.ToString();
    }
    void greenValueChanged(float value){
        DataManager.setGreen(value);
        float multiplied = value * 255;
        int casted = (int)multiplied;
        greenCount.Text = casted.ToString();
    }
    void blueValueChanged(float value){
        DataManager.setBlue(value);
        float multiplied = value * 255;
        int casted = (int)multiplied;
        blueCount.Text = casted.ToString();
    }
    void backBtn_pressed(){
        optionsMenu.SetVisible(!optionsMenu.IsVisible());
        mainMenu.SetVisible(!mainMenu.IsVisible());
    }
}
