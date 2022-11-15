using BussinessLayer.Interface;
using Model;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Service
{
    public class BookBL : IBookBL
    {
        private readonly IBookRL ibookRL;
        public BookBL(IBookRL ibookRL)
        {
            this.ibookRL = ibookRL;
        }

        public BookModel AddBook(BookModel bookModel)
        {
            try
            {
                return this.ibookRL.AddBook(bookModel);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public BookModel UpdateBook(BookModel bookModel, int BookId)
        {
            try
            {
                return this.ibookRL.UpdateBook(bookModel, BookId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string DeleteBook(int BookId)
        {
            try
            {
                return this.ibookRL.DeleteBook(BookId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<GetBookModel> GetAllBooks()
        {
            try
            {
                return this.ibookRL.GetAllBooks();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public GetBookModel GetBookById(int BookId)
        {
            try
            {
                return this.ibookRL.GetBookById(BookId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
