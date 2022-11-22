using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookStoreProject.Data;
using BookStoreProject.Model;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp;

namespace BookStoreProject.Pages.Books
{
    public class DetailsModel : PageModel
    {
        private readonly BookStoreProject.Data.AppDbContext _context;        

        public DetailsModel(BookStoreProject.Data.AppDbContext context)
        {
            _context = context;
        }

      public Book Book { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FirstOrDefaultAsync(m => m.BookId == id);
            book.BookCategories = await _context.BookCategories.FirstOrDefaultAsync(x => x.BookCategoryId == book.BookCategoryId);
            if (book == null)
            {
                return NotFound();
            }
            else 
            {
                Book = book;
            }
            return Page();
        }
    }
}
