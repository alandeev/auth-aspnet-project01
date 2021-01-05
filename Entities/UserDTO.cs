using System.ComponentModel.DataAnnotations;

namespace backend_aspnet_crud.Entities.UserDTO
{
    public class UserConnectDTO {
        
        [Required(ErrorMessage="This field is required")]
        [MinLength(4, ErrorMessage="This field must be at least 4 characters")]
        [MaxLength(12, ErrorMessage="This field must have a maximum of 12 characters")]
        public string username { get; set; }

        [Required(ErrorMessage="This field is required")]
        [MinLength(4, ErrorMessage="This field must be at least 4 characters")]
        [MaxLength(12, ErrorMessage="This field must have a maximum of 12 characters")]
        public string password { get; set; }
    }

    public class UserRegisterDTO {
        [Required(ErrorMessage="This field is required")]
        [MinLength(4, ErrorMessage="This field must be at least 4 characters")]
        [MaxLength(12, ErrorMessage="This field must have a maximum of 12 characters")]
        public string name { get; set; }

        [Required(ErrorMessage="This field is required")]
        [MinLength(4, ErrorMessage="This field must be at least 4 characters")]
        [MaxLength(12, ErrorMessage="This field must have a maximum of 12 characters")]
        public string username { get; set; }

        [Required(ErrorMessage="This field is required")]
        [MinLength(6, ErrorMessage="This field must be at least 6 characters")]
        [MaxLength(20, ErrorMessage="This field must have a maximum of 20 characters")]
        public string password { get; set; }

        [Required(ErrorMessage="This field is required")]
        [MinLength(4, ErrorMessage="This field must be at least 4 characters")]
        [MaxLength(12, ErrorMessage="This field must have a maximum of 12 characters")]
        public string role { get; set; }
    }

    public class UserFindByUsernameDTO {
        [Required(ErrorMessage="This field is required")]
        [MinLength(4, ErrorMessage="This field must be at least 4 characters")]
        [MaxLength(12, ErrorMessage="This field must have a maximum of 12 characters")]
        public string username { get; set; }

    }
}