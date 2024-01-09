using System.ComponentModel.DataAnnotations;

namespace Project.Core.Dtos
{
    public class DepositDto
    {
        public int UserId { get; set; }
        [Range(1.0, double.MaxValue)]
        public double Amount { get; set; }
    }
}
