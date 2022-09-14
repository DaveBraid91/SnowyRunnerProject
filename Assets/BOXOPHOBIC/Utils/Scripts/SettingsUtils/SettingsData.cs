// Cristian Pop - https://boxophobic.com/

using UnityEngine;

namespace BOXOPHOBIC.Utils.Scripts.SettingsUtils
{
    [CreateAssetMenu(fileName = "Data", menuName = "BOXOPHOBIC/Settings Data")]
    public class SettingsData : ScriptableObject
    {
        [Space]
        public string data = "";
    }
}