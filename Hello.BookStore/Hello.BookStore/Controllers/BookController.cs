using Hello.BookStore.Models;
using Hello.BookStore.Repository;
using Hello.BookStore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hello.BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository = null;
        private readonly ILanguageRepository _languageRepository = null;
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _webHostEnvironment; // this is for saving file in database.
        public BookController(IBookRepository bookRepository,
            ILanguageRepository languageRepository,
            IUserService userService,
            IWebHostEnvironment webHostEnvironment)
        {
            _bookRepository = bookRepository;
            _languageRepository = languageRepository;
            _userService = userService;
            _webHostEnvironment = webHostEnvironment;
        }
        [Authorize]

        public async Task<IActionResult> GetAllBooks(bool isSuccess = true)
        {

            ViewBag.data = await _bookRepository.GetAllBooks();
            ViewBag.IsSuccess = isSuccess;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GetAllBooks(BookModel model)
        {
            ViewBag.data = await _bookRepository.GetAllBooks();
            var book = _bookRepository.SearchBook(model.Title ?? "");
            if (book == null)
            {
                return RedirectToAction(nameof(GetAllBooks), new { isSuccess = false });
            }
            return View(book);
        }
        [Authorize]
        [Route("book-details/{id:int:min(1)}", Name ="bookDetailsRoute")] // route constraints
        public ViewResult GetBook(int id)
        {
            var isLoggedIn = _userService.IsAuthenticated();
            if(isLoggedIn)
            {
                var book = _bookRepository.GetBookById(id);
                return View(book);
            }
            return View("Account", "LogIn");
        }
        public BookModel SearchBooks(string bookName)
        {
            return _bookRepository.SearchBook(bookName);
        }

        [Authorize]
        public Task<ViewResult> AddNewBook(bool isSuccess = false, int bookId = 0)
        {
            ViewBag.IsSuccess = isSuccess;
            ViewBag.BookId = bookId;
            return Task.FromResult(View());
        }
        [HttpPost]
        public async Task<IActionResult> AddNewBook(BookModel bookModel)
        {
            if(ModelState.IsValid)
            {
                if(bookModel.CoverPhoto != null)
                {
                    string folder = "books/cover/";
                    bookModel.CoverImageUrl = await UploadFile(folder, bookModel.CoverPhoto);
                }

                if (bookModel.BookPdf != null)
                {
                    string folder = "books/pdf/";
                    bookModel.BookPdfUrl = await UploadFile(folder, bookModel.BookPdf);
                }
                int id = await _bookRepository.AddNewBook(bookModel);
                if (id > 0)
                {
                    return RedirectToAction(nameof(AddNewBook), new { isSuccess = true, bookId = id });
                }
            }

            return View();
        }

        public ViewResult ReviewOfBookById(int id, int isSuccess = 2)
        {
            ViewBag.IsSuccess = isSuccess;
            ViewBag.BookId = id;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ReviewOfBookById(ReviewBookModel reviewBookModel)
        {
            int SuccessTrue = 1, SuccessFalse = 0, DefaultSuccess = 2; 
            if(ModelState.IsValid)
            {
                var now = DateTime.Now.ToLongDateString();
                var AllBooks = await _bookRepository.GetAllBooks();
                bool isValidBook = _bookRepository.FindBookByIdAndTitle(reviewBookModel, AllBooks);
                if(isValidBook)
                {
                    int id = await _bookRepository.ReviewOfBookById(now, reviewBookModel);
                    if (id > 0 && isValidBook) return RedirectToAction(nameof(ReviewOfBookById), new { bookId = id, isSuccess = SuccessTrue });
                    else return RedirectToAction(nameof(ReviewOfBookById), new { bookId = id, isSuccess = DefaultSuccess  });
                }
                else
                {
                    return RedirectToAction(nameof(ReviewOfBookById), new { bookId = reviewBookModel.BookId, isSuccess = SuccessFalse });
                }
            }
            return View();
            
        }



        private async Task<string> UploadFile(string folderPath, IFormFile file)
        {
            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;
            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);
            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
            return "/" + folderPath;
        }

    }
}
