package View;

import javax.swing.*;
import javax.swing.table.DefaultTableModel;
import javax.swing.table.TableModel;
import java.awt.*;
import java.sql.*;

import Models.RepairCompany;
import ViewModel.DeleteFromDB;
import ViewModel.ShowSecondFormButtonListener;
import ViewModel.ShowTableButtonListener;
import ViewModel.UpdateButtonListener;


public class Form extends JFrame
{

    private String[] orderColumnName = new String[]{"Номер заказа", "Наименование",
            "Продолжительность", "Цена", "Клиент"};
    private String[] machineColumnName = new String[]{"Номер станка", "Страна производитель", "Год производства", "Бренд"};
    private JPanel mainPanel;
    private JTable table1;
    private JButton showTableButton;
    private JButton showSecondFormButton;
    private JButton deleteButton;
    private JButton updateFormButton;
    private JTable table2;
    RepairCompany repairCompany = new RepairCompany();
    ShowSecondFormButtonListener secondFormListener;

    private Connection connection;
    private Statement state;
    private int result;

    private DefaultTableModel tableModel;
    private DefaultTableModel tableModel2;

    public Form()
    {

    }

    public void createForm()
    {
        tableModel = new DefaultTableModel();
        tableModel2 = new DefaultTableModel();
        repairCompany.connect();
        repairCompany.connectToMachinesTable();
        Object[][] ordersArr = new Object[repairCompany.getOrders().size()][];

        for (int i = 0; i < repairCompany.getOrders().size(); i++)
        {
            int repairId = repairCompany.getOrders().get(i).getRepairOrderId();
            String name = repairCompany.getOrders().get(i).getName();
            String duration = repairCompany.getOrders().get(i).getDuration();
            int cost = repairCompany.getOrders().get(i).getCost();
            String clientName = repairCompany.getOrders().get(i).getClientName();
            Object[] data = {repairId, name, duration, cost, clientName};
            ordersArr[i] = data;
        }

        tableModel.setColumnIdentifiers(orderColumnName);
        for(int i=0; i<ordersArr.length; i++)
            tableModel.addRow(ordersArr[i]);
        table1 = new JTable(tableModel);
        JScrollPane scroll1 = new JScrollPane(table1);

        showTableButton = new JButton("Отобразить таблицу");
        ShowTableButtonListener listener = new ShowTableButtonListener();
        listener.setForm(this);
        showTableButton.addActionListener(listener);

        showSecondFormButton = new JButton("Внести изменения в заказы");
        secondFormListener = new ShowSecondFormButtonListener();
        showSecondFormButton.addActionListener(secondFormListener);
        secondFormListener.setForm2(this);

        updateFormButton = new JButton("Обновить");
        UpdateButtonListener updateListener = new UpdateButtonListener();
        updateFormButton.addActionListener(updateListener);
        updateListener.setForm(this);

        deleteButton = new JButton("Удалить");
        DeleteFromDB deleteListener = new DeleteFromDB();
        deleteButton.addActionListener(deleteListener);
        deleteListener.setForm(this);

        mainPanel = new JPanel();
        mainPanel.setLayout(new BorderLayout());
        mainPanel.add(scroll1, BorderLayout.CENTER);
        mainPanel.add(showTableButton, BorderLayout.AFTER_LAST_LINE);
        mainPanel.add(showSecondFormButton, BorderLayout.NORTH);
        mainPanel.add(deleteButton, BorderLayout.BEFORE_LINE_BEGINS);
        mainPanel.add(updateFormButton, BorderLayout.EAST);

        JFrame frame = new JFrame("RepairCompany");
        frame.setDefaultCloseOperation(WindowConstants.EXIT_ON_CLOSE);
        frame.add(mainPanel);
        frame.setSize(500, 500);
        frame.setLocationRelativeTo(null);
        frame.setVisible(true);
        frame.setExtendedState(MAXIMIZED_BOTH);
    }

    public static void main(String[] args)
    {
        Form form = new Form();
        form.createForm();
    }

    public void showTable()
    {
        Object[][] machinesArr = new Object[repairCompany.getMachines().size()][];

        int selectIndex = table1.getSelectedRow();
        TableModel tableModel = table1.getModel();
        Object value = tableModel.getValueAt(selectIndex, 0);

        for (int j = 0; j < repairCompany.getMachines().size(); j++)
        {
            int temp = repairCompany.getMachines().get(j).getOrderId();
            if(temp == (int)value)
            {
                int machineId = repairCompany.getMachines().get(j).getMachineId();
                String prodCountry = repairCompany.getMachines().get(j).getProducingCountry();
                int year = repairCompany.getMachines().get(j).getYearOfIssue();
                String brand = repairCompany.getMachines().get(j).getBrand();
                Object[] machineData = {machineId, prodCountry, year, brand};
                machinesArr[j] = machineData;
            }
        }

        tableModel2.setColumnIdentifiers(machineColumnName);
        tableModel2.setRowCount(0);
        for(int i=0; i<machinesArr.length; i++)
        {
            if(machinesArr[i] != null)
            {
                tableModel2.addRow(machinesArr[i]);
            }

        }
        table2 = new JTable(tableModel2);

        JScrollPane scroll2 = new JScrollPane(table2);
        mainPanel.add(scroll2, BorderLayout.EAST);
        mainPanel.updateUI();
    }

    public void showSecondForm()
    {
        SecondForm secondForm = new SecondForm();
        secondForm.createSecondForm();
    }


    public void connectToDelete()
    {
        String url = "jdbc:mysql://localhost:3306/oop_kursach";
        String user = "root";
        String password = "0000";

        int selectIndex = table1.getSelectedRow();
        TableModel tableModel = table1.getModel();
        Object temp = tableModel.getValueAt(selectIndex, 0);

        String sqlQuery = "DELETE from repairs_order WHERE " + temp + "= idrepairs_order";

        try
        {
            Class.forName("com.mysql.cj.jdbc.Driver").getDeclaredConstructor().newInstance();
            connection = DriverManager.getConnection(url, user, password);
            state = connection.createStatement();
            result =  state.executeUpdate(sqlQuery);

        }
        catch (Exception exception)
        {
            System.out.println("Something goes wrong: " + exception.getStackTrace());
        }
        finally
        {
            try { connection.close(); } catch(SQLException se) { }
            try { state.close(); } catch(SQLException se) { }
            //try { result.close(); } catch(SQLException se) { }
        }

    }

    public void updateTable()
    {
        repairCompany.connect();
        Object[][] ordersArr = new Object[repairCompany.getOrders().size()][];

        for (int i = 0; i < repairCompany.getOrders().size(); i++)
        {
            int repairId = repairCompany.getOrders().get(i).getRepairOrderId();
            String name = repairCompany.getOrders().get(i).getName();
            String duration = repairCompany.getOrders().get(i).getDuration();
            int cost = repairCompany.getOrders().get(i).getCost();
            String clientName = repairCompany.getOrders().get(i).getClientName();
            Object[] data = {repairId, name, duration, cost, clientName};
            ordersArr[i] = data;
        }
        tableModel.setRowCount(0);
        for(int i=0; i<ordersArr.length; i++)
        {
            tableModel.addRow(ordersArr[i]);
        }

        table1.setModel(tableModel);
        mainPanel.updateUI();
    }
}