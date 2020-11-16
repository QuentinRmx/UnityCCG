using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Engine.Cards
{
    [Serializable]
    public struct CardInfo
    {
        public int InstanceId;
        public string Name;
        public int MaxHealth;
        public int Health;
        public int Attack;
        public int Identifier;
        public string Text;
        public int CardEffectAssociated;
        public int HealAmount;
        [JsonConverter(typeof(StringEnumConverter))]
        public ECardCategory CardCategory;
    }
}