using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeaLearning.Api.Extensions;
using PeaLearning.Api.Helpers;
using PeaLearning.Api.Requests.Users;
using PeaLearning.Api.ViewModels.User;
using PeaLearning.Application.Queries;
using PeaLearning.Common;
using PeaLearning.Domain.AggregateModels.UserAggregate;
using PeaLearning.Domain.DomainServices.Interfaces;
using PeaLearning.Domain.DomainServices.Models;

namespace PeaLearning.Api.Controllers.Admin
{
    [Authorize(Roles = Constants.Role.Administrator)]
    public class UserController : AdminV1Controller
    {
        private readonly IUserService _userService;
        private readonly IMediator _mediator;

        public UserController(IUserService userService, IMediator mediator)
        {
            _userService = userService;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] UserCreateRequest userCreateRequest)
        {
            var user = new User(userCreateRequest.UserName, userCreateRequest.FirstName, userCreateRequest.LastName,
                userCreateRequest.Email, userCreateRequest.Avatar, userCreateRequest.Gender, userCreateRequest.Address,
                userCreateRequest.IsActive);
            var result = await _userService.CreateUser(user, userCreateRequest.Password, userCreateRequest.RoleNames);
            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse(result));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersPaging(string query, int skip, int take = 10)
        {
            // var result = await _userService.GetUserPaging(query, skip, take);
            var result = await _mediator.Send(new ListUsersQuery(skip, take, query));
            return Ok(result);
        }

        [HttpGet("{id}/get-by-id")]
        public async Task<IActionResult> GetById(Guid id)
        {
            // var user = await _userService.GetById(id);
            var user = await _mediator.Send(new GetUserQuery(id));
            if (user == null)
                return NotFound(new ApiNotFoundResponse($"Cannot found user with id: {id}"));
            var userVm = new UserVm(user.Id.ToString(), user.FirstName, user.LastName, user.DisplayName, user.Avatar,
                user.Gender ? 1 : 0, user.Address, user.IsActive ? 1 : 0);
            return Ok(userVm);
        }

        [HttpGet("{get-information-user}")]
        public async Task<IActionResult> GetInformationUser()
        {
            var id = User.GetUserId();
            var user = await _mediator.Send(new GetUserQuery(new Guid(id)));
            if (user == null)
                return NotFound(new ApiNotFoundResponse($"Cannot found user with id: {id}"));
            var userVm = new UserVm(user.Id.ToString(), user.FirstName, user.LastName, user.DisplayName, user.Avatar,
                user.Gender ? 1 : 0, user.Address, user.IsActive ? 1 : 0);
            return Ok(userVm);
        }

        [HttpPut("user-change-password")]
        [ApiValidationFilter]
        public async Task<IActionResult> PutCurrentUserPassword([FromBody] UserPasswordChangeRequest request)
        {
            var id = User.GetUserId();
            var result = await _userService.ChangeUserPassword(id, request.CurrentPassword, request.NewPassword);
            if (result == null)
                return NotFound(new ApiNotFoundResponse($"Cannot found user with id: {id}"));
            if (result.Succeeded)
            {
                return NoContent();
            }

            return BadRequest(new ApiBadRequestResponse(result));
        }


        [HttpPost("{userId}/roles")]
        public async Task<IActionResult> PostRolesToUserUser(string userId, [FromBody] RoleAssignRequest request)
        {
            if (request.RoleNames?.Length == 0)
            {
                return BadRequest(new ApiBadRequestResponse("Role names cannot empty"));
            }

            var result = await _userService.AddToRoles(userId, request.RoleNames);
            if (result == null)
                return NotFound(new ApiNotFoundResponse($"Cannot found user with id: {userId}"));
            if (result.Succeeded)
                return Ok();

            return BadRequest(new ApiBadRequestResponse(result));
        }

        [HttpPut("{id}/change-password")]
        [ApiValidationFilter]
        public async Task<IActionResult> PutUserPassword(string id, [FromBody] UserPasswordChangeRequest request)
        {
            var result = await _userService.ChangeUserPassword(id, request.CurrentPassword, request.NewPassword);
            if (result == null)
                return NotFound(new ApiNotFoundResponse($"Cannot found user with id: {id}"));
            if (result.Succeeded)
            {
                return NoContent();
            }

            return BadRequest(new ApiBadRequestResponse(result));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(string id, [FromBody] UserUpdateRequest request)
        {
            var userUpdateModel = new UserUpdateModel()
            {
                Avatar = request.Avatar,
                Gender = request.Gender,
                Address = request.Address,
                IsActive = request.IsActive,
                FirstName = request.FirstName,
                LastName = request.LastName
            };
            var result = await _userService.UpdateUser(id, userUpdateModel);
            if (result == null)
                return NotFound(new ApiNotFoundResponse($"Cannot found user with id: {id}"));
            if (result.Succeeded)
            {
                return NoContent();
            }

            return BadRequest(new ApiBadRequestResponse(result));
        }
    }
}