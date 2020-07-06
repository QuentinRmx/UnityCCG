using System;

namespace Engine.Cards
{
    [Serializable]
    public struct CardInfo
    {
        public int InstanceId;
        public string Name;
        public int Health;
        public int Attack;
        public int MaxHealth;
        public int Identifier;
        public string Text;
    }
}