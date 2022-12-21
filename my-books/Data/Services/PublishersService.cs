using my_books.Data.Models;
using my_books.Data.Paging;
using my_books.Data.ViewModels;
using my_books.Exeptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace my_books.Data.Services
{
    public class PublishersService
    {
        private AppDbContext _context;
        public PublishersService(AppDbContext context)
        {
            _context = context;
        }
        public List<Publisher>getAllPublishers(string sortby , string searchstring ,int ? pageNumber)
        {
            var result = _context.Publishers.OrderBy(n => n.Name).ToList();
            if (!string.IsNullOrEmpty(sortby))
            {
                switch (sortby)
                {
                    case "name_desc":
                        result = result.OrderByDescending(n => n.Name).ToList();
                        break;
                    default:
                        break;
                }
            }
            if (!string.IsNullOrEmpty(searchstring))
            {
                result = result.Where(n => n.Name.Contains(searchstring,StringComparison.CurrentCultureIgnoreCase)).ToList();
            }

            //Paging - helper class for getting page number data -
            int pageSize = 5;
            result = PaginatedList<Publisher>.Create(result.AsQueryable(), pageNumber ?? 1, pageSize);
            return result;
        }

        public Publisher AddPublisher(PublisherVM publisher)
        {
            if (stringStrartsWithNumbers(publisher.Name))
                throw new PublisherNameExeption("Name Starts With Numbers", publisher.Name);
            var _publisher = new Publisher()
            {
               Name = publisher.Name
            };
            _context.Publishers.Add(_publisher);
            _context.SaveChanges();

            return _publisher;
        }

        public Publisher GetPublisherById(int id) => _context.Publishers.FirstOrDefault(n => n.Id == id);

        public PublisherWithBooksAndAuthorsVM GetPublisherData(int publisherId)
        {
            var _publisherData = _context.Publishers.Where(n => n.Id == publisherId)
                .Select(n => new PublisherWithBooksAndAuthorsVM()
                {
                    Name = n.Name,
                    BookAuthors = n.Books.Select(n => new BookAuthorVM()
                    {
                        BookName = n.Title,
                        BookAuthors = n.Book_Authors.Select(n => n.Author.FullName).ToList()
                    }).ToList()
                }).FirstOrDefault();

            return _publisherData;
        }

        public void DeletePublisherById(int id)
        {
            var _publisher = _context.Publishers.FirstOrDefault(n => n.Id == id);

            if(_publisher != null)
            {
                _context.Publishers.Remove(_publisher);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception($"the publisher with this id : {id} not exist");
            }
        }
        private bool stringStrartsWithNumbers(string str)
        {
            if (Regex.IsMatch(str, @"^\d")) return true;
            return false;
        }
    }
}
