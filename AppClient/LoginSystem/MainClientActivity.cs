using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace LoginSystem
{
    [Activity(Label = "LoginSystem", MainLauncher = false, Icon = "@drawable/icon")]
    public class MainClientActivity : Activity
    {
        private List<string> categories;
        private ListView categoriesListView;
        CategoryListViewAdapter adapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MenuLayout);
            // Create your application here

            categories = new List<string>();
            categoriesListView = FindViewById<ListView>(Resource.Id.listViewCateg);
            categories.Add("Pizza");
            categories.Add("Sousmarin");
            categories.Add("Pâte");
            categories.Add("Salade");
            categories.Add("Poulet");
            categories.Add("Boisson");
            categories.Add("Dessert");

            adapter = new CategoryListViewAdapter(this, categories);

            categoriesListView.Adapter = adapter;
            categoriesListView.ItemClick += CategoriesListView_ItemClick;

        }

        private void CategoriesListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            FragmentTransaction transaction = FragmentManager.BeginTransaction();
            Dialog_Category categoryDialog = new Dialog_Category(e.Position, this);
            categoryDialog.Show(transaction, "dialog fragment");
            
        }
    }
}