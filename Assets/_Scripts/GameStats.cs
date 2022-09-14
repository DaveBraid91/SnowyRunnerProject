using System;
using _Scripts.GameFlow;
using UnityEngine;

namespace _Scripts
{
    public class GameStats : MonoBehaviour
    {

        private static GameStats _instance;
        public static GameStats Instance { get { return _instance; } }

        //Score
        public float score;
        public float highScore;
        public float distanceModifier = 1.5f;

        //Collectables
        public int totalCollectables;
        public int collectedThisRound;
        public float pointsCollectable = 10.0f;

        //Internal Cooldown
        private float _lastScoreUpdate;
        private float _scoreUpdateDelta = 0.2f;

        //Action
        public Action<int> OnCollectableChange;
        public Action<float> OnScoreChange;

        private void Awake()
        {
            _instance = this;
        }

        private void Update()
        {
            //Score System
            var s = GameManager.Instance.motor.transform.position.z * distanceModifier;
            s += collectedThisRound * pointsCollectable;

            if (!(s > score)) return;
            score = s;
            //The score is updated on the UI every _scoreUpdateDelta, so the UI doesnt have to render
            //every frame, which would cause a big performance issue for mobile devices
            if (!(Time.time - _lastScoreUpdate > _scoreUpdateDelta)) return;
            _lastScoreUpdate = Time.time;
            OnScoreChange?.Invoke(score);
        }
        
        /// <summary>
        /// Updates the collectables collected this round and invokes OnCollectableChange
        /// </summary>
        public void CollectCollectable()
        {
            collectedThisRound++;
            OnCollectableChange?.Invoke(collectedThisRound);
        }

        /// <summary>
        /// Resets the stats
        /// </summary>
        public void ResetSession()
        {
            score = 0;
            collectedThisRound = 0;

            OnScoreChange?.Invoke(score);
            OnCollectableChange?.Invoke(collectedThisRound);
        }

        /// <summary>
        /// Transforms the score float without decimals to a string with the format "0000000"
        /// </summary>
        /// <returns>The formatted string</returns>
        public string ScoreToText()
        {
            return ((int)score).ToString("0000000");
        }

        /// <summary>
        /// Transforms the collectables int to a string with the format "000"
        /// </summary>
        /// <returns>The formatted string</returns>
        public string CollectablesToText()
        {
            return $"x{collectedThisRound:000}";
        }
    }
}
