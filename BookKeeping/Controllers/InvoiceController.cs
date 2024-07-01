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

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 發票匯入頁面
        /// </summary>
        /// <returns></returns>
        public IActionResult Add()
        {     
            //取得明細分類列表
            var CategoryList = _invoiceService.getCategory();

            //組成SelectList傳至View
            ViewBag.Category = new SelectList(CategoryList, "Value", "Text", 0);

            return View();
        }

        /// <summary>
        /// 實際儲存發票資料功能
        /// </summary>
        /// <param name="Invoices"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 讀取CSV檔案
        /// </summary>
        /// <returns></returns>
        [HttpPost("ReadFile")]
        public IActionResult ReadFile(IFormFileCollection files)
        {
            //讀取CSV的方法
            List<Invoice> invoices = _invoiceService.ReadCSV(files);
            
            return Json(invoices);
        }

        /// <summary>
        /// 檢視列表
        /// </summary>
        /// <returns></returns>
        public IActionResult GetAll()
        {
            List<Invoice> model= _invoiceService.GetAll();

            //取得明細分類列表
            var CategoryList = _invoiceService.getCategory();

            //組成SelectList傳至View
            ViewBag.Category = new SelectList(CategoryList, "Value", "Text", 0);

            return View(model);
        }

        public IActionResult Edit()
        {

            return View();
        }

        public IActionResult Delete()
        {
            return View();
        }
    }
}
