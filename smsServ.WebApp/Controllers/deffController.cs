using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using smsServ.SmsUtils.DBControl;
using smsServ.SmsUtils.Utils;
using smsServ.WebApp.Models;

namespace smsServ.WebApp.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class deffController : ControllerBase
    {
        private string TAG = "deff";
        IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build();
        DBConnection dbconn;
        public deffController()
        {
            dbconn = new DBConnection(config["dbIp1"], config["dbIp2"], config["dbInstance"]);
        }
        [HttpGet]
        public async Task<productModel> Get()
        {
            productModel product = new productModel();
            try
            {
                if (dbconn.idbStatOK())
                {
                    DataTable dt = dbconn.getTable(smsQry.getBund());
                    List<Product> prdList = new List<Product>();

                    foreach(DataRow dr in dt.Rows)
                    {
                        Product prd = new Product();
                        prd.productName = dr["BUNDLE_CODE"].ToString();
                        prd.productCode = dr["PRODUCT_NAME"].ToString();
                        prdList.Add(prd);
                    }
                    product.isSuccess = true;
                    product.resultMessage = "success";
                    product.products = prdList;
                }

            }
            catch (Exception ex)
            {
                product.isSuccess = false;
                product.resultMessage = ex.Message;
                LogWriter._error(TAG, ex.ToString());
            }
            return product;
        }
    }
}