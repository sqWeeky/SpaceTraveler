using System.Runtime.InteropServices;
using Configs;
using Managers;
using Players;
using Reflex.Attributes;
using Reflex.Core;
using UnityEngine;

namespace StateMachine.States
{
    public abstract class GameState : IGameState
    {
        [Inject] protected GameStateMachine StateMachine { get; private set; }
        [Inject] protected UIManager UIManager { get; private set; }
        [Inject] protected AudioManager AudioManager { get; private set; }
        [Inject] protected InputManager InputManager { get; private set; }
        [Inject] protected LevelManager LevelManager { get; private set; }
        //protected Player Player { get; private set; }

        [Inject]
        public void Construct(Container container)
        {
            Debug.LogError("Constructing Game State");
            StateMachine = container.Resolve<GameStateMachine>();
            UIManager = container.Resolve<UIManager>();
            AudioManager = container.Resolve<AudioManager>();
            InputManager = container.Resolve<InputManager>();
            LevelManager = container.Resolve<LevelManager>();
        }

        public virtual void Enter()
        {
        }

        public virtual void Update()
        {
        }

        public virtual void Exit()
        {
        }

        protected void ChangeState<T>() where T : GameState => StateMachine.ChangeState<T>();
    }
}