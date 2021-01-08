using System.ComponentModel.DataAnnotations;

namespace backend_aspnet_crud.Entities.UserDTO
{
    public class PostCreateDTO {
        [Required(ErrorMessage="This field is required")]
        [MinLength(4, ErrorMessage="This field must be at least 4 characters")]
        [MaxLength(20, ErrorMessage="This field must have a maximum of 12 characters")]
        public string title { get; set; }
        
        [Required(ErrorMessage="This field is required")]
        [MinLength(4, ErrorMessage="This field must be at least 4 characters")]
        [MaxLength(20, ErrorMessage="This field must have a maximum of 12 characters")]
        public string description { get; set; }
    }
}