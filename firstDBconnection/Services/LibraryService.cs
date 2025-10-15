using firstDBconnection.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using firstDBconnection.Exceptions;



namespace firstDBconnection.Services
{
    public class LibraryService
    {
        private readonly Library_App _context;

        public LibraryService(Library_App context)
        {
            _context = context;
        }

        public async Task<Author> AddAuthor(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
            return author;
        }

        public async Task<List<Author>> GetAuthors()
        {
            return await _context.Authors.ToListAsync();
        }

        public async Task<Author> GetAuthor(int id)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
    
            if (author == null)
            {
                throw new AuthorNotFoundException($"Author with ID {id} not found.");
            }

            return author;
        }

        public async Task<Book> AddBook(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<List<Book>> GetBooks()
        {
            return await _context.Books.ToListAsync();
        }
    }
}