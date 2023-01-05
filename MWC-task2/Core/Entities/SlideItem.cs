
using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public class SlideItem
{
    public int Id { get; set; }
    [Required]
    public string? SubTitle { get; set; }
    [Required(ErrorMessage ="bos buraxmayin"),MaxLength(250)]
    public string? Title { get; set; }
    [Required,MaxLength(150)]
    public string? Desc { get; set; }
    public string? Button { get; set; }
}
