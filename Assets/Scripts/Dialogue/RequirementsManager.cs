using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Dialogue
{
    public class RequirementsManager
    {
        private Dictionary<string, int> m_requirements = new Dictionary<string, int>();

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

        public bool CheckMeetsRequirements(IEnumerable<DialogueRequirement> requirements)
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

            return meetsSoftRequirements && meetsHardRequirements;
        }

        private bool CheckMeetsRequirement(DialogueRequirement requirement)
        {
            return CheckMeetsRequirement(requirement.Name, requirement.Value, requirement.RequirementMode);
        }

        private bool CheckMeetsRequirement(string name, int value, RequirementMode mode)
        {
            if (String.IsNullOrEmpty(name))
                return true;

            if (m_requirements.TryGetValue(name, out int result))
            {
                switch (mode)
                {
                    case RequirementMode.GREATER_THAN:
                        return value > result;
                    case RequirementMode.LESS_THAN:
                        return value < result;
                    case RequirementMode.EQUAL:
                        return value == result;
                    case RequirementMode.NOT_EQUAL:
                        return value != result;
                    default:
                        return false;
                }
            }

            return false;
        }
    }
}