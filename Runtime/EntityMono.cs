using System;
using Dories.Componentization.Runtime.Utils;
using UnityEngine;

namespace Dories.Componentization.Runtime
{
    public class EntityMono : MonoBehaviour, IDisposable
    {
        //Entity实体
        private Entity m_Entity;
        
        private Entity Entity => m_Entity ??= ComponentFactory.Acquire<Entity>();

        protected virtual void OnDestroy()
        {
            if (m_Entity != null)
            {
                ComponentFactory.Release(m_Entity);
            }
        }
        
        /// <summary>
        /// 添加子组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T AddComponent<T>(object userData = null) where T : Component
        {
            return Entity.AddComponent<T>(userData);
        }

        /// <summary>
        /// 添加子组件
        /// </summary>
        /// <param name="component"></param>
        /// <exception cref="Exception"></exception>
        public void AddComponent(Component component)
        {
            Entity.AddComponent(component);
        }
        
        /// <summary>
        /// 移除子组件
        /// </summary>
        /// <param name="component"></param>
        /// <param name="isRelease"></param>
        /// <exception cref="Exception"></exception>
        public void RemoveComponent(Component component, bool isRelease = true)
        {
            Entity.RemoveComponent(component, isRelease);
        }

        /// <summary>
        /// 获取指定类型的第一个子组件
        /// </summary>
        /// <param name="componentType"></param>
        /// <returns></returns>
        public Component GetComponentCSharp(Type componentType)
        {
            return Entity.GetComponent(componentType);
        }

        /// <summary>
        /// 获取指定类型的第index个子组件
        /// </summary>
        /// <param name="componentType"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public Component GetComponent(Type componentType, int index)
        {
            return Entity.GetComponent(componentType, index);
        }

        /// <summary>
        /// 获取指定类型的第一个子组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetComponentCSharp<T>() where T : Component
        {
            return Entity.GetComponent<T>();
        }

        /// <summary>
        /// 获取指定类型的第index个子组件
        /// </summary>
        /// <param name="index"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetComponent<T>(int index) where T : Component
        {
            return Entity.GetComponent<T>(index);
        }

        public void Dispose()
        {
            m_Entity?.Dispose();
        }
    }
}