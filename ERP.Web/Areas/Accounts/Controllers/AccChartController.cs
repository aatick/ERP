using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Accounts.Data.AccountsDataModel;
using Accounts.Service;
using AutoMapper;
using Common.Data.CommonDataModel;
using Common.Service;
using Common.Service.StoredProcedure;
using ERP.Web.Controllers;
using ERP.Web.Helpers;
using ERP.Web.ViewModels;

namespace Accounts.Controllers
{
    public class AccChartController : BaseController
    {
        #region Variables
        private readonly IAccCategoryService accCategoryService;
        private readonly IAccChartService accChartService;
        private readonly IAccNoteService accNoteService;
        private readonly ISPService spService;
        //private readonly IProductService productService;
        //private readonly ILoanSummaryService loanSummaryService;
        //private readonly IPurposeService purposeService;

        public AccChartController(IAccCategoryService accCategoryService, IAccChartService accChartService, IAccNoteService accNoteService, ISPService spService)
        {
            this.accCategoryService = accCategoryService;
            this.accChartService = accChartService;
            this.accNoteService = accNoteService;
            this.spService = spService;
            //this.productService = productService;
            //this.loanSummaryService = loanSummaryService;
            //this.purposeService = purposeService;
        }
        #endregion

        #region Methods


        public JsonResult GetAccChartListForTreeView()
        {
            var AccList = spService.GetDataBySqlCommand("SELECT C.AccID,C.AccCode,C.AccName,C.ParentAccCode,(SELECT AccID  FROM AccChart WHERE AccCode = C.ParentAccCode) AS ParentId FROM AccChart AS C WHERE C.IsActive = 1").Tables[0];

            var AccChart = AccList.AsEnumerable().Select(row => new {

                AccID = row.Field<int>("AccID"),
                AccCode = row.Field<string>("AccCode"),
                AccName = row.Field<string>("AccName"),
                ParentId = row.Field<int?>("ParentId"),
                ParentAccCode = row.Field<string>("ParentAccCode"),
            
            }).ToList();
            return Json(AccChart,JsonRequestBehavior.AllowGet);
        }

        public JsonResult AccChartEdit(string AccID,string IsTransaction, string AccName,string ModuleID)
        {
            var result = string.Empty;
            try
            {
                var acc = accChartService.GetById(Convert.ToInt32(AccID));
                acc.AccName = AccName;
                acc.ModuleID = Convert.ToInt32(ModuleID);
                acc.BrokerBranchId = SessionHelper.LoggedInOfficeId;
                acc.IsTransaction = Convert.ToBoolean(IsTransaction);

                accChartService.Update(acc);
                return Json( result = "Save successfull",JsonRequestBehavior.AllowGet);

            }
            catch(Exception ex)
            {
                return Json(result = ex.Message, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult GetAccCodeList()
        {
            try
            {

                var Broker = accChartService.GetAll();
                var BrokerList = Broker.OrderBy(o => o.AccCode).Select(s => new AccChartViewModel()
                {
                    
                    AccID = s.AccID,
                    AccCode = s.AccCode,
                    AccName = s.AccName,
                    AccLevel = s.AccLevel,//AccLevel  FirstLevel SecondLevel ThirdLevel  FourthLevel CategoryName IsTransaction  Nature ModuleID
                    FirstLevel = s.FirstLevel,
                    SecondLevel = s.SecondLevel,
                    ThirdLevel = s.ThirdLevel,
                    FourthLevel = s.FourthLevel,
                    FifthLevel = s.FifthLevel,
                    CategoryID = s.CategoryID,
                    CategoryName = s.AccCategory.CategoryName,
                    OfficeLevel = s.OfficeLevel,
                    IsTransaction = s.IsTransaction,
                    Nature = s.Nature,
                    ModuleID = s.ModuleID,

                });

                return Json(BrokerList, JsonRequestBehavior.AllowGet);


                //long TotCount;
                //var codeDetail = accChartService.GetAccChartDetail();//LoggedInOrganizationID
                //var detail = codeDetail.ToList();
                //var viewDetail = Mapper.Map<IEnumerable<AccChart>, IEnumerable<AccChartViewModel>>(codeDetail);
                //List<AccChartViewModel> detail = new List<AccChartViewModel>();
                //int row_indx = 1;
                //foreach (var vd in viewDetail)
                //{
                //    var note_details = "";
                //    int note_id = 0;
                //    int.TryParse(vd.NoteID.ToString(), out note_id);
                //    if (note_id > 0)
                //        note_details = accNoteService.GetNoteDetail(vd.NoteID);
                //    var details = new AccChartViewModel() { SlNo = row_indx, AccID = vd.AccID, AccCode = vd.AccCode, AccName = vd.AccName, AccLevel = vd.AccLevel, FirstLevel = vd.FirstLevel, SecondLevel = vd.SecondLevel, ThirdLevel = vd.ThirdLevel, FourthLevel = vd.FourthLevel, FifthLevel = vd.FifthLevel, IsTransaction = vd.IsTransaction, OfficeLevel = vd.OfficeLevel, ModuleID = vd.ModuleID, NoteID = vd.NoteID, NoteName = note_details };
                //    detail.Add(details);
                //    row_indx++;
                //}
               // var currentPageCodes = detail.ToList();

               // return Json(new { Result = "OK", Records = currentPageCodes });

                //var codeDetail = accChartService.GetChartDetail(LoggedInOrganizationID, filterColumn, filterValue);
                ////var detail = codeDetail.ToList();
                //var viewDetail = Mapper.Map<IEnumerable<AccChart>, IEnumerable<AccChartViewModel>>(codeDetail);
                //List<AccChartViewModel> detail = new List<AccChartViewModel>();
                //int row_indx = 1;
                //foreach (var vd in viewDetail)
                //{
                //    var note_details = "";
                //    int note_id = 0;
                //    int.TryParse(vd.NoteID.ToString(), out note_id);
                //    if (note_id > 0)
                //        note_details = accNoteService.GetNoteDetail(vd.NoteID);
                //    var details = new AccChartViewModel() { SlNo = row_indx, AccID = vd.AccID, AccCode = vd.AccCode, AccName = vd.AccName, AccLevel = vd.AccLevel, FirstLevel = vd.FirstLevel, SecondLevel = vd.SecondLevel, ThirdLevel = vd.ThirdLevel, FourthLevel = vd.FourthLevel, FifthLevel = vd.FifthLevel, IsTransaction = vd.IsTransaction, OfficeLevel = vd.OfficeLevel, ModuleID = vd.ModuleID, NoteID = vd.NoteID, NoteName = note_details };
                //    detail.Add(details);
                //    row_indx++;
                //}
                //var currentPageCodes = detail.ToList().Skip(jtStartIndex).Take(jtPageSize);

                //return Json(new { Result = "OK", Records = currentPageCodes, TotalRecordCount = row_indx - 1 });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        public JsonResult GetAccNote()
        {

            try
            {
                var allNotes = accNoteService.GetAll().Where(m => m.IsActive == true && m.OrgID == 1); //LoggedInOrganizationID
                var viewNote = allNotes.Select(x => x).ToList().Select(x => new ListItem
                {
                    Value = x.NoteID.ToString(),
                    Text = x.NoteNo.ToString() + " - " + x.NoteName.ToString()
                });
                var note_items = new List<ListItem>();
                note_items.Add(new ListItem() { Text = "Select None", Value = "0", Selected = true });
                note_items.AddRange(viewNote);              
                
                var noteList = note_items.ToList().Select(c => new { DisplayText = c.Text.ToString(), Value = c.Value }).OrderBy(s => s.Value);                

                return Json(new { Result = "OK", Options = noteList });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteAccCode(int AccID)
        {
            try
            {
                //accChartService.Delete(AccID);
                var acc_Chart = accChartService.GetById(AccID);
                acc_Chart.IsActive = false;
                acc_Chart.InActiveDate = DateTime.Now;
                accChartService.Update(acc_Chart);
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
        private void MapDropDownList(AccChartViewModel model)
        {
            var allCategory = accCategoryService.GetAll();
            var viewCat = allCategory.Select(x => x).ToList().Select(x => new SelectListItem
            {
                Value = x.CategoryID.ToString(),
                Text = x.CategoryName.ToString()
            });
            var cat_items = new List<SelectListItem>();
            cat_items.Add(new SelectListItem() { Text = "Please Select", Value = "0", Selected = true });
            cat_items.AddRange(viewCat);
            model.CategoryList = cat_items;

            var offc_item = new List<SelectListItem>();
           // offc_item.Add(new SelectListItem() { Text = "Branch Office", Value = "4" });
            //offc_item.Add(new SelectListItem() { Text = "Area Office", Value = "3" });
            //offc_item.Add(new SelectListItem() { Text = "Zone Office", Value = "2" });
            offc_item.Add(new SelectListItem() { Text = "Head Office", Value = "1" });
            model.OfficeList = offc_item;

            var module_item = new List<SelectListItem>();
            module_item.Add(new SelectListItem() { Text = "Accounting", Value = "1" });
            module_item.Add(new SelectListItem() { Text = "Portfolio", Value = "2" });
            model.ModuleList = module_item;

            //var allNotes = accNoteService.GetAll().Where(m => m.IsActive == true && m.OrgID == 1 );//LoggedInOrganizationID
            //var viewNote = allNotes.Select(x => x).ToList().Select(x => new SelectListItem
            //{
            //    Value = x.NoteID.ToString(),
            //    Text = x.NoteNo.ToString() + " - " + x.NoteName.ToString()
            //});
            //var note_items = new List<SelectListItem>();
            //note_items.Add(new SelectListItem() { Text = "Please Select", Value = "0", Selected = true });
            //note_items.AddRange(viewNote);
            //model.AccNoteList = note_items;


            //switch (s.InvestorID) { case 11: { return true; } default: return false; }
        }
        public JsonResult GetParentList(string acc_code)
        {
            var acc = accChartService.GetAll().Where(m => m.AccLevel != 5).ToList();
            var accList = new List<AccChart>();
            accList = acc;
            var accChart = accList.Where(m => string.Format("{0} - {1}", m.AccCode, m.AccName).ToLower().Contains(acc_code.ToLower())).Select(m1 => new { m1.AccID, AccFullName = string.Format("{0} - {1}", m1.AccCode, m1.AccName), m1.FirstLevel, m1.SecondLevel, m1.ThirdLevel, m1.FourthLevel, m1.FifthLevel }).ToList();
            return Json(accChart, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetParentCodeDetail(string ParentCode)
        {
            var acc = accChartService.GetByAccCode(ParentCode);
            if (acc != null)
            {
                var result = new { FirstLevel = acc.FirstLevel, AccName = acc.AccName, SecondLevel = acc.SecondLevel, ThirdLevel = acc.ThirdLevel, FourthLevel = acc.FourthLevel, FifthLevel = acc.FifthLevel, AccLevel = acc.AccLevel, CategoryID = acc.CategoryID };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var result = new { FirstLevel = "", AccName = "", SecondLevel = "", ThirdLevel = "", FourthLevel = "", FifthLevel = "", AccLevel = 0 };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetCodeDetail(string AccCode)
        {
            var acc = accChartService.GetByAccCode(AccCode);
            if (acc != null)
            {
                var result = new { AccCode  = acc.AccCode};
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var result = new { AccCode = "" };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SaveAccCode(int AccID,string ParentCode, string FirstLevel, string SecondLevel, string ThirdLevel, string FourthLevel, string AccCode, string AccName, string AccLevel, string Nature, bool IsTransaction, string CategoryID, string OfficeLevel, string NoteID)//string ModuleID, 
        {
            var entity = new AccChart();
            string result = "";
            string msg = "";
            var Acc_Id = 0;

                if (AccID == 0) //Save
                {
                    if (accChartService.IsValidAccCode(AccCode))
                    {
                        if (FirstLevel != "")
                        {
                            entity.FirstLevel = FirstLevel;
                            if (SecondLevel != "")
                            {
                                entity.SecondLevel = SecondLevel;
                                if (ThirdLevel != "")
                                {
                                    entity.ThirdLevel = ThirdLevel;
                                    if (FourthLevel != "")
                                    {
                                        entity.AccCode = AccCode;
                                        entity.AccName = AccName;
                                        entity.FourthLevel = FourthLevel;
                                        entity.FifthLevel = AccCode;
                                        entity.AccLevel = 5;
                                    }
                                    else
                                    {
                                        entity.AccCode = AccCode;
                                        entity.AccName = AccName;
                                        entity.FourthLevel = AccCode;
                                        entity.AccLevel = 4;
                                    }
                                }
                                else
                                {
                                    entity.AccCode = AccCode;
                                    entity.AccName = AccName;
                                    entity.ThirdLevel = AccCode;
                                    entity.AccLevel = 3;
                                }
                            }
                            else
                            {
                                entity.AccCode = AccCode;
                                entity.AccName = AccName;
                                entity.SecondLevel = AccCode;
                                entity.AccLevel = 2;
                            }
                        }
                        else
                        {
                            entity.AccCode = AccCode;
                            entity.AccName = AccName;
                            entity.FirstLevel = AccCode;
                            entity.AccLevel = 1;
                        }
                        //entity.IsTransaction = Convert.ToBoolean(IsTransaction);
                        entity.ParentAccCode = ParentCode;
                        entity.IsTransaction = IsTransaction;
                        entity.CategoryID = Convert.ToInt32(CategoryID);
                        entity.Nature = Nature;
                        entity.OfficeLevel = Convert.ToInt32(OfficeLevel);
                        //entity.ModuleID = Convert.ToInt32(ModuleID);
                        if (NoteID != "" || Convert.ToInt32(NoteID) > 0)
                            entity.NoteID = Convert.ToInt32(NoteID);
                        AccChartViewModel viewModel = new AccChartViewModel();
                        entity.CreateUser = SessionHelper.LoggedInUserId.ToString();// viewModel.CreateUserId;
                        entity.CreateDate = viewModel.CreateDate;
                        entity.IsActive = true;
                        entity.InActiveDate = viewModel.CreateDate;
                        entity.BrokerBranchId = SessionHelper.LoggedInOfficeId;
                        entity.OrgID = 1;// Convert.ToInt32(LoggedInOrganizationID);    
                        var chk = new AccChart();
                        chk = accChartService.Create(entity);
                        Acc_Id = chk.AccID;
                    }
                    else
                    {
                        result = "A";
                    }
                   
                }
                else  //Edit
                {
                    var entity2 = accChartService.GetById(AccID);
                    if (FirstLevel != "")
                    {
                        entity2.FirstLevel = FirstLevel;
                        if (SecondLevel != "")
                        {
                            entity2.SecondLevel = SecondLevel;
                            if (ThirdLevel != "")
                            {
                                entity2.ThirdLevel = ThirdLevel;
                                if (FourthLevel != "")
                                {
                                    entity2.AccCode = AccCode;
                                    entity2.AccName = AccName;
                                    entity2.FourthLevel = FourthLevel;
                                    entity2.FifthLevel = AccCode;
                                    entity2.AccLevel = 5;
                                }
                                else
                                {
                                    entity2.AccCode = AccCode;
                                    entity2.AccName = AccName;
                                    entity2.FourthLevel = AccCode;
                                    entity2.AccLevel = 4;
                                }
                            }
                            else
                            {
                                entity2.AccCode = AccCode;
                                entity2.AccName = AccName;
                                entity2.ThirdLevel = AccCode;
                                entity2.AccLevel = 3;
                            }
                        }
                        else
                        {
                            entity2.AccCode = AccCode;
                            entity2.AccName = AccName;
                            entity2.SecondLevel = AccCode;
                            entity2.AccLevel = 2;
                        }
                    }
                    else
                    {
                        entity2.AccCode = AccCode;
                        entity2.AccName = AccName;
                        entity2.FirstLevel = AccCode;
                        entity2.AccLevel = 1;
                    }
                    //entity.IsTransaction = Convert.ToBoolean(IsTransaction);
                    entity2.ParentAccCode = ParentCode;
                    entity2.IsTransaction = IsTransaction;
                    entity2.CategoryID = Convert.ToInt32(CategoryID);
                    entity2.Nature = Nature;
                    entity2.OfficeLevel = Convert.ToInt32(OfficeLevel);
                    //entity.ModuleID = Convert.ToInt32(ModuleID);
                    if (NoteID != "" || Convert.ToInt32(NoteID) > 0)
                        entity2.NoteID = Convert.ToInt32(NoteID);
                    AccChartViewModel viewModel = new AccChartViewModel();
                    entity2.CreateUser = SessionHelper.LoggedInUserId.ToString();// viewModel.CreateUserId;
                    entity2.CreateDate = viewModel.CreateDate;
                    entity2.IsActive = true;
                    entity2.InActiveDate = viewModel.CreateDate;
                    entity2.BrokerBranchId = SessionHelper.LoggedInOfficeId;
                    entity2.OrgID = 1;// Convert.ToInt32(LoggedInOrganizationID);    

                    accChartService.Update(entity2);
                    Acc_Id = entity2.AccID;
                }
               

                //var emtpy = new AccChartViewModel();
                //MapDropDownList(emtpy);
                //result = "Saved Successfully.";

                if (Acc_Id > 0)
                    result = "S";
                else
                    result = "F";
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Events
        // GET: AccChart


        public ActionResult AccChartList()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }

        // GET: AccChart/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AccChart/Create
        public ActionResult Create()
        {
            var model = new AccChartViewModel();
            MapDropDownList(model);
            model.IsTransaction = true;
            return View(model);

        }

        // POST: AccChart/Create
        [HttpPost]
        public ActionResult Create(AccChartViewModel model)
        {
            return View();
            //try
            //{
            //    var entity = Mapper.Map<AccChartViewModel, AccChart>(model);

            //    //Add Validlation Logic.

            //    if (ModelState.IsValid)
            //    {

            //        string msg = "";
            //        if (accChartService.IsValidAccCode(entity.AccCode))
            //        {
            //            if (model.FirstLevel != null)
            //            {
            //                entity.FirstLevel = model.FirstLevel;
            //                if (model.SecondLevel != null)
            //                {
            //                    entity.SecondLevel = model.SecondLevel;
            //                    if (model.ThirdLevel != null)
            //                    {
            //                        entity.ThirdLevel = model.ThirdLevel;
            //                        if(model.FourthLevel != null)
            //                        {
            //                            entity.FourthLevel = model.FourthLevel;
            //                            entity.FifthLevel = model.AccCode;
            //                            entity.AccLevel = 5;
            //                        }
            //                        else
            //                        {
            //                            entity.FourthLevel = model.AccCode;
            //                            entity.AccLevel = 4;
            //                        }                                 
            //                    }
            //                    else
            //                    {
            //                        entity.ThirdLevel = model.AccCode;
            //                        entity.AccLevel = 3;
            //                    }
            //                }
            //                else
            //                {
            //                    entity.SecondLevel = model.AccCode;
            //                    entity.AccLevel = 2;
            //                }
            //            }
            //            else
            //            {
            //                entity.FirstLevel = model.AccCode;
            //                entity.AccLevel = 1;
            //            }
            //            entity.IsTransaction = model.IsTransaction;
            //            accChartService.Create(entity);
            //            var emtpy = new AccChartViewModel();
            //            MapDropDownList(emtpy);

            //            return View(emtpy);
            //        }
            //        else
            //        {
            //            var emtpy = new AccChartViewModel();
            //            MapDropDownList(emtpy);
            //            ModelState.AddModelError("Validation", msg);
            //        }

            //    }
            //    MapDropDownList(model);
            //    return View(model);

            //}
            //catch
            //{
            //    return View();
            //}
        }

        // GET: AccChart/Edit/5
        public ActionResult Edit(int Id)
        {
            var comInfo = accChartService.GetById(Convert.ToInt32(Id));
            var entity = Mapper.Map<AccChart, AccChartViewModel>(comInfo);
            entity.Nature = comInfo.Nature;
            if (comInfo != null)
            {
                if (comInfo.ParentAccCode != null)
                {
                    entity.ParentCode = comInfo.ParentAccCode;
                    entity.ParentAccName = accChartService.GetByAccCode(comInfo.ParentAccCode).AccName;
                }
               
            }
           
            MapDropDownList(entity);
            return View(entity);
        }

        // POST: AccChart/Edit/5
        [HttpPost]
        public JsonResult Edit(AccChartViewModel model)
        {
            try
            {
                //var entity = accChartService.GetById(model.AccID);
                //if (ModelState.IsValid)
                //{
                //    entity.AccName = model.AccName;
                //    entity.IsTransaction = model.IsTransaction;
                //    entity.OfficeLevel = model.OfficeLevel;
                //    entity.ModuleID = model.ModuleID;
                //    entity.BrokerBranchId = SessionHelper.LoggedInOfficeId;
                //    if (model.NoteID != 0)
                //        entity.NoteID = model.NoteID;
                //    else
                //        entity.NoteID = null;
                //    accChartService.Update(entity);
                //}
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        // GET: AccChart/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AccChart/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        #endregion
    }
}
