using UI.QuestioningUI;
using UnityEngine;

namespace Dialogue.InvokableQuestions
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Dialogue/Questions/LeaveDialogueQuestion")]
    public class LeaveQuestion : PlayerQuestion
    {
        public override void PreInvokeAction(QuestioningPanel panel)
        {
            panel.ExitQuestioning();
        }
    }
}