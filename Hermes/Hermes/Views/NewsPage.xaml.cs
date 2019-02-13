using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Hermes.Models;

namespace Hermes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewsPage : ContentPage
    {
        private const string newLine = "\r\n";

        public NewsPage()
        {
            InitializeComponent();
            BindingContext = this;
            
            List<Label> labelArr = generateReports();
            Grid grid = new Grid { Padding = 20 };
            ScrollView scroll = new ScrollView { };
            StackLayout stack = new StackLayout { Orientation = StackOrientation.Vertical };

            grid.Children.Add(scroll);
            scroll.Content = stack;
            for(int i = 0; i < labelArr.Count; i++)
            {
                stack.Children.Add(labelArr[i]);
            }

            this.Content = grid;
        }


        public List<Label> generateReports()
        {
            List<Label> labelArr = new List<Label>();
            List<Item> itemList = new List<Item>();

            Item a1 = new Item
            {
                Id = "1",
                Description = "Hello",
                Text = "World"
            };
            Item a2 = new Item
            {
                Id = "2",
                Description = "Hello",
                Text = "World"
            };
            itemList.Add(a1);
            itemList.Add(a2);
            for (int i = 0; i < itemList.Count; i++)
            {
                labelArr.Add(new Label()
                {
                    Text = itemList[i].Id + newLine + itemList[i].Description + newLine + itemList[i].Text + newLine
                });
            }
            return labelArr;
        }
    }
}
