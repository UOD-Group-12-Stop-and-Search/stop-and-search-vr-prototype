using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine.SceneManagement;

namespace Scenes
{
    public class SceneLoader
    {
        private static readonly Stack<Dictionary<string, object>> s_comittedArgHistory = new Stack<Dictionary<string, object>>();
        private readonly Dictionary<string, object> m_args = new Dictionary<string, object>();

        public void Push(string name, object arg)
        {
            m_args[name] = arg;
        }

        public void Push(params KeyValuePair<string, object>[] args)
        {
            foreach (KeyValuePair<string,object> pair in args)
            {
                m_args[pair.Key] = pair.Value;
            }
        }

        public void CommitScene(int buildIndex)
        {
            s_comittedArgHistory.Push(m_args);
            SceneManager.LoadScene(buildIndex);
        }

        public void CommitScene(string name)
        {
            s_comittedArgHistory.Push(m_args);
            SceneManager.LoadScene(name);
        }

        public static IReadOnlyDictionary<string, object> GetArguments()
        {
            if (s_comittedArgHistory.Any())
                return new ReadOnlyDictionary<string, object>(s_comittedArgHistory.Pop());
            else
                return new Dictionary<string, object>();
        }
    }
}