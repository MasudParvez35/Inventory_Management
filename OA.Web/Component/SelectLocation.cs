using Microsoft.AspNetCore.Mvc;

namespace OA_WEB.Component;

public class SelectLocationViewComponent : ViewComponent
{
    public SelectLocationViewComponent()
    {
        
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View();
    }
}
