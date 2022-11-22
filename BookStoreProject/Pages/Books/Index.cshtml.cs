using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookStoreProject.Data;
using BookStoreProject.Model;
using Microsoft.CodeAnalysis.CSharp;
using System.Net;

namespace BookStoreProject.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly BookStoreProject.Data.AppDbContext _context;

        public string TitleSort {get; set;}
        public string AuthorSort {get; set;}
        public string BookCategorySort {get; set;}
        public string RatingSort {get; set;}
        public string DateSort {get; set;}

        public string CurrentFilter { get; set; }

        public IndexModel(BookStoreProject.Data.AppDbContext context)
        {
            _context = context;
        }

        public PaginationList<Book> Book { get;set; } = default!;

        public async Task OnGetAsync(string sortOrder, string searchString, int? pageIndex)
        {
            if (_context.Books != null)
            {
                TitleSort = sortOrder == "title_asc_sort" ? "title_desc_sort" : "title_asc_sort";
                AuthorSort = sortOrder == "author_asc_sort" ? "author_desc_sort" : "author_asc_sort";
                BookCategorySort = sortOrder == "bookcategory_asc_sort" ? "bookcategory_desc_sort" : "bookcategory_asc_sort";
                RatingSort = sortOrder == "rating_asc_sort" ? "rating_desc_sort" : "rating_asc_sort";
                DateSort = sortOrder == "Date" ? "date_desc" : "Date";

                IQueryable<Book> booksIQ = from s in _context.Books select s;

                foreach (var item in booksIQ)
                {
                    item.BookCategories = await _context.BookCategories.FirstOrDefaultAsync(x => x.BookCategoryId == item.BookCategoryId);
                }

                if (!String.IsNullOrEmpty(searchString))
                {
                    booksIQ = booksIQ.Where(x => x.Title.Contains(searchString) || x.AuthorName.Contains(searchString) || x.BookCategories.BookCategoryName.Contains(searchString));
                }              
               

                switch (sortOrder)
                {
                    case "title_asc_sort":
                        booksIQ = booksIQ.OrderBy(x => x.Title);
                        break;
                    case "title_desc_sort":
                        booksIQ = booksIQ.OrderByDescending(x => x.Title);
                        break;
                    case "author_asc_sort":
                        booksIQ = booksIQ.OrderBy(x => x.AuthorName);
                        break;
                    case "author_desc_sort":
                        booksIQ = booksIQ.OrderByDescending(x => x.AuthorName);
                        break;
                    case "bookcategory_asc_sort":
                        booksIQ = booksIQ.OrderBy(x => x.BookCategories);
                        break;
                    case "bookcategory_desc_sort":
                        booksIQ = booksIQ.OrderByDescending(x => x.BookCategories);
                        break;
                    case "rating_asc_sort":
                        booksIQ = booksIQ.OrderBy(x => x.Rating);
                        break;
                    case "rating_desc_sort":
                        booksIQ = booksIQ.OrderByDescending(x => x.Rating);
                        break;
                    case "Date":
                        booksIQ = booksIQ.OrderBy(x => x.Releasedate);
                        break;
                    case "date_desc":
                        booksIQ = booksIQ.OrderByDescending(x => x.Releasedate);
                        break;
                    default:
                        booksIQ = booksIQ.OrderBy(x => x.Title);
                        break;

                }
                var pageSize = 2;
                Book = await PaginationList<Book>.CreateAsync(booksIQ, pageIndex ?? 1, pageSize);
            }
        }
    }
}
