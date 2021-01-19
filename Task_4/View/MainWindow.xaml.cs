using System;
using System.Windows;
using View.DI;
using ViewModel;

namespace View
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            CreditCardListViewModel mc = (CreditCardListViewModel) DataContext;
            mc.WindowResolver = new CreditCardDetailsResolver();
        }
    }
}