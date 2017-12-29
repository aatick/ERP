//using System.Transactions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Common.Data;
using Common.Service;
using Common.Service.StoredProcedure;
using DotNetOpenAuth.AspNet;
using ERP.Web.Filters;
using ERP.Web.Helpers;
using ERP.Web.Models;
using ERP.Web.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;


namespace ERP.Web.Controllers
{

    [Authorize]

    public class AccountController : Controller
    {
        #region Variables
        private UserManager<ApplicationUser> userManager;
        private readonly IBrokerEmployeeService brokerEmployeeService;
        private readonly ISPService sPService;
        private readonly IAspNetRoleService roleService;
        private readonly IAspNetUserService aspNetUserService;
        private readonly IUserInfoSPService userInfoSPService;
        private readonly ILogger loggger;
        private readonly ISecurityService securityService;
        private readonly IUserLoginService userLoginService;
        private readonly IOrganizationService organizationService;
        private readonly IBrokerBranchService brokerBranchService;
        private readonly IEmployeeProfileService employeeProfileService;
        public AccountController(UserManager<ApplicationUser> userManager, ILogger loggger, IBrokerEmployeeService brokerEmployeeService, IAspNetRoleService roleService, IAspNetUserService aspNetUserService, IUserInfoSPService userInfoSPService, ISecurityService securityService, IUserLoginService userLoginService, ISPService sPService, IOrganizationService organizationService
                , IBrokerBranchService brokerBranchService, IEmployeeProfileService employeeProfileService)
        {
            this.userManager = userManager;
            this.loggger = loggger;
            this.brokerEmployeeService = brokerEmployeeService;
            this.roleService = roleService;
            this.aspNetUserService = aspNetUserService;
            this.userInfoSPService = userInfoSPService;
            this.securityService = securityService;
            this.userLoginService = userLoginService;
            this.sPService = sPService;
            this.organizationService = organizationService;
            this.brokerBranchService = brokerBranchService;
            this.employeeProfileService = employeeProfileService;
        }
        #endregion

        #region Methods
        private void LogRequest()
        {
            try
            {
                var logObject = Logger.GetLogObject();
                loggger.LogRequest(logObject);
            }
            catch (Exception ex)
            {

            }
        }

        public JsonResult GetBrokerEmployeeForRegister()
        {
            try
            {
                List<BrokerEmployeeViewModel> Result = new List<BrokerEmployeeViewModel>();
                {
                    var ResultList = sPService.GetBrokerEmployeeForRegister();

                    Result = ResultList.Tables[0].AsEnumerable()
                    .Select(row => new BrokerEmployeeViewModel
                    {
                        UserId = row.Field<int>("UserId"),
                        EmployeeName = row.Field<string>("EmployeeName"),
                        EmployeeCode = row.Field<string>("EmployeeCode"),
                        Email = row.Field<string>("Email")
                    }).ToList();
                }
                return Json(Result, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        //private void MapDropdownListValues()
        //{
        //    //var personTyp = new List<SelectListItem>();
        //    //personTyp.Add(new SelectListItem() { Text = "Please Select", Value = "" });
        //    //////personTyp.Add(new SelectListItem() { Text = "Teacher", Value = "Teacher" });
        //    //personTyp.Add(new SelectListItem() { Text = "Staff", Value = "Staff" });
        //    //////personTyp.Add(new SelectListItem() { Text = "Student", Value = "Student" });
        //    //ViewBag.PersontTypList = personTyp;




        //    var personList = new List<SelectListItem>();
        //    personList.Add(new SelectListItem() { Text = "", Value = "", Selected = true });
        //    ViewBag.PersonList = personList;
        //}
        public JsonResult LoadRoleList()
        {

            var RoleList = new List<SelectListItem>();

            var role_List = roleService.GetAll();
            var viewrole = role_List.Select(x => x).ToList().Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name.ToString()
            });
            RoleList.Add(new SelectListItem() { Text = "Please Select", Value = "0", Selected = true });
            RoleList.AddRange(viewrole);
            return Json(RoleList, JsonRequestBehavior.AllowGet);



        }


        public string NoDayInitialMenu(string ProjectName)
        {
            var IsDayIni = "0";
            var Menu =
                sPService.GetDataBySqlCommand(
                    "SELECT AspNetRoleModuleId FROM AspNetRoleModule RM INNER JOIN AspNetSecurityModule M ON M.AspNetSecurityModuleId=RM.ModuleId WHERE RM.RoleId =" +
                    SessionHelper.LoggedIn_RoleId + " AND M.SecurityModuleCode ='DayProcess'").Tables[0];

            if (Menu.Rows.Count > 0)
            {
                SessionHelper.IsDayInitiated = false;
                SessionHelper.BusinessDate = sPService.GetBusinessDay().ToString("dd/MM/yyyy");
                var parentModules =
                    securityService.GetAllPrentModule(ProjectName)
                        .Where(
                            x =>
                                x.SecurityModuleCode == "ALLREPORT" ||
                                x.SecurityModuleCode == "EmailSMS" ||
                                x.SecurityModuleCode == "PROCESS" ||
                                x.SecurityModuleCode == "GenFile"
                                )
                        .ToList();
                var parentSecondModules =
                    securityService.GetSecondPrentModule(ProjectName)
                        .Where(
                            x =>
                                parentModules.FirstOrDefault(
                                    z => z.AspNetSecurityModuleId == x.ParentModuleId) != null)
                        .ToList();
                var Project = securityService.GetAllProject(SessionHelper.LoggedIn_RoleId, ProjectName).ToList();
                var userModules = securityService.GeAllRoleModules(SessionHelper.LoggedIn_RoleId, ProjectName).ToList();
                var reportModules = securityService.GetReportModules(SessionHelper.LoggedInUserId, ProjectName).ToList();
                SessionHelper.LogSessionInfo(parentModules, parentSecondModules, userModules, reportModules, Project);
                IsDayIni = "Index"; //RedirectToAction("Index", "DayProcess");
                return IsDayIni;
            }
            else
            {
                SessionHelper.IsDayInitiated = false;
                SessionHelper.BusinessDate = sPService.GetBusinessDay().ToString("dd/MM/yyyy");
                var parentModules =
                    securityService.GetAllPrentModule(ProjectName)
                        .Where(
                            x =>
                                x.SecurityModuleCode == "ALLREPORT" ||
                                x.SecurityModuleCode == "EmailSMS")
                        .ToList();
                var parentSecondModules =
                    securityService.GetSecondPrentModule(ProjectName)
                        .Where(
                            x =>
                                parentModules.FirstOrDefault(
                                    z => z.AspNetSecurityModuleId == x.ParentModuleId) != null)
                        .ToList();
                var Project = securityService.GetAllProject(SessionHelper.LoggedIn_RoleId, ProjectName).ToList();
                var userModules = securityService.GeAllRoleModules(SessionHelper.LoggedIn_RoleId, ProjectName).ToList();
                var reportModules = securityService.GetReportModules(SessionHelper.LoggedInUserId, ProjectName).ToList();
                SessionHelper.LogSessionInfo(parentModules, parentSecondModules, userModules, reportModules, Project);
                IsDayIni = "NotDayInitial";// return RedirectToAction("NotDayInitial", "DayProcess");
                return IsDayIni;
            }
        }

        public void UserMenuInSession(string ProjectName)
        {
            var Project = securityService.GetAllProject(SessionHelper.LoggedIn_RoleId, ProjectName).ToList();
            var parentModules = securityService.GetAllPrentModule(ProjectName).ToList();
            var parentSecondModules = securityService.GetSecondPrentModule(ProjectName).ToList();
            var userModules = securityService.GeAllRoleModules(SessionHelper.LoggedIn_RoleId, ProjectName).ToList();
            var reportModules = securityService.GetReportModules(SessionHelper.LoggedInUserId, ProjectName).ToList();
            SessionHelper.LogSessionInfo(parentModules, parentSecondModules, userModules, reportModules, Project);
        }
        public string ProjectWiseMenu(string ProjectName)
        {
            var ReturnPage = ProjectName;

            var EmpInfo = employeeProfileService.GetByEmail(SessionHelper.UserName);//HR

            if (ProjectName == "UPMS")
            {
                var fileStatus = sPService.GetDataWithParameter(new
                {
                    TRANSACTION_DATE = sPService.GetBusinessDay().ToString("dd MMM yyyy")
                }, "SP_GET_NOT_PROCESSED_FILE_NAME");

                if (fileStatus.Tables.Count > 0)
                {
                    if (fileStatus.Tables[0].Rows.Count > 0)
                    {
                        SessionHelper.NotProcessedFile = fileStatus.Tables[0].Rows[0][0].ToString();
                    }
                }

                var param = new { OfficeId = EmpInfo.emp_branch_id };
                var InitialStatus = sPService.GetDataWithParameter(param, "GetDayInitialDate");
                if (InitialStatus.Tables[0].Rows.Count > 0)
                {
                    SessionHelper.BusinessDate = InitialStatus.Tables[0].Rows[0][2].ToString() == ""
                        ? sPService.GetBusinessDay().ToString("dd/MM/yyyy")
                        : InitialStatus.Tables[0].Rows[0][2].ToString();
                    SessionHelper.IsDayInitiated = Convert.ToBoolean(InitialStatus.Tables[0].Rows[0][1]);
                    SessionHelper.TransactionDate =
                        Convert.ToDateTime(
                            ReportHelper.FormatDateToString(
                                InitialStatus.Tables[0].Rows[0][2].ToString()));

                    UserMenuInSession(ProjectName);


                    return ReturnPage;
                }
                else // No day Initial
                {
                    ReturnPage = NoDayInitialMenu(ProjectName);
                    return ReturnPage;

                }
            }
            else
            {
                UserMenuInSession(ProjectName);
                return ReturnPage;
            }
        }

        public ActionResult Project_X(string ProjectName)
        {

            var ReturnPage = ProjectWiseMenu(ProjectName);

            if (ProjectName == "0") // All Project Permission
            {
                return Json(new { Result = "Success", Message = "Successfull.", Url = "/Home/DashIndex" }, JsonRequestBehavior.AllowGet);
            }
            else if (ReturnPage == "Index") // Day Initial
            {
                return Json(new { Result = "Success", Message = "Successfull.", Url = "/Upms/DayProcess/Index" }, JsonRequestBehavior.AllowGet);
            }
            else if (ReturnPage == "NotDayInitial")// Day Initial
            {
                return Json(new { Result = "Success", Message = "Successfull.", Url = "/Upms/DayProcess/NotDayInitial" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var ProjectHomePage = SessionHelper.ddlProjects.Where(x => x.ProjectShortName == ProjectName).FirstOrDefault().ProjectHomePage;
                return Json(new { Result = "Success", Message = "Successfull.", Url = "/Home/" + ProjectHomePage }, JsonRequestBehavior.AllowGet);
            }
        }

        public void ReportSetting()
        {
            var header = sPService.GetDataWithoutParameter("SP_GET_REPORT_HEADER").Tables[0].AsEnumerable().Select(x => new ReportHeader()
            {
                Id = x.Field<int>(0),
                CompanyNameLeft = x.Field<int>(1),
                CompanyNameHeight = x.Field<int>(2),
                CompanyNameTop = x.Field<int>(3),
                CompanyNameWidth = x.Field<int>(4),
                CompanyNameFontSize = x.Field<int>(5),
                CompanyNameBold = x.Field<bool>(6),
                CompanyNameAlign = x.Field<string>(7),

                CompanyAddressLeft = x.Field<int>(8),
                CompanyAddressHeight = x.Field<int>(9),
                CompanyAddressTop = x.Field<int>(10),
                CompanyAddressWidth = x.Field<int>(11),
                CompanyAddressFontSize = x.Field<int>(12),
                CompanyAddressBold = x.Field<bool>(13),
                CompanyAddressAlign = x.Field<string>(14),

                ReportNameLeft = x.Field<int>(15),
                ReportNameHeight = x.Field<int>(16),
                ReportNameTop = x.Field<int>(17),
                ReportNameWidth = x.Field<int>(18),
                ReportNameFontSize = x.Field<int>(19),
                ReportNameBold = x.Field<bool>(20),
                ReportNameAlign = x.Field<string>(21),

                CompanyLogoLeft = x.Field<int>(22),
                CompanyLogoHeight = x.Field<int>(23),
                CompanyLogoTop = x.Field<int>(24),
                CompanyLogoWidth = x.Field<int>(25),

                FirstLineLeft = x.Field<int>(26),
                FirstLineTop = x.Field<int>(27),
                SecondLineLeft = x.Field<int>(28),
                SecondLineTop = x.Field<int>(29),
                FirstLineSuppress = x.Field<bool>(30),
                SecondLineSuppress = x.Field<bool>(31),

                ReportType = x.Field<string>(32),
                FirstLineWidth = x.Field<int>(33),
                SecondLineWidth = x.Field<int>(34)
            }).ToList();

            SessionHelper.ReportHeader = header;
            SessionHelper.TopShare = sPService.GetDataWithoutParameter("SP_GET_TOP_5_HIGHER_PRICE_SHARE").Tables[0];

        }

        public JsonResult GetRegisterInformation()
        {
            try
            {
                List<BrokerEmployeeViewModel> List_EmployeeInfo = new List<BrokerEmployeeViewModel>();
                var empList = sPService.GetDataWithoutParameter("SP_GetUserList");

                List_EmployeeInfo = empList.Tables[0].AsEnumerable()
                .Select(row => new BrokerEmployeeViewModel
                {
                    RowSl = row.Field<long>("RowSl"),
                    UserId = row.Field<int>("UserId"),
                    Email = row.Field<string>("UserName"),
                    EmployeeCode = row.Field<string>("EmployeeCode"),
                    EmployeeName = row.Field<string>("EmployeeName"),
                    RoleName = row.Field<string>("RoleName"),

                }).ToList();

                return Json(List_EmployeeInfo, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult RegisterInfoDelete(string UserId)
        {
            var entity = aspNetUserService.GetByUserId(Convert.ToInt32(UserId));
            string Result = "OK";
            if (ModelState.IsValid)
            {
                entity.IsRemoved = true;
                aspNetUserService.Update(entity);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public string PopulateBody(string UserName, string newPassword)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/EmailHTMLPage/PasswordChange.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{UserName}", UserName);
            body = body.Replace("{newPassword}", newPassword);
            return body;
        }

        public JsonResult RegisterInfoResetPassword(string UserId)
        {
            var entity = aspNetUserService.GetByUserId(Convert.ToInt32(UserId));
            string EmployeeName = string.Empty;
            EmployeeName = brokerEmployeeService.GetByEmail(entity.UserName).EmployeeName;

            string Result = "";

            Random rnd = new Random();
            int myRandomNo = rnd.Next(10000000, 99999999);
            //var myRandomNo = "12345678";

            userManager.RemovePassword(entity.Id);
            userManager.AddPassword(entity.Id, myRandomNo.ToString());

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(SessionHelper.OrgEmail, "Ucas Portfolio");
            mail.To.Add(entity.UserName);
            mail.Subject = "Password Reset Confirmation";
            mail.Body = PopulateBody(EmployeeName, myRandomNo.ToString());
            //mail.Body = @"Dear " + EmployeeName + ",<br />" + " Your password has been changed. Your new password is " + myRandomNo.ToString();

            mail.IsBodyHtml = true; //*This line is must

            // //send the message  //smtp.gmail.com
            // SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            // smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            //// SmtpClient smtp = new SmtpClient("grameen.grameen.com");
            // //smtp.Credentials = new System.Net.NetworkCredential("nu@grameen.com", "nu2014");
            // smtp.Credentials = new System.Net.NetworkCredential("mominalislam91@gmail.com", "Mins@124");
            // smtp.Port = 587;
            // smtp.EnableSsl = false;
            // smtp.Send(mail);

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com"; // the host name
            smtp.Port = 587; //port number
            smtp.EnableSsl = true; //whether your smtp server requires SSL
            smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            smtp.Credentials = new System.Net.NetworkCredential(SessionHelper.OrgEmail, SessionHelper.OrgEmailPassword);
            smtp.Timeout = 20000;
            smtp.Send(mail);

            Result = "OK";

            sPService.GetDataBySqlCommand("UPDATE AspNetUsers SET Hashing='" + myRandomNo.ToString() + "' WHERE UserId=" + UserId);

            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckCurrentPassword(string CrtPassword)
        {
            var entity = aspNetUserService.GetByUserId(Convert.ToInt32(SessionHelper.LoggedInUserId));
            //ApplicationUser cUser = userManager.FindById(entity.Id);
            string Result = "";
            ////if (ModelState.IsValid)
            ////{
            //var currentPwd = CrtPassword;
            // userManager.RemovePassword(entity.Id);
            //userManager.AddPassword(entity.Id, "12345678");
            var user = new ApplicationUser() { Id = entity.Id, UserName = entity.UserName, PasswordHash = entity.PasswordHash, DateCreated = Convert.ToDateTime(entity.DateCreated), Activated = entity.Activated, UserId = SessionHelper.LoggedInUserId, RoleId = entity.RoleId };
            var chk = userManager.CheckPassword(user, CrtPassword);
            if (chk == true)
            {
                Result = "OK";
            }
            else
            {
                Result = "No";
            }

            //}
            //return Json(result, JsonRequestBehavior.AllowGet);
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        //SessionHelper.LoggedInPersonId = user.PersonId;
        //SessionHelper.LoggedInPersonType = user.PersonType;
        public byte[] GetImageFromDataBase(int UserId)
        {

            var empImg = brokerEmployeeService.GetById(UserId);
            if (empImg != null)
            {
                var img = empImg.Photograph;
                byte[] cover = img;
                return cover;
            }
            else
            {
                byte[] cover = null;
                return cover;
            }



        }
        public ActionResult RetrieveUserImage(int PersonId)
        {
            byte[] cover = GetImageFromDataBase(PersonId);
            if (cover != null)
            {
                return File(cover, "image/*");
            }
            else
            {
                string ImgPathAbsolute = HttpContext.Server.MapPath("~/images/blank-headshot.jpg");
                Image img = Image.FromFile(ImgPathAbsolute);
                byte[] blnk;
                using (MemoryStream ms = new MemoryStream())
                {
                    img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    blnk = ms.ToArray();
                }

                return File(blnk, "image/*"); ;
            }
        }

        //[SessionExpireFilter]
        //[DisableCache]

        #endregion

        #region Events

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            LogRequest();
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model, string returnUrl)
        {
            LogRequest();
            if (ModelState.IsValid)
            {
                if (ReportHelper.CheckSoftwareExpiration())
                {
                    var entity = aspNetUserService.GetByEmail(model.UserName);
                    if (entity != null)
                    {
                        var user = await userManager.FindAsync(model.UserName, model.Password);
                        if (user != null)
                        {
                            await SignInAsync(user, model.RememberMe);
                            // var User_Name = brokerEmployeeService.GetByEmail(model.UserName); //.EmployeeName;
                            var EmpInfo = employeeProfileService.GetByEmail(model.UserName); //HR
                            if (EmpInfo != null)
                            {
                                #region Session Value

                                SessionHelper.LoggedInUserFullName = EmpInfo.emp_name;
                                SessionHelper.LoggedInUserId = Convert.ToInt32(entity.UserId);
                                SessionHelper.UserName = model.UserName;
                                SessionHelper.LoggedInUserId_Hrm = EmpInfo.emp_id;
                                SessionHelper.EmployeeCode = EmpInfo.emp_office_code;
                                SessionHelper.LoggedInOfficeId = EmpInfo.emp_branch_id;
                                SessionHelper.LoggedIn_RoleId = entity.RoleId;
                                SessionHelper.RoleName = roleService.GetByRoleId(entity.RoleId.ToString()).Name;
                                //GetByRoleId(entity.RoleId);
                                var organization = organizationService.GetAll().FirstOrDefault();
                                SessionHelper.OrganizationLogo = organization.OrganizationLogoName;
                                SessionHelper.OrganizationName = organization.OrganizationName;
                                SessionHelper.OrganizationBuySaleOrderLogo = organization.BuySaleOrderLogo;
                                SessionHelper.OrganizationShortName = organization.OrganizationShortName;
                                SessionHelper.OrganizationDealerCode = organization.OrganizationDealerCode;
                                SessionHelper.OrganizationAddress = organization.OrganizationAddress;
                                SessionHelper.OrgEmail = organization.Email;
                                SessionHelper.OrgEmailPassword = organization.Password;
                                SessionHelper.SMSPassword = organization.SMSPassword;
                                SessionHelper.SMSMobileNo = organization.SMSMobileNo;
                                SessionHelper.SMSUserName = organization.SMSUserName;
                                SessionHelper.OrganizationLogoPath =
                                    organization.SoftwareLogo;
                                SessionHelper.IsDSEPrimary = organization.IsDSEPrimary;
                                SessionHelper.LoggedInOfficeName =
                                    brokerBranchService.GetById(EmpInfo.emp_branch_id).BrokerBranchName;
                                SessionHelper.IsDayInitiated = false;
                                SessionHelper.NotProcessedFile = "";
                                SessionHelper.BusinessDate = "";
                                SessionHelper.BusinessDate = sPService.GetBusinessDay().ToString("dd/MM/yyyy");
                                SessionHelper.TransactionDate =
                                    Convert.ToDateTime(
                                        ReportHelper.FormatDateToString(
                                            sPService.GetBusinessDay().ToString("dd/MM/yyyy")));



                                #endregion

                                ReportSetting();

                                var Project = securityService.GetAllProject(SessionHelper.LoggedIn_RoleId, "0").ToList();
                                SessionHelper.UserprojectPermission(Project);

                                if (Project.Count == 1) //he has one project permission
                                {
                                    var ProjectName = Project.FirstOrDefault().ProjectShortName;


                                    var ReturnPage = ProjectWiseMenu(ProjectName);

                                    if (ReturnPage == "Index") // Day Initial
                                    {
                                        return RedirectToAction("Index", "DayProcess", new { Area = "Upms" });
                                    }
                                    else if (ReturnPage == "NotDayInitial") // Day Initial
                                    {
                                        return RedirectToAction("NotDayInitial", "DayProcess", new { Area = "Upms" });
                                    }
                                    else
                                    {
                                        return RedirectToAction("" + Project.FirstOrDefault().ProjectHomePage + "",
                                            "Home");
                                    }
                                }
                                else
                                {
                                    return RedirectToAction("Projects", "Home");
                                }


                                //var ProjectName = "";
                                //Project_X(ProjectName);

                            }
                            else
                            {
                                return RedirectToAction("UnauthorizedAccess", "Home");
                                // ModelState.AddModelError("", "Information not found.");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "Invalid username or password.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid username or password.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Software has been expired due to payment issue. Please contact to your vendor.");
                }
            }
            else
            {
                // If we got this far, something failed, redisplay form
                ModelState.AddModelError("", "The user name or password provided is incorrect.");
            }
            return View(model);
        }
        private IAuthenticationManager _authnManager;

        // Modified this from private to public and add the setter
        public IAuthenticationManager AuthenticationManager
        {
            get
            {
                if (_authnManager == null)
                    _authnManager = HttpContext.GetOwinContext().Authentication;
                return _authnManager;
            }
            set { _authnManager = value; }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            LogRequest();
            AuthenticationManager.SignOut();
            Session.Clear();
            Session.Abandon();
            //gHRMSessionFacade.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult LogOff(bool logOff)
        {
            LogRequest();
            AuthenticationManager.SignOut();
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult RegisterIndex()
        {
            return View();
        }

        //[AllowAnonymous]
        //[SessionExpireFilter]
        //[DisableCache]

        public ActionResult ChangePassword()
        {
            var chngModel = new ChangePasswordViewModel();
            chngModel.UserId = SessionHelper.LoggedInUserId;

            var User = aspNetUserService.GetByUserId(SessionHelper.LoggedInUserId);
            chngModel.PersonName = User.FirstName;

            chngModel.UserName = User.UserName;

            return View(chngModel);
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {

            try
            {
                var Result = "0";
                var curPwd = aspNetUserService.GetByUserId(SessionHelper.LoggedInUserId).PasswordHash;
                if (model.NewPassword == model.ConfirmPassword)
                {
                    var entity = aspNetUserService.GetByUserId(SessionHelper.LoggedInUserId);
                    userManager.RemovePassword(entity.Id);
                    userManager.AddPassword(entity.Id, model.NewPassword);
                    sPService.GetDataBySqlCommand("UPDATE AspNetUsers SET Hashing='" + model.NewPassword + "' WHERE UserId=" + SessionHelper.LoggedInUserId);
                    Result = "1";
                    return Json(Result, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json(Result, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                throw;
            }
        }

        public ActionResult Register()
        {
            IEnumerable<SelectListItem> items = new SelectList(" ");
            ViewData["RoleId"] = items;
            // MapDropdownListValues();
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        // [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //[SessionExpireFilter]
        //[DisableCache]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            //if (ModelState.IsValid)
            //{
            // var person = new UserLoginViewModel();

            //  var emp = brokerEmployeeService.GetAll().Where(s=>s.Id == model.UserId).FirstOrDefault();
            //person.UserLoginId = emp.Id;
            //person.UserId = emp.Id;
            // person.PersonType = "User";
            // person.PersonEmail = emp.Email;
            // model.RoleId = model.RoleId;



            //   if (emp != null)
            //{
            try
            {
                var user = new ApplicationUser() { UserId = model.UserId, UserName = model.UserName, FirstName = model.PersonName, RoleId = model.RoleId };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    sPService.GetDataBySqlCommand("UPDATE AspNetUsers SET Hashing='" + model.Password + "' WHERE UserId=" + model.UserId);
                    return Json(new { Result = "OK", Message = "Login Created successfully." }, JsonRequestBehavior.AllowGet);
                    //await SignInAsync(user, isPersistent: false);
                    //return RedirectToAction("Index", "Home");
                }
                else
                {
                    var msg = "";
                    foreach (var r in result.Errors)
                    {
                        msg = string.Format("{0}<br/>{1}", msg, r);
                    }
                    return Json(new { Result = "ERROR", Message = msg }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            //}
            //else
            //    return Json(new { Result = "ERROR", Message = "Invalid Email." }, JsonRequestBehavior.AllowGet);

            // }

            // return Json(new { Result = "ERROR", Message = "Please correct required fields." }, JsonRequestBehavior.AllowGet);
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
        public ActionResult RegisterEdit(int UserId)
        {
            var per = aspNetUserService.GetByUserId(UserId);
            // MapDropdownListValues();
            IEnumerable<SelectListItem> items = new SelectList(" ");
            ViewData["RoleId"] = items;
            var regModel = new RegisterModel();
            regModel.UserId = Convert.ToInt32(per.UserId);
            regModel.PersonName = per.FirstName;
            regModel.UserName = per.UserName;
            regModel.RoleId = per.RoleId;
            // regModel.Password = per.PasswordHash;
            return View(regModel);

        }

        [HttpPost]
        public ActionResult RegisterEdit(RegisterModel model)
        {
            try
            {
                var personList = aspNetUserService.GetByUserId(model.UserId); //aspNetUserService.GetByPerson(model.PersonId, model.PersonType);
                personList.RoleId = model.RoleId;
                personList.UserName = model.UserName;
                aspNetUserService.Update(personList);
                return Json(new { Result = "OK", Message = "Login update successfully." }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Disassociate(string provider, string providerUserId)
        {
            string ownerAccount = OAuthWebSecurity.GetUserName(provider, providerUserId);
            ManageMessageId? message = null;

            // Only disassociate the account if the currently logged in user is the owner
            if (ownerAccount == User.Identity.Name)
            {
                // Use a transaction to prevent the user from deleting their last login credential
                using (var scope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, new System.Transactions.TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.Serializable }))
                {
                    bool hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
                    if (hasLocalAccount || OAuthWebSecurity.GetAccountsFromUserName(User.Identity.Name).Count > 1)
                    {
                        OAuthWebSecurity.DeleteAccount(provider, providerUserId);
                        scope.Complete();
                        message = ManageMessageId.RemoveLoginSuccess;
                    }
                }
            }

            return RedirectToAction("Manage", new { Message = message });
        }


        //
        // GET: /Account/Manage
        [SessionExpireFilter]
        [DisableCache]
        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : "";
            // ViewBag.HasLocalPassword = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }


        //
        // POST: /Account/Manage

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionExpireFilter]
        [DisableCache]
        public ActionResult Manage(LocalPasswordModel model)
        {
            bool hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.HasLocalPassword = hasLocalAccount;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasLocalAccount)
            {
                if (ModelState.IsValid)
                {
                    // ChangePassword will throw an exception rather than return false in certain failure scenarios.
                    bool changePasswordSucceeded;
                    try
                    {
                        changePasswordSucceeded = WebSecurity.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword);
                    }
                    catch (Exception)
                    {
                        changePasswordSucceeded = false;
                    }

                    if (changePasswordSucceeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                    }
                }
            }
            else
            {
                // User does not have a local password so remove any validation errors caused by a missing
                // OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        WebSecurity.CreateAccount(User.Identity.Name, model.NewPassword);
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError("", String.Format("Unable to create local account. An account with the name \"{0}\" may already exist.", User.Identity.Name));
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionExpireFilter]
        [DisableCache]
        public ActionResult ChangePassword_1(LocalPasswordModel model)
        {

            if (ModelState.IsValid)
            {
                // ChangePassword will throw an exception rather than return false in certain failure scenarios.
                // bool changePasswordSucceeded;
                try
                {
                    if (model.OldPassword == model.NewPassword)
                        return Json(new { Result = "ERROR", Message = "Old password and new password cannot be same" }, JsonRequestBehavior.AllowGet);
                    var userId = User.Identity.GetUserId();
                    var result = userManager.ChangePassword(userId, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                        //changePasswordSucceeded = WebSecurity.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword);
                        //if (changePasswordSucceeded)
                        return Json(new { Result = "OK", Message = "Password changed successfully." }, JsonRequestBehavior.AllowGet);
                    else
                        return Json(new { Result = "ERROR", Message = "Failed. " + string.Join(",", result.Errors) }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }

            else
                return Json(new { Result = "ERROR", Message = "Please correct form date." }, JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /Account/ExternalLogin

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            return new ExternalLoginResult(provider, Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/ExternalLoginCallback

        [AllowAnonymous]
        public ActionResult ExternalLoginCallback(string returnUrl)
        {
            AuthenticationResult result = OAuthWebSecurity.VerifyAuthentication(Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
            if (!result.IsSuccessful)
            {
                return RedirectToAction("ExternalLoginFailure");
            }

            if (OAuthWebSecurity.Login(result.Provider, result.ProviderUserId, createPersistentCookie: false))
            {
                return RedirectToLocal(returnUrl);
            }

            if (User.Identity.IsAuthenticated)
            {
                // If the current user is logged in add the new account
                OAuthWebSecurity.CreateOrUpdateAccount(result.Provider, result.ProviderUserId, User.Identity.Name);
                return RedirectToLocal(returnUrl);
            }
            else
            {
                // User is new, ask for their desired membership name
                string loginData = OAuthWebSecurity.SerializeProviderUserId(result.Provider, result.ProviderUserId);
                ViewBag.ProviderDisplayName = OAuthWebSecurity.GetOAuthClientData(result.Provider).DisplayName;
                ViewBag.ReturnUrl = returnUrl;
                return View("ExternalLoginConfirmation", new RegisterExternalLoginModel { UserName = result.UserName, ExternalLoginData = loginData });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLoginConfirmation(RegisterExternalLoginModel model, string returnUrl)
        {
            string provider = null;
            string providerUserId = null;

            if (User.Identity.IsAuthenticated || !OAuthWebSecurity.TryDeserializeProviderUserId(model.ExternalLoginData, out provider, out providerUserId))
            {
                return RedirectToAction("Manage");
            }

            if (ModelState.IsValid)
            {
                // Insert a new user into the database
                using (UsersContext db = new UsersContext())
                {
                    UserProfile user = db.UserProfiles.FirstOrDefault(u => u.UserName.ToLower() == model.UserName.ToLower());
                    // Check if user already exists
                    if (user == null)
                    {
                        // Insert name into the profile table
                        db.UserProfiles.Add(new UserProfile { UserName = model.UserName });
                        db.SaveChanges();

                        OAuthWebSecurity.CreateOrUpdateAccount(provider, providerUserId, model.UserName);
                        OAuthWebSecurity.Login(provider, providerUserId, createPersistentCookie: false);

                        return RedirectToLocal(returnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("UserName", "User name already exists. Please enter a different user name.");
                    }
                }
            }

            ViewBag.ProviderDisplayName = OAuthWebSecurity.GetOAuthClientData(provider).DisplayName;
            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // GET: /Account/ExternalLoginFailure

        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [AllowAnonymous]
        [ChildActionOnly]
        public ActionResult ExternalLoginsList(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return PartialView("_ExternalLoginsListPartial", OAuthWebSecurity.RegisteredClientData);
        }

        [ChildActionOnly]
        public ActionResult RemoveExternalLogins()
        {
            ICollection<OAuthAccount> accounts = OAuthWebSecurity.GetAccountsFromUserName(User.Identity.Name);
            List<ExternalLogin> externalLogins = new List<ExternalLogin>();
            foreach (OAuthAccount account in accounts)
            {
                AuthenticationClientData clientData = OAuthWebSecurity.GetOAuthClientData(account.Provider);

                externalLogins.Add(new ExternalLogin
                {
                    Provider = account.Provider,
                    ProviderDisplayName = clientData.DisplayName,
                    ProviderUserId = account.ProviderUserId,
                });
            }

            ViewBag.ShowRemoveButton = externalLogins.Count > 1 || OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            return PartialView("_RemoveExternalLoginsPartial", externalLogins);
        }


        #endregion

        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }

        internal class ExternalLoginResult : ActionResult
        {
            public ExternalLoginResult(string provider, string returnUrl)
            {
                Provider = provider;
                ReturnUrl = returnUrl;
            }

            public string Provider { get; private set; }
            public string ReturnUrl { get; private set; }

            public override void ExecuteResult(ControllerContext context)
            {
                OAuthWebSecurity.RequestAuthentication(Provider, ReturnUrl);
            }
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
