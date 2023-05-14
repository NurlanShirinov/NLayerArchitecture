using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using NLayer.API.Filters;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{

    public class CategoriesController : CustomBaseController
    {

        private readonly ICategoryService _scategoryService;

        public CategoriesController(ICategoryService scategoryService)
        {
            _scategoryService = scategoryService;
        }


        // api/categories/GetSingleCategoryByIdWithProducts/2
        [HttpGet("[action]")]
        public async Task<IActionResult> GetSingleCategoryByIdWithProducts(int categoryId)
        {
            return CreateActionResult(await _scategoryService.GetSingleCategoryByIdWithProductsAsync(categoryId));
        }

    }
}
