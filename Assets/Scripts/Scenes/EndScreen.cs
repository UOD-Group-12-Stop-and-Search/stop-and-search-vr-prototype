using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes
{
    public class EndScreen : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI m_text = null!;

        [SerializeField]
        private float m_secondsBetweenCharacters = 0.25f;

        private void Start()
        {
            IReadOnlyDictionary<string,object> args = SceneLoader.GetArguments();
            string text = (string)args.GetValueOrDefault("end_screen_text", "Invalid argument");

            StartCoroutine(AddTextOverTime(text));
        }

        private IEnumerator AddTextOverTime(string text)
        {
            Stack<char> remainingText = new Stack<char>(text.Reverse());

            string currentText = String.Empty;

            while (remainingText.Any())
            {
                currentText += remainingText.Pop();
                m_text.text = currentText;
                yield return new WaitForSeconds(m_secondsBetweenCharacters);
            }
        }

        public void ReturnToMainMenu()
        {
            SceneManager.LoadScene("Scenes/MainMenu");
        }

        public void Exit()
        {
            if (!Application.isEditor)
                Application.Quit();
        }
    }
}