namespace T084_Posts.Dtos
{
	public class UserForLoginConfirmationDto
	{
		public byte[] PasswordHash { get; set; } = [];
		public byte[] PasswordSalt { get; set; } = [];
	}
}
