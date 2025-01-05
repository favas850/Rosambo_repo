using System;
using System.Collections.Generic;
using UnityEngine;

namespace Rosambo.Systems.TimerSystem
{
    public class TimerService : MonoBehaviour, ITimerService
    {
        private List<Timer> _timers = new List<Timer>();

        private void Start()
        {
            RosamboApplication.ServiceLocator.AddService<ITimerService>(this);
        }

        private void Update()
        {
            Tick(Time.deltaTime);
        }

        public void AddTimer(Timer timer)
        {
            if (_timers.Contains(timer) == false)
            {
                _timers.Add(timer);
            }
        }

        public void RemoveTimer(Timer timer)
        {
            _timers.Remove(timer);
        }

        private void Tick(float deltaTime)
        {
            for (int i = _timers.Count - 1; i >= 0; i--)
            {
                if (_timers[i].Tick(deltaTime))
                {
                    _timers.RemoveAt(i);
                }
            }
        }
    }

    public interface ITimerService : IService
    {
        void AddTimer(Timer timer);
        void RemoveTimer(Timer timer);
    }

    public class Timer
    {
        public event Action Expired;
        public float TimeLeft;
        public bool Finished;

        public bool Tick(float deltaTime)
        {
            TimeLeft -= deltaTime;
            if (TimeLeft <= 0)
            {
                Finished = true;
                Expired?.Invoke();
                return true;
            }

            return false;
        }

        public void ResetTo(float timeLeft)
        {
            TimeLeft = timeLeft;
            Finished = false;
        }
    }
}