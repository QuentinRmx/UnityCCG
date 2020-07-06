using System;
using Engine.Cards;
using Engine.Cards.CardEffects;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.GameObjects
{
    public abstract class CardGameObject : MonoBehaviour
    {
        // PUBLIC ATTRIBUTES
        public int InstanceId;

        public Image ArtworkImage;

        public Text TextName;


        // METHODS

        protected void SetCardData(CardInfo infos)
        {
            InstanceId = infos.InstanceId;
            TextName.text = infos.Name;
        }

        public void LoadArtwork(Sprite artworkSprite)
        {
            ArtworkImage.sprite = artworkSprite;
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