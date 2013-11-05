using System.Web.Mvc;
using SPHealthChequeConverter.Helpers;
using SPHealthChequeConverter.Models;

namespace SPHealthChequeConverter.Controllers
{
    public class HomeController : Controller
    {
        private readonly IChequeValueConverter _chequeValueConverter;

        public HomeController(IChequeValueConverter chequeValueConverter)
        {
            _chequeValueConverter = chequeValueConverter;
        }

        public ActionResult Index()
        {            
            return View();
        }

        [HttpPost]
        public ActionResult Create(ChequeSubmitDetails chequeSubmitDetails)
        {
            if (!ModelState.IsValid)
                return View("Index", chequeSubmitDetails);

            var chequeDisplayDetails = new ChequeDisplayDetails
            {
                Amount = chequeSubmitDetails.Amount,
                Date = chequeSubmitDetails.Date,
                Name = chequeSubmitDetails.Name,
                AmountInWords = _chequeValueConverter.Convert(chequeSubmitDetails.Amount)
            };

            return View(chequeDisplayDetails);
        }

    }
}
