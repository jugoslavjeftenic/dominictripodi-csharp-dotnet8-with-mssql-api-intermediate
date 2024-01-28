namespace T075_Security.Dtos
{
	public class UserForLoginConfirmationDto
	{
		public byte[] PasswordHash { get; set; } = [];
		public byte[] PasswordSalt { get; set; } = [];
	}
}
