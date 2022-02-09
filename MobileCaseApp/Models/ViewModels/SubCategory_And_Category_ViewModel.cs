using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileCaseApp.Models.ViewModels
{
    public class SubCategory_And_Category_ViewModel
    {
        public IEnumerable<Category> CategoryList { get; set; }
        public SubCategory SubCategory { get; set; }
        public List<string> SubCategoryList { get; set; }
        public string StatusMessage { get; set; }

    }
}
