using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using View.Interface;

namespace View
{
    public class CreditCardViewDialog : IModalDialog
    {
        private CreditCardView view;

        void IModalDialog.BindViewModel<TViewModel>(TViewModel viewModel)
        {
            GetDialog().DataContext = viewModel;
        }

        void IModalDialog.ShowDialog()
        {
            GetDialog().ShowDialog();
        }

        void IModalDialog.Close()
        {
            GetDialog().Close();
        }

        private CreditCardView GetDialog()
        {
            if (view == null)
            {
                view = new CreditCardView();
                view.Closed += new EventHandler(ViewClosed);
            }
            return view;
        }

        void ViewClosed(object sender, EventArgs e)
        {
            view = null;
        }
    }
}
