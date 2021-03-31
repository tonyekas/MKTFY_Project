using Microsoft.EntityFrameworkCore;
using MKTFY.App.Repositories.Interfaces;
using MKTFY.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.App.Repositories
{
    public class FAQRepository : BaseRepository<FAQ, Guid, ApplicationDbContext>, IFAQRepository
    {
        public FAQRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {

        }

        public async Task<FAQ> UpdateFaq(FAQ list)
        {
            //var updateFaq = await _entityDbset.FirstOrDefaultAsync(uplist => uplist.Id == list.Id);
            var updateFaq = await _entityDbset.FindAsync(list.Id).ConfigureAwait(false);
            updateFaq.Question = list.Question;
            updateFaq.Answer = list.Answer;

            return updateFaq;
        }


    }
}
