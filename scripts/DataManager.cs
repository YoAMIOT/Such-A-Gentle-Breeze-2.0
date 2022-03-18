using Godot;
using System;

public class DataManager : Node
{
    private string optionsFilePath = "user://saveOptions.save";
    private string textFileFrPath = "res://ressources/JSON/TextFR.json";
    public Color uiColor;
    public Godot.Collections.Dictionary textDictionary;



    //Ready Function
    public override void _Ready()
    {
        loadOptionsData();
        loadTextFromJson();
    }



    //Save Options
    public void saveOptionsData(){
        File optionsFile = new File();
        optionsFile.Open(optionsFilePath, File.ModeFlags.Write);
            optionsFile.StoreVar(uiColor);
        optionsFile.Close();
    }
    //Load Options
    public void loadOptionsData(){
        File optionsFile = new File();
        if(optionsFile.FileExists(optionsFilePath)){
            optionsFile.Open(optionsFilePath, File.ModeFlags.Read);
            uiColor = (Color)optionsFile.GetVar();
        }
        optionsFile.Close();
        setUiColor();
    }

    //Load Text
    public void loadTextFromJson(){
        File textFile = new File();
        textFile.Open(textFileFrPath, File.ModeFlags.Read);
        string jsonTxt = textFile.GetAsText();
        textDictionary = (Godot.Collections.Dictionary)JSON.Parse(jsonTxt).Result;
        textFile.Close();
    }

    public Godot.Collections.Array loadSceneTextFromDictionary(string sceneRef){
        Godot.Collections.Array sceneTextArray = (Godot.Collections.Array)textDictionary[sceneRef];
        return sceneTextArray;
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
