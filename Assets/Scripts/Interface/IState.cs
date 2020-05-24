using System;
using UnityEngine;

namespace Assets.Scripts.Interface
{
    /// <summary>
    /// Интерфейс всех состояний
    /// </summary>
    public interface IState
    {
        void OnUpdate();

        void OnStart();

        void OnDispose();
    }
}
