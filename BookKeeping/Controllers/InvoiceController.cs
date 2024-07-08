using Microsoft.AspNetCore.Mvc;
using BookKeeping.Models;
using BookKeeping.Repository.Dtos;
using BookKeeping.Service.Implement;
using BookKeeping.Service.Interface;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;

namespace BookKeeping.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            this._invoiceService = invoiceService;
        }

        [HttpGet("[Controller]/[action]")]
        public IActionResult Create()
        {
            //取得明細分類列表
            var CategoryList = _invoiceService.getCategory();

            //組成SelectList傳至View
            ViewBag.CategoryList = new SelectList(CategoryList, "Value", "Text", 0);

            return View();
        }

        [HttpGet("[controller]/{ID}")]
        public IActionResult Edit(Guid ID)
        {
            Invoice model = _invoiceService.GetByID(ID)[0];

            var CategoryList = _invoiceService.getCategory();
            ViewBag.Category = new SelectList(CategoryList, "Value", "Text", 0);

            return View(model);
        }
 
        [HttpPost("[controller]")]
        public IActionResult Add(List<Invoice> Invoices)
        {
            string rtn = _invoiceService.Add(Invoices);

            if (!string.IsNullOrEmpty(rtn))
            {
                return BadRequest(rtn);
            }
            return Ok();
        }

        [HttpGet("[controller]")]
        public IActionResult GetAll()
        {
            List<Invoice> model = _invoiceService.GetAll();

            return View(model);
        }

        [HttpPut("[controller]")]
        public IActionResult Update(Invoice invoice)
        {
            string rtn = _invoiceService.Update(invoice);

            return Json(rtn);
        }

        [HttpDelete("[controller]")]
        public IActionResult Delete(Guid ID)
        {
            string rtn = _invoiceService.Delete(ID);

            return Json(rtn);
        }

        [HttpPost("ReadFile")]
        public IActionResult ReadFile(IFormFileCollection files)
        {
            //讀取CSV的方法
            List<Invoice> invoices = _invoiceService.ReadCSV(files);

            return Json(invoices);
        }        
    }
}
