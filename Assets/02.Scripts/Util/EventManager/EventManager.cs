using System.Collections.Generic;
using System;

namespace EventManagers
{
    public class EventManager
    {
        /// <summary>
        /// EventManager.FunctionName("KeyName" , Action)
        /// </summary>
        private static Dictionary<string, Action> eventDictionary = new Dictionary<string, Action>();

        public static void StartListening(string eventName, Action listener)
        {
            Action thisEvent;
            if (eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent += listener;
                eventDictionary[eventName] = thisEvent;
            }
            else
            {
                eventDictionary.Add(eventName, listener);
            }
        }

        public static void StopListening(string eventName, Action listener)
        {
            Action thisEvent;
            if (eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent -= listener;
                eventDictionary[eventName] = thisEvent;
            }
            else
            {
                eventDictionary.Remove(eventName);
            }
        }

        public static void TriggerEvent(string eventName)
        {
            Action thisEvent;
            if (eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent?.Invoke();
            }
        }
        

    }
}
