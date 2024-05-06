using Pathing.States;
using UI.QuestioningUI;
using UnityEngine;

namespace Dialogue.InvokableQuestions
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Dialogue/Questions/ArrestTargetQuestion")]
    public class ArrestQuestion : PlayerQuestion
    {
        public override void PreInvokeAction(QuestioningPanel panel)
        {
            panel.ExitQuestioning();
            panel.Host.StateMachine.CurrentState = panel.Host.GetComponent<ArrestedState>();
        }
    }
}