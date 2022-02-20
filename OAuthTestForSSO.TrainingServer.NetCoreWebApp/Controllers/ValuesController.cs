using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace EncryptionServer.NetCoreWebApp.Controllers
{
    [ApiController]
    public class ValuesController : ControllerBase
    {
        /// <summary>
        ///     Self test
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/SelfTest")]
        public ActionResult<string> Get()
        {
            return "VTE SYSTEM IS READY";
        }


        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/DoMagic")]
        public ActionResult<string> DoMagic()
        {
            string result = String.Empty;
            IQueryCollection queryCollection = HttpContext.Request.Query;

            foreach (KeyValuePair<string, StringValues> keyValuePairs in queryCollection)
            {
                result += keyValuePairs.Key + "=" + keyValuePairs.Value + "\n";
            }

            return "SELF TEST:\n" + result;
        }


        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/DoMagic2")]
        public ActionResult<string> DoMagic2()
        {
            string result = String.Empty;
            IQueryCollection queryCollection = HttpContext.Request.Query;

            foreach (KeyValuePair<string, StringValues> keyValuePairs in queryCollection)
            {
                result += keyValuePairs.Key + "=" + keyValuePairs.Value + "\n";
            }

            return "SELF TEST 2:\n" + result;
        }

        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Handshake")]
        public ActionResult<string> Handshake()
        {
            IFormCollection formCollection = HttpContext.Request.Form;

            foreach (KeyValuePair<string, StringValues> keyValuePairs in formCollection)
            {
                string value = String.Empty;
                if (keyValuePairs.Key == "Authorization")
                {
                    value = keyValuePairs.Value.ToString();
                }
                else
                if (keyValuePairs.Key == "grant_type")
                {
                    value = keyValuePairs.Value.ToString();
                }
            }

            return "100500!";
        }

        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/SecureRequest")]
        public ActionResult<string> SecureRequest()
        {
            string result = String.Empty;
            IQueryCollection queryCollection = HttpContext.Request.Query;

            foreach (KeyValuePair<string, StringValues> keyValuePairs in queryCollection)
            {
                result += keyValuePairs.Key + "=" + keyValuePairs.Value + "\n";
            }

            return "SELF TEST 3:\n" + result;
            //IFormCollection formCollection = HttpContext.Request.Form;

            //foreach (KeyValuePair<string, StringValues> keyValuePairs in formCollection)
            //{

            //}

            //return "100500!";
        }
    }
}
