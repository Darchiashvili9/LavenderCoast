#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LearnMathRu_0._1.Models;
using System.Data;
using Microsoft.Data.SqlClient;

namespace LearnMathRu_0._1.Controllers
{
    public class ProductsController : Controller
    {
        private readonly LavandaDB _context;

        public ProductsController(LavandaDB context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            //ამის მიხედვით უნდა გაკეთდეს გრიდში გამოტანა ბაზის მონაცემების;
            return View(await _context.Product.ToListAsync());
        }

        //public List<string> GetProducts()
        //{
        //    List<string> tmp = _context.Product.Select(o => o.OrderReceiveDate).Where(o=>o!=null).ToList();
        //    return tmp;
        //    //return (IQueryable<MyClass>)db.MyClasses.Select(m => m.date.ToUniversalTime());
        //}

        [HttpGet]
        public async Task<IActionResult> VisitBookedDatesGet()
        {
            //string select =
            //    "SELECT * " +
            //    "FROM u1572292_George.PRODUCT " +
            //    "WHERE VisitDate IS NOT NULL " +
            //    "AND [OrderReceiveTime] IS NOT NULL	" +
            //    "AND (Select [dbo].[GetQuentityByDate](VisitDate,'1'))>14 " +
            //    "AND (Select [dbo].[GetQuentityByDate](VisitDate,'2'))>199 " +
            //    "AND (Select [dbo].[GetQuentityByDate](VisitDate,'3'))>14 ";
            //try
            //{
            //    var dates = _context.Product.FromSqlRaw(select).ToList();
            //    var list= dates.Select(s=>s.VisitDate).Distinct().ToList();
            //    return Json(list);
            //}

            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}


            string StoredProc = "exec [dbo].[SP_GETDATES]";
            try
            {
                List<Product> jsonRes = await _context.Product.FromSqlRaw(StoredProc).ToListAsync();

                List<string> badDates = new List<string>();
                foreach (var item in jsonRes)
                {
                    badDates.Add(item.VisitDate.ToString());
                }

                badDates.Sort();
                return Json(badDates);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> GetTakenTimesByDate([FromBody]string orderDate)
        {
            //ამ პროცედურებში გასაწერი იქნება რო კონკრეტულად 1 კვირის თარიღები წამოიღოს ყოველ ჯერზე მხოლოდ
            //ანუ მიმდინარე თარიღი უნდა გადაეცეს და პროცედურაში დამუშავდეს რო +7 დღის მონაცემები წამოიღოს მხოლოდ;

            string StoredProc = "exec [dbo].[sp_getTakenTimesByDate] " +
                         "@orderDate = " + "'" + orderDate + "'";
            try
            {
                //var param1 = new SqlParameter();
                //param1.ParameterName = "@orderDate";
                //param1.Value = orderDate;

                List<Product> jsonRes = await _context.Product.FromSqlRaw(StoredProc).ToListAsync();

                //List<string> TakenTimes = new List<string>();
                //foreach (var item in jsonRes)
                //{
                //    TakenTimes.Add(item.OrderReceiveTime);
                //}

                //ViewBag.TakeTimes = "utro";


                return Json(/*TakenTimes*/jsonRes);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return NoContent();
        }

        //MASTER-CLASS
        [HttpGet]
        public async Task<IActionResult> MasterClassBookedDatesGet()
        {
            string StoredProc = "exec [dbo].[SP_GetMasterClassDates]";
            try
            {
                List<Product> jsonRes = await _context.Product.FromSqlRaw(StoredProc).ToListAsync();

                List<string> badDates = new List<string>();
                foreach (var item in jsonRes)
                {
                    badDates.Add(item.MasterclassDate.ToString());
                }

                badDates.Sort();
                return Json(badDates);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> getTakenTimesByDateMasterClass([FromBody] string orderDate)
        {
            //ამ პროცედურებში გასაწერი იქნება რო კონკრეტულად 1 კვირის თარიღები წამოიღოს ყოველ ჯერზე მხოლოდ
            //ანუ მიმდინარე თარიღი უნდა გადაეცეს და პროცედურაში დამუშავდეს რო +7 დღის მონაცემები წამოიღოს მხოლოდ;

            string StoredProc = "exec [dbo].[sp_getTakenTimesByDateMasterClass] " +
                         "@orderDate = " + "'" + orderDate + "'";
            try
            {
                //var param1 = new SqlParameter();
                //param1.ParameterName = "@orderDate";
                //param1.Value = orderDate;

                List<Product> jsonRes = await _context.Product.FromSqlRaw(StoredProc).ToListAsync();

                //List<string> TakenTimes = new List<string>();
                //foreach (var item in jsonRes)
                //{
                //    TakenTimes.Add(item.OrderReceiveTime);
                //}

                //ViewBag.TakeTimes = "utro";


                return Json(/*TakenTimes*/jsonRes);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return NoContent();
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            Product prod = new Product();

            return PartialView(prod);
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,ProductPrice,ProductQuantity")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("ProductId,ProductName,ProductPrice,ProductQuantity")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int? id)
        {
            return _context.Product.Any(e => e.ProductId == id);
        }
    }
}
