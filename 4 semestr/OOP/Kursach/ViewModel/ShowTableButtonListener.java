package ViewModel;

import View.Form;

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

public class ShowTableButtonListener implements ActionListener
{
    Form form;

    public void setForm(Form refForm)
    {
        form=refForm;
    }

    @Override
    public void actionPerformed(ActionEvent e)
    {
        form.showTable();
    }
}
