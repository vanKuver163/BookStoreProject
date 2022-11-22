using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookStoreProject.Data;
using BookStoreProject.Model;

namespace BookStoreProject.Pages.Categorys
{
    public class IndexModel : PageModel
    {
        private readonly BookStoreProject.Data.AppDbContext _context;

        public IndexModel(BookStoreProject.Data.AppDbContext context)
        {
            _context = context;
        }

        public IList<BookCategory> BookCategory { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.BookCategories != null)
            {
                BookCategory = await _context.BookCategories.ToListAsync();
            }
        }
    }
}
