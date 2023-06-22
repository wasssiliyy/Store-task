using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_task.ViewModels.UCViewModels
{
    public class ProductsUCViewModel : BaseViewModel
    {
        private string _productName;

        public string ProductName
        {
            get { return _productName; }
            set { _productName = value; OnPropertyChanged(); }
        }

        private int _productQuantity;

        public int ProductQuantity
        {
            get { return _productQuantity; }
            set { _productQuantity = value; OnPropertyChanged(); }
        }

        private string _productPrice;

        public string ProductPrice
        {
            get { return _productPrice; }
            set { _productPrice = value; OnPropertyChanged(); }
        }

        private string _imagePath;

        public string ImagePath
        {
            get { return _imagePath; }
            set { _imagePath = value; OnPropertyChanged(); }
        }
    }
}
