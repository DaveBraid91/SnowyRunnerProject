// Cristian Pop - https://boxophobic.com/

using UnityEngine;

namespace BOXOPHOBIC.Utils.Scripts.StyledInspector
{
    public class StyledLayers : PropertyAttribute
    {
        public string display = "";
        public StyledLayers()
        {
        }

        public StyledLayers(string display)
        {
            this.display = display;
        }
    }
}

