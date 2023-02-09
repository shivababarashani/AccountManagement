using System.ComponentModel.DataAnnotations;

namespace AccountManagement.Framework.Common
{
    public enum TransactionType
    {
        [Display(Name = "جزئیات مشتری")]
        GetCustomerInfo = 1,

        [Display(Name = "لیست حساب های مشتری بر اساس کد ملی")]
        GetCustomerDepositslist = 2,
        
        [Display(Name = "جزئیات حساب")]
        GetAccountInfo = 3,

        [Display(Name = "بلاک واریز")]
        BlockDeposit = 4,

        [Display(Name = "بلاک برداشت")]
        BlockWithdraw = 5,

    }
}
