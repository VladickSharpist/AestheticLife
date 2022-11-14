// using AestheticLife.Auth.Services.Abstractions.Interfaces;
// using System.IO;  
// using System.Net;  
// using System.Net.Mail;
// using System.Text;
// using AestheticLife.Auth.Services.Abstractions.Models;
// using AestheticLife.Core.Abstractions.Helpers;
// using AestheticLife.DataAccess.Domain.Models;
// using AestheticLife.DataAccess.Domain.User.Models;
// using Microsoft.AspNetCore.Identity;
// using MimeKit;
//
// namespace AestheticLife.Auth.Services.Implementations;
//
// public class EmailService : IEmailService
// {
//     private const string MAIL_LETTER_SENDER = "AestheticsLife2022@gmail.com";
//     private const string PATH_TO_ACTION = "/api/Account/EmailConfirmation";
//     private const string MESSAGE_SUBJECT = "AetheticLife confirmation email";
//     private IConfigurationHelper _helper;
//     
//     private UserManager<ApplicationUser> _userManager;
//
//
//     public EmailService(
//         UserManager<ApplicationUser> userManager,
//         IConfigurationHelper helper)
//     {
//         _userManager = userManager;
//         _helper = helper;
//     }
//
//     public async Task SendEmailAsync(ConfirmUserEmailDto modelDto)
//     {
//         //token надо захешировать
//         //сделать максимальную абстракцию
//         var user = await _userManager.FindByIdAsync(modelDto.Id);
//         var letterRecipient = $"{user.Email}";
//         var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
//         var link = $"{_helper.Localhost}{PATH_TO_ACTION}?token={token}&userId={modelDto.Id}";
//         var body = $"<div>Hello,{user.Name} follow the link to confirm your email: <a href=\"{link}\">Click here</a></div>";
//
//         using var message = GetConfiguredMailMessage(body, letterRecipient);
//         using var client = GetConfiguredSmtpClient();
//         client.Send(message);
//     }
//
//     public async Task<bool> ConfirmEmail(string userId, string token)
//     {
//         var user = await _userManager.FindByIdAsync(userId);
//         var result = await _userManager.ConfirmEmailAsync(user, token);
//
//         return result.Succeeded;
//     }
//
//
//     private MailMessage GetConfiguredMailMessage(string body, string letterRecipient)
//         => new(MAIL_LETTER_SENDER, letterRecipient)
//         {
//             BodyEncoding = Encoding.UTF8,
//             IsBodyHtml = true,
//             Subject = MESSAGE_SUBJECT,
//             Body = body,
//         };
//
//
//     private SmtpClient GetConfiguredSmtpClient(string host = "smtp.gmail.com", int port = 587)
//         => new(host, port)
//         {
//             EnableSsl = true,
//             UseDefaultCredentials = false,
//             Credentials = new NetworkCredential("AestheticsLife2022", "oikuafhqbrawbyrb")
//         };
// }
