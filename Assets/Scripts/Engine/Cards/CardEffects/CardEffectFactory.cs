using System.Collections.Generic;
using System.IO;
using System.Linq;
using Engine.Cards.Targets;
using Newtonsoft.Json;
using UnityEngine;

namespace Engine.Cards.CardEffects
{
    public class CardEffectFactory
    {
        private static CardEffectFactory _instance;

        private List<AbstractCardEffect> _cardEffects;

        public static CardEffectFactory Instance => _instance = _instance ?? new CardEffectFactory();

        private CardEffectFactory()
        {
            
//            LoadJsonData();
        }

        private void LoadJsonData()
        {
            string json = File.ReadAllText("Assets/Resources/Data/effectsData.json");
            List<AbstractCardEffect> data = JsonConvert.DeserializeObject<List<AbstractCardEffect>>(json);

            _cardEffects = data;
        }


        public AbstractCardEffect Create(int effectIdentifier)
        {
//            return null;
//            return effectIdentifier >= _cardEffects.Count ? null : _cardEffects[effectIdentifier];
            AbstractCardEffect effect = null;
            switch (effectIdentifier)
            {
                case 0:
                    effect = new CardEffectIdle(1)
                    {
                        Description = "Idle"
                    };
                    break;
                case 1:
                    effect = new CardEffectPool(0, new[] {Create(0), Create(2)});
                    break;
                case 2:
                    effect = new CardEffectAttack(2, ETargetSelector.Player){
                        Description = "Idle"
                    };
                    break;
                case 3:
                    effect = new CardEffectAttack(3, ETargetSelector.AllEnemy);
                    break;
                case 4:
                    effect = new CardEffectAttack(4, ETargetSelector.RandomEnemy);
                    break;
            }

            return effect;
        }

        public void Serialize()
        {
            List<AbstractCardEffect> effects = new List<AbstractCardEffect>
            {
                Instance.Create(0), Instance.Create(1), Instance.Create(2), Instance.Create(3)
            };
            string json = JsonConvert.SerializeObject(effects.ToArray(), Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });
            Debug.Log(json);
        }
    }
}