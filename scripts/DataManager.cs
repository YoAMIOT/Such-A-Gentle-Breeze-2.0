using Godot;
using System;

public class DataManager : Node
{
    private string optionsFile = "user://saveOptions.save";
    public Color uiColor;



    //Ready Function
    public override void _Ready()
    {
        loadOptionsData();
    }



    //Save Options
    public void saveOptionsData(){
        File saveOptions = new File();
        saveOptions.Open(optionsFile, File.ModeFlags.Write);
            saveOptions.StoreVar(uiColor);
        saveOptions.Close();
    }
    //Load Options
    public void loadOptionsData(){
        File loadOptions = new File();
        if(loadOptions.FileExists(optionsFile)){
            loadOptions.Open(optionsFile, File.ModeFlags.Read);
            uiColor = (Color)loadOptions.GetVar();
        }
        loadOptions.Close();
        setUiColor();
    }



    public void setRed(float newValue){
        uiColor.r = newValue;
        setUiColor();
    }
    public void setGreen(float newValue){
        uiColor.g = newValue;
        setUiColor();
    }
    public void setBlue(float newValue){
        uiColor.b = newValue;
        setUiColor();
    }

    public void setUiColor(){
        GetParent().GetNode<Control>("GameScene").Modulate = uiColor;
        saveOptionsData();
    }
}
