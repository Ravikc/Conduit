using System;
using System.Collections.Generic;
using System.Text;

namespace Conduit.ApplicationCore.DTOs.Article
{   
    public class UpsertArticleRequestDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
    }
}
