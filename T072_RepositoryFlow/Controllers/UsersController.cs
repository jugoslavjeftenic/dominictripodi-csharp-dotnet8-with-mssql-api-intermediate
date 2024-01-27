using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using T072_RepositoryFlow.Dtos;
using T072_RepositoryFlow.Models;
using T072_RepositoryFlow.Repositories;

namespace T072_RepositoryFlow.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class UsersController(IUsersRepository usersRepository) : ControllerBase
	{
		private readonly IUsersRepository _usersRepository = usersRepository;
		private readonly Mapper _mapper = new(new MapperConfiguration(cfg =>
		{
			cfg.CreateMap<UserToAddDto, UserModel>();
		}));

		// UserModel -------------------------------------------
		// Create
		[HttpPost("AddUser")]
		public IActionResult AddUser(UserToAddDto user)
		{
			UserModel userToAdd = _mapper.Map<UserModel>(user);

			_usersRepository.AddEntity(userToAdd);
			if (_usersRepository.SaveChanges())
			{
				return Ok();
			}

			throw new Exception("Failed to Add User.");
		}

		// Read - All
		[HttpGet("GetUsers")]
		public IEnumerable<UserModel> GetUsers()
		{
			return _usersRepository.GetUsers();
		}

		// Read - ById
		[HttpGet("GetUser/{userId}")]
		public UserModel GetUser(int userId)
		{
			return _usersRepository.GetUser(userId);
		}

		// Update
		[HttpPut("EditUser")]
		public IActionResult EditUser(UserModel user)
		{
			UserModel userToEdit = _usersRepository.GetUser(user.UserId);

			userToEdit.FirstName = user.FirstName;
			userToEdit.LastName = user.LastName;
			userToEdit.Email = user.Email;
			userToEdit.Gender = user.Gender;
			userToEdit.Active = user.Active;

			if (_usersRepository.SaveChanges())
			{
				return Ok();
			}

			throw new Exception("Failed to Update User.");
		}

		// Delete
		[HttpDelete("DeleteUser/{userId}")]
		public IActionResult DeleteUser(int userId)
		{
			UserModel userToDelete = _usersRepository.GetUser(userId);

			_usersRepository.RemoveEntity(userToDelete);

			if (_usersRepository.SaveChanges())
			{
				return Ok();
			}

			throw new Exception("Failed to Delete User.");
		}

		// UserSalaryModel -------------------------------------
		// Create
		[HttpPost("AddUserSalary")]
		public IActionResult AddUserSalary(UserSalaryModel userSalary)
		{
			_usersRepository.AddEntity(userSalary);

			if (_usersRepository.SaveChanges())
			{
				return Ok();
			}

			throw new Exception("Failed to Add User's Salary.");
		}

		// Read - ById
		[HttpGet("GetUserSalary/{userId}")]
		public UserSalaryModel GetUserSalary(int userId)
		{
			return _usersRepository.GetUserSalary(userId);
		}

		// Update
		[HttpPut("EditUserSalary")]
		public IActionResult EditUserSalary(UserSalaryModel userSalary)
		{
			UserSalaryModel userSalaryToEdit = _usersRepository.GetUserSalary(userSalary.UserId);

			userSalaryToEdit.UserId = userSalary.UserId;
			userSalaryToEdit.Salary = userSalary.Salary;
			userSalaryToEdit.AvgSalary = userSalary.AvgSalary;

			if (_usersRepository.SaveChanges())
			{
				return Ok();
			}

			throw new Exception("Failed to Update User's Salary.");
		}

		// Delete
		[HttpDelete("DeleteUserSalary/{userId}")]
		public IActionResult DeleteUserSalary(int userId)
		{
			UserSalaryModel userSalaryToDelete = _usersRepository.GetUserSalary(userId);

			_usersRepository.RemoveEntity(userSalaryToDelete);

			if (_usersRepository.SaveChanges())
			{
				return Ok();
			}

			throw new Exception("Failed to Delete User's Salary.");
		}

		// UserJobInfoModel ------------------------------------
		// Create
		[HttpPost("AddUserJobInfo")]
		public IActionResult AddUserJobInfo(UserJobInfoModel userJobInfo)
		{
			_usersRepository.AddEntity(userJobInfo);

			if (_usersRepository.SaveChanges())
			{
				return Ok();
			}

			throw new Exception("Failed to Add User's Job Info.");
		}

		// Read - ById
		[HttpGet("GetUserJobInfo/{userId}")]
		public UserJobInfoModel GetUserJobInfo(int userId)
		{
			return _usersRepository.GetUserJobInfo(userId);
		}

		// Update
		[HttpPut("EditUserJobInfo")]
		public IActionResult EditUserJobInfo(UserJobInfoModel userJobInfo)
		{
			UserJobInfoModel userJobInfoToEdit = _usersRepository.GetUserJobInfo(userJobInfo.UserId);

			userJobInfoToEdit.UserId = userJobInfo.UserId;
			userJobInfoToEdit.JobTitle = userJobInfo.JobTitle;
			userJobInfoToEdit.Department = userJobInfo.Department;

			if (_usersRepository.SaveChanges())
			{
				return Ok();
			}

			throw new Exception("Failed to Update User's Job Info.");
		}

		// Delete
		[HttpDelete("DeleteUserJobInfo/{userId}")]
		public IActionResult DeleteUserJobInfo(int userId)
		{
			UserJobInfoModel userJobInfoToDelete = _usersRepository.GetUserJobInfo(userId);

			_usersRepository.RemoveEntity(userJobInfoToDelete);

			if (_usersRepository.SaveChanges())
			{
				return Ok();
			}

			throw new Exception("Failed to Delete User's Job Info.");
		}
	}
}
