using Godot;
using System;

public class TextScene : Control
{
    private DataManager DataManager;
    private RichTextLabel RTL;
    private Timer TypingTimer;
    public string currentScene;
    private Godot.Collections.Dictionary currentSceneData;
    private Godot.Collections.Array currentSceneText;
    private string choiceASceneRef;
    private string choiceBSceneRef;
    private int page = 0;


    //Ready Function
    public override void _Ready(){
        DataManager = GetNode<DataManager>("/root/DataManager");
        RTL = GetNode<RichTextLabel>("RichTextLabel");
        TypingTimer = GetNode<Timer>("TypingTimer");

        if(DataManager.currentScene != ""){
            gatherAllSceneDatas(currentScene);
        }
        else if(DataManager.currentScene == ""){
            DataManager.currentScene = "1-1";
            gatherAllSceneDatas(currentScene);
        }

        RTL.BbcodeText = ((string)currentSceneText[page]);
        RTL.VisibleCharacters = 0;

        TypingTimer.Connect("timeout", this, "typingTimerTimeout");
    }



    //Input function
    public override void _Input(InputEvent inputEvent){
        if(inputEvent is InputEventMouseButton && inputEvent.IsPressed()){
            if(RTL.VisibleCharacters > RTL.GetTotalCharacterCount()){
                return;
            }
            //Show all the characters
            else {
                RTL.VisibleCharacters = RTL.GetTotalCharacterCount();
            }
        }
    }


    //TimeOut function
    private void typingTimerTimeout(){
        if(RTL.VisibleCharacters < RTL.GetTotalCharacterCount()){
            RTL.VisibleCharacters = RTL.VisibleCharacters + 1;
        }
    }



    //Function to get the scene datas and apply them to the script
    private void gatherAllSceneDatas(string sceneRef){
        currentScene = DataManager.currentScene;
        currentSceneData = DataManager.loadSceneFromJson(sceneRef);
        currentSceneText = (Godot.Collections.Array)currentSceneData["text"];
        choiceASceneRef = (string)currentSceneData["choiceA"];
        choiceBSceneRef = (string)currentSceneData["choiceB"];
    }
}
