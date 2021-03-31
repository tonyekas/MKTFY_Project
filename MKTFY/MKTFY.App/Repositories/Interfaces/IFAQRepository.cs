using Microsoft.AspNetCore.Mvc;
using MKTFY.Models.Entities;
using MKTFY.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.App.Repositories.Interfaces
{
    public interface IFAQRepository : IBaseRepository<FAQ, Guid>
    {
        Task<FAQ> UpdateFaq(FAQ list);
    }
}
