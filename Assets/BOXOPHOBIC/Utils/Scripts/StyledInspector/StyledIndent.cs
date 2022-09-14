// Cristian Pop - https://boxophobic.com/

using UnityEngine;

namespace BOXOPHOBIC.Utils.Scripts.StyledInspector
{
    public class StyledIndent : PropertyAttribute
    {
        public int indent;

        public StyledIndent(int indent)
        {
            this.indent = indent;
        }
    }
}

