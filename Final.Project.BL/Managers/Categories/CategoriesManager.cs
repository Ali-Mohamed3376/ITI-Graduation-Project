
using Final.Project.DAL;

namespace Final.Project.BL;

public class CategoriesManager: ICategoriesManager
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoriesManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    #region GetAllCategories
    public IEnumerable<CategoryDto> GetAllCategoriesDto()
    {
        IEnumerable<Category> categoriesFromDb = _unitOfWork.CategoryRepo.GetAll();
        IEnumerable<CategoryDto> categoriesDto = categoriesFromDb.Select(c => new CategoryDto()
        {
            Name = c.Name,
            Id = c.Id,
        });
        return categoriesDto;
    }


    #endregion


    #region GetCategoryById
    public CategoryDto? GetCategoryById(int id)
    {
        Category? category = _unitOfWork.CategoryRepo.GetById(id);
        if (category is null) { return null; }
        return new CategoryDto()
        {
            Id = category.Id,
            Name = category.Name,

        };

    }
    #endregion


    #region Get Category with Products
    public IEnumerable<ProductChildDto>? GetCategoryWithProducts(int id)
    {
        IEnumerable<Product>? ProductsFromDb = _unitOfWork.CategoryRepo.GetByIdWithProducts(id);
        if (ProductsFromDb is null) { return null; };

        var productsInThisCategory = ProductsFromDb.Select(c => new ProductChildDto
        {

            Id = c.Id,
            Image = c.Image,
            Name = c.Name,
            Price = c.Price,
            CategoryName=c.Category.Name
        });

        return productsInThisCategory;
        
    }

    #endregion


}









  