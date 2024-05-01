using UI.QuestioningUI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Dialogue.InvokableQuestions
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Dialogue/Questions/SwitchDialogueQuestion")]
    public class SwitchDialogue : PlayerQuestion
    {
        [SerializeField] private Dialogue m_searchDialogue = null!;

        [SerializeField] private bool m_resetRequirements;

        public override void PostInvokeAction(QuestioningPanel panel)
        {
            panel.RequirementsManager.ClearValues();
            panel.PopulatePanel(m_searchDialogue);
        }
    }
}