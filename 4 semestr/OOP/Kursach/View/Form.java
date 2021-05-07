package View;

import javax.swing.*;

public class Form extends JFrame
{

    private JPanel mainPanel;
    private JTable mainTable;

    public Form()
    {
        setContentPane(mainPanel);
        setVisible(true);

        setDefaultCloseOperation(WindowConstants.EXIT_ON_CLOSE);
    }

    public static void main(String[] args)
    {
        new Form();
    }
}
