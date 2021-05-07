using System;
using System.Collections.Generic;
using System.Text;

namespace AVA.ForBrain.ApplicationCore.Entities
{
    public class Pagination<TEntity>
    {
        public int CurrentPage { get; set; }

        public int PageSize { get; set; }

        public int TotalPages { get; set; }

        public int TotalItems { get; set; }

        public string OrderBy { get; set; }

        public bool OrderByDesc { get; set; }

        public List<TEntity> Result { get; set; }
    }
}
