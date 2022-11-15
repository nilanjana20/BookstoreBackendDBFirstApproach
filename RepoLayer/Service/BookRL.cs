using Microsoft.Extensions.Configuration;
using Model;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Text;

namespace RepoLayer.Service
{
    public class BookRL : IBookRL
    {
        private readonly IConfiguration configuration;
        public BookRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        SqlConnection sqlConnection;

        public BookModel AddBook(BookModel bookModel)
        {
            sqlConnection = new SqlConnection(this.configuration.GetConnectionString("DBConnection"));

            using (sqlConnection)
            {

                try
                {
                    SqlCommand cmd = new SqlCommand("AddBook", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    sqlConnection.Open();
                    cmd.Parameters.AddWithValue("@BookName ", bookModel.BookName);
                    cmd.Parameters.AddWithValue("@AuthorName", bookModel.AuthorName);
                    cmd.Parameters.AddWithValue("@Rating ", bookModel.Rating);
                    cmd.Parameters.AddWithValue("@RatingCount", bookModel.RatingCount);
                    cmd.Parameters.AddWithValue("@DiscountPrice ", bookModel.DiscountPrice);
                    cmd.Parameters.AddWithValue("@ActualPrice", bookModel.ActualPrice);
                    cmd.Parameters.AddWithValue("@Description ", bookModel.Description);
                    cmd.Parameters.AddWithValue("@BookImage", bookModel.BookImage);
                    cmd.Parameters.AddWithValue("@BookQuantity", bookModel.BookQuantity);

                    var result = cmd.ExecuteNonQuery();

                    if (result != 0)
                    {
                        return bookModel;
                    }
                    else
                    {
                        return null;
                    }
                }

                catch (Exception ex)
                {

                    throw ex;
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
        }

        public BookModel UpdateBook(BookModel bookModel, int BookId)
        {
            sqlConnection = new SqlConnection(this.configuration.GetConnectionString("DBConnection"));
            using (sqlConnection)
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("UpdateBook", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    sqlConnection.Open();
                    cmd.Parameters.AddWithValue("@BookName ", bookModel.BookName);
                    cmd.Parameters.AddWithValue("@AuthorName", bookModel.AuthorName);
                    cmd.Parameters.AddWithValue("@Rating ", bookModel.Rating);
                    cmd.Parameters.AddWithValue("@RatingCount", bookModel.RatingCount);
                    cmd.Parameters.AddWithValue("@DiscountPrice ", bookModel.DiscountPrice);
                    cmd.Parameters.AddWithValue("@ActualPrice", bookModel.ActualPrice);
                    cmd.Parameters.AddWithValue("@Description ", bookModel.Description);
                    cmd.Parameters.AddWithValue("@BookImage", bookModel.BookImage);
                    cmd.Parameters.AddWithValue("@BookQuantity", bookModel.BookQuantity);
                    cmd.Parameters.AddWithValue("@BookId ", BookId);


                    var result = cmd.ExecuteNonQuery();

                    if (result != 0)
                    {
                        return bookModel;
                    }
                    else
                    {
                        return null;
                    }
                }

                catch (Exception ex)
                {

                    throw ex;
                }
                finally
                {
                    sqlConnection.Close();
                }

            }
        }

        public string DeleteBook(int BookId)
        {
            sqlConnection = new SqlConnection(this.configuration.GetConnectionString("DBConnection"));
            using (sqlConnection)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("DeleteBook", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    sqlConnection.Open();

                    cmd.Parameters.AddWithValue("@BookId ", BookId);
                    var result = cmd.ExecuteNonQuery();

                    if (result != 0)
                    {
                        return "Book deleted";
                    }
                    else
                    {
                        return "Failed to delete";
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
                finally
                {
                    sqlConnection.Close();
                }

            }
        }

        public List<GetBookModel> GetAllBooks()
        {
            List<GetBookModel> books = new List<GetBookModel>();
            sqlConnection = new SqlConnection(this.configuration.GetConnectionString("DBConnection"));
            sqlConnection = new SqlConnection(this.configuration.GetConnectionString("DBConnection"));
            using (sqlConnection)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("GetAllBooks", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    sqlConnection.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            books.Add(new GetBookModel
                            {
                                BookId = Convert.ToInt32(reader["BookId"]),
                                BookName = reader["BookName"].ToString(),
                                AuthorName = reader["AuthorName"].ToString(),
                                Rating = reader["Rating"].ToString(),
                                RatingCount = Convert.ToInt32(reader["RatingCount"]),
                                DiscountPrice = reader["DiscountPrice"].ToString(),
                                ActualPrice = reader["ActualPrice"].ToString(),
                                Description = reader["Description"].ToString(),
                                BookImage = reader["BookImage"].ToString(),
                                BookQuantity = Convert.ToInt32(reader["BookQuantity"]),
                            });
                        }

                        return books;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
                finally
                {
                    sqlConnection.Close();
                }

            }
        }
        public GetBookModel GetBookById(int BookId)
        {
            sqlConnection = new SqlConnection(this.configuration.GetConnectionString("DBConnection"));
            using (sqlConnection)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("GetBookById", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@BookId ", BookId);

                    sqlConnection.Open();
                    var result = cmd.ExecuteNonQuery();

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        GetBookModel model = new GetBookModel();
                        while (reader.Read())
                        {
                            model.BookId = Convert.ToInt32(reader["BookId"]);
                            model.BookName = reader["BookName"].ToString();
                            model.AuthorName = reader["AuthorName"].ToString();
                            model.Rating = reader["Rating"].ToString();
                            model.RatingCount = Convert.ToInt32(reader["RatingCount"]);
                            model.DiscountPrice = reader["DiscountPrice"].ToString();
                            model.ActualPrice = reader["ActualPrice"].ToString();
                            model.BookImage = reader["BookImage"].ToString();
                            model.BookQuantity = Convert.ToInt32(reader["BookQuantity"]);

                        }

                        return model;
                    }
                    else
                    {
                        return null;
                    }
                }

                catch (Exception ex)
                {

                    throw ex;
                }
                finally
                {
                    sqlConnection.Close();
                }
            }

        }
    }
}
