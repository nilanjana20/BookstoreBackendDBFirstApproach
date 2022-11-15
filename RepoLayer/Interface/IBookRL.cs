using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Interface
{
    public interface IBookRL
    {
        public BookModel AddBook(BookModel bookModel);
        public BookModel UpdateBook(BookModel bookModel, int BookId);
        public string DeleteBook(int BookId);
        public List<GetBookModel> GetAllBooks();
        public GetBookModel GetBookById(int BookId);
    }
}
