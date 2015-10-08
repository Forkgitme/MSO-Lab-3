using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab3
{
    class Ticket
    {
        public bool dueToday;
        public string departure, destination;
        public float price;
        public UIClass ticketType;
        public UIDiscount discount;

        public Ticket()
        {

        }

        public float CalculatePrice(string departure, string destination, UIClass type, UIDiscount discount)
        {
            //We have no reason to extend CalculatePrice to the pricecalculator class as Ticket is already the information expert
            return PriceCalculator.Calculate(departure, destination, type, discount);
        }
    }

    public static class PriceCalculator
    {
        public static float Calculate(string departure, string destination, UIClass type, UIDiscount discount)
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
            return ret;
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
                    col = 4;
                    break;
                case UIDiscount.TwentyDiscount:
                    col = 5;
                    break;
                case UIDiscount.FortyDiscount:
                    col = 6;
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
                    col = 1;
                    break;
                case UIDiscount.TwentyDiscount:
                    col = 2;
                    break;
                case UIDiscount.FortyDiscount:
                    col = 3;
                    break;
            }

            return PricingTable.getPrice(tariefeenheden, col);
        }
    }
}
