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
    class CategoryItemsAdapter : BaseAdapter<string>
    {
        private List<string> myItems;
        private Context myContext;

        public CategoryItemsAdapter(Context context, List<string> items)
        {
            myItems = items;
            myContext = context;
        }

        public override string this[int position]
        {
            get { return myItems[position]; }
        }

        public override int Count
        {
            get { return myItems.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;

            if (row == null)
            {
                row = LayoutInflater.From(myContext).Inflate(Resource.Layout.SubdivisionItem, null, false);
            }
            TextView nbFood = row.FindViewById<TextView>(Resource.Id.nbCount);
            Button btnPlus = row.FindViewById<Button>(Resource.Id.btnPlus);
            btnPlus.Click += (object sender, EventArgs e) =>
                {
                    int temp = int.Parse(nbFood.Text);
                    temp++;
                    nbFood.Text = temp.ToString();
                };
            Button btnMinus = row.FindViewById<Button>(Resource.Id.btnMinus);
            btnMinus.Click += (object sender, EventArgs e) =>
                {
                    if (Int32.Parse(nbFood.Text) > 0)
                    {
                        int temp = Int32.Parse(nbFood.Text);
                        temp--;
                        nbFood.Text = temp.ToString();
                    }
                };

            TextView text = row.FindViewById<TextView>(Resource.Id.item);
            text.Text = myItems[position];

            return row;
        }
    }
}