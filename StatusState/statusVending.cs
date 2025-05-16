using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatusState
{
    public class statusVending

    {
        public enum Status { Idle, Order, Payment, Done };
        public Status status = Status.Idle;
        public String Get_Current_Status()
        {
            return this.status.ToString();


        }
        public String set_Status_Idle()
        {
            this.status = Status.Idle;
            return "Idle";
        }

        public String set_Status_Order()
        {
            this.status = Status.Order;
            return "Order";

        }

        public String set_Status_Payment()
        {
            this.status = Status.Payment;
            return "Payment";
        }

        public String set_Status_Done()
        {
            this.status = Status.Done;
            return "Done";
        }
    }
}
