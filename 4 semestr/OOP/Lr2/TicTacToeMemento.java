public class TicTacToeMemento
{
    private char[][] array = new char[3][3];

    public TicTacToeMemento(TicTacToe ticTacToe)
    {
        setArray(ticTacToe.getArray());
    }

    public void setArray(char[][] arr)
    {
        this.array = new char[arr.length][arr.length];
        for(int i = 0; i < array.length; i++)
        {
            System.arraycopy(arr[i], 0,array[i], 0, arr[i].length);
        }
    }

    public char[][] getArray()
    {
        return array;
    }
}
