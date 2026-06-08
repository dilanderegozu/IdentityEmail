namespace IdentityEmail.Dtos
{
    public class LoginUserDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsPersistent { get; set; }
    }
}
