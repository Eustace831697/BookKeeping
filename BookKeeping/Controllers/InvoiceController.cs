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

        //發票匯入頁面
        public IActionResult Add()
        {
            //取得明細分類列表
            var CategoryList = _invoiceService.getCategory();

            //組成SelectList傳至View
            ViewBag.Category = new SelectList(CategoryList, "Value", "Text", 0);

            return View();
        }

        //call Api儲存發票
        [HttpPost("AddInvoice")]
        public IActionResult AddInvoice(List<Invoice> Invoices)
        {
            string rtn = _invoiceService.Add(Invoices);

            if (!string.IsNullOrEmpty(rtn))
            {
                return BadRequest(rtn);
            }
            return Ok();
        }

        //Add頁面讀取CSV檔案        
        [HttpPost("ReadFile")]
        public IActionResult ReadFile(IFormFileCollection files)
        {
            //讀取CSV的方法
            List<Invoice> invoices = _invoiceService.ReadCSV(files);

            return Json(invoices);
        }

        //取得全部資料
        public IActionResult GetAll()
        {
            List<Invoice> model = _invoiceService.GetAll();            

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(Guid ID)
        {
            Invoice model = _invoiceService.GetByID(ID)[0];

            var CategoryList = _invoiceService.getCategory();
            ViewBag.Category = new SelectList(CategoryList, "Value", "Text", 0);           

            return View(model);
        }

        [HttpPost("UpdateInvoice")]
        //更新資料的api
        public IActionResult UpdateInvoice(Invoice invoice)
        {
            string rtn = _invoiceService.Update(invoice);

            return Json(rtn); 
        }

        [HttpGet]
        public IActionResult Delete(Guid ID)
        {
            string rtn = _invoiceService.Delete(ID);

            return Json(rtn);
        }
    }
}
