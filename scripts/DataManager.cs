using Godot;
using System;

public class DataManager : Node
{
    private string optionsFile = "user://saveOptions.save";
    private int red;
    private int green;
    private int blue;
    public override void _Ready()
    {
        loadOptionsData();
    }

    public void saveOptionsData(){
        File saveOptions = new File();
        saveOptions.Open(optionsFile, File.ModeFlags.Write);
        saveOptions.StoreVar(red);
        saveOptions.StoreVar(green);
        saveOptions.StoreVar(blue);
        saveOptions.Close();
    }

    public void loadOptionsData(){
        File loadOptions = new File();
        if(loadOptions.FileExists(optionsFile)){
            loadOptions.Open(optionsFile, File.ModeFlags.Write);
            this.red = (int)loadOptions.GetVar();
            this.green = (int)loadOptions.GetVar();
            this.blue = (int)loadOptions.GetVar();
        }
    }
}
