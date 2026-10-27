using SpaceTraveler.Scripts.Messages.MessageServices;
using UnityEngine;

namespace SpaceTraveler.Scripts.Messages
{
    public class MessagesListener : MonoBehaviour,
        IMessageSubscriber<Messages.GameMessages.WorldChestOpened>
    {
        private void OnEnable()
        {
            MessageService.Subscribe(this);           
        }

        private void OnDisable()
        {
            MessageService.Unsubscribe(this);
        }

        public void OnMessage(GameMessages.WorldChestOpened message)
        {
            
        }
    }
}