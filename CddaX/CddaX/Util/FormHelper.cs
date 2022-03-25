using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace CddaX.Util
{
    public class FormHelper
    {
        private FormHelper() { }

        public static void PadLabelToEdit(Control l)
        {
            Padding p = l.Padding;
            p.Top = 3;
            l.Padding = p;
        }

        public static void TopAlignCheckboxToEdit(Control checkbox, Control edit)
        {
            var bb = checkbox as ButtonBase;
            if (bb != null && bb.FlatStyle == FlatStyle.System)
            {
                // FIXME! this is more like alchemy than science
                checkbox.Top = edit.Top + 1;
            }
            else
            {
                checkbox.Top = edit.Top + 2;
            }
        }

        public static void TopAlignCheckboxToEditByMargin(Control checkbox, Control edit)
        {
            var bb = checkbox as ButtonBase;
            var margin = checkbox.Margin;
            if (bb != null && bb.FlatStyle == FlatStyle.System)
            {
                // FIXME! this is more like alchemy than science
                margin.Top = edit.Margin.Top + 1;
            }
            else
            {
                margin.Top = edit.Margin.Top + 2;
            }
            checkbox.Margin = margin;
        }

        public static void TopAlignEditToCheckbox(Control edit, Control checkbox)
        {
            var bb = checkbox as ButtonBase;
            if (bb != null && bb.FlatStyle == FlatStyle.System)
            {
                // FIXME! this is more like alchemy than science
                edit.Top = checkbox.Top - 1;
            }
            else
            {
                edit.Top = checkbox.Top - 2;
            }
        }

        public static void TopAlignEditToCheckboxByMargin(Control edit, Control checkbox)
        {
            var bb = checkbox as ButtonBase;
            var margin = edit.Margin;
            if (bb != null && bb.FlatStyle == FlatStyle.System)
            {
                // FIXME! this is more like alchemy than science
                margin.Top = checkbox.Margin.Top - 1;
            }
            else
            {
                margin.Top = checkbox.Margin.Top - 2;
            }
            edit.Margin = margin;
        }

        public static void TopAlignComboToCheckbox(Control combo, Control checkbox)
        {
            // FIXME! this is more like alchemy than science
            combo.Top = checkbox.Top - 2;
        }

        public static void TopAlignComboToCheckboxByMargin(Control combo, Control checkbox)
        {
            // FIXME! this is more like alchemy than science
            Padding margin = combo.Margin;
            margin.Top = checkbox.Margin.Top - 2;
            combo.Margin = margin;
        }

        public static void AutoPadAllLabelsToEdits(Control container)
        {
            foreach (Control child in container.Controls)
            {
                if (child is Label)
                {
                    foreach (Control maybeEdit in container.Controls)
                    {
                        if ((maybeEdit is TextBox || maybeEdit is ComboBox)
                            && maybeEdit.Top == child.Top
                            && child.TabIndex + 1 == maybeEdit.TabIndex)
                        {
                            PadLabelToEdit(child);
                            break;
                        }
                    }
                }

                AutoPadAllLabelsToEdits(child);
            }
        }

        public static void SetToolstripIcon(ToolStripItem item, Icon icon)
        {
            using (Icon i = new Icon(icon, SystemInformation.SmallIconSize))
            {
                item.Image = i.ToBitmap();
                item.ImageScaling = ToolStripItemImageScaling.None;
            }
        }

        public static void MakeToolStripBold(ToolStripItem item)
        {
            Font f = new Font(item.Font, FontStyle.Bold);
            item.Font = f;
        }

        public static void ActivateSegoeUi(Control form)
        {
            if (OSHelper.IsVistaOrLater)
            {
                // Only use Segoe UI on Vista+, even if it is present
                // on older machines

                Font font = new Font("Segoe UI", 9);
                if ("Segoe UI".Equals(font.Name, StringComparison.InvariantCultureIgnoreCase))
                {
                    // only do this if the font is actually present, which might
                    // not be the case in Wine or some other weird system
                    form.Font = font;
                }
            }
        }

        private static Color HeaderLabelColor()
        {
            try
            {
                // vista+ header style
                var vsr = new VisualStyleRenderer("TEXTSTYLE", 1, 0);
                return vsr.GetColor(ColorProperty.TextColor);
            }
            catch (Exception)
            {
            }

            try
            {
                // running on XP? try the GroupBox header color
                var vsr = new VisualStyleRenderer(VisualStyleElement.Button.GroupBox.Normal.ClassName,
                                                    VisualStyleElement.Button.GroupBox.Normal.Part,
                                                    (int)GroupBoxState.Normal);
                return vsr.GetColor(ColorProperty.TextColor);
            }
            catch (Exception)
            {
            }

            // still nothing? use normal control color
            return SystemColors.ControlText;
        }

        public static void FormatHeaderLabel(params Control[] labels)
        {
            Color c = HeaderLabelColor();

            foreach (Control l in labels)
            {
                l.ForeColor = c;
                l.Font = new Font(l.Font.FontFamily, 11);
            }
        }

        public static void MakeFormWidthSymmetric(Control form, Control leftmostControl, Control rightmostControl)
        {
            int clientLeft = form.PointToScreen(new Point(0, 0)).X;
            int controlLeft = leftmostControl.Parent.PointToScreen(leftmostControl.Location).X;
            int controlRight = rightmostControl.Parent.PointToScreen(rightmostControl.Location).X + rightmostControl.Width;

            int windowBorder = form.Width - form.ClientSize.Width;
            int controlWidth = controlRight - controlLeft;
            int padding = controlLeft - clientLeft;

            form.Width = controlWidth + 2 * padding + windowBorder;
        }

        public static Control RightmostControl(params Control[] list)
        {
            Control rightest = null;
            int rightestRight = 0;

            foreach (Control c in list)
            {
                int controlRight = c.Parent.PointToScreen(c.Location).X + c.Width;
                if (controlRight > rightestRight)
                {
                    rightest = c;
                    rightestRight = controlRight;
                }
            }

            return rightest;
        }

        public static void VerticallyCenter(Control subject, Control reference)
        {
            subject.Top = reference.Top + (reference.Height - subject.Height) / 2;
        }

        public static void VerticallyCenterByMargin(Control subject, Control reference)
        {
            var m = subject.Margin;
            m.Top = reference.Margin.Top + (reference.Height - subject.Height) / 2;
            subject.Margin = m;
        }
    }
}
