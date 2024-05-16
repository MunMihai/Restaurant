using Microsoft.AspNetCore.Mvc;
using Restaurant.DB;
using Restaurant.Decorator;
using Restaurant.Entities;

namespace Restaurant.Controllers;

[Route("api/feedback")]
[ApiController]
public class FeedbackController : ControllerBase
{
    private readonly RestaurantDbContext _context;

    public FeedbackController(RestaurantDbContext context)
    {
        _context = context;
    }

    [HttpGet("all")]
    public IActionResult GetAllFeedback()
    {
        var basicFeedback = _context.Feedback.ToList();
        var decoratedFeedback = _context.DecoratedFeedback.Select(df => df.Feedback).ToList();

        // Combine basic and decorated feedback and remove duplicates
        var allFeedback = basicFeedback.Union(decoratedFeedback).ToList();

        return Ok(allFeedback);
    }

    [HttpPost("attachphoto")]
    public IActionResult AttachPhotoToFeedback(int feedbackId, string photoLink)
    {
        
            var existingFeedback = _context.Feedback.FirstOrDefault(f => f.Id == feedbackId);
            if (existingFeedback == null)
                return NotFound("Feedback not found");

            var existingDecoratedFeedback = _context.DecoratedFeedback.FirstOrDefault(d => d.FeedbackId == feedbackId);
            if (existingDecoratedFeedback != null)
                return BadRequest("Feedback is already decorated with a photo");

            var decoratedFeedback = new FeedbackWithPhotoDecorator(existingFeedback, photoLink);
            _context.DecoratedFeedback.Add(decoratedFeedback);
            _context.SaveChanges();
            return Ok("Photo attached to feedback successfully");
    }

    [HttpPost("create")]
    public IActionResult CreateFeedback(string content)
    {
       
            var basicFeedback = new BasicFeedback { Content = content };
            _context.Feedback.Add(basicFeedback);
            _context.SaveChanges();
            return Ok("Feedback created successfully");
      
    }
}
