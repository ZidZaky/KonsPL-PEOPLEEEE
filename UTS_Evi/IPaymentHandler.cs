using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTS_PEOPLEEEE
{
    public interface IPaymentHandler<TCurrency> where TCurrency : Currency
    {
        bool Pay(string currencyCode, double amountInBaseCurrency);
    }
}
