using BussinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using System;

namespace Bookstore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookBL bookBL;
        public BookController(IBookBL bookBL)
        {
            this.bookBL = bookBL;
        }

        [Authorize]
        [HttpPost("AddBook")]
        public ActionResult AddBook(BookModel bookModel)
        {
            try
            {
                var book = this.bookBL.AddBook(bookModel);
                if (book != null)
                {
                    return this.Ok(new { success = true, message = "Book added successfully", data = book });
                }
                return this.BadRequest(new { success = false, message = "Failed to add book", data = book });
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [Authorize]
        [HttpPut("UpdateBook")]
        public ActionResult UpdateBook(BookModel bookModel, int BookId)
        {
            try
            {
                var book = this.bookBL.UpdateBook(bookModel, BookId);
                if (book != null)
                {
                    return this.Ok(new { success = true, message = "Book updated successfully", data = book });
                }
                return this.BadRequest(new { success = false, message = "Failed to update a book", data = book });
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [Authorize]
        [HttpDelete("DeleteBook")]
        public ActionResult DeleteBook(int BookId)
        {
            try
            {
                var book = this.bookBL.DeleteBook(BookId);
                if (book != null)
                {
                    return this.Ok(new { success = true, message = "Book is deleted", data = book });
                }
                return this.BadRequest(new { success = false, message = "Failed to delete a book", data = book });
            }
            catch
            {

                throw;
            }
        }

        [Authorize]
        [HttpGet("GetAllBooks")]
        public ActionResult GetAllBooks()
        {
            try
            {
                var book = this.bookBL.GetAllBooks();

                if (book != null)
                {
                    return this.Ok(new { success = true, message = "Getting all of your books", data = book });
                }
                return this.BadRequest(new { success = false, message = "Failed to get your all books", data = book });

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //[Authorize]
        [HttpGet("GetBookById")]
        public ActionResult GetBookById(int BookId)
        {
            try
            {
                var book = this.bookBL.GetBookById(BookId);

                if (book != null)
                {
                    return this.Ok(new { success = true, message = "Getting your book", data = book });
                }
                return this.BadRequest(new { success = false, message = "Failed to get your book", data = book });
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
