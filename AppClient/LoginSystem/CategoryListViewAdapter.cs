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
    class CategoryListViewAdapter: BaseAdapter<string>
    {
        private List<string> myItems;
        private Context myContext;

        public CategoryListViewAdapter(Context context, List<string> items)
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
                row = LayoutInflater.From(myContext).Inflate(Resource.Layout.MenuListCategory, null, false);
            }
            TextView text = row.FindViewById<TextView>(Resource.Id.category);
            text.Text = myItems[position];

            return row;
        }
    }
}