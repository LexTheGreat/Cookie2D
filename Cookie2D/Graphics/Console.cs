﻿using System;
using Gwen.Control;
using Gwen.Control.Layout;
using Gwen;

namespace Cookie2D.Windows
{
    public class Console : DockBase
    {
        private readonly ListBox m_TextOutput;
        private TabButton m_Button;
        private readonly CollapsibleList m_List;

        public Console(Base parent) : base(parent)
        {
            IsHidden = true;
            SetSize(800,200);
            SetPosition(0, 400);
            Dock = Pos.Fill;
            m_List = new CollapsibleList(parent);
            m_Button = TopDock.TabControl.AddPage("Console");
            m_TextOutput = new ListBox(m_Button.Page);
            m_TextOutput.Dock = Pos.Fill;
            TextBox textbox = new TextBox(m_Button.Page);
            textbox.Dock = Pos.Bottom;
            textbox.SubmitPressed += OnSubmit;
            PrintText("Console loaded succesfully!");
        }

        public void PrintText(string str)
        {
            m_TextOutput.AddRow(str);
            m_TextOutput.ScrollToBottom();
            this.BringToFront();
        }

        private void OnSubmit(Base control, EventArgs args)
        {
            TextBox box = control as TextBox;
            if (box.Text != "")
            {
                PrintText(box.Text);
                box.Text = "";
            }
        }
    }
}