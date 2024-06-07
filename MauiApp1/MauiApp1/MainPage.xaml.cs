using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        private readonly DbService _dbService;
        private int _editItemId = -1;
        private ObservableCollection<Item> _items;

        public MainPage(DbService dbService)
        {
            InitializeComponent();
            _dbService = dbService;
            _items = new ObservableCollection<Item>();
            Task.Run(async () => await LoadItems());
        }

        private async Task LoadItems()
        {
            var items = await _dbService.GetItems();
            _items.Clear();
            foreach (var item in items)
            {
                _items.Add(item);
            }
            listView.ItemsSource = _items;
        }

        private async void saveButton_Clicked(object sender, EventArgs e)
        {
            if (decimal.TryParse(priceEntryField.Text, out decimal price) && int.TryParse(quantityEntryField.Text, out int quantity))
            {
                if (_editItemId == -1)
                {
                    await _dbService.Create(new Item
                    {
                        Name = nameEntryField.Text,
                        Price = price,
                        Quantity = quantity
                    });
                }
                else
                {
                    var item = _items[_editItemId];
                    item.Name = nameEntryField.Text;
                    item.Price = price;
                    item.Quantity = quantity;
                    await _dbService.Update(item);
                    _editItemId = -1;
                }

                await LoadItems();
                nameEntryField.Text = string.Empty;
                priceEntryField.Text = string.Empty;
                quantityEntryField.Text = string.Empty;
            }
            else
            {
                await DisplayAlert("Error", "Invalid price or quantity. Please enter valid numbers.");
            }
        }

        private void listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = (Item)e.Item;
            nameEntryField.Text = item.Name;
            priceEntryField.Text = item.Price.ToString();
            quantityEntryField.Text = item.Quantity.ToString();
            _editItemId = _items.IndexOf(item);
        }

        private async void deleteButton_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var item = button?.BindingContext as Item;

            if (item != null)
            {
                await _dbService.Delete(item);
                await LoadItems(); 
            }
        }
    }
}