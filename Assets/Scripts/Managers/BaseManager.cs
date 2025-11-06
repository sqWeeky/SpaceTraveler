using Reflex.Attributes;
using Reflex.Core;
using UnityEngine;

namespace Managers
{
    public class BaseManager : MonoBehaviour, IManager, IInjectContainer
    {
        protected Container _container;

        [Inject]
        public void Construct(Container container)
        {
            _container = container;
        }

        public virtual void InitManager()
        {
            return;
        }

        public void SetContainer(Container container)
        {
            _container = container;
        }
    }
}