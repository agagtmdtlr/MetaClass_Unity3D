using System;
using UnityEngine;

namespace Event
{
    public class GameEvents
    {
        /// <summary>
        /// int currentHP , int maxHP
        /// </summary>
        public Action<int,int> OnDamage;
        /// <summary>
        /// state name
        /// </summary>
        public Action<string> OnChangedState;
        /// <summary>
        /// skill key
        /// </summary>
        public Action<int> OnCastSkill;
    }

    public class EventSystem : MonoBehaviour
    {
    }

}