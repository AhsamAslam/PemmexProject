using PemmexCommonLibs.Domain.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailServices.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailRequest mailRequest);
    }
}
