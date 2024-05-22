using Items;
using Pathing.States;
using Scenes;
using UI.QuestioningUI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Dialogue.InvokableQuestions
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Dialogue/Questions/ArrestTargetQuestion")]
    public class ArrestQuestion : PlayerQuestion
    {
        public override void PostInvokeAction(QuestioningPanel panel)
        {
            panel.ExitQuestioning();
            panel.Host.StateMachine.CurrentState = panel.Host.GetComponent<ArrestedState>();

            SceneLoader sceneLoader = new SceneLoader();

            if (NpcItemController.NpcWithSusItem == panel.Host.gameObject)
            {
                sceneLoader.Push("end_screen_text", "You arrested the correct person!");
            }
            else
            {
                sceneLoader.Push("end_screen_text", "You arrested the wrong person!");
            }

            sceneLoader.CommitScene("Scenes/EndScene");
        }
    }
}