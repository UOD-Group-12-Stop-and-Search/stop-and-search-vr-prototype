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

            NpcItemController[] controllers = FindObjectsOfType<NpcItemController>();

            SceneLoader sceneLoader = new SceneLoader();

            // TODO: Not this!!!! This is temporary until the updated item controller branch is merged.
            bool found = false;
            foreach (NpcItemController controller in controllers)
            {
                if (controller.npcWithSusItem == panel.Host.gameObject)
                {
                    found = true;
                    break;
                }
            }

            if (found)
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