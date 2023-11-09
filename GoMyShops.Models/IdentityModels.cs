using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
//using Microsoft.AspNet.Identity.EntityFramework;
//using Microsoft.AspNetCore.Identity.Owin;
//using GoMyShops.IdentityExtensions;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
//using Microsoft.Data.Entity;
using Microsoft.EntityFrameworkCore.SqlServer;
//using Microsoft.Owin;
using GoMyShops.Commons;
using System.Net;
using System.Net.Mail;
using GoMyShops;
//using Microsoft.AspNetCore.Http.Authentication;

using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace GoMyShops.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.

    public class AppClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
    {
        public AppClaimsPrincipalFactory(
            UserManager<ApplicationUser> userManager
            , RoleManager<IdentityRole> roleManager
            , IOptions<IdentityOptions> optionsAccessor)
        : base(userManager, roleManager, optionsAccessor)
        { }

        public async override Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
        {
            var principal = await base.CreateAsync(user);
            return principal;
        }
    }


    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            DateTime dt = Convert.ToDateTime(DateTime.Now.ToString(CommonSetting.DateFormatYYYYYMMDDHHNNSS));

            CreateDate = dt;
            IsApproved = false;
            IsActive = true;
            LastLoginDate = DateTime.Now;
            LastActivityDate = DateTime.Now;
            LastPasswordChangedDate = dt;
            LastLockoutDate = DateTime.Parse("1/1/1900");
            FailedPasswordAnswerAttemptWindowStart = DateTime.Parse("1/1/1900");
            FailedPasswordAttemptWindowStart = DateTime.Parse("1/1/1900");
            PreviousUserPasswords = new List<PreviousPassword>();
        }

        public System.DateTime LastActivityDate { get; set; }
        public string? PasswordQuestion { get; set; } 
        public string? PasswordAnswer { get; set; } 
        public bool IsApproved { get; set; }
        public bool IsLockedOut { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime LastLoginDate { get; set; }
        public System.DateTime LastPasswordChangedDate { get; set; }
        public System.DateTime LastLockoutDate { get; set; }
        public int FailedPasswordAttemptCount { get; set; }
        public System.DateTime FailedPasswordAttemptWindowStart { get; set; }
        public int FailedPasswordAnswerAttemptCount { get; set; }
        public System.DateTime FailedPasswordAnswerAttemptWindowStart { get; set; }

        //public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        //{
        //    // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType

        //    //ClaimsPrincipal claimsPrincipal = await _claimsPrincipalFactory.CreateAsync(user);
        //    //((ClaimsIdentity)claimsPrincipal.Identity);

        //    //Todo Harris (Test) Modify Core
        //    var factory = new UserClaimsPrincipalFactory<ApplicationUser>(manager,null);
        //    ClaimsPrincipal claimsPrincipal=await factory.CreateAsync(this);
        //    return (ClaimsIdentity)claimsPrincipal.Identity;
     
        //}
      
        public virtual IList<PreviousPassword> PreviousUserPasswords { get; set; }

    }

    [PrimaryKey(nameof(UserId), nameof(PasswordHash))]
    public class PreviousPassword
    {
        public PreviousPassword()
        {
            CreateDate = DateTimeOffset.Now;
        }

        [Column(Order = 0)]
        public string UserId { get; set; } = string.Empty;

        [Column(Order = 1)]
        public string PasswordHash { get; set; } = string.Empty;

        public DateTimeOffset CreateDate { get; set; }
 

        public virtual ApplicationUser User { get; set; }

    }


    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        //Todo Harris (Test) Modify Core
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
       : base(options)
        {
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
          
        //    modelBuilder.Entity<PreviousPassword>()
        //   .HasKey(p => new { p.PasswordHash, p.UserId });
        //    modelBuilder.Entity<PreviousPassword>().ToTable("PreviousPassword");

        //}


        //public ApplicationDbContext()
        //    : base("DefaultConnection", throwIfV1Schema: false)
        //{
        //}

        //public static ApplicationDbContext Create()
        //{
        //    //Todo Harris (Test) Modify Core
        //    var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        //        optionsBuilder
        //        .UseSqlServer(connectionString, providerOptions => providerOptions.CommandTimeout(180))
        //        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

        //    return new ApplicationDbContext(optionsBuilder.Options);
        //}
        //
    }

    //Todo Harris (Test) Modify Core
    //public class ApplicationUserManager : UserManager<ApplicationUser>
    //{
    //    public ApplicationUserManager(
    //   DbContextOptions<ApplicationDbContext> options,
    //   IServiceProvider services,
    //   IHttpContextAccessor contextAccessor,
    //   ILogger<UserManager<ApplicationUser>> logger)
    //   : base(
    //         new UserStore<ApplicationUser>(new ApplicationDbContext(contextAccessor)),
    //         new CustomOptions(),
    //         new PasswordHasher<ApplicationUser>(),
    //         new UserValidator<ApplicationUser>[] { new UserValidator<ApplicationUser>() },
    //         new PasswordValidator[] { new PasswordValidator() },
    //         new UpperInvariantLookupNormalizer(),
    //         new IdentityErrorDescriber(),
    //         services,
    //         logger
    //         // , contextAccessor
    //         )
    //    {
    //    }


    //    //public ApplicationUserManager(IUserStore<ApplicationUser> store)
    //    //   : base(store)
    //    //{
    //    //    //PasswordValidator = new MinimumLengthValidator(3);
    //    //}


    //    //Todo Harris (Test) Modify Core
    //    //    public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
    //    //    {
    //    //        var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
    //    //        //var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext);
    //    //        // Configure validation logic for usernames
    //    //        manager.UserValidators = new UserValidator<ApplicationUser>(manager)
    //    //        {
    //    //            AllowOnlyAlphanumericUserNames = false,
    //    //            RequireUniqueEmail = false
    //    //        };


    //    //        // Configure validation logic for passwords
    //    //        manager.PasswordValidator = new PasswordValidator
    //    //        {
    //    //            RequiredLength = 12,
    //    //            RequireNonLetterOrDigit = true,
    //    //            RequireDigit = true,
    //    //            RequireLowercase = true,
    //    //            RequireUppercase = true,          

    //    //        };



    //    //        // Configure user lockout defaults
    //    //        manager.UserLockoutEnabledByDefault = true;
    //    //        manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
    //    //        manager.MaxFailedAccessAttemptsBeforeLockout = 3;

    //    //        // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
    //    //        // You can write your own provider and plug it in here.
    //    //        manager.RegisterTwoFactorProvider(CommonSetting.TwoFactorProvider, new PhoneNumberTokenProvider<ApplicationUser>
    //    //        {
    //    //            MessageFormat = "Your security code is {0}"
    //    //        });
    //    //        manager.RegisterTwoFactorProvider("EmailCode", new EmailTokenProvider<ApplicationUser>
    //    //        {
    //    //            Subject = "Security Code",
    //    //            BodyFormat = "Your security code is {0}"
    //    //        });

    //    //        if (manager.EmailService==null)
    //    //        {
    //    //            manager.EmailService = new EmailService();
    //    //        }//end if

    //    //        manager.SmsService = new SmsService();

    //    //        //var dataProtectionProvider = Startup.DataProtectionProvider;

    //    //        var provider = new DpapiDataProtectionProvider("MT");
    //    //        manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(
    //    //        provider.Create("EmailConfirmation"))
    //    //        {
    //    //            TokenLifespan = TimeSpan.FromDays(1)
    //    //        }
    //    //        ;


    //    //        return manager;
    //    //    }

    //    }

    //Todo Harris (Test) Modify Core
    //public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    //{
    //    public ApplicationSignInManager(ApplicationUserManager userManager, System.Net.AuthenticationManager authenticationManager)
    //        : base(userManager, authenticationManager)
    //    {
    //    }

    //    public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
    //    {
    //        return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
    //    }

    //    public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
    //    {
    //        return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
    //    }
    //}


    public class EmailService : IEmailSender//IIdentityMessageService
    {
        //Todo Harris (Test) Modify Core
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            // Plug in your email service here to send an email.
            await configSMTPasync(email, subject, message);
            //return Task.FromResult(0);
        }


        public void Send(string email, string subject, string message)
        {
            // Plug in your email service here to send an email.
            configSMTP( email,  subject,  message);

        }

        private void  configSMTP(string email, string subject, string message)
        {      
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;//25, 465 & 587
            smtp.EnableSsl = true;
            smtp.ServicePoint.MaxIdleTime = 2;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;         
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            smtp.Credentials = new NetworkCredential("harrisyer@gmail.com", "HarrisYer@1218");
            var Message = new MailMessage("harrisyer@gmail.com", email);
            Message.Subject = subject;
            Message.Body = message;
            Message.IsBodyHtml = true;
            Task.Factory.StartNew(() => smtp.Send(Message));        
        }

        public async Task configSMTPasync(string email, string subject, string message)
        {
            //SmtpClient smtp = new SmtpClient();
            //Todo Harris (Test) Modify Core

            var Message = new MailMessage("harrisyer@gmail.com", email);
            Message.Subject = subject;
            Message.Body = message;
            Message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;//25, 465 & 587
                smtp.EnableSsl = true;
                smtp.ServicePoint.MaxIdleTime = 2;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                smtp.Credentials = new NetworkCredential("harrisyer@gmail.com", "HarrisYer@1218");
               // await smtp.ConnectAsync("smtp.relay.uri", 25, SecureSocketOptions.None).ConfigureAwait(false);
                await smtp.SendMailAsync(Message).ConfigureAwait(false);
                //await smtp.DisconnectAsync(true).ConfigureAwait(false);
            }


            //Task.Factory.StartNew(() => smtp.Send(Message));
        }

        ////public async Task SendAsync(Microsoft.AspNetCore.Identity.UI.Pages.Account.Internal.IdentityMessage message)
        ////{
        ////    // Plug in your email service here to send an email.
        ////    await configSMTPasync(message);


        ////    //return Task.FromResult(0);
        ////}

        ////private async Task configSMTPasync(IdentityMessage message1)
        ////{
        ////    //SmtpClient smtp = new SmtpClient();
        ////    //smtp.Host = "smtp.zoho.com";
        ////    //smtp.Port = 465;//25, 465 & 587
        ////    ////smtp.Host = "DESKTOP-MPHK583";
        ////    // smtp.EnableSsl = true;           
        ////    //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        ////    //smtp.UseDefaultCredentials = false;

        ////    //smtp.Credentials = new NetworkCredential("harris.yer@mifun.my", "UhArVYXHv0k8");
        ////    //var message = new MailMessage("harris.yer@mifun.my", message1.Destination);
        ////    ////using (var message = new MailMessage("harrisyer@gmail.com", message1.Destination))
        ////    ////{
        ////    //message.Subject = message1.Subject;
        ////    //message.Body = message1.Body;
        ////    //message.IsBodyHtml = true;
        ////    //await smtp.SendMailAsync(message);

          

        ////    SmtpClient smtp = new SmtpClient();
        ////    smtp.Host = "smtp.gmail.com";
        ////    smtp.Port = 587;//25, 465 & 587
        ////    smtp.EnableSsl = true;
        ////    smtp.ServicePoint.MaxIdleTime = 2;
        ////    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        ////    smtp.UseDefaultCredentials = false;
        ////    //smtp.Credentials = new NetworkCredential("tanahgao1111@gmail.com", "Password@1218");
        ////    //var message = new MailMessage("tanahgao1111@gmail.com", message1.Destination);
        ////    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        ////    smtp.Credentials = new NetworkCredential("harrisyer@gmail.com", "N0r&$1y#p");
        ////    var message = new MailMessage("harrisyer@gmail.com", message1.Destination);
        ////    message.Subject = message1.Subject;
        ////    message.Body = message1.Body;
        ////    message.IsBodyHtml = true;
        ////    await smtp.SendMailAsync(message);


        ////}
    }

    public class EmailServiceSettlement : IEmailSender
    {
        //Todo Harris (Test) Modify Core
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var Message = new MailMessage("harrisyer@gmail.com", email);
            Message.Subject = subject;
            Message.Body = message;
            Message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;//25, 465 & 587
                smtp.EnableSsl = true;
                smtp.ServicePoint.MaxIdleTime = 2;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                smtp.Credentials = new NetworkCredential("harrisyer@gmail.com", "HarrisYer@1218");
                // await smtp.ConnectAsync("smtp.relay.uri", 25, SecureSocketOptions.None).ConfigureAwait(false);
                await smtp.SendMailAsync(Message).ConfigureAwait(false);
                //await smtp.DisconnectAsync(true).ConfigureAwait(false);
            }
            //return Task.FromResult(0);
        }

        //public async Task SendAsync(IdentityMessage message1)
        //{
        //    SmtpClient smtp = new SmtpClient();
        //    smtp.Host = "smtp.gmail.com";
        //    smtp.Port = 587;
        //    smtp.EnableSsl = true;
        //    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        //    smtp.UseDefaultCredentials = false;
        //    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        //    smtp.Credentials = new NetworkCredential("harrisyer@gmail.com", "HarrisYer@1218");
        //    var message = new MailMessage("harrisyer@gmail.com", message1.Destination);
        //    //smtp.Credentials = new NetworkCredential("tanahgao1111@gmail.com", "Password@1218");
        //    //var message = new MailMessage("tanahgao1111@gmail.com", message1.Destination);
        //    message.Subject = message1.Subject;
        //    message.Body = message1.Body;
        //    message.IsBodyHtml = true;
        //    await smtp.SendMailAsync(message);
        //}

    }

    public class EmailServiceDispute : IEmailSender
    {
        //Todo Harris (Test) Modify Core
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var Message = new MailMessage("harrisyer@gmail.com", email);
            Message.Subject = subject;
            Message.Body = message;
            Message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;//25, 465 & 587
                smtp.EnableSsl = true;
                smtp.ServicePoint.MaxIdleTime = 2;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                smtp.Credentials = new NetworkCredential("harrisyer@gmail.com", "HarrisYer@1218");
                // await smtp.ConnectAsync("smtp.relay.uri", 25, SecureSocketOptions.None).ConfigureAwait(false);
                await smtp.SendMailAsync(Message).ConfigureAwait(false);
                //await smtp.DisconnectAsync(true).ConfigureAwait(false);
            }
            //return Task.FromResult(0);
        }
        //public async Task SendAsync(IdentityMessage message1)
        //{
        //    SmtpClient smtp = new SmtpClient();
        //    smtp.Host = "smtp.gmail.com";
        //    smtp.Port = 587;
        //    smtp.EnableSsl = true;
        //    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        //    smtp.UseDefaultCredentials = false;
        //    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        //    smtp.Credentials = new NetworkCredential("harrisyer@gmail.com", "N0r&$1y#p");
        //    var message = new MailMessage("harrisyer@gmail.com", message1.Destination);
        //    //smtp.Credentials = new NetworkCredential("tanahgao1111@gmail.com", "Password@1218");
        //    //var message = new MailMessage("tanahgao1111@gmail.com", message1.Destination);
        //    message.Subject = message1.Subject;
        //    message.Body = message1.Body;
        //    message.IsBodyHtml = true;
        //    await smtp.SendMailAsync(message);
        //}

    }

    public class EmailServiceRefund : IEmailSender
    {
        //Todo Harris (Test) Modify Core
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var Message = new MailMessage("harrisyer@gmail.com", email);
            Message.Subject = subject;
            Message.Body = message;
            Message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;//25, 465 & 587
                smtp.EnableSsl = true;
                smtp.ServicePoint.MaxIdleTime = 2;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                smtp.Credentials = new NetworkCredential("harrisyer@gmail.com", "HarrisYer@1218");
                // await smtp.ConnectAsync("smtp.relay.uri", 25, SecureSocketOptions.None).ConfigureAwait(false);
                await smtp.SendMailAsync(Message).ConfigureAwait(false);
                //await smtp.DisconnectAsync(true).ConfigureAwait(false);
            }
            //return Task.FromResult(0);
        }

        //public async Task SendAsync(IdentityMessage message1)
        //{
        //    SmtpClient smtp = new SmtpClient();
        //    smtp.Host = "smtp.gmail.com";
        //    smtp.Port = 587;
        //    smtp.EnableSsl = true;
        //    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        //    smtp.UseDefaultCredentials = false;
        //    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        //    smtp.Credentials = new NetworkCredential("harrisyer@gmail.com", "N0r&$1y#p");
        //    var message = new MailMessage("harrisyer@gmail.com", message1.Destination);
        //    //smtp.Credentials = new NetworkCredential("tanahgao1111@gmail.com", "Password@1218");
        //    //var message = new MailMessage("tanahgao1111@gmail.com", message1.Destination);
        //    message.Subject = message1.Subject;
        //    message.Body = message1.Body;
        //    message.IsBodyHtml = true;
        //    await smtp.SendMailAsync(message);
        //}

    }

        //TODO Harris Core-temp-off
        //public class SmsService : ISmsSender
        //{
        //    public Task SendAsync(IdentityMessage message)
        //    {
        //        // Plug in your SMS service here to send a text message.
        //        return Task.FromResult(0);
        //    }
        //}

        //Todo Harris (Test) Modify Core
        //public class ApplicationUserStore : UserStore<ApplicationUser>
        //    {
        //        public ApplicationUserStore(DbContext context)
        //            : base(context)
        //        {
        //        }

        //        public override async Task CreateAsync(ApplicationUser user)
        //        {
        //            await base.CreateAsync(user);

        //            await AddToPreviousPasswordsAsync(user, user.PasswordHash);
        //        }

        //        public Task AddToPreviousPasswordsAsync(ApplicationUser user, string password)
        //        {
        //            user.PreviousUserPasswords.Add(new PreviousPassword() { UserId = user.Id, PasswordHash = password });
        //            return UpdateAsync(user);
        //        }
        //    }

    }