using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Conduit.ApplicationCore.Errors
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
