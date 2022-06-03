namespace Heliconia.WebApp.Controllers.Categories
{
    public class AddChildCategoryRequest
    {
        public string Name { get; set; }

        public string ParentCategoryId { get; set; }
    }
}