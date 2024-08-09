using Microsoft.AspNetCore.Mvc;

namespace OA_WEB.Controllers
{
    public class OrderController : Controller
    {
        public async Task<IActionResult> List()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View();
        }
    }
}
