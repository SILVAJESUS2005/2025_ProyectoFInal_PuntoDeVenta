
using System;
using System.Windows.Forms;

public class DataGridViewNumericUpDownColumn : DataGridViewColumn
{
    public decimal Minimum { get; set; }
    public decimal Maximum { get; set; }
    public decimal Increment { get; set; }
    public int DecimalPlaces { get; set; }
    public decimal Value { get; set; } // Nueva propiedad Value

    public DataGridViewNumericUpDownColumn() : base(new DataGridViewNumericUpDownCell())
    {
        Minimum = 0;
        Maximum = 100;
        Increment = 1;
        DecimalPlaces = 0;
        Value = 0;
    }

    public override object Clone()
    {
        var col = (DataGridViewNumericUpDownColumn)base.Clone();
        col.Minimum = this.Minimum;
        col.Maximum = this.Maximum;
        col.Increment = this.Increment;
        col.DecimalPlaces = this.DecimalPlaces;
        col.Value = this.Value;
        return col;
    }

    public override DataGridViewCell CellTemplate
    {
        get => base.CellTemplate;
        set
        {
            if (value != null && !value.GetType().IsAssignableFrom(typeof(DataGridViewNumericUpDownCell)))
                throw new InvalidCastException("Debe ser una celda de tipo DataGridViewNumericUpDownCell.");
            base.CellTemplate = value;
        }
    }
}

public class DataGridViewNumericUpDownCell : DataGridViewTextBoxCell
{
    public DataGridViewNumericUpDownCell() : base()
    {
        this.Style.Format = "N2";
    }

    public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
    {
        base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
        NumericUpDownEditingControl ctl = DataGridView.EditingControl as NumericUpDownEditingControl;

        var owningColumn = this.OwningColumn as DataGridViewNumericUpDownColumn;

        ctl.Minimum = owningColumn.Minimum;
        ctl.Maximum = owningColumn.Maximum;
        ctl.Increment = owningColumn.Increment;
        ctl.DecimalPlaces = owningColumn.DecimalPlaces;
        ctl.Value = (this.Value == null || this.Value == DBNull.Value) ? owningColumn.Value : Convert.ToDecimal(this.Value);
    }

    public override Type EditType => typeof(NumericUpDownEditingControl);

    public override Type ValueType => typeof(decimal);

    public override object DefaultNewRowValue => 0m;
}

class NumericUpDownEditingControl : NumericUpDown, IDataGridViewEditingControl
{
    private DataGridView dataGridView;
    private bool valueChanged = false;
    private int rowIndex;

    public NumericUpDownEditingControl()
    {
        this.DecimalPlaces = 0;
        this.Minimum = 0;
        this.Maximum = 100;
    }

    public object EditingControlFormattedValue
    {
        get => this.Value.ToString("N" + this.DecimalPlaces);
        set
        {
            if (decimal.TryParse(value?.ToString(), out decimal result))
                this.Value = result;
        }
    }

    public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
    {
        return EditingControlFormattedValue;
    }

    public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
    {
        this.Font = dataGridViewCellStyle.Font;
        this.ForeColor = dataGridViewCellStyle.ForeColor;
        this.BackColor = dataGridViewCellStyle.BackColor;
    }

    public int EditingControlRowIndex
    {
        get => rowIndex;
        set => rowIndex = value;
    }

    public bool EditingControlWantsInputKey(Keys key, bool dataGridViewWantsInputKey)
    {
        switch (key & Keys.KeyCode)
        {
            case Keys.Left:
            case Keys.Right:
            case Keys.Up:
            case Keys.Down:
            case Keys.Home:
            case Keys.End:
                return true;
            default:
                return !dataGridViewWantsInputKey;
        }
    }

    public void PrepareEditingControlForEdit(bool selectAll)
    {
        if (selectAll) this.Select(0, this.Text.Length);
    }

    public bool RepositionEditingControlOnValueChange => false;

    public DataGridView EditingControlDataGridView
    {
        get => dataGridView;
        set => dataGridView = value;
    }

    public bool EditingControlValueChanged
    {
        get => valueChanged;
        set => valueChanged = value;
    }

    public Cursor EditingPanelCursor => base.Cursor;

    protected override void OnValueChanged(EventArgs e)
    {
        base.OnValueChanged(e);
        valueChanged = true;
        EditingControlDataGridView?.NotifyCurrentCellDirty(true);
    }
}
