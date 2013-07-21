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
    public partial class AddPayments : Telerik.WinControls.UI.RadForm
    {
        //to get values from invoice
        private string customerID;
        private string customerName;



        string paymentNumber;
        string isCashCheque;
        string isCreditCardPayment;
        string invoiceNumber; // used for the cus no auto generation purpose
        DataSet ds_name;
        DataSet ds_ID;


        /// <summary>
        /// Initializes a new instance of the <see cref="AddPayments"/> class.
        /// </summary>
        /// <param name="customerID">The customer ID.</param>
        /// <param name="customerName">Name of the customer.</param>
        public AddPayments(string customerID, string customerName)
        {
            InitializeComponent();
            this.customerID = customerID;
            this.customerName = customerName;
            this.grdFillCustomer.Visible = false;

        }


        /// <summary>
        /// Initializes a new instance of the <see cref="AddPayments"/> class.
        /// </summary>
        public AddPayments()
        {
            InitializeComponent();
        }

        private void btnCancal_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddPayments_Load(object sender, EventArgs e)
        {
            loadCustomrCombo();
            txtCusName.Focus();
            loadInvoiceNo();
            radCashPayment.PerformClick();
            grpCheque.Visible = false;
            grpCreditCard.Visible = false;
            pbCash.Visible = true;
            pbCheque.Visible = false;
            pbCreditCard.Visible = false;
            if (GlobleAccess.openType == "I")
            {
                txtCusName.Text = GlobleAccess.customerName;
                txtCusNo.Text = GlobleAccess.customerNo;
                txtAmount.Text = GlobleAccess.amount;
                cmb_InvoiceNo.Text = GlobleAccess.invoiceNo;
                string q = "SELECT * FROM customer WHERE customername='" + txtCusName.Text + "'";
                filter_customer_ID(q);
                setAmount();
                //insertAccountDetaila();
            }


            //   grdFillCustomer.Visible = false;



            ////to fill data from invoice form
            //  txtCusName.Text = customerID;
            // txtCusNo.Text = customerName;

            grdFillCustomer.Visible = false;


            addbutten.Visible = true;
            this.Height = 385;
            grpCreditCard.Visible = false;
            pbCash.Visible = true;
            pbCreditCard.Visible = false;

            fillGridInformation();


        }

        private void radChequePayment_Click(object sender, EventArgs e)
        {
            this.Height = 670;

            lblTA.Visible = true;
            lblTotalAmount.Visible = true;
            //lblTotalAmount.Text = txtAmount.Text;
            //txtAmount.Text = "";


            grpCheque.Visible = true;
            grpCreditCard.Visible = false;
            chkCreditCardPayment.Visible = false;


            txtChequeNo.Text = "";
            txtNote.Text = "";
            cmbBank.Text = "";
            dtpPaymentDate.ResetText();
            dtpIssudDate.ResetText();
            dtpChequeDate.ResetText();
            chkIsCashCheque.Checked = false;
            grdFillCustomer.Visible = false;

        }

        private void radCashPayment_Click(object sender, EventArgs e)
        {
            if (txtAmount.Text == "" || txtAmount.Text == "Total Amount (Rs)")
            {

            }
            else
            {
                if (GlobleAccess.openType == "I") { }
                else
                    txtAmount.Text = lblTotalAmount.Text;
                //   lblTotalAmount.Visible = false;
                //   lblTotalAmount.Visible = false;
            }
            grpCheque.Visible = false;
            grpCreditCard.Visible = false;
            chkCreditCardPayment.Visible = true;
            Clear();

            addbutten.Visible = true;
            this.Height = 385;
            grpCreditCard.Visible = false;
            pbCash.Visible = true;
            pbCreditCard.Visible = false;
            chkCreditCardPayment.Checked = false;

        }


        private void btnAddPayment_Click(object sender, EventArgs e)
        {
            GlobleAccess.invoiceNo = cmb_InvoiceNo.Text;
            if (txtPaymentNo.Text == "")
            {
                MessageBox.Show("Please Check Payment No !", "Message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            else if (cmb_InvoiceNo.Text == "")
            {
                MessageBox.Show("Please select a invoice !", "Message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else if (txtCusName.Text == "" || txtCusNo.Text == "")
            {
                MessageBox.Show("Please select customer !", "Message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else if (txtAmount.Text.Trim() == "")
            {
                MessageBox.Show("Please add the payment amount!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else if (radCashPayment.Checked == true)
            {


                string paymentNo = txtPaymentNo.Text.ToString();
                string cusNo = txtCusNo.Text.ToString();
                string customerName = txtCusName.Text.ToString();
                string date = dtpPaymentDate.Value.Date.ToString("yyyy-MM-dd") + " " + DateTime.Now.TimeOfDay.Hours.ToString() + ":" + DateTime.Now.TimeOfDay.Minutes.ToString() + ":" + DateTime.Now.TimeOfDay.Seconds.ToString();
                string payAmount = txtAmount.Text.ToString();

                string q1 = "INSERT INTO payments(paymentno, customer, amount,paymentdate,invoiceno) VALUES('" + paymentNo + "','" + cusNo + "', '" + payAmount + "','" + date + "','" + cmb_InvoiceNo.Text + "')";
                bool status1 = middle_access.db_access.InsertData(q1);
                bool status2 = true;



                if (chkCreditCardPayment.Checked)
                {
                    if (txtCardNo.Text == "")
                    {
                        MessageBox.Show("Please enter credit card no !", "Message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }

                    else if (txtReciptNo.Text == "")
                    {
                        MessageBox.Show("Please enter recipt no !", "Message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }

                    else if (cmdCardType.Text.ToString() == "")
                        MessageBox.Show("Please select credit card type !", "Message", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                    else
                    {
                        // isCreditCardPayment = "1";
                        string receiptNo = txtReciptNo.Text.ToString();
                        string creditCardNo = txtCardNo.Text.ToString();
                        string cardType = cmdCardType.SelectedItem.ToString();
                        string creditNote = txtCreditNote.Text.ToString();
                        string expireDate = dtpExpDate.Value.Date.ToString("yyyy-MM-dd" + " " + DateTime.Now.TimeOfDay.Hours.ToString() + ":" + DateTime.Now.TimeOfDay.Minutes.ToString() + ":" + DateTime.Now.TimeOfDay.Seconds.ToString());

                        string q2 = "INSERT INTO creditcard (reciptNo, paymentNo, cardNo, cardType,expireDate,note) VALUES ('" + receiptNo + "', '" + paymentNo + "', '" + creditCardNo + "', '" + cardType + "', '" + expireDate + "','" + creditNote + "')";
                        status2 = middle_access.db_access.InsertData(q2);

                    }

                }
                else
                {
                    //isCreditCardPayment = "0";
                }



                if (status1 == true && status2 == true) // if data is insert
                {
                    insertAccountDetaila();
                    MessageBox.Show("Cash Payment successfully added!");
                    Clear();
                    autoIncrement();

                    if (GlobleAccess.openType != "I")
                    {
                        Insert_Data_To_temp_invoice_print();
                    }

                    CurrentInvoicePrint ci = new CurrentInvoicePrint();
                    ci.Show();
                }
                else if (status1 == true && status2 == false) // if data is insert
                {
                    MessageBox.Show("Incorrect credit card transaction!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    string q11 = "DELETE FROM payments WHERE paymentNo='" + paymentNo + "'";
                    middle_access.db_access.DeleteData(q11);
                }

                else if (status1 == false && status2 == true) // if data is insert
                {
                    MessageBox.Show("Error : Payment not added !", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    string q12 = "DELETE FROM creditcard WHERE reciptNo='" + txtReciptNo.Text + "'";
                    middle_access.db_access.DeleteData(q12);
                }
                else
                {
                    MessageBox.Show("Error : Payment not added !", "Message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                /////////////////////////////

            }

            else if (radChequePayment.Checked == true)
            {


                if (txtChequeNo.Text == "")
                {
                    MessageBox.Show("Add Cheque No !", "Message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else if (cmbBank.Text == "")
                {

                    MessageBox.Show("Select a Bank !", "Message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

                else
                {

                    if (chkIsCashCheque.Checked)
                    {
                        isCashCheque = "1";
                    }
                    else
                    {
                        isCashCheque = "0";
                    }

                    string paymentNo = txtPaymentNo.Text.ToString();
                    string cusNo = txtCusNo.Text.ToString();
                    string customerName = txtCusName.Text.ToString();
                    string date = dtpPaymentDate.Value.Date.ToString("yyyy-MM-dd" + " " + DateTime.Now.TimeOfDay.Hours.ToString() + ":" + DateTime.Now.TimeOfDay.Minutes.ToString() + ":" + DateTime.Now.TimeOfDay.Seconds.ToString());
                    string dueDate = dtpChequeDate.Value.Date.ToString("yyyy-MM-dd" + " " + DateTime.Now.TimeOfDay.Hours.ToString() + ":" + DateTime.Now.TimeOfDay.Minutes.ToString() + ":" + DateTime.Now.TimeOfDay.Seconds.ToString());
                    string payAmount = txtAmount.Text.ToString();
                    string chequeNo = txtChequeNo.Text.ToString();
                    string bank = cmbBank.Text.ToString();
                    string note = txtNote.Text.ToString();

                    string q1 = "INSERT INTO payments(paymentno, customer, amount,paymentdate,invoiceno) VALUES('" + paymentNo + "','" + cusNo + "', '" + payAmount + "','" + date + "','" + cmb_InvoiceNo.Text + "')";
                    bool status1 = middle_access.db_access.InsertData(q1);


                    //check dates what are isuue date etc...
                    string q2 = "INSERT INTO cheque (payments,chequeno, note, givendate, bank, status, duedate,isCashCheque) VALUES ('" + paymentNo + "', '" + chequeNo + "', '" + note + "','" + date + "' , '" + bank + "', 'P', '" + dueDate + "','" + isCashCheque + "')";
                    bool status2 = middle_access.db_access.InsertData(q2);

                    if (status1 == true && status2 == true) // if data is insert
                    {
                        insertAccountDetaila();
                        MessageBox.Show("Check Payment successfully added!");
                        Clear();
                        autoIncrement();

                        if (GlobleAccess.openType != "I")
                        {
                            Insert_Data_To_temp_invoice_print();
                        }

                        CurrentInvoicePrint ci = new CurrentInvoicePrint();
                        ci.Show();
                    }
                    else if (status1 == true && status2 == false) // if data is insert
                    {
                        MessageBox.Show("Cheque number is alredy exsists!");
                        string q11 = "DELETE FROM payments WHERE paymentNo='" + paymentNo + "'";
                        middle_access.db_access.DeleteData(q11);
                    }
                    else if (status1 == false && status2 == true) // if data is insert
                    {
                        MessageBox.Show("Error : Payment not added !", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        string q12 = "DELETE FROM cheque WHERE chequeno='" + txtChequeNo.Text + "'";
                        middle_access.db_access.DeleteData(q12);
                    }
                    else
                    {
                        MessageBox.Show("Error : Payment not added !", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    /////////////////////////////

                }
            }
            setAmount();
            fillGridInformation();



        }

        private void autoIncrement()
        {
            int nextInvNo = (Convert.ToInt32(invoiceNumber)) + 1;
            string q = "UPDATE autoincrem SET maxno = " + nextInvNo + "  WHERE tablename = 'P'";
            bool status = middle_access.db_access.UpdateData(q);
            loadInvoiceNo();
        }



        public void Clear()
        {
            // txtCusNo.Text = "";
            // txtCusName.Text = "";
            txtAmount.Text = "";
            txtChequeNo.Text = "";
            txtNote.Text = "";
            //// txtPaymentNo.Text = "";
            cmbBank.Text = "";
            dtpPaymentDate.ResetText();
            dtpIssudDate.ResetText();
            dtpChequeDate.ResetText();
            chkIsCashCheque.Checked = false;
            grdFillCustomer.Visible = false;

        }




        private void loadInvoiceNo()
        {
            string q1 = "SELECT maxno FROM autoincrem where tablename='P'";
            DataSet ds_InviceNo = middle_access.db_access.SelectData(q1); // fill data set with country which stoed in make table           

            if (ds_InviceNo != null) // if data set is not null
            {
                invoiceNumber = ds_InviceNo.Tables[0].Rows[0][0].ToString();
                txtPaymentNo.Text = "P " + invoiceNumber + "";


            }
            else
                txtPaymentNo.Text = null; //fill table 
        }



        private void radTextBox2_Click(object sender, EventArgs e)
        {
            //txtCustomerName.Text = "";
        }


        //to view the credit card panel
        private void chkCreditCardPayment_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            pbCheque.Visible = false;
            pbCash.Visible = false;
            pbCreditCard.Visible = true;

            if (chkCreditCardPayment.Checked == true)
            {
                addbutten.Visible = false;
                this.Height = 670;
                grpCreditCard.Visible = true;

            }

            else
            {
                addbutten.Visible = true;
                this.Height = 385;
                grpCreditCard.Visible = false;
                pbCash.Visible = true;
                pbCreditCard.Visible = false;
            }
        }

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {
            bool check;
            float a;
            check = float.TryParse(txtAmount.Text, out a);

            if (txtAmount.Text == "")
            {
                // MessageBox.Show("Please Enater a Payment Amount!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            if (!check)
            {
                // MessageBox.Show("you can type Numaric values only !", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtAmount.Text = " ";
            }


        }

        private void txtAmount_TextChanging(object sender, TextChangingEventArgs e)
        {

        }

        private void radCashPayment_CheckedChanged(object sender, EventArgs e)
        {
            pbCreditCard.Visible = false;
            pbCheque.Visible = false;
            pbCash.Visible = true;
            chkCreditCardPayment.Checked = false;
            addbutten.Visible = true;
        }

        private void radChequePayment_CheckedChanged(object sender, EventArgs e)
        {
            addbutten.Visible = false;
            if (chkCreditCardPayment.Checked == true)
            {
                this.Height = 610;
            }
            else
            {
                this.Height = 385;
            }
            pbCreditCard.Visible = false;
            pbCash.Visible = false;
            pbCheque.Visible = true;
        }


        private void txtCusName_TextChanged(object sender, EventArgs e)
        {
            string name = txtCusName.Text;
            if (name != null)
            {
                filter_customer_name(name);
            }
            else
            {
                grdFillCustomer.Visible = false;
            }
        }

        private void filter_customer_name(string name)
        {
            string q1 = "SELECT customername FROM customer WHERE customername  LIKE  '" + name + "%'";
            ds_name = middle_access.db_access.SelectData(q1); // fill data set with country which stoed in make table 
            if (ds_name != null) // if data set is not null
            {
                grdFillCustomer.Visible = false;
                grdFillCustomer.Visible = true;
                grdFillCustomer.DataSource = ds_name.Tables[0].DefaultView;
                testCMB.DisplayMember = "customername";
                testCMB.DataSource = ds_name.Tables[0].DefaultView;
            }
            else
            {
                grdFillCustomer.DataSource = null;
                grdFillCustomer.Visible = false;
            }
        }

        private void grdFillCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCusName.Text = grdFillCustomer.Rows[e.RowIndex].Cells[0].Value.ToString();
            //txtCusName.Focus();
            grdFillCustomer.Visible = false;
            //string q = "SELECT * FROM addpaymentsaccount WHERE customername='" + txtCusName.Text + "'";  
            string q = "SELECT * FROM customer WHERE customername='" + txtCusName.Text + "'";
            filter_customer_ID(q);
            setAmount();
            fillInvoiceCombo();
            fillGridInformation();

            //  lblTA.Visible = true;
            //  lblTotalAmount.Visible = true;
            // lblTotalAmount.Text = txtAmount.Text;             
        }

        private void fillInvoiceCombo()
        {

            //string q = "SELECT * FROM view_1 WHERE credit <>0 AND customerNo='"+ txtCusNo.Text +"'";// AND SUM(A.enteredAmount)>0.00
            string q = "SELECT invoiceno FROM invoice WHERE customer='" + txtCusNo.Text + "'";// AND SUM(A.enteredAmount)>0.00
            DataSet ds_customer_id = middle_access.db_access.SelectData(q);
            if (ds_customer_id != null)
            {
                cmb_InvoiceNo.DataSource = ds_customer_id.Tables[0];
                cmb_InvoiceNo.DisplayMember = "invoiceno";
                cmb_InvoiceNo.ValueMember = "invoiceno";
                //  cmb_InvoiceNo.Text = "";           
            }
            else
            {
                cmb_InvoiceNo.DataSource = null;
            }
        }

        private void fillGridInformation()
        {
            //txtAmount.Clear();
            string q1 = "";
            if (cmb_InvoiceNo.Text == "")
            {
                q1 = "select c.chequeno AS Cheque_No,c.givendate AS Given_Date,c.duedate AS Due_Date,c.bank AS Back,p.amount AS Amount from cheque c,customer Cc,payments p where  Cc.customerno = '" + txtCusNo.Text + "' and  Cc.customerno = p.customer and p.paymentno = c.payments and c.`status`= 'P'";

            }
            else
            {
                q1 = "select c.chequeno AS Cheque_No,c.givendate AS Given_Date,c.duedate AS Due_Date,c.bank AS Back,p.amount AS Amount from cheque c,customer Cc,payments p where  Cc.customerno = '" + txtCusNo.Text + "' and p.invoiceno='" + cmb_InvoiceNo.Text + "' and  Cc.customerno = p.customer and p.paymentno = c.payments and c.`status`= 'P'";
            }
            DataSet ds_Details = middle_access.db_access.SelectData(q1);
            if (ds_Details != null)
                grdCusPaymentHistory.DataSource = ds_Details.Tables[0].DefaultView;
            else
                grdCusPaymentHistory.DataSource = null;

            double ChequeTotal = 0.00;
            double totCreditAmt = 0.00;
            double extra = 0.00;
            string amt = "0";

            for (int i = 0; i < grdCusPaymentHistory.RowCount; i++)
            {
                string amount = grdCusPaymentHistory.Rows[i].Cells[4].Value.ToString();
                if (amount == "" || amount == null)
                {
                    amount = "0";
                }
                double amountValue = Convert.ToDouble(amount);

                ChequeTotal = amountValue + ChequeTotal;
            }

            lblChequeTotal.Text = ChequeTotal.ToString("0.00");

            if (radCashPayment.Checked == true)
            {
                amt = txtAmount.Text.Trim();
            }
            else if (radChequePayment.Checked == true)
            {
                amt = lblTotalAmount.Text;
            }
            if (amt != "")
            {
                if (amt == "Total Amount (Rs)")
                {
                    totCreditAmt = 0.00;
                }
                else
                {
                    totCreditAmt = Convert.ToDouble(amt);
                }
            }


            extra = (ChequeTotal - totCreditAmt);
            try
            {
                if (extra > 0)
                {
                    lblExtraAmount.Text = extra.ToString("0.00");
                }
                else
                {
                    lblExtraAmount.Text = "No Extra Payaments";
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void filter_customer_ID(string q)
        {
            string q1 = q;
            ds_ID = middle_access.db_access.SelectData(q1); // fill data set with country which stoed in make table 
            if (ds_ID != null) // if data set is not null
            {
                txtCusNo.Text = ds_ID.Tables[0].Rows[0][0].ToString();
                lblCreditLimit.Text = ds_ID.Tables[0].Rows[0][4].ToString() + ".00";
            }
            else
            {
                //com_customer_id.DataSource = null;
            }
        }

        private void setAmount()
        {
            double invoiceCount = 0.00;
            double paymentCount = 0.00;
            string q = "SELECT SUM(invoicetotal) FROM invoice WHERE customer='" + txtCusNo.Text + "' AND invoiceno='" + cmb_InvoiceNo.Text + "'";
            DataSet ds = middle_access.db_access.SelectData(q);
            if (ds != null)
            {
                if (ds.Tables[0].Rows[0][0].ToString() == "")
                {
                    invoiceCount = 0.00;
                }
                else
                {
                    invoiceCount = Convert.ToDouble(ds.Tables[0].Rows[0][0].ToString());
                }

            }

            string invoiceNo = cmb_InvoiceNo.Text;
            if (invoiceNo == "")
                invoiceNo = "-";

            q = "SELECT SUM(enteredAmount) FROM addpaymentsaccount WHERE customerNo='" + txtCusNo.Text + "' AND  invoiceNo='" + invoiceNo + "' ";
            ds = middle_access.db_access.SelectData(q);
            if (ds != null)
            {
                //string a = ds.Tables[0].Rows[0][0].ToString();
                if (ds.Tables[0].Rows[0][0].ToString() == "")
                {
                    paymentCount = 0.00;
                }
                else
                {
                    paymentCount = Convert.ToDouble(ds.Tables[0].Rows[0][0].ToString());
                }

            }

            if (radChequePayment.Checked != true)
            {
                lblTA.Visible = true;
                lblTotalAmount.Visible = true;
                if (GlobleAccess.openType == "I")
                {
                    lblTotalAmount.Text = (invoiceCount - paymentCount).ToString("0.00");
                }
                else
                {
                    txtAmount.Text = (invoiceCount - paymentCount).ToString("0.00");
                    lblTotalAmount.Text = txtAmount.Text;
                }
            }

            else
            {
                lblTA.Visible = true;
                lblTotalAmount.Visible = true;

                if (GlobleAccess.openType == "I")
                {
                    lblTotalAmount.Text = (invoiceCount - paymentCount).ToString("0.00");
                }
                else
                {
                    txtAmount.Text = (invoiceCount - paymentCount).ToString("0.00");
                    lblTotalAmount.Text = txtAmount.Text;
                }
                //lblTotalAmount.Text = txtAmount.Text;
                // txtAmount.Text = "";
            }

            if (txtAmount.Text != "")
            {
                double x = Convert.ToDouble(txtAmount.Text);
                if (x == 0)
                {
                    addbutten.Enabled = false;
                    btnAddPayment.Enabled = false;
                }
                else
                {
                    addbutten.Enabled = true;
                    btnAddPayment.Enabled = true;
                }
            }
        }

        public void getCustomerTotalBalanceLeft()
        {
            string q = "SELECT * FROM customer WHERE customername='" + txtCusName.Text + "'";

        }

        private void txtCusName_TabIndexChanged(object sender, EventArgs e)
        {
            if (grdFillCustomer.Visible == true)
            {
                //grdFillCustomer.DataSource = null;
                grdFillCustomer.Visible = false;
            }

        }

        private void addbutten_Click(object sender, EventArgs e)
        {

        }

        private void insertAccountDetaila()
        {
            string paymentMethod = "";
            string dateTime = dtpPaymentDate.Value.Date.ToString("yyyy-MM-dd") + " " + DateTime.Now.TimeOfDay.Hours.ToString() + ":" + DateTime.Now.TimeOfDay.Minutes.ToString() + ":" + DateTime.Now.TimeOfDay.Seconds.ToString(); ;

            if (radCashPayment.Checked == true)
            {
                if (chkCreditCardPayment.Checked == true)
                    paymentMethod = "Credit Card Paynemt/" + cmdCardType.Text + "/" + dateTime;
                else
                    paymentMethod = "Cacsh Paynemt/" + dateTime;
            }
            else if (radChequePayment.Checked == true)
            {
                if (chkIsCashCheque.Checked == true)
                    paymentMethod = "Cheque Paynemt(Cash)/" + txtChequeNo.Text + "-" + cmbBank.Text + "/" + dateTime;
                else
                    paymentMethod = "Cheque Paynemt/" + txtChequeNo.Text + "-" + cmbBank.Text + "/" + dateTime;
            }

            if (GlobleAccess.amount == "")
            {
                GlobleAccess.amount = lblTotalAmount.Text;
            }

            if (GlobleAccess.openType == "I")
            {
                string date = dtpPaymentDate.Value.Date.ToString("yyyy-MM-dd");
                string q = "INSERT INTO addpaymentsaccount (customerNo,paymentNo, invoiceNo, customerName, paymentDate, invoiceAmount, enteredAmount,paymentMethodTxt) VALUES ('" + txtCusNo.Text + "', '" + txtPaymentNo.Text + "', '" + GlobleAccess.invoiceNo + "','" + txtCusName.Text + "','" + date + "' , " + GlobleAccess.amount + ", " + Convert.ToDecimal(txtAmount.Text) + ",'" + paymentMethod + "')";
                middle_access.db_access.InsertData(q);
            }
            else
            {
                string date = dtpPaymentDate.Value.Date.ToString("yyyy-MM-dd");
                string q = "INSERT INTO addpaymentsaccount (customerNo,paymentNo, invoiceNo, customerName, paymentDate, invoiceAmount, enteredAmount,paymentMethodTxt) VALUES ('" + txtCusNo.Text + "', '" + txtPaymentNo.Text + "', '" + cmb_InvoiceNo.Text + "','" + txtCusName.Text + "','" + date + "' , " + lblTotalAmount.Text + ", " + Convert.ToDecimal(txtAmount.Text) + ",'" + paymentMethod + "')";
                middle_access.db_access.InsertData(q);
            }
        }

        private void cancalbutten_Click(object sender, EventArgs e)
        {

        }

        private void radButton3_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["Invoice"] != null)
            {
                Form f = (Form)Application.OpenForms["Invoice"];
                f.Enabled = true;

            }

            this.Close();

        }

        private void radButton2_Click(object sender, EventArgs e)
        {


        }

        private void radButton4_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["AddPayments"] == null)
            {
                AddPayments a = new AddPayments();
                a.MdiParent = DHNAULA.ActiveForm;
                a.Size = new System.Drawing.Size(820, 400);
                a.Location = new System.Drawing.Point(330, 50);
                a.Show();
            }

        }

        private void radButton2_Click_1(object sender, EventArgs e)
        {

            if (Application.OpenForms["managePayments"] == null)
            {
                if (Application.OpenForms["Invoice"] == null)
                {
                    managePayments mp = new managePayments();
                    mp.MdiParent = DHNAULA.ActiveForm;
                    mp.Size = new System.Drawing.Size(1100, 670);
                    mp.Location = new System.Drawing.Point(5, 5);
                    mp.Show();
                    this.Close();
                }

            }

        }

        private void txtCusName_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            GlobleAccess.invoiceNo = cmb_InvoiceNo.Text;
            if (txtPaymentNo.Text == "")
            {
                MessageBox.Show("Please Check Payment No !", "Message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else if (cmb_InvoiceNo.Text == "")
            {
                MessageBox.Show("Please select a invoice !", "Message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else if (txtCusName.Text == "" || txtCusNo.Text == "")
            {
                MessageBox.Show("Please select customer !", "Message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else if (txtAmount.Text.Trim() == "")
            {
                MessageBox.Show("Please add the payment amount", "Message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else if (radCashPayment.Checked == true)
            {
                string paymentNo = txtPaymentNo.Text.ToString();
                string cusNo = txtCusNo.Text.ToString();
                string customerName = txtCusName.Text.ToString();
                string date = dtpPaymentDate.Value.Date.ToString("yyyy-MM-dd") + " " + DateTime.Now.TimeOfDay.Hours.ToString() + ":" + DateTime.Now.TimeOfDay.Minutes.ToString() + ":" + DateTime.Now.TimeOfDay.Seconds.ToString();

                string payAmount = txtAmount.Text.ToString();

                string q1 = "INSERT INTO payments(paymentno, customer, amount,paymentdate,invoiceno) VALUES('" + paymentNo + "','" + cusNo + "', '" + payAmount + "','" + date + "','" + cmb_InvoiceNo.Text + "')";
                bool status1 = middle_access.db_access.InsertData(q1);
                bool status2 = true;



                if (chkCreditCardPayment.Checked)
                {
                    if (txtCardNo.Text == "")
                    {
                        MessageBox.Show("Please enter credit card no !", "Message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }

                    else if (txtReciptNo.Text == "")
                    {
                        MessageBox.Show("Please enter recipt no !", "Message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }

                    else if (cmdCardType.Text.ToString() == "")
                        MessageBox.Show("Please select credit card type !", "Message", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                    else
                    {
                        // isCreditCardPayment = "1";
                        string receiptNo = txtReciptNo.Text.ToString();
                        string creditCardNo = txtCardNo.Text.ToString();
                        string cardType = cmdCardType.SelectedItem.ToString();
                        string creditNote = txtCreditNote.Text.ToString();
                        string expireDate = dtpExpDate.Value.Date.ToString("yyyy-MM-dd");

                        string q2 = "INSERT INTO creditcard (reciptNo, paymentNo, cardNo, cardType,expireDate,note) VALUES ('" + receiptNo + "', '" + paymentNo + "', '" + creditCardNo + "', '" + cardType + "', '" + expireDate + "','" + creditNote + "')";
                        status2 = middle_access.db_access.InsertData(q2);

                    }

                }
                else
                {
                    //isCreditCardPayment = "0";
                }



                if (status1 == true && status2 == true) // if data is insert
                {
                    //if (GlobleAccess.openType == "M")
                    //{
                    insertAccountDetaila();
                    //}
                    MessageBox.Show("Cash Payment successfully added !");
                    Clear();
                    autoIncrement();
                    if (GlobleAccess.openType != "I")
                    {
                        Insert_Data_To_temp_invoice_print();
                    }

                    // CurrentInvoicePrint.paymenCount = GlobleAccess.Get_Payments_Sum();
                    CurrentInvoicePrint ci = new CurrentInvoicePrint();
                    ci.Show();

                }

                else
                {
                    MessageBox.Show("Error : Payment not added !", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                /////////////////////////////

            }

            else if (radChequePayment.Checked == true)
            {


                if (txtChequeNo.Text == "")
                {
                    MessageBox.Show("Add Cheque No !", "Message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else if (cmbBank.Text == "")
                {

                    MessageBox.Show("Select a Bank !", "Message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

                else
                {

                    if (chkIsCashCheque.Checked)
                    {
                        isCashCheque = "1";
                    }
                    else
                    {
                        isCashCheque = "0";
                    }

                    string paymentNo = txtPaymentNo.Text.ToString();
                    string cusNo = txtCusNo.Text.ToString();
                    string customerName = txtCusName.Text.ToString();
                    string date = dtpPaymentDate.Value.Date.ToString("yyyy-MM-dd");
                    string payAmount = txtAmount.Text.ToString();
                    string chequeNo = txtChequeNo.Text.ToString();
                    string bank = cmbBank.Text.ToString();
                    string note = txtNote.Text.ToString();

                    string q1 = "INSERT INTO payments(paymentno, customer, amount,paymentdate,invoiceno) VALUES('" + paymentNo + "','" + cusNo + "', '" + payAmount + "','" + date + "','" + cmb_InvoiceNo.Text + "')";
                    bool status1 = middle_access.db_access.InsertData(q1);


                    //check dates what are isuue date etc...
                    string q2 = "INSERT INTO cheque (payments,chequeno, note, givendate, bank, status, duedate,isCashCheque) VALUES ('" + paymentNo + "', '" + chequeNo + "', '" + note + "','" + date + "' , '" + bank + "', 'P', '2012-03-04','" + isCashCheque + "')";
                    bool status2 = middle_access.db_access.InsertData(q2);

                    if (status1 == true && status2 == true) // if data is insert
                    {
                        // if (GlobleAccess.openType == "M")
                        // {
                        insertAccountDetaila();
                        // }
                        MessageBox.Show("Check Payment successfully added!");
                        Clear();
                        autoIncrement();

                        if (GlobleAccess.openType != "I")
                        {
                            Insert_Data_To_temp_invoice_print();
                        }

                        //  CurrentInvoicePrint.paymenCount = GlobleAccess.Get_Payments_Sum();
                        CurrentInvoicePrint ci = new CurrentInvoicePrint();
                        ci.Show();
                    }
                    else
                    {
                        MessageBox.Show("Error : Payment not added !", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    /////////////////////////////

                }
            }


            setAmount();
        }

        private void txtAmount_Leave(object sender, EventArgs e)
        {
            //if (!txtAmount.Text.Contains("."))
            //{
            //    txtAmount.Text = txtAmount.Text + ".00";
            //}
        }

        private void cmb_InvoiceNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            setAmount();
            fillGridInformation();
        }


        private void Insert_Data_To_temp_invoice_print()
        {
            string q = "SELECT I.invoiceno,I.invoicedate,C.customername,IL.itemno,IL.itemname,IL.soldqty,IL.retailprice,I.invoicetotal,IL.discount,I.invoicenote,I.vehicleno FROM invoice I,customer C,invoicelines IL WHERE C.customerno=I.customer AND I.invoiceno = IL.invoiceno AND I.invoiceno='" + cmb_InvoiceNo.Text + "'";
            DataSet ds = middle_access.db_access.SelectData(q);

            //string q1 = "SELECT * FROM invoicelines  WHERE invoiceno='"+ cmb_InvoiceNo.Text +"'";
            //DataSet ds1 = middle_access.db_access.SelectData(q1);

            //string q2 = "";
            if (ds != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string invoice_no = ds.Tables[0].Rows[i][0].ToString();
                    string invoice_date = ds.Tables[0].Rows[i][1].ToString();
                    string customer_name = ds.Tables[0].Rows[i][2].ToString();
                    string item_no = ds.Tables[0].Rows[i][3].ToString();
                    string item_name = ds.Tables[0].Rows[i][4].ToString();
                    string qty = ds.Tables[0].Rows[i][5].ToString();
                    string unit_price = ds.Tables[0].Rows[i][6].ToString() + ".00";
                    string price = (Convert.ToDouble(ds.Tables[0].Rows[i][5].ToString()) * Convert.ToDouble(ds.Tables[0].Rows[i][6].ToString())).ToString("0.00");
                    string total_price = ds.Tables[0].Rows[i][7].ToString() + ".00";
                    string _discount = ds.Tables[0].Rows[i][8].ToString();
                    string refNo = ds.Tables[0].Rows[i][9].ToString();
                    string vehicleno = ds.Tables[0].Rows[i][10].ToString();

                    if (_discount.Trim() == "")
                    {
                        _discount = "0.00";
                    }
                    double discount = Convert.ToDouble(_discount);

                    string discountedPrice = (((Convert.ToDouble(ds.Tables[0].Rows[i][6].ToString()) * Convert.ToDouble(ds.Tables[0].Rows[i][5].ToString())) / 100) * (100 - discount)).ToString("0.00");


                    string q3 = "";
                    if (discount == 0.00)
                    {
                        q3 = "INSERT INTO temp_invoice_print(invoice_no,invoice_date,customer_name,item_no,item_name,qty,unit_price,price,total_price,discount,refNo,vehicleno)VALUES('" + invoice_no + "','" + invoice_date + "','" + customer_name + "','" + item_no + "','" + item_name + "','" + qty + "','" + unit_price + "','" + discountedPrice + "','" + total_price + "',' ','" + refNo + "','" + vehicleno + "')";
                    }
                    else
                    {
                        q3 = "INSERT INTO temp_invoice_print(invoice_no,invoice_date,customer_name,item_no,item_name,qty,unit_price,price,total_price,discount,refNo,vehicleno)VALUES('" + invoice_no + "','" + invoice_date + "','" + customer_name + "','" + item_no + "','" + item_name + "'," + qty + ",'" + unit_price + "','" + discountedPrice + "','" + total_price + "','" + discount.ToString() + "%" + "','" + refNo + "','" + vehicleno + "')";
                    }
                    bool status3 = middle_access.db_access.InsertData(q3);
                    if (status3 != true)
                    {
                        MessageBox.Show("Error!");
                        string q4 = "DELETE FROM temp_invoice_print";
                        middle_access.db_access.DeleteData(q4);
                    }

                }
            }


        }

        private void Insert_Data_To_temp_invoice_Payment_print()
        {
            string q = "SELECT  FROM payments P,creditcard CR,cheque CH WHERE P.paymentno=CR.paymentNo AND P.paymentno = CH.payments AND P.invoiceno='" + cmb_InvoiceNo.Text + "'";
            DataSet ds = middle_access.db_access.SelectData(q);
        }

        private void radButton1_Click_1(object sender, EventArgs e)
        {
            if (cmb_InvoiceNo.Text == "")
            {
                MessageBox.Show("Please select an invoice number!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                /*Payments_Full_Report pr = new Payments_Full_Report();
                pr.MdiParent = DHNAULA.ActiveForm;
                pr.Show();*/
                bool status = false;
                string invoice_no = cmb_InvoiceNo.Text;
                GlobleAccess.invoiceNo = invoice_no;
                string q = "SELECT I.invoiceno,I.invoicedate,C.customername,IL.itemno,IL.itemname,IL.soldqty,IL.retailprice,IL.soldqty*IL.retailprice,I.invoicetotal,IL.discount,I.invoicenote,I.vehicleno FROM invoice I,invoicelines IL, customer C WHERE C.customerno = I.customer AND I.invoiceno = IL.invoiceno AND I.invoiceno = '" + invoice_no + "'";
                DataSet ds_invoice = middle_access.db_access.SelectData(q);
                if (ds_invoice != null)
                {
                    // int i = 0;
                    for (int i = 0; i < ds_invoice.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr_invoice = ds_invoice.Tables[0].Rows[i];

                        string invoice_date = dr_invoice.ItemArray.GetValue(1).ToString();
                        string customer_name = dr_invoice.ItemArray.GetValue(2).ToString();
                        string item_no = dr_invoice.ItemArray.GetValue(3).ToString();
                        string item_name = dr_invoice.ItemArray.GetValue(4).ToString();
                        string qty = dr_invoice.ItemArray.GetValue(5).ToString();
                        string unit_price = dr_invoice.ItemArray.GetValue(6).ToString();
                        string price = dr_invoice.ItemArray.GetValue(7).ToString();
                        string total_price = dr_invoice.ItemArray.GetValue(8).ToString();
                        string discount = dr_invoice.ItemArray.GetValue(9).ToString();
                        string refNo = dr_invoice.ItemArray.GetValue(10).ToString();
                        string vehicleNo = dr_invoice.ItemArray.GetValue(11).ToString();

                        if (discount == "")
                        {
                            discount = " ";
                            price = ((Convert.ToDouble(qty) * Convert.ToDouble(unit_price)) / 100 * (100 - 0)).ToString();
                        }
                        else if (discount != " ")
                        {
                            price = ((Convert.ToDouble(qty) * Convert.ToDouble(unit_price)) / 100 * (100 - Convert.ToDouble(discount))).ToString();
                            discount = discount + " %";

                        }

                        string q3 = "INSERT INTO temp_invoice_print(invoice_no,invoice_date,customer_name,item_no,item_name,qty,unit_price,price,total_price,discount,refNo,vehicleNo)VALUES('" + invoice_no + "','" + invoice_date + "','" + customer_name + "','" + item_no + "','" + item_name + "','" + qty + "','" + unit_price + ".00','" + price + ".00','" + total_price + ".00','" + discount + "','" + refNo + "','" + vehicleNo + "')";
                        status = middle_access.db_access.InsertData(q3);
                    }

                    if (status)
                    {
                        this.Enabled = false;
                        CurrentInvoicePrint ci = new CurrentInvoicePrint();
                        ci.MdiParent = DHNAULA.ActiveForm;
                        ci.Show();
                    }
                    else
                    {
                        MessageBox.Show("Invoice number error!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void grdFillCustomer_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void grdFillCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void grdFillCustomer_CellClick_2(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void grdFillCustomer_CellClick_3(object sender, DataGridViewCellEventArgs e)
        {
            txtCusName.Text = grdFillCustomer.Rows[e.RowIndex].Cells[0].Value.ToString();
            //txtCusName.Focus();
            grdFillCustomer.Visible = false;
            //string q = "SELECT * FROM addpaymentsaccount WHERE customername='" + txtCusName.Text + "'";  
            string q = "SELECT * FROM customer WHERE customername='" + txtCusName.Text + "'";
            filter_customer_ID(q);
            setAmount();
            fillInvoiceCombo();
            fillGridInformation();
        }

        private void AddPayments_FormClosing(object sender, FormClosingEventArgs e)
        {
            string q4 = "DELETE  FROM temp_invoice_print";
            middle_access.db_access.DeleteData(q4);
        }

        private void radButton4_Click_1(object sender, EventArgs e)
        {
            if (cmb_InvoiceNo.Text == "")
            {
                MessageBox.Show("Please select a customer!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                //string previous = "";
                string txt = "";
                string invoice_no = "";
                string invoice_date = "";
                bool status = false;
                //GlobleAccess.invoiceNo = invoice_no;
                string q = "SELECT I.invoiceno,I.invoicedate,C.customername,IL.itemno,IL.itemname,IL.soldqty,IL.retailprice,IL.soldqty*IL.retailprice,I.invoicetotal,IL.discount,I.invoicenote,I.vehicleno FROM invoice I,invoicelines IL, customer C WHERE C.customerno = I.customer AND I.invoiceno = IL.invoiceno AND I.customer = '" + txtCusNo.Text + "'";
                DataSet ds_invoice = middle_access.db_access.SelectData(q);
                if (ds_invoice != null)
                {
                    // int i = 0;
                    for (int i = 0; i < ds_invoice.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr_invoice = ds_invoice.Tables[0].Rows[i];
                        invoice_no = dr_invoice.ItemArray.GetValue(0).ToString();
                        invoice_date = dr_invoice.ItemArray.GetValue(1).ToString();

                        //string customer_name = dr_invoice.ItemArray.GetValue(2).ToString();
                        string item_no = dr_invoice.ItemArray.GetValue(3).ToString();
                        string item_name = dr_invoice.ItemArray.GetValue(4).ToString();
                        string qty = dr_invoice.ItemArray.GetValue(5).ToString();
                        string unit_price = dr_invoice.ItemArray.GetValue(6).ToString();
                        string price = dr_invoice.ItemArray.GetValue(7).ToString();

                        string total_price = dr_invoice.ItemArray.GetValue(8).ToString();
                        //string discount = dr_invoice.ItemArray.GetValue(9).ToString();
                        //string refNo = dr_invoice.ItemArray.GetValue(10).ToString();
                        //string vehicleNo = dr_invoice.ItemArray.GetValue(11).ToString();

                        /*if (discount == "")
                        {
                            discount = " ";
                            price = ((Convert.ToDouble(qty) * Convert.ToDouble(unit_price)) / 100 * (100 - 0)).ToString();
                        }
                        else if (discount != " ")
                        {
                            price = ((Convert.ToDouble(qty) * Convert.ToDouble(unit_price)) / 100 * (100 - Convert.ToDouble(discount))).ToString();
                            discount = discount + " %";

                        }
                        */
                        //if (i == 0)
                        //{
                        txt += item_name + "     - Qty: " + qty + " - Unit Price: " + unit_price + Environment.NewLine;
                        //    string q3 = "INSERT INTO temp_invoice_print_all(invoiceNo,invoiceDate,invoiceLinesText)VALUES('" + invoice_no + "','" + invoice_date + "','" + txt + "')";
                        //    status = middle_access.db_access.InsertData(q3);
                        //    txt = "";
                        //}
                        //else
                        //{
                        for (int j = i + 1; j < ds_invoice.Tables[0].Rows.Count; j++)
                        {
                            DataRow dr_invoice_ = ds_invoice.Tables[0].Rows[j];
                            string invoiceNo = dr_invoice_.ItemArray.GetValue(0).ToString();
                            if (invoice_no == invoiceNo)
                            {
                                txt += dr_invoice_.ItemArray.GetValue(4).ToString() + " -   Qty: " + dr_invoice_.ItemArray.GetValue(5).ToString() + " - Unit Price: " + dr_invoice_.ItemArray.GetValue(6).ToString() + Environment.NewLine;
                            }
                        }
                        string q3 = "INSERT INTO temp_invoice_print_all(invoiceNo,invoiceDate,invoiceLinesText,total)VALUES('" + invoice_no + "','" + invoice_date + "','" + txt + "','" + total_price + "')";
                        status = middle_access.db_access.InsertData(q3);
                        txt = "";
                        //}

                        //if (i!=0 && previous == invoice_no)
                        //{                            

                        //}
                        //else {
                        //    txt += item_name + " - Qty: " + qty + " - Unit Price: " + unit_price + Environment.NewLine;
                        //    if (ds_invoice.Tables[0].Rows.Count == i+1)
                        //    {
                        //        string q3 = "INSERT INTO temp_invoice_print_all(invoiceNo,invoiceDate,invoiceLinesText)VALUES('" + invoice_no + "','" + invoice_date + "','" + txt + "')";
                        //        status = middle_access.db_access.InsertData(q3);
                        //    }
                        //}
                        //    previous = invoice_no;
                        //        }

                        //    }

                    }

                    //string q3 = "INSERT INTO temp_invoice_print(invoice_no,invoice_date,customer_name,item_no,item_name,qty,unit_price,price,total_price,discount,refNo,vehicleNo)VALUES('" + invoice_no + "','" + invoice_date + "','" + customer_name + "','" + item_no + "','" + item_name + "','" + qty + "','" + unit_price + ".00','" + price + ".00','" + total_price + ".00','" + discount + "','" + refNo + "','" + vehicleNo + "')";
                    //status = middle_access.db_access.InsertData(q3);
                    // }

                }
                GlobleAccess.cusID = txtCusNo.Text;
                Payments_Report pr = new Payments_Report();
                pr.MdiParent = DHNAULA.ActiveForm;
                pr.Show();
            }
        }



        private void testCMB_TextChanged(object sender, EventArgs e)
       {
            if (ds_name != null)
            {
                //foreach (DataRow dr in ds_name.Tables[0].Rows)
                //{
                    int index = testCMB.FindString(testCMB.Text.Trim());
           
                //}             
                //testCMB.DroppedDown = Enabled;
            }
        }

        /// <summary>
        /// Loads the customr combo.
        /// </summary>
        public void loadCustomrCombo()
        {
            try
            {
                //string name = testCMB.Text;
                string q1 = "SELECT customername FROM customer";
                ds_name = middle_access.db_access.SelectData(q1);
                
                testCMB.DisplayMember = "customername";
                testCMB.DataSource = ds_name.Tables[0].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
