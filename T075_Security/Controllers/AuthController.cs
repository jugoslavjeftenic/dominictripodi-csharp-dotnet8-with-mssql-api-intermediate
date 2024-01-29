using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using T075_Security.Data;
using T075_Security.Dtos;

namespace T075_Security.Controllers
{
	public class AuthController(IConfiguration config) : ControllerBase
	{

		private readonly DataContextDapper _dapper = new(config);
		private readonly IConfiguration _config = config;

		[HttpPost("Register")]
		public IActionResult Register(UserForRegistrationDto userForRegistration)
		{
			if (userForRegistration.Password == userForRegistration.PasswordConfirm)
			{
				string sqlCheckUserExists =
					$"SELECT [Email] FROM TutorialAppSchema.Auth WHERE Email = '{userForRegistration.Email}'";

				IEnumerable<string> existingUsers = _dapper.LoadData<string>(sqlCheckUserExists);

				if (!existingUsers.Any())
				{
					byte[] passwordSalt = [128 / 8];

					using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
					{
						rng.GetNonZeroBytes(passwordSalt);
					}

					byte[] passwordHash = GetPasswordHash(userForRegistration.Password, passwordSalt);

					string sqlAddAuth = @$"
					INSERT INTO TutorialAppSchema.Auth (
						[Email], 
						[PasswordHash], 
						[PasswordSalt]
					) VALUES (
						'{userForRegistration.Email}', 
						@PasswordHash, 
						@PasswordSalt
					)";

					List<SqlParameter> sqlParameters = [];

					SqlParameter passwordSaltParameter = new("@PasswordSalt", SqlDbType.VarBinary)
					{
						Value = passwordSalt
					};

					SqlParameter passwordHashParameter = new("@PasswordHash", SqlDbType.VarBinary)
					{
						Value = passwordHash
					};

					sqlParameters.Add(passwordSaltParameter);
					sqlParameters.Add(passwordHashParameter);

					if (_dapper.ExecuteSqlWithParameters(sqlAddAuth, sqlParameters))
					{
						string sqlAddUser = @$"
						INSERT INTO [TutorialAppSchema].[Users] (
							[FirstName],
							[LastName],
							[Email],
							[Gender],
							[Active]
						) VALUES (
							'{userForRegistration.FirstName}',
							'{userForRegistration.LastName}',
							'{userForRegistration.Email}',
							'{userForRegistration.Gender}',
							'1'
						)";

						if (_dapper.ExecuteSql(sqlAddUser))
						{
							return Ok();
						}

						throw new Exception("Failed to add user.");
					}

					throw new Exception("Failed to register user.");
				}

				throw new Exception("User with this email already exists!");
			}

			throw new Exception("Passwords do not match!");
		}

		[HttpPost("Login")]
		public IActionResult Login(UserForLoginDto userForLogin)
		{
			string sqlForHashAndSalt = @$"
			SELECT 
				[PasswordHash], 
				[PasswordSalt] 
			FROM TutorialAppSchema.Auth 
			WHERE Email = '{userForLogin.Email}'
			";

			UserForLoginConfirmationDto userForLoginConfirmation =
				_dapper.LoadDataSingle<UserForLoginConfirmationDto>(sqlForHashAndSalt);


			byte[] passwordHash =
				GetPasswordHash(userForLogin.Password, userForLoginConfirmation.PasswordSalt);

			for (int i = 0; i < passwordHash.Length; i++)
			{
				if (passwordHash[i] != userForLoginConfirmation.PasswordHash[i])
				{
					return StatusCode(401, "Incorrect password!");
				}
			}

			return Ok();
		}

		private byte[] GetPasswordHash(string password, byte[] passwordSalt)
		{
			string passwordSaltPlusString =
				_config.GetSection("AppSettings: PasswordKey")
				.Value + Convert.ToBase64String(passwordSalt);

			return KeyDerivation.Pbkdf2(
				password: password,
				salt: Encoding.ASCII.GetBytes(passwordSaltPlusString),
				prf: KeyDerivationPrf.HMACSHA256,
				iterationCount: 100000,
				numBytesRequested: 256 / 8
			);
		}
	}
}
