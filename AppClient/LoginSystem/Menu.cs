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
    class Menu
    {
        public List<string> pizzaList = new List<string>();
        public List<string> soumarinList = new List<string>();
        public List<string> dessertList = new List<string>();
        public List<string> boissonList = new List<string>();
        public List<string> pateList = new List<string>();
        public List<string> saladeList = new List<string>();
        public List<string> pouletList = new List<string>();

        public List<List<string>> myMenu = new List<List<string>>();

        public Menu()
        {
            pizzaList.Add("peperoni");
            pizzaList.Add("all dress");
            pizzaList.Add("hawaienne");
            pizzaList.Add("fromage");
            pizzaList.Add("trois viande");

            soumarinList.Add("jambon");
            soumarinList.Add("poulet");
            soumarinList.Add("italien");
            soumarinList.Add("club");

            dessertList.Add("brownies");
            dessertList.Add("creme glace");
            dessertList.Add("smarties");
            dessertList.Add("fondu au chocolat");

            boissonList.Add("biere");
            boissonList.Add("7up");
            boissonList.Add("coke");
            boissonList.Add("jus");

            pateList.Add("aldente");
            pateList.Add("bin molle");
            pateList.Add("sauce tomate");
            pateList.Add("alfredo");
            pateList.Add("a la viande");

            saladeList.Add("cesar");
            saladeList.Add("grec");
            saladeList.Add("tomate-concombre");

            pouletList.Add("panne");
            pouletList.Add("grille");

            myMenu.Add(pizzaList);
            myMenu.Add(soumarinList);
            myMenu.Add(pateList);
            myMenu.Add(saladeList);
            myMenu.Add(pouletList);
            myMenu.Add(boissonList);
            myMenu.Add(dessertList);


        }

        public List<string> getMyMenu(int pos)
        {
            return myMenu[pos];
        }
    }

    
}