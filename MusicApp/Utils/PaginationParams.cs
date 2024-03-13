using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class PaginationParams
    {
        private const int _maxItemsPerPage = 25;
        private int pageSize;        

        public int PageNumber { get; set; } = 1;

        public int PageSize
        {
            get => pageSize;
            set => pageSize = value > _maxItemsPerPage ? _maxItemsPerPage : value;
        }
    }

    public class PaginationAlbum : PaginationParams
    {
        private string genre = "";
        public string Genre 
        {
            get => genre;
            set => genre = value == null ? "" : value;
        }
        public string SortBy { get; set; }
        public string OrderBy { get; set; }
    }
}
