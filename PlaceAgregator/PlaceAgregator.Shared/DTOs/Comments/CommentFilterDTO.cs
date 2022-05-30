using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceAgregator.Shared.DTOs.Comments
{
    public class CommentFilterDTO : BaseFilterDTO
    {
        public bool? IsBlocked { get; set; }
        public string? UserName { get; set; }
        public string? UserId { get; set; }
        public int? PlaceId { get; set; }
    }
}
