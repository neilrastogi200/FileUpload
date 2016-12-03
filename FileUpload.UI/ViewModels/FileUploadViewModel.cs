using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using FileUpload.UI.Validation;

namespace FileUpload.UI.ViewModels
{
    public class FileUploadViewModel
    {
        public string FileName { get; set; }

        [FileTypesValidation("csv")]
        [Required(ErrorMessage = "Please select a csv. file")]
        public HttpPostedFileBase File { get; set; }

        public ConcurrentBag<string> ErrorList { get; set; }
    }
}