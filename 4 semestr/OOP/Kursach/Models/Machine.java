package Models;

import java.sql.*;

public class Machine
{
    private int machineId;
    private String producingCountry;
    private int yearOfIssue;
    private String brand;


    public int getMachineId()
    {
        return machineId;
    }

    public String getProducingCountry()
    {
        return producingCountry;
    }

    public void setMachineId(int machineId)
    {
        this.machineId = machineId;
    }

    public void setProducingCountry(String producingCountry)
    {
        this.producingCountry = producingCountry;
    }

    public void setYearOfIssue(int yearOfIssue)
    {
        this.yearOfIssue = yearOfIssue;
    }

    public void setBrand(String brand)
    {
        this.brand = brand;
    }

    public int getYearOfIssue()
    {
        return yearOfIssue;
    }

    public String getBrand()
    {
        return brand;
    }


}
