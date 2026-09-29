using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace QuemPegouOVeiculo.Shared.Presentation
{
    internal static class DesktopTheme
    {
        internal static readonly Color Background = Color.FromArgb(244, 247, 251);
        internal static readonly Color Surface = Color.White;
        internal static readonly Color Ink = Color.FromArgb(29, 43, 62);
        internal static readonly Color Muted = Color.FromArgb(98, 111, 130);
        internal static readonly Color Accent = Color.FromArgb(32, 96, 191);
        private static readonly Color Border = Color.FromArgb(210, 219, 231);
        private static readonly Font BodyFont = new Font("Segoe UI", 9F);
        private static readonly Font ButtonFont = new Font("Segoe UI", 9F, FontStyle.Bold);

        internal static void Apply(Form form)
        {
            form.SuspendLayout();
            try
            {
                form.AutoScaleMode = AutoScaleMode.None;
                form.Font = BodyFont;
                form.BackColor = Background;

                if (form.IsMdiContainer)
                {
                    StyleShell(form);
                    return;
                }

                var controls = form.Controls.Cast<Control>().ToArray();
                foreach (var control in controls)
                    StyleControl(control);

                ArrangeContent(form, controls);
            }
            finally
            {
                form.ResumeLayout(true);
            }
        }

        private static void ArrangeContent(Form form, Control[] controls)
        {
            var originalSize = form.ClientSize;
            var grid = controls.OfType<DataGridView>().FirstOrDefault();
            var hasGrid = grid != null;
            var width = hasGrid ? Math.Max(originalSize.Width + 28, 680) : originalSize.Width + 28;
            var height = hasGrid ? Math.Max(originalSize.Height + 96, 560) : originalSize.Height + 84;

            form.ClientSize = new Size(width, height);
            form.TopMost = false;

            if (hasGrid)
            {
                form.FormBorderStyle = FormBorderStyle.Sizable;
                form.MaximizeBox = true;
                form.MinimizeBox = true;
                form.MinimumSize = form.Size;
            }

            foreach (var control in controls)
            {
                control.Location = new Point(control.Left + 14, control.Top + 68);

                if (hasGrid && control is Button && IsEditAction(control.Name))
                {
                    control.Left = width - 14 - 96;
                    control.Width = 96;
                    control.Height = 26;
                }

                if (hasGrid && control.Name.StartsWith("TxtPesquisa", StringComparison.OrdinalIgnoreCase))
                {
                    control.Width = width - control.Left - 14;
                    control.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                }
            }

            if (hasGrid)
            {
                grid.Width = width - grid.Left - 14;
                grid.Height = height - grid.Top - 14;
                grid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

                var visibleColumns = grid.Columns.Cast<DataGridViewColumn>()
                    .Where(column => column.Visible).ToArray();
                if (visibleColumns.Length > 0 && visibleColumns.Sum(column => column.Width) < grid.Width - 20)
                    visibleColumns.Last().AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            var header = new Panel
            {
                Dock = DockStyle.Top,
                Height = 55,
                BackColor = Surface,
                TabStop = false
            };
            var title = new Label
            {
                AutoSize = true,
                Text = form.Text,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Ink,
                Location = new Point(18, 13)
            };
            var line = new Panel { Dock = DockStyle.Bottom, Height = 1, BackColor = Border };
            header.Controls.Add(title);
            header.Controls.Add(line);
            form.Controls.Add(header);
            header.BringToFront();
        }

        private static bool IsEditAction(string name)
        {
            return name == "BtnGravar" || name == "BtnAlterar" || name == "BtnExcluir";
        }

        private static void StyleControl(Control control)
        {
            control.Font = BodyFont;

            if (control is Button button)
                StyleButton(button);
            else if (control is DataGridView grid)
                StyleGrid(grid);
            else if (control is TextBoxBase textBox)
            {
                textBox.BackColor = Surface;
                textBox.ForeColor = Ink;
                textBox.BorderStyle = BorderStyle.FixedSingle;
            }
            else if (control is ComboBox combo)
            {
                combo.BackColor = Surface;
                combo.ForeColor = Ink;
                combo.FlatStyle = FlatStyle.Flat;
            }
            else if (control is GroupBox group)
            {
                group.ForeColor = Ink;
                group.BackColor = Background;
            }
            else if (control is Label label)
                label.ForeColor = label.Name.StartsWith("Lbl", StringComparison.Ordinal) ? Ink : Muted;
            else if (control is UserControl || control is Panel)
                control.BackColor = Background;

            foreach (Control child in control.Controls)
                StyleControl(child);
        }

        internal static void StyleButton(Button button)
        {
            var primary = button.Name == "BtnGravar" || button.Name == "BtnListar";
            var destructive = button.Name == "BtnExcluir";
            button.Font = ButtonFont;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = primary ? 0 : 1;
            button.FlatAppearance.BorderColor = destructive ? Color.FromArgb(232, 185, 185) : Border;
            button.BackColor = primary ? Accent : Surface;
            button.ForeColor = primary ? Surface : destructive ? Color.FromArgb(160, 55, 55) : Ink;
            button.Cursor = Cursors.Hand;
        }

        private static void StyleGrid(DataGridView grid)
        {
            grid.BackgroundColor = Surface;
            grid.BorderStyle = BorderStyle.None;
            grid.RowHeadersVisible = false;
            grid.EnableHeadersVisualStyles = false;
            grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            grid.GridColor = Color.FromArgb(232, 237, 244);
            grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            grid.ColumnHeadersHeight = 34;
            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(37, 54, 78);
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Surface;
            grid.ColumnHeadersDefaultCellStyle.Font = ButtonFont;
            grid.ColumnHeadersDefaultCellStyle.Padding = new Padding(7, 0, 0, 0);
            grid.DefaultCellStyle.BackColor = Surface;
            grid.DefaultCellStyle.ForeColor = Ink;
            grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(224, 237, 255);
            grid.DefaultCellStyle.SelectionForeColor = Ink;
            grid.DefaultCellStyle.Padding = new Padding(7, 2, 7, 2);
            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(247, 249, 252);
            grid.RowTemplate.Height = 29;
            foreach (DataGridViewColumn column in grid.Columns)
                column.HeaderText = column.HeaderText.Replace('_', ' ');
        }

        private static void StyleShell(Form form)
        {
            form.WindowState = FormWindowState.Maximized;
            foreach (Control control in form.Controls)
            {
                if (control is MdiClient mdi)
                    mdi.BackColor = Background;
                else if (control is MenuStrip menu)
                {
                    menu.AutoSize = false;
                    menu.Height = 48;
                    menu.Padding = new Padding(16, 5, 8, 5);
                    menu.Font = new Font("Segoe UI", 10F);
                    menu.BackColor = Color.FromArgb(29, 43, 62);
                    menu.ForeColor = Surface;
                    menu.Renderer = new MenuRenderer();
                    foreach (ToolStripMenuItem item in menu.Items)
                    {
                        item.Padding = new Padding(10, 6, 10, 6);
                        item.ForeColor = Surface;
                        StyleMenuChildren(item);
                    }
                }
                else if (control is StatusStrip status)
                {
                    status.Font = BodyFont;
                    status.BackColor = Surface;
                    status.ForeColor = Muted;
                    status.Padding = new Padding(12, 3, 12, 3);
                    status.SizingGrip = false;
                }
            }
        }

        private static void StyleMenuChildren(ToolStripMenuItem parent)
        {
            foreach (ToolStripMenuItem item in parent.DropDownItems.OfType<ToolStripMenuItem>())
            {
                item.ForeColor = Ink;
                item.Padding = new Padding(8, 4, 8, 4);
                StyleMenuChildren(item);
            }
        }

        private sealed class MenuColors : ProfessionalColorTable
        {
            public override Color MenuItemSelected => Color.FromArgb(49, 78, 115);
            public override Color MenuItemBorder => Color.FromArgb(49, 78, 115);
            public override Color MenuItemSelectedGradientBegin => MenuItemSelected;
            public override Color MenuItemSelectedGradientEnd => MenuItemSelected;
            public override Color MenuItemPressedGradientBegin => MenuItemSelected;
            public override Color MenuItemPressedGradientEnd => MenuItemSelected;
            public override Color ToolStripDropDownBackground => Surface;
            public override Color ImageMarginGradientBegin => Surface;
            public override Color ImageMarginGradientMiddle => Surface;
            public override Color ImageMarginGradientEnd => Surface;
        }

        private sealed class MenuRenderer : ToolStripProfessionalRenderer
        {
            public MenuRenderer() : base(new MenuColors()) { }

            protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
            {
                var topLevel = e.Item.Owner is MenuStrip;
                var color = topLevel
                    ? e.Item.Selected ? Color.FromArgb(49, 78, 115) : Color.FromArgb(29, 43, 62)
                    : e.Item.Selected ? Color.FromArgb(231, 240, 252) : Surface;

                using (var brush = new SolidBrush(color))
                    e.Graphics.FillRectangle(brush, new Rectangle(Point.Empty, e.Item.Size));
            }
        }
    }
}
