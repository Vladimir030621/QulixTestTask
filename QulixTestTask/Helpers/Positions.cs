using System.Collections.Generic;

namespace QulixTestTask.Helpers
{
    public static class Positions
    {
        public static Dictionary<int, string> AllPositions()
        {
            return new Dictionary<int, string>()
            {
                { 1, "Software developer" },
                { 2, "Manual QA" },
                { 3, "Business analyst" },
                { 4, "Project manager" }
            };
        }
    }
}
