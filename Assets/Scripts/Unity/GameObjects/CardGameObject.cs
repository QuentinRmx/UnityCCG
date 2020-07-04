using System;
using Engine.Bridges;
using Engine.Cards;
using Engine.Cards.CardEffects;
using States;
using Unity.Components;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.GameObjects
{
    public abstract class CardGameObject : MonoBehaviour
    {
        // PUBLIC ATTRIBUTES
        public int InstanceId;

        public Image ArtworkImage;


        // METHODS

        public void SetCardData(CardInfo infos)
        {
            InstanceId = infos.InstanceId;
        }

        /// <summary>
        /// Called by the Card object when something is updated and the CardGameObject associated needs to be associated
        /// (typically health changes or status).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void DataChangedCardData(object sender, EventArgs args)
        {
            // TODO: Pass raw data so we don't use an engine class here.
            if (sender is Card c)
            {
                SetCardData(c.CardInfo);
            }
        }
    }
}