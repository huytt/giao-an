using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTelecom.Domain.Core.ExClass
{
    public class WebAppConstant
    {
        #region Custom order form
        public const string COF_ONLINE_PAY = "COF-1";
        public const string COF_PRE_PAY = "COF-2";
        public const string COF_POST_PAY = "COF-3";
        #endregion

        #region Status Direction
        public const string SDC_REQUEST = "SDC1";
        public const string SDC_ACCEPT = "SDC2";
        public const string SDC_REJECT = "SDC3";
        public const string SDC_HOLD = "SDC4";
        public const string SDC_CANCEL = "SDC5";
        public const string SDC_ASSIGN = "SDC6";
        public const string SDC_SUBMIT = "SDC7";
        public const string SDC_COMPLETE = "SDC8";
        public const string SDC_RETURN = "SDC9";
        #endregion

        public static readonly List<long?> SALE_DEPARTMENT = new List<long?> { 1, 2, 3 };
        public static readonly List<long?> ACCOUNTANT_DEPARTMENT = new List<long?> { 9, 10 };

        #region Deparment
        //JQCOF-1-D500
        //JQCOF-2-D500
        //JQCOF-3-D500
        public const string DP_DESIGN = "DS";
        public const string DP_SALE_GALAGALA = "SG";
        public const string GROUP_SALE_GALAGALA_MANAGER = "JQCOF-1-D500";
        public const string GROUP_SALE_GALAGALA_SALE_CALL = "JQCOF-2-D500";
        public const string GROUP_SALE_GALAGALA_SALE_EXCEPTION = "JQCOF-3-D500";

        public const string GROUP_SALE_GALAGALA_ACCOUNTANT_MANAGER = "JQCOF-1-D200";
        public const string GROUP_SALE_GALAGALA_ACCOUNTANT_STAFF = "JQCOF-2-D200";

        public const string GROUP_SALE_GALAGALA_LOGISTIC_MANAGER = "JQCOF-1-D600";
        public const string GROUP_SALE_GALAGALA_LOGISTIC_STAFF_1 = "JQCOF-2-D600";
        public const string GROUP_SALE_GALAGALA_LOGISTIC_STAFF_2 = "JQCOF-3-D600";

        public const long PERMISSION_GROUP_SALE_MANAGER = 1;
        public const long PERMISSION_GROUP_SALE_CALL = 2;
        public const long PERMISSION_GROUP_SALE_EXCEPTION = 3;

        public const long PERMISSION_GROUP_ACCOUNTANT_MANAGER = 9;
        public const long PERMISSION_GROUP_ACCOUNTANT_STAFF = 10;


        public const string DP_SALE_XONE = "SX";
        public const string DP_ACCOUNTING_FINANCE = "AF";
        public const string DP_LOGISTIC = "LG";
        #endregion



        #region
        public const string PAYMENT_ONLINE_PAY = "PTC-1";
        public const string PAYMENT_PRE_PAY = "PTC-2";
        public const string PAYMENT_POST_PAY = "PTC-3";


        #endregion
    }
}
