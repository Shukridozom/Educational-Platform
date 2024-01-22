using System.ComponentModel.DataAnnotations;

namespace Project.Core.Dtos
{
    public class PaginationDto
    {
        [Range(1, 50)]
        public int PageIndex { get; set; }
        [Range(1, 50)]
        public int PageLength { get; set; }

        public PaginationDto()
        {
            PageIndex = 1;
            PageLength = 10;
        }
    }
}
