namespace ApplicationLayer.DTOs.Auth.Login
{
    public class LoginResponseDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public EnumRoleUser Role { get; set; }
        public bool Flag { get; set; }
        public string Message { get; set; }
    }
}
