using System.Collections.Generic;

namespace Conduit.ApplicationCore.DTOs
{
    public class Errors
    {
        public IEnumerable<string> Body { get; set; }

    }

    public class ErrorsDtoRoot
    {
        public Errors Errors { get; set; }
    }
}
