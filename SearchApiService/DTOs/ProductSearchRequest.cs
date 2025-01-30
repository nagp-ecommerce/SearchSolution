﻿namespace SearchApiService.DTOs
{
    public class ProductSearchRequest
    {
        // search by productname or categoryname
        public string query { get; set; }
        public int page { get; set; } = 1;
        public int pageSize { get; set; } = 10;
    }
}
