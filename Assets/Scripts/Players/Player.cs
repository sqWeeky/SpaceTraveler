using System;
using UnityEngine;

namespace Players
{
    public class Player : MonoBehaviour
    {
        public event Action OnPlayerDied;
        public event Action OnLevelComplete;
    }
}
