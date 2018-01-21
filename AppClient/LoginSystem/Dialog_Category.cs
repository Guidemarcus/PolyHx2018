﻿using System;
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
    class Dialog_Category : DialogFragment
    {
        private ListView sousSectionListView;
        Menu sousSection;
        private int position;
        private MainClientActivity clientActivityTemp;
        private Button btnDismiss;
        private Button btnPlus;
        private Button btnMinus;
        private TextView nbFood;

        public Dialog_Category(int position, MainClientActivity mainClientActivity)
        {
            this.position = position;
            clientActivityTemp = mainClientActivity;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.SubdivisionCategory, container, false);
            var accessMenuView = inflater.Inflate(Resource.Layout.SubdivisionItem, container, false);

            sousSection = new Menu();
            sousSectionListView = view.FindViewById<ListView>(Resource.Id.listViewSousSection);
            sousSectionListView.Adapter = new CategoryItemsAdapter(clientActivityTemp, sousSection.getMyMenu(position));
            btnDismiss = view.FindViewById<Button>(Resource.Id.dismiss);
            btnDismiss.Click += BtnDismiss_Click;
            btnPlus = accessMenuView.FindViewById<Button>(Resource.Id.btnPlus);
            btnPlus.Click += BtnPlus_Click;
            btnMinus = accessMenuView.FindViewById<Button>(Resource.Id.btnMinus);
            btnMinus.Click += BtnMinus_Click;
            nbFood = accessMenuView.FindViewById<TextView>(Resource.Id.nbCount);
            return view;
        }

        private void BtnMinus_Click(object sender, EventArgs e)
        {
            if(Int32.Parse(nbFood.Text) > 0)
            {
                int temp = Int32.Parse(nbFood.Text);
                temp--;
                nbFood.Text = temp + "";
            }
        }

        private void BtnPlus_Click(object sender, EventArgs e)
        {
            int temp = Int32.Parse(nbFood.Text);
            temp++;
            nbFood.Text = temp + "";
            Console.WriteLine();
        }

        private void BtnDismiss_Click(object sender, EventArgs e)
        {
            this.Dismiss();
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle); //Sets the title bar to invisible
            base.OnActivityCreated(savedInstanceState);
            Dialog.Window.Attributes.WindowAnimations = Resource.Style.category_animation; //set the animation
        }

    }
}