using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dories.Componentization.Runtime.Utils
{
    public static partial class ComponentFactory
    {
        private static Dictionary<Type, ISingleton> m_SingletonComponents = new Dictionary<Type, ISingleton>();

        public static T RegisterSingletonComponent<T>(T singleton = null) where T : Component, ISingleton
        {
            return RegisterSingletonComponent(typeof(T), singleton) as T;
        }

        public static Component RegisterSingletonComponent(Type type, ISingleton singleton = null)
        {
            if (!typeof(ISingleton).IsAssignableFrom(type))
            {
                Debug.LogError($"Singleton component {type} not registered");
                return null;
            }

            if (singleton != null)
            {
                if (!singleton.GetType().IsAssignableFrom(type))
                {
                    Debug.LogError($"Singleton component {type} not registered");
                    return null;
                }

                if (!m_SingletonComponents.TryAdd(type, singleton))
                {
                    Debug.LogError($"Singleton component {type} already registered");
                }
            }
            else
            {
                if (typeof(ISingleton).IsAssignableFrom(type))
                {
                    singleton = Acquire(type) as ISingleton;
                }

            }


            return singleton as Component;
        }

        public static void UnregisterSingletonComponent<T>() where T : ISingleton
        {
            if (!m_SingletonComponents.ContainsKey(typeof(T)))
            {
                Debug.LogError($"Singleton component {typeof(T)} not registered");
                return;
            }

            m_SingletonComponents.Remove(typeof(T));
        }

        public static bool HasSingletonComponent<T>() where T : ISingleton
        {
            return m_SingletonComponents.ContainsKey(typeof(T));
        }

        public static bool HasSingletonComponent(Type type)
        {
            if (!typeof(ISingleton).IsAssignableFrom(type))
            {
                Debug.LogError($"Singleton component {type} not registered");
                return false;
            }

            ;
            return m_SingletonComponents.ContainsKey(type);
        }

        public static T GetSingletonComponent<T>() where T : ISingleton
        {
            if (!m_SingletonComponents.TryGetValue(typeof(T), out var singleton))
            {
                Debug.LogError($"Singleton component {typeof(T)} not registered");
                return default(T);
            }

            return (T)singleton;
        }

        public static Component GetSingletonComponent(Type type)
        {
            if (!typeof(ISingleton).IsAssignableFrom(type))
            {
                Debug.LogError($"Singleton component {type} not registered");
                return null;
            }

            if (!m_SingletonComponents.TryGetValue(type, out var singleton))
            {
                Debug.LogError($"Singleton component {type} not registered");
                return null;
            }

            return singleton as Component;
        }

        public static T GetOrAddSingletonComponent<T>() where T : Component, ISingleton
        {
            if (!HasSingletonComponent(typeof(T)))
            {
                return RegisterSingletonComponent<T>();
            }
            else
            {
                return GetSingletonComponent<T>();
            }
        }
    }
}