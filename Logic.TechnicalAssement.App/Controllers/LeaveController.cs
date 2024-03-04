using InvestmentManagement.Shared.Extensions;
using Logic.TechnicalAssement.App.Models;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Logic.TechnicalAssement.App.Controllers
{
    public class LeaveController : Controller
    {
        private readonly ILogger<LeaveController> _logger;
        private IHttpContextAccessor HttpContextAccessor;
        private HttpContext _httpContext;

        private List<LeaveViewModel> leaveRequests;
        private string _key = "myleaves";

        public LeaveController(ILogger<LeaveController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            HttpContextAccessor = httpContextAccessor;
            
            leaveRequests = new List<LeaveViewModel>();
            _httpContext = HttpContextAccessor.HttpContext;

            if (_httpContext.Session.Keys.FirstOrDefault() == null)
            {
                _httpContext.Session.SetObject(_key, leaveRequests);
            }
           

            


        }

        [HttpGet]
        public ActionResult GetLeavesApplied()
        {
            
            leaveRequests =  (List<LeaveViewModel>)  HttpContext.Session.GetObject<List<LeaveViewModel>>(_key);
            return PartialView("_LeavesPartial", leaveRequests);
        }

        [HttpPost]
        public ActionResult AddLeave(IFormCollection formCollection)
        {
            string res = "FAIL";
            try
            {

                string? leavetype = formCollection["Leavetype"];
                string? firstname = formCollection["FirstName"];
                string? lastname = formCollection["LastName"];
                string? email = formCollection["Email"];
                string? startdate = formCollection["StartDate"];
                string? enddate = formCollection["EndDate"];

                LeaveViewModel model = new LeaveViewModel();
                model.Id = Utilities.GeneratUniqueId();
                model.FirstName = firstname;
                model.LastName = lastname;
                model.Email = email;
                string[] dateparts = startdate.Split("/");
                model.StartDate = new DateTime(int.Parse(dateparts[2]), int.Parse(dateparts[1]), int.Parse(dateparts[0]));
                dateparts = enddate.Split("/");
                model.EndDate = new DateTime(int.Parse(dateparts[2]), int.Parse(dateparts[1]), int.Parse(dateparts[0]));

                if (model.StartDate == model.EndDate)
                    model.IsHalfDay = true;
                else
                    model.IsHalfDay = false;

                leaveRequests = (List<LeaveViewModel>)HttpContext.Session.GetObject<List<LeaveViewModel>>(_key);


                leaveRequests.Add(model);
                _httpContext.Session.SetObject(_key, leaveRequests);
                res = "PASS";
            }
            catch(Exception ex)
            {

            }
            var rJson = new { Result = res };
             return Json(rJson); 
        }
        public IActionResult Index()
        {

            IList<SelectLeaveType> _leavetypes = EnumExtensions.EnumDetails();

            List<SelectListItem>? leaveTypes = new List<SelectListItem>();
            
             
            foreach (var leavetype in _leavetypes)
            {

                leaveTypes.Add(new SelectListItem { Text=leavetype.DisplayName,Value= leavetype.Name });
               
            };

            ViewBag.leavetypes = leaveTypes;
           
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
