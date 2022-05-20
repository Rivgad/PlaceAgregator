using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;

namespace PlaceAgregator.Entities
{
    /// <summary>
    /// Class <see cref="Comment"/> represents a user's remark about <see cref="Entities.Place"/>
    /// </summary>
    [DataContract]
    public class Comment : Entity, IValidatableObject
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public User User { get; set; }
        /// <summary>
        /// Text of comment
        /// </summary>
        [Required]
        [MinLength(1)]
        public string Text { get;  set; }

        /// <summary>
        /// Publication date and time
        /// </summary>
        public DateTime PublicationDate { get;  set; }

        /// <summary>
        /// Comment rating in range from 1 to 5
        /// </summary>
        [Range(1, 5)]
        public int Rating { get;  set; }

        /// <summary>
        /// The id of place for which the comment was created
        /// </summary>
        [Required]
        public int PlaceId { get;  set; }

        /// <summary>
        /// The place for which the comment was created
        /// </summary>
        [JsonIgnore]
        public Place? Place { get;  set; }

        /// <summary>
        /// Initializes a new instance of <see cref="Comment"/> class
        /// </summary>
        private Comment() { }

        /// <summary>
        /// Initializes a new instance of <see cref="Comment"/> class
        /// </summary>
        /// <param name="place">The place for which the comment is created</param>
        /// <param name="text">Comment text</param>
        /// <param name="publicationDate">Date and time of publication of comment</param>
        /// <param name="rating">Rating for place in range from 1 to 5</param>
        public Comment(Place place, string text, DateTime publicationDate, int rating)
            : this(placeId: place.Id,
                  text: text, 
                  publicationDate: publicationDate, 
                  rating: rating)
        {
            Place = place;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="Comment"/> class with current DateTime
        /// </summary>
        /// <param name="place">The place for which the comment is created</param>
        /// <param name="text">Comment text</param>
        /// <param name="rating">Rating for place in range from 1 to 5</param>
        public Comment(Place place, string text, int rating)
            : this(placeId: place.Id,
                  text: text,
                  rating: rating)
        {
            Place = place;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="Comment"/> class
        /// </summary>
        /// <param name="placeId">The id of place for which the comment is created</param>
        /// <param name="text">Comment text</param>
        /// <param name="publicationDate">Date and time of publication of comment</param>
        /// <param name="rating"></param>
        public Comment(int placeId, string text, DateTime publicationDate, int rating)
        {
            PlaceId = placeId;
            Text = text;
            PublicationDate = publicationDate;
            Rating = rating;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="Comment"/> class with current DateTime
        /// </summary>
        /// <param name="placeId">The id of place for which the comment is created</param>
        /// <param name="text">Comment text</param>
        /// <param name="rating">Rating for place in range from 1 to 5</param>
        public Comment(int placeId, string text, int rating)
            : this(placeId: placeId,
                  text: text,
                  publicationDate: DateTime.Now,
                  rating: rating)
        { }

        [JsonConstructor]
        public Comment(int id,int placeId, string text, DateTime publicationDate, int rating)
        {
            Id = id;
            PlaceId = placeId;
            Text = text;
            PublicationDate = publicationDate;
            Rating = rating;
        }

        public void ChangeText(string text)
        {
            if (text == null || string.IsNullOrEmpty(text))
                throw new ArgumentNullException($"{nameof(text)}");

            Text = text;
        }
        public void ChangeRating([Range(1,5)]int rating)
        {
            var ctx = new ValidationContext(rating);
            ICollection<ValidationResult> errors = new List<ValidationResult>();
            var result = Validator.TryValidateProperty(rating, ctx, errors);
            if (result == false)
                throw new ArgumentException(errors.First().ErrorMessage);

            Rating = rating;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            ICollection<ValidationResult> errors = new List<ValidationResult>();


            return errors;
        }
    }
}
