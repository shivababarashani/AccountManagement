using System.ComponentModel.DataAnnotations;

namespace AccountManagement.Contract.Enums
{
    public enum ExecutDemandPacketStatus
    {
        [Display(Name = "موفق")]
        Successful = 0,

        [Display(Name = "ناموفق")]
        Unsuccessful = 1,

        [Display(Name = "تلاش مجدد")]
        Retry = 2,
    }

}
