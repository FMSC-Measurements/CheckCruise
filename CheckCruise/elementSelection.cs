using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CheckCruise
{
    public partial class elementSelection : Form
    {
        #region
            public string elementToReturn;
            public string toleranceToReturn;
            public string unitToReturn;
            public string parameterToReturn;
            public string weightToReturn;
        #endregion

        public elementSelection()
        {
            InitializeComponent();
        }

        public void setupDialog()
        {
            selectedElement.Text = elementToReturn;
            selectedTolerance.Text = toleranceToReturn;
            selectedUnit.Text = unitToReturn;
            selectedParameter.Text = parameterToReturn;
            selectedWeight.Text = weightToReturn;
            //  make element read only so it can't be changed
            selectedElement.Enabled = false;
            return;
        }   //  end setupDialog


        private void onCancel(object sender, EventArgs e)
        {
            DialogResult nResult = MessageBox.Show("Are you sure you want to cancel?", "CONFIRMATION", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (nResult == DialogResult.Yes)
            {
                Close();
                return;
            }   //  endif
            return;
        }   //  end onCancel

        private void onExit(object sender, EventArgs e)
        {
            elementToReturn = selectedElement.Text;
            toleranceToReturn = selectedTolerance.Text;
            unitToReturn = selectedUnit.Text;
            parameterToReturn = selectedParameter.Text;
            weightToReturn = selectedWeight.Text;
            Close();
            return;
        }   //  end onExit
    }
}
