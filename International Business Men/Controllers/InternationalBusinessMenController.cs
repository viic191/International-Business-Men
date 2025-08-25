using International_Business_Men.business;
using International_Business_Men.Class;
using Microsoft.AspNetCore.Mvc;
namespace International_Business_Men.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class InternationalBusinessMenController : ControllerBase
    {

        private readonly ILogger<InternationalBusinessMenController> _logger;
        private business.business Business = new business.business();
        public InternationalBusinessMenController(ILogger<InternationalBusinessMenController> logger)
        {
            _logger = logger;
        

           
        }

        [HttpGet(Name = "GetTransactions")]
        public List<transactions> GetTransactions()
        {
            return Business.ReadTransactions();

        }

        [HttpGet(Name = "GetRates")]
        public List<rates> GetRates()
        {
            return Business.ReadRates();

        }
        [HttpGet(Name = "DetailSKu")]
        public List<transactionsEur> DetailSku()
            {

            return Business.DetailSKu();

            }

    }
}
