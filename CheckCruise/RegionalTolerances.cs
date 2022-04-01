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
    public partial class RegionalTolerances : Form
    {
        #region
            public string checkFileName;
            dataBaseCommands dbc = new dataBaseCommands();
            BindingSource tolerancesBindingSource = new BindingSource();
            private string currRegion;
            List<TolerancesList> tolList = new List<TolerancesList>();
        #endregion

        public RegionalTolerances()
        {
            InitializeComponent();
        }

        public void setupDialog()
        {
            dbc.DAL = new CruiseDAL.DAL(checkFileName);

            //  What's the region?
            currRegion = dbc.getRegion();
            currentRegion.Text = currRegion;

            //  Grab any tolerances data or pull regional defaults
            //  this command pulls tolerances from current check cruise file
            tolList = dbc.getTolerances();
            //  this pulls the default regional tolerances if the tolerance table in the file is empty
            if (tolList.Count == 0)
            {
                DefaultTolerances dt = new DefaultTolerances();
                tolList = dt.getDefaultTolerances(currRegion);
            }   //  endif

            //  disable check boxes first and then enable as tolerance list sets check boxes
            PrimaryGrossVol.Enabled = false;
            SecondaryGrossVol.Enabled = false;
            PrimarySecondaryGrossVol.Enabled = false;
            PrimaryNetVol.Enabled = false;
            SecondaryNetVol.Enabled = false;
            PrimarySecondaryNetVol.Enabled = false;
            //  seems like because its a bound list, need to set checkboxes first
            checkBoxes();

            tolerancesBindingSource.DataSource = tolList;
            toleranceData.DataSource = tolerancesBindingSource;

            //  fill remaing fields
            updateVolumeSection();

            return;
        }   //  end setupDialog

        
        private void checkBoxes()
        {
            if (tolList[0].T_IncludeVolume == 1) includeVolume.Checked = true;
            if (tolList[0].T_BySpecies == 1) volumeBySpecies.Checked = true;
            if (tolList[0].T_ByProduct == 1) volumeByProduct.Checked = true;
            if (tolList[0].T_CUFTPGtolerance == 1)
            {
                PrimaryGrossVol.Enabled = true;
                PrimaryGrossVol.Checked = true;
                CubicVolume.Checked = true;
            }   //  endif
            else if(tolList[0].T_CUFTSGtolerance == 1)
            {
                SecondaryGrossVol.Enabled = true;
                SecondaryGrossVol.Checked = true;
                CubicVolume.Checked = true;
            }   //  endif
            else if (tolList[0].T_CUFTPSGtolerance == 1)
            {
                PrimarySecondaryGrossVol.Enabled = true;
                PrimarySecondaryGrossVol.Checked = true;
                CubicVolume.Checked = true;
            }   //  endif
            if (tolList[0].T_CUFTPNtolerance == 1)
            {
                PrimaryNetVol.Enabled = true;
                PrimaryNetVol.Checked = true;
            }   //  endif
            if (tolList[0].T_CUFTSNtolerance == 1)
            {
                SecondaryNetVol.Enabled = true;
                SecondaryNetVol.Checked = true;
            }   //  endif
            if (tolList[0].T_CUFTPSNtolerance == 1)
            {
                PrimarySecondaryNetVol.Enabled = true;
                PrimarySecondaryNetVol.Checked = true;
            }   //  endif

            if (tolList[0].T_BDFTPGtolerance == 1)
            {
                PrimaryGrossVol.Enabled = true;
                PrimaryGrossVol.Checked = true;
                BoardVolume.Checked = true;
            }
            else if(tolList[0].T_BDFTSGtolerance == 1)
            {
                SecondaryGrossVol.Enabled = true;
                SecondaryGrossVol.Checked = true;
                BoardVolume.Checked = true;
            }
            else if(tolList[0].T_BDFTPSGtolerance == 1)
            {
                PrimarySecondaryGrossVol.Enabled = true;
                PrimarySecondaryGrossVol.Checked = true;
                BoardVolume.Checked = true;
            }   //  endif
            if (tolList[0].T_BDFTPNtolerance == 1)
            {
                PrimaryNetVol.Enabled = true;
                PrimaryNetVol.Checked = true;
            }   //  endif
            if (tolList[0].T_BDFTSNtolerance == 1)
            {
                SecondaryNetVol.Enabled = true;
                SecondaryNetVol.Checked = true;
            }   //  endif

            if (tolList[0].T_BDFTPSNtolerance == 1)
            {
                PrimarySecondaryNetVol.Enabled = true;
                PrimarySecondaryNetVol.Checked = true;
            }   //  endif

            return;
        }   //  end checkBoxes


        private void onCancel(object sender, EventArgs e)
        {
            DialogResult nResult = MessageBox.Show("Are you sure you want to cancel?\nAny changes made will not be saved.", "CONFIRMATION", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (nResult == DialogResult.Yes)
            {
                Close();
                return;
            }
            return;
        }   //  end onCancel


        private void onExit(object sender, EventArgs e)
        {
            //  update tolerances list with any needed values
            if (individualAccuracy.Text == "0" || individualAccuracy.Text == "")
            {
                MessageBox.Show("Individual accuracy cannot be blank or zero.\nPlease enter a value.", "MISSING VALUE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                individualAccuracy.Focus();
                return;
            }
            else tolList[0].T_ElementAccuracy = (float)Convert.ToDouble(individualAccuracy.Text);

            if (overallAccuracy.Text == "0" || overallAccuracy.Text == "")
            {
                MessageBox.Show("Overall accuracy cannot be blank or zero.\nPlease enter a value.", "MISSING VALUE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                overallAccuracy.Focus();
                return;
            }
            else tolList[0].T_OverallAccuracy = (float)Convert.ToDouble(overallAccuracy.Text);

            if (volumeByProduct.Checked == true)
                tolList[0].T_ByProduct = 1;
            else tolList[0].T_ByProduct = 0;
            if (volumeBySpecies.Checked == true)
                tolList[0].T_BySpecies = 1;
            else tolList[0].T_BySpecies = 0;
            //  need to default to by species if nothing was checked
            if (volumeByProduct.Checked == false && volumeBySpecies.Checked == false)
                tolList[0].T_BySpecies = 1;

            //  same for volume stuff
            if (CubicVolume.Checked == false && BoardVolume.Checked == false)
                CubicVolume.Checked = true;

            if (includeVolume.Checked == true)
            {
                tolList[0].T_IncludeVolume = 1;
                if (CubicVolume.Checked == true)
                {
                    if (PrimaryGrossVol.Checked == true)
                    {
                        tolList[0].T_CUFTPGtolerance = (float)Convert.ToDouble(PrimaryGrossPC.Text);
                        tolList[0].T_CUFTPNtolerance = (float)Convert.ToDouble(PrimaryNetPC.Text);
                    }
                    else if (SecondaryGrossVol.Checked == true)
                    {
                        tolList[0].T_CUFTSGtolerance = (float)Convert.ToDouble(SecondaryGrossPC.Text);
                        tolList[0].T_CUFTSNtolerance = (float)Convert.ToDouble(SecondaryNetPC.Text);
                    }
                    else if (PrimarySecondaryGrossVol.Checked == true)
                    {
                        tolList[0].T_CUFTPSGtolerance = (float)Convert.ToDouble(PrimarySecondaryGrossPC.Text);
                        tolList[0].T_CUFTPSNtolerance = (float)Convert.ToDouble(PrimarySecondaryNetPC.Text);
                    }
                    else
                    {
                        //  default to primary product
                        tolList[0].T_CUFTPGtolerance = (float)Convert.ToDouble(PrimaryGrossPC.Text);
                        tolList[0].T_CUFTPNtolerance = (float)Convert.ToDouble(PrimaryNetPC.Text);
                    }   //  endif
                }
                else if(CubicVolume.Checked == false)
                {
                    //  set all tolerances to zero
                    tolList[0].T_CUFTPGtolerance = 0;
                    tolList[0].T_CUFTPNtolerance = 0;
                    tolList[0].T_CUFTSGtolerance = 0;
                    tolList[0].T_CUFTSNtolerance = 0;
                    tolList[0].T_CUFTPSGtolerance = 0;
                    tolList[0].T_CUFTPSNtolerance = 0;
                }   // endif
                if(BoardVolume.Checked == true)
                {
                    if (PrimaryGrossVol.Checked == true)
                    {
                        tolList[0].T_BDFTPGtolerance = (float)Convert.ToDouble(PrimaryGrossPC.Text);
                        tolList[0].T_BDFTPNtolerance = (float)Convert.ToDouble(PrimaryNetPC.Text);
                    }
                    else if (SecondaryGrossVol.Checked == true)
                    {
                        tolList[0].T_BDFTSGtolerance = (float)Convert.ToDouble(SecondaryGrossPC.Text);
                        tolList[0].T_BDFTSNtolerance = (float)Convert.ToDouble(SecondaryNetPC.Text);
                    }
                    else if (PrimarySecondaryGrossVol.Checked == true)
                    {
                        tolList[0].T_BDFTPSGtolerance = (float)Convert.ToDouble(PrimarySecondaryGrossPC.Text);
                        tolList[0].T_BDFTPSNtolerance = (float)Convert.ToDouble(PrimarySecondaryNetPC.Text);
                    }
                    else
                    {
                        //  default to primary product
                        tolList[0].T_CUFTPGtolerance = (float)Convert.ToDouble(PrimaryGrossPC.Text);
                        tolList[0].T_CUFTPNtolerance = (float)Convert.ToDouble(PrimaryNetPC.Text);
                    }   //  endif
                }
                else if(BoardVolume.Checked == false)
                {
                    tolList[0].T_BDFTPGtolerance = 0;
                    tolList[0].T_BDFTPNtolerance = 0;
                    tolList[0].T_BDFTSGtolerance = 0;
                    tolList[0].T_BDFTSNtolerance = 0;
                    tolList[0].T_BDFTPSGtolerance = 0;
                    tolList[0].T_BDFTPSNtolerance = 0;
                }   //  endif
            }
            else if (includeVolume.Checked == false)
            {
                tolList[0].T_IncludeVolume = 0;
                if (CubicVolume.Checked == true)
                {
                    if (PrimaryGrossVol.Checked == true)
                    {
                        tolList[0].T_CUFTPGtolerance = 1;
                        tolList[0].T_CUFTPNtolerance = 1;
                        tolList[0].T_CUFTSGtolerance = 0;
                        tolList[0].T_CUFTSNtolerance = 0;
                        tolList[0].T_CUFTPSGtolerance = 0;
                        tolList[0].T_CUFTPSNtolerance = 0;
                    }
                    else if (SecondaryGrossVol.Checked == true)
                    {
                        tolList[0].T_CUFTSGtolerance = 1;
                        tolList[0].T_CUFTSNtolerance = 1;
                        tolList[0].T_CUFTPGtolerance = 0;
                        tolList[0].T_CUFTPNtolerance = 0;
                        tolList[0].T_CUFTPSGtolerance = 0;
                        tolList[0].T_CUFTPSNtolerance = 0;
                    }
                    else if (PrimarySecondaryGrossVol.Checked == true)
                    {
                        tolList[0].T_CUFTPSGtolerance = 1;
                        tolList[0].T_CUFTPSNtolerance = 1;
                        tolList[0].T_CUFTPGtolerance = 0;
                        tolList[0].T_CUFTPNtolerance = 0;
                        tolList[0].T_CUFTSGtolerance = 0;
                        tolList[0].T_CUFTSNtolerance = 0;
                    }   //  endif
                }   //  endif
                if (BoardVolume.Checked == true)
                {
                    if (PrimaryGrossVol.Checked == true)
                    {
                        tolList[0].T_BDFTPGtolerance = 1;
                        tolList[0].T_BDFTPNtolerance = 1;
                        tolList[0].T_BDFTSGtolerance = 0;
                        tolList[0].T_BDFTSNtolerance = 0;
                        tolList[0].T_BDFTPSGtolerance = 0;
                        tolList[0].T_BDFTPSNtolerance = 0;
                    }
                    else if (SecondaryGrossVol.Checked == true)
                    {
                        tolList[0].T_BDFTSGtolerance = 1;
                        tolList[0].T_BDFTSNtolerance = 1;
                        tolList[0].T_BDFTPGtolerance = 0;
                        tolList[0].T_BDFTPNtolerance = 0;
                        tolList[0].T_BDFTPSGtolerance = 0;
                        tolList[0].T_BDFTPSNtolerance = 0;
                    }
                    else if (PrimarySecondaryGrossVol.Checked == true)
                    {
                        tolList[0].T_BDFTPSGtolerance = 1;
                        tolList[0].T_BDFTPSNtolerance = 1;
                        tolList[0].T_BDFTPGtolerance = 0;
                        tolList[0].T_BDFTPNtolerance = 0;
                        tolList[0].T_BDFTSGtolerance = 0;
                        tolList[0].T_BDFTSNtolerance = 0;
                    }   //  endif
                }   //  endif
            }   //  endif
            

            //  add date stamp
            tolList[0].T_DateStamp = DateTime.Now.ToString();

            dbc.saveTolerances(tolList);

            Close();
            return;
        }   //  end onExit

        private void onDeleteRow(object sender, EventArgs e)
        {
            int nthRow = toleranceData.CurrentCell.RowIndex;
            toleranceData.Rows.RemoveAt(nthRow);
        }  //  end onDeleteRow


        private void updateVolumeSection()
        {
            if (tolList[0].T_IncludeVolume == 1)
            {
                if(tolList[0].T_BySpecies == 1)
                {
                    volumeBySpecies.Enabled = true;
                    volumeBySpecies.Checked = true;
                    volumeByProduct.Enabled = false;
                }   //  endif BySpecies
                if (tolList[0].T_ByProduct == 1)
                {
                    volumeByProduct.Enabled = true;
                    volumeByProduct.Checked = true;
                    volumeBySpecies.Enabled = false;
                }   //  endif ByProduct
                //  set type of volume
                if (tolList[0].T_CUFTPGtolerance > 0 && tolList[0].T_CUFTPNtolerance > 0)
                {
                    //  check the box 
                    CubicVolume.Enabled = true;
                    CubicVolume.Checked = true;
                }   //  endif
                if(tolList[0].T_BDFTPGtolerance > 0 && tolList[0].T_BDFTPNtolerance >0)
                {
                    BoardVolume.Enabled = true;
                    BoardVolume.Checked = true;
                }   //  endif

                //  Primary product
                if(tolList[0].T_CUFTPGtolerance > 0 || tolList[0].T_BDFTPGtolerance > 0)
                {
                    disableEnablePrimary(true);
                    PrimaryGrossVol.Checked = true;
                    PrimaryNetVol.Checked = true;
                    PrimaryGrossPC.Text = tolList[0].T_CUFTPGtolerance.ToString();
                    PrimaryNetPC.Text = tolList[0].T_CUFTPNtolerance.ToString();
                    //  diable all other products
                    disableEnableSecondary(false);
                    disableEnablePrimarySecondary(false);
                }   //  endif updating primary product

                //  Secondary product
                if (tolList[0].T_CUFTSGtolerance > 0 || tolList[0].T_BDFTSGtolerance > 0)
                {
                    disableEnableSecondary(true);
                    SecondaryGrossVol.Checked = true;
                    SecondaryNetVol.Checked = true;
                    SecondaryGrossPC.Text = tolList[0].T_CUFTSGtolerance.ToString();
                    SecondaryNetPC.Text = tolList[0].T_CUFTSNtolerance.ToString();
                    //  disable other products
                    disableEnablePrimary(false);
                    disableEnablePrimarySecondary(false);
                }   //  endif updating secondary product

                //  Primary and Secondary product
                if (tolList[0].T_CUFTPSGtolerance > 0 || tolList[0].T_BDFTPSGtolerance > 0)
                {
                    disableEnablePrimarySecondary(true);
                    PrimarySecondaryGrossVol.Checked = true;
                    PrimarySecondaryNetVol.Checked = true;
                    PrimarySecondaryGrossPC.Text = tolList[0].T_CUFTPSGtolerance.ToString();
                    PrimarySecondaryNetPC.Text = tolList[0].T_CUFTPSNtolerance.ToString();
                    //  disable other products
                    disableEnablePrimary(false);
                    disableEnableSecondary(false);
                }   //  endif updating primary/secondary product

            }
            else if(tolList[0].T_IncludeVolume != 1 )
            {
                //  volume fields need to be disabled
                volumeByProduct.Enabled = true;
                volumeBySpecies.Enabled = true;
                CubicVolume.Enabled = true;
                BoardVolume.Enabled = true;
                PrimaryGrossPC.Enabled = false;
                PrimaryNetPC.Enabled = false;
                SecondaryGrossPC.Enabled = false;
                SecondaryNetPC.Enabled = false;
                PrimarySecondaryGrossPC.Enabled = false;
                PrimarySecondaryNetPC.Enabled = false;
            }   //  endif

            //  update accuracy fields even if volume was or was not included
            if (currRegion == "6" || currRegion == "06")
                individualAccuracy.Text = "90";
            else if (currRegion == "10")
                individualAccuracy.Text = "80";
            else individualAccuracy.Text = tolList[0].T_ElementAccuracy.ToString();
            overallAccuracy.Text = tolList[0].T_OverallAccuracy.ToString();
            return;
        }   //  end updateVolumeSection


        private void onIncludeVolume(object sender, EventArgs e)
        {
            //  when checked, volume fields need to be enabled
            volumeByProduct.Enabled = true;
            volumeBySpecies.Enabled = true;
            CubicVolume.Enabled = true;
            BoardVolume.Enabled = true;
            disableEnablePrimary(true);
            disableEnableSecondary(true);
            disableEnablePrimarySecondary(true);
        }   //  end onIncludeVolume

        private void onAdd(object sender, EventArgs e)
        {
            elementSelection es = new elementSelection();
            es.ShowDialog();
            //  make sure element selected is not already in the tolerances list
            int nthRow = tolList.FindIndex(
                delegate(TolerancesList t)
                {
                    return t.T_Element == es.elementToReturn;
                });
            if (nthRow >= 0)
            {
                MessageBox.Show("Selected element is already in the list.\nPlease make a different selection.", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }   //  endif
            TolerancesList tl = new TolerancesList();
            tl.T_Element = es.elementToReturn;
            tl.T_Tolerance = es.toleranceToReturn;
            tl.T_Units = es.unitToReturn;
            tl.T_AddParam = es.parameterToReturn;
            tl.T_Weight = (float)Convert.ToDouble(es.weightToReturn);
            tolList.Add(tl);
            tolerancesBindingSource.ResetBindings(false);
        }   //  end onAdd

        private void onEdit(object sender, EventArgs e)
        {
            int nthRow = toleranceData.CurrentCell.RowIndex;
            elementSelection es = new elementSelection();
            es.elementToReturn = tolList[nthRow].T_Element;
            es.toleranceToReturn = tolList[nthRow].T_Tolerance;
            es.unitToReturn = tolList[nthRow].T_Units;
            es.parameterToReturn = tolList[nthRow].T_AddParam;
            es.weightToReturn = tolList[nthRow].T_Weight.ToString();
            es.setupDialog();
            es.ShowDialog();
            tolList[nthRow].T_Element = es.elementToReturn;
            tolList[nthRow].T_Tolerance = es.toleranceToReturn;
            tolList[nthRow].T_Units = es.unitToReturn;
            tolList[nthRow].T_AddParam = es.parameterToReturn;
            tolList[nthRow].T_Weight = (float)Convert.ToDouble(es.weightToReturn);
            tolerancesBindingSource.ResetBindings(false);
            return;
        }

        private void disableEnablePrimary(Boolean displayStatus)
        {
            PrimaryGrossVol.Enabled = displayStatus;
            PrimaryGrossPC.Enabled = displayStatus;
            PrimaryNetVol.Enabled = displayStatus;
            PrimaryNetPC.Enabled = displayStatus;
            return;
        }   //  end disableEnablePrimary

        private void disableEnableSecondary(Boolean displayStatus)
        {
            SecondaryGrossVol.Enabled = displayStatus;
            SecondaryGrossPC.Enabled = displayStatus;
            SecondaryNetVol.Enabled = displayStatus;
            SecondaryNetPC.Enabled = displayStatus;
            return;
        }   //  end disableEnableSecondary

        private void disableEnablePrimarySecondary(Boolean displayStatus)
        {
            PrimarySecondaryGrossVol.Enabled = displayStatus;
            PrimarySecondaryGrossPC.Enabled = displayStatus;
            PrimarySecondaryNetVol.Enabled = displayStatus;
            PrimarySecondaryNetPC.Enabled = displayStatus;
            return;
        }   //  end disableEnablePrimarySecondary

        private void onPrimaryGrossVol(object sender, EventArgs e)
        {
            if (PrimaryGrossVol.Checked == true)
            {
                //  disable other gross check boxes
                SecondaryGrossVol.Enabled = false;
                PrimarySecondaryGrossVol.Enabled = false;
            }
            else if (PrimaryGrossVol.Checked == false)
            {
                PrimaryGrossVol.Enabled = false;
                //  enable other gross check boxes
                SecondaryGrossVol.Enabled = true;
                PrimarySecondaryGrossVol.Enabled = true;
            }   //  endif
        }   //  end onPrimaryGrossVOl

        private void onSecondaryGrossVol(object sender, EventArgs e)
        {
            if (SecondaryGrossVol.Checked == true)
            {
                //  disable other boxes
                PrimaryGrossVol.Enabled = false;
                PrimarySecondaryGrossVol.Enabled = false;
            }
            else if (SecondaryGrossVol.Checked == false)
            {
                SecondaryGrossVol.Enabled = false;
                //  enable other check boxes
                PrimaryGrossVol.Enabled = true;
                PrimarySecondaryGrossVol.Enabled = true;
            }   //  endif
        }   //  end onSecondaryGrossVol

        private void onPrimarySecondaryGrossVol(object sender, EventArgs e)
        {
            if(PrimarySecondaryGrossVol.Checked == true)
            {
                //  disable other boxes
                PrimaryGrossVol.Enabled = false;
                SecondaryGrossVol.Enabled = false;
            }
            else if(PrimarySecondaryGrossVol.Checked == false)
            {
                PrimarySecondaryGrossVol.Enabled = false;
                //  endable other boxes
                PrimaryGrossVol.Enabled = true;
                SecondaryGrossVol.Enabled = true;
            }   //  endif
        }   //  end onPrimarySecondaryGrossVol

        private void onPrimaryNetVol(object sender, EventArgs e)
        {
            if(PrimaryNetVol.Checked == true)
            {
                //  disable other boxes
                SecondaryNetVol.Enabled = false;
                PrimarySecondaryNetVol.Enabled = false;
            }
            else if(PrimaryNetVol.Checked == false)
            {
                PrimaryNetVol.Enabled = false;
                //  enable other boxes
                SecondaryNetVol.Enabled = true;
                PrimarySecondaryNetVol.Enabled = true;
            }   //  endif
        }   //  end onPrimaryNetVol

        private void onSecondaryNetVol(object sender, EventArgs e)
        {
            if (SecondaryNetVol.Checked == true)
            {
                //  disable other boxes
                PrimaryNetVol.Enabled = false;
                PrimarySecondaryNetVol.Enabled = false;
            }
            else if (SecondaryNetVol.Checked == false)
            {
                SecondaryNetVol.Enabled = false;
                //  enable other boxes
                PrimaryNetVol.Enabled = true;
                PrimarySecondaryNetVol.Enabled = true;
            }   //  endif
        }

        private void onPrimarySecondaryNetVol(object sender, EventArgs e)
        {
            if (PrimarySecondaryNetVol.Checked == true)
            {
                //  disable other boxes
                PrimaryNetVol.Enabled = false;
                SecondaryNetVol.Enabled = false;
            }
            else if (PrimarySecondaryNetVol.Checked == false)
            {
                PrimarySecondaryNetVol.Enabled = false;
                //  endable other boxes
                PrimaryNetVol.Enabled = true;
                SecondaryNetVol.Enabled = true;
            }   //  endif
        }
    }
}
