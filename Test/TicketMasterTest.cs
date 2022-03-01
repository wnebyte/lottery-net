using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lottery;
using Lottery.Util;

namespace Test
{
    [TestClass]
    public class TicketMasterTest
    {
        [TestInitialize]
        public void Setup()
        {
      
        }

        [TestMethod]
        public void Test00()
        {
            ISet<Ticket> tickets = TicketMaster.Draw();
            foreach (Ticket ticket in tickets)
            {
                Console.WriteLine(ticket);
            }
        }

        [TestMethod]
        public void Test01()
        {
            int number = 15;
            ICollection<ISet<Ticket>> list = TicketMaster.DrawMany(number);
            Assert.AreEqual(number, list.Count);
        }

        // 1.8 sec
        [TestMethod]
        public void Test02()
        {
            IDictionary<int, int> counter = new Dictionary<int, int>()
            {
                { 0, 0 },
                { 1, 0 },
                { 2, 0 },
                { 3, 0 },
                { 4, 0 },
                { 5, 0 },
                { 6, 0 },
                { 7, 0 },
            };
            ISet<Ticket> usr = TicketMaster.Draw();
            ICollection<ISet<Ticket>> list = TicketMaster.DrawMany(1000000);

            foreach (ISet<Ticket> set in list)
            {
                // range: (0, 7)
                int index = usr.Intersections(set);
                counter[index]++;
            }

            foreach (KeyValuePair<int, int> kv in counter)
            {
                Console.WriteLine(
                        "{0} : {1}", kv.Key, kv.Value
                    );
            }
        }
    }
}
