using Godot;
using System;

public class TextScene : Control
{
    public string currentScene;
    private int page = 0;
    private Godot.Collections.Array currentSceneText;
    private Godot.Collections.Dictionary currentSceneData;
    private DataManager DataManager;



    public override void _Ready(){
        DataManager = GetNode<DataManager>("/root/DataManager");

        if(DataManager.currentScene != ""){
            currentScene = DataManager.currentScene;
            currentSceneData = DataManager.getSceneDatas(currentScene);
        }
    }

    private void getCurrentSceneTextArray(){
        currentSceneText = DataManager.loadTextFromJson(currentScene);
    }
}
