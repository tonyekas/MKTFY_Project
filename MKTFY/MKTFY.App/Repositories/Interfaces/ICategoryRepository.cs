using MKTFY.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.App.Repositories.Interfaces
{
    public interface ICategoryRepository : IBaseRepository<Category, Guid>
    {
        Task<Category> AddCat(string cat);
        Task<Category> FindCatByName(string cat);
    }
}
