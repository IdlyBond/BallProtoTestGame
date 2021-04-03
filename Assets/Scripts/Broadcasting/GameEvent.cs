using System;
using System.Collections.Generic;

namespace AndromedaCore.LevelManagement
{
    public class GameEvent <T>
    {
        private readonly List<Action<T>> _callbacks = new List<Action<T>>();

        public void Subscribe(Action<T> callback)
        {
            _callbacks.Add(callback);
        }
    
        public void Unsubscribe(Action<T> callback)
        {
            _callbacks.Remove(callback);
        }

        public void Publish(T unit)
        {
            foreach (Action<T> callback in _callbacks) callback?.Invoke(unit);
        }
    }
}