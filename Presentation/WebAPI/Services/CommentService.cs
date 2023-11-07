using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Yomikaze.Domain.Common;
using Yomikaze.Domain.Database.Entities;
using Yomikaze.Domain.Database.Entities.Identity;
using Yomikaze.WebAPI.Models;
using Yomikaze.WebAPI.Models.Common;
using Yomikaze.WebAPI.Models.Request;

namespace Yomikaze.WebAPI.Services;

public class CommentService
{
    private readonly IDao<Comment> _commentDao;
    private readonly IDao<Comic> _comicDao;
    private readonly UserManager<User> _userManager;

    public CommentService(IDao<Comment> commentDao, IDao<Comic> comicDao, UserManager<User> userManager)
    {
        _commentDao = commentDao;
        _comicDao = comicDao;
        _userManager = userManager;
    }

    // create comment
    public async Task<CommentModel> CreateCommentAsync(CommentRequestModel requestModel, ClaimsPrincipal principal)
    {
        var comic = await _comicDao.GetAsync(requestModel.ComicId) ?? throw new ApiServiceException("Comic not found");
        var user = await _userManager.GetUserAsync(principal) ?? throw new ApiServiceException("User not found");
        var content = requestModel.Content.Trim();
        var replyTo = null as Comment;
        if (requestModel.ReplyToId is not null) replyTo = await _commentDao.GetAsync(requestModel.ReplyToId.Value) ?? throw new ApiServiceException("Reply comment not found");
        if (replyTo is not null)
        {
            // get root comment
            while (replyTo.ReplyTo is not null)
            {
                replyTo = replyTo.ReplyTo;
            }
        }
        var comment = new Comment
        {
            Comic = comic,
            User = user,
            Content = content,
            ReplyTo = replyTo,
        };
        await _commentDao.AddAsync(comment);
        _commentDao.SaveChanges();
        var model = comment.ToModel();
        return model;
    }

    public async Task<CommentModel> UpdateCommentAsync(CommentRequestModel model, ClaimsPrincipal principal)
    {
        var id = model.Id ?? throw new ApiServiceException("Comment id is required");
        var comment = await _commentDao.GetAsync(id) ?? throw new ApiServiceException("Comment not found");
        var user = await _userManager.GetUserAsync(principal) ?? throw new ApiServiceException("User not found");
        if (comment.User != user) throw new ApiServiceException("You are not the owner of this comment");
        comment.Content = model.Content;
        await _commentDao.UpdateAsync(comment);
        _commentDao.SaveChanges();
        return comment.ToModel();
    }

    public async Task DeleteCommentAsync(CommentRequestModel model)
    {
        var id = model.Id ?? throw new ApiServiceException("Comment id is required");
        var comment = await _commentDao.GetAsync(id) ?? throw new ApiServiceException("Comment not found");
        await _commentDao.DeleteAsync(comment);
        _commentDao.SaveChanges();
    }

    public async Task<CommentModel> GetCommentAsync(long id)
    {
        var comment = await _commentDao.GetAsync(id) ?? throw new ApiServiceException("Comment not found");
        return comment.ToModel();
    }

    public async Task<IEnumerable<CommentModel>> GetCommentsAsync(long comicId)
    {
        var comic = await _comicDao.GetAsync(comicId) ?? throw new ApiServiceException("Comic not found");
        var comments = await _commentDao.FindAsync(c => c.ComicId == comic.Id);
        return comments.Select(c => c.ToModel());
    }

}
