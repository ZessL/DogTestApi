using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogTestApi.Misc
{
    public class PaginatedList<T> : List<T>
    {
        public int totalPages { get; private set; }
        public int currentPage { get; private set; }
        public int pageSize { get; private set; }
        public bool hasPreviousPage
        {
            get
            {
                return (currentPage > 1);
            }
        }
        public bool hasNextPage
        {
            get
            {
                return (currentPage < totalPages);
            }
        }

        public PaginatedList(List<T> items, int pageSize)
        {
            this.pageSize = pageSize;
            this.totalPages = (int)Math.Ceiling((double)(this.totalPages / this.pageSize));
            this.AddRange(items);
        }

        public List<T> returnListFromPage(int pageNumber)
        {
            List<T> outList = new List<T>();
            outList = this.Skip(pageNumber * pageSize).Take(pageSize).ToList();
            return outList;
        }

        public new void Add(T item)
        {
            base.Add(item);
            if(this.Count > this.pageSize * this.totalPages)
            {
                this.pageSize++;
            }
        }
    }
}
