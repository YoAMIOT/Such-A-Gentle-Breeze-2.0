using Godot;
using System;

public class TextScene : Control
{
    //Scene nodes
    private DataManager DataManager;
    private SoundManager SoundManager;
    private RichTextLabel RTL;
    private Control Buttons;
    private Button ChoiceABtn;
    private Button ChoiceBBtn;
    private Control PauseMenu;
    private Vector2 smallBtn = new Vector2 (960, 248);
    private Vector2 bigBtn = new Vector2 (1920, 248);

    //Variables
    private Godot.Collections.Dictionary currentSceneData;
    private Godot.Collections.Array currentSceneText;
    private string choiceASceneRef;
    private string choiceBSceneRef;
    private string choiceATxt;
    private string choiceBTxt;
    private int page = 0;
    private bool writing;
    private bool paused = false;


    //Ready Function
    public override void _Ready(){
        //Node linking
        DataManager = GetNode<DataManager>("/root/DataManager");
        SoundManager = GetNode<SoundManager>("/root/SoundManager");
        RTL = GetNode<RichTextLabel>("RichTextLabel");
        Buttons = GetNode<Control>("Buttons");
        ChoiceABtn = GetNode<Button>("Buttons/ChoiceABtn");
        ChoiceBBtn = GetNode<Button>("Buttons/ChoiceBBtn");
        PauseMenu = GetParent<Control>().GetNode<Control>("PauseMenu");

        //Check if the player started a new campaign or loaded a saved game
        if(DataManager.currentScene != ""){
            gatherAllSceneDatas(DataManager.currentScene);
        }
        else if(DataManager.currentScene == ""){
            DataManager.currentScene = "1-1";
            gatherAllSceneDatas(DataManager.currentScene);
        }

        //Reset the RTL
        resetRTL();

        //Signals
        PauseMenu.Connect("pauseMenuSwitch", this, "switchPauseMenu");
        ChoiceABtn.Connect("pressed", this, "choiceAPressed");
        ChoiceBBtn.Connect("pressed", this, "choiceBPressed");
    }



    //Input function
    public override void _Input(InputEvent inputEvent){
        if(inputEvent is InputEventMouseButton && inputEvent.IsPressed() && paused == false){
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
        else if (inputEvent.IsActionPressed("ui_cancel")){
            SoundManager.playButtonSound();
            switchPauseMenu();
        }
    }



    //Pause Menu switch
    public void switchPauseMenu(){
        paused = !paused;
        PauseMenu.GetNode<Control>("OptionsMenu").Visible = false;
        PauseMenu.GetNode<Control>("WarnPopup").Visible = false;
        PauseMenu.GetNode<Control>("Main").Visible = true;
        this.Visible = !this.Visible;
        PauseMenu.Visible = !PauseMenu.Visible;
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
        if(choiceASceneRef == "DTR"){
            GetTree().ChangeScene("res://scenes/DTR.tscn");
        }
        else{
            DataManager.currentScene = choiceASceneRef;
            gatherAllSceneDatas(choiceASceneRef);
            resetRTL();
            SoundManager.playButtonSound();
        }
    }
    private void choiceBPressed(){
        DataManager.currentScene = choiceBSceneRef;
        gatherAllSceneDatas(choiceBSceneRef);
        resetRTL();
        SoundManager.playButtonSound();
    }



    //Reset the scene with new parameters
    private void resetRTL(){
        page = 0;
        RTL.BbcodeText = ((string)currentSceneText[page]);
        RTL.VisibleCharacters = 0;
        writing = true;
        Buttons.Visible = false;
        ChoiceABtn.Text = choiceATxt;
        ChoiceBBtn.Text = choiceBTxt;
        DataManager.saveUserData();
        //Checks if there's a B choice
        if(choiceBTxt == "none"){
            ChoiceBBtn.Visible = false;
            ChoiceABtn.SetSize(bigBtn);
        }
        else{
            ChoiceBBtn.Visible = true;
            ChoiceABtn.SetSize(smallBtn);
        }
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
