using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab3
{
    abstract class Payment
    {
        public virtual void BeginTransaction(float Price){}
        public virtual bool EndTransaction() { return true; }
        public virtual void CancelTransaction() { }
    }

    class CashPayment : Payment
    {
        IKEAMyntAtare2000 coin;

        public CashPayment()
        {
            coin = new IKEAMyntAtare2000();
        }

        public override void BeginTransaction(float price)
        {
            coin.starta();
            coin.betala((int)Math.Round(price * 100));
        }

        public override bool EndTransaction()
        {
            coin.stoppa();
            return true;
        }

        public override void CancelTransaction()
        {
            coin.stoppa();
        }
    }

    class CreditPayment : Payment
    {
        CreditCard c;
        int id;

        public CreditPayment()
        {
            c = new CreditCard();
        }

        public override void BeginTransaction(float price)
        {
            c.Connect();
            //50 cent extra charge for creditcards
            id = c.BeginTransaction(price + 0.50f);
        }

        public override bool EndTransaction()
        {
            return c.EndTransaction(id);
        }

        public override void CancelTransaction()
        {
            c.CancelTransaction(id);
        }
    }

    class DebitPayment : Payment
    {
        DebitCard d;
        int id;

        public DebitPayment()
        {
            d = new DebitCard();
        }

        public override void BeginTransaction(float price)
        {
            d.Connect();
            id = d.BeginTransaction(price);
        }

        public override bool EndTransaction()
        {
            return d.EndTransaction(id);
        }

        public override void CancelTransaction()
        {
            d.CancelTransaction(id);
        }
    }
}
