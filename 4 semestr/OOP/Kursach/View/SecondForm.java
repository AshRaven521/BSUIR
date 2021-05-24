package View;

import ViewModel.InsertInDB;

import javax.swing.*;
import java.awt.*;
import java.sql.*;

public class SecondForm
{
    private JPanel changesPanel;
    private JTextField orderNameText;
    private JTextField orderDurationText;
    private JTextField orderCostText;
    private JTextField orderClientText;
    private JLabel orderNameLabel;
    private JLabel orderDurationLabel;
    private JLabel orderCostLabel;
    private JLabel orderClientLabel;
    private JButton buttonOfChanges;

    private Connection connection;
    private Statement state;
    private int result;

    public SecondForm()
    {

    }

    public void createSecondForm()
    {
        orderClientText = new JTextField(15);
        orderCostText = new JTextField(15);
        orderDurationText = new JTextField(15);
        orderNameText = new JTextField(15);

        orderClientLabel = new JLabel("Номер клиента");
        orderClientLabel.setHorizontalAlignment(JLabel.LEFT);
        orderCostLabel = new JLabel("Цена ремонта");
        orderCostLabel.setHorizontalAlignment(JLabel.LEFT);
        orderDurationLabel = new JLabel("Длительность");
        orderDurationLabel.setHorizontalAlignment(JLabel.LEFT);
        orderNameLabel = new JLabel("Наименование заказа");
        orderNameLabel.setHorizontalAlignment(JLabel.LEFT);

        buttonOfChanges = new JButton("Внести изменения");
        InsertInDB changes = new InsertInDB();
        changes.setForm(this);
        buttonOfChanges.addActionListener(changes);

        changesPanel = new JPanel();
        changesPanel.setLayout(new GridBagLayout());
        GridBagConstraints constraints = new GridBagConstraints();
        constraints.gridy = 0;
        constraints.gridx = 0;
        changesPanel.add(orderNameLabel,constraints);
        constraints.gridx = 1;
        changesPanel.add(orderNameText,constraints);
        constraints.gridy = 1;
        constraints.gridx = 0;
        changesPanel.add(orderDurationLabel,constraints);
        constraints.gridx = 1;
        changesPanel.add(orderDurationText,constraints);
        constraints.gridy = 2;
        constraints.gridx = 0;
        changesPanel.add(orderCostLabel,constraints);
        constraints.gridx = 1;
        changesPanel.add(orderCostText,constraints);
        constraints.gridy = 3;
        constraints.gridx = 0;
        changesPanel.add(orderClientLabel,constraints);
        constraints.gridx = 1;
        changesPanel.add(orderClientText,constraints);
        constraints.gridy = 7;
        constraints.gridx = 1;
        changesPanel.add(buttonOfChanges,constraints);

        JFrame frame2 = new JFrame("Repair orders changes");
        frame2.add(changesPanel);
        frame2.setSize(400, 400);
        frame2.setLocationRelativeTo(null);
        frame2.setVisible(true);
    }

    public void connectForChanges()
    {
        String url = "jdbc:mysql://localhost:3306/oop_kursach";
        String user = "root";
        String password = "0000";
        String str = "'" + orderNameText.getText() + "', " + "'" + orderDurationText.getText()
                + "', " + orderCostText.getText() + ", " + orderClientText.getText();
        String sqlQuery = "INSERT INTO repairs_order (name,duration,cost,idclients) VALUE (" + str + ")";

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

}
