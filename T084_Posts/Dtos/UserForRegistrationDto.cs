﻿namespace T084_Posts.Dtos
{
	public class UserForRegistrationDto
	{
		public string FirstName { get; set; } = "";
		public string LastName { get; set; } = "";
		public string Email { get; set; } = "";
		public string Gender { get; set; } = "";
		public string Password { get; set; } = "";
		public string PasswordConfirm { get; set; } = "";
	}
}
