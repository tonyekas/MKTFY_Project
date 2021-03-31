using MKTFY.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MKTFY.Models.Entities
{
    public class FAQ : BaseEntity<Guid>
    {
        public FAQ()
            : base() { }

        public FAQ(FAQAddVM src)
              : base()
        {
            Question = src.Question;
            Answer = src.Answer;

        }

        public FAQ(FAQUpdateVM src)
        {
            Id = src.Id;
            Question = src.Question;
            Answer = src.Answer;
        }

        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
