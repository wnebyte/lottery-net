using Lottery.Util;
using System;

namespace Lottery
{
    public class Ticket : IEquatable<Ticket>, IComparable<Ticket>, IComparable<int>
    {
        public int Number { get; }

        public Ticket()
        {
            var rand = RandomGenerator.Get(1, 35);
            this.Number = rand.Generate();
        }

        public bool Equals(Ticket ticket)
        {
            if (ticket == null)
                return false;
            if (ReferenceEquals(this, ticket))
                return true;
            if (this.GetType() != ticket.GetType())
                return false;
            return this.Number == ticket.Number;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (this.GetType() != obj.GetType())
                return false;
            Ticket ticket = (Ticket)obj;
            return this.Number == ticket.Number;
        }

        public override int GetHashCode()
        {
            return this.Number;
        }

        public override string ToString()
        {
            return string.Format(
                    "Lottery.Ticket[Number: {0}]", Number
                );
        }

        public int CompareTo(int other)
        {
            return this.Number - other;
        }

        public int CompareTo(Ticket other)
        {
            return this.Number - other.Number;
        }
    }
}
