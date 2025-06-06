using System.ComponentModel.DataAnnotations;

namespace RealWorldApp.Client.Enums
{
    public enum BadgeColor
    {
        [Display(Name = "badge bg-primary")]
        Primary,

        [Display(Name = "badge bg-secondary")]
        Secondary,

        [Display(Name = "badge bg-success")]
        Success,

        [Display(Name = "badge bg-danger")]
        Danger,

        [Display(Name = "badge bg-warning text-dark")]
        Warning,

        [Display(Name = "badge bg-info")]
        Info,

        [Display(Name = "badge bg-light text-dark")]
        Light,

        [Display(Name = "badge bg-dark")]
        Dark
    }
}
