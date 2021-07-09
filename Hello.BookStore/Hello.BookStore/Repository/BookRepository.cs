using Hello.BookStore.Data;
using Hello.BookStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hello.BookStore.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext _context = null;
        private readonly IConfiguration _configuration;

        public BookRepository(BookStoreContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<int> AddNewBook(BookModel model)
        {
            var newBook = new Books()
            {
                Author = model.Author,
                Description = model.Description,
                Title = model.Title,
                TotalPages = model.TotalPages ?? 0,
                CategoryId = model.CategoryId,
                LanguageId = model.LanguageId,
                CoverImageUrl = model.CoverImageUrl,
                BookPdfUrl = model.BookPdfUrl
            };

            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();

            /*
             _context.Add(newBook);
            _context.SaveChanges();
             */

            return newBook.ID;

        }
        public async Task<int> ReviewOfBookById(string now, ReviewBookModel model)
        {
            var newReview = new ReviewBook()
            {
                BookId = model.BookId,
                BookName = model.BookName,
                ReviewerName = model.ReviewerName,
                ReviewTime = now,
                BookReviewDetails = model.BookReviewDetails
            };

            await _context.ReviewBook.AddAsync(newReview);
            await _context.SaveChangesAsync();

            return newReview.BookId;
        }
        public async Task<List<BookModel>> GetAllBooks()
        {

            return await _context.Books.Select(book => new BookModel()
            {
                Author = book.Author,
                CategoryId = book.CategoryId,
                Category = book.Category.Name,
                Description = book.Description,
                ID = book.ID,
                LanguageId = book.LanguageId,
                Language = book.Language.Name,
                Title = book.Title,
                TotalPages = book.TotalPages,
                CoverImageUrl = book.CoverImageUrl
            }).ToListAsync();

            /*
             _context.Books.ToList() ;
             */
        }

        public async Task<List<ReviewBookModel>> GetAllReviewOfBook()
        {

            return await _context.ReviewBook.Select(review => new ReviewBookModel()
            {
                BookId = review.BookId,
                BookName = review.BookName,
                ReviewerName = review.ReviewerName,
                ReviewTime = review.ReviewTime,
                BookReviewDetails = review.BookReviewDetails
            }).ToListAsync();
        }

        public async Task<List<BookModel>> GetTopBookAsync(int count)
        {

            return await _context.Books.Select(book => new BookModel()
            {
                Author = book.Author,
                CategoryId = book.CategoryId,
                Category = book.Category.Name,
                Description = book.Description,
                ID = book.ID,
                LanguageId = book.LanguageId,
                Language = book.Language.Name,
                Title = book.Title,
                TotalPages = book.TotalPages,
                CoverImageUrl = book.CoverImageUrl
            }).Take(count).ToListAsync();
        }
        public BookModel GetBookById(int id)
        {

            return _context.Books.Where(x => x.ID == id)
                .Select(book => new BookModel()
                {
                    Author = book.Author,
                    CategoryId = book.CategoryId,
                    Category = book.Category.Name,
                    Description = book.Description,
                    ID = book.ID,
                    LanguageId = book.LanguageId,
                    Language = book.Language.Name,
                    Title = book.Title,
                    TotalPages = book.TotalPages,
                    CoverImageUrl = book.CoverImageUrl,
                    BookPdfUrl = book.BookPdfUrl
                }).FirstOrDefault();

            /*
             var object = _context.books.Find(id);
             */
        }

        public BookModel SearchBook(string title)
        {
            return _context.Books.Where(x => (x.Title.ToLower() ?? "") == title.ToLower())
                .Select(book => new BookModel()
                {
                    Author = book.Author,
                    CategoryId = book.CategoryId,
                    Category = book.Category.Name,
                    Description = book.Description,
                    ID = book.ID,
                    LanguageId = book.LanguageId,
                    Language = book.Language.Name,
                    Title = book.Title,
                    TotalPages = book.TotalPages,
                    CoverImageUrl = book.CoverImageUrl,
                    BookPdfUrl = book.BookPdfUrl
                }).FirstOrDefault();
        }
        public bool FindBookByIdAndTitle(ReviewBookModel model, List<BookModel> AllBooks)
        {
            var bookModel = _context.Books.Where(x => x.ID == model.BookId && x.Title.ToLower() == model.BookName.ToLower())
                .Select(book => new BookModel()
                {
                    Author = book.Author,
                    CategoryId = book.CategoryId,
                    Category = book.Category.Name,
                    Description = book.Description,
                    ID = book.ID,
                    LanguageId = book.LanguageId,
                    Language = book.Language.Name,
                    Title = book.Title,
                    TotalPages = book.TotalPages,
                    CoverImageUrl = book.CoverImageUrl,
                    BookPdfUrl = book.BookPdfUrl
                }).FirstOrDefault();

            return (bookModel != null) ;
        }

        public string GetAppName()
        {
            return _configuration.GetValue<string>("AppName");
        }

    }
}
