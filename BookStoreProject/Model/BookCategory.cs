using System.ComponentModel.DataAnnotations;

namespace BookStoreProject.Model
{
    public class BookCategory
    {
        [Key]
        public int  BookCategoryId { get; set; }

        [Display(Name ="Category")]
        public string BookCategoryName { get; set; }
    }
}
