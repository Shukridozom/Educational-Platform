using System.ComponentModel.DataAnnotations;

namespace Project.Core.Dtos
{
    public class WithdrawDto
    {
        [Range(1.0,double.MaxValue)]
        public double Amount { get; set; }
    }
}
