using UI.QuestioningUI;
using UnityEngine;

namespace Dialogue.InvokableQuestions
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Dialogue/Questions/ArrestTargetQuestion")]
    public class ArrestQuestion : PlayerQuestion
    {
        public override void PreInvokeAction(QuestioningPanel panel)
        {
            // need an actual game end function here
            Debug.Log("Mods! Arrest this man!");
        }
    }
}