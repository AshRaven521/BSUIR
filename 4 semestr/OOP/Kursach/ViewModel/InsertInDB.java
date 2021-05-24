package ViewModel;

import View.Form;
import View.SecondForm;

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

public class InsertInDB implements ActionListener
{
    SecondForm form2;
    //Form form;
    public void setForm(SecondForm refForm2)
    {
        form2 = refForm2;
        //form = refForm;
    }
    @Override
    public void actionPerformed(ActionEvent e)
    {
        form2.connectForChanges();
        //form.updateTable();
    }
}
