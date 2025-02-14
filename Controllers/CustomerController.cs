using System.Threading.Tasks;
using ConstructorApp.Models;
using ConstructorApp.Repository;
using ConstructorApp.Services;
using Microsoft.AspNetCore.Mvc;
using Models.ViewModels;
namespace ConstructorApp.Controllers
{
    public class CustomerController(ICustomerService customerService, IUnitOfWork unitOfWork) : BaseController
    {
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 10;
            var (pagedData, totalPages) = await customerService.GetPagedAsync(page, pageSize);
            var customerViewModel = new CustomerViewModel
            {
                PaginationModel = new PaginationModel
                {
                    CurrentPage = page,
                    TotalPages = totalPages,
                    PageUrl = "/Customer/Index"
                },
                Customers = pagedData  // Önemli: Tüm müşteriler yerine sayfalanmış veriyi kullanıyoruz
            };

            return View(customerViewModel);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                customerService.AddAsync(customer);
                await unitOfWork.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(customer);
        }
        public async Task<IActionResult> Details(int id)
        {
            var customer =await customerService.GetByIdAsync(id);
            return View(customer);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(Customer customer)
        {
            if (ModelState.IsValid)
            {
                await customerService.UpdateAsync(customer);
                await unitOfWork.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(customer);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await customerService.GetByIdAsync(id);
            await customerService.DeleteAsync(customer.Id);
            await unitOfWork.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}