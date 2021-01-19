using System;
using System.Windows;
using ViewModel;

namespace View
{
    public partial class CreditCardView : Window
    {
        public CreditCardView()
        {
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            CreditCardViewModel mc = (CreditCardViewModel) DataContext;
        }
    }
}