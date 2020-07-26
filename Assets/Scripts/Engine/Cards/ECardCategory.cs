using System.Runtime.Serialization;

namespace Engine.Cards
{
    public enum ECardCategory
    {
        [EnumMember(Value = "player")]
        Player,
        [EnumMember(Value = "enemy")]
        Enemy
    }
}