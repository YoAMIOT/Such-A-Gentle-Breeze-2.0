using Godot;
using System;

public class DataManager : Node
{
    private string optionsFilePath = "user://saveOptions.save";
    private string saveFilePath = "user://saveUserData.save";
    private string textFileFrPath = "res://ressources/JSON/TextFR.json";
    private string dataFilePath = "res://ressources/JSON/sceneRepertory.json";
    public Color uiColor;
    public string currentScene = "";
    public Godot.Collections.Dictionary textDictionary;



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
    //Load UserData
    public void loadUserData(){
        File saveFile = new File();
        if(saveFile.FileExists(saveFilePath)){
            saveFile.Open(saveFilePath, File.ModeFlags.Read);
            currentScene = (String)saveFile.GetVar();
            saveFile.Close();
        }
    }

    //Load Text
    public Godot.Collections.Array loadTextFromJson(string sceneRef){
        File textFile = new File();
        textFile.Open(textFileFrPath, File.ModeFlags.Read);
        string jsonTxt = textFile.GetAsText();
        Godot.Collections.Dictionary textDict = (Godot.Collections.Dictionary)JSON.Parse(jsonTxt).Result;
        textFile.Close();
        Godot.Collections.Array sceneTextArray = (Godot.Collections.Array)textDict[sceneRef];
        return sceneTextArray;
    }
    //Load Scene Datas
    public Godot.Collections.Dictionary getSceneDatas(string sceneRef){
        File dataFile = new File();
        dataFile.Open(dataFilePath, File.ModeFlags.Read);
        string jsonTxt = dataFile.GetAsText();
        Godot.Collections.Dictionary dataDict = (Godot.Collections.Dictionary)JSON.Parse(jsonTxt).Result;
        dataFile.Close();
        Godot.Collections.Dictionary sceneDatas = (Godot.Collections.Dictionary)dataDict[sceneRef];
        return sceneDatas;
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
