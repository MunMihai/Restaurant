using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Restaurant.Entities;

namespace Restaurant.Decorator
{
    // Decorator
    public abstract class FeedbackDecorator : IFeedback
    {
        private readonly IFeedback _feedback;

        public FeedbackDecorator(IFeedback feedback)
        {
            _feedback = feedback;
        }

        public virtual string GetContent() => _feedback.GetContent();
    }



    [Table("FeedbackWithPhoto")]
    public class FeedbackWithPhotoDecorator : FeedbackDecorator
    {
        [Key] // Specify the photo link as the key
        public string PhotoLink { get; set; }

        public int FeedbackId { get; set; } // Foreign key

        public BasicFeedback Feedback { get; set; } // Navigation property

        public FeedbackWithPhotoDecorator() : base(null) { } // Parameterless constructor

        public FeedbackWithPhotoDecorator(BasicFeedback basicFeedback, string photoLink) : base(basicFeedback)
        {
            PhotoLink = photoLink;
            Feedback = basicFeedback;
        }

        public override string GetContent()
        {
            return $"{base.GetContent()} [Photo Link: {PhotoLink}]";
        }
    }
}


