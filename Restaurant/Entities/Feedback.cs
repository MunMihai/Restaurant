using System;
namespace Restaurant.Entities
{
    // Feedback Interface
    public interface IFeedback
    {
        string GetContent();
    }

    // Concrete Feedback Class
    public class BasicFeedback : IFeedback
    {
        public int Id { get; set; }
        public string Content { get; set; }

        public string GetContent() => Content;
    }
}