
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


    #region Get Category with Products By Id
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
            Discount= c.Discount
           
        });

        return productsInThisCategory;
        
    }

    #endregion


    #region Get All Categories with products
    public IEnumerable<CategoryDetailsDto> GetAllCategoriesWithProducts()
    {
        IEnumerable<Category>? categoriesFromDb = _unitOfWork.CategoryRepo.GetAllCategoriesWithAllProducts();
        IEnumerable<CategoryDetailsDto> CategoriesDto = categoriesFromDb
            .Select(c => new CategoryDetailsDto
            {
                Id = c.Id,
                Name = c.Name,
                Products = c.Products.Select(p => new ProductChildDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Image = p.Image, 
                    Discount= p.Discount
                }).ToList()

            });
        return CategoriesDto;

    }
    #endregion
}









