
using FluentValidation;
namespace DBConnect.Models

{
    public class Users
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }  
        public string Email { get; set; }
        public string password { get; set; }
    }
    public class UserVal : AbstractValidator<Users>
    {
        public UserVal()
        {
            RuleFor(x => x.Name).NotEmpty(); 
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("It is not correctly address email !");
            RuleFor(x => x.password).NotEmpty().MinimumLength(10).WithMessage("Password string too small !");
            RuleFor(x => x.CreatedDate).NotEmpty();
        }
    }
}
