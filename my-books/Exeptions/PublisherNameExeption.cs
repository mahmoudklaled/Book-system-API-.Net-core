using System;

namespace my_books.Exeptions
{
    public class PublisherNameExeption:Exception
    {
        public string PublisherName { get; set; }
        public PublisherNameExeption()
        {

        }

        public PublisherNameExeption(string message):base(message) 
        {

        }

        public PublisherNameExeption(string message , Exception inner) : base(message,inner)
        {

        }
        public PublisherNameExeption(string message , string publishername):this(message)
        {
            PublisherName= publishername;

        }
    }
}
