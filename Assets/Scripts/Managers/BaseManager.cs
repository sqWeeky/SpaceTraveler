using UnityEngine;

namespace Managers
{
    public class BaseManager: MonoBehaviour, IManager
    {
        public virtual void InitManager()
        {
            return;
        }
    }
}