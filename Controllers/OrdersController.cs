#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LearnMathRu_0._1.Models;
using Yandex.Checkout.V3;
using Yandex.Checkout.V3.Demo;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using System.Text;
using Newtonsoft.Json.Linq;

namespace LearnMathRu_0._1.Controllers
{
    public class OrdersController : Controller
    {
        private readonly LavandaDB _context;
        ConfirmModel confirmModel;

        public OrdersController(LavandaDB context)
        {
            _context = context;
            confirmModel = new ConfirmModel();
        }

        // GET: Orders
        public IActionResult Index()
        {
            return View();
        }

        // GET: Orders/Details/5
        public IActionResult Details(int id)
        {
            //მესიჯს ვიღებთ გადახდის შესახებ
            var data = PaymentStorage.Payments[id];
            confirmModel.Id = id;
            var payment = data.Client.GetPayment(data.Payment.Id);
            ProcessPayment(payment);

            //ვამოწმებთ დაბრუნებულ მესიჯს- გადაიხადა თუ არა მომხმარებელმა ფული?
            var myJsonString = confirmModel.Payment;
            var myJObject = JObject.Parse(myJsonString);
            var checkPaymentStatus = (myJObject.SelectToken("status").Value<string>());
            var checkIfPayed = (myJObject.SelectToken("paid").Value<string>());


            if (checkPaymentStatus == "waiting_for_capture" && checkIfPayed == "True")
            {
                //თუკი გადახდილია სწორედ ყველაფერი მაშინ ბაზაში ვააფდეითებთ რომ მომხმარებელმა გადაიხადა წარმატებით;
                _context.Order.OrderByDescending(o => o.Id).FirstOrDefault().CustPayment = true;
                _context.SaveChanges();

                ViewBag.PaymentStatus = "Успешно отправлено!";
                ViewBag.PaymentMessage = "Ближайшее время наш менджер свяжеться с вами для подтверждения заказа";

                return View();
            }
            else if (checkPaymentStatus == "pending" && checkIfPayed == "False" || checkPaymentStatus == "canceled" && checkIfPayed == "False")
            {
                //ასევე ვააფდეითებთ ბაზას;
                _context.Order.OrderByDescending(o => o.Id).FirstOrDefault().CustPayment = false;
                _context.SaveChanges();

                ViewBag.PaymentStatus = "Произошла ошибка";
                ViewBag.PaymentMessage = "Пожалуйста, сделайте заказ по номеру";
                return View();
            }

            return Redirect("https://localhost:7115");
            //   return View();
        }

        protected void ProcessPayment(Payment payment)
        {
            confirmModel.Payment = Serializer.SerializeObject(payment);

            confirmModel.AllowConfirm = payment.Paid && payment.Status == PaymentStatus.WaitingForCapture;

            var sb = new StringBuilder();
            if (payment.Paid == false)
                sb.AppendLine("Not paid");
            if (payment.Status != PaymentStatus.WaitingForCapture)
                sb.AppendFormat("Status: {0}", payment.Status).AppendLine();
            if (payment.Status == PaymentStatus.Canceled)
                sb.AppendFormat("Payment canceled: {0} at {1}", payment.CancellationDetails.Reason,
                    payment.CancellationDetails.Party).AppendLine();

            confirmModel.Message = sb.ToString();
        }

        public IActionResult CreateProd()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "CustomerAddress");
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "ProductName");

            return PartialView("CreateProdPartialView");
        }

        public IActionResult CreateVisit()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "CustomerAddress");
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "ProductName");

            var list = new List<string>();
            list.Add("morningTime");
            list.Add("dayTime");
            ViewBag.timeChoose = list;


            return PartialView("CreateVisitPartialView");
        }


        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "CustomerAddress");
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "ProductName");
            return View();
        }

        public bool SaveToDb(Order order)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(order);
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    ViewBag.PaymentStatus = ex.Message;
                    ViewBag.PaymentMessage = ex.Data;
                    //  return View("Details");
                }
            }
            return false;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Order order)
        {
            bool checkIfSaved = this.SaveToDb(order);

            //bazashia sanaxavi rato ar inaxavs, სავარაუდოდ რაღაც ველებს აქვთ პრობლემა;
            if (checkIfSaved)
            {
                try
                {
                    var client = new Client(new NewPaymentModel().ShopId, new NewPaymentModel().SecretKey);
                    var id = PaymentStorage.GetNextId();

                    var url = Request.GetUri();

                    // ეს ალტერნატივად შეიძლება გამოდგეს აიდის მაგივრად;
                    // _context.Order.OrderByDescending(o => o.OrderId).FirstOrDefault().OrderId
                    // id ewera aq tavidan
                    var redirect = $"{url.Scheme}://{url.Authority}/Orders/Details/{id}";

                    var data = client.CreatePayment(
                        new NewPayment()
                        {
                            Amount = new Amount()
                            {
                                Value = order.Product.ProductPrice,
                            },
                            Confirmation = new Confirmation()
                            {
                                Type = ConfirmationType.Redirect,
                                ReturnUrl = redirect
                            },
                            Description = "Order"
                        });

                    PaymentStorage.Payments[id] = new QueryData() { Client = client, Payment = data };

                    return Redirect(data.Confirmation.ConfirmationUrl);

                }
                catch (Exception ex)
                {
                    ViewBag.PaymentStatus = ex.Message;
                    ViewBag.PaymentMessage = ex.Data;
                    return View("Details");
                }
            }
            return View("Details");
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "CustomerAddress", order.CustomerId);
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "ProductName", order.ProductId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,CustomerId,ProductId,OrderReceiveDate,OrderReceiveTime,CustPayment")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
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
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "CustomerAddress", order.CustomerId);
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "ProductName", order.ProductId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Customer)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var order = await _context.Order.FindAsync(id);
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int? id)
        {
            return _context.Order.Any(e => e.Id == id);
        }


        public async Task<IActionResult> DataBaseView()
        {
            //SELECT*
            //FROM u1572292_George.[CUSTOMER] as cust
            //JOIN u1572292_George.[ORDER] as ord
            //ON cust.CustomerId = ord.CustomerId
            //LEFT JOIN u1572292_George.[PRODUCT] as prod
            //ON prod.ProductId = ord.ProductId

            var query =
                       from ord in _context.Order
                       join cust in _context.Customer
                       on ord.CustomerId equals cust.CustomerId
                       join prod in _context.Product
                       on ord.ProductId equals prod.ProductId
                       orderby ord.orderDate descending
                       select new { ord, cust, prod };

            DatabaseModel dbModel = new DatabaseModel();

            dbModel.ord = query.Select(o => o.ord).ToList();
            dbModel.cust = query.Select(c => c.cust).ToList();
            dbModel.prod = query.Select(p => p.prod).ToList();

            return View(dbModel);
        }
    }
}
