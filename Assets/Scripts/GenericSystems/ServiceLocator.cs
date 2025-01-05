using System;
using System.Collections.Generic;
using UnityEngine;

namespace Rosambo.Systems
{
    public class ServiceLocator
    {
        private Action<IService> _onServiceAdded;

        private readonly Dictionary<Type, IService> _services = new();

        public void AddService<TType>(TType service) where TType: IService
        {
            if (_services.TryAdd(typeof(TType), service) == false)
            {
                Debug.LogError($"Service type {typeof(TType)} already exist!");
                return;
            }

            _onServiceAdded?.Invoke(service);
        }

        public void RemoveService<TType>()
        {
            if (_services.Remove(typeof(TType)) == false)
            {
                Debug.LogError($"Service type {typeof(TType)} doesn't exist!");
            }
        }

        public TType GetService<TType>()
        {
            if (_services.TryGetValue(typeof(TType), out var service) == false)
            {
                Debug.LogError($"Service type {typeof(TType)} not found!");
            }

            return (TType)service;
        }

        public void RegisterForServiceChange<TType>(Action<IService> onAdded)
        {
            if (_services.TryGetValue(typeof(TType), out var value))
            {
                onAdded?.Invoke(value);
            }
            else
            {
                _onServiceAdded += onAdded;
            }
        }

        public void UnRegisterForServiceChange(Action<IService> onAdded)
        {
            _onServiceAdded -= onAdded;
        }
    }
}