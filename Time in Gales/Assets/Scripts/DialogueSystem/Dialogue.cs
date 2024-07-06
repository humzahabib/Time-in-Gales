using UnityEngine;



[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue")]
public class Dialogue : ScriptableObject
{
    public Sprite narratorImage;
    public string message;

    public Dialogue daughterDialogue;

}