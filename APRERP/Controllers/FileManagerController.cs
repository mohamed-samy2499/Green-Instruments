using Kendo.Mvc.Infrastructure;
using Kendo.Mvc.UI;
using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Web;
using System.Web.Mvc;

namespace APRERP.Controllers
{
    public class FileManagerDataController : Controller
    {




        public ActionResult Index( string twoFoldersPathMerged)
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
            Microsoft.SharePoint.Client.Folder files  = clientContext.Web.GetFolderByServerRelativeUrl(siteUrl+"/Documnets/"+mainDoc+"/"+secondDoc);
            clientContext.Load(files);
            clientContext.ExecuteQuery();
            List<Microsoft.SharePoint.Client.File> myFiles = new List<Microsoft.SharePoint.Client.File>();
            List<FileManagerEntry> myFileManager = new List<FileManagerEntry>();

            for(int i = 0; i < files.ItemCount; i++)
            {
                myFiles.Add(files.Folders[0].Files[i]);
            }
            //foreach(var file in myFiles)
            //{
            //    myFileManager.Add(file);
            //}
            return View(myFiles);
            //try
            //{
            //    directoryBrowser.Server = Server;

            //    var result = directoryBrowser.GetFiles(path, Filter)
            //        .Concat(directoryBrowser.GetDirectories(path)).Select(VirtualizePath);

            //    return Json(result, JsonRequestBehavior.AllowGet);
            //}
            //catch (DirectoryNotFoundException)
            //{
            //    throw new HttpException(404, "File Not Found");
            //}


            //throw new HttpException(403, "Forbidden");
        }



    //public class FileContentBrowser
    //{
    //    public IEnumerable<FileManagerEntry> GetFiles(string path, string filter)
    //    {
    //        var directory = new DirectoryInfo(Server.MapPath(path));

    //        var extensions = (filter ?? "*").Split(new string[] { ", ", ",", "; ", ";" }, System.StringSplitOptions.RemoveEmptyEntries);

    //        return extensions.SelectMany(directory.GetFiles)
    //            .Select(file => new FileManagerEntry
    //            {
    //                Name = Path.GetFileNameWithoutExtension(file.Name),
    //                Size = file.Length,
    //                Path = file.FullName,
    //                Extension = file.Extension,
    //                IsDirectory = false,
    //                HasDirectories = false,
    //                Created = file.CreationTime,
    //                CreatedUtc = file.CreationTimeUtc,
    //                Modified = file.LastWriteTime,
    //                ModifiedUtc = file.LastWriteTimeUtc
    //            });
    //    }


    }
}

