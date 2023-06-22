
namespace Final.Project.BL;

public interface ICategoriesManager
{
    IEnumerable<CategoryDto> GetAllCategoriesDto();
    public CategoryDto? GetCategoryById(int id);

    IEnumerable<ProductChildDto>? GetCategoryWithProducts(int id);
}









