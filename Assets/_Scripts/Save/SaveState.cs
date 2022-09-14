using System;

namespace _Scripts.Save
{
    /// <summary>
    /// SaveSate is a class with serializable data, so its information can be saved or loaded from a serialized
    /// file on any device
    /// </summary>
    [System.Serializable]
    public class SaveState
    {
        [NonSerialized] private const int HAT_COUNT = 16;

        public int Highscore { set; get; }
        public int TotalCollectables { set; get; }
        public DateTime LastSaveTime { set; get; }
        public int CurrentHatIndex { set; get; }
        public byte[] UnlockedHatFlag { set; get; }

        public SaveState()
        {
            Highscore = 0;
            TotalCollectables = 0;
            LastSaveTime = DateTime.Now;
            CurrentHatIndex = 0;
            UnlockedHatFlag = new byte[HAT_COUNT];
            UnlockedHatFlag[0] = 1;
        }
    }
}
