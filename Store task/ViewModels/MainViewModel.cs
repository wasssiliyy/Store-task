using Store_task.Commands;
using Store_task.Models;
using Store_task.Repository;
using Store_task.ViewModels.UCViewModels;
using Store_task.Views.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_task.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public RelayCommand InsertCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand SelectionChanged { get; set; }

        private ObservableCollection<Product> _allProducts;

        public ObservableCollection<Product> AllProducts
        {
            get { return _allProducts; }
            set { _allProducts = value; }
        }

        private ObservableCollection<Category> _allCategories;

        public ObservableCollection<Category> AllCategories
        {
            get { return _allCategories; }
            set { _allCategories = value; }
        }


        private Category selectedItem;

        public Category SelectedItem
        {
            get { return selectedItem; }
            set { selectedItem = value; OnPropertyChanged(); }
        }

        public Repo ProductsRepo { get; set; }

        public async Task GetAllCategories()
        {
            await ProductsRepo.GetAllCategories(AllCategories);
        }

        public async Task GetAllProducts()
        {
            await ProductsRepo.GetAllProduct(AllProducts);
        }

        public async void CallCategory()
        {
            await GetAllCategories();
        }

        public async void CallProductUC()
        {
            await GetAllProducts();

            App.MyGrid.Children.Clear();
            ProductsUC productUC;
            ProductsUCViewModel productsViewModel;
            for (int i = 0; i < AllProducts.Count; i++)
            {
                productUC = new ProductsUC();
                productsViewModel = new ProductsUCViewModel();
                productsViewModel.ProductName = AllProducts[i].Name;
                productsViewModel.ProductPrice = $"{AllProducts[i].Price} $";
                productsViewModel.ProductQuantity = AllProducts[i].Quantity;
                productsViewModel.ImagePath = AllProducts[i].ImagePath;
                productUC.DataContext = productsViewModel;
                App.MyGrid.Children.Add(productUC);
            }
        }



        public MainViewModel()
        {
            AllProducts = new ObservableCollection<Product>();
            AllCategories = new ObservableCollection<Category>();
            ProductsRepo = new Repo();

            CallProductUC();
            CallCategory();

            InsertCommand = new RelayCommand((obj) =>
            {
                InsertUC insertUC = new InsertUC();
                InsertUCViewModel insertUCVM = new InsertUCViewModel();
                App.MyGrid.Children.Clear();
                App.MyGrid.Children.Add(insertUC);
            });

            DeleteCommand = new RelayCommand((obj) =>
            {
                DeleteUC deleteUC = new DeleteUC();
                DeleteViewModel deleteUCVM = new DeleteViewModel();
                App.MyGrid.Children.Clear();
                App.MyGrid.Children.Add(deleteUC);
            });


            SelectionChanged = new RelayCommand((obj) =>
            {
                App.MyGrid.Children.Clear();
                ProductsUC productUC;
                ProductsUCViewModel productsViewModel;
                for (int i = 0; i < AllProducts.Count; i++)
                {
                    if (AllProducts[i].CategoryId == SelectedItem.Id)
                    {
                        productUC = new ProductsUC();
                        productsViewModel = new ProductsUCViewModel();
                        productsViewModel.ProductName = AllProducts[i].Name;
                        productsViewModel.ProductPrice = $"{AllProducts[i].Price} $";
                        productsViewModel.ProductQuantity = AllProducts[i].Quantity;
                        productsViewModel.ImagePath = AllProducts[i].ImagePath;
                        productUC.DataContext = productsViewModel;
                        App.MyGrid.Children.Add(productUC);
                    }
                }
            });
        }
    }
}
