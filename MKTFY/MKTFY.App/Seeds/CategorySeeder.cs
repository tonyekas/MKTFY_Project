using MKTFY.App.Repositories;
using MKTFY.App.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.App.Seeds
{
    public static class CategorySeeder
    {
        public static async Task InitialCategory(ICategoryRepository catRepo) //ICategoryRepository
        {
            var household = await catRepo.FindCatByName("Real Estate");
            if (household == null)
            {
                await catRepo.AddCat("Real Estate");
            }

            var elect = await catRepo.FindCatByName("Electronics");
            if (elect == null)
            {
                await catRepo.AddCat("Electronics");
            }

            var auto = await catRepo.FindCatByName("Cars & Vehicles");
            if (auto == null)
            {
                await catRepo.AddCat("Cars & Vehicles");
            }

            // We may need to include deals under the category
            //var deal = cat.FindCatByName("Deals");
            //if (deal == null)
            //{
            //    await cat.AddCat("Deals");
            //}

            //await cat.AddCat("Real Estate");
            //await cat.AddCat("Deals");
            //await cat.AddCat("Cat & Vehicles");
            //await cat.AddCat("Electronics");
            ////
            //await cat.AddCat("Appliances");
        }
    }
}
