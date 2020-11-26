using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Loh.Backend.Extensions;
using Loh.Backend.Model.Dto;
using Loh.Backend.Model.Enums;

namespace Loh.Backend.Model
{
    public class Card
    {
        public CardRank Rank { get; }

        public CardSuit Suit { get; }
        public CardType Type { get; }

        public string Description { get; private set; }

        public Card(CardRank rank, CardSuit suit)
        {
            if (rank == CardRank.Default)
                ThrowCardCtorException("Попытка создания карты без указания старшинства!");

            if (suit == CardSuit.Default)
                ThrowCardCtorException("Попытка создания карты без указания масти!");

            Rank = rank;
            Suit = suit;
            Type = CardType.Ordinary;

            FillDescription();
        }

        public Card(CardType type)
        {
            if (type == CardType.Default || type == CardType.Ordinary)
                ThrowCardCtorException("Этот конструктор надо использовать только для необычных карт!");

            Type = type;

            FillDescription();
        }

        private void FillDescription()
        {
            if (Type.In(CardType.Ordinary, CardType.Default))
                Description = $"{Rank.GetDescription()} {Suit.GetDescription()}".CapitalizeFirstLetter();
            else
                Description = Type.GetDescription();
        }

        private void ThrowCardCtorException(string message)
        {
            throw new Exception($"Ошибка при создании карты! {message}");
        }

        public bool EqualToDto(CardDto cardDto)
        {
            return Suit == cardDto.Suit &&
                   Rank == cardDto.Rank &&
                   Type == cardDto.Type;
        }

        public override string ToString()
        {
            return Description;
        }
    }
}
