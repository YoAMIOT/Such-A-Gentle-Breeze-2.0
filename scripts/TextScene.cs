using Godot;
using System;

public class TextScene : Control
{
    private string sceneString = "SceneText";
    private int sceneNumber = 1;
    public string currentScene;
    private Godot.Collections.Array currentSceneTextArray;
    private DataManager DataManager;


    public override void _Ready(){
        DataManager = GetNode<DataManager>("/root/DataManager");
    }

    private void getCurrentSceneTextArray(){
        currentSceneTextArray = DataManager.loadSceneTextFromDictionary(currentScene);
    }
}
