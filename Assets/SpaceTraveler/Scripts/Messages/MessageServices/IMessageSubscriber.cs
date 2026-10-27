using UnityEngine;

namespace SpaceTraveler.Scripts.Messages.MessageServices
{
    public interface IMessageSubscriber
    {
    }

    public interface IMessageSubscriber<in T> : IMessageSubscriber where T : IMessage
    {
        void OnMessage(T message);
    }
}