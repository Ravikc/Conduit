using Conduit.ApplicationCore.DTOs.Tags;
using Conduit.ApplicationCore.DTOs.User;
using System;
using System.Collections.Generic;

namespace Conduit.ApplicationCore.DTOs.Articles
{
    public class ArticleDtoRoot<T>
    {
        public T Article { get; set; }
    }

    public class ArticleResponseDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public bool Favourited { get; set; }
        public string Slug { get; set; }
        public IEnumerable<TagDto> TagList { get; set; }
        public int FavouritesCount { get; set; }
        public UserDto Author { get; set; }
    }
}
