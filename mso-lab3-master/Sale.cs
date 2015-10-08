using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Lab3
{
    class Sale
    {
        private string date = "9-10-2015";
        private Ticket t;

        public void PrintReceipt(string date, float price)
        {
            MessageBox.Show("Date: " + date + " Price: " + price);
        }

        public void PrintTicket()
        {
            MessageBox.Show("From: " + t.departure + " To: " + t.destination + " Class: " + t.ticketType);
        }

        public void CreateTicket(string dep, string dest, UIClass type, UIDiscount disc, bool single)
        {
            t = new Ticket(dep, dest, type, disc, single);
        }

        public void WriteToLog()
        {
            MessageBox.Show("Transaction written to Log.");
        }

        public void ProcessPayment(UIInfo info)
        {
            Payment p;
            float tprice = t.price;

            switch (info.Payment)
            {
                default:
                case UIPayment.Cash:
                    p = new CashPayment();
                    break;
                case UIPayment.CreditCard:
                    p = new CreditPayment();
                    tprice += 0.5f;
                    break;
                case UIPayment.DebitCard:
                    p = new DebitPayment();
                    break;
            }

            p.BeginTransaction(t.price);
            bool b = p.EndTransaction();

            if (b) { MessageBox.Show("Your payment has been processed succesfully."); }
            else { MessageBox.Show("Your payment was not processed."); }
            WriteToLog();
            PrintReceipt(date, tprice);
        }
    }
}
