# Web Forum

A simple web forum made with asp net mvc 5
## Quick Demos

![cHXliZsmET](https://user-images.githubusercontent.com/55974615/81474555-f83a7700-920e-11ea-8e4b-c9d09c83ce50.gif)

![JYlCk1QK5J](https://user-images.githubusercontent.com/55974615/81474646-93cbe780-920f-11ea-8f53-103623d8eb7b.gif)

![nw3F3Yti89](https://user-images.githubusercontent.com/55974615/81474718-ec9b8000-920f-11ea-96f2-de1c5841f918.gif)

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.
### Prerequisites

To open/run/edit this project you will need [Visual Studio](https://visualstudio.microsoft.com/ru/downloads/) or [Rider](https://www.jetbrains.com/ru-ru/rider/) as well as MSSQLLocalDB which can be installed via visual studio installer or downloaded [manually](https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb?view=sql-server-ver15)

### Setting Up

To install and run this project to your local machine you need to follow next steps:

Clone this repo via

```
git clone https://github.com/neistow/Forum.git
```

Open project in any IDE, then head to *Web.config* and configure connection string

```
    <connectionStrings>
        <add name="[ConnectioName]"
             connectionString="Data Source=(LocalDb)\[LocalDBName];Initial Catalog=[CatalogName];Integrated Security=True"
             providerName="System.Data.SqlClient" />
    </connectionStrings>
```
By default connection name is "DefaultConnection", DataSource is set to MSSQLLocalDb and Catalog name is the same as connection name.

If you want to change connection name you should also change connection name in *ApplicationDbContext.cs* which can be found in Persistence Folder
```
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false) <-- Connection Name here
        {
            Configuration.LazyLoadingEnabled = false;
        }
```
Please notice that sometimes due to unknown reasons two instances of DB can be created: One with ConntectionName and other with CatalogName, to avoid this set Connection Name to the same name as Catalog name or vice versa

After you completed configuration of connection strings it's time to run the migrations

If you are using VS type next in NuGet Package Manager Console

```
update-database
```

If you are using Rider there is no need to run the migrations. It will be automatically done during first launch.

![dbo](https://user-images.githubusercontent.com/55974615/81477958-1e6a1200-9223-11ea-907a-6778069ef3aa.png)

Database Scheme should look like this.

At this point application is ready to use, if it's nessecary you can also configure email vefirication.

### Configuring Email Verification
To send email confirmations you'll need to configure SMTP to do this go to *Controllers > AccountController* and uncomment the following lines:

```
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // UNCOMMENT THIS
            // var user = await UserManager.FindByNameAsync(model.Email);
            // if (user != null)
            // {
            //     if (!await UserManager.IsEmailConfirmedAsync(user.Id))
            //     {
            //         ViewBag.errorMessage = "You must have a confirmed email to log on.";
            //         return View("Error");
            //     }
            // }
            ....
```
```
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser {UserName = model.NickName, Email = model.Email};
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // UNCOMMENT THIS
                    // var code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new {userId = user.Id, code = code},
                    //     protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account",
                    //     $"Please confirm your account by clicking <a href=\"{callbackUrl}\">here</a>");
                 ...
```

After that head to *App_Start > IdentityConfig.cs* to configure smtp itself
```
        public override async Task SendEmailAsync(string userId, string subject, string body)
        {
            var from = new MailAddress("youremail@mail.com", "ForumAdmin");
            var email = await GetEmailAsync(userId);
            var to = new MailAddress(email);
            var message = new MailMessage(from, to)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
            
            using (var smtp = new SmtpClient("smtp.mailhost.com", 587))
            {
                smtp.Credentials =
                    new NetworkCredential("youremail@mail.com",
                        "yourpassword");
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(message);
            }
        }
```
To deliver messages in this example I'll be using [MailGun](https://www.mailgun.com/)

After Registration head to Sending > Domains

![image](https://user-images.githubusercontent.com/55974615/81473904-9710a480-920a-11ea-892c-cdbf28182470.png)

Click on your free sandbox mailbox and for simplicity we will use SMTP. After you click it you will see Host,Port,UserName and password. Use those values to configure SMTP.
![image](https://user-images.githubusercontent.com/55974615/81473999-5a917880-920b-11ea-93f0-b41bb5833246.png)

Because we are using provided domain for testing purposes, we should add email which we will use for registration to *Authorized Recipients*.
![image](https://user-images.githubusercontent.com/55974615/81474174-47cb7380-920c-11ea-9e01-5633d85fd441.png)

After a few minutes, we can use email we verified for registration. Now we will receive email after registaion
![image](https://user-images.githubusercontent.com/55974615/81474281-f5d71d80-920c-11ea-985b-b272f082bc4c.png)

## Built With

* [ASP.NET](https://dotnet.microsoft.com/apps/aspnet) - The web framework used
* [Bootstrap](https://getbootstrap.com/) - Open source toolkit for developing with HTML, CSS, and JS
* [EntityFramework](https://docs.microsoft.com/en-us/ef/) - Modern object-database mapper for .NET
* [Ninject](http://www.ninject.org/) - Dependency Injector
* And others

## Authors

* **Gleb Chernigin** - [Neistow](https://github.com/Neistow)
