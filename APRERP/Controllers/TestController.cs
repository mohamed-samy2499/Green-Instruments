using APRERP.Models;
using Kendo.Mvc.Examples.Controllers;
using Kendo.Mvc.UI;
using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace APRERP.Controllers
{
    public class TestController : Controller
    {
        // GET: Test


        public ActionResult myIndex(string ListName)
        {
            string siteUrl = "https://hoejrupitdk.sharepoint.com/sites/Hoejrupit";
            SecureString passWord = new SecureString();

            // var password = "Freelancer123!";
            var password = "Freelancer123!";
            var securePassword = new SecureString();
            foreach (char c in password)
            {
                securePassword.AppendChar(c);
            }
            ClientContext clientContext = new ClientContext(siteUrl);
            clientContext.Credentials = new SharePointOnlineCredentials("jhc@hoejrupit.dk", securePassword);/*passWord*/

            List oList = clientContext.Web.Lists.GetByTitle(ListName);
            //get all the lists
            //List<List> oLists = clientContext.Web.Lists.ToList();
            CamlQuery camlQuery = new CamlQuery();
            camlQuery.ViewXml = "<View/>";
            ListItemCollection listItems = oList.GetItems(camlQuery);
            clientContext.Load(oList);
            clientContext.Load(listItems);
            //DateTime timer1 = DateTime.Now;
            clientContext.ExecuteQuery();
            //DateTime timer2 = DateTime.Now;
            //int d3 = (int)(timer2 - timer1).TotalMilliseconds;
            //using (StreamWriter writer = new StreamWriter("D:\\FULL STACK DIPLOMA\\data.txt"))
            //{
            //    writer.WriteLine(d3);
            //}
            //timer1 = DateTime.Now;
            DataTable resdtable = new DataTable();
            if (listItems.Count > 0)
                resdtable = GetDataTableFromListItemCollection(listItems);

            var unwantedColumnNames = unwantedColumns();
            //timer2 = DateTime.Now;
            //int d4 = (int)(timer2 - timer1).TotalMilliseconds;
            //using (StreamWriter writer = new StreamWriter("D:\\FULL STACK DIPLOMA\\data1.txt"))
            //{
            //    writer.WriteLine(d4);
            //}
            List<string> ColumnsHeaders = new List<string>();

            //using (StreamWriter writer = new StreamWriter("D:\\FULL STACK DIPLOMA\\data.txt"))
            //{
            for (int i = 0; i < resdtable.Columns.Count; i++)
            {
                if(!(unwantedColumnNames.Contains(resdtable.Columns[i].ColumnName)))
                    ColumnsHeaders.Add(resdtable.Columns[i].ColumnName);
                // writer.WriteLine("this.DataTable.Columns.Add(\""+ resdtable.Columns[i].ColumnName + "\",typeof(string)); ");
            }
            ViewBag.ColumnsHeaders = ColumnsHeaders;
            ViewBag.ListTitle = oList.Title;
            ViewBag.unwantedColumns = unwantedColumnNames;

            //if (ListName == "unwantedColumns") { 

            //    using (StreamWriter writer = new StreamWriter("D:\\FULL STACK DIPLOMA\\data.txt"))
            //    {
            //        foreach(var name in ColumnsHeaders)
            //        {
            //            if(!(name == "ID" || name == "FilePath"))
            //                writer.WriteLine("unwantedColumnsNames.Add(\""+name+"\");");

            //        }
            //    }
            //}

            //}
            //List<TEstData> estDatas = new List<TEstData>();
            //DataTable dt2 = new DataTable();
            //dt2.Columns.Add("UserName", typeof(string));
            //dt2.Columns.Add("UserAge", typeof(int));

            //for (int i = 0; i < 25; i++)
            //{
            //    DataRow dr3 = dt2.NewRow();
            //    dr3 = dt2.NewRow();
            //    dr3["UserName"] = "Shahnawaz Dhani " + i.ToString(); // or dr[0]="Shahnawaz";
            //    dr3["UserAge"] = 10 + i; // or dr[1]=24;
            //    dt2.Rows.Add(dr3);

            //}
            //for (int i = 0; i < dt2.Rows.Count; i++)
            //{
            //    estDatas.Add(new TEstData()
            //    {
            //        Age = Convert.ToInt32(dt2.Rows[i]["UserAge"]),
            //        Name = dt2.Rows[i]["UserName"].ToString()
            //    });
            //}
            //DataTableVM dataTableVM = new DataTableVM();
            //dataTableVM.DataTable = resdtable.Copy();
            //List<Row> rows = new List<Row>();
            //resdtable.Rows.CopyTo(rows, 0);
            //DataColumn DeleteColumn = new DataColumn();
            //DeleteColumn.ColumnName = "Delete";
            //DeleteColumn.DataType = typeof(HtmlElement);
            //HtmlElement deleteElement = new HtmlElement("a");
            //IDictionary<string,string> keyValuePairs = new Dictionary<string,string>();
            //keyValuePairs.Add("href", " Url.Action('Delete', 'Test')");
            //deleteElement.Attributes(keyValuePairs);
            //DeleteColumn.DefaultValue = deleteElement;
            //resdtable.Columns.Add(DeleteColumn);
            return View(resdtable);
        }
        public List<string> unwantedColumns()
        {
            List<string> unwantedColumnsNames = new List<string>();
            unwantedColumnsNames.Add("ContentTypeId");
            unwantedColumnsNames.Add("Title");
            unwantedColumnsNames.Add("Modified");
            unwantedColumnsNames.Add("Created");
            unwantedColumnsNames.Add("Author");
            unwantedColumnsNames.Add("Editor");
            unwantedColumnsNames.Add("_HasCopyDestinations");
            unwantedColumnsNames.Add("_CopySource");
            unwantedColumnsNames.Add("owshiddenversion");
            unwantedColumnsNames.Add("WorkflowVersion");
            unwantedColumnsNames.Add("_UIVersion");
            unwantedColumnsNames.Add("_UIVersionString");
            unwantedColumnsNames.Add("Attachments");
            unwantedColumnsNames.Add("_ModerationStatus");
            unwantedColumnsNames.Add("_ModerationComments");
            unwantedColumnsNames.Add("InstanceID");
            unwantedColumnsNames.Add("Order");
            unwantedColumnsNames.Add("GUID");
            unwantedColumnsNames.Add("WorkflowInstanceID");
            unwantedColumnsNames.Add("FileRef");
            unwantedColumnsNames.Add("FileDirRef");
            unwantedColumnsNames.Add("Last_x0020_Modified");
            unwantedColumnsNames.Add("Created_x0020_Date");
            unwantedColumnsNames.Add("FSObjType");
            unwantedColumnsNames.Add("SortBehavior");
            unwantedColumnsNames.Add("UniqueId");
            unwantedColumnsNames.Add("ParentUniqueId");
            unwantedColumnsNames.Add("SyncClientId");
            unwantedColumnsNames.Add("ProgId");
            unwantedColumnsNames.Add("ScopeId");
            unwantedColumnsNames.Add("File_x0020_Type");
            unwantedColumnsNames.Add("MetaInfo");
            unwantedColumnsNames.Add("_Level");
            unwantedColumnsNames.Add("_IsCurrentVersion");
            unwantedColumnsNames.Add("ItemChildCount");
            unwantedColumnsNames.Add("FolderChildCount");
            unwantedColumnsNames.Add("Restricted");
            unwantedColumnsNames.Add("OriginatorId");
            unwantedColumnsNames.Add("NoExecute");
            unwantedColumnsNames.Add("ContentVersion");
            unwantedColumnsNames.Add("_ComplianceFlags");
            unwantedColumnsNames.Add("_ComplianceTag");
            unwantedColumnsNames.Add("_ComplianceTagWrittenTime");
            unwantedColumnsNames.Add("_ComplianceTagUserId");
            unwantedColumnsNames.Add("AccessPolicy");
            unwantedColumnsNames.Add("_VirusStatus");
            unwantedColumnsNames.Add("_VirusVendorID");
            unwantedColumnsNames.Add("_VirusInfo");
            unwantedColumnsNames.Add("AppAuthor");
            unwantedColumnsNames.Add("AppEditor");
            unwantedColumnsNames.Add("SMTotalSize");
            unwantedColumnsNames.Add("SMLastModifiedDate");
            unwantedColumnsNames.Add("SMTotalFileStreamSize");
            unwantedColumnsNames.Add("SMTotalFileCount");
            unwantedColumnsNames.Add("ComplianceAssetId");
            unwantedColumnsNames.Add("FileLeafRef");
            unwantedColumnsNames.Add("_CommentFlags");
            unwantedColumnsNames.Add("_CommentCount");
            return unwantedColumnsNames;
        }
        public DataTable GetDataTableFromListItemCollection(ListItemCollection listItems)
        {
            DataTable dt = new DataTable();
            var columnsCount = listItems[0].FieldValues.Keys.Count;
            //for(int i = 0; i < columnsCount; i++)
            //{
            //    if(listItems[0].FieldValues.Keys.ElementAt(i) == "ID" || listItems[0].FieldValues.Keys.ElementAt(i) == "Title")
            //        dt.Columns.Add(listItems[0].FieldValues.Keys.ElementAt(i));    
            //    if(i == 56&& columnsCount > 58)
            //    {
            //        while (i < (columnsCount - 3))
            //        {
            //            dt.Columns.Add(listItems[0].FieldValues.Keys.ElementAt(i));
            //            i++;
            //        }
            //        break;
            //    }

            //}
            foreach (var field in listItems[0].FieldValues.Keys)
            {
                dt.Columns.Add(field);
            }

            foreach (var item in listItems)
            {
                DataRow dr = dt.NewRow();

                foreach (var item1 in item.FieldValues.Select((value, i) => new { i, value }))
                {
                    var obj = item1.value;
                    var index = item1.i;
                    //if(obj.Key == "ID" || obj.Key == "Title" || (index>=56 && index<columnsCount-3))


                    if (obj.Value != null)
                    {
                        string key = obj.Key;
                        string type = obj.Value.GetType().FullName;

                        if (type == "Microsoft.SharePoint.Client.FieldLookupValue")
                        {
                            dr[obj.Key] = ((FieldLookupValue)obj.Value).LookupValue;
                        }
                        else if (type == "Microsoft.SharePoint.Client.FieldUserValue")
                        {
                            dr[obj.Key] = ((FieldUserValue)obj.Value).LookupValue;
                        }
                        else if (type == "Microsoft.SharePoint.Client.FieldUserValue[]")
                        {
                            FieldUserValue[] multValue = (FieldUserValue[])obj.Value;
                            foreach (FieldUserValue fieldUserValue in multValue)
                            {
                                dr[obj.Key] += "&" + fieldUserValue.LookupId + "=" + fieldUserValue.LookupValue;
                            }
                        }
                        else if (type == "Microsoft.SharePoint.Client.FieldLookupValue[]")
                        {
                            FieldLookupValue[] multValue = (FieldLookupValue[])obj.Value;
                            foreach (FieldLookupValue fieldLookupValue in multValue)
                            {
                                dr[obj.Key] += "&" + fieldLookupValue.LookupId + "=" + fieldLookupValue.LookupValue;
                            }
                        }
                        else if (type == "System.DateTime")
                        {
                            if (obj.Value.ToString().Length > 0)
                            {
                                var date = obj.Value.ToString().Split(' ');
                                if (date[0].Length > 0)
                                {
                                    dr[obj.Key] = date[0];
                                }
                            }
                        }
                        else
                        {
                            dr[obj.Key] = obj.Value;
                        }
                    }
                    else
                    {
                        dr[obj.Key] = null;
                    }

                }
                dt.Rows.Add(dr);
            }

            return dt;
        }

        
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, System.Web.Mvc.FormCollection form)
        {
            //if (string.IsNullOrEmpty(txtTitle.Text.ToString()) && string.IsNullOrEmpty(txtPAHCertitifcateID.Text.ToString()) && string.IsNullOrEmpty(txtPAHSerialNo.Text.ToString()))
            //{
            //    MessageBox.Show("Fill all values !");
            //    return;
            //}
            try
            {

            string siteUrl = "https://hoejrupitdk.sharepoint.com/sites/Hoejrupit";
            SecureString passWord = new SecureString();

            var password = "Freelancer123!";
            var securePassword = new SecureString();
            foreach (char c in password)
            {
                securePassword.AppendChar(c);
            }
            ClientContext clientContext = new ClientContext(siteUrl);
            clientContext.Credentials = new SharePointOnlineCredentials("jhc@hoejrupit.dk", securePassword);/*passWord*/
            List oList = clientContext.Web.Lists.GetByTitle("PAH Certificate");
            ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
            ListItem oListItem = oList.AddItem(itemCreateInfo);
            #region DataAssigning
            //oListItem["ContentTypeId"] = record.ContentTypeId.ToString().Trim();
            //oListItem["Title"] = record.Title.ToString().Trim();
            //oListItem["_ModerationComments"] = record._ModerationComments.ToString().Trim();
            //oListItem["File_x0020_Type"] = record.File_x0020_Type.ToString().Trim();
            //oListItem["ComplianceAssetId"] = record.ComplianceAssetId.ToString().Trim();
            //oListItem["PAHCertitifcateID"] = record.PAHCertitifcateID.ToString().Trim();
            //oListItem["PAHSerialNo"] = record.PAHSerialNo.ToString().Trim();
            //oListItem["CertificateNo"] = record.CertificateNo.ToString().Trim();
            //oListItem["Custemor"] = record.Custemor.ToString().Trim();
            //oListItem["OrderNo"] = record.OrderNo.ToString().Trim();
            //oListItem["VesselName"] = record.VesselName.ToString().Trim();
            //oListItem["Partno"] = record.Partno.ToString().Trim();
            //oListItem["SystemSerianlNo"] = record.SystemSerianlNo.ToString().Trim();
            //oListItem["PerformedBy"] = record.PerformedBy.ToString().Trim();
            //oListItem["InitialStartingDate"] = record.InitialStartingDate.ToString().Trim();
            //oListItem["CertifyValid"] = record.CertifyValid.ToString().Trim();
            //oListItem["IMO"] = record.IMO.ToString().Trim();
            //oListItem["GIFilesID"] = record.GIFilesID.ToString().Trim();
            //oListItem["FacCalibrationDate"] = record.FacCalibrationDate.ToString().Trim();
            //oListItem["FilePath"] = record.FilePath.ToString().Trim();
            //oListItem["ID"] = record.ID.ToString().Trim();
            //oListItem["Modified"] = record.Modified.ToString().Trim();
            //oListItem["Created"] = record.Created.ToString().Trim();
            //oListItem["Author"] = record.Author.ToString().Trim();
            //oListItem["Editor"] = record.Editor.ToString().Trim();
            //oListItem["_HasCopyDestinations"] = record._HasCopyDestinations.ToString().Trim();
            //oListItem["_CopySource"] = record._CopySource.ToString().Trim();
            //oListItem["owshiddenversion"] = record.owshiddenversion.ToString().Trim();
            //oListItem["WorkflowVersion"] = record.WorkflowVersion.ToString().Trim();
            //oListItem["_UIVersion"] = record._UIVersion.ToString().Trim();
            //oListItem["_UIVersionString"] = record._UIVersionString.ToString().Trim();
            //oListItem["Attachments"] = record.Attachments.ToString().Trim();
            //oListItem["_ModerationStatus"] = record._ModerationStatus.ToString().Trim();
            //oListItem["InstanceID"] = record.InstanceID.ToString().Trim();
            //oListItem["Order"] = record.Order.ToString().Trim();
            //oListItem["GUID"] = record.GUID.ToString().Trim();
            //oListItem["WorkflowInstanceID"] = record.WorkflowInstanceID.ToString().Trim();
            //oListItem["FileRef"] = record.FileRef.ToString().Trim();
            //oListItem["FileDirRef"] = record.FileDirRef.ToString().Trim();
            //oListItem["Last_x0020_Modified"] = record.Last_x0020_Modified.ToString().Trim();
            //oListItem["Created_x0020_Date"] = record.Created_x0020_Date.ToString().Trim();
            //oListItem["FSObjType"] = record.FSObjType.ToString().Trim();
            //oListItem["SortBehavior"] = record.SortBehavior.ToString().Trim();
            //oListItem["FileLeafRef"] = record.FileLeafRef.ToString().Trim();
            //oListItem["UniqueId"] = record.UniqueId.ToString().Trim();
            //oListItem["ParentUniqueId"] = record.ParentUniqueId.ToString().Trim();
            //oListItem["SyncClientId"] = record.SyncClientId.ToString().Trim();
            //oListItem["ProgId"] = record.ProgId.ToString().Trim();
            //oListItem["ScopeId"] = record.ScopeId.ToString().Trim();
            //oListItem["MetaInfo"] = record.MetaInfo.ToString().Trim();
            //oListItem["_Level"] = record._Level.ToString().Trim();
            //oListItem["_IsCurrentVersion"] = record._IsCurrentVersion.ToString().Trim();
            //oListItem["ItemChildCount"] = record.ItemChildCount.ToString().Trim();
            //oListItem["FolderChildCount"] = record.FolderChildCount.ToString().Trim();
            //oListItem["Restricted"] = record.Restricted.ToString().Trim();
            //oListItem["OriginatorId"] = record.OriginatorId.ToString().Trim();
            //oListItem["NoExecute"] = record.NoExecute.ToString().Trim();
            //oListItem["ContentVersion"] = record.ContentVersion.ToString().Trim();
            //oListItem["_ComplianceFlags"] = record._ComplianceFlags.ToString().Trim();
            //oListItem["_ComplianceTag"] = record._ComplianceTag.ToString().Trim();
            //oListItem["_ComplianceTagWrittenTime"] = record._ComplianceTagWrittenTime.ToString().Trim();
            //oListItem["_ComplianceTagUserId"] = record._ComplianceTagUserId.ToString().Trim();
            //oListItem["AccessPolicy"] = record.AccessPolicy.ToString().Trim();
            //oListItem["_VirusStatus"] = record._VirusStatus.ToString().Trim();
            //oListItem["_VirusVendorID"] = record._VirusVendorID.ToString().Trim();
            //oListItem["_VirusInfo"] = record._VirusInfo.ToString().Trim();
            //oListItem["AppAuthor"] = record.AppAuthor.ToString().Trim();
            //oListItem["AppEditor"] = record.AppEditor.ToString().Trim();
            //oListItem["SMTotalSize"] = record.SMTotalSize.ToString().Trim();
            //oListItem["SMLastModifiedDate"] = record.SMLastModifiedDate.ToString().Trim();
            //oListItem["SMTotalFileStreamSize"] = record.SMTotalFileStreamSize.ToString().Trim();
            //oListItem["SMTotalFileCount"] = record.SMTotalFileCount.ToString().Trim();
            //oListItem["_CommentFlags"] = record._CommentFlags.ToString().Trim();
            //oListItem["_CommentCount"] = record._CommentCount.ToString().Trim();

            #endregion

            for (int i = 0; i < form.Count; i++)
            {
                string item = form[form.AllKeys[i]];
                if (!string.IsNullOrEmpty(form[form.AllKeys[i]].ToString()) && form.AllKeys[i] != "RowVersion" && form.AllKeys[i] != "IsNew" && form.AllKeys[i] != "IsEdit")
                    oListItem[form.AllKeys[i]] = form[form.AllKeys[i]].ToString().Trim();
            }
            //oListItem["Title"] = "Viper";
            //oListItem["PAHCertitifcateID"] = "0909090909";
            // oListItem["ID"] = 786;
            oListItem.Update();

            clientContext.ExecuteQuery();
            }
            catch(Exception ex)
            {
                throw new HttpException(404, ex.Message);
            }
            return RedirectToAction("myIndex");

        }


        public ActionResult Delete([DataSourceRequest] DataSourceRequest request, int ID)
        {
            string siteUrl = "https://hoejrupitdk.sharepoint.com/sites/Hoejrupit";
            SecureString passWord = new SecureString();

            var password = "Freelancer123!";
            var securePassword = new SecureString();
            foreach (char c in password)
            {
                securePassword.AppendChar(c);
            }
            ClientContext clientContext = new ClientContext(siteUrl);
            clientContext.Credentials = new SharePointOnlineCredentials("jhc@hoejrupit.dk", securePassword);/*passWord*/
            List oList = clientContext.Web.Lists.GetByTitle("PAH Certificate");
            ListItem itemToDelete = oList.GetItemById(ID);
            itemToDelete.DeleteObject();
            clientContext.ExecuteQuery();
            return View();


        }


        public ActionResult Documents(string twoFoldersPathMerged)
        {
            ViewBag.ErrorMessage = "false";
            ViewBag.Doc = twoFoldersPathMerged;
            string mainDoc = "";
            string secondDoc = "";
            for (int i = 0; i < twoFoldersPathMerged.Length; i++)
            {
                if (twoFoldersPathMerged[i] == '_')
                {
                    mainDoc = twoFoldersPathMerged.Substring(0, i);
                    secondDoc = twoFoldersPathMerged.Substring(i + 1);
                }
            }
            ViewBag.mainDoc = mainDoc;
            ViewBag.secondDoc = secondDoc;
            return View();
        }
        public ActionResult AttachmentsError()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Read(string twoFoldersPathMerged)
        {
            try
            {

                string mainDoc = "";
                string secondDoc = "";
                for (int i = 0; i < twoFoldersPathMerged.Length; i++)
                {
                    if (twoFoldersPathMerged[i] == '_')
                    {
                        mainDoc = twoFoldersPathMerged.Substring(0, i);
                        secondDoc = twoFoldersPathMerged.Substring(i + 1);
                    }
                }
                ViewBag.mainDoc = mainDoc;
                ViewBag.secondDoc = secondDoc;
                string siteUrl = "https://hoejrupitdk.sharepoint.com/sites/Hoejrupit";
                var password = "Freelancer123!";
                var securePassword = new SecureString();
                foreach (char c in password)
                {
                    securePassword.AppendChar(c);
                }
                ClientContext clientContext = new ClientContext(siteUrl);
                clientContext.Credentials = new SharePointOnlineCredentials("jhc@hoejrupit.dk", securePassword);/*passWord*/
                Microsoft.SharePoint.Client.Folder files = clientContext.Web.GetFolderByServerRelativeUrl(siteUrl + "/Shared Documents/" + mainDoc + "/" + secondDoc);
                clientContext.Load(files, k => k.Files, k => k.Folders);
                clientContext.ExecuteQuery();
                List<Microsoft.SharePoint.Client.File> myFiles = new List<Microsoft.SharePoint.Client.File>();
                List<FileManagerEntry> myFileManager = new List<FileManagerEntry>();

                for (int i = 0; i < files.Files.Count; i++)
                {
                    myFiles.Add(files.Files[i]);
                }
                foreach(var file in myFiles)
                {

                    var fileRef = file.ServerRelativeUrl;
                    var fileInfo = Microsoft.SharePoint.Client.File.OpenBinaryDirect(clientContext, fileRef);
                    //var fileName = Path.Combine( "~/Attachments", (string)file.Name);
                    //Directory.CreateDirectory("~/Content/Attachments/" + mainDoc);
                    //Directory.CreateDirectory("~/Content/Attachments/" + mainDoc + "/" + secondDoc);
                    //var virtualPath = "~/Content/Attachments/" + mainDoc;
                    //var filePath = HostingEnvironment.MapPath(virtualPath);
                    string path = "~/Content/Attachments/" + mainDoc;
                    string MainPath = "";
                    string activeDir = @"~/Content/Attachments/";
                    if (!Directory.Exists(Server.MapPath(path)))
                    {
                        MainPath = HostingEnvironment.MapPath(Path.Combine(activeDir, mainDoc));
        
                        Directory.CreateDirectory(MainPath);
                    }
                    else
                    {
                        MainPath = HostingEnvironment.MapPath(Path.Combine(activeDir, mainDoc));

                    }
                    string path1 = "~/Content/Attachments/" + mainDoc + "/" + secondDoc;
                    string MainPath1 = "";
                    string activeDir1 = @"~/Content/Attachments/" + mainDoc + "/";
                    if (!Directory.Exists(Server.MapPath(path1)))
                    {
                        MainPath1 = HostingEnvironment.MapPath(Path.Combine(activeDir1, secondDoc));

                        Directory.CreateDirectory(MainPath1);
                    }
                    else
                    {
                        MainPath1 = HostingEnvironment.MapPath(Path.Combine(activeDir1, secondDoc));

                    }

                    var path2 = Path.Combine(MainPath1,
                                            System.IO.Path.GetFileName(file.Name));
                
                    using (var fs = new FileStream(path2, FileMode.Create, FileAccess.Write))
                    {
                        fileInfo.Stream.CopyTo(fs);
                    }
                }

                string rootPath = Server.MapPath("~/Content/Attachments/");
                foreach (var file in myFiles)
                {
                    myFileManager.Add(new FileManagerEntry
                    {
                        Name = file.Name,
                        Size = 20,
                        Path = rootPath + mainDoc + '/' + secondDoc + '/' + file.Name,
                        Extension = "",
                        IsDirectory = false,
                        HasDirectories = false,
                        CreatedUtc = DateTime.UtcNow,
                        Created = DateTime.Now,
                        ModifiedUtc = DateTime.UtcNow,
                        Modified = DateTime.Now
                    });
                }
                //foreach(var file in myFiles)
                //{
                //    myFileManager.Add(file);
                //}
                ViewBag.ErrorMessage = "false";

                return Json(myFileManager, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                //Response.Write("<script>alert('Data inserted successfully')</script>");
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);
                ViewBag.ErrorMessage = "true";
               // Server.Transfer("AttachmentsError.cshtml");
                //return Json(new {});
                return RedirectToAction("AttachmentsError");

                //return Json(Url.Action("AttachmentsError", "Test"));
                //throw new Exception("there are no attachments");
            }
        }

        [HttpGet]
        public FileResult Download(string path)
        {
            //var virtualPath = "~/Content/Attachments/" + path;
            //var filePath = HostingEnvironment.MapPath(virtualPath);
            FileInfo file = new FileInfo(path);

            System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
            {
                FileName = file.Name,
                Inline = false
            };
            Response.Headers.Add("Content-Disposition", cd.ToString());
            Response.Headers.Add("X-Content-Type-Options", "nosniff");

            string contentType = MimeMapping.GetMimeMapping(file.Name);
            var readStream = System.IO.File.ReadAllBytes(path);
            return File(readStream, contentType);
        }
        public ActionResult TreeViewIndex()
        {

            List<string> result = new List<string>();
            string siteUrl = "https://hoejrupitdk.sharepoint.com/sites/Hoejrupit";
            SecureString passWord = new SecureString();

            // var password = "Freelancer123!";
            var password = "Freelancer123!";
            var securePassword = new SecureString();
            foreach (char c in password)
            {
                securePassword.AppendChar(c);
            }
            ClientContext clientContext = new ClientContext(siteUrl);
            clientContext.Credentials = new SharePointOnlineCredentials("jhc@hoejrupit.dk", securePassword);/*passWord*/
            //List oList = clientContext.Web.Lists.GetByTitle("PAH Certificate");
            //get all the lists



            List<string> AllListsNames = unwantedListNames();

            var resultNames = clientContext.Web.Lists;
            //CamlQuery camlQuery = new CamlQuery();
            //camlQuery.ViewXml = "<View/>";
            //ListItemCollection listItems = oList.GetItems(camlQuery);
            clientContext.Load(resultNames);
            //DateTime timer1 = DateTime.Now;
            clientContext.ExecuteQuery();
                foreach (var resultName in resultNames) {
                    if(!AllListsNames.Contains(resultName.Title))
                        result.Add(resultName.Title);
                }

            return View(result);
        }
        public ActionResult SearchAllLists(string SearchValue) {


            List<List> Preresult = new List<List>();
            List<DataTable> Results = new List<DataTable>();
            string siteUrl = "https://hoejrupitdk.sharepoint.com/sites/Hoejrupit";
            SecureString passWord = new SecureString();

            // var password = "Freelancer123!";
            var password = "Freelancer123!";
            var securePassword = new SecureString();
            foreach (char c in password)
            {
                securePassword.AppendChar(c);
            }
            ClientContext clientContext = new ClientContext(siteUrl);
            clientContext.Credentials = new SharePointOnlineCredentials("jhc@hoejrupit.dk", securePassword);/*passWord*/
            //List oList = clientContext.Web.Lists.GetByTitle("PAH Certificate");
            //get all the lists



            List<string> AllListsNames = unwantedListNames();

            var resultNames = clientContext.Web.Lists;
            //CamlQuery camlQuery = new CamlQuery();
            //camlQuery.ViewXml = "<View/>";
            //ListItemCollection listItems = oList.GetItems(camlQuery);
            clientContext.Load(resultNames);
            //DateTime timer1 = DateTime.Now;
            clientContext.ExecuteQuery();
            foreach (var resultName in resultNames)
            {
                if (!AllListsNames.Contains(resultName.Title))
                    Preresult.Add(resultName);
            }
            var ListTitles = new List<string>();
            foreach (var result in Preresult) { 
                var dataTable = new DataTable();
                DataTable resdtable = getDataTableFromListName(result.Title);
                var unwantedColumnNames = unwantedColumns();
                if(resdtable != null)
                {
                    var columnNames = new List<string>();
                    foreach(DataColumn column in resdtable.Columns)
                    {
                        columnNames.Add(column.ColumnName);
                    }
                    foreach(var column in unwantedColumnNames)
                    {
                        if(columnNames.Contains(column))
                            resdtable.Columns.Remove(column);
                        
                    }
                    dataTable = resdtable.Clone();
                    foreach(DataRow row in resdtable.Rows)
                    {
                        foreach(DataColumn col in resdtable.Columns)
                        {
                            if (row[col].ToString().Contains(SearchValue))
                            {
                                dataTable.ImportRow(row);
                            }
                        }
                    }
                    if(dataTable.Rows.Count > 0)
                    {
                        ListTitles.Add(result.Title);
                        Results.Add(dataTable);
                    }
                    ViewBag.ListTitles = ListTitles;
                }
                else
                {
                    continue;
                }


            }
            return View(Results); 
        }
        public DataTable getDataTableFromListName(string ListName)
        {
            string siteUrl = "https://hoejrupitdk.sharepoint.com/sites/Hoejrupit";
            SecureString passWord = new SecureString();


            var password = "Freelancer123!";
            var securePassword = new SecureString();
            foreach (char c in password)
            {
                securePassword.AppendChar(c);
            }
            ClientContext clientContext = new ClientContext(siteUrl);
            clientContext.Credentials = new SharePointOnlineCredentials("jhc@hoejrupit.dk", securePassword);/*passWord*/

            List oList = clientContext.Web.Lists.GetByTitle(ListName);

            CamlQuery camlQuery = new CamlQuery();
            camlQuery.ViewXml = "<View/>";
            ListItemCollection listItems = oList.GetItems(camlQuery);
            clientContext.Load(oList);
            clientContext.Load(listItems);

            clientContext.ExecuteQuery();

            DataTable resdtable = new DataTable();
            if (listItems.Count > 0)
                resdtable = GetDataTableFromListItemCollection(listItems);




            return resdtable;
        }
        public List<string> unwantedListNames()
        {
            List<string> AllListsNames = new List<string>();
            //AllListsNames.Add("PAH Certificate");
            //AllListsNames.Add("Water Monitor Configuration");
            //AllListsNames.Add("ServiceReports");
            //AllListsNames.Add("demolitionist");
            //AllListsNames.Add("samilist5");
            //AllListsNames.Add("samilist3");
            //AllListsNames.Add("samilist4");
            //AllListsNames.Add("Sami2");
            //AllListsNames.Add("Turbidity Certificate");
            //AllListsNames.Add("MaxTam123456");
            //AllListsNames.Add("mylist");
            //AllListsNames.Add("MaxTam1234");
            //AllListsNames.Add("ON H");
            //AllListsNames.Add("demolist");
            AllListsNames.Add("appdata");
            AllListsNames.Add("appfiles");
            AllListsNames.Add("Sammensat udseende");
            AllListsNames.Add("Konverterede formularer");
            AllListsNames.Add("Dokumenter");
            AllListsNames.Add("Formularskabeloner");
            AllListsNames.Add("Galleri med listeskabeloner");
            AllListsNames.Add("Bibliotek over vedligeholdelseslogfiler");
            AllListsNames.Add("Oversigt over mastersider");
            AllListsNames.Add("Søgekonfigurationsliste");
            AllListsNames.Add("SharePointHomeCacheList");
            AllListsNames.Add("Sharing Links");
            AllListsNames.Add("Webstedsaktiver");
            AllListsNames.Add("Webstedssider");
            AllListsNames.Add("Løsningsgalleri");
            AllListsNames.Add("Typografibibliotek");
            AllListsNames.Add("TaxonomyHiddenList");
            AllListsNames.Add("Temagalleri");
            AllListsNames.Add("Liste med brugeroplysninger");
            AllListsNames.Add("Galleri med webdele");
            AllListsNames.Add("Web Template Extensions");
            return AllListsNames;
        }
        //public ActionResult GetDataTable([DataSourceRequest] DataSourceRequest request)
        //{
        //    string siteUrl = "https://hoejrupitdk.sharepoint.com/sites/Hoejrupit";
        //    SecureString passWord = new SecureString();
        //    //public Stopwatch stopwatch  = new Stopwatch();
        //    // var password = "Freelancer123!";
        //    var password = "Freelancer123!";
        //    var securePassword = new SecureString();
        //    foreach (char c in password)
        //    {
        //        securePassword.AppendChar(c);
        //    }
        //    ClientContext clientContext = new ClientContext(siteUrl);
        //    clientContext.Credentials = new SharePointOnlineCredentials("jhc@hoejrupit.dk", securePassword);/*passWord*/
        //    List oList = clientContext.Web.Lists.GetByTitle("PAH Certificate");

        //    CamlQuery camlQuery = new CamlQuery();
        //    camlQuery.ViewXml = "<View/>";
        //    ListItemCollection listItems = oList.GetItems(camlQuery);
        //    clientContext.Load(oList);
        //    clientContext.Load(listItems);
        //    clientContext.ExecuteQuery();

        //    DataTable resdtable = GetDataTableFromListItemCollection(listItems);
        //    //using (StreamWriter writer = new StreamWriter(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/data.txt")))
        //    //{
        //    //    writer.WriteLine("Monica Rathbun");
        //    //    for (int i = 0; i < resdtable.Columns.Count; i++)
        //    //    {
        //    //        writer.WriteLine("oListItem["+ resdtable.Columns[i].ColumnName + "] = record."+ resdtable.Columns[i].ColumnName + "; ");
        //    //    }
        //    //}
        //    //DataColumn DeleteColumn = new DataColumn();
        //    //DeleteColumn.ColumnName = "Delete";
        //    //DeleteColumn.DataType = typeof(HtmlElement);
        //    //DeleteColumn.DefaultValue = "<a href='" + Url.Action("Delete", "Test") + "/#=ID#'>Delete</a>";
        //    //resdtable.Columns.Add(DeleteColumn);
        //    return Json(resdtable);
        //}

        //public ActionResult Index()
        //{
        //    List<TEstData> estDatas = new List<TEstData>();
        //    // Create a DataTable and add two Columns to it
        //    DataTable dt = new DataTable();
        //    dt.Columns.Add("Name", typeof(string));
        //    dt.Columns.Add("Age", typeof(int));

        //    // Create a DataRow, add Name and Age data, and add to the DataTable
        //    DataRow dr = dt.NewRow();
        //    dr["Name"] = "Mohammad"; // or dr[0]="Mohammad";
        //    dr["Age"] = 24; // or dr[1]=24;
        //    dt.Rows.Add(dr);

        //    // Create another DataRow, add Name and Age data, and add to the DataTable
        //    dr = dt.NewRow();
        //    dr["Name"] = "Shahnawaz"; // or dr[0]="Shahnawaz";
        //    dr["Age"] = 25; // or dr[1]=24;
        //    dt.Rows.Add(dr);
        //    dr = dt.NewRow();
        //    dr["Name"] = "Shahnawaz1"; // or dr[0]="Shahnawaz";
        //    dr["Age"] = 26; // or dr[1]=24;
        //    dt.Rows.Add(dr);
        //    dr = dt.NewRow();
        //    dr["Name"] = "Shahnawaz2"; // or dr[0]="Shahnawaz";
        //    dr["Age"] = 27; // or dr[1]=24;
        //    dt.Rows.Add(dr);
        //    dr = dt.NewRow();
        //    dr["Name"] = "Shahnawaz3"; // or dr[0]="Shahnawaz";
        //    dr["Age"] = 28; // or dr[1]=24;
        //    dt.Rows.Add(dr);
        //    dr = dt.NewRow();
        //    dr["Name"] = "Shahnawaz4"; // or dr[0]="Shahnawaz";
        //    dr["Age"] = 29; // or dr[1]=24;
        //    dt.Rows.Add(dr);
        //    dr = dt.NewRow();
        //    dr["Name"] = "Shahnawaz5"; // or dr[0]="Shahnawaz";
        //    dr["Age"] = 30; // or dr[1]=24;
        //    dt.Rows.Add(dr);
        //    dr = dt.NewRow();
        //    dr["Name"] = "Shahnawaz6"; // or dr[0]="Shahnawaz";
        //    dr["Age"] = 31; // or dr[1]=24;
        //    dt.Rows.Add(dr);
        //    dr = dt.NewRow();
        //    dr["Name"] = "Shahnawaz7"; // or dr[0]="Shahnawaz";
        //    dr["Age"] = 32; // or dr[1]=24;
        //    dt.Rows.Add(dr);
        //    dr = dt.NewRow();
        //    dr["Name"] = "Shahnawaz8"; // or dr[0]="Shahnawaz";
        //    dr["Age"] = 33; // or dr[1]=24;
        //    dt.Rows.Add(dr);
        //    dr = dt.NewRow();
        //    dr["Name"] = "Shahnawaz9"; // or dr[0]="Shahnawaz";
        //    dr["Age"] = 34; // or dr[1]=24;
        //    dt.Rows.Add(dr);
        //    DataTable dt2 = new DataTable();
        //    dt2.Columns.Add("UserName", typeof(string));
        //    dt2.Columns.Add("UserAge", typeof(int));

        //    for (int i = 0; i < 25; i++)
        //    {
        //        DataRow dr3 = dt2.NewRow();
        //        dr3 = dt2.NewRow();
        //        dr3["UserName"] = "Shahnawaz Dhani " + i.ToString(); // or dr[0]="Shahnawaz";
        //        dr3["UserAge"] = 10 + i; // or dr[1]=24;
        //        dt2.Rows.Add(dr3);

        //    }
        //    for (int i = 0; i < dt2.Rows.Count; i++)
        //    {
        //        estDatas.Add(new TEstData()
        //        {
        //            Age = Convert.ToInt32(dt2.Rows[i]["UserAge"]),
        //            Name = dt2.Rows[i]["UserName"].ToString()
        //        });
        //    }

        //    // DataBind to your UI control, if necessary (a GridView, in this example)
        //    //GridView1.DataSource = dt;
        //    return View(estDatas);
        //}

    }
}

