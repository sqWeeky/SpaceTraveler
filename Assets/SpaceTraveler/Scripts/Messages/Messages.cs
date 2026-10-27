using SpaceTraveler.Scripts.Messages.MessageServices;
using UnityEngine;

namespace SpaceTraveler.Scripts.Messages
{
    public static partial class Timer
    {
        public sealed class GameStarted : BaseMessage<GameStarted, string>
        {
        }

        public sealed class TimerStart : BaseMessage<TimerStart>
        {
        }

        public sealed class TimerPaused : BaseMessage<TimerPaused>
        {
        }

        public sealed class TimerStop : BaseMessage<TimerStop>
        {
        }
    }

    public static partial class GameMessages
    {
        public sealed class NewSwordEquiped : BaseMessage<NewSwordEquiped>
        {
        }

        public sealed class WorldChestOpened : BaseMessage<WorldChestOpened>
        {
        }
    }
}