package ViewModel;

import View.Form;
import View.SecondForm;

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

public class ShowSecondFormButtonListener implements ActionListener
{

    Form form;
    public void setForm2(Form refForm )
    {
        form = refForm;
    }
    @Override
    public void actionPerformed(ActionEvent e)
    {
        form.showSecondForm();
    }
}
