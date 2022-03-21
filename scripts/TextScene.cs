using Godot;
using System;

public class TextScene : Control
{
    //Scene nodes
    private DataManager DataManager;
    private RichTextLabel RTL;
    private Control Buttons;
    private Button ChoiceABtn;
    private Button ChoiceBBtn;

    //Variables
    private Godot.Collections.Dictionary currentSceneData;
    private Godot.Collections.Array currentSceneText;
    private string choiceASceneRef;
    private string choiceBSceneRef;
    private string choiceATxt;
    private string choiceBTxt;
    private int page = 0;
    private bool writing;


    //Ready Function
    public override void _Ready(){
        DataManager = GetNode<DataManager>("/root/DataManager");
        RTL = GetNode<RichTextLabel>("RichTextLabel");
        Buttons = GetNode<Control>("Buttons");
        ChoiceABtn = GetNode<Button>("Buttons/ChoiceABtn");
        ChoiceBBtn = GetNode<Button>("Buttons/ChoiceBBtn");
        writing = true;

        if(DataManager.currentScene != ""){
            gatherAllSceneDatas(DataManager.currentScene);
        }
        else if(DataManager.currentScene == ""){
            DataManager.currentScene = "1-1";
            gatherAllSceneDatas(DataManager.currentScene);
        }

        RTL.BbcodeText = ((string)currentSceneText[page]);
        RTL.VisibleCharacters = 0;
        ChoiceABtn.Text = choiceATxt;
        ChoiceBBtn.Text = choiceBTxt;

        ChoiceABtn.Connect("pressed", this, "choiceAPressed");
        ChoiceBBtn.Connect("pressed", this, "choiceBPressed");
    }



    //Input function
    public override void _Input(InputEvent inputEvent){
        if(inputEvent is InputEventMouseButton && inputEvent.IsPressed()){
            if(RTL.VisibleCharacters == RTL.GetTotalCharacterCount()){
                if (page < currentSceneText.Count - 1){
                    page += 1;
                    RTL.BbcodeText = (string)currentSceneText[page];
                    RTL.VisibleCharacters = 0;
                    writing = true;
                }
                else if(page == currentSceneText.Count - 1){
                    Buttons.Visible = true;
                }
            }
            //Show all the characters
            else {
                RTL.VisibleCharacters = RTL.GetTotalCharacterCount();
                writing = false;
            }
        }
    }



    //Process function
    public override void _Process(float delta)
    {
        if(writing == true){
            if(RTL.VisibleCharacters < RTL.GetTotalCharacterCount()){
                RTL.VisibleCharacters = RTL.VisibleCharacters + 1;
            }
            else if(RTL.VisibleCharacters > RTL.GetTotalCharacterCount()){
                RTL.VisibleCharacters = RTL.GetTotalCharacterCount();
            }

            if(RTL.VisibleCharacters == RTL.GetTotalCharacterCount()){
                writing = false;
            }
        }
    }



    //ButtonPressed
    private void choiceAPressed(){
        DataManager.currentScene = choiceASceneRef;
        gatherAllSceneDatas(choiceASceneRef);
        resetRTL();
    }
    private void choiceBPressed(){
        DataManager.currentScene = choiceBSceneRef;
        gatherAllSceneDatas(choiceBSceneRef);
        resetRTL();
    }



    //Reset the scene with new parameters
    private void resetRTL(){
        page = 0;
        RTL.BbcodeText = ((string)currentSceneText[page]);
        RTL.VisibleCharacters = 0;
        writing = true;
        //TypingTimer.Paused = false;
        Buttons.Visible = false;
        ChoiceABtn.Text = choiceATxt;
        ChoiceBBtn.Text = choiceBTxt;
        DataManager.saveUserData();
    }



    //Function to get the scene datas and apply them to the script
    private void gatherAllSceneDatas(string sceneRef){
        currentSceneData = DataManager.loadSceneFromJson(sceneRef);
        currentSceneText = (Godot.Collections.Array)currentSceneData["text"];
        choiceASceneRef = (string)currentSceneData["choiceARef"];
        choiceBSceneRef = (string)currentSceneData["choiceBRef"];
        choiceATxt = (string)currentSceneData["choiceATxt"];
        choiceBTxt = (string)currentSceneData["choiceBTxt"];
    }
}
