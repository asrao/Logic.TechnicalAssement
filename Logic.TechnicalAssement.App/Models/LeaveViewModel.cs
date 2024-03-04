using InvestmentManagement.Shared.Extensions;
using System.ComponentModel.DataAnnotations;

namespace Logic.TechnicalAssement.App.Models
{
    public class LeaveViewModel
    {
        public long Id { get; set; }
        public LeaveTypeEnum LeaveType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsHalfDay {  get; set; }

        public string GetLeaveTypeDisplayName()
        {
            return LeaveType.GetDisplayName();
        }
    }
    public enum LeaveTypeEnum
    {
        [Display(Name = "Annual Leave")]
        AnnualLeave,
        [Display(Name = "Sick Leave")]
        SickLeave,
        [Display(Name = "Compassionate Leave")]
        CompassionateLeave,
        [Display(Name = "Unpaid Leave")]
        UnpaidLeave,
    }
}
