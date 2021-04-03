using UnityEngine;

namespace AndromedaCore.LevelManagement
{
    public static class WorldBroadcast
    {
        public static readonly GameEvent<GameObject> LevelWin = new GameEvent<GameObject>();
        public static readonly GameEvent<GameObject> LevelLoose = new GameEvent<GameObject>();
        public static readonly GameEvent<GameObject> LevelStart = new GameEvent<GameObject>();
        public static readonly GameEvent<GameObject> WinConditionAchieved = new GameEvent<GameObject>();
        public static readonly GameEvent<GameObject> LooseConditionAchieved = new GameEvent<GameObject>();
        public static readonly GameEvent<GameObject> OnApplicationStart = new GameEvent<GameObject>();
        public static readonly GameEvent<int> OnBulletDestroyed = new GameEvent<int>();
    }
}

