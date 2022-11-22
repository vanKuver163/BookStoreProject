using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreProject.Model
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        [Display(Name = "Author")]
        [Required(ErrorMessage ="Please enter Title name. It's Required")]
        [StringLength(50, MinimumLength =3, ErrorMessage="Title name is too Short. Please enter valid Title name")]
        public string Title { get; set; }

        [Display(Name = "Author name")]
        [Required(ErrorMessage = "Please enter Author name. It's Required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Author name is too Short. Please enter valid Author name")]
        public string AuthorName { get; set; }

        [Display(Name = "Releasa data")]
        [DataType(DataType.Date)]
        [DataValidation(ErrorMessage ="Title Release data should be Less then Current date")]
        public DateTime Releasedate { get; set; }

        [Required(ErrorMessage = "Please enter Rating Between (1 to 5)")]
        [Range(1,5,ErrorMessage = "Please enter Rating Between (1 to 5) ONLY")]
        public string Rating { get; set; }

        [Display(Name = "Category")]
        public int BookCategoryId { get; set; }

        [ForeignKey("BookCategoryId")]
        public virtual BookCategory? BookCategories { get; set; }
    }

    public class DataValidation:ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            DateTime todayDate = Convert.ToDateTime(value);
            return todayDate <= DateTime.Now;
        }

    }
}
