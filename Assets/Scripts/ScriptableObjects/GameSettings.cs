using Assets.Scripts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Game Settings Scriptable Object", menuName = "Game Settings", order = 1)]
    public class GameSettings : ScriptableObject
    {
        public Data data;
    }
}
