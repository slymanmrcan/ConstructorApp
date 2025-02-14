using System.Threading.Tasks;
using ConstructorApp.Models;
using ConstructorApp.Repository;
using Microsoft.AspNetCore.Mvc;
using Models.ViewModels;
namespace ConstructorApp.Controllers
{
    public class CustomerController(ICustomerRepository customerRepository, IUnitOfWork unitOfWork) : BaseController
    {
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 10;
            var customers = await customerRepository.GetAllAsync();
            var pagedData = customers.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var totalPages = (int)Math.Ceiling(customers.Count() / (double)pageSize);

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
        public async Task<IActionResult> Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                customerRepository.AddAsync(customer);
                await unitOfWork.SavaChagensAsync();
                return RedirectToAction("Index");
            }
            return View(customer);
        }
        public async Task<IActionResult> Details(int id)
        {
            var customer =await customerRepository.GetByIdAsync(id);
            return View(customer);
        }
        [HttpPost]
        public async Task<IActionResult> Details(Customer customer)
        {
            if (ModelState.IsValid)
            {
                await customerRepository.UpdateAsync(customer);
                await unitOfWork.SavaChagensAsync();
                return RedirectToAction("Index");
            }
            return View(customer);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await customerRepository.GetByIdAsync(id);
            await customerRepository.DeleteAsync(customer);
            await unitOfWork.SavaChagensAsync();
            return RedirectToAction("Index");
        }
    }
}