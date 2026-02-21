using UnityEngine;

namespace Systems
{
    public class HealthSystem : MonoBehaviour
    {
        private int _heath;
        
        public int Heath => _heath;
        
        public void ChangeValue(int value)
        {
            if (value > 0)
                _heath += value;
            
            if (_heath <= 0)
                Die();
        }

        private void Die()
        {
            
        }
    }
}