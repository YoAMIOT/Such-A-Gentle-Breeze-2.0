using Godot;
using System;

public class SoundManager : Node
{
    private AudioStreamPlayer buttonSound;



    //Ready function
    public override void _Ready()
    {
        buttonSound = GetNode<AudioStreamPlayer>("ButtonSound");
    }



    //Function to play button sound
    public void playButtonSound(){
        buttonSound.Play();
    }
}
