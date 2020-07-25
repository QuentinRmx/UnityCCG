using System.Collections.Generic;
using System.IO;
using System.Linq;
using Engine.Cards.Targets;
using Engine.Utils;
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
            
            LoadJsonData();
        }

        private void LoadJsonData()
        {
            string json = File.ReadAllText("Assets/Resources/Data/effectsData.json");
            AbstractCardEffect[] data = JsonConvert.DeserializeObject<AbstractCardEffect[]>(json, Configuration.JsonSettings);

            _cardEffects = new List<AbstractCardEffect>(data);
        }


        public AbstractCardEffect Create(int effectIdentifier)
        {
//            return null;
            return effectIdentifier >= _cardEffects.Count ? null : _cardEffects[effectIdentifier];
//            AbstractCardEffect effect = null;
//            switch (effectIdentifier)
//            {
//                case 0:
//                    effect = new CardEffectIdle(effectIdentifier)
//                    {
//                        Description = "Idle"
//                    };
//                    break;
//                case 1:
//                    effect = new CardEffectPool(effectIdentifier, new[] {Create(0), Create(2), Create(6), Create(7)});
//                    break;
//                case 2:
//                    effect = new CardEffectAttack(effectIdentifier, ETargetSelector.Player);
//                    break;
//                case 3:
//                    effect = new CardEffectAttack(effectIdentifier, ETargetSelector.AllEnemy);
//                    break;
//                case 4:
//                    effect = new CardEffectAttack(effectIdentifier, ETargetSelector.RandomEnemy);
//                    break;
//                case 5:
//                    effect = new CardEffectHeal(effectIdentifier, ETargetSelector.Player);
//                    break;
//                case 6:
//                    effect = new CardEffectHeal(effectIdentifier, ETargetSelector.SelfEnemy);
//                    break;
//                case 7:
//                    effect = new CardEffectCriticalStrike(effectIdentifier, ETargetSelector.Player);
//                    break;
//            }

//            return effect;
        }

        public void Serialize()
        {
            List<AbstractCardEffect> effects = new List<AbstractCardEffect>
            {
                Instance.Create(0), Instance.Create(1), Instance.Create(2), Instance.Create(3), Instance.Create(4),
                Instance.Create(5), Instance.Create(6), Instance.Create(7)
            };
            string json = JsonConvert.SerializeObject(effects.ToArray(), Configuration.JsonSettings);
            Debug.Log(json);
        }
    }
}