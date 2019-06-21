using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoanMgntAPI.Helper
{
    public class EnumList
    {
        public enum Roles
        {
            Admin = 1,
            Agent = 2,
            Customer = 3,
        }

        public enum EmailTemplateType
        {
            AgentRegistration=1,
            UserRegistration=2,
            ForgotPassword=3
        }

        public enum ResponseType
        {
            Success = 1,
            Isinactive = 2,
            Error = 3
        }

        public enum PaymentType
        {
            cash = 1,
            cheque = 2
        }
    }
}
