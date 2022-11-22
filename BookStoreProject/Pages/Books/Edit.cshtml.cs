using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookStoreProject.Data;
using BookStoreProject.Model;

namespace BookStoreProject.Pages.Books
{
    public class EditModel : PageModel
    {
        private readonly BookStoreProject.Data.AppDbContext _context;
        public List<SelectListItem> GetBookCategories { get; set; }


        public EditModel(BookStoreProject.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }
            GetBookCategories = _context.BookCategories.Select(i => new SelectListItem { Text = i.BookCategoryName, Value = i.BookCategoryId.ToString() }).ToList();

            var book =  await _context.Books.FirstOrDefaultAsync(m => m.BookId == id);

            book.BookCategories = await _context.BookCategories.FirstOrDefaultAsync(x => x.BookCategoryId == book.BookCategoryId);
            if (book == null)
            {
                return NotFound();
            }
            Book = book;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(Book.BookId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BookExists(int id)
        {
          return _context.Books.Any(e => e.BookId == id);
        }
    }
}
