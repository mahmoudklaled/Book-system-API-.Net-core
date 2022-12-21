using System;
using System.Collections.Generic;
using System.Linq;

namespace my_books.Data.Paging
{
    public class PaginatedList<T>:List<T>
    {
        public int index { get; private set; }
        public int totalPages { get; private set; }
        public PaginatedList(List<T>item ,int count , int _index , int pageSize) 
        {
            index = _index;
            totalPages = (int)Math.Ceiling(count/(double)pageSize);
            this.AddRange(item);
        }
        public bool hasPreviousPage
        {
            get
            {
                return index > 1;
            }
        }
        public bool hasNextPage
        {
            get { return index < totalPages; }
        }
        public static PaginatedList<T> Create(IQueryable<T> source , int page_indx , int pageSize)
        {
            var count = source.Count(); ;
            var item = source.Skip(page_indx - 1 * pageSize).Take(pageSize).ToList();
            return new PaginatedList<T>(item, count , page_indx , pageSize);
        }
    }
}
