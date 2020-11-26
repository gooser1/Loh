using System;
using System.Collections.Generic;

namespace Loh.Backend.Model
{
    public class Player
    {
        public string Name { get; private set; }
        public Guid UserId { get; }

        public List<Card> Hand;
        public bool IsFinished = false;

        public Player(string name)
        {
            Name = name;
            UserId = Guid.NewGuid();
        }

        public void Rename(string newName)
        {
            Name = newName;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
