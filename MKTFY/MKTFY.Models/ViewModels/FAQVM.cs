using MKTFY.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MKTFY.Models.ViewModels
{
    public class FAQVM
    {
        public FAQVM(FAQ src)
        {
            Id = src.Id;
            Question = src.Question;
            Answer = src.Answer;
        }

        public Guid Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
