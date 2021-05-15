package View;

import javax.swing.*;
import javax.swing.table.DefaultTableModel;
import javax.swing.table.TableModel;
import java.awt.*;
import java.awt.event.ContainerAdapter;
import java.awt.event.ContainerEvent;
import Models.RepairCompany;

public class Form extends JFrame
{

    private String[] machineColumnName = new String[]{"Номер станка", "Бренд",
            "Год производства", "Страна производитель"};
    private DefaultTableModel tableModel = new DefaultTableModel(machineColumnName, 0);
    private JPanel mainPanel;
    private JTable table1;
    private JButton button1;


    public Form()
    {

        RepairCompany repairCompany = new RepairCompany();
        repairCompany.connect();
        Object[][] arr = new Object[repairCompany.getMachines().size()][];

        for (int i = 0; i < repairCompany.getMachines().size(); i++)
        {
            int id = repairCompany.getMachines().get(i).getMachineId();
            String brand = repairCompany.getMachines().get(i).getBrand();
            int yearOfIssue = repairCompany.getMachines().get(i).getYearOfIssue();
            String prodCountry = repairCompany.getMachines().get(i).getProducingCountry();
            Object[] data = {id, brand, yearOfIssue, prodCountry};
            arr[i] = data;

        }

        JFrame frame = new JFrame("RepairCompany");

        mainPanel = new JPanel();
        mainPanel.setLayout(new BorderLayout());

        table1 = new JTable(arr, machineColumnName);

        JScrollPane tableContainer = new JScrollPane(table1);

        mainPanel.add(tableContainer, BorderLayout.CENTER);
        frame.getContentPane().add(mainPanel);

        frame.pack();
        frame.setVisible(true);
        frame.setDefaultCloseOperation(WindowConstants.EXIT_ON_CLOSE);


//        table1 = new JTable(tableModel);
//        table1 = new JTable(arr, machineColumnName);
//        JScrollPane scrollPane = new JScrollPane(table1);
//        table1.setFillsViewportHeight(true);
//        table1 = new JTable(tableModel);

    }


    public static void main(String[] args)
    {
        new Form();
    }


    private void createUIComponents() {
        // TODO: place custom component creation code here
    }
}
