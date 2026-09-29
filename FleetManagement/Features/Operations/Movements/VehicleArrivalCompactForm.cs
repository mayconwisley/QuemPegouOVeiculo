using FleetManagement.Desktop.Models;
using System;
using System.Windows.Forms;

namespace FleetManagement
{
    public partial class VehicleArrivalCompactForm : FleetManagement.Shared.Presentation.ThemedForm
    {
        VehicleMovementModel movement;
        int movementId;
        DateTime arrivalAt;

        VehicleArrivalForm parentForm = null;

        public VehicleArrivalCompactForm()
        {
            InitializeComponent();
        }

        public VehicleArrivalCompactForm(int movementId)
        {
            InitializeComponent();
            this.movementId = movementId;
        }

        public VehicleArrivalCompactForm(int movementId, VehicleArrivalForm form)
        {
            InitializeComponent();
            parentForm = form;
            this.movementId = movementId;
        }

        private bool HandleOperation(char operation)
        {
            movement = new VehicleMovementModel();
            try
            {
                DateTime.TryParse(MktDtArrival.Text.Trim(), out arrivalAt);
                movement.Id = movementId;

                if (arrivalAt.Date == DateTime.Parse("01/01/0001 00:00:00"))
                {
                    movement.ArrivalAt = null;
                }
                else
                {
                    movement.ArrivalAt = arrivalAt;
                }
                movement.FinalMileage = TxtFinalMileage.Text.Trim();


                switch (operation)
                {
                    case 'U':
                        FleetManagement.Desktop.Client.Features.Operations.Movements.Update.CompleteMovement(movement);
                        return true;
                    default:
                        MessageBox.Show("Opção não encontrada");
                        return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        private void VehicleArrivalCompactForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                parentForm?.LoadRecords();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (HandleOperation('U'))
                Close();
        }

        private void VehicleArrivalCompactForm_Load(object sender, EventArgs e)
        {
            MktDtArrival.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
        }


    }
}
