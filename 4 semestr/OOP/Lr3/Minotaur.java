public class Minotaur implements Unit {

    @Override
    public String getName() {
        return "Минотавр";
    }

    @Override
    public int getX() {
        return 13;
    }

    @Override
    public int getY() {
        return 14;
    }

    @Override
    public void move(int x, int y) {
        System.out.println("Минотавр идет из " + x + " в " + y);
    }

    @Override
    public String shoot() {
        return "Минотавр выстрелил";
    }
}
