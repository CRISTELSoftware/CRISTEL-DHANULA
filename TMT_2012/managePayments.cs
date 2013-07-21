using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace TMT_2012
{
    /// <summary>
    /// 
    /// </summary>
    public partial class managePayments : Telerik.WinControls.UI.RadForm
    {
        public managePayments()
        {
            InitializeComponent();
        }

        private void managePayments_Load(object sender, EventArgs e)
        {
            grdFillCustomer.Visible = false;
            pnlChangeStatus.Visible = false;
            radCashPayment.PerformClick();
        }

        private void radCashPayment_Click(object sender, EventArgs e)
        {
            fillCashPaymentGrid();
        }

        // Cash Payments
        private void fillCashPaymentGrid()
        {
            string q1 = "select C.customername AS Customer_Name,C.customerno AS Customer_No,P.paymentno AS Payment_No,P.amount AS Amount,P.paymentdate AS Date,P.paymentsnote AS Note from payments P,customer C where P.customer=C.customerno and  P.paymentno NOT IN( select C.payments from cheque C)and  P.paymentno NOT IN( select CR.paymentNo from creditcard CR)";
            DataSet ds_add_customer_tyre = middle_access.db_access.SelectData(q1);
            if (ds_add_customer_tyre != null)
            {                
                grdPayments.DataSource = ds_add_customer_tyre.Tables[0].DefaultView;
            }
        }

        private void radChequePayment_CheckedChanged(object sender, EventArgs e)
        {
            grdPayments.DataSource = null;
            fillChequePaymemt();
        }

        // Normal Cheque Payments
        private void fillChequePaymemt()
        {
            string q1 = "select C.customername AS Customer_Name,C.customerno AS Customer_No,P.paymentno AS Payment_No,P.amount AS Amount,P.paymentdate AS Date,P.paymentsnote AS Note,CH.chequeno AS Cheque_No,CH.givendate AS Date_Given,CH.duedate AS Date_Due,CH.bank AS Bank,CH.status AS Status,CH.note AS Note from payments P,customer C,cheque CH where P.customer=C.customerno and P.paymentno=CH.payments and CH.isCashCheque ='0'";
            DataSet ds_add_customer_tyre = middle_access.db_access.SelectData(q1);
            if (ds_add_customer_tyre != null)
            {
                grdPayments.DataSource = ds_add_customer_tyre.Tables[0].DefaultView;
            }
        }

        private void radCashPayment_CheckedChanged(object sender, EventArgs e)
        {
            grdPayments.DataSource = null;
            fillCashPaymentGrid();
        }

        private void radCashChqPayments_CheckedChanged(object sender, EventArgs e)
        {
            grdPayments.DataSource = null;
            fillCashChequeGrid();

        }


        // Cash Cheque Payments
        private void fillCashChequeGrid()
        {
            string q1 = "select C.customername AS Customer_Name,C.customerno AS Customer_No,P.paymentno AS Payment_No,P.amount AS Amount,P.paymentdate AS Date,P.paymentsnote AS Payment_Note,CH.chequeno AS Cheque_No,CH.givendate AS Date_Given,CH.duedate AS Date_Due,CH.bank AS Bamk,CH.status AS Status,CH.note AS Note from payments P,customer C,cheque CH where P.customer=C.customerno and P.paymentno=CH.payments and CH.isCashCheque ='1'";
            DataSet ds_add_customer_tyre = middle_access.db_access.SelectData(q1);
            if (ds_add_customer_tyre != null)
            {
                grdPayments.DataSource = ds_add_customer_tyre.Tables[0].DefaultView;
            }           
            //setColors();           
        }

       
        


        private void btnCloseStatus_Click(object sender, EventArgs e)
        {
            grdPayments.Enabled = true;
            lblChequeNo.Text = "";
            lblBank.Text = "";
            lblChequeNo.Text = "";
            lblBank.Text = "";
            lblDueDate.Text = "";
            lblCustomerName.Text = "";
            lblAmount.Text = "";
            lblNote.Text = "";
            pnlChangeStatus.Visible = false;
        }

        private void grdPayments_CellDoubleClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            //managePayments mp = new managePayments();
            if (radCashChqPayments.Checked == true || radChequePayment.Checked == true)
            {
               // grdPayments.Enabled = false;
                pnlChangeStatus.Visible = true;
                string chequeNo = grdPayments.Rows[e.RowIndex].Cells[6].Value.ToString();
                string bank = grdPayments.Rows[e.RowIndex].Cells[9].Value.ToString();
                string dueDate = grdPayments.Rows[e.RowIndex].Cells[8].Value.ToString();
                string name = grdPayments.Rows[e.RowIndex].Cells[0].Value.ToString();
                string amount = grdPayments.Rows[e.RowIndex].Cells[3].Value.ToString();
                string paymentNote = grdPayments.Rows[e.RowIndex].Cells[11].Value.ToString();
                string curentStatus = grdPayments.Rows[e.RowIndex].Cells[10].Value.ToString();

                lblChequeNo.Text = chequeNo;
                lblBank.Text = bank;
                lblDueDate.Text = dueDate;
                lblCustomerName.Text = name;
                lblAmount.Text = amount;
                lblNote.Text = paymentNote;
                lblCurrentStatus.Text =curentStatus;
               
            }

            else
            {
                //pnlChangeStatus.Visible = false;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you Sure you want to set Status as OK ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                string chequeNo = lblChequeNo.Text; // local variable in btn click

                string q1 = "UPDATE cheque SET status = 'OK'  WHERE chequeno = '" + chequeNo + "'";
                bool status = middle_access.db_access.UpdateData(q1);

                if (status == true)
                {
                    MessageBox.Show("Status Changed Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (radChequePayment.Checked == true)
                    {
                        fillChequePaymemt();
                    }
                    else if (radCashChqPayments.Checked == true)
                    {
                        fillCashChequeGrid();
                    }

                    pnlChangeStatus.Visible= false;//set  visible false the panel
                    grdPayments.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Status Changed error", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

            }
            
            }

        private void btnReturned_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you Sure you want to set Status as Returned ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                string chequeNo = lblChequeNo.Text; // local variable in btn click

                string q1 = "UPDATE cheque SET status = 'Returened'  WHERE chequeno = '" + chequeNo + "'";
                bool status = middle_access.db_access.UpdateData(q1);

                if (status == true)
                {
                    MessageBox.Show("Status Changed Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (radChequePayment.Checked == true)
                    {
                        fillChequePaymemt();
                    }
                    else if (radCashChqPayments.Checked == true)
                    {
                        fillCashChequeGrid();
                    }
                    pnlChangeStatus.Visible = false;//set  visible false the panel
                    grdPayments.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Status Changed error", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

            }

        }

        private void btnPending_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you Sure you want to set Status as Pending ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                string chequeNo = lblChequeNo.Text; // local variable in btn click

                string q1 = "UPDATE cheque SET status = 'Pending'  WHERE chequeno = '" + chequeNo + "'";
                bool status = middle_access.db_access.UpdateData(q1);

                if (status == true)
                {
                    MessageBox.Show("Status Changed Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (radChequePayment.Checked== true)
                    {
                        fillChequePaymemt();
                    }
                    else if (radCashChqPayments.Checked == true)
                    {
                        fillCashChequeGrid();        
                    }
                    pnlChangeStatus.Visible = false;//set  visible false the panel
                    grdPayments.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Status Changed error", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

            }
        }

        private void grdPayments_RowFormatting(object sender, Telerik.WinControls.UI.RowFormattingEventArgs e)
        {
            
        }

        private void grdPayments_BackColorChanged(object sender, EventArgs e)
        {
           
        }

        private void radCreditcardPayments_CheckedChanged(object sender, EventArgs e)
        {
            grdPayments.DataSource = null;
            fillGridCreditCard();

        }

        private void fillGridCreditCard()
        {
            string q1 = "select Cc.customername AS Customer_Name,Cc.customerno AS Customer_No,C.reciptNo AS Recipt_No,C.paymentNo AS Payment_No,C.cardNo AS Card_No,C.cardType AS Card_Type,P.amount AS Amount from creditcard C,payments P, Customer Cc where P.paymentno = C.paymentNo AND P.customer =Cc.customerno";
            DataSet ds_Creditcard_Payments = middle_access.db_access.SelectData(q1);
            if (ds_Creditcard_Payments!=null)
            {
            grdPayments.DataSource = ds_Creditcard_Payments.Tables[0].DefaultView; 
            }
            else{
            }
    
        }

        private void grdPayments_CurrentRowChanged(object sender, Telerik.WinControls.UI.CurrentRowChangedEventArgs e)
        {

        }

        private void grdPayments_EditorRequired(object sender, Telerik.WinControls.UI.EditorRequiredEventArgs e)
        {
            

        }

        private void radButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radButton1_Click(object sender, EventArgs e)
        {

        }

        private void radButton4_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["AddPayments"] == null)
            {
                AddPayments a = new AddPayments();
                a.MdiParent = DHNAULA.ActiveForm;
                a.Size = new System.Drawing.Size(1100, 670);
                a.Location = new System.Drawing.Point(5, 5);
                a.Show();
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = txtSearchCustomer.Text;

            // Cash Payments
            if (radCashPayment.Checked == true)
            {
                grdPayments.DataSource = null;
                string q1 = "select C.customername AS Customer_Name,C.customerno AS Customer_No,P.paymentno AS Payment_No,P.amount AS Amount,P.paymentdate AS Date,P.paymentsnote AS Note from payments P,customer C where P.customer=C.customerno and C.customername ='" + name + "' and  P.paymentno  NOT IN( select C.payments from cheque C)and  P.paymentno  NOT IN( select CR.paymentNo from creditcard CR)";
                DataSet ds_add_customer_tyre = middle_access.db_access.SelectData(q1);
                if (ds_add_customer_tyre != null)
                {
                    grdPayments.DataSource = ds_add_customer_tyre.Tables[0].DefaultView;
                }
                else
                {
                    MessageBox.Show("Selected Customer does not have Cash Payments");
                }
            }

            // Cheque Payments
            else if (radChequePayment.Checked == true)
            {
                grdPayments.DataSource = null;

                grdPayments.DataSource = null;
                string q1 = "select C.customername AS Customer_Name,C.customerno AS Customer_No,P.paymentno AS Payment_No,P.amount AS Amount,P.paymentdate AS Date,P.paymentsnote AS Note,CH.chequeno AS Cheque_No,CH.givendate AS Date_Given,CH.duedate AS Date_Due,CH.bank AS Bank,CH.status AS Status,CH.note AS Note from payments P,customer C,cheque CH where P.customer=C.customerno and P.paymentno=CH.payments and CH.isCashCheque ='0' and C.customername = '" + name + "';";
                DataSet ds_add_customer_tyre = middle_access.db_access.SelectData(q1);
                if (ds_add_customer_tyre != null)
                {
                    grdPayments.DataSource = ds_add_customer_tyre.Tables[0].DefaultView;
                }
                else
                {
                    MessageBox.Show("Selected Customer does not have Cheque Payments");
                }
            }

                //Cash Cheque Payments
            else if (radCashChqPayments.Checked == true)
            {
                grdPayments.DataSource = null;

                grdPayments.DataSource = null;
                string q1 = "select C.customername AS Customer_Name,C.customerno AS Customer_No,P.paymentno AS Payment_No,P.amount AS Amount,P.paymentdate AS Date,P.paymentsnote AS Payment_Note,CH.chequeno AS Cheque_No,CH.givendate AS Date_Given,CH.duedate AS Date_Due,CH.bank AS Bamk,CH.status AS Status,CH.note AS Note from payments P,customer C,cheque CH where P.customer=C.customerno and P.paymentno=CH.payments and CH.isCashCheque ='1' and C.customername = '" + name + "';";
                DataSet ds_add_customer_tyre = middle_access.db_access.SelectData(q1);
                if (ds_add_customer_tyre != null)
                {
                    grdPayments.DataSource = ds_add_customer_tyre.Tables[0].DefaultView;
                }
                else
                {
                    MessageBox.Show("Selected Customer does not have Cash Cheque Payments");
                }
            }
            //Credit Card Payments
            else if (radCreditcardPayments.Checked == true)
            {
                grdPayments.DataSource = null;

                grdPayments.DataSource = null;
                string q1 = "select Cc.customername AS Customer_Name,Cc.customerno AS Customer_No,C.reciptNo AS Recipt_No,C.paymentNo AS Payment_No,C.cardNo AS Card_No,C.cardType AS Card_Type,P.amount AS Amount from creditcard C,payments P, Customer Cc where P.paymentno = C.paymentNo and Cc.customername = '" + name + "' AND P.customer =Cc.customerno ;";
                DataSet ds_add_customer_tyre = middle_access.db_access.SelectData(q1);
                if (ds_add_customer_tyre != null)
                {
                    grdPayments.DataSource = ds_add_customer_tyre.Tables[0].DefaultView;
                }
                else
                {
                    MessageBox.Show("Selected Customer does not have Creditcard Payments");
                }
            }
        }

        private void txtSearchCustomer_TextChanged(object sender, EventArgs e)
        {
            string name = txtSearchCustomer.Text;
            filter_customer_name(name);
        }

        private void filter_customer_name(string name)
        {
            string q1 = "SELECT customername FROM customer WHERE customername  LIKE  '" + name + "%' ";
            DataSet ds_name = middle_access.db_access.SelectData(q1); // fill data set with country which stoed in make table 
            if (ds_name != null) // if data set is not null
            {

                grdFillCustomer.Visible = false;
                grdFillCustomer.Visible = true;
                grdFillCustomer.DataSource = ds_name.Tables[0];
            }


            else
            {
                grdFillCustomer.DataSource = null;
                grdFillCustomer.Visible = false;

            }
        }

        private void grdFillCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtSearchCustomer.Text = grdFillCustomer.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtSearchCustomer.Focus();
            grdFillCustomer.Visible = false;
            string q = "SELECT * FROM customer WHERE customername='" + txtSearchCustomer.Text + "'";
            filter_customer(q);
        }

        private void filter_customer(string q)
        {
            DataSet ds_customer = middle_access.db_access.SelectData(q);
            grdFillCustomer.DataSource = ds_customer.Tables[0];
        }
   

       
        }
    }

