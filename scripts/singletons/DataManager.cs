using Godot;
using System;

public class DataManager : Node
{
    private string optionsFilePath = "user://saveOptions.save";
    private string saveFilePath = "user://saveUserData.save";
    private string textFileFrPath = "res://ressources/JSON/TextFR.json";
    public Color uiColor;
    public string currentScene = "";
    public bool popAnimDone = false;



    //Ready Function
    public override void _Ready()
    {
        loadOptionsData();
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
            optionsFile.Close();
        }
        setUiColor();
    }

    //Save UserData
    public void saveUserData(){
        File saveFile = new File();
        saveFile.Open(saveFilePath, File.ModeFlags.Write);
        saveFile.StoreVar(currentScene);
        saveFile.Close();
    }
    //Check if User has ever played the game
    public bool userDataExists(){
        bool saveFileExists = false;
        File saveFile = new File();
        if(saveFile.FileExists(saveFilePath)){
            saveFileExists = true;
        }
        return saveFileExists;
    }
    //Load UserData
    public void loadUserData(){
        File saveFile = new File();
        if(userDataExists() == true){
            saveFile.Open(saveFilePath, File.ModeFlags.Read);
            currentScene = (String)saveFile.GetVar();
            saveFile.Close();
        }
    }



    //Load Scene from json
    public Godot.Collections.Dictionary loadSceneFromJson(string sceneRef){
        File sceneFile = new File();
        sceneFile.Open(textFileFrPath, File.ModeFlags.Read);
        string jsonTxt = sceneFile.GetAsText();
        Godot.Collections.Dictionary jsonDict = (Godot.Collections.Dictionary)JSON.Parse(jsonTxt).Result;
        sceneFile.Close();
        return (Godot.Collections.Dictionary)jsonDict[sceneRef];
    }



    //UI Color modifiers
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
