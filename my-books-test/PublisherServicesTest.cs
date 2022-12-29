using Microsoft.EntityFrameworkCore;
using my_books.Data;
using my_books.Data.Services;
using my_books.Data.Models;
using System.Linq;

namespace my_books_test
{
    public class PublisherServicesTest
    {
        private static DbContextOptions<AppDbContext> DbContextOptions= new  DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "BookDbTest")
            .Options;
        AppDbContext context;
        PublishersService PublisherService;

        [OneTimeSetUp]
        public void Setup()
        {
            context = new AppDbContext(DbContextOptions);
            context.Database.EnsureCreated();

            SeedDatabase();
            PublisherService = new PublishersService(context);
        }

        [Test]
        public void getAllPublishers_WithNoSortBy_WithNoSearchingString_WithNoPageNumber()
        {
            //Assert.Pass();
            var result = PublisherService.getAllPublishers("", "", null);
            Assert.That(result.Count , Is.EqualTo(5));
            //Assert.AreEqual(result.Count, 3);

        }

        [Test]
        public void getAllPublishers_WithNoSortBy_WithNoSearchingString_WithPageNumber()
        {
            //Assert.Pass();
            var result = PublisherService.getAllPublishers("", "", 2);
            Assert.That(result.Count, Is.EqualTo(1));
            //Assert.AreEqual(result.Count, 3);

        }
        [Test]
        public void getAllPublishers_WithNoSortBy_WithSearchingString_WithNoPageNumber()
        {
            //Assert.Pass();
            var result = PublisherService.getAllPublishers("", "4", null);
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.FirstOrDefault().Name, Is.EqualTo("Publisher 4"));

        }
        [Test]
        public void getAllPublishers_WithSortBy_WithNOSearchingString_WithNoPageNumber()
        {
            //Assert.Pass();
            var result = PublisherService.getAllPublishers("name_desc", "", null);
            Assert.That(result.Count, Is.EqualTo(5));
            Assert.That(result.FirstOrDefault().Name, Is.EqualTo("Publisher 6"));

        }

        [OneTimeTearDown]
        public void TearDown()
        {
            context.Database.EnsureDeleted();
        }

        private void SeedDatabase()
        {
            //throw new NotImplementedException();
            var publishers = new List<Publisher>
            {
                    new Publisher() {
                        Id = 1,
                        Name = "Publisher 1"
                    },
                    new Publisher() {
                        Id = 2,
                        Name = "Publisher 2"
                    },
                    new Publisher() {
                        Id = 3,
                        Name = "Publisher 3"
                    },
                    new Publisher() {
                        Id = 4,
                        Name = "Publisher 4"
                    },
                    new Publisher() {
                        Id = 5,
                        Name = "Publisher 5"
                    },
                    new Publisher() {
                        Id = 6,
                        Name = "Publisher 6"
                    }
            };
            context.Publishers.AddRange(publishers);

            var authors = new List<Author>()
            {
                new Author()
                {
                    Id = 1,
                    FullName = "Author 1"
                },
                new Author()
                {
                    Id = 2,
                    FullName = "Author 2"
                }
            };
            context.Authors.AddRange(authors);


            var books = new List<Book>()
            {
                new Book()
                {
                    Id = 1,
                    Title = "Book 1 Title",
                    Description = "Book 1 Description",
                    IsRead = false,
                    Genre = "Genre",
                    CoverUrl = "https://...",
                    DateAdded = DateTime.Now.AddDays(-10),
                    PublisherId = 1
                },
                new Book()
                {
                    Id = 2,
                    Title = "Book 2 Title",
                    Description = "Book 2 Description",
                    IsRead = false,
                    Genre = "Genre",
                    CoverUrl = "https://...",
                    DateAdded = DateTime.Now.AddDays(-10),
                    PublisherId = 1
                }
            };
            context.Books.AddRange(books);

            var books_authors = new List<Book_Author>()
            {
                new Book_Author()
                {
                    Id = 1,
                    BookId = 1,
                    AuthorId = 1
                },
                new Book_Author()
                {
                    Id = 2,
                    BookId = 1,
                    AuthorId = 2
                },
                new Book_Author()
                {
                    Id = 3,
                    BookId = 2,
                    AuthorId = 2
                },
            };
            context.Books_Authors.AddRange(books_authors);


            context.SaveChanges();
        }

        
    }
}