namespace ApplicationLayer.DTOs.Auth.Login
{
    public class LoginResponseDto
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public EnumRoleUser Role { get; set; } = EnumRoleUser.None;
        public bool IsSuccessful { get; set; } = false;
        public List<string> Message { get; set; } = new List<string>();
    }
}
