using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab3
{
    class Ticket
    {
        public string departure, destination;
        public float price;
        public UIClass ticketType;
        public UIDiscount discount;
        public bool singleFare;

        public Ticket(string dep, string dest, UIClass type, UIDiscount disc, bool single)
        {
            departure = dep;
            destination = dest;
            ticketType = type;
            discount = disc;
            singleFare = single;
            price = PriceCalculator.Calculate(departure, destination, ticketType, discount, singleFare);
        }
    }

    public static class PriceCalculator
    {
        public static float Calculate(string departure, string destination, UIClass type, UIDiscount discount, bool singleFare)
        {
            float ret = 0;
            int tariefeenheden = Tariefeenheden.getTariefeenheden(departure, destination);
            switch (type)
            {
                case UIClass.FirstClass:
                    ret = FirstClassCalculator.Calculate(tariefeenheden, discount);
                    break;
                case UIClass.SecondClass:
                    ret = SecondClassCalculator.Calculate(tariefeenheden, discount);
                    break;
            }

            if (singleFare) { return ret; }
            else { return ret * 2; }
        }
    }

    public static class FirstClassCalculator
    {
        public static float Calculate(int tariefeenheden, UIDiscount discount)
        {
            int col;
            switch (discount)
            {
                default:
                case UIDiscount.NoDiscount:
                    col = 3;
                    break;
                case UIDiscount.TwentyDiscount:
                    col = 4;
                    break;
                case UIDiscount.FortyDiscount:
                    col = 5;
                    break;
            }

            return PricingTable.getPrice(tariefeenheden, col);
        }
    }

    public static class SecondClassCalculator
    {
        public static float Calculate(int tariefeenheden, UIDiscount discount)
        {
            int col;
            switch (discount)
            {
                default:
                case UIDiscount.NoDiscount:
                    col = 0;
                    break;
                case UIDiscount.TwentyDiscount:
                    col = 1;
                    break;
                case UIDiscount.FortyDiscount:
                    col = 2;
                    break;
            }

            return PricingTable.getPrice(tariefeenheden, col);
        }
    }
}
