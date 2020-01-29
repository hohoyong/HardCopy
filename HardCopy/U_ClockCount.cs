namespace HardCopy
{
    using System;
    using System.Windows.Forms;
    public partial class U_ClockCount : UserControl
    {

        public U_ClockCount()
        {
            InitializeComponent();
        }
        decimal _value;
        public decimal CLOCK
        {
            get
            {
                return _value;
            }
            set
            {
                numericUpDown1.Value = value;
                _value = numericUpDown1.Value;
            }
        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            _value = numericUpDown1.Value;
        }
    }
}
