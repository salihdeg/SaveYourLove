using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue")]
public class Dialogue : ScriptableObject
{
    public string speakerName;
    public Sprite faceImage;
    [TextArea]
    public string dialogue;
    [Tooltip("Maximum 4 answer")]
    public List<string> answers;
    public int trueAnswerIndex = 0;
    public bool isQuestion = false;
}
