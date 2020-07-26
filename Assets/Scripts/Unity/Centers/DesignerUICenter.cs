using System.Collections.Generic;
using System.Linq;
using Engine.Bridges;
using Engine.Cards;
using Engine.Cards.CardEffects;
using Unity.GameObjects;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

namespace Unity.Centers
{
    public class DesignerUICenter : UICenter
    {
        public InputField InputIdentifier;

        public InputField InputName;

        public Dropdown DropdownEffectSelection;

        public HandCardGameObject CurrentCardGo;

        public Button ButtonSaveData;

        public Dropdown DropdownCardSelection;

        public Button ButtonAddCard;

        public Card CurrentCard;

        public List<Card> CardCollection;

        public ResourceCenter ResourceCenter;

        // Start is called before the first frame update
        void Start()
        {
            if (ButtonSaveData != null)
            {
                ButtonSaveData.onClick.AddListener(ButtonSaveOnClick);
            }

            if (DropdownCardSelection != null)
            {
                DropdownCardSelection.onValueChanged.AddListener(DropdownCardSelectionChanged);
            }

            if (ButtonAddCard != null)
            {
                ButtonAddCard.onClick.AddListener(ButtonAddCardOnClick);
            }
        }

        private void ButtonAddCardOnClick()
        {
            Card card = new Card(new CardEffectIdle())
            {
                CardInfo = new CardInfo()
                {
                    Name = "",
                    Identifier = CardCollection.Max(c => c.CardInfo.Identifier) + 1,
                    CardCategory = ECardCategory.Player
                }
            };
            // TODO: Add artwork
            CardCollection.Add(card);
            DropdownCardSelection.options.Add(
                new Dropdown.OptionData(
                    $"{card.CardInfo.Identifier.ToString().PadLeft(3, '0')}: {card.CardInfo.Name}"));
            DropdownCardSelection.SetValueWithoutNotify(CardCollection.IndexOf(card));
        }

        private void DropdownCardSelectionChanged(int arg0)
        {
            SetCurrentCard(CardCollection[arg0]);
        }

        private void SetCurrentCard(Card card)
        {
            if (card == null) return;

            CurrentCard = card;
            CurrentCardGo.SetCardData(CurrentCard.CardInfo);
            CurrentCardGo.LoadArtwork(ResourceCenter.CardArtworks[CurrentCard.CardInfo.Identifier]);
            CardInfo info = CurrentCard.CardInfo;
            InputIdentifier.text = info.Identifier.ToString().PadLeft(3, '0');
            InputName.text = info.Name;
        }

        private void ButtonSaveOnClick()
        {
            _bridge.OverrideJsonCardData(CardCollection);
        }

        // Update is called once per frame
        void Update()
        {
        }

        public new void SetBridge(IBridge bridge)
        {
            _bridge = bridge;
            CardCollection = _bridge.GetAllCardsFromJson().Where(c => c.CardInfo.CardCategory == ECardCategory.Player)
                .ToList();
            // TODO: Set dropdown
            DropdownCardSelection.options.Clear();
            List<Dropdown.OptionData> dropdownOptions = new List<Dropdown.OptionData>();
            foreach (Card card in CardCollection)
            {
                dropdownOptions.Add(
                    new Dropdown.OptionData(
                        $"{card.CardInfo.Identifier.ToString().PadLeft(3, '0')}: {card.CardInfo.Name}"));
            }

            DropdownCardSelection.AddOptions(dropdownOptions);
            DropdownCardSelection.RefreshShownValue();
            SetCurrentCard(CardCollection[0]);
        }
    }
}