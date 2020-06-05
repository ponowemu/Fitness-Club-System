using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Trimfit.Data;
using Trimfit.Models;
using Trimfit.Models.ModelView;

namespace Trimfit.Controllers
{
    public class VoucherController : Controller
    {
        private readonly IApiContext _apiContext;
        public VoucherController(IApiContext apiContext)
        {
            _apiContext = apiContext;
        }

        public async Task<IActionResult> List()
        {
            ViewData["Header"] = "Lista dostępnych karnetów";
            var list = await _apiContext.GetRequest("Vouchers/");
            var voucherList = JsonConvert.DeserializeObject<IEnumerable<Voucher>>(list.Value.ToString());

            List<VoucherView> vouchersView = new List<VoucherView>();

            foreach (var vc in voucherList)
            {
                if (vc.Activitiy_Id != null && vc.Activitiy_Id.Count > 0)
                {
                    foreach (var ac in vc.Activitiy_Id)
                    {
                        if (ac != 0)
                            vc.Activities.Add(await GetActivity(ac));
                    }
                }
            }
            return View(voucherList);
        }

        public async Task<JsonResult> AddNewVoucher(Voucher voucher)
        {
            var res = await _apiContext.PostRequest("Vouchers/", voucher);
            if (res.Value.ToString() == "400")
            {
                Response.StatusCode = 404;
                return new JsonResult("Nie można dodać nowego karnetu.");
            }
            return res;
        }
        public async Task<JsonResult> UpdateVoucher(Voucher voucher)
        {
            var res = await _apiContext.PutRequest("Vouchers/" + voucher.Voucher_Id + "/", voucher);
            return res;
        }
        public async Task<IActionResult> Manage()
        {
            ViewData["Header"] = "Zarządzaj karnetami";

            var list = await _apiContext.GetRequest("Vouchers/");
            var voucherList = JsonConvert.DeserializeObject<IEnumerable<Voucher>>(list.Value.ToString());

            List<VoucherView> vouchersView = new List<VoucherView>();

            foreach (var vc in voucherList)
            {
                if (vc.Activitiy_Id != null && vc.Activitiy_Id.Count > 0)
                {
                    foreach (var ac in vc.Activitiy_Id)
                    {
                        if (ac != 0)
                            vc.Activities.Add(await GetActivity(ac));
                    }
                }
            }
            return View(voucherList);
        }
        public async Task<IEnumerable<VoucherCustomer>> GetVoucherCustomers(int voucherId)
        {
            var voucher = await _apiContext.GetRequest("VoucherCustomers/" + voucherId + "/Customers/");
            var vc = JsonConvert.DeserializeObject<IEnumerable<VoucherCustomer>>(voucher.Value.ToString());

            return vc;
        }
        public async Task DeleteAsync(int id)
        {
            await _apiContext.DeleteRequest("Vouchers/" + id);
        }
        public async Task<JsonResult> GetVoucher(int voucherId)
        {
            var voucher = await _apiContext.GetRequest("Vouchers/" + voucherId + "");
            var vc = JsonConvert.DeserializeObject<Voucher>(voucher.Value.ToString());

            var voucherTypes = await _apiContext.GetRequest("VoucherTypes/");
            var vt = JsonConvert.DeserializeObject<IEnumerable<VoucherType>>(voucherTypes.Value.ToString());

            var activities = await _apiContext.GetRequest("Activities/");
            var ac = JsonConvert.DeserializeObject<IEnumerable<Activity>>(activities.Value.ToString());


            return new JsonResult(new
            {
                voucher = vc,
                voucherTypes = vt,
                activities = ac
            });
        }
        public async Task<JsonResult> DeleteConnection(int id)
        {
            return await _apiContext.DeleteRequest("VoucherCustomers/" + id);
        }
        public async Task<JsonResult> AssignCustomer(int customer_id, int voucher_id, DateTime start)
        {
            var el = new VoucherCustomer
            {
                Added = start,
                Voucher_Id = voucher_id,
                Customer_Id = customer_id,
                IsActive = true
            };

            var voucher = await _apiContext.GetRequest("Vouchers/" + voucher_id);
            var vc = JsonConvert.DeserializeObject<Voucher>(voucher.Value.ToString());

            if (vc != null)
            {
                el.Voucher_Customer_Suspend_Number = vc.Voucher_Max_Suspend_Times; // ustawiamy licznik na możliwą liczbę zamrożeń
                if (vc.Voucher_Startdate != null)
                    if (start < vc.Voucher_Startdate)
                    {
                        Response.StatusCode = 404;
                        return new JsonResult("Okres rozpoczęcia karnetu przypada przed zadeklarowaną datą!");
                    }

                if (vc.Voucher_Type_Id == 1)
                {
                    // open
                    if (vc.VoucherDaysNumber.HasValue && vc.VoucherDaysNumber > 0)
                    {
                        var diff = start.AddDays(vc.VoucherDaysNumber.Value);
                        if (vc.Voucher_Enddate != null)
                        {
                            if (diff > vc.Voucher_Enddate)
                            {
                                Response.StatusCode = 404;
                                return new JsonResult("Okres zakończenia karnetu przypada po określonym limicie!");
                            }
                        }
                        el.VoucherEndDate = diff;
                        el.FreeEntries = vc.Voucher_Entries_Number;
                    }
                    if (vc.Voucher_Entries_Number.HasValue && vc.Voucher_Entries_Number > 0)
                    {
                        el.FreeEntries = vc.Voucher_Entries_Number;
                    }
                }
                else if (vc.Voucher_Type_Id == 2)
                {
                    if (vc.VoucherDaysNumber.HasValue && vc.VoucherDaysNumber > 0)
                    {
                        var diff = start.AddDays(vc.VoucherDaysNumber.Value);
                        if (vc.Voucher_Enddate != null)
                        {
                            if (diff > vc.Voucher_Enddate)
                            {
                                Response.StatusCode = 404;
                                return new JsonResult("Okres zakończenia karnetu przypada po określonym limicie!");
                            }
                        }
                        el.VoucherEndDate = diff;
                        el.FreeEntries = vc.Voucher_Entries_Number;
                    }

                    // okresowy
                }
                else if (vc.Voucher_Type_Id == 3)
                {
                    // ilościowy
                    if (vc.Voucher_Entries_Number.HasValue)
                        el.FreeEntries = vc.Voucher_Entries_Number;
                }
                var result = await _apiContext.PostRequest("VoucherCustomers", el);
                if(result.Value.ToString() == "400")
                {
                    Response.StatusCode = 404;
                    return new JsonResult("Wystąpił błąd podczas przypisywanie vouchera. Zweryfikuj poprawność danych");
                }
                return result;
            }
            else
                return new JsonResult("Nie odnalazłem takiego karnetu w systemie.");

        }
        private async Task<Activity> GetActivity(int id)
        {
            var ac = await _apiContext.GetRequest("Activities/" + id + "");
            var activity = JsonConvert.DeserializeObject<Activity>(ac.Value.ToString());
            return activity;
        }
        public async Task<IActionResult> Add()
        {
            var result = await _apiContext.GetRequest("VoucherTypes/");
            var activities = await _apiContext.GetRequest("Activities/");

            ViewData["voucherTypes"] = JsonConvert.DeserializeObject<IEnumerable<VoucherType>>(result.Value.ToString());
            ViewData["activities"] = JsonConvert.DeserializeObject<IEnumerable<Activity>>(activities.Value.ToString());

            return View();
        }
    }
}