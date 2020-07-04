using System;

namespace Engine.Cards.CardEffects
{
    [Serializable]
    public struct CardInfo
    {
        public int InstanceId;
        public string Name;
        public int Health;
        public int Attack;
        public int MaxHealth;
    }
}