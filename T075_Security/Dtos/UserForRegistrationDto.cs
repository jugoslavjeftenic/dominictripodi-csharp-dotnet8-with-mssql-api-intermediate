﻿namespace T075_Security.Dtos
{
	public class UserForRegistrationDto
	{
		public string Email { get; set; } = "";
		public string Password { get; set; } = "";
		public string PasswordConfirm { get; set; } = "";
	}
}
