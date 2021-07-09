using Hello.BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hello.BookStore.Repository
{
    public interface IBookRepository
    {
        Task<int> AddNewBook(BookModel model);
        Task<List<BookModel>> GetAllBooks();
        BookModel GetBookById(int id);
        Task<List<BookModel>> GetTopBookAsync(int count);
        BookModel SearchBook(string title);
        Task<int> ReviewOfBookById(string now, ReviewBookModel model);
        bool FindBookByIdAndTitle(ReviewBookModel model, List<BookModel> AllBooks);

        Task<List<ReviewBookModel>> GetAllReviewOfBook();
        string GetAppName();
    }
}