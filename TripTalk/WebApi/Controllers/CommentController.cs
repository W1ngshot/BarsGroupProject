using Core;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dto;

namespace WebApi.Controllers;

[Authorize]
public class CommentController : Controller
{
    private readonly ICommentService _commentService;
    private readonly IUserService _userService;

    public CommentController(ICommentService commentService, IUserService userService)
    {
        _commentService = commentService;
        _userService = userService;
    }

    public async Task Add(AddCommentDto commentDto)
    {
        var user = User.Identity?.Name ?? throw new Exception(ErrorMessages.AuthError);
        var userId = await _userService.GetUserIdByEmailAsync(user);
        await _commentService.CreateCommentAsync(commentDto.Message, userId, commentDto.ArticleId);
    }

    public async Task Edit(EditCommentDto commentDto)
    {
        var user = User.Identity?.Name ?? throw new Exception(ErrorMessages.AuthError);
        var userId = await _userService.GetUserIdByEmailAsync(user);
        await _commentService.EditCommentAsync(commentDto.CommentId, commentDto.Message);
        //TODO передавать сюда userId и проверять == ли оно владельцу коммента
    }

    public async Task Delete(int commentId)
    {
        var user = User.Identity?.Name ?? throw new Exception(ErrorMessages.AuthError);
        var userId = await _userService.GetUserIdByEmailAsync(user);
        await _commentService.DeleteCommentAsync(commentId);
        //TODO передавать сюда userId и проверять == ли оно владельцу коммента
    }
}