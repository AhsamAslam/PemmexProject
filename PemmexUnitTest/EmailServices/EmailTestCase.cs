using EmailServices.Controllers;
using EmailServices.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PemmexCommonLibs.Application.Interfaces;
using PemmexCommonLibs.Domain.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PemmexUnitTest.EmailService
{
    [TestClass]
    public class EmailTestCase
    {
        private readonly IEmailService mailService;
        private readonly ILogService _logService;
        EmailController emailController;
        public EmailTestCase()
        {
            emailController = new EmailController(mailService,_logService);
        }

        #region Send Test Case
        [TestMethod]
        public void Send_Test1()
        {
            EmailRequest emailRequest = new EmailRequest();
            emailRequest.ToEmail = "1111";
            emailRequest.Subject = "12233";
            emailRequest.Body = "11223";
            emailRequest.Attachments = null;

            Task<IActionResult> result = emailController.Send(emailRequest) as Task<IActionResult>;

            Assert.IsNotNull(result);
        }
        #endregion
    }
}
