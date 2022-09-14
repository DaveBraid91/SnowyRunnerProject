using UnityEngine;

namespace BOXOPHOBIC.Utils.Scripts.StyledInspector
{
    public class StyledTexturePreview : PropertyAttribute
    {
        public string displayName = "";

        public StyledTexturePreview()
        {
            this.displayName = "";
        }

        public StyledTexturePreview(string displayName)
        {
            this.displayName = displayName;
        }
    }
}

