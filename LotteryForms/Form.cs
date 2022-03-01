using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lottery;
using Lottery.Util;

namespace LotteryForms
{
    public partial class Form : System.Windows.Forms.Form
    {
        private static readonly int LOWER = 1;

        private static readonly int UPPER = 35;

        private static readonly IRandomGenerator rand = RandomGenerator.Get(LOWER, UPPER);

        private static readonly System.Globalization.NumberStyles style 
            = System.Globalization.NumberStyles.Integer;

        private readonly TextBox[] ticketBoxes;

        public Form()
        {
            InitializeComponent();
            this.ticketBoxes = new TextBox[7]
            { 
                TicketOne,
                TicketTwo,
                TicketThree,
                TicketFour,
                TicketFive,
                TicketSix,
                TicketSeven
            };
        }

        private async void ButtonDraw_Click(object sender, EventArgs e)
        {
            ISet<int> tickets = new HashSet<int>(7);
            string text;
            int value;
            
            // iterate each ticket text-box 
            foreach (TextBox box in ticketBoxes)
            {
                text = box.Text;
                try
                {
                    value = int.Parse(text, style);
                    // value is not within the declared range
                    if (value < LOWER || value > UPPER)
                    {
                        throw new FormatException(
                                string.Format(
                                        "Ticket must be within: {0} <= n <= {1}.", LOWER, UPPER
                                    ));
                    }
                    // value has already been specified
                    if (!tickets.Add(value))
                    {
                        throw new FormatException(
                                string.Format(
                                        "Multiple tickets exist with number: {0}.", value
                                     ));
                    }
                }
                catch (FormatException ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }

            // parse the draw text-box
            text = DrawsBox.Text;
            try
            {
                int min = 1;
                int max = 1000000; // 1 * 10^6
                value = int.Parse(text, style);
                // value is not within the declared range
                if (value < min || value > max)
                {
                    throw new FormatException(
                            string.Format(
                                "Number of draws must be within: {0} <= n <= {1}.", min, max
                                ));
                }
                // produce the automated tickets and compute the result
                var wins = await Task.Run<IDictionary<int, int>>(() =>
                {
                    ICollection<ISet<int>> reference = TicketMaster.DrawMany(rand, value);
                    IDictionary<int, int> wins = TicketMaster.Match(tickets, reference);
                    return wins;
                });
                // display the result
                SevenWinsBox.Text = wins[7].ToString();
                SixWinsBox.Text = wins[6].ToString();
                FiveWinsBox.Text = wins[5].ToString();
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
