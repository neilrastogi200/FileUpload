using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace FileUpload.UI.Validation
{
    public class FileTypesValidation : ValidationAttribute
    {
        private readonly List<string> _types;

        public FileTypesValidation(string types)
        {
            _types = types.Split(',').ToList();
        }

        public override bool IsValid(object value)
        {
            if (value == null) return true;

            var httpPostedFileBase = value as HttpPostedFileBase;

            var fileExt = Path.GetExtension(httpPostedFileBase.FileName).Substring(1);
            return _types.Contains(fileExt, StringComparer.OrdinalIgnoreCase);
        }

        public override string FormatErrorMessage(string name)
        {
            return $"Invalid file type. Only the following types {string.Join(", ", _types)} are supported.";
        }
    }
}