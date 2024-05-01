using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Dialogue
{
    public class RequirementsManager
    {
        private Dictionary<string, int> m_requirements = new Dictionary<string, int>();

        public void SetValue(NameIntPair pair)
        {
            m_requirements[pair.Name] = pair.Value;
        }

        public void ReportRequirement(ResponseResult requirement)
        {
            // ensure the key exists
            m_requirements.TryAdd(requirement.Name, 0);

            switch (requirement.SetMode)
            {
                case ResponseLevelSetMode.OVERWRITE:
                    m_requirements[requirement.Name] = requirement.Value;
                    break;
                case ResponseLevelSetMode.ADD:
                    m_requirements[requirement.Name] += requirement.Value;
                    break;
                case ResponseLevelSetMode.LIMIT:
                    m_requirements[requirement.Name] = Math.Min(m_requirements[requirement.Name], requirement.Value);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public (bool meetsHardRequirements, bool meetsSoftRequirements) CheckMeetsRequirements(IEnumerable<DialogueRequirement> requirements)
        {
            bool meetsSoftRequirements = true;
            bool meetsHardRequirements = true;

            foreach (DialogueRequirement requirement in requirements)
            {
                switch (requirement.RequirementLevel)
                {
                    case RequirementLevel.HARD:
                        meetsHardRequirements = meetsHardRequirements && CheckMeetsRequirement(requirement);
                        break;
                    case RequirementLevel.SOFT:
                        meetsSoftRequirements = meetsSoftRequirements && CheckMeetsRequirement(requirement);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return (meetsSoftRequirements, meetsHardRequirements);
        }

        private bool CheckMeetsRequirement(DialogueRequirement requirement)
        {
            return CheckMeetsRequirement(requirement.Name, requirement.Value, requirement.RequirementMode);
        }

        private bool CheckMeetsRequirement(string name, int value, RequirementMode mode)
        {
            if (String.IsNullOrEmpty(name))
                return true;

            // ensure than an entry exists
            m_requirements.TryAdd(name, 0);

            int result = m_requirements[name];
            switch (mode)
            {
                case RequirementMode.GREATER_THAN:
                    return result > value;
                case RequirementMode.GREATER_THAN_OR_EQUAL_TO:
                    return result >= value;
                case RequirementMode.LESS_THAN:
                    return result < value;
                case RequirementMode.LESS_THAN_OR_EQUAL_TO:
                    return result <= value;
                case RequirementMode.EQUAL:
                    return result == value;
                case RequirementMode.NOT_EQUAL:
                    return result != value;
                default:
                    return false;
            }
        }

        public void ClearValues()
        {
            m_requirements.Clear();
        }
    }
}