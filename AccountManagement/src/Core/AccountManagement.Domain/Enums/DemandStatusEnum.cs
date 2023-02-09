using System.ComponentModel.DataAnnotations;

namespace AccountManagement.Domain.Enums
{
    public enum DemandStatusEnum : int
    {
        [Display(Name = "در حال انجام تراکنش")]
        DoingTransaction = 1,

        [Display(Name = "تراکنش انجام شده")]
        DoneTransaction = 2,

        [Display(Name = "سپرده ندارد")]
        NotExistAccount = 3,

        [Display(Name = "حسابی با نوع موردنظر یافت نشد")]
        NoMatchedAccount = 4,

        [Display(Name = "اطلاعات ورودی اشتباه است")]
        InputIncorrect = 5,

        [Display(Name = "شماره مشتری معتبر نیست")]
        CustomerNumberInValid = 6,

        [Display(Name = "مقداری برای پارامتر ورودی اجباری ارسال نشده است")]
        NoFillMandatoryInput = 7,

        [Display(Name = "سرویس در دسترس نمی باشد")]
        NotAvailable = 8,
    }
}
