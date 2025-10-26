using group6_Mendoza_Hontanosass__lab3.Data.Repositories;
using group6_Mendoza_Hontanosass__lab3.Models;
using group6_Mendoza_Hontanosass__lab3.Models.ViewModels;
using group6_Mendoza_Hontanosass__lab3.Services;
/// <summary>
/// Comment Controller
/// </summary>
/// <remarks>
/// Handles comment CRUD operations on episodes
/// </remarks>
/// <author>Carl Nicolas Mendoza and Neil Hontanosas</author>
/// <version>0.1</version>
/// <date>2025-10-26</date>
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using group6_Mendoza_Hontanosass__lab3.Data.Repositories;
using group6_Mendoza_Hontanosass__lab3.Models;
using group6_Mendoza_Hontanosass__lab3.Models.ViewModels;
namespace group6_Mendoza_Hontanosass__lab3.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        private readonly ICommentRepository _commentRepository;
        private readonly UserManager<User> _userManager;

        public CommentController(ICommentRepository commentRepository, UserManager<User> userManager)
        {
            _commentRepository = commentRepository;
            _userManager = userManager;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CommentViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.GetUserAsync(User);
                    if (user == null)
                    {
                        return Unauthorized();
                    }

                    var comment = new Comment
                    {
                        EpisodeID = model.EpisodeID,
                        PodcastID = model.PodcastID,
                        UserID = user.Id,
                        Username = user.UserName!,
                        Text = model.Text,
                        Timestamp = DateTime.UtcNow
                    };

                    await _commentRepository.CreateAsync(comment);
                    TempData["Success"] = "Comment added successfully!";
                }
                catch (Exception ex)
                {
                    TempData["Error"] = $"Error adding comment: {ex.Message}";
                }
            }

            return RedirectToAction("Details", "Episode", new { id = model.EpisodeID });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int episodeId, string commentId)
        {
            var comment = await _commentRepository.GetByIdAsync(episodeId, commentId);
            if (comment == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            if (comment.UserID != userId)
            {
                return Forbid();
            }

            var model = new CommentViewModel
            {
                CommentID = comment.CommentID,
                EpisodeID = comment.EpisodeID,
                PodcastID = comment.PodcastID,
                Text = comment.Text
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CommentViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var comment = await _commentRepository.GetByIdAsync(model.EpisodeID, model.CommentID!);
                    if (comment == null)
                    {
                        return NotFound();
                    }

                    var userId = _userManager.GetUserId(User);
                    if (comment.UserID != userId)
                    {
                        return Forbid();
                    }

                    comment.Text = model.Text;
                    await _commentRepository.UpdateAsync(comment);
                    TempData["Success"] = "Comment updated successfully!";
                }
                catch (Exception ex)
                {
                    TempData["Error"] = $"Error updating comment: {ex.Message}";
                }
            }

            return RedirectToAction("Details", "Episode", new { id = model.EpisodeID });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int episodeId, string commentId)
        {
            var comment = await _commentRepository.GetByIdAsync(episodeId, commentId);
            if (comment == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            if (comment.UserID != userId && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            await _commentRepository.DeleteAsync(episodeId, commentId);
            TempData["Success"] = "Comment deleted successfully!";
            return RedirectToAction("Details", "Episode", new { id = episodeId });
        }

    }
}
