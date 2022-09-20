using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Organization.API.Commands.SaveCurrencies;
using Organization.API.Dtos;
using PemmexCommonLibs.Application.Helpers;
using PemmexCommonLibs.Application.Interfaces;
using PemmexCommonLibs.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Organization.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CurrencyController : ApiControllerBase
    {
        private readonly ILogService _logService;

        public CurrencyController(ILogService logService)
        {
            _logService = logService;
        }
        [HttpPost]
        public async Task<ActionResult<ResponseMessage>> PostAsync(SaveCurrenciesCommand command)
        {
            try
            {
                var data =  await Mediator.Send(command);
                return new ResponseMessage(true, EResponse.OK, null, data);
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Currency_{CurrentUser.EmployeeIdentifier}");
                return new ResponseMessage(false, EResponse.UnexpectedError, e.Message, null);
            }
        }
        [HttpGet]
        [Route("CurrencyConvert")]
        public async Task<ActionResult<CurrencyDto>> CurrencyConvert(string from, string to)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    try
                    {
                        CurrencyDto dto = new CurrencyDto();
                        client.BaseAddress = new Uri("https://free.currconv.com");
                        var response = await client.GetAsync($"/api/v7/convert?q={from}_{to}&compact=ultra&apiKey=cb08aa78614b13519582");
                        var stringResult = await response.Content.ReadAsStringAsync();
                        var dictResult = JsonConvert.DeserializeObject<Dictionary<string, double>>(stringResult);
                        dto.country = dictResult.Keys.FirstOrDefault();
                        dto.currency = dictResult.Values.FirstOrDefault();
                        return dto;
                    }
                    catch (HttpRequestException httpRequestException)
                    {
                        throw httpRequestException;
                    }
                }

            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Currency_{CurrentUser.EmployeeIdentifier}");
                throw;
            }
        }

        [HttpGet]
        [Route("CurrencyConvert2")]
        public async Task<ActionResult<CurrencyDto>> CurrencyConvert2(string from, string to)
        {
            try
            {
                WebClient web = new WebClient();
                CurrencyDto dto = new CurrencyDto();

                Uri uri = new Uri(string.Format("http://www.google.com/ig/calculator?hl=en&q={2}{0}%3D%3F{1}", from.ToUpper(), to.ToUpper(), 1));
                string response = web.DownloadString(uri);
                Regex regex = new Regex("rhs: \\\"(\\d*.\\d*)");
                Match match = regex.Match(response);
                string test = match.ToString();
                decimal rate = Convert.ToDecimal(match.Groups[1].Value);

                //string URL = "http://www.google.com/finance/converter?a=" + 1 + "&from=" + from + "&to=" + to;
                //byte[] databuffer = Encoding.ASCII.GetBytes("test=postvar&test2=another");
                //HttpWebRequest _webreqquest = (HttpWebRequest)WebRequest.Create(URL);
                //_webreqquest.Method = "POST";
                //_webreqquest.ContentType = "application/x-www-form-urlencoded";
                //_webreqquest.ContentLength = databuffer.Length;
                //Stream PostData = _webreqquest.GetRequestStream();
                //PostData.Write(databuffer, 0, databuffer.Length);
                //PostData.Close();
                //HttpWebResponse WebResp = (HttpWebResponse)_webreqquest.GetResponse();
                //Stream finalanswer = WebResp.GetResponseStream();
                //StreamReader _answer = new StreamReader(finalanswer);
                //string[] value = Regex.Split(_answer.ReadToEnd(), "&nbsp;");


                //int first = value[1].IndexOf("<div id=currency_converter_result>");
                //int last = value[1].LastIndexOf("</span>");

                //string str2 = value[1].Substring(first, last - first);
                return dto;
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Currency_{CurrentUser.EmployeeIdentifier}");
                throw;
            }
        }
        //[HttpPost]
        //public async Task<ActionResult<ResponseMessage>> GetAsync(string organizationIdentifier)
        //{
        //    try
        //    {
        //        var data = await Mediator.Send(command);
        //        return new ResponseMessage(true, EResponse.OK, null, data);
        //    }
        //    catch (Exception e)
        //    {
        //        return new ResponseMessage(false, EResponse.UnexpectedError, e.Message, null);
        //    }
        //}

    }
}
