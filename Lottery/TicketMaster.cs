using System.Collections.Generic;
using System.Threading.Tasks;
using Lottery.Util;

namespace Lottery
{
    public static class TicketMaster
    {
        public static ISet<Ticket> Draw()
        {
            int count = 7;
            ISet<Ticket> tickets = new HashSet<Ticket>(count);

            while (tickets.Count < count)
            {
                Ticket ticket = new();

                while (!tickets.Add(ticket))
                {
                    ticket = new Ticket();
                }
            }

            return tickets;
        }

        public static ISet<int> Draw(IRandomGenerator rand)
        {
            int count = 7;
            ISet<int> tickets = new HashSet<int>(count);

            while (tickets.Count < count)
            {
                int ticket = rand.Generate();

                while (!tickets.Add(ticket))
                {
                    ticket = rand.Generate();
                }
            }

            return tickets;
        }

        public static ICollection<ISet<Ticket>> DrawMany(int number)
        {
            ICollection<ISet<Ticket>> set = new List<ISet<Ticket>>(number);

            for (int i = 0; i < number; i++)
            {
                set.Add(Draw());
            }

            return set;
        }

        public static ICollection<ISet<int>> DrawMany(IRandomGenerator rand, int number)
        {
            ICollection<ISet<int>> set = new List<ISet<int>>(number);

            for (int i = 0; i < number; i++)
            {
                set.Add(Draw(rand));
            }

            return set;
        }

        public static async Task<ICollection<ISet<int>>> DrawManyAsync(IRandomGenerator rand, int number)
        {
            ICollection<ISet<int>> set = new List<ISet<int>>(number);

            return await Task.Run<ICollection<ISet<int>>>(() =>
              {
                  for (int i = 0; i < number; i++)
                  {
                      set.Add(Draw(rand));
                  }
                  return set;
              });
        }

        public static IDictionary<int, int> Match(ISet<Ticket> tickets, ICollection<ISet<Ticket>> reference)
        {
            IDictionary<int, int> counter = new Dictionary<int, int>(tickets.Count);

            for (int i = 0; i <= tickets.Count; i++)
            {
                counter[i] = 0;
            }
            foreach (ISet<Ticket> set in reference)
            {
                int index = tickets.Intersections(set);
                counter[index]++;
            }

            return counter;
        }

        public static IDictionary<int, int> Match(ISet<int> tickets, ICollection<ISet<int>> reference)
        {
            IDictionary<int, int> counter = new Dictionary<int, int>(tickets.Count);

            for (int i = 0; i <= tickets.Count; i++)
            {
                counter[i] = 0;
            }
            foreach (ISet<int> set in reference)
            {
                int index = tickets.Intersections(set);
                counter[index]++;
            }

            return counter;
        }

        public static async Task<IDictionary<int, int>> MatchAsync(ISet<int> tickets, ICollection<ISet<int>> reference)
        {
            IDictionary<int, int> counter = new Dictionary<int, int>(tickets.Count);

            for (int i = 0; i <= tickets.Count; i++)
            {
                counter[i] = 0;
            }
            return await Task.Run<IDictionary<int, int>>(() =>
            {
                foreach (ISet<int> set in reference)
                {
                    int index = tickets.Intersections(set);
                    counter[index]++;
                }
                return counter;
            });
        }
    }
}
