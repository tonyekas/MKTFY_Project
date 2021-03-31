using System;

namespace MKTFY.Models.ViewModels
{
    public class GenResponseVM
    {
        public bool isSuccess { get; set; }
        public string Message { get; set; }

        //public IEnumerable<string> Errors { get; set; }

        public DateTime? ExpireDate { get; set; }

    }
}
