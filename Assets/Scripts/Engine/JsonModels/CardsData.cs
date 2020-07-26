using System.Collections.Generic;
using Engine.Cards;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Engine.JsonModels
{
    [JsonObject]
    public class CardsData
    {

        // ATTRIBUTES
        
        [JsonProperty]
        public int LastIdentifier { get; set; }

        [JsonProperty]
        public List<CardInfo> CardInfos { get; set; }
        
        // CONSTRUCTORS

        // METHODS


    }
}