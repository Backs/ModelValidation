namespace Api.Service.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class PostModel : IValidatableObject
    {
        public string Name { get; set; }

        public string SecondName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if ((string.IsNullOrWhiteSpace(this.Name) && string.IsNullOrWhiteSpace(this.SecondName)) || (!string.IsNullOrWhiteSpace(this.Name) && !string.IsNullOrWhiteSpace(this.SecondName)))
            {
                yield return new ValidationResult("Name or SecondName must be set, not both");
            }
        }
    }
}