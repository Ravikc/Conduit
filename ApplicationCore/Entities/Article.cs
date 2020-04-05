using Conduit.Common.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Conduit.ApplicationCore.Entities
{
    public class Article : AuditableEntity<int>
    {
        private string _slug;
        public string Slug
        {
            get
            {
                _slug = _slug ?? GetSlug();
                return _slug;
            }
            private set
            {
                _slug = value;
            }
        }

        [Required, MaxLength(256)]
        public string Title { get; set; }

        [Required, MaxLength(512)]
        public string Description { get; set; }

        [Required]
        public string Body { get; set; }

        public IEnumerable<Tag> Tags { get; set; }

        public string AuthorId { get; set; }
        public ApplicationUser Author { get; set; }

        private string GetSlug()
        {
            string text = Title ?? string.Empty;

            string str = text.RemoveDiacritics().ToLowerInvariant();
            // invalid chars           
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim 
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            str = Regex.Replace(str, @"\s", "-"); // hyphens   

            return str;
        }

        public void UpdateSlugWithId()
        {
            _slug = $"{Slug}-{Id.ToString(CultureInfo.InvariantCulture)}";
        }
    }
}