namespace CatsWebApp.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CatDetailsModel : IValidatableObject
    {    
        [Range(1, int.MaxValue)]
        public int Id { get; set; }

        [Required]
        [MaxLength(10, ErrorMessage = "Invalid length. It should be below 10")]
        public string Name { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    var errors = new List<ValidationResult>();

        //    if (Id == 5 && Name == "Pesho")
        //    {
        //        yield return new ValidationResult("Sorry, Id cannot be 5 if your name is Pesho");

        //    }
        //    else if(Id == 6)
        //    {
        //        yield return new ValidationResult("DASDASDASDA");
        //    }

        //    return errors;
        //}
    }
}
